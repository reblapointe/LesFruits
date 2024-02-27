using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;

namespace LesFruits
{
    public class Program
    {
        static void Main(string[] _)
        {
            var fruits = new List<Fruit>()
            {
                new Fruit() { Nom = "Abricot"},   new Fruit() { Nom = "Banane"},    new Fruit() { Nom = "Cerise"}, new Fruit() { Nom = "Datte"},
                new Fruit() { Nom = "Framboise"}, new Fruit() { Nom = "Grenade"},   new Fruit() { Nom = "Kiwi"},   new Fruit() { Nom = "Lime"},
                new Fruit() { Nom = "Mangue"},    new Fruit() { Nom = "Nectarine"}, new Fruit() { Nom = "Olive"},  new Fruit() { Nom = "Pomme"}
            };

            var personnes = new List<Personne>()
            {
                new Personne() { Nom = "Alice",   Genre = 'F', Age = 22, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[10] } },
                new Personne() { Nom = "Bob",     Genre = 'M', Age = 12, FruitsAimes = new List<Fruit>() { fruits[4], fruits[5], fruits[6], fruits[7], fruits[8] } },
                new Personne() { Nom = "Charlie", Genre = 'M', Age = 31, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[4], fruits[11] } },
                new Personne() { Nom = "Diane",   Genre = 'F', Age = 45, FruitsAimes = new List<Fruit>() { fruits[2], fruits[4] } },
                new Personne() { Nom = "Eve",     Genre = 'F', Age = 4,  FruitsAimes = new List<Fruit>() { } },
            };

            Console.WriteLine("Les fruits qui contiennent la lettre A sont : ");
            IEnumerable<Fruit> reponse = fruits.Where(AvecA);

            Console.WriteLine ($"{string.Join(separator: ", ", values: reponse)}");
            double ageMoyenM = personnes.Where(p => p.Genre == 'M').Average(p => p.Age);
            Console.WriteLine($"Age moyen des hommes {ageMoyenM}");

            var query = personnes.Where(p => p.Genre == 'M').
                            OrderBy(p => p.Age).
                            Select(p => new { p.Age, p.Genre});
            Console.WriteLine($"Age et genre des hommes : {string.Join(separator: ", ", values: query)}");

            var enfants = LesEnfants(personnes);
            Console.WriteLine($"Les enfants : {string.Join(separator: ", ", values: enfants)}");

            Personne aine = LaPlusVieille(personnes);
            Console.WriteLine($"La personne la plus âgée est : {aine}");

            var pop = PlusPopulaire(personnes);
            Console.WriteLine($"Les fruits les plus populaires : {string.Join(separator: ", ", values: pop)}");

            ParGenre(personnes);

            Console.Read();
        }
        public static IEnumerable<Fruit> PlusPopulaire(IEnumerable<Personne> personnes)
        {
            // return personnes.SelectMany(p => p.FruitsAimes).GroupBy(f => f).OrderByDescending(g => g.Count()).Select(g => g.Key);

            return from p in personnes
                   from f in p.FruitsAimes
                   group f by f into fruits
                   orderby fruits.Count() descending
                   select fruits.Key;
        }
        public static IEnumerable<Personne> LesEnfants(IEnumerable<Personne> personnes)
        {
            // return personnes.Where(p => p.Age < 18).Select(p => p);
            return from p in personnes where p.Age < 18 select p;
        }

        public static void ParGenre(IEnumerable<Personne> personnes)
        {
            /*var genres = personnes.GroupBy(p => p.Genre).Select(g => new {
                key = g.Key, 
                count = g.Count(), 
                max = g.Max(p => p.Age), 
                min = g.Min(p => p.Age) });*/

            var genres = from p in personnes
                         group p by p.Genre into g
                         select new
                         {
                             key = g.Key,
                             count = g.Count(),
                             max = g.Max(p => p.Age),
                             min = g.Min(p => p.Age)
                         };

            foreach (var g in genres)
                Console.WriteLine($"Genre : {g.key}\n" +
                    $"Nombre de personnes de ce genre : {g.count}\n" +
                    $"L'âge de la personne la plus vieille de ce genre : {g.max}\n" +
                    $"L'âge de la personne la plus jeune de ce genre : {g.min}\n");

        }


        public static Personne LaPlusVieille(IEnumerable<Personne> personnes)
        {
            // return (from p in personnes orderby p.Age select p).FirstOrDefault();
            return personnes.OrderByDescending(p => p.Age).FirstOrDefault();
        }
        static bool AvecA(Fruit fruit)
        {
            return fruit.Nom.ToUpper().Contains("A");
        }
    }
}

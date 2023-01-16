using System;
using System.Collections.Generic;
using System.Text;

namespace LesFruits
{
    public class Fruit
    {
        public string Nom { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Fruit fruit &&
                   Nom == fruit.Nom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nom);
        }

        public override string ToString()
        {
            return Nom;
        }

    }
}

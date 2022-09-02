using System;
using System.ComponentModel.DataAnnotations;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities
{
    public class ChickenBreed: EntityBase, IIdentified<int>
    {
        public ChickenBreed(
            string name,
            Color primaryColor)
        {
            Name = name;
            PrimaryColor = primaryColor;
        }

        [Obsolete("For EF/tests only.", error: true)]
        protected ChickenBreed() { }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Color PrimaryColor { get; set; }
    }

    public enum Color
    {
        Black = 1,
        Brown = 2,
        White = 3,
        Blonde = 4
    }
}

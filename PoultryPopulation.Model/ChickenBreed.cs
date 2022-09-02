﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PoultryPopulation.Persistence.Models
{
    public class ChickenBreed
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
        public int ChickenBreedId { get; set; }
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

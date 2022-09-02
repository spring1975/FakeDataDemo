using System.Collections.Generic;
using PoultryPopulation.Entities;

namespace PoultryPopulation
{
    public abstract class EntityCollections
    {
        internal IEnumerable<Address> Addresses { get; set; }
        internal IEnumerable<Chicken> Chickens { get; set; }
        internal IEnumerable<ChickenBreed> ChickenBreeds { get; set; }
        internal IEnumerable<ChickenCoop> ChickenCoops { get; set; }
        internal IEnumerable<Owner> Owners { get; set; }

    }
}
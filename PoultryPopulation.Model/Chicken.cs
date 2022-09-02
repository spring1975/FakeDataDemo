using System;
using PoultryPopulation.Persistence.Models;

namespace PoultryPopulation.Model
{
    public class Chicken
    {
        #region POCO
        //public Chicken() { }
        #endregion

        #region Restricted default constructor
        //[Obsolete("For EF/tests only.", error: true)]
        //protected Chicken() { }
        //public Chicken(
        //    string name,
        //    DateTime birthdate,
        //    bool isAdoptable,
        //    int chickenBreedId,
        //    ChickenBreed chickenBreed,
        //    int chickenCoopId,
        //    ChickenCoop chickenCoop,
        //    int? ownerId,
        //    Owner owner)
        //{
        //    Name = name;
        //    IsAdoptable = isAdoptable;
        //    Birthdate = birthdate;
        //    ChickenBreedId = chickenBreedId;
        //    ChickenCoopId = chickenCoopId;
        //    OwnerId = ownerId;
        //}
        #endregion

        [Key]
        public int ChickenId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAdoptable { get; set; }
        public int ChickenBreedId { get; set; }
        [ForeignKey(nameof(ChickenBreedId))]
        public virtual ChickenBreed ChickenBreed { get; set; }
        public int ChickenCoopId { get; set; }
        [ForeignKey(nameof(ChickenCoopId))]
        public virtual ChickenCoop ChickenCoop { get; set; }
        public int? OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual Owner Owner { get; set; }

    }
}

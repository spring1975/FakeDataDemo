using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities
{
    public class Chicken: EntityBase, IIdentified<int>
    {
        #region Parameterless constructor
        public Chicken() { }
        #endregion

        #region Greedy default constructor
        //[Obsolete("For EF/tests only.", error: true)]
        //protected Chicken() { }
        public Chicken(
            string name,
            Gender gender,
            DateTime birthdate,
            bool isAdoptable,
            decimal? adoptionFee,
            int chickenBreedId,
            ChickenBreed chickenBreed,
            int chickenCoopId,
            ChickenCoop chickenCoop,
            int? ownerId)
        {
            Name = name;
            Gender = gender;
            IsAdoptable = isAdoptable;
            AdoptionFee = adoptionFee;
            Birthdate = birthdate;
            ChickenBreedId = chickenBreedId;
            ChickenCoopId = chickenCoopId;
            OwnerId = ownerId;
        }
        #endregion

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAdoptable { get; set; }
        public decimal? AdoptionFee { get; set; }
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

using System;
using System.ComponentModel.DataAnnotations;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities.Abstracts
{
    public abstract class EntityBase : ICreatedModified 
    {
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        [MaxLength(255)]
        public string CreatedBy { get; set; }
        [MaxLength(255)]
        public string ModifiedBy { get; set; }
    }
}

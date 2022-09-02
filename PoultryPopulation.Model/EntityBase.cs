using System;
using System.ComponentModel.DataAnnotations;
using PoultryPopulation.Model.Interfaces;

namespace PoultryPopulation.Persistence.Models
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

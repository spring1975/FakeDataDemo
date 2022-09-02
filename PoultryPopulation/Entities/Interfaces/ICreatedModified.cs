using System;

namespace PoultryPopulation.Entities.Interfaces
{
    public interface ICreatedModified
    {
        DateTimeOffset? Created { get; set; }
        DateTimeOffset? Modified { get; set; }
    }
}

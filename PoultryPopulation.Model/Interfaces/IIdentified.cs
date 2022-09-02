namespace PoultryPopulation.Model.Interfaces
{
    public interface IIdentified<TKey>
    {
        TKey Id { get; set; }
    }
}

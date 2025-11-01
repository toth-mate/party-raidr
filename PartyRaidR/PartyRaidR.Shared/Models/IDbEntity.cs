namespace PartyRaidR.Shared.Models
{
    public interface IDbEntity<TEntity> where TEntity : class, new()
    {
        string Id { get; set; }
        bool HasId => Id is not null && Id != string.Empty && !Id.All(i => i == '0');
        string DbSetName => string.Concat(new TEntity().GetType(), 's');
    }
}

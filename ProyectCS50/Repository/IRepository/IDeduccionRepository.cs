using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface IDeduccionRepository : IRepository<Deduccione>
    {
        Task<Deduccione> UpdateAsync(Deduccione entity);
    }
}

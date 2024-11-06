using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface INominaRepository : IRepository<Nomina>
    {
        Task<Nomina> UpdateAsync(Nomina entity);
    }
}

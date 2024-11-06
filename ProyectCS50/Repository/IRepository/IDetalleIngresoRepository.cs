using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface IDetalleIngresoRepository : IRepository<DetalleIngreso>
    {
        Task<DetalleIngreso> UpdateAsync(DetalleIngreso entity);
    }
}

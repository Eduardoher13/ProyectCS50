using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        Task<Empleado> UpdateAsync(Empleado entity);
    }
}

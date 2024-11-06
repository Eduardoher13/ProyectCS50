using ProyectCS50.Models;
using System.Threading.Tasks;

namespace ProyectCS50.Repository.IRepository
{
    public interface IDetalleDeduccionRepository : IRepository<DetalleDeduccione>
    {
        Task<DetalleDeduccione> UpdateAsync(DetalleDeduccione entity);
    }
}

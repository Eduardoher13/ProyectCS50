using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Repository
{
    public class DetalleDeduccionRepository : Repository<DetalleDeduccione>, IDetalleDeduccionRepository
    {
        private readonly NominaContext _context;

        public DetalleDeduccionRepository(NominaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DetalleDeduccione> UpdateAsync(DetalleDeduccione entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

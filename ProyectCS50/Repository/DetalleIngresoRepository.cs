using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Repository
{
    public class DetalleIngresoRepository : Repository<DetalleIngreso>, IDetalleIngresoRepository
    {

        private readonly NominaContext _context;


        public DetalleIngresoRepository(NominaContext context) : base(context)
        {
            _context = context;

        }

        public async Task<DetalleIngreso> UpdateAsync(DetalleIngreso entity)
        {

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}

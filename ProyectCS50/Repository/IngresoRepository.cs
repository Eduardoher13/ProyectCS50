using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Repository
{
    public class IngresoRepository : Repository<Ingreso>, I_IngresoRepository
    {

        private readonly NominaContext _context;


        public IngresoRepository(NominaContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Ingreso> UpdateAsync(Ingreso entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}

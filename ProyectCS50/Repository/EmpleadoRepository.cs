using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Repository
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        private readonly NominaContext _context;

        public EmpleadoRepository(NominaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Empleado> UpdateAsync(Empleado entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

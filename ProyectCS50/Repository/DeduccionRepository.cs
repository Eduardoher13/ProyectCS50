using Microsoft.EntityFrameworkCore;
using ProyectCS50.Models;
using ProyectCS50.Repository.IRepository;

namespace ProyectCS50.Repository
{
    public class DeduccionRepository : Repository<Deduccione>, IDeduccionRepository
    {
        private readonly NominaContext _context;

        public DeduccionRepository(NominaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deduccione> UpdateAsync(Deduccione entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

  
    }
}

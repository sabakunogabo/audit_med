using AuditMed.Api.Data.Context;
using AuditMed.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditMed.Api.Repositories.AtencionRepository
{
    public class AtencionRepository : IAtencionRepository
    {
        private readonly AuditMedDBContext _context;

        public AtencionRepository(AuditMedDBContext context)
        {
            _context = context;
        }

        public async Task<RegistroAtencion?> GetByIdAsync(int id)
        {
            return await _context.RegistroAtencion.AsNoTracking().FirstOrDefaultAsync(x => x.IdAtencion == id);
        }

        public async Task<IEnumerable<RegistroAtencion>> GetAllAsync()
        {
            return await _context.RegistroAtencion.AsNoTracking().ToListAsync();
        }

        public async Task<RegistroAtencion> AddAsync(RegistroAtencion entity)
        {
            await _context.RegistroAtencion.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RegistroAtencion?> UpdateAsync(RegistroAtencion entity)
        {
            _context.RegistroAtencion.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.RegistroAtencion.FindAsync(id);
            if (entity == null) return false;

            _context.RegistroAtencion.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
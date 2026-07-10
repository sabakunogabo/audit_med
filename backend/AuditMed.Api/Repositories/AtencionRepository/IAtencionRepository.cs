using AuditMed.Api.Data.Entities;

namespace AuditMed.Api.Repositories.AtencionRepository
{
    public interface IAtencionRepository
    {
        Task<RegistroAtencion?> GetByIdAsync(int id);
        Task<IEnumerable<RegistroAtencion>> GetAllAsync();
        Task<RegistroAtencion> AddAsync(RegistroAtencion entity);
        Task<RegistroAtencion?> UpdateAsync(RegistroAtencion entity);
        Task<bool> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
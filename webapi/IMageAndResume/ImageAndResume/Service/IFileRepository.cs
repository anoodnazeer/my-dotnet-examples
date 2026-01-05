using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageAndResume.Models;

namespace ImageAndResume.Service
{
    public interface  IFileRepository
    {
        Task AddAsync(FileDocument entity);
        Task UpdateAsync(FileDocument entity);
        Task<FileDocument?> GetByIdAsync(Guid id);
        Task<List<FileDocument>> GetAllAsync();
        Task DeleteAsync(FileDocument entity);
        Task<bool> SaveChangesAsync();
    }
}

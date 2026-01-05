using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageAndResume.Dto;
using ImageAndResume.Service;
using ImageAndResume.Models;

namespace ImageAndResume.Service
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;


        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(FileDocument entity)
        {
            await _context.FileDocuments.AddAsync(entity);
        }


        public async Task DeleteAsync(FileDocument entity)
        {
            _context.FileDocuments.Remove(entity);
            await Task.CompletedTask;
        }


        public async Task<List<FileDocument>> GetAllAsync()
        {
            return await _context.FileDocuments
            .AsNoTracking()
            .OrderByDescending(f => f.UploadedOn)
            .ToListAsync();
        }


        public async Task<FileDocument?> GetByIdAsync(Guid id)
        {
            return await _context.FileDocuments.FindAsync(id);
        }


        public async Task UpdateAsync(FileDocument entity)
        {
            _context.FileDocuments.Update(entity);
            await Task.CompletedTask;
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}


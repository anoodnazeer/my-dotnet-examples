using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageAndResume.Dto;
using ImageAndResume.Service;
using ImageAndResume.Models;
using ImageAndResume.RequestObject;
using Microsoft.AspNetCore.Http;

namespace ImageAndResume.Service
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;


        // Allowed content types
        private static readonly string[] permittedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
        private static readonly string[] permittedDocTypes = new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
        private const long maxFileSizeBytes = 20 * 1024 * 1024; // 20 MB limit - adjust as needed

        public FileService(IFileRepository repository)
        {
            _repository = repository;
        }

        public async Task<FileDto> CreateAsync(FileCreateRequest request)
        {
            ValidateIFormFile(request.File);


            using var ms = new MemoryStream();
            await request.File.CopyToAsync(ms);
            var bytes = ms.ToArray();


            var entity = new FileDocument
            {
                Id = Guid.NewGuid(),
                FileName = Path.GetFileName(request.File.FileName),
                ContentType = request.File.ContentType,
                Size = request.File.Length,
                Data = bytes,
                Description = request.Description,
                UploadedOn = DateTime.UtcNow
            };


            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();


            return MapToDto(entity);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;


            await _repository.DeleteAsync(existing);
            return await _repository.SaveChangesAsync();
        }


        public async Task<IEnumerable<FileDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(MapToDto);
        }


        public async Task<FileDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }


        public async Task<(byte[] Data, string ContentType, string FileName)?> GetFileContentAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return (entity.Data, entity.ContentType, entity.FileName);
        }

        public async Task<FileDto?> UpdateAsync(Guid id, FileUpdateRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;


            // If a new file is provided, validate and replace bytes
            if (request.File != null)
            {
                ValidateIFormFile(request.File);
                using var ms = new MemoryStream();
                await request.File.CopyToAsync(ms);
                existing.Data = ms.ToArray();
                existing.Size = request.File.Length;
                existing.FileName = Path.GetFileName(request.File.FileName);
                existing.ContentType = request.File.ContentType;
            }


            // Update metadata
            if (request.Description != null)
                existing.Description = request.Description;


            existing.UploadedOn = DateTime.UtcNow; // update timestamp


            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();


            return MapToDto(existing);
        }

        private static FileDto MapToDto(FileDocument e)
        {
            return new FileDto
            {
                Id = e.Id,
                FileName = e.FileName,
                ContentType = e.ContentType,
                Size = e.Size,
                Description = e.Description,
                UploadedOn = e.UploadedOn
            };
        }


        private void ValidateIFormFile(IFormFile file)
        {
            if (file == null) throw new ArgumentException("No file provided");


            if (file.Length <= 0) throw new ArgumentException("File is empty");


            if (file.Length > maxFileSizeBytes)
                throw new ArgumentException($"File size exceeds limit of {maxFileSizeBytes} bytes");


            var ct = file.ContentType?.ToLowerInvariant() ?? string.Empty;


            var allowed = permittedImageTypes.Concat(permittedDocTypes);
            if (!allowed.Contains(ct))
            {
                throw new ArgumentException("Unsupported file type. Allowed: images (jpg/png/gif), pdf, doc, docx");
            }
        }
    }
}


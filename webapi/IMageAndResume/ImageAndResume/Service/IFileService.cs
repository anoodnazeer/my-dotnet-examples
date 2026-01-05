using ImageAndResume.Dto;
using ImageAndResume.RequestObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageAndResume.Service
{
    public interface  IFileService
    {
        Task<FileDto> CreateAsync(FileCreateRequest request);
        Task<FileDto?> UpdateAsync(Guid id, FileUpdateRequest request);
        Task<FileDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<FileDto>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
        // Helper to return file bytes + metadata for download
        Task<(byte[] Data, string ContentType, string FileName)?> GetFileContentAsync(Guid id);
    }
}

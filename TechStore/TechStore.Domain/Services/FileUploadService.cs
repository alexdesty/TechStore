using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TechStore.Domain.Interfaces.Services;

namespace TechStore.Domain.Services;

public class FileUploadService : IFileUploadService
{
    private readonly string FolderUploadPath;
    public FileUploadService(string folderUploadPath)
    {
        FolderUploadPath = folderUploadPath;
    }

    public async Task UploadFileAsync(string pathToSave, string fileName, IFormFile file)
    {
        var filePath = Path.Combine(FolderUploadPath, pathToSave);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        var fullPath = Path.Combine(filePath, fileName);
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }
}

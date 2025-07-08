using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TechStore.Domain.Interfaces.Services;

public interface IFileUploadService
{
    Task UploadFileAsync(string pathToSave, string fileName, IFormFile file);
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Extensions
{
    public static class IFormFileExtensions
    {
        public static async Task<File> ToFileModel(this IFormFile formFile)
        {
            if (formFile == null) return null;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                memoryStream.Position = 0;
                await formFile.CopyToAsync(memoryStream);
                var file = new File
                {
                    FileName = formFile.FileName,
                    FileContent = memoryStream.ToArray(),
                    Extension = System.IO.Path.GetExtension(formFile.FileName)
                };
                return file;
            }
        }
    }
}

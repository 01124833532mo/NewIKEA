using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
	internal class AttachmentService : IAttachmentService
	{
		private readonly List<string> _allowedExtentions = new() { ".png", ".jpg", ".jpeg" };
		private const int _allowedMaxSize = 2_097_152; 
		public string? Upload(IFormFile file, string folderName)
		{
			var extention = Path.GetExtension(file.FileName);

			if (!_allowedExtentions.Contains(extention))
				return null;

			if (file.Length > _allowedMaxSize)
				return null;



			//var folderPath = $"{Directory.GetCurrentDirectory}\\wwwroot\\Files\\{folderName}";

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
			if(! Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = $"{Guid.NewGuid()}{extention}"; // must be unique 

			var filePath=Path.Combine(folderPath, fileName); // file location placed

			using var fileStream = new FileStream(filePath, FileMode.Create);

			file.CopyTo(fileStream);

			return fileName;
		}


		public bool Delete(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				return true;
			}
			return false;

		}

	}
}

using Microsoft.AspNetCore.Mvc;

namespace Lab_2.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult Index()
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files"));
            return View(files);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var fileName = Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(file.FileName);
                var uniqueFileName = fileName + fileExtension;

                var filePath = Path.Combine(uploadDirectory, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Upload");
        }



        public IActionResult View(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);
            return PhysicalFile(filePath, "application/octet-stream", enableRangeProcessing: true);
        }
    }

}

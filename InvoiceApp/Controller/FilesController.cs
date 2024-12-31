using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FilesController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetFile(string fileName)
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "SecureUploads");
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, "application/octet-stream", fileName);
    }
}

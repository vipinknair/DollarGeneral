using Dollar.Models;
using Dollar.Models.Home;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dollar.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            List<CsvFields> csvSeperator = System.IO.File.ReadAllLines(path)
                                           .Skip(1)
                                           .Select(v => CsvFields.FromCsv(v))
                                           .ToList();
            return View(csvSeperator);
        }
        [HttpPost]
        public ActionResult UploadDatatable(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "uploads", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            List<CsvFields> data = System.IO.File.ReadAllLines(path)
                                           .Skip(1)
                                           .Select(v => CsvFields.FromCsv(v))
                                           .ToList();

            //string jsonString = JsonSerializer.Serialize(csvSeperator);
            //string data = "{\"data\":" + jsonString + "}";
            //return Json(data);
            return Json(new { jsonData=data });
        }
        public IActionResult Upload()
        {
            List<CsvFields> csvSeperator = new List<CsvFields>();
            return View(csvSeperator);
        }
        public IActionResult UploadDatatable()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

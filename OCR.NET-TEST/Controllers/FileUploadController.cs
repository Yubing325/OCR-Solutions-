using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR.NET_TEST.ViewModels;

namespace OCR.NET_TEST.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        // GET: FileUpload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileUpload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileUpload/Create
        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = @"C:\Users\yblia\Desktop\发票图片.jpg";
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePaths });
        }

        //[HttpPost]
        //public IActionResult Upload(FileModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = null;
        //        if(model.Files != null)
        //        {
        //            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Files.FileName;

        //            string filePath = Path.Combine(uploadFolder, uniqueFileName);

        //            model.Files.CopyToAsync(new FileStream(filePath, FileMode.Create));

        //        }


        //    }

        //    return View();
        //}

        // GET: FileUpload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileUpload/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FileUpload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileUpload/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
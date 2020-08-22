using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR.NET_TEST.Models;
using OCR.NET_TEST.Services;
using OCR.NET_TEST.ViewModels;

namespace OCR.NET_TEST.Controllers
{
    public class OCRController : Controller
    {
        private readonly OCRService ocrService;

        public OCRController(OCRService ocrService)
        {
            this.ocrService = ocrService;
        }

        // GET: OCR
        public async Task<IActionResult> Index()
        {
           // var result = await ocrService.GetInvoice();
           return View();
        }

        // GET: OCR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OCR/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileModel model)
        {

            List<Root> result = new List<Root>();

            if (ModelState.IsValid)
            {
                result = await ocrService.GetInvoice(model);

            }

            return View("View", result);
        }

        // POST: OCR/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OCR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OCR/Edit/5
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

        // GET: OCR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OCR/Delete/5
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
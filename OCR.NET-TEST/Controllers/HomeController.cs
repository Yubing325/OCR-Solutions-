using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OCR.NET_TEST.Models;

namespace OCR.NET_TEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [HttpGet]
        public IActionResult Test()
        {

            var roots = new List<Root>()
            {
                new Root()
                {
                    log_id = 1,
                }
            };

            var sb = new StringBuilder();

            sb.AppendLine("类型, 名称, commodity");

            foreach (var root in roots)
            {

                sb.AppendLine($"{root.log_id}, {root.log_id}");

            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            return File(Encoding.GetEncoding("GB2312").GetBytes(sb.ToString()), "text/csv", "invoices.csv");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using monitoringMVC.DB;
using monitoringMVC.Models;

namespace monitoringMVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetRam()
        {
            var resultram = PostgresDataBase.GetDataRam();
            return View(resultram);
        }
        
        public IActionResult GetDisk()
        {
            var resultDisk = PostgresDataBase.GetDataDisk();
            return View(resultDisk);
        }

        public IActionResult GetProcess()
        {
            var resultProcess = PostgresDataBase.GetDataProcess();
            return View(resultProcess);
        }
    }
}
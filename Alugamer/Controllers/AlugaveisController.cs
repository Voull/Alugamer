using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Alugamer.Database;
using Alugamer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
	public class AlugaveisController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

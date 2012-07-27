using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace MessageBoard.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			using (var r = new MySqlRepository ()) {
				ViewData ["Messages"] = r.GetTop100 ();
			}
			return View ();
		}
	}
}


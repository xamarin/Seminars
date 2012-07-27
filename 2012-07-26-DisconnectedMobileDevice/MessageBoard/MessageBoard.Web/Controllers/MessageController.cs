using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;
using System.Xml;
using System.Text;

namespace MessageBoard.Web.Controllers
{
	public class MessageController : Controller
	{
		public ActionResult Details (Guid id)
		{
			using (var r = new MySqlRepository ()) {
				var m = r.Get (id);
				var content = "";
				using (var w = new StringWriter ()) {
					using (var xw = new XmlTextWriter (w)) {
						m.WriteXml (xw);
					}
					content = w.GetStringBuilder ().ToString ();
				}
				return Content (content, "text/xml");
			}
		}
	}
}


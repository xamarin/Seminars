using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;
using System.Xml;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Web.Controllers
{
	public class MessagesController : Controller
	{
		void SimulateLatency ()
		{
			System.Threading.Thread.Sleep ((int)((0.5 + new Random ().NextDouble ()) * 2000));
		}

		public ActionResult Index ()
		{
			using (var r = new MySqlRepository ()) {
				var top = r.GetTop100 ();
				SimulateLatency ();

				var content = "";
				using (var w = new StringWriter ()) {
					using (var xw = new XmlTextWriter (w)) {
						Message.WriteAllXml (top, xw);
					}
					content = w.GetStringBuilder ().ToString ();
				}
				return Content (content, "text/xml");
			}
		}

		[HttpPost]
		public ActionResult New (string @from, string text)
		{
			var m = new Message {
				Id = Guid.NewGuid (),
				From = @from,
				Text = text,
				Time = DateTime.UtcNow,
			};
			if (string.IsNullOrEmpty (m.From)) {
				m.From = "Anonymous";
			}
			if (!string.IsNullOrEmpty (m.Text)) {
				using (var r = new MySqlRepository ()) {
					r.Add (m);
				}
			}
			return RedirectToAction ("Index", "Home");
		}
	}
}


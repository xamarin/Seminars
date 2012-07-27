using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;

namespace MessageBoard
{
	public class MessageBoardClient
	{
		bool _simulateErrors;

		public static readonly TimeSpan RefreshInterval = TimeSpan.FromSeconds (5);

		public MessageBoardClient (bool simulateErrors = false)
		{
			_simulateErrors = simulateErrors;
		}

		public List<Message> GetMostRecent ()
		{
			if (_simulateErrors) {
				throw new ApplicationException ("Simulated network failure");
			}
			else {
				System.Diagnostics.Debug.WriteLine ("Began downloading");

				var client = new WebClient ();
				var content = client.DownloadString ("http://localhost:8080/Messages");

				System.Diagnostics.Debug.WriteLine ("Download succeeded");

				using (var r = new StringReader (content)) {
					using (var xr = new XmlTextReader (r)) {

						return Message.ReadAllXml (xr).ToList ();
					}
				}
			}
		}

		public Task<List<Message>> GetMostRecentAsync ()
		{
			return Task.Factory.StartNew (delegate {
				return GetMostRecent ();
			});
		}

		public void PostNewMessage (Message message)
		{
			if (_simulateErrors) {
				throw new ApplicationException ("Simulated network failure");
			}
			else {
				System.Diagnostics.Debug.WriteLine ("Posting");

				var data = string.Format (
					"from={0}&text={1}",
					Uri.EscapeDataString (message.From),
					Uri.EscapeDataString (message.Text));
				var dataBytes = Encoding.UTF8.GetBytes (data);

				var req = (HttpWebRequest)WebRequest.Create ("http://localhost:8080/Messages/New");
				req.Method = "POST";
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = dataBytes.Length;
				using (var s = req.GetRequestStream ()) {
					s.Write (dataBytes, 0, dataBytes.Length);
				}
				req.GetResponse ();
			}
		}

		public Task PostNewMessageAsync (Message message)
		{
			return Task.Factory.StartNew (delegate {
				PostNewMessage (message);
			});
		}
	}
}


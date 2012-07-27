using System;
using System.Xml;
using System.Collections.Generic;

namespace MessageBoard
{
	public class Message
	{
		public Guid Id { get; set; }
		public string From { get; set; }
		public string Text { get; set; }
		public DateTime Time { get; set; }

		public Message ()
		{
			From = Text = "";
		}

		#region Serialization

		public static void WriteAllXml (IEnumerable<Message> messages, XmlWriter w)
		{
			w.WriteStartElement ("Messages");
			foreach (var m in messages) {
				m.WriteXml (w);
			}
			w.WriteEndElement ();
		}

		public void WriteXml (XmlWriter w)
		{
			w.WriteStartElement ("Message");
			w.WriteElementString ("Id", Id.ToString ());
			w.WriteElementString ("From", From);
			w.WriteElementString ("Text", Text);
			w.WriteElementString ("Time", Time.ToString ("O"));
			w.WriteEndElement ();
		}

		public static IEnumerable<Message> ReadAllXml (XmlReader r)
		{
			while (r.Read ()) {
				if (r.IsStartElement ("Message")) {
					var m = new Message ();
					m.ReadXml (r.ReadSubtree ());
					yield return m;
				}
			}
		}

		public void ReadXml (XmlReader r)
		{
			while (r.Read ()) {
				if (r.IsStartElement ("Id")) {
					Id = new Guid (r.ReadString ());
				}
				else if (r.IsStartElement ("From")) {
					From = r.ReadString ();
				}
				else if (r.IsStartElement ("Text")) {
					Text = r.ReadString ();
				}
				else if (r.IsStartElement ("Time")) {
					Time = DateTime.Parse (r.ReadString ());
				}
			}
		}

		#endregion
	}
}


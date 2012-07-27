using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MessageBoard.Web
{
	public class MySqlRepository : IDisposable
	{
		MySqlConnection _db;

		public MySqlRepository ()
		{
			_db = new MySqlConnection ("Server=localhost;Database=mb;Uid=root;Pwd=sqladmin;");
			_db.Open ();
		}

		public void Dispose ()
		{
			_db.Close ();
			_db = null;
		}

		public List<Message> GetTop100 ()
		{
			var l = new List<Message> ();
			using (var c = _db.CreateCommand ()) {
				c.CommandText = "SELECT * FROM Message ORDER BY Time DESC LIMIT 100";
				using (var r = c.ExecuteReader ()) {
					while (r.Read ()) {
						l.Add (ReadMessage (r));
					}
				}
			}
			return l;
		}

		public Message Get (Guid id)
		{
			using (var c = _db.CreateCommand ()) {
				c.CommandText = "SELECT * FROM Message WHERE Id = ?id LIMIT 1";
				c.Parameters.AddRange (new [] {
					new MySqlParameter ("id", id.ToString ()),
				});
				using (var r = c.ExecuteReader ()) {
					if (r.Read ()) {
						return ReadMessage (r);
					}
				}
				throw new ApplicationException ("Message Not Found");
			}
		}

		public void Add (Message message)
		{
			using (var c = _db.CreateCommand ()) {
				c.CommandText = "INSERT INTO Message(Id,`From`,Text,Time) VALUES(?id,?from,?text,?time)";
				c.Parameters.AddRange (new [] {
					new MySqlParameter ("id", message.Id.ToString ()),
					new MySqlParameter ("from", message.From),
					new MySqlParameter ("text", message.Text),
					new MySqlParameter ("time", message.Time)
				});
				c.ExecuteNonQuery ();
			}
		}

		Message ReadMessage (MySqlDataReader r)
		{
			return new Message {
				Id = new Guid (r.GetString ("Id")),
				From = r.GetString ("From"),
				Text = r.GetString ("Text"),
				Time = r.GetDateTime ("Time"),
			};
		}
	}
}


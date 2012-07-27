using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoard.iOS
{
	public class Outbox
	{
		SQLiteConnection _db;

		public Outbox ()
		{
			_db = new SQLiteConnection (
				Path.Combine (
					Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments),
					"Outbox.db"),
				storeDateTimeAsTicks: true);
			_db.CreateTable<Message> ();
		}

		public Message GetOldestMessage ()
		{
			var q = from m in _db.Table<Message> ()
					orderby m.Time ascending
					select m;
			return q.Take (1).FirstOrDefault ();
		}

		public void Add (Message message)
		{
			_db.Insert (message);
		}

		public void Remove (Message message)
		{
			_db.Execute ("DELETE FROM Message WHERE Id = ?", message.Id);
		}
	}
}


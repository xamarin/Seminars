using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace MessageBoard.iOS
{
	public class LocalStorage
	{
		SQLiteConnection _db;

		public LocalStorage ()
		{
			_db = new SQLiteConnection (
				Path.Combine (
					Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments),
					"LocalStorage.db"),
				storeDateTimeAsTicks: true);
			_db.CreateTable<Message> ();
		}

		public List<Message> GetMostRecentMessages ()
		{
			var q = from m in _db.Table<Message> ()
					orderby m.Time descending
					select m;
			return q.Take (100).ToList ();
		}

		public void Save (IEnumerable<Message> messages)
		{
			_db.RunInTransaction (delegate {
				_db.Execute ("DELETE FROM Message");
				_db.InsertAll (messages, beginTransaction: false);
			});
		}
	}
}


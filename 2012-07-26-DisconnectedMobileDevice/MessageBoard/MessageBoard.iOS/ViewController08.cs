using System;
using System.Collections.Generic;

namespace MessageBoard.iOS
{
	public class ViewController08 : ViewController07
	{
		LocalStorage _localStorage;

		public ViewController08 ()
		{
			Title = "08 Local Storage";
			SimulateErrors = false;

			_localStorage = new LocalStorage ();
			_messages = _localStorage.GetMostRecentMessages ();
		}

		protected override void ShowMessages (List<Message> messages)
		{
			_localStorage.Save (messages);
			base.ShowMessages (messages);
		}
	}
}


using System;
using System.Threading.Tasks;

namespace MessageBoard.iOS
{
	public class ViewController03 : ViewController02
	{
		public ViewController03 ()
		{
			Title = "03 Asynchronous";
			SimulateErrors = true;
		}

		protected override void Refresh ()
		{
			var client = GetMessageBoardClient ();

			client.GetMostRecentAsync ().ContinueWith (task => {

				ShowMessages (task.Result);
				
			}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}
}


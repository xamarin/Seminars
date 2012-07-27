using System;
using System.Threading.Tasks;

namespace MessageBoard.iOS
{
	public class ViewController04 : ViewController03
	{
		public ViewController04 ()
		{
			Title = "04 Async with Errors";
			SimulateErrors = true;
		}

		#region Fixed error handler
		protected override void ShowError (Exception ex)
		{
			var e = ex;
			while (e.InnerException != null) {
				e = e.InnerException;
			}
			base.ShowError (e);
		}
		#endregion

		protected override void Refresh ()
		{
			var c = GetMessageBoardClient ();

			c.GetMostRecentAsync ().ContinueWith (task => {

				if (task.IsFaulted) {
					ShowError (task.Exception);
				}
				else {
					ShowMessages (task.Result);
				}

//				try {
//					ShowMessages (task.Result);
//				}
//				catch (Exception ex) {
//					ShowError (ex);
//				}
				
			}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}
}


using System;
using System.Threading.Tasks;
using System.Threading;

namespace MessageBoard.iOS
{
	public class OutboxService
	{
		bool _simulateErrors;

		Outbox _outbox;

		object _lock = new object ();

		AutoResetEvent _newMessageEvent = new AutoResetEvent (false);

		public OutboxService (bool simulateErrors)
		{
			_simulateErrors = simulateErrors;
			_outbox = new Outbox ();
		}

		public Task Start ()
		{
			return Task.Factory.StartNew (delegate {
				Run ();
			}, TaskCreationOptions.LongRunning);
		}

		public void AddMessage (Message message)
		{
			lock (_lock) {
				_outbox.Add (message);
				_newMessageEvent.Set ();
			}
		}

		void Run ()
		{
			while (true) {

				Message message = null;
				lock (_lock) {
					message = _outbox.GetOldestMessage ();
				}

				if (message == null) {
					_newMessageEvent.WaitOne (TimeSpan.FromHours (1));
				}
				else {
					try {
						var client = new MessageBoardClient (simulateErrors: _simulateErrors);
						client.PostNewMessage (message);
						lock (_lock) {
							_outbox.Remove (message);
						}
					}
					catch (Exception) {
						_newMessageEvent.WaitOne (TimeSpan.FromSeconds (10));
					}
				}
			}
		}
	}
}


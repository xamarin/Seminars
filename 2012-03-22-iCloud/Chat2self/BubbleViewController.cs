using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BubbleCell
{
	public class BubbleViewController : DialogViewController
	{
		Section chatSection;
		UITextView input;
		UIButton done;
		NSObject keyValueNotification;

		public BubbleViewController () : base (UITableViewStyle.Plain, null)
		{
			// initialize the list, and show the 'instructions' bubble
			Root = new RootElement ("Chat Sample") {
				(chatSection = new Section () {
					new ChatBubble (BubbleType.RightBlue, "Install this app on two or more devices with iOS5 that are iCloud-enabled, then start typing and have some fun :-)"),
				})
			};
			
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}

		public override void ViewDidLoad ()
		{
			// add the inputs to the DialogViewController - bit of a hack :)
			base.ViewDidLoad ();

			var f = TableView.Frame;
			f.Height -= 65;
			f.Y += 65;
			TableView.Frame = f;

			input = new UITextView(new RectangleF(3, 23, 247, 37));
			input.BackgroundColor= UIColor.FromRGB(207,220,255);
			input.Font = UIFont.SystemFontOfSize (16f);
			done = UIButton.FromType (UIButtonType.RoundedRect);
			done.Frame = new RectangleF(255, 23, 60, 37); // 400

			done.SetTitle ("Cloud", UIControlState.Normal);

			// SEND BUTTON 
			done.TouchUpInside += (sender, e) => {
				if (input.Text.Length > 0) {
					Console.WriteLine ("Update icloud kv with\n    " + UIDevice.CurrentDevice.Name+":"+ input.Text);
	
					// add chat to list
					chatSection.Add (new ChatBubble (BubbleType.Left, input.Text));
					// scroll to the end
					var lastIP = NSIndexPath.FromRowSection(chatSection.Count-1, 0);
					TableView.ScrollToRow (lastIP, UITableViewScrollPosition.Bottom, true);
					// update the cloud
					var store = NSUbiquitousKeyValueStore.DefaultStore;
					store.SetString(UIDevice.CurrentDevice.Name, input.Text);
					store.Synchronize ();
					// reset
					input.Text = "";
					input.ResignFirstResponder ();
				}
			};
		}

		public override void ViewDidAppear (bool animated)
		{
			// finish adding the input controls to the DialogViewController (see, hacky)
			base.ViewDidAppear (animated);
			var parent = View.Superview;
			parent.Add (input);
			parent.Add (done);
			parent.BackgroundColor = UIColor.White;
		}
		
		public override void ViewWillAppear (bool animated)
		{
			// set up the notification that will trigger when new icloud key-value pairs
			// come from the server, so we can display them 
			base.ViewWillAppear (animated);
		
			keyValueNotification = 
			NSNotificationCenter.DefaultCenter.AddObserver (
				NSUbiquitousKeyValueStore.DidChangeExternallyNotification
				, delegate (NSNotification n)
			{
				// NOTIFICATION received from iCloud
				Console.WriteLine("Cloud notification received");
				NSDictionary userInfo = n.UserInfo;
			
				NSNumber reason = (NSNumber)userInfo.ObjectForKey(NSUbiquitousKeyValueStore.ChangeReasonKey);
				int ireason = reason.IntValue;
				
				Console.WriteLine("reason (enum): " + (NSUbiquitousKeyValueStoreChangeReason)ireason);
			
				NSArray changedKeys = (NSArray)userInfo.ObjectForKey (NSUbiquitousKeyValueStore.ChangedKeysKey);
				var changedKeysList = new List<string> ();
				var messages = new List<string> ();
				for (uint i = 0; i < changedKeys.Count; i++)
				{
					var key = new NSString (changedKeys.ValueAt(i));
					Console.WriteLine("changedKey (value): " + key);

					if (key != UIDevice.CurrentDevice.Name) {
						// it's from another device, so display it
						Console.WriteLine("Adding to conversation " + key);

						var store = NSUbiquitousKeyValueStore.DefaultStore;
						store.Synchronize(); 

						var msg = key + ": " + store.GetString(key); // show key in message
						messages.Add (msg);
					}
					changedKeysList.Add (key);
				}
				// add new chat bubbles as the server updates the icloud key-value store
				InvokeOnMainThread (delegate { 
					try {
						var chats = new List<Element> ();
						if (messages.Count > 0) {
							foreach (var msg in messages) {
								chats.Add (new ChatBubble (BubbleType.Right, msg));	
							}
							chatSection.AddAll (chats.ToArray ());
							var lastIP = NSIndexPath.FromRowSection(chatSection.Count-1, 0);
							TableView.ScrollToRow (lastIP, UITableViewScrollPosition.Bottom, true);
						}
					} catch (Exception e) { Console.WriteLine ("Problem adding chats to chatSection " + e.Message); }
				});
			});

			Console.WriteLine ("DefaultStore.Synchronize="+NSUbiquitousKeyValueStore.DefaultStore.Synchronize ());
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if (keyValueNotification != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver (keyValueNotification);
		}

		public enum NSUbiquitousKeyValueStoreChangeReason
		{
				ServerChange = 0
			,	InitialSyncChange = 1
			,	QuotaViolationChange = 2
		}
	}
}


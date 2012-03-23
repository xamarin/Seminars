using System;
using System.Collections.Generic;
using System.IO;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

namespace Cloud {
	public class TaskListScreen : DialogViewController {

		UIBarButtonItem addButton, refreshButton;
		
		List<TaskDocument> tasks;

		NSMetadataQuery query;
		
		public TaskListScreen () : base (UITableViewStyle.Plain, new RootElement("Loading..."))
		{
			Title = "TaskCloud";
			tasks = new List<TaskDocument>();

			// whenever the application is 'activated' we re-check iCloud
			NSNotificationCenter.DefaultCenter.AddObserver (this
					,new Selector("loadTasks:")
					,UIApplication.DidBecomeActiveNotification
					,null);
			// load after the AppDelegate determines that iCloud is available
			NSNotificationCenter.DefaultCenter.AddObserver (this
					, new Selector("loadTasks:")
					, new NSString("iCloudConnected"),
					null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (s,e) =>{
				var filename = DateTime.Now.ToString ("yyyyMMdd_HHmmss") + ".task";
				if (AppDelegate.HasiCloud) {
					var p1 = Path.Combine(AppDelegate.iCloudUrl.Path, "Documents");
					var p2 = Path.Combine (p1, filename);
					var ubiq = new NSUrl(p2, false);

					var task = new TaskDocument(ubiq);
					task.Save (task.FileUrl, UIDocumentSaveOperation.ForCreating
					, (success) => {
						Console.WriteLine ("Save completion:"+ success);
						tasks.Add (task);
						Reload();
					});
				}
			});
			NavigationItem.RightBarButtonItem = addButton;
			
			// UIBarButtonSystemItem.Refresh or http://barrow.io/posts/iphone-emoji/
			refreshButton = new UIBarButtonItem('\uE049'.ToString ()
			, UIBarButtonItemStyle.Plain
			, (s,e) => {
				LoadTasks(null);
			});

			NavigationItem.LeftBarButtonItem = refreshButton;
			LoadTasks(null);			
		}
	
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			Reload ();
		}

		void Reload() {
			InvokeOnMainThread(()=>{
				Root = 	new RootElement ("TaskCloud") {
						new Section () {
						from task in tasks
						orderby task.LocalizedName
						select (Element) new StringElement (task.TheTask.Title, () =>{
							var ts = new TaskScreen(task);
							NavigationController.PushViewController (ts, true);
						})
					}
				};
			});
		}


		[Export("loadTasks:")]
		void LoadTasks (NSNotification notification) {

			Console.WriteLine ("LoadTasks()");

			if (notification != null && notification.UserInfo != null) {
				var u = notification.UserInfo.ValueForKey(new NSString("HasiCloud")) as NSNumber;
				if (u != null)
					Console.WriteLine ("Has iCloud notification:"+ u.BoolValue);
			}

			if (AppDelegate.HasiCloud) {
				query = new NSMetadataQuery();
				query.SearchScopes = new NSObject [] {NSMetadataQuery.QueryUbiquitousDocumentsScope};
				var pred = NSPredicate.FromFormat ("%K like '*.task'"
							, new NSObject[] {NSMetadataQuery.ItemFSNameKey});
				//Console.WriteLine ("Predicate:" + pred.PredicateFormat);
				query.Predicate = pred;
				NSNotificationCenter.DefaultCenter.AddObserver (this
					, new Selector("queryDidFinishGathering:")
					,NSMetadataQuery.DidFinishGatheringNotification
					, query);
				
				query.StartQuery ();
			} else {
				Console.WriteLine ("HasiCloud not populated yet");
				InvokeOnMainThread(()=>{
				 	var uia = new UIAlertView("Connecting to iCloud...", "Press the \uE049 button to refresh",null, "OK", null);
					uia.Show ();
				});
			}
		}

		[Export("queryDidFinishGathering:")]
		void DidFinishGathering (NSNotification notification) {
			Console.WriteLine ("DidFinishGathering: notification");
			var query = (NSMetadataQuery)notification.Object;
			query.DisableUpdates();
			query.StopQuery();

			NSNotificationCenter.DefaultCenter.RemoveObserver (this, NSMetadataQuery.DidFinishGatheringNotification, query);
			LoadData(query);
			query = null;
		}

		void LoadData (NSMetadataQuery query) {
			Console.WriteLine ("LoadData()");	
			
			tasks = new List<TaskDocument>();

			foreach (var item in query.Results) {
				Console.WriteLine ("Found iCloud document for list");

				NSUrl url = (NSUrl)item.ValueForAttribute(NSMetadataQuery.ItemURLKey);
				var task = new TaskDocument(url);
				task.Open ( (success) => {
					if (success) {
						Console.WriteLine ("iCloud document added");
						tasks.Add (task);
						Reload (); // hacky to keep doing this...
					} else
						Console.WriteLine ("failed to open iCloud document");
				});
			}
		}
	}
}
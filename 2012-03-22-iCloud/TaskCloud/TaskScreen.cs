using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

namespace Cloud {
	public class TaskScreen : UIViewController {
		UIButton saveButton;
		UILabel doneLabel;
		UISwitch doneSwitch;
		UITextView descriptionText, titleText;

		TaskDocument doc;
		
		public TaskScreen (TaskDocument taskDoc) {
			doc = taskDoc;
		}

		public override void ViewDidLoad ()
		{	
			base.ViewDidLoad ();
			
			#region UI Controls (you could do this in XIB if you want)
			saveButton = UIButton.FromType(UIButtonType.RoundedRect);
			saveButton.Frame = new RectangleF(10,10,145,50);
			saveButton.SetTitle("Save", UIControlState.Normal);
			saveButton.SetTitle("waiting...", UIControlState.Disabled);
			saveButton.Enabled = false;
			
			doneSwitch = new UISwitch();
			doneSwitch.Frame = new RectangleF(180, 25, 145, 50);
			doneSwitch.Enabled = false;
			doneLabel = new UILabel();
			doneLabel.Frame = new RectangleF(200, 10, 145, 15);
			doneLabel.Text = "Done?";

			titleText = new UITextView(new RectangleF(10, 70, 300, 40));
			titleText.BackgroundColor = UIColor.FromRGB(240,240,240);
			titleText.Editable = true;
			titleText.BackgroundColor = UIColor.FromRGB(240,240,240);

			descriptionText = new UITextView(new RectangleF(10, 130, 300, 180));
			descriptionText.BackgroundColor = UIColor.FromRGB(240,240,240);
			descriptionText.Editable = true;
			descriptionText.ScrollEnabled = true;
			descriptionText.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

			// Add the controls to the view
			this.Add(saveButton);
			this.Add(doneLabel);
			this.Add(doneSwitch);
			this.Add(descriptionText);
			this.Add(titleText);
			#endregion

			saveButton.TouchUpInside += (sender, e) => {

				doc.TheTask.Title = titleText.Text;
				doc.TheTask.Description = descriptionText.Text;
				doc.TheTask.IsDone = doneSwitch.On;

				doc.UpdateChangeCount (UIDocumentChangeKind.Done);	// tell UIDocument it needs saving
				descriptionText.ResignFirstResponder ();			// hide keyboard
				titleText.ResignFirstResponder ();
			};
			
			LoadData ();

			NSNotificationCenter.DefaultCenter.AddObserver (this
				, new Selector("dataReloaded:")
				, new NSString("taskModified"),
				null);

		}
		[Export("dataReloaded:")]
		void DataReloaded (NSNotification notification) {
			doc = (TaskDocument)notification.Object;
			LoadData();
		}
		
		void LoadData () {
			Title = doc.TheTask.Title;
			titleText.Text = doc.TheTask.Title;
			descriptionText.Text = doc.TheTask.Description;
			doneSwitch.On = doc.TheTask.IsDone;			

			saveButton.Enabled = true;
			doneSwitch.Enabled = true;
		}
	}
}
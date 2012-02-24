using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class EditableTableController : UITableViewController
	{
		private UIButton _button;
		private UIBarButtonItem _buttonEdit;
		private UIBarButtonItem _buttonDone;
		private EditableTableSource _datasource;
		
		public EditableTableController(UITableViewStyle style) : base(style)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			NavigationItem.Title = "Editable table demo";
			
			// Add Edit and Done buttons
			_buttonEdit = new UIBarButtonItem(UIBarButtonSystemItem.Edit);
			_buttonDone = new UIBarButtonItem(UIBarButtonSystemItem.Done);
			_buttonEdit.Clicked += Handle_buttonEditClicked;
			_buttonDone.Clicked += Handle_buttonDoneClicked;
			
			NavigationItem.RightBarButtonItem = _buttonEdit;
			
			_datasource = new EditableTableSource();
			TableView.Source = _datasource;
			
			_button = UIButton.FromType(UIButtonType.ContactAdd);
			_button.SetTitle("Add",UIControlState.Normal);
			_button.Frame = new System.Drawing.RectangleF(285,80,23,23);
			
			ParentViewController.View.AddSubview(_button);
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			_button.RemoveFromSuperview();
		}

		
		private void Handle_buttonDoneClicked (object sender, EventArgs e)
		{
			Editing = false;
			NavigationItem.RightBarButtonItem = _buttonEdit;
		}

		private void Handle_buttonEditClicked (object sender, EventArgs e)
		{
			Editing = true;
			NavigationItem.RightBarButtonItem = _buttonDone;
		}
	}
}

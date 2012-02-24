using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class SettingsDataSource : UITableViewDataSource
	{
		private List<string> _items;
		private string _cellId;
		
		public SettingsDataSource()
		{
			_cellId = "cellid";
			_items = new List<string>()
			{
		//		"Series Styles",
				"Aspect",
				"Themes",
				"Color Palettes",
				"Legend",
				"Tools",
				"Functions",
			};
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Settings";
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_cellId); 
			
			if (cell == null )
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			
			cell.TextLabel.Text = _items[indexPath.Row];
			    
			return cell; 
		}
	}
}
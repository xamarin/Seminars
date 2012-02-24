using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ThemesDataSource : UITableViewDataSource
	{
		public List<ItemInfo> Items {get;private set;}
		private string _cellId;
		
		public ThemesDataSource()
		{
			_cellId = "cellid";
			Items = new List<ItemInfo>()
			{			
				new ItemInfo(0,"BlackIsBackTheme",""),
				new ItemInfo(1,"OperaTheme",""),
				new ItemInfo(2,"TeeChartTheme",""),
				new ItemInfo(3,"ExcelTheme",""),
				new ItemInfo(4,"ClassicTheme",""),
				new ItemInfo(5,"XPTheme",""),
				new ItemInfo(6,"WebTheme",""),
				new ItemInfo(7,"BusinessTheme",""),
				new ItemInfo(8,"BlueSkyTheme",""),
				new ItemInfo(9,"GrayscaleTheme",""),
				new ItemInfo(10,"RandomTheme","")
			};
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Themes";
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return Items.Count;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_cellId); 
			
			if ( cell == null )
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);
				
				// Set its Accessory if it should be highlighted.
				//if (indexPath.Row == Settings.SelectedIndex)
				//	cell.Accessory = UITableViewCellAccessory.Checkmark;
			}
			
			cell.TextLabel.Text = Items[indexPath.Row].Text;
			    
			return cell; 
		}
	}
	
	/*public class Settings
	{
		public static int SelectedIndex = 0;	
	}*/
	
	/// <summary>
	/// A class for holding a table item text and an associated value.
	/// </summary>
	/*public class ItemInfo
	{
		public int Value { get; set; }
		public string Text { get; set; }
		
		public ItemInfo(int val,string text)
		{
			Value = 	val;
			Text = text;
		}
		
		public override string ToString ()
		{
			return string.Format("[ItemInfo: Value={0}, Text={1}]", Value, Text);
		}
	}*/
}

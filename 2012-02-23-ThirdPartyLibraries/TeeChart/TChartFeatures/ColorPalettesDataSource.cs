using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ColorPalettesDataSource : UITableViewDataSource
	{
		public List<ItemInfo> Items {get;private set;}
		private string _cellId;
		
		public ColorPalettesDataSource()
		{
			_cellId = "cellid";
			Items = new List<ItemInfo>()
			{			
				new ItemInfo(0,"TeeChart",""),
				new ItemInfo(1,"Excel",""),
				new ItemInfo(2,"Victorian",""),
				new ItemInfo(3,"Pastels",""),
				new ItemInfo(4,"Solid",""),
				new ItemInfo(5,"Classic",""),
				new ItemInfo(6,"Web",""),
				new ItemInfo(7,"Modern",""),
				new ItemInfo(8,"Rainbow",""),
				new ItemInfo(9,"Win. XP",""),
				new ItemInfo(10,"Mac OS",""),
				new ItemInfo(11,"Windows Vista",""),
				new ItemInfo(12,"Grayscale",""),
				new ItemInfo(13,"Opera",""),
				new ItemInfo(14,"Warm",""),
				new ItemInfo(15,"Cool",""),
				new ItemInfo(16,"OnBlack","")
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

using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ToolsDataSource : UITableViewDataSource
	{
		public List<ItemInfo> Items {get;private set;}
		private string _cellId;
		
		public ToolsDataSource()
		{
			_cellId = "cellid";
			Items = new List<ItemInfo>()
			{			
				new ItemInfo(0,"Annotation",""),
				new ItemInfo(1,"ChartImage",""),
				new ItemInfo(2,"ExtraLegend",""),
				new ItemInfo(3,"GridBand",""),
				new ItemInfo(4,"GridTranspose",""),
				new ItemInfo(5,"Rotate",""),
				new ItemInfo(6,"ColorLine",""),
				new ItemInfo(7,"ColorBand",""),
				new ItemInfo(8,"SeriesBandTool",""),				
				new ItemInfo(9,"SubChartTool",""),
				new ItemInfo(10,"SeriesRegionTool",""),
				new ItemInfo(11,"LegendPalette",""),
				new ItemInfo(12,"AxisBreaksTool","")
				//new ItemInfo(13,"DataTableTool",""),
				//new ItemInfo(10,"Marker",""),
				//new ItemInfo(5,"MarksTip",""),
				//new ItemInfo(6,"NearestPoint",""),
				//new ItemInfo(7,"PageNumber",""),
				//new ItemInfo(8,"PieTool",""),
				//new ItemInfo(10,"LegendScrollBar",""),
				//new ItemInfo(11,"SeriesAnimation",""),
				//new ItemInfo(12,"SurfaceNearestTool",""),
				//new ItemInfo(13,"CursorTool",""),
				//new ItemInfo(14,"DragMarks",""),
				//new ItemInfo(15,"AxisArrow",""),
				//new ItemInfo(18,"DrawLine",""),
				//new ItemInfo(19,"DragPoint",""),
				//new ItemInfo(20,"GanttTool",""),
				//new ItemInfo(21,"AxisScroll",""),
				//new ItemInfo(22,"SeriesHotspot",""),
				//new ItemInfo(23,"ZoomTool",""),
				//new ItemInfo(24,"ScrollTool",""),
				//new ItemInfo(25,"LightTool",""),
				//new ItemInfo(26,"FibonacciTool",""),
				//new ItemInfo(29,"FaderTool",""),
				//new ItemInfo(30,"RectangleTool",""),
				//new ItemInfo(31,"Selector",""),
				//new ItemInfo(34,"SeriesStats",""),
				//new ItemInfo(35,"SeriesTranspose",""),
				//new ItemInfo(37,"ClipSeries",""),
				//new ItemInfo(38,"BannerTool",""),
				//new ItemInfo(39,"Magnify",""),
				//new ItemInfo(41,"CustomHotspot",""),
			};
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Tools";
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


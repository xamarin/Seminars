using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class FunctionsDataSource : UITableViewDataSource
	{
		public List<ItemInfo> Items {get;private set;}
		private string _cellId;
		
		public FunctionsDataSource()
		{
			_cellId = "cellid";
			Items = new List<ItemInfo>()
			{			
				new ItemInfo(0,"Add",""),
				new ItemInfo(1,"Subtract",""),
				new ItemInfo(2,"Multiply",""),
				new ItemInfo(3,"Divide",""),
				new ItemInfo(4,"High",""),
				new ItemInfo(5,"Low",""),
				new ItemInfo(6,"Average",""),
				new ItemInfo(7,"Count",""),
				new ItemInfo(8,"Momentum",""),
				new ItemInfo(9,"MomentumDivision",""),
				new ItemInfo(10,"Cumulative",""),
				new ItemInfo(11,"ExpAverage",""),
				new ItemInfo(12,"Smoothing",""),
				new ItemInfo(13,"Custom",""),
				new ItemInfo(14,"RootMeanSquare",""),
				new ItemInfo(15,"StdDeviation",""),
				new ItemInfo(16,"Stochastic",""),
				new ItemInfo(17,"ExpMovAverage",""),
				new ItemInfo(18,"Performance",""),
				new ItemInfo(19,"CrossPoints",""),
				new ItemInfo(20,"CompressOHLC",""),
				new ItemInfo(21,"CLVFunction",""),
				new ItemInfo(22,"OBVFunction",""),
				new ItemInfo(23,"CCIFunction",""),
				new ItemInfo(24,"MovingAverage",""),
				new ItemInfo(25,"PVOFunction",""),
				new ItemInfo(26,"DownSampling",""),
				new ItemInfo(27,"TrendFunction",""),
				new ItemInfo(28,"CorrelationFunction",""),
				new ItemInfo(29,"VarianceFunction",""),
				new ItemInfo(30,"PerimeterFunction",""),
				new ItemInfo(31,"PolyFitting",""),
				new ItemInfo(32,"Bollinger",""),
				new ItemInfo(33,"MACDFunction",""),
				new ItemInfo(34,"RSIFunction",""),
				new ItemInfo(35,"ADXFunction",""),
				new ItemInfo(36,"MedianFunction",""),
				new ItemInfo(37,"ModeFunction",""),
				new ItemInfo(38,"ExpTrendFunction",""),
				new ItemInfo(39,"HistogramFunction",""),
				new ItemInfo(40,"SARFunction","")	
			};
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Functions";
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
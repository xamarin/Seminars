using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

namespace TChartFeatures
{
	public class AspectDataSource : UITableViewDataSource
	{
		private List<AspectItem> _items;
		private string _cellId;
		private string _section2CellId;
		private AspectController _parentController;
		 
		
		public AspectDataSource(AspectController parent)
		{
			_cellId = "section1Cell";
			_section2CellId = "section2Cell";
			
			_parentController = parent;
			_items = new List<AspectItem>();
			
			InitAdvanced();
		}
		

		private void InitAdvanced()
		{
			// A switcher View3D
			AspectItem item1 = new AspectItem("3D","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			UISwitch switcher1 = new UISwitch();		
			item1.AccessoryView = switcher1;
			switcher1.On=_parentController.chart.Chart.Aspect.View3D;
			switcher1.ValueChanged+=delegate{
				_parentController.chart.Chart.Aspect.View3D = switcher1.On;
			};
			
			// Chart3DPercent slider
			AspectItem item3DPercent = new AspectItem("3D Percent","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);			
			UISlider slider3DPercent = new UISlider();
			slider3DPercent.MaxValue = 100f;
			slider3DPercent.Value = 0f;
			slider3DPercent.MinValue = 0f;
			slider3DPercent.Frame = new RectangleF(140,9,150,23);
			
			slider3DPercent.Value=_parentController.chart.Chart.Aspect.Chart3DPercent;
			slider3DPercent.ValueChanged+=delegate{
				_parentController.chart.Chart.Aspect.Chart3DPercent = (int)slider3DPercent.Value;
			};
			item3DPercent.ContentView = slider3DPercent;			
			
			
			// ZoomScrollStyle automatic
			AspectItem itemZoomScroll = new AspectItem("ZoomScroll Auto","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			UISwitch switcherZoomScroll = new UISwitch();		
			itemZoomScroll.AccessoryView = switcherZoomScroll;
			switcherZoomScroll.On=_parentController.chart.Chart.Aspect.ZoomScrollStyle==Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Auto;
			switcherZoomScroll.ValueChanged+=delegate{
				if (switcherZoomScroll.On)
					_parentController.chart.Chart.Aspect.ZoomScrollStyle = Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Auto;
				else
					_parentController.chart.Chart.Aspect.ZoomScrollStyle = Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;						
			};
			
			// A switcher Zoon style InChart or AllChart
			AspectItem itemZoomStyle = new AspectItem("Zoom InChart","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			UISwitch switcherZoomStyle = new UISwitch();		
			itemZoomStyle.AccessoryView = switcherZoomStyle;
			switcherZoomStyle.On=(_parentController.chart.Chart.Aspect.ZoomStyle == Steema.TeeChart.Drawing.Aspect.ZoomStyles.InChart);			
			switcherZoomStyle.ValueChanged+=delegate{
				if (switcherZoomStyle.On)
					_parentController.chart.Chart.Aspect.ZoomStyle = Steema.TeeChart.Drawing.Aspect.ZoomStyles.InChart;
				else
					_parentController.chart.Chart.Aspect.ZoomStyle = Steema.TeeChart.Drawing.Aspect.ZoomStyles.FullChart;	
			};
			
			// Rotation slider
			AspectItem itemRotation = new AspectItem("Rotation","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);			
			UISlider sliderRotation = new UISlider();
			sliderRotation.MaxValue = 100f;
			sliderRotation.Value = 0f;
			sliderRotation.MinValue = 0f;
			sliderRotation.Frame = new RectangleF(140,9,150,23);
			
			sliderRotation.Value=_parentController.chart.Chart.Aspect.Rotation;
			sliderRotation.ValueChanged+=delegate{
				_parentController.chart.Chart.Aspect.Rotation = (int)sliderRotation.Value;
			};
			itemRotation.ContentView = sliderRotation;			
			
			// A switcher orthogonal 
			AspectItem item2 = new AspectItem("Orthogonal","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			UISwitch switcher2 = new UISwitch();		
			item2.AccessoryView = switcher2;
			switcher2.On=_parentController.chart.Chart.Aspect.Orthogonal;
			switcher2.ValueChanged+=delegate{
				_parentController.chart.Chart.Aspect.Orthogonal = switcher2.On;
			};
						
			_items.Add(item1);
			_items.Add(item3DPercent);
			_items.Add(itemZoomScroll);
			_items.Add(itemZoomStyle);
			_items.Add(item2);
			_items.Add(itemRotation);
		}
		
	public override string TitleForHeader(UITableView tableView, int section)
		{
		    return "Aspect settings";
		}
					
		public override int RowsInSection(UITableView tableview, int section)
		{
			    return 5;
		}

		public override int NumberOfSections(UITableView tableView)
		{
			return 1;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// Strange things happen if we use the same cellids for both sections - some reuse is attempted by Cocoa.
			string cellId = _cellId;
			var row = indexPath.Row;
			
			if (indexPath.Section == 1)
			{
				row += 7;
				cellId = _section2CellId;
			}
			
			var info = _items[row];
			
			UITableViewCell cell = tableView.DequeueReusableCell(cellId); 
			
			if (cell == null )
			{
				cell = new UITableViewCell(info.Style,cellId);
			}
			
			cell.TextLabel.Text = info.Text;
			cell.Accessory = info.Accessory;
				
			if (!string.IsNullOrEmpty(info.DetailText))
				cell.DetailTextLabel.Text = info.DetailText;
			
			if (info.Image != null)
			{
				cell.ImageView.Frame = new RectangleF(0,0,150,100);
				cell.ImageView.HighlightedImage = info.Image;
				cell.ImageView.Image = info.Image;		
			}
			
			if (info.ContentView != null)
			{
				//info.ContentView.BackgroundColor = UIColor.Green;
				cell.ContentView.Add(info.ContentView);
			}
			
			// Alternative method is to use AccessoryForRow in the UITableViewDelegate
			if (info.AccessoryView != null)
			{
				//info.AccessoryView.BackgroundColor = UIColor.Red;
				cell.AccessoryView = info.AccessoryView;	
			}
			
			return cell;
		}
	}
	
	/// <summary>
	/// Encapsulates a UITableViewCell's different styles
	/// </summary>
	public class AspectItem
	{
		public string DetailText {get;set;}
		public string Text { get; set; }
		public UITableViewCellStyle Style { get; set; }
		public UITableViewCellAccessory Accessory { get; set; }
		
		//
		public UIImage Image { get; set; }
		public UIView ContentView { get;set;}
		public UIView AccessoryView { get;set;}
		
		public AspectItem(string text,string detailtext,UITableViewCellStyle style,UITableViewCellAccessory accessory)
		{
			Text = text;
			DetailText = detailtext;
			Style = style;
			Accessory = accessory;
		}
	}
}
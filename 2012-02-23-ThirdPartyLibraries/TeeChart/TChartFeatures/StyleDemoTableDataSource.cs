using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

namespace TChartFeatures
{
	public class StyleDemoTableDataSource : UITableViewDataSource
	{
		private List<StyleItem> _items;
		private string _cellId;
		private string _section2CellId;
		private StyleDemoTableController _parentController;
		
		public StyleDemoTableDataSource(StyleDemoTableController parent)
		{
			_cellId = "section1Cell";
			_section2CellId = "section2Cell";
			
			_parentController = parent;
			_items = new List<StyleItem>();
			
			InitBasic();
			InitAdvanced();
		}
		
		private void InitBasic()
		{
			// Some basic rows
			StyleItem item1 = new StyleItem("Default","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			StyleItem item2 = new StyleItem("Subtitle","Here is more description",UITableViewCellStyle.Subtitle,UITableViewCellAccessory.None);
			StyleItem item3 = new StyleItem("Value 1 - no accessory","Here is more description",UITableViewCellStyle.Value1,UITableViewCellAccessory.None);
			StyleItem item4 = new StyleItem("Value 2 - no accessory","Here is more description",UITableViewCellStyle.Value2,UITableViewCellAccessory.None);
			
			StyleItem item5 = new StyleItem("Checkmark","",UITableViewCellStyle.Default,UITableViewCellAccessory.Checkmark);
			StyleItem item6 = new StyleItem("DetailDisclosureButton","",UITableViewCellStyle.Default,UITableViewCellAccessory.DetailDisclosureButton);
			StyleItem item7 = new StyleItem("DisclosureIndicator","",UITableViewCellStyle.Default,UITableViewCellAccessory.DisclosureIndicator);
			
			_items.Add(item1);
			_items.Add(item2);
			_items.Add(item3);
			_items.Add(item4);
			_items.Add(item5);
			_items.Add(item6);
			_items.Add(item7);	
		}
		
		private void InitAdvanced()
		{
			// Single image
			StyleItem item1 = new StyleItem("Image","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			item1.Image = UIImage.FromFile("bulb_on.png");
			
			//
			// Two images
			//
			StyleItem item2 = new StyleItem("Two images","",UITableViewCellStyle.Default,UITableViewCellAccessory.DisclosureIndicator);
			item2.Image = UIImage.FromFile("bulb_on.png");
			
			// This is the easiest way to set the right image if you don't want to trap it being clicked in the delegate
			UIImageView imageView = new UIImageView(UIImage.FromFile("bulb_off.png"),UIImage.FromFile("bulb_off.png"));
			item2.AccessoryView = imageView;
			
			// If you want to trap AccessoryButtonTapped in the delegate, you need to use a UIButton
			UIButton button = UIButton.FromType(UIButtonType.Custom);
/* pep temp commented			button.TouchDown += delegate {
				UITableViewController nextController = new CheckmarkDemoTableController(UITableViewStyle.Grouped);
				_parentController.NavigationController.PushViewController(nextController,true);
			};*/
			
			button.SetBackgroundImage(UIImage.FromFile("bulb_off.png"),UIControlState.Normal);
			button.SetBackgroundImage(UIImage.FromFile("bulb_off.png"),UIControlState.Selected);
			button.Frame = new System.Drawing.RectangleF(0,0,26,26);
			item2.AccessoryView = button;
			
			//
			// Two images + custom control
			//
			StyleItem item3 = new StyleItem("","",UITableViewCellStyle.Default,UITableViewCellAccessory.DisclosureIndicator);
			item3.Image = UIImage.FromFile("bulb_on.png");
			
			UISlider slider = new UISlider();
			slider.MaxValue = 100f;
			slider.Value = 50f;
			slider.MinValue = 0f;
			slider.Frame = new RectangleF(50,9,215,23);
			item3.ContentView = slider;
			
			UIImageView imageView1 = new UIImageView(UIImage.FromFile("bulb_off.png"),UIImage.FromFile("bulb_off.png"));
			item3.AccessoryView = imageView1;
			
			// A switcher
			StyleItem item4 = new StyleItem("Free beer?","",UITableViewCellStyle.Default,UITableViewCellAccessory.None);
			UISwitch switcher = new UISwitch();			
			item4.AccessoryView = switcher;
			
			_items.Add(item1);
			_items.Add(item2);
			_items.Add(item3);
			_items.Add(item4);
		}
		
		public override string TitleForHeader(UITableView tableView, int section)
		{
			if (section == 0)
				return "Basic styling";
			else
			    return "Advanced styling";
		}
		
		public override int RowsInSection(UITableView tableview, int section)
		{
			if (section == 0)
				return 7;
			else
			    return 4;
		}
		
		public override int NumberOfSections(UITableView tableView)
		{
			return 2;
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
				info.ContentView.BackgroundColor = UIColor.Green;
				cell.ContentView.Add(info.ContentView);
			}
			
			// Alternative method is to use AccessoryForRow in the UITableViewDelegate
			if (info.AccessoryView != null)
			{
				info.AccessoryView.BackgroundColor = UIColor.Red;
				cell.AccessoryView = info.AccessoryView;	
			}
			
			return cell;
		}
	}
	
	/// <summary>
	/// Encapsulates a UITableViewCell's different styles
	/// </summary>
	public class StyleItem
	{
		public string DetailText {get;set;}
		public string Text { get; set; }
		public UITableViewCellStyle Style { get; set; }
		public UITableViewCellAccessory Accessory { get; set; }
		
		//
		public UIImage Image { get; set; }
		public UIView ContentView { get;set;}
		public UIView AccessoryView { get;set;}
		
		public StyleItem(string text,string detailtext,UITableViewCellStyle style,UITableViewCellAccessory accessory)
		{
			Text = text;
			DetailText = detailtext;
			Style = style;
			Accessory = accessory;
		}
	}
}

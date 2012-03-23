//
// Bubble.cs: Provides both a UITableViewCell that can be used with UITableViews
// as well as a ChatBubble which is a MonoTouch.Dialog Element that can be used
// inside a DialogViewController for quick UIs.
//
// Author:
//   Miguel de Icaza
//
using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using System.Drawing;

namespace BubbleCell
{
	public enum BubbleType {
		Left, Right, RightBlue
	}

	public class BubbleCell : UITableViewCell {
		public static NSString KeyLeft = new NSString ("BubbleElementLeft");
		public static NSString KeyRight = new NSString ("BubbleElementRight");
		public static NSString KeyRightBlue = new NSString ("BubbleElementRightBlue");
		public static UIImage bleft, bright, left, right, rightblue, brightblue; 
		public static UIFont font = UIFont.SystemFontOfSize (14);
		UIView view;
		UIView imageView;
		UILabel label;
		BubbleType isLeft;
		
		static BubbleCell ()
		{
			//var a = new System.Uri ("http://www.google.com", UriKind.Absolute);
			bright = UIImage.FromFile ("green.png");
			bleft = UIImage.FromFile ("grey.png");
			brightblue = UIImage.FromFile ("blue.png");

			left = bleft.CreateResizableImage (new UIEdgeInsets (10, 16, 18, 26));
			right = bright.CreateResizableImage (new UIEdgeInsets (11, 11, 17, 18));
			rightblue = brightblue.CreateResizableImage (new UIEdgeInsets (11, 11, 17, 18));
		}
		
		public BubbleCell (BubbleType isLeft) : base (UITableViewCellStyle.Default, 
				isLeft==BubbleType.Left?KeyLeft:isLeft==BubbleType.Right?KeyRight:KeyRightBlue)
		{
			var rect = new RectangleF (0, 0, 1, 1);
			this.isLeft = isLeft;
			view = new UIView (rect);
			imageView = new UIImageView (isLeft==BubbleType.Left?left:isLeft==BubbleType.Right?right:rightblue);
			view.AddSubview (imageView);
			label = new UILabel (rect) {
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines = 0,
				Font = font,
				BackgroundColor = UIColor.Clear
			};
			view.AddSubview (label);
			ContentView.Add (view);
		}
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var frame = ContentView.Frame;
			var size = GetSizeForText (this, label.Text) + BubblePadding;
			imageView.Frame = new RectangleF (new PointF (isLeft==BubbleType.Left ? 10 : frame.Width-size.Width-10, frame.Y), size);
			view.SetNeedsDisplay ();
			frame = imageView.Frame;
			label.Frame = new RectangleF (new PointF (frame.X + (isLeft==BubbleType.Left ? 12 : 8), frame.Y + 6), size-BubblePadding);
		}
		
		static internal SizeF BubblePadding = new SizeF (22, 16);
		
		static internal SizeF GetSizeForText (UIView tv, string text)
		{
			return tv.StringSize (text, font, new SizeF (tv.Bounds.Width*.7f-10-22, 99999));
		}
		
		public void Update (string text)
		{
			label.Text = text;
			SetNeedsLayout ();
		}
	}
	
	public class ChatBubble : Element, IElementSizing {
		BubbleType isLeft;
		
		public ChatBubble (BubbleType isLeft, string text) : base (text)
		{
			this.isLeft = isLeft;
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (isLeft==BubbleType.Left?BubbleCell.KeyLeft:isLeft==BubbleType.Right?BubbleCell.KeyRight:BubbleCell.KeyRightBlue) as BubbleCell;
			if (cell == null)
				cell = new BubbleCell (isLeft);
			cell.Update (Caption);
			return cell;
		}
		
		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return BubbleCell.GetSizeForText (tableView, Caption).Height + BubbleCell.BubblePadding.Height;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;


// Android Property Animation API
using Android.Animation;



namespace XaminarAnimations
{
	public class KarmaMeter : View
	{
		const int DefaultHeight = 20;
		const int DefaultWidth = 120;

		double position = 0.5;
		Paint positivePaint;
		Paint negativePaint;

		public KarmaMeter (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public KarmaMeter (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
			positivePaint = new Paint {
				AntiAlias = true,
				Color = Color.Rgb (0x99, 0xcc, 0),
			};
			positivePaint.SetStyle (Paint.Style.FillAndStroke);

			negativePaint = new Paint {
				AntiAlias = true,
				Color = Color.Rgb (0xff, 0x44, 0x44)
			};
			negativePaint.SetStyle (Paint.Style.FillAndStroke);
		}

		public void SetKarmaBasedOnValues (int totalGiven, int totalGotten, bool animate = true)
		{
			var value = (((totalGiven - totalGotten) / (float)(totalGiven + totalGotten)) + 1) / 2f;
			SetKarmaValue (value, animate);
		}

		public double KarmaValue {
			get {
				return position;
			}
			set {
				position = Math.Max (0f, Math.Min (value, 1f));
				Invalidate ();
			}
		}



		public void SetKarmaValue (double value, bool animate)
		{
			if (!animate) {
				KarmaValue = value;
				return;
			}

			var animator = ValueAnimator.OfFloat ((float)position, (float)Math.Max (0f, Math.Min (value, 1f)));
			animator.SetDuration (500);
			animator.Update += (sender, e) => KarmaValue = (double)e.Animation.AnimatedValue;
			animator.Start ();
		}





		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			var width = View.MeasureSpec.GetSize (widthMeasureSpec);
			SetMeasuredDimension (width < DefaultWidth ? DefaultWidth : width,
			                      DefaultHeight);
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			float middle = canvas.Width * (float)position;
			canvas.DrawPaint (negativePaint);
			canvas.DrawRect (0,
			                 0,
			                 middle,
			                 canvas.Height,
			                 positivePaint);
		}
	}
}


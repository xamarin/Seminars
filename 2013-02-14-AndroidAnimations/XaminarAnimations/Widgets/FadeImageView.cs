
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
using Android.Graphics.Drawables;


// Android View Animation API
using Android.Views.Animations;
using Android.Animation;


namespace XaminarAnimations
{
	public class FadeImageView : ImageView
	{
		Animation fadeInAnimation;
		Animation fadeOutAnimation;

		public FadeImageView (Context ctx) : base (ctx)
		{
			Initialize ();
		}

		public FadeImageView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public FadeImageView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}



		void Initialize ()
		{
			fadeInAnimation = new AlphaAnimation (0, 1) {
				Duration = 500
			};
			fadeOutAnimation = new AlphaAnimation (1, 0) {
				Duration = 100
			};
		}

		void DoAnimation (bool really, Action changePic)
		{
			if (!really)
				changePic ();
			else {
				EventHandler<Animation.AnimationEndEventArgs> callback = (s, e) => {
					changePic ();
					StartAnimation (fadeInAnimation);
					fadeOutAnimation.AnimationEnd -= callback;
				};
				fadeOutAnimation.AnimationEnd += callback;
				StartAnimation (fadeOutAnimation);
			}
		}

		void DoAnimation2 (bool really, Action changePic)
		{
			if (!really)
				changePic ();
			else {
				var fdIn = ObjectAnimator.OfFloat (this, "alpha", new float[] { 0, 1 });
				var fdOut = ObjectAnimator.OfFloat (this, "alpha", new float[] { 1, 0 });
				fdOut.AnimationEnd += (sender, e) => changePic ();

				var animator = new AnimatorSet ();
				animator.SetInterpolator (new LinearInterpolator ());
				//animator.PlaySequentially (fdOut, fdIn);
				animator.Play (fdOut);
				animator.Play (fdIn).After (fdOut).After (2000);
				animator.Start ();
			}
		}



		public void SetImageBitmap (Bitmap bm, bool animate)
		{
			DoAnimation2 (animate, () => SetImageBitmap (bm));
		}

		public void SetImageDrawable (Drawable drawable, bool animate)
		{
			DoAnimation2 (animate, () => SetImageDrawable (drawable));
		}

		public void SetImageResource (int resId, bool animate)
		{
			DoAnimation2 (animate, () => SetImageResource (resId));
		}

		public void SetImageURI (Android.Net.Uri uri, bool animate)
		{
			DoAnimation2 (animate, () => SetImageURI (uri));
		}





		/*void DoAnimation3 (bool really, Action changePic)
		{
			if (!really)
				changePic ();
			else {
				var fdIn = ObjectAnimator.OfFloat (this, "alpha", new float[] { 1, 0 })
					.SetDuration (500);
				fdIn.AnimationEnd += (sender, e) => changePic ();
				var fdOut = ObjectAnimator.OfFloat (this, "alpha", new float[] { 0, 1})
					.SetDuration (250);
				
				var animatorSet = new AnimatorSet ();
				animatorSet.SetInterpolator (new LinearInterpolator ());
				//animatorSet.PlaySequentially (fdIn, fdOut);
				animatorSet.Play (fdIn);
				animatorSet.Play (fdOut).After (fdIn).After (2000);
				animatorSet.Start ();
			}
		}*/
	}
}


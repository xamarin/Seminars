
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

namespace XaminarAnimations
{
	public class ViewAnimationFragment : Fragment
	{
		TextView counterText;
		FadeImageView pictureZone;
		Button switchButton;

		int[] pictures;
		int currentPictureIndex = 1;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			pictures = new int[] { Resource.Drawable.pic1, Resource.Drawable.pic2 };
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.ViewAnimationLayout, container, false);
			counterText = view.FindViewById<TextView> (Resource.Id.counterText);
			pictureZone = view.FindViewById<FadeImageView> (Resource.Id.pictureZone);
			switchButton = view.FindViewById<Button> (Resource.Id.switchButton);

			switchButton.Click += (sender, e) => {
				var index = currentPictureIndex++ % pictures.Length;
				counterText.Text = (index + 1).ToString ();
				pictureZone.SetImageResource (pictures[index], true);
			};

			return view;
		}
	}
}



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
	public class PropertyAnimationFragment : Fragment
	{
		KarmaMeter karmaMeter;
		SeekBar karmaSeeker;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.PropertyAnimationLayout, container, false);
			karmaMeter = view.FindViewById<KarmaMeter> (Resource.Id.karmaMeter);
			karmaSeeker = view.FindViewById<SeekBar> (Resource.Id.karmaSeeker);
			karmaSeeker.StopTrackingTouch +=
				(sender, e) => karmaMeter.SetKarmaValue (((double)karmaSeeker.Progress) / karmaSeeker.Max, true);

			return view;
		}
	}
}


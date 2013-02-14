
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
	public class LayoutAnimationFragment : Fragment
	{
		Button switchButton;
		LinearLayout firstLayout;
		LinearLayout secondLayout;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.LayoutAnimationLayout, container, false);
			switchButton = view.FindViewById<Button> (Resource.Id.switchButton);
			firstLayout = view.FindViewById<LinearLayout> (Resource.Id.firstLayout);
			secondLayout = view.FindViewById<LinearLayout> (Resource.Id.secondLayout);

			switchButton.Click += (sender, e) => {
				if (firstLayout.Visibility == ViewStates.Visible)
					SwitchToSecondLayout ();
				else
					SwitchToFirstLayout ();
			};

			return view;
		}


		void SwitchToSecondLayout ()
		{
			var parms = secondLayout.LayoutParameters as LinearLayout.LayoutParams;
			parms = new LinearLayout.LayoutParams (parms);
			parms.Height = ViewGroup.LayoutParams.WrapContent;
			parms.Gravity = GravityFlags.Center;

			secondLayout.LayoutParameters = parms;
			firstLayout.Visibility = ViewStates.Gone;
		}

		void SwitchToFirstLayout ()
		{
			firstLayout.Visibility = ViewStates.Visible;
			var parms = secondLayout.LayoutParameters as LinearLayout.LayoutParams;
			parms = new LinearLayout.LayoutParams (parms);
			parms.Height = 1;
			parms.Gravity = GravityFlags.Center;
			secondLayout.LayoutParameters = parms;
		}


	}
}


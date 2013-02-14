using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Java.Lang;

namespace XaminarAnimations
{
	[Activity (Label = "Fun with Animations", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
	public class MainActivity : Activity
	{
		Fragment layoutFragment;
		Fragment viewFragment;
		Fragment propertyFragment;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			var tab = ActionBar.NewTab ().SetText ("View");
			tab.TabSelected += (_, e) => {
				if (viewFragment == null) {
					viewFragment = Fragment.Instantiate (this,
					                                     Class.FromType (typeof (ViewAnimationFragment)).Name);
					e.FragmentTransaction.Add (Android.Resource.Id.Content, viewFragment, "view-tab");
				} else {
					e.FragmentTransaction.Attach (viewFragment);
				}
			};
			tab.TabUnselected += (_, e) => {
				if (viewFragment != null)
					e.FragmentTransaction.Detach (viewFragment);
			};
			ActionBar.AddTab (tab);

			tab = ActionBar.NewTab ().SetText ("Property");
			tab.TabSelected += (_, e) => {
				if (propertyFragment == null) {
					propertyFragment = Fragment.Instantiate (this,
					                                         Class.FromType (typeof (PropertyAnimationFragment)).Name);
					e.FragmentTransaction.Add (Android.Resource.Id.Content, propertyFragment, "property-tab");
				} else {
					e.FragmentTransaction.Attach (propertyFragment);
				}
			};
			tab.TabUnselected += (_, e) => {
				if (propertyFragment != null)
					e.FragmentTransaction.Detach (propertyFragment);
			};
			ActionBar.AddTab (tab);

			tab = ActionBar.NewTab ().SetText ("Layout");
			tab.TabSelected += (_, e) => {
				if (layoutFragment == null) {
					layoutFragment = Fragment.Instantiate (this,
					                                       Class.FromType (typeof (LayoutAnimationFragment)).Name);
					e.FragmentTransaction.Add (Android.Resource.Id.Content, layoutFragment, "layout-tab");
				} else {
					e.FragmentTransaction.Attach (layoutFragment);
				}
			};
			tab.TabUnselected += (_, e) => {
				if (layoutFragment != null)
					e.FragmentTransaction.Detach (layoutFragment);
			};
			ActionBar.AddTab (tab);
		}
	}
}



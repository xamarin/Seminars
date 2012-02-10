using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloTabsICS
{
    [Activity (Label = "HelloTabsICS", MainLauncher = true)]
    public class Activity1 : Activity
    {   
        SampleTabFragment _sampleFragment;
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
   
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
                 
            AddTab ("Tab 1", Resource.Drawable.ic_tab_white);
            AddTab ("Tab 2", Resource.Drawable.ic_tab_white);
        }
        
        void AddTab (string tabText, int iconResourceId)
        {
            var tab = this.ActionBar.NewTab ();            
            tab.SetText (tabText);
            tab.SetIcon (Resource.Drawable.ic_tab_white);
            
            // must set event handler before adding tab
            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) { 
                if (_sampleFragment != null)
                    e.FragmentTransaction.Remove (_sampleFragment);
       
                _sampleFragment = new SampleTabFragment (tabText);
                
                e.FragmentTransaction.Add (Resource.Id.fragmentContainer, _sampleFragment);
            };
            
            this.ActionBar.AddTab (tab);
        }
        
        class SampleTabFragment: Fragment
        {    
            string _text;
            
            public SampleTabFragment (string text)
            {
                _text = text;
            }
            
            public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView (inflater, container, savedInstanceState);
                
                var view = inflater.Inflate (Resource.Layout.Tab, container, false);
                var sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView);             
                sampleTextView.Text = "sample fragment text: " + _text;

                return view;
            }
        }
    }
}



using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.OS;

namespace TwitterSearch.UI.Droid
{
    [Activity(Label = "TwitterSearch", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _goButton;
        private EditText _searchEditText;
        private TextView _randomLabel;
        private TextView _searchLabel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _goButton = this.FindViewById<Button>(Resource.Id.GoButton);
            _searchEditText = this.FindViewById<EditText>(Resource.Id.SearchEdit);
            _randomLabel = this.FindViewById<TextView>(Resource.Id.RandomLabel);
            _searchLabel = this.FindViewById<TextView>(Resource.Id.SearchLabel);

            _searchEditText.Text = "dinosaur";
            _randomLabel.Click += RandomLabelOnClick;
            _goButton.Click += GoButtonOnClick;
        }

        private bool ShouldSearch(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;
            if (word.ToLower() == "javascript")
                return false;
            return true;
        }

        private void GoButtonOnClick(object sender, EventArgs eventArgs)
        {
            var text = _searchEditText.Text;
            if (!ShouldSearch(text))
                return;
            var intent = new Intent(this, typeof(ResultsActivity));
            intent.PutExtra(Constants.SearchKey, text);
            intent.AddFlags(ActivityFlags.NewTask);
            this.StartActivity(intent);
        }

        private void RandomLabelOnClick(object sender, EventArgs eventArgs)
        {
            var items = new[] { "Dinosaur", "WP7", "MonoTouch", "MonoDroid", "slodge", "kittens" };

            var r = new Random();
            var originalText = _searchEditText.Text;
            var newText = originalText;
            while (originalText == newText)
            {
                var which = r.Next(items.Length);
                newText = items[which];
            }
            _searchEditText.Text = newText;
        }
    }
}


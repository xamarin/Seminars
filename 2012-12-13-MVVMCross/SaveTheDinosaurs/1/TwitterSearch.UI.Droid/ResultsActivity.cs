using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace TwitterSearch.UI.Droid
{
    [Activity(Label = "TwitterSearch Results", Icon = "@drawable/icon")]
    public class ResultsActivity : Activity
    {
        private bool _isSearching;
        private string _searchText;
        private MyAdapter _myAdapter;
        private ListView _theList;
        private ProgressBar _theProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Results);
            _searchText = this.Intent.GetStringExtra(Constants.SearchKey);
            _myAdapter = new MyAdapter(this);
            _theList = this.FindViewById<ListView>(Resource.Id.TheList);
            _theList.Adapter = _myAdapter;
            _theProgressBar = this.FindViewById<ProgressBar>(Resource.Id.TheProgress);
            StartSearch();
        }

        private void StartSearch()
        {
            if (_isSearching)
                return;

            _isSearching = true;
            TwitterSearch.StartAsyncSearch(_searchText, Success, Error);
        }

        private void Success(IEnumerable<Tweet> tweets)
        {
            // marshall back to UI thread
            this.RunOnUiThread(() =>
                {
                    _myAdapter.SetTweets(tweets);
                    _isSearching = false;
                    _theProgressBar.Visibility = ViewStates.Gone;
                });
        }

        private void Error(Exception obj)
        {
            // todo - display the error - one day!
            this.RunOnUiThread(() =>
                {
                    _isSearching = false;
                    _theProgressBar.Visibility = ViewStates.Gone;
                });
        }

        public class MyAdapter : BaseAdapter
        {
            public class JavaTweet : Java.Lang.Object
            {
                public Tweet Tweet { get; private set; }

                public JavaTweet(Tweet tweet)
                {
                    Tweet = tweet;
                }
            }

            private List<Tweet> _tweets;
            private ResultsActivity _parent;

            public MyAdapter(ResultsActivity parent)
            {
                _tweets = new List<Tweet>();
                _parent = parent;
            }

            public void SetTweets(IEnumerable<Tweet> tweets)
            {
                _tweets = tweets.ToList();
                NotifyDataSetChanged();
            }

            public override Object GetItem(int position)
            {
                return new JavaTweet(_tweets[position]);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View view = convertView;
                if (view == null)
                {
                    view = LayoutInflater.From(_parent).Inflate(Resource.Layout.Item_Tweet, parent, false);
                }

                var tweet = _tweets[position];
                view.FindViewById<TextView>(Resource.Id.NameTextView).Text = tweet.Author;
                view.FindViewById<TextView>(Resource.Id.TimeTextView).Text = TimeAgo(tweet.Timestamp);
                view.FindViewById<TextView>(Resource.Id.ContentTextView).Text = tweet.Title;
                var web = new WebClient();
                web.DownloadDataCompleted += (s, e) =>
                    {
                        _parent.RunOnUiThread(() =>
                            {
                                var bm = BitmapFactory.DecodeByteArray(e.Result, 0, e.Result.Length);
                                view.FindViewById<ImageView>(Resource.Id.TwitterImageView).SetImageBitmap(bm);
                            });
                    };
                web.DownloadDataAsync(new Uri(tweet.ProfileImageUrl));

                return view;
            }

            private static string TimeAgo(DateTime when)
            {
                var difference = (DateTime.UtcNow - when).TotalSeconds;

                string whichFormat;
                int valueToFormat;
                if (difference < 30.0)
                {
                    whichFormat = "Just now";
                    valueToFormat = 0;
                }
                else if (difference < 100.0)
                {
                    whichFormat = "{0}s ago";
                    valueToFormat = (int)difference;
                }
                else if (difference < 3600.0)
                {
                    whichFormat = "{0}m ago";
                    valueToFormat = (int)(difference / 60);
                }
                else if (difference < 24 * 3600)
                {
                    whichFormat = "{0}h ago";
                    valueToFormat = (int)(difference / (3600));
                }
                else
                {
                    whichFormat = "{0}d ago";
                    valueToFormat = (int)(difference / (3600 * 24));
                }

                return string.Format(whichFormat, valueToFormat);
            }

            public override int Count
            {
                get { return _tweets.Count; }
            }
        }
    }
}
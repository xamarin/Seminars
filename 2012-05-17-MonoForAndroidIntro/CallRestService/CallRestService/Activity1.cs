using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Json;

namespace CallRestService
{
    [Activity (Label = "CallRestService", MainLauncher = true)]
    public class Activity1 : ListActivity
    {
        List<SearchResultItem> _results;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            string id = "Enter your Bing API ID here";

            string query = "xamarin";

            string url = String.Format (
                "http://api.bing.net/json.aspx?AppId={0}&Query={1}&Sources=web&web.count=10",
                id,
                query
            );
         
            var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
         
            httpReq.BeginGetResponse ((ar) => {
             
                var request = (HttpWebRequest)ar.AsyncState;
            
                using (var response = (HttpWebResponse)request.EndGetResponse (ar)) {
                 
                    var s = response.GetResponseStream ();
                 
                    var j = (JsonObject)JsonObject.Load (s);

                    var results = (from result in (JsonArray)j ["SearchResponse"] ["Web"] ["Results"]
                                let jResult = result as JsonObject 
                                select new SearchResultItem { Title = jResult["Title"], Url = jResult["Url"] }).ToList ();                        
                   
                    RunOnUiThread (() => { 
                        _results = results;
                        ListAdapter = new ArrayAdapter<SearchResultItem> (
                            this,
                            Resource.Layout.SearchItemView,
                            results
                        );
                    });
                }

            }, httpReq);
        }

        protected override void OnListItemClick (Android.Widget.ListView l, View v, int position, long id)
        {
            base.OnListItemClick (l, v, position, id);
            string url = _results [position].Url;
            if (!string.IsNullOrEmpty (url)) {
                var uri = Android.Net.Uri.Parse (url);
                var intent = new Intent (Intent.ActionView, uri); 
                StartActivity (intent); 
            } else {
                Toast.MakeText (this, "No url available", ToastLength.Short);
            }
        }
    }
}



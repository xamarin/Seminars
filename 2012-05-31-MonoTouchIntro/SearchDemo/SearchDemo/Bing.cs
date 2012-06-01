using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Json;
using System.IO;
using System.Text;
using System.Web;
using MonoTouch.Foundation;
using MonoTouch.AVFoundation;

namespace SearchDemo
{
    public delegate void SynchronizerDelegate (List<SearchItem> results);

    public class Bing
    {

        const string BING_API_ID = "Enter your Bing API ID";

        static SynchronizerDelegate sync;

        public Bing ()
        {
        }
        
        public Bing (SynchronizerDelegate sync)
        {
            Bing.sync = sync;
        }

        public void Search (string text)
        {
            var t = new Thread (Search);
            t.Start (text);
        }

        void Search (object text)
        {
            string bingSearch = String.Format (
                "http://api.bing.net/json.aspx?AppId={0}&Query={1}&Sources=web&web.count=10",
                BING_API_ID,
                text
            );

            var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (bingSearch));

            using (HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse ()) {

                var response = httpRes.GetResponseStream ();
                var json = (JsonObject)JsonObject.Load (response);
            
                var results = (from result in (JsonArray)json ["SearchResponse"] ["Web"] ["Results"]
                                let jResult = result as JsonObject 
                                select new SearchItem { Title = jResult["Title"], Url = jResult["Url"] }).ToList ();
            
                if (sync != null)
                    sync (results);
            }
        }

        #region Search using LINQ to XML

        void SearchLinqToXml (object text)
        {
            string bingSearch = String.Format (
                "http://api.bing.net/xml.aspx?AppId={0}&Query={1}&Sources=web&web.count=10",
                BING_API_ID,
                text
            );

            XDocument x = XDocument.Load (bingSearch);
            XName xWebResult = (XNamespace)"http://schemas.microsoft.com/LiveSearch/2008/04/XML/web" + "WebResult";
            XName xTitle = (XNamespace)"http://schemas.microsoft.com/LiveSearch/2008/04/XML/web" + "Title";
            
            var results = (from result in x.Descendants (xWebResult).Elements (xTitle)
                select new SearchItem { Title = result.Value }).ToList ();
            
            if (sync != null)
                sync (results);
        }

        #endregion

        #region Search using Core Foundation

        BingConnectionDelegate cnDelegate = new BingConnectionDelegate ();

        public void SearchCoreFoundation (string text)
        {
            string bingSearch = String.Format (
                "http://api.bing.net/xml.aspx?AppId={0}&Query={1}&Sources=web&web.count=10",
                BING_API_ID,
                text
            );

            var bingRequest = new NSUrlRequest (NSUrl.FromString (HttpUtility.UrlPathEncode (bingSearch)));
            cnDelegate = new BingConnectionDelegate ();
            var cn = new NSUrlConnection (bingRequest, cnDelegate);
            cn.Start ();
        }
      
        class BingConnectionDelegate : NSUrlConnectionDelegate
        {
            StringBuilder sb;

            public BingConnectionDelegate ()
            {
                sb = new StringBuilder ();
            }

            public override void ReceivedData (NSUrlConnection connection, NSData data)
            {
                string xml = data.ToString ();
                
                sb.Append (xml);
            }

            public override void FinishedLoading (NSUrlConnection connection)
            {
                string xml = sb.ToString ();

                XDocument x = XDocument.Load (new StringReader (xml));
                XName xWebResult = (XNamespace)"http://schemas.microsoft.com/LiveSearch/2008/04/XML/web" + "WebResult";
                XName xTitle = (XNamespace)"http://schemas.microsoft.com/LiveSearch/2008/04/XML/web" + "Title";
                
                var results = (from result in x.Descendants (xWebResult).Elements (xTitle)
                    select new SearchItem { Title = result.Value }).ToList ();
                
                if (sync != null)
                    sync (results);
            }
        }
        
        #endregion

        AVAudioPlayer audioPlayer;

        public void Speak (string text, string lang)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            
            try {
                string uri = String.Format (
                    "http://api.microsofttranslator.com/v2/Http.svc/Speak?appId={0}&text={1}&language={2}",
                    BING_API_ID,
                    text,
                    lang
                );
                
                request = (HttpWebRequest)WebRequest.Create (uri);
                response = (HttpWebResponse)request.GetResponse ();

                Stream s = response.GetResponseStream ();
                NSData d = NSData.FromStream (s);

                audioPlayer = AVAudioPlayer.FromData (d);
                audioPlayer.PrepareToPlay ();
                audioPlayer.Play ();
                
            } catch (System.Net.WebException) {
                
                if (response != null)
                    response.Close ();
            }
        }   
    }
}
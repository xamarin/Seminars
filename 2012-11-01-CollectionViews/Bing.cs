using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Linq;
using System.Json;
using System.IO;
using System.Text;
using System.Web;
using MonoTouch.Foundation;

namespace BingImageGridSplit
{
    public delegate void SynchronizerDelegate (List<string> results);
    
    public class Bing
    {
        static SynchronizerDelegate sync;

        const string AZURE_KEY = "INSERT YOUR KEY HERE";

        public Bing (SynchronizerDelegate sync)
        {
            Bing.sync = sync;
        }
        
        public void ImageSearch (string searchString)
        {
            var t = new Thread (DoSearch);
            t.Start (searchString);
        }
        
        void DoSearch (object data)
        {
            string uri = String.Format("https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/Image?Query=%27{0}%27&$top=100&$format=Json", data);
           
            var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (uri));
            
            httpReq.Credentials = new NetworkCredential (AZURE_KEY, AZURE_KEY);
            
            try {
                using (HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse ()) {
                    
                    var response = httpRes.GetResponseStream ();
                    var json = (JsonObject)JsonObject.Load (response);
                    
                    var results = (from result in (JsonArray)json ["d"] ["results"]
                                   let jResult = result as JsonObject 
                                   select jResult ["Thumbnail"] ["MediaUrl"].ToString ()).ToList ();

                    if (sync != null)
                        sync (results);
                }
            } catch (Exception) {
                if (sync != null)
                    sync (null);
            }
        }
       
    }
}
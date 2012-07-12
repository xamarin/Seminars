using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace MapDemo
{
    public partial class MapDemoViewController : UIViewController
    {
        MyMapDelegate mapDel;

        public MapDemoViewController () : base ("MapDemoViewController", null)
        {
        }
        
        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            // change map type and show user location
            map.MapType = MKMapType.Hybrid;
            map.ShowsUserLocation = true;

            // set map center and region
            double lat = 42.374260;
            double lon = -71.120824;
            var mapCenter = new CLLocationCoordinate2D (lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance (mapCenter, 2000, 2000);
            map.CenterCoordinate = mapCenter;
            map.Region = mapRegion;

            // add an annotation
            map.AddAnnotation (new MKPointAnnotation (){Title="MyAnnotation", Coordinate = new CLLocationCoordinate2D (42.364260, -71.120824)});

            // set the map delegate
            mapDel = new MyMapDelegate ();
            map.Delegate = mapDel;

            // add a custom annotation
            map.AddAnnotation (new MonkeyAnnotation ("Xamarin", mapCenter));

            // add an overlay
            var circleOverlay = MKCircle.Circle (mapCenter, 1000);
            map.AddOverlay (circleOverlay);
        }

        class MyMapDelegate : MKMapViewDelegate
        {
            string pId = "PinAnnotation";
            string mId = "MonkeyAnnotation";

            public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
            {
                MKAnnotationView anView;

                if (annotation is MKUserLocation)
                    return null; 

                if (annotation is MonkeyAnnotation) {

                    // show monkey annotation
                    anView = mapView.DequeueReusableAnnotation (mId);

                    if (anView == null)
                        anView = new MKAnnotationView (annotation, mId);
                
                    anView.Image = UIImage.FromFile ("monkey.png");
                    anView.CanShowCallout = true;
                    anView.Draggable = true;
                    anView.RightCalloutAccessoryView = UIButton.FromType (UIButtonType.DetailDisclosure);

                } else {

                    // show pin annotation
                    anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (pId);

                    if (anView == null)
                        anView = new MKPinAnnotationView (annotation, pId);
                
                    ((MKPinAnnotationView)anView).PinColor = MKPinAnnotationColor.Green;
                    anView.CanShowCallout = true;
                }

                return anView;
            }

            public override void CalloutAccessoryControlTapped (MKMapView mapView, MKAnnotationView view, UIControl control)
            {
                var monkeyAn = view.Annotation as MonkeyAnnotation;
                
                if (monkeyAn != null) {
                    var alert = new UIAlertView ("Monkey Annotation", monkeyAn.Title, null, "OK");
                    alert.Show ();
                }
            }

            public override MKOverlayView GetViewForOverlay (MKMapView mapView, NSObject overlay)
            {
                var circleOverlay = overlay as MKCircle;
                var circleView = new MKCircleView (circleOverlay);
                circleView.FillColor = UIColor.Red;
                circleView.Alpha = 0.4f;
                return circleView;
            }
        }
        
        public override void ViewDidUnload ()
        {
            base.ViewDidUnload ();
            
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
            
            ReleaseDesignerOutlets ();
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}


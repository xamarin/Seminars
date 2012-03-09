using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Xamarin.Contacts;

namespace MT52Demo
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        DialogViewController _vc;
        UINavigationController _nav;
        RootElement _rootElement;
        UIBarButtonItem _addButton;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            var book = new AddressBook ();
            
            _rootElement = new RootElement ("Json Example"){
                
                new Section ("Json Demo"){
                    JsonElement.FromFile ("sample.json"),        
                    new JsonElement ("Load from url", "http://localhost/sample.json")                    
                },
                new Section ("MT.D+Linq+Xamarin.Mobile"){
                    new RootElement ("Contacts with Phones") {
                        from c in book.Where (c => c.Phones.Count () > 0)
                        select new Section (c.DisplayName){
                            from p in c.Phones
                            select (Element)new StringElement (p.Number)
                        }
                    }
                },
                new Section ("Tasks Sample using Json")
            };
             
            _vc = new DialogViewController (_rootElement);
            _nav = new UINavigationController (_vc);
            
            window.RootViewController = _nav;
            window.MakeKeyAndVisible ();
            
            #region task demo
            
            int n = 0;
            
            _addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);
            _vc.NavigationItem.RightBarButtonItem = _addButton;
            
            _addButton.Clicked += (sender, e) => {
                
                ++n;
                
                var task = new Task{Name = "task " + n, DueDate = DateTime.Now};
                
                var taskElement = JsonElement.FromFile ("task.json");
                
                taskElement.Caption = task.Name;
                
                var description = taskElement ["task-description"] as EntryElement;
                
                if (description != null) {
                    description.Caption = task.Name;
                    description.Value = task.Description;       
                }
                
                var duedate = taskElement ["task-duedate"] as DateElement;
                
                if (duedate != null) {                
                    duedate.DateValue = task.DueDate;
                }
         
                _rootElement [2].Add (taskElement);
            };
            
            #endregion
            
            return true;
        }
    }
}


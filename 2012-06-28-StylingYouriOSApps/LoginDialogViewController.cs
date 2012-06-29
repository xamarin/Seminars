using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.AddressBook;
using MonoTouch.AddressBookUI;

namespace Xaminar
{
    public class LoginDialogViewController : BaseDialogViewController
    {
        public LoginDialogViewController() : this(false)
        {

        }

        public LoginDialogViewController(bool pushing) : base(pushing)
        {
            Title = "Login";
            Style = UITableViewStyle.Plain;
        }

        StyledStringElement firstElement, secondElement;
        public override void LoadView()
        {
            base.LoadView();

            RootElement root = new RootElement(Title);

            //you can also pass a view into the section, which makes putting in a new gradient seperator easy!
            // if you want the text, tho, you DO need to add your own UILabel.
            root.Add(new Section("") {
                (firstElement = new StyledStringElement("Address Book Viewer", "Show it!") {
                    Accessory = UITableViewCellAccessory.DisclosureIndicator
                }),
                (secondElement = new StyledStringElement("Push another of the same", DateTime.Now.ToString("hh:MM:ss")) {
                    Accessory = UITableViewCellAccessory.DisclosureIndicator
                })
            });

            firstElement.Tapped += delegate() {
                //lets throw up a contact picker, so we can show that the UIAppearance sticks
                ShowContactPicker();

            };

            secondElement.Tapped += delegate {
                var lv = new LoginDialogViewController(true);
                NavigationController.PushViewController(lv, true);
            };


            Root = root;

            //get rid of the nasty lines under our items
            TableView.TableFooterView = new UIView() {
                BackgroundColor = UIColor.Clear
            };

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.ReloadData();
        }

        ABPeoplePickerNavigationController peoplePicker;
        private void ShowContactPicker()
        {

            // The TableView.ReloadData() is to force the cells to reload, as they were showing as transparent.

            peoplePicker = new ABPeoplePickerNavigationController();
            peoplePicker.Cancelled += (sender, e) => { peoplePicker.DismissModalViewControllerAnimated(true); TableView.ReloadData();};
            peoplePicker.SelectPerson += (sender, e) => { peoplePicker.DismissModalViewControllerAnimated(true); TableView.ReloadData();};
            PresentModalViewController(peoplePicker, true); 
        }

    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Imaging;
using Xamarin.Contacts;

namespace ContactsSample
{
	public class MainPageViewModel
		: INotifyPropertyChanged
	{
		public MainPageViewModel()
		{
			Contact = Contacts.First();
		}

		public event EventHandler SelectedContact;
		public event PropertyChangedEventHandler PropertyChanged;

		public IEnumerable<Contact> Contacts
		{
			get { return addressBook; }
		}

		private BitmapImage thumb;
		public BitmapImage Thumbnail
		{
			get { return this.thumb ?? (this.thumb = this.contact.GetThumbnail()); }
		}

		private Contact contact;
		public Contact Contact
		{
			get { return this.contact; }
			set
			{
				if (this.contact == value)
					return;

				this.contact = value;
				this.thumb = null;
				OnPropertyChanged ("Contact");
				OnPropertyChanged ("Thumbnail");

				if (value != null)
					OnSelectedContact (EventArgs.Empty);
			}
		}

		private readonly AddressBook addressBook = new AddressBook();
		
		private void OnPropertyChanged (string name)
		{
			var changed = PropertyChanged;
			if (changed != null)
				changed (this, new PropertyChangedEventArgs (name));
		}

		private void OnSelectedContact (EventArgs e)
		{
			var selected = SelectedContact;
			if (selected != null)
				selected (this, e);
		}
	}
}

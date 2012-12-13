using System;
using System.Windows.Input;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ViewModels;

namespace TwitterSearch.Core.ViewModels
{
    public class HomeViewModel
        : MvxViewModel
    {
        public HomeViewModel()
        {
            PickRandomSearchTerm();
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; RaisePropertyChanged("SearchText"); }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new MvxRelayCommand(DoSearch);
            }
        }

        public ICommand PickRandomCommand
        {
            get
            {
                return new MvxRelayCommand(PickRandomSearchTerm);
            }
        }

        private void DoSearch()
        {
            var text = SearchText;
            if (!ShouldSearch(text))
                return;

            RequestNavigate<TwitterViewModel>(new { searchTerm = text });
        }

        private bool ShouldSearch(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;
            if (word.ToLower() == "javascript")
                return false;
            return true;
        }

        private void PickRandomSearchTerm()
        {
            var items = new[] { "Dinosaur", "WP7", "MonoTouch", "MonoDroid", "slodge", "kittens" };
            var r = new Random();
            var originalText = SearchText;
            var newText = originalText;
            while (originalText == newText)
            {
                var which = r.Next(items.Length);
                newText = items[which];
            }
            SearchText = newText;
        }
    }
}
using System;
using MonoTouch.Dialog;

namespace Xaminar
{
    public enum Category
    {
        Travel,
        Lodging,
        Books
    }

    /*
     * MonoTouch.Dialog auto-bound stuff appears to be quite hard to customize - atleast without changing the underlying api.
     * 
     * You can do it if you have control over the creation of the RootElement or DialogViewController (as I do in AppDelegate),
     * but if you have a pushed selection (like Category is), you need to create a new DialogViewController which inherits from your
     * base, so it can set the backgrounds, as UIAppearance doesn't yet allow a UITableView to be styled.
     * 
     * And without changing MonoTouch.Dialog, you can't do that. I think the BindingContext would need to allow you to inherit from it,
     * and allow you to make a new RootElement, which in turn allows you to override the creation of a DialogViewController....
     * 
     * Just have to use the "normal" way of doing it - ie, not using the bindings.
     * 
     */
        
    public class ExpenseModel
    {
        [Section("Expense Entry")]
    
        [Entry("Enter expense name")]
        public string Name;
    
        [Section("Expense Details")]
     
        [Caption("Description")]
        [Entry]
        public string Details;
        
        [Checkbox]
        public bool IsApproved = true;
    
        [Caption("Category")]
        public Category ExpenseCategory;
    }
}


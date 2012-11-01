using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;

namespace BingImageGridSplit
{
    public class AnimalsController : DialogViewController
    {
        public event EventHandler<AnimalSelectedEventArgs> AnimalSelected;

        List<string> animals = new List<string>{"Dolphins", "Cats", "Dogs", "Lions", 
            "Tigers", "Bears"};

        public List<string> Animals {
            get {
                return animals;
            }
        }

        public AnimalsController () : base (null)
        {
            Root = new RootElement ("Animals") {
                new Section () {
                    from animal in animals
                        select (Element) new StringElement(animal, () => {   
                            if(AnimalSelected != null)
                                AnimalSelected(this, new AnimalSelectedEventArgs{Animal = animal});
                        })
                }   
            };
        }
    }

    public class AnimalSelectedEventArgs : EventArgs
    {
        public string Animal { get; set; }
    }
}
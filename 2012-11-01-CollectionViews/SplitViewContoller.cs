using System;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;

namespace BingImageGridSplit
{
    public class SplitViewContoller : UISplitViewController
    {
        AnimalsController animalsController;
        BingImageGridViewController animalImageController;
        SplitDelegate sd;
        UICollectionViewFlowLayout flowLayout;
//        CircleLayout circleLayout;

        public SplitViewContoller () : base()
        {
            sd = new SplitDelegate ();
            Delegate = sd;

            animalsController = new AnimalsController ();

            flowLayout = new UICollectionViewFlowLayout (){
                SectionInset = new UIEdgeInsets (20,20,20,20)
            };
            animalImageController = new BingImageGridViewController (flowLayout);

//            circleLayout = new CircleLayout ();
//            animalImageController = new BingImageGridViewController (circleLayout);
           
            animalsController.AnimalSelected += (sender, e) => {
                animalImageController.LoadImages (e.Animal);
            };

            ViewControllers = new UIViewController[] {
                animalsController,
                animalImageController
            };
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            animalImageController.LoadImages (animalsController.Animals [0]);
        }

        class SplitDelegate : UISplitViewControllerDelegate
        {
            public override bool ShouldHideViewController (UISplitViewController svc, 
                UIViewController viewController, UIInterfaceOrientation inOrientation)
            { 
                return false; 
            }
        }

    }
}


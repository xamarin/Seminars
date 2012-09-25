using System;
using MonoTouch.UIKit;

namespace Presentation.AnimationPresentation
{
	[Demo ("View Controller", "Transitions")]
	public class TransitionsViewController : DemoViewController
	{
		public void CoverVertical ()
		{
			var vc = new DogViewController {
				ModalTransitionStyle = UIModalTransitionStyle.CoverVertical,
				ModalPresentationStyle = UIModalPresentationStyle.FormSheet,
			};
			PresentViewController (vc, true, null);
		}

		public void FlipHorizontal ()
		{
			var vc = new DogViewController {
				ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal,
				ModalPresentationStyle = UIModalPresentationStyle.FormSheet,
			};
			PresentViewController (vc, true, null);
		}

		public void CrossDissolve ()
		{
			var vc = new DogViewController {
				ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve,
				ModalPresentationStyle = UIModalPresentationStyle.FormSheet,
			};
			PresentViewController (vc, true, null);
		}
	}
}


using System;
using System.Linq;

namespace Presentation
{
	public class Demo
	{
		public string Name { get; private set; }
		public string Group { get; private set; }
		public Type ViewControllerType { get; private set; }

		public Demo (Type type)
		{
			ViewControllerType = type;

			var a = type.GetCustomAttributes (typeof (DemoAttribute), true)
					.OfType<DemoAttribute> ()
					.FirstOrDefault ();

			if (a != null) {
				Name = a.Name;
				Group = a.Group;
			}
			else {
				Name = type.Name;
				Group = "";
			}
		}
	}
}


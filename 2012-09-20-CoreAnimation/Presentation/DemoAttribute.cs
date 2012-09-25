using System;

namespace Presentation
{
	public class DemoAttribute : Attribute
	{
		public string Name { get; set; }
		public string Group { get; set; }

		public DemoAttribute (string name, string group)
		{
			Name = name;
			Group = group;
		}
	}
}


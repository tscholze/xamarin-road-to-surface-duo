using System;

namespace rTsd.Utils.MicrosoftDuoLibrary
{
    public class LayoutGuideChangedEventArgs : EventArgs
	{
		public LayoutGuideChangedEventArgs(LayoutGuide layoutGuide)
		{
			LayoutGuide = layoutGuide;
		}

		public LayoutGuide LayoutGuide { get; }
	}
}

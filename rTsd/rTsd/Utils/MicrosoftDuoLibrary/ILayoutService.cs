using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace rTsd.Utils.MicrosoftDuoLibrary
{
	public interface ILayoutService
	{
		Point? GetLocationOnScreen(VisualElement visualElement);

		void AddLayoutGuide(string name, Rectangle location);

		IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

		event EventHandler<LayoutGuideChangedEventArgs> LayoutGuideChanged;
	}

	public abstract class LayoutServiceBase : ILayoutService
	{
		public LayoutServiceBase()
		{
			LayoutGuides = LayoutGuidesInternal;
		}

		public event EventHandler<LayoutGuideChangedEventArgs> LayoutGuideChanged;

		public IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

		Dictionary<string, LayoutGuide> LayoutGuidesInternal { get; } =
			new Dictionary<string, LayoutGuide>();

		public void AddLayoutGuide(string name, Rectangle location)
		{
			var guide = new LayoutGuide(name, location);
			LayoutGuidesInternal[name] = guide;
			LayoutGuideChanged?.Invoke(this, new LayoutGuideChangedEventArgs(guide));
		}

		public abstract Point? GetLocationOnScreen(VisualElement visualElement);
	}

	public class LayoutGuide
	{
		public LayoutGuide(string name, Rectangle rectangle)
		{
			Name = name;
			Rectangle = rectangle;
		}

		public string Name { get; }
		public Rectangle Rectangle { get; }
	}
}

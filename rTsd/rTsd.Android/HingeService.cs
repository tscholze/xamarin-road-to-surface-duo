using System;
using System.ComponentModel;
using System.Linq;

using Android.App;
using Android.Views;
using Microsoft.Device.Display;
using rTsd.Droid;
using rTsd.Utils.MicrosoftDuoLibrary;
using Xamarin.Forms;

[assembly: Dependency(typeof(HingeService))]
namespace rTsd.Droid
{
	public class HingeService : IHingeService, IDisposable
	{
		static ScreenHelper helper;
		bool isDuo = false;
		HingeSensor hingeSensor;
		int _hingeAngle;
		Rectangle _hingeLocation;
		static Activity mainActivity;

		public static Activity MainActivity
		{
			get => mainActivity;
			set => mainActivity = value;
		}

		ILayoutService LayoutService => DependencyService.Get<ILayoutService>();

		public HingeService()
		{
			if (helper == null)
				helper = new ScreenHelper();

			isDuo = helper.Initialize(MainActivity);

			if (isDuo)
			{
				hingeSensor = new HingeSensor(MainActivity);
				hingeSensor.OnSensorChanged += OnSensorChanged;
				hingeSensor.StartListening();
			}
		}

		void OnSensorChanged(object sender, HingeSensor.HingeSensorChangedEventArgs e)
		{
			if (_hingeLocation != GetHinge())
			{
				_hingeLocation = GetHinge();
				LayoutService.AddLayoutGuide("Hinge", _hingeLocation);
			}

			if (_hingeAngle != e.HingeAngle)
				OnHingeUpdated?.Invoke(this, new HingeEventArgs(e.HingeAngle));

			_hingeAngle = e.HingeAngle;
		}

		public void Dispose()
		{
			if (hingeSensor != null)
			{
				hingeSensor.OnSensorChanged -= OnSensorChanged;
				hingeSensor.StopListening();
			}
		}

		public bool IsSpanned
			=> isDuo && (helper?.IsDualMode ?? false);

		public Rectangle GetHinge()
		{
			if (!isDuo || helper == null)
				return Rectangle.Zero;

			var rotation = ScreenHelper.GetRotation(helper.Activity);
			var hinge = helper.DisplayMask.GetBoundingRectsForRotation(rotation).FirstOrDefault();
			var hingeDp = new Rectangle(PixelsToDp(hinge.Left), PixelsToDp(hinge.Top), PixelsToDp(hinge.Width()), PixelsToDp(hinge.Height()));

			return hingeDp;
		}

		public bool IsLandscape
		{
			get
			{
				if (!isDuo || helper == null)
					return false;

				var rotation = ScreenHelper.GetRotation(helper.Activity);

				return (rotation == SurfaceOrientation.Rotation270 || rotation == SurfaceOrientation.Rotation90);
			}
		}

		double PixelsToDp(double px)
			=> px / MainActivity.Resources.DisplayMetrics.Density;

		public event EventHandler<HingeEventArgs> OnHingeUpdated;
		public event PropertyChangedEventHandler PropertyChanged;


		#region Custom changes

		public bool IsDuo => isDuo;

		#endregion
	}
}
using Foundation;
using NDC.Reminders.iOS.Controllers;
using UIKit;

namespace NDC.Reminders.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

		    var controller = new MainViewController
		    {
		        View = { BackgroundColor = UIColor.White },
		        Title = "Reminders"
		    };
            Window.RootViewController = new UINavigationController(controller);
            
            Window.MakeKeyAndVisible();

            return true;
		}
	}
}



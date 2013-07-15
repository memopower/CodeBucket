using System;
using MonoTouch.UIKit;
using RedPlum;
using System.Threading;
using MonoTouch;
using CodeBucket.Data;
using CodeFramework.Views;
using System.Linq;

namespace CodeBucket.Bitbucket.Controllers.Accounts
{
    public partial class LoginViewController : UIViewController
    {
        public Action<string, string> Login;

        private string _username;

        public string Username {
            get { return _username; }
            set {
                _username = value;
                if (User != null)
                    User.Text = _username;
            }
        }

        public LoginViewController()
            : base("LoginViewController", null)
        {
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(NavigationButton.Create(CodeFramework.Images.Buttons.Back, () => NavigationController.PopViewControllerAnimated(true)));
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.FromPatternImage(Images.LogoBehind);

            Title = "Login";
            Logo.Image = Images.BitbucketLogo;
            if (Username != null)
                User.Text = Username;

            User.ShouldReturn = delegate {
                Password.BecomeFirstResponder();
                return true;
            };
            Password.ShouldReturn = delegate {
                Password.ResignFirstResponder();

                //Run this in another thread
                Login(User.Text, Password.Text);
                return true;
            };
        }

        [Obsolete("Deprecated in iOS 6.0")]
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();

            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;

            ReleaseDesignerOutlets();
        }

        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                if (toInterfaceOrientation == UIInterfaceOrientation.Portrait || toInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown)
                    return true;
            }
            else
            {
                // Return true for supported orientations
                return true;
            }

            return false;
        }
    }
}


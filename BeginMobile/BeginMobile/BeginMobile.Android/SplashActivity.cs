using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BeginMobile.Android
{
    [Activity(Theme = "@style/ThemeSplash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(3000); //Let's wait awhile...
            StartActivity(typeof(MainActivity));
            // Create your application here
        }
    }
}
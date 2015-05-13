using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Views;
using Android.Widget;
using BeginMobile.Android.Renderers;
using BeginMobile.Menu;
using BeginMobile.Pages;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Wall;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(AppHome), typeof(CustomTabbedRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class CustomTabbedRenderer: TabbedRenderer
    {
        private Activity _activity;

        private const string LimitCounter = "9+";
        private const string SpaceCounter = " ";

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;

            if (_activity != null)
            {
                var actionBar = _activity.ActionBar;
                SetUpTabSelected(actionBar);
            }
        }

        protected override void OnDraw(global::Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
            var appHome = (AppHome)this.Element;
            ActionBar actionBar = _activity.ActionBar;

            SetUpTabs(actionBar, appHome);
        }

        # region "Helper methods"
        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }


        private void SetUpTabs(ActionBar actionBar, AppHome appHome)
        {
            if (actionBar.TabCount > 0)
            {
                var count = 0;
                var selectedTab = actionBar.SelectedTab;
                var numTabSelected = selectedTab != null ? selectedTab.Position : 0 ;

                while (count < actionBar.TabCount)
                {
                    var tabOne = actionBar.GetTabAt(count);

                    if (TabIsEmpty(tabOne))
                    {
                        var childTab = (TabContent)appHome.Children[count];

                        tabOne.SetCustomView(Resource.Layout.BarTabLayout);

                        var tabIcontAux = tabOne.CustomView.FindViewById<ImageView>(Resource.Id.tab_icon);
                        var tabTextAux = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_title);
                        var tabBadge = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_badge);

                        tabTextAux.Text = childTab.Title;

                        var typeChild = childTab.GetType();

                        if (typeChild == typeof(WallPage))
                        {
                            var tabMessage = (WallPage)childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconwallinactive
                                : Resource.Drawable.iconwallactive);

                            tabBadge.Visibility = ViewStates.Invisible;
                        }
                        else if (typeChild == typeof(MessageListPage))
                        {
                            var tabMessage = (MessageListPage)childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmessagesinactive
                                : Resource.Drawable.iconmessagesactive);

                            int counter;

                            if (int.TryParse(tabMessage.LabelCounter.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9 ?
                                        LimitCounter : SpaceCounter + tabMessage.LabelCounter.Text + SpaceCounter;

                                    tabTextAux.Gravity = GravityFlags.Bottom | GravityFlags.Left;
                                }
                                else
                                {
                                    tabBadge.Visibility = ViewStates.Invisible;
                                }
                            }
                            else
                            {
                                tabBadge.Visibility = ViewStates.Invisible;
                            }
                        }
                        else if (typeChild == typeof(Pages.Notifications.Notification))
                        {
                            var tabNotification = (Pages.Notifications.Notification)childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconnotificationsinactive
                                : Resource.Drawable.iconnotificationsactive);

                            int counter;

                            if (int.TryParse(tabNotification.LabelCounter.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9
                                        ? LimitCounter
                                        : SpaceCounter + tabNotification.LabelCounter.Text + SpaceCounter;

                                    tabTextAux.Gravity = GravityFlags.Bottom | GravityFlags.Left;
                                }
                                else
                                {
                                    tabBadge.Visibility = ViewStates.Invisible;
                                }
                            }
                            else
                            {
                                tabBadge.Visibility = ViewStates.Invisible;
                            }
                        }
                        else if (typeChild == typeof(ContactPage))
                        {
                            var tabMessage = (ContactPage)childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconcontactsinactive
                                : Resource.Drawable.iconcontactsactive);
                            tabBadge.Visibility = ViewStates.Invisible;
                        }
                        else if (typeChild == typeof(OptionsPage))
                        {
                            var tabMessage = (OptionsPage)childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmenuinactive
                                : Resource.Drawable.iconmenuactive);
                            tabBadge.Visibility = ViewStates.Invisible;
                        }

                    }
                    count++;
                }
            }
        }

        private void SetUpTabSelected(ActionBar actionBar)
        {
            var tabselected = actionBar.SelectedTab;
            if (actionBar.TabCount > 0 && tabselected != null && tabselected.CustomView != null)
            {
                var count = 0;
                var numTabSelected = actionBar.SelectedNavigationIndex;

                while (count < actionBar.TabCount)
                {
                    var tabOne = actionBar.GetTabAt(count);
                    var tabIcontAux = tabOne.CustomView.FindViewById<ImageView>(Resource.Id.tab_icon);

                    switch (count)
                    {
                        case 0:
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconwallinactive
                                : Resource.Drawable.iconwallactive);
                            break;
                        case 1:
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconnotificationsinactive
                                : Resource.Drawable.iconnotificationsactive);
                            break;
                        case 2:
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmessagesinactive
                                : Resource.Drawable.iconmessagesactive);
                            break;
                        case 3:
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconcontactsinactive
                                : Resource.Drawable.iconcontactsactive);
                            break;
                        case 4:
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmenuinactive
                                : Resource.Drawable.iconmenuactive);
                            break;
                    }

                    count++;
                }
            }
        }

        #endregion
    }
}
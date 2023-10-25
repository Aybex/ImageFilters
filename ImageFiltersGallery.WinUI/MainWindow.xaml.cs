﻿using ImageFiltersGallery.WinUI.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ImageFiltersGallery.WinUI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow(string appTitle)
    {
        InitializeComponent();
        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
        ContentFrame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());

        //ExtendsContentIntoTitleBar = true;
        this.AppTitle.Text = appTitle;
        SetTitleBar(AppTitleBar);
    }

    private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
            ContentFrame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);

        else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
        {
            Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString()!);
            ContentFrame.Navigate(newPage, null, args.RecommendedNavigationTransitionInfo);
        }
    }

    private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentFrame.CanGoBack) ContentFrame.GoBack();
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
    {
        NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

        // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
        if (ContentFrame.SourcePageType == typeof(SettingsPage))
            NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;

        else if (ContentFrame.SourcePageType != null)
        {
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(ContentFrame.SourcePageType.FullName));
        }

        NavigationViewControl.Header = ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
    }

}
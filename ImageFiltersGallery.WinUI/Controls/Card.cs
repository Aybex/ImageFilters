using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ImageFiltersGallery.WinUI.Controls;

public class Card : ContentControl
{
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header), typeof(object), typeof(Card), new PropertyMetadata(null));

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public Card()
    {
        //this.DefaultStyleKey = typeof(Card);

    }
}
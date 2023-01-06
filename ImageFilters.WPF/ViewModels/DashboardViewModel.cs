using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Wpf.Ui.Common.Interfaces;
using ImageFilters;
using ImageFilters.Imager.Interface;
using ImageFilters.Scripting;
using ImageFilters.Scripting.ScriptActions;
using LiveChartsCore.Defaults;
using System.Runtime.InteropServices;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Numerics;
using System.IO;

namespace ImageFilters.WPF.ViewModels;

public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized;
    readonly ScriptEngine scriptEngine = new();

    [ObservableProperty] private Bitmap _srcImage;

    //generate onchage
    partial void OnSrcImageChanged(Bitmap value)
    {
        scriptEngine.LoadImage(value);
    }


    [ObservableProperty]
    private Dictionary<string, IImageManipulator> _filters = SupportedManipulators.MANIPULATORS.ToDictionary(x => x.Key, y => y.Value);

    [ObservableProperty] private KeyValuePair<string, IImageManipulator>? _selectedFilter;

    //TODO: Implement Kernel function visualisation
    public ISeries[] Series { get; set; } = new ISeries[]
        {
        new LineSeries<ObservablePoint>
        {
          //  Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
            Values = new ObservableCollection<ObservablePoint>
            {
                new (-1, 2),
                new (-0.5, 0.5),
                new (0, 1),
                new (.5, .5),
                new (1, 2),

            },
            Fill = new SolidColorPaint(SKColor.Parse("00A881")),
            GeometrySize = 0,
        }
        };

 

    [RelayCommand]
    private void ProcessImage()
    {
        Console.WriteLine("Image processing in view model :");

        Thread thread = Thread.CurrentThread;
        Console.WriteLine($"Background: {thread.IsBackground}\n Thread Pool: {thread.IsThreadPoolThread}\n Thread ID: {thread.ManagedThreadId}\n");


        float factor = 4;
        bool applyToTarget = false;
        string methodName = SupportedManipulators.MANIPULATORS[0].Key;
        IImageManipulator method = SupportedManipulators.MANIPULATORS[0].Value;
        ushort targetWidth = Convert.ToUInt16(factor * _srcImage.Width);
        ushort targetHeight = Convert.ToUInt16(factor * _srcImage.Height);
        bool maintainAspect = false;
        bool useThresholds = true;
        bool useCenteredGrid = false;
        byte repetitionCount = 1;
        const OutOfBoundsMode horizontalBph = OutOfBoundsMode.HalfSampleSymmetric;
        const OutOfBoundsMode verticalBph = OutOfBoundsMode.HalfSampleSymmetric;
        float radius = 1;

        var command = new ResizeCommand(applyToTarget, method, targetWidth, targetHeight, 0, maintainAspect, horizontalBph, verticalBph, repetitionCount, useThresholds, useCenteredGrid, radius);

        scriptEngine.ExecuteAction(command);
        var resultImage = scriptEngine.GdiTarget;

        Console.WriteLine(resultImage.Width);
    }






    public void OnNavigatedTo()
    {

        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        SelectedFilter = Filters.First();
        _isInitialized = true;
    }

}
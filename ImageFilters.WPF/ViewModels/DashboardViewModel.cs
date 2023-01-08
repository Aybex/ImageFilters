using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFilters.Core;
using ImageFilters.Core.Imager.Interface;
using ImageFilters.Core.Scripting;
using ImageFilters.Core.Scripting.ScriptActions;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ImageFilters.WPF.Helpers;
using Wpf.Ui.Common.Interfaces;

namespace ImageFilters.WPF.ViewModels;

public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    //Fields
    private bool _isInitialized;
    private readonly ScriptEngine _scriptEngine = new();

    [NotifyPropertyChangedFor(nameof(SourceSize))]
    [ObservableProperty] private BitmapImage? _sourceImage;
    [ObservableProperty] private bool _imageNotLoaded = true;
    [ObservableProperty] private ushort _sourceWidth;
    [ObservableProperty] private ushort _sourceHeight;
    [ObservableProperty] private Dictionary<string, IImageManipulator> _filters = SupportedManipulators.MANIPULATORS.ToDictionary(x => x.Key, y => y.Value);
    [ObservableProperty] private KeyValuePair<string, IImageManipulator>? _selectedFilter;

    [NotifyPropertyChangedFor(nameof(TargetSize))]
    [ObservableProperty] private BitmapImage? _targetImage;
    [ObservableProperty] private ushort _targetWidth;
    [ObservableProperty] private ushort _targetHeight;

    public string SourceSize => $"W : {SourceWidth} | H : {SourceHeight}";
    public string TargetSize => $"W : {TargetWidth} | H : {TargetHeight}";

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

    //generate onchage
    partial void OnSourceImageChanged(BitmapImage value)
    {
        if (value is null) return;
        ImageNotLoaded = false;
        TargetWidth = SourceWidth = (ushort)value.Width;
        TargetHeight = SourceHeight = (ushort)value.Height;

        _scriptEngine.ExecuteAction(new LoadBitmapCommand(value.ToGdiImage()));
    }

    [RelayCommand]
    private void ProcessImage()
    {
        Console.WriteLine("Image processing in view model :");

        Thread thread = Thread.CurrentThread;
        Console.WriteLine($"Background: {thread.IsBackground}\n Thread Pool: {thread.IsThreadPoolThread}\n Thread ID: {thread.ManagedThreadId}\n");


        float factor = 4;
        bool applyToTarget = false;
        
        IImageManipulator method = SelectedFilter.Value.Value;
        ushort targetWidth = TargetWidth;
        ushort targetHeight = TargetHeight;
        bool maintainAspect = false;
        bool useThresholds = true;
        bool useCenteredGrid = false;
        byte repetitionCount = 1;
        const OutOfBoundsMode horizontalBph = OutOfBoundsMode.HalfSampleSymmetric;
        const OutOfBoundsMode verticalBph = OutOfBoundsMode.HalfSampleSymmetric;
        float radius = 1;

        var command = new ResizeCommand(applyToTarget, method, targetWidth, targetHeight, 0, maintainAspect, horizontalBph, verticalBph, repetitionCount, useThresholds, useCenteredGrid, radius);

        _scriptEngine.ExecuteAction(command);
        TargetImage = _scriptEngine.GdiTarget.ToBitmapImage();
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
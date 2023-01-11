using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFilters.Core;
using ImageFilters.Core.ImageManipulators;
using ImageFilters.Core.Imager.Interface;
using ImageFilters.Core.Scripting;
using ImageFilters.Core.Scripting.ScriptActions;
using ImageFilters.GUI.Helpers;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;

namespace ImageFilters.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    //Fields
    private readonly ScriptEngine _scriptEngine = new();

    [NotifyPropertyChangedFor(nameof(SourceSize))]
    [ObservableProperty] private BitmapImage? _sourceImage;
    [ObservableProperty] private bool _imageNotLoaded = true;
    [ObservableProperty] private ushort _sourceWidth;
    [ObservableProperty] private ushort _sourceHeight;
    [ObservableProperty] private Dictionary<string, IImageManipulator> _filters = SupportedManipulators.MANIPULATORS.ToDictionary(x => x.Key, y => y.Value);

    [NotifyPropertyChangedFor(nameof(FilterMethod))]
    [ObservableProperty] private KeyValuePair<string, IImageManipulator>? _selectedFilter;

    [NotifyPropertyChangedFor(nameof(TargetSize))]
    [ObservableProperty] private BitmapImage? _targetImage;
    [ObservableProperty] private ushort _targetWidth;
    [ObservableProperty] private ushort _targetHeight;
    [ObservableProperty] private bool _keepAspect;

    private IImageManipulator FilterMethod => SelectedFilter!.Value.Value;
    
    public ISeries[] Series { get; set; } =
    {
        new LineSeries<ObservablePoint>
        {
            Values = new ObservablePoint[]
            {
                new(-2f,0f),
                new(-1.75f,-0.02f),
                new(-1.5f,-0.06f),
                new(-1.25f,-0.07f),
                new(-1f,0f),
                new(-0.75f,0.23f),
                new(-0.5f,0.56f),
                new(-0.25f,0.87f),
                new(0f,1f),
                new(0.25f,0.87f),
                new(0.5f,0.56f),
                new(0.75f,0.23f),
                new(1f,0f),
                new(1.25f,-0.07f),
                new(1.5f,-0.06f),
                new(1.75f,-0.02f),
                new(2f,0f)
            },
            // Set he Fill property to build an area series
            // by default the series has a fill color based on your app theme
            Fill = new SolidColorPaint(SKColors.CornflowerBlue),

            Stroke = null,
            GeometryFill = null,
            GeometryStroke = null,
            
        }
    };
    
    public string TargetSize => (TargetImage is null ? $"W : {0} | H : {0}"
                                    : $"W : {TargetImage.PixelWidth} | H : {TargetImage.PixelHeight}");

    public string SourceSize => (SourceImage is null ? $"W : {0} | H : {0}"
                                : $"W : {SourceWidth} | H : {SourceHeight}");

    partial void OnSourceImageChanged(BitmapImage? value)
    {
        if (value is null) return;
        ImageNotLoaded = false;
        TargetWidth = SourceWidth = (ushort)value.Width;
        TargetHeight = SourceHeight = (ushort)value.Height;

        _scriptEngine.ExecuteAction(new LoadBitmapCommand(value.ToGdiImage()));
    }

    partial void OnTargetWidthChanged(ushort value)
    {
        if (!KeepAspect)
            return;
        var aspectRatio = (float)SourceWidth / SourceHeight;

        TargetHeight = (ushort)(TargetWidth / aspectRatio);
    }

    partial void OnSelectedFilterChanged(KeyValuePair<string, IImageManipulator>? value)
    {
        if(value is null)
            return;
        
        var method = value.Value.Value;
        var kernelBasedResampler = method as Resampler;
        var kernelBasedRadiusResampler = method as RadiusResampler;
        if (kernelBasedResampler is null && kernelBasedRadiusResampler is null)
            return;

        var info = kernelBasedRadiusResampler?.GetKernelMethodInfo(2) ?? kernelBasedResampler!.GetKernelMethodInfo();

        List<ObservablePoint> values = new();
        
        for (var x = -info.KernelRadius; x <= info.KernelRadius; x += 0.1f)
            values.Add(new(x, Math.Round(info.Kernel(x), 3)));

        Series[0].Values = values;
    }
    [RelayCommand]
    private async Task ProcessImage()
    {
        if (SourceImage is null) return;

        await Task.Run(() =>
        {
            float factor = 4;
            bool applyToTarget = false;

            IImageManipulator method = SelectedFilter.Value.Value;

            bool useThresholds = true;
            bool useCenteredGrid = false;
            byte repetitionCount = 1;
            const OutOfBoundsMode horizontalBph = OutOfBoundsMode.HalfSampleSymmetric;
            const OutOfBoundsMode verticalBph = OutOfBoundsMode.HalfSampleSymmetric;
            float radius = 1;

            var command = new ResizeCommand(applyToTarget, method, TargetWidth, TargetHeight, 0, KeepAspect,
                horizontalBph, verticalBph, repetitionCount, useThresholds, useCenteredGrid, radius);

            _scriptEngine.ExecuteAction(command);
        });
        TargetImage = _scriptEngine.GdiTarget.ToBitmapImage();
        TargetWidth = (ushort)TargetImage.PixelWidth;
        TargetHeight = (ushort)TargetImage.PixelHeight;
    }

    public void CalculateDimensions(bool maintainWidth = true)
    {
        if (!KeepAspect)
            return;
        var aspectRatio = (float)SourceWidth / SourceHeight;

        if (maintainWidth)
            TargetHeight = (ushort)(TargetWidth / aspectRatio);
        else
            TargetWidth = (ushort)(TargetHeight * aspectRatio);
    }

    public MainWindowViewModel()
    {
        SelectedFilter = Filters.First();
    }

}
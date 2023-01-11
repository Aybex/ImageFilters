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
using LiveChartsCore.SkiaSharpView.Painting.Effects;
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
			
			// Set he Fill property to build an area series
			// by default the series has a fill color based on your app theme
			Fill = new SolidColorPaint(SKColors.CornflowerBlue),

            Stroke = null,
            GeometryFill = null,
            GeometryStroke = null,

        }
    };

    public Axis[] XAxes { get; set; } = {
    new() {
        NamePaint = new SolidColorPaint(SKColors.White),
        LabelsPaint = new SolidColorPaint(SKColors.White),
        TextSize = 10,
        MinStep = 0.5,
        SeparatorsPaint = new SolidColorPaint(SKColors.White) { StrokeThickness = 1, PathEffect = new DashEffect(new float[] { 3, 3 }) }
    }};

    public Axis[] YAxes { get; set; } = {
    new() {
        NamePaint = new SolidColorPaint(SKColors.White),
        LabelsPaint = new SolidColorPaint(SKColors.White),
        TextSize = 10,
        MinStep = 1,

        SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray) { StrokeThickness = 1, PathEffect = new DashEffect(new float[] { 3, 3 }) }
    }};



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
        List<ObservablePoint> values = new();
        if (value is not null)
        {
            var method = value.Value.Value;
            var kernelBasedResampler = method as Resampler;
            var kernelBasedRadiusResampler = method as RadiusResampler;
            if (kernelBasedResampler is null && kernelBasedRadiusResampler is null)
                return;

            var info = kernelBasedRadiusResampler?.GetKernelMethodInfo(2) ?? kernelBasedResampler!.GetKernelMethodInfo();


            double minY = 0f;
            for (var x = -info.KernelRadius; x <= info.KernelRadius; x += 0.05f)
            {
                var y = Math.Round(info.Kernel(x), 3);
                if (y < minY)
                    minY = y;
                values.Add(new(x, y));

            }

            YAxes[0].MinLimit = minY - 0.5f;
        }
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
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFilters.Core;
using ImageFilters.Core.Imager.Interface;
using ImageFilters.Core.Scripting;
using ImageFilters.Core.Scripting.ScriptActions;
using ImageFilters.GUI.Helpers;


namespace ImageFilters.GUI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
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

    public MainWindowViewModel()
    {
        SelectedFilter = Filters.First();
        _isInitialized = true;
    }
    //TODO: Implement Kernel function visualisation

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

    

}
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
using SkiaSharp;

namespace ImageFilters.GUI.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
	//Fields
	private readonly ScriptEngine _scriptEngine = new();

	[NotifyPropertyChangedFor(nameof(SourceSize))]
	[ObservableProperty] private BitmapImage? _sourceImage;
	[ObservableProperty] private bool _imageNotLoaded = true;
	[ObservableProperty] private ushort _sourceWidth;
	[ObservableProperty] private ushort _sourceHeight;
	[ObservableProperty] private Dictionary<string, IImageManipulator> _filters = new(SupportedManipulators.MANIPULATORS);

	[ObservableProperty] private IImageManipulator _selectedFilter;
	[NotifyPropertyChangedFor(nameof(TargetSize))]
	[ObservableProperty] private BitmapImage? _targetImage;
	[ObservableProperty] private ushort _targetWidth;
	[ObservableProperty] private ushort _targetHeight;
	[ObservableProperty] private bool _keepAspect;
	[ObservableProperty] private bool _useCentralGrid;
	[ObservableProperty] private bool _useTreshholds;

	[NotifyPropertyChangedFor(nameof(SelectedFilter))]
	[ObservableProperty] private float _radius = 1;

	[ObservableProperty] private OutOfBoundsMode _boundsMode;

	public ISeries[] Series { get; set; } =
	{
		new LineSeries<ObservablePoint>
		{
			
			// Set he Fill property to build an area series
			// by default the series has a fill color based on your app theme
			Fill = new SolidColorPaint(SKColors.Aquamarine),

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

	partial void OnSelectedFilterChanged(IImageManipulator value)
	{
		UpdateKernelChart(value);
	}
	partial void OnRadiusChanged(float value)
	{
		UpdateKernelChart(SelectedFilter);
	}

	void UpdateKernelChart(IImageManipulator method)
	{
		List<ObservablePoint> values = new();

		Radius = method.SupportsRadius ? Radius : 1;
		var kernelBasedResampler = method as Resampler;
		var kernelBasedRadiusResampler = method as RadiusResampler;
		double minY = 0f;

		if (kernelBasedResampler is not null || kernelBasedRadiusResampler is not null)
		{
			var info = kernelBasedRadiusResampler?.GetKernelMethodInfo(Radius) != null ? kernelBasedRadiusResampler.GetKernelMethodInfo(Radius) : kernelBasedResampler!.GetKernelMethodInfo();
			for (var x = -info.KernelRadius; x <= info.KernelRadius; x += 0.05f)
			{
				var y = Math.Round(info.Kernel(x), 3);
				if (y < minY)
					minY = y;
				values.Add(new(x, y));
			}
		}
		YAxes[0].MinLimit = minY - 0.5f;
		Series[0].Values = values;
	}
	[RelayCommand]
	private async Task ProcessImage()
	{
		if (SourceImage is null) return;

		await Task.Run(() =>
		{
			bool applyToTarget = false;
			byte repetitionCount = 1;

			var command = new ResizeCommand(applyToTarget, SelectedFilter, TargetWidth, TargetHeight, 0, KeepAspect,
				BoundsMode, BoundsMode, repetitionCount, UseTreshholds, UseCentralGrid, Radius);

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



}
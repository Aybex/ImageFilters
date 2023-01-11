using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace ImageFilters.GUI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        LiveCharts.Configure(config =>
                config
                    // registers SkiaSharp as the library backend
                    // REQUIRED unless you build your own
                    .AddSkiaSharp()

                    // adds the default supported types
                    // OPTIONAL but highly recommend
                    .AddDefaultMappers()

                    // select a theme, default is Light
                    // OPTIONAL
                    //.AddDarkTheme()
                    .AddLightTheme()

            // .HasMap<Foo>( .... )
            // .HasMap<Bar>( .... )
        );
    }
}
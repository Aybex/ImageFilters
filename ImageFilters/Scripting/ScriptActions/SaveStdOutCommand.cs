using System.Drawing;
using System.Drawing.Imaging;
using ImageFilters.Imager;

namespace ImageFilters.Scripting.ScriptActions;

public class SaveStdOutCommand : IScriptAction
{
    #region Implementation of IScriptAction
    public bool ChangesSourceImage => false;

    public bool ChangesTargetImage => false;
    public bool ProvidesNewGdiSource => false;

    public bool Execute()
    {
        using var stream = Console.OpenStandardOutput();
        TargetImage.ToBitmap().Save(stream, ImageFormat.Png);

        return true;
    }

    public Bitmap GdiSource => null;

    public cImage SourceImage { get; set; }

    public cImage TargetImage { get; set; }
    #endregion

}
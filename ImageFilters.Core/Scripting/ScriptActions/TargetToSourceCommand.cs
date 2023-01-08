using System.Drawing;
using ImageFilters.Core.Imager;

namespace ImageFilters.Core.Scripting.ScriptActions;

public class TargetToSourceCommand : IScriptAction
{
    #region Implementation of IScriptAction
    public bool ChangesSourceImage => true;
    public bool ChangesTargetImage => true;
    public bool ProvidesNewGdiSource => false;

    public bool Execute()
    {
        SourceImage = TargetImage;
        TargetImage = null;
        return true;
    }

    public Bitmap GdiSource => null;

    public cImage SourceImage { get; set; }

    public cImage TargetImage { get; set; }
    #endregion
}
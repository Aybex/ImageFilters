using System.Diagnostics.Contracts;
using System.Drawing;
using ImageFilters.Core.Imager;

namespace ImageFilters.Core.Scripting;

public class ScriptEngine
{

    /// <summary>
    /// Current source image.
    /// </summary>
    private cImage _sourceImage;
    /// <summary>
    /// Current source image as a GDI+ version.
    /// </summary>
    private Bitmap _gdiSource;
    /// <summary>
    /// Current target image.
    /// </summary>
    private cImage _targetImage;
    /// <summary>
    /// Current source image as a GDI+ version.
    /// </summary>
    private Bitmap _gdiTarget;

    /// <summary>
    /// Gets or sets the source image.
    /// </summary>
    /// <value>
    /// The source image.
    /// </value>
    public cImage SourceImage
    {
        get => _sourceImage;
        private set
        {
            _sourceImage = value;
            _gdiSource?.Dispose();
            _gdiSource = null;
        }
    }

    /// <summary>
    /// Gets or sets the target image.
    /// </summary>
    /// <value>
    /// The target image.
    /// </value>
    public cImage TargetImage
    {
        get => _targetImage;
        private set
        {
            _targetImage = value;
            _gdiTarget?.Dispose();
            _gdiTarget = null;
        }
    }

    /// <summary>
    /// Gets the GDI source.
    /// </summary>
    public Bitmap GdiSource => _gdiSource ?? (_gdiSource = _sourceImage?.ToBitmap());

    /// <summary>
    /// Gets the GDI target.
    /// </summary>
    public Bitmap GdiTarget => _gdiTarget ?? (_gdiTarget = _targetImage?.ToBitmap());

    /// <summary>
    /// Current list of actions.
    /// </summary>
    private readonly List<IScriptAction> _actionList = new();

    /// <summary>
    /// Gets a value indicating whether this instance is source image changed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is source image changed; otherwise, <c>false</c>.
    /// </value>
    public bool IsSourceImageChanged { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is target image changed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is target image changed; otherwise, <c>false</c>.
    /// </value>
    public bool IsTargetImageChanged { get; private set; }

    /// <summary>
    /// Clears the action list.
    /// </summary>
    public void Clear() => _actionList.Clear();

    /// <summary>
    /// Gets the actions.
    /// Note: We're creating an enumeration so our own list stays save and is not modified by another class.
    /// </summary>
    public IEnumerable<IScriptAction> Actions => _actionList.Select(t => t);

    /// <summary>
    /// Executes the action.
    /// </summary>
    /// <param name="action">The action.</param>
    public void ExecuteAction(IScriptAction action) => _ExecuteAction(action, true);

    /// <summary>
    /// Repeats the actions from the action list.
    /// </summary>
    /// <param name="preAction">The pre action.</param>
    /// <param name="postAction">The post action.</param>
    public void RepeatActions(Action<ScriptEngine, IScriptAction> preAction = null, Action<ScriptEngine, IScriptAction> postAction = null)
    {
        var actions = _actionList;
        foreach (var action in actions)
        {
            preAction?.Invoke(this, action);
            _ExecuteAction(action, false);
            postAction?.Invoke(this, action);
        }
    }

    /// <summary>
    /// Adds the given action without executing it.
    /// </summary>
    /// <param name="action">The action.</param>
    public void AddWithoutExecution(IScriptAction action)
    {
        Contract.Requires(action != null);
        _actionList.Add(action);
    }

    /// <summary>
    /// Executes the given action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="addToList">if set to <c>true</c> the action will be added to the action list afterwards.</param>
    private void _ExecuteAction(IScriptAction action, bool addToList)
    {
        Contract.Requires(action != null);

        action.SourceImage = SourceImage;
        action.TargetImage = TargetImage;

        IsSourceImageChanged = false;
        IsTargetImageChanged = false;

        var result = action.Execute();

        // execution of action failed
        Contract.Assert(result, "action failed somehow");

        if (addToList)
            AddWithoutExecution(action);

        if (action.ChangesSourceImage)
        {
            SourceImage = action.SourceImage;
            IsSourceImageChanged = true;
        }

        if (action.ChangesTargetImage)
        {
            TargetImage = action.TargetImage;
            IsTargetImageChanged = true;
        }

        if (action.ProvidesNewGdiSource)
            _gdiSource = action.GdiSource;
    }

    /// <summary>
    /// Removes everything since the last source change.
    /// </summary>
    public void RevertToLastSource()
    {
        while (_actionList.Any() && !_actionList.Last().ChangesSourceImage)
            _actionList.RemoveAt(_actionList.Count - 1);
    }
}

namespace ImageFilters.Library.Imager.Interface; 

public class ScalerInformation {
  private readonly string _displayName;
  private readonly string _description;
  private readonly byte _scaleFactorX;
  private readonly byte _scaleFactorY;
  public ScalerInformation(string displayName, string description, byte scaleFactorX, byte scaleFactorY) {
    _description = description;
    _scaleFactorX = scaleFactorX;
    _scaleFactorY = scaleFactorY;
    _displayName = displayName;
  }

  public string Description => _description;

  public byte ScaleFactorX => _scaleFactorX;

  public byte ScaleFactorY => _scaleFactorY;

  public string DisplayName => _displayName;
}
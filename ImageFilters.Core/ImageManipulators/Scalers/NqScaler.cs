using ImageFilters.Core.Imager;
using ImageFilters.Core.Imager.Interface;

namespace ImageFilters.Core.ImageManipulators.Scalers; 

public class NqScaler : AScaler {
  private readonly NqScalerType _type;
  private readonly NqMode _mode;

  #region Implementation of AScaler
  public override cImage Apply(cImage source) => source.ApplyScaler(_type, _mode);
  public override byte ScaleFactorX { get; }
  public override byte ScaleFactorY { get; }
  public override string Description => ReflectionUtils.GetDescriptionForEnumValue(_type);

  #endregion

  public NqScaler(NqScalerType type, NqMode mode) {
    var info = cImage.GetPixelScalerInfo(type);
    _type = type;
    _mode = mode;
    ScaleFactorX = info.Item1;
    ScaleFactorY = info.Item2;
  }
}
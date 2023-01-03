namespace ImageFilters.Library; 

public enum ExitCode {
  OK = 0,
  UnknownParameter=1,
  TooLessArguments=2,
  JpegNotSupportedOnThisPlatform=3,
  NothingToSave=4,
  FilenameMustNotBeNull=5,
  InvalidTargetDimensions=6,
  CouldNotParseDimensionsAsWord=7,
  NothingToResize=8,
  UnknownFilter=9,
  ExceptionDuringImageLoad=10,
  ExceptionDuringImageWrite=11,
  TargetFileCouldNotBeSaved=17,
  InvalidFilterDescription=12,
  CouldNotParseParameterAsFloat=13,
  CouldNotParseParameterAsByte=14,
  InvalidOutOfBoundsMode=15,
  RuntimeError=16,
}
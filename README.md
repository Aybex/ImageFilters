# ImageFilters

This project is a collection of multiple image filters packed together (mostly upscalers), written in C# .NET 7.
The goal is more like getting each possible available rescaling algorithm into the library.

This project is based on the work of [Hawkynt](https://github.com/Hawkynt), initial project available [here](https://github.com/Hawkynt/2dimagefilter).

The project was optimized, modernized, ported to .NET 7 and split into 3 different C# Projects : Library of all filters and upscalers, UI and CLI.
A new GUI was made in WPF in modern fluent windows 11 style using WIN-UI Library

  Running all **129 Filters** on a 128x128 image, on a 4x Upscale when it's available gives **-60%**  of time reduction and **-82%** of memory gain.
| Method | Mean | Error | StdDev | Median | Allocated |
|--|--|--|--|--|--| 
|Original with .NET Framework 4.6|1608 ms|42 ms|104 ms|1607 ms|3359 MB|
|This project with .NET 7|646 ms|9 ms|15 ms|646 ms|621 MB| 

## Prerequisites

 - [.NET 7 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-7.0.1-windows-x64-installer)

## List of Filters
 - NearestNeighbor <GDI+>
- Bilinear <GDI+>
- Bicubic <GDI+>
- HighQualityBilinear <GDI+>
- HighQualityBicubic <GDI+>
- Rectangular
- Bicubic
- Schaum2
- Schaum3
- BSpline2
- BSpline3
- BSpline5
- BSpline7
- BSpline9
- BSpline11
- OMoms3
- OMoms5
- OMoms7
- Triangular
- Welch
- Hann
- Hamming
- Blackman
- Nuttal
- BlackmanNuttal
- BlackmanHarris
- FlatTop
- PowerOfCosine
- Cosine
- Gauss
- Tukey
- Poisson
- BartlettHann
- HanningPoisson
- Bohman
- Cauchy
- Lanczos
- -50% Scanlines
- +50% Scanlines
- +100% Scanlines
- -50% VScanlines
- +50% VScanlines
- +100% VScanlines
- MAME TV 2x
- MAME TV 3x
- MAME RGB 2x
- MAME RGB 3x
- Hawkynt TV 2x
- Hawkynt TV 3x
- Bilinear Plus Original
- Bilinear Plus
- Eagle 2x
- Eagle 3x
- Eagle 3xB
- SuperEagle
- SaI 2x
- Super SaI
- AdvInterp 2x
- AdvInterp 3x
- Scale 2x
- Scale 3x
- EPXB
- EPXC
- EPX3
- Reverse AA
- DES
- DES II
- 2xSCL
- Super 2xSCL
- Ultra 2xSCL
- XBR 2x <NoBlend>
- XBR 3x <NoBlend>
- XBR 3x (modified) <NoBlend>
- XBR 4x <NoBlend>
- XBR 2x
- XBR 3x
- XBR 3x (modified)
- XBR 4x
- XBR 5x (legacy)
- XBRz 2x
- XBRz 3x
- XBRz 4x
- XBRz 5x
- HQ 2x
- HQ 2x Bold
- HQ 2x Smart
- HQ 2x3
- HQ 2x3 Bold
- HQ 2x3 Smart
- HQ 2x4
- HQ 2x4 Bold
- HQ 2x4 Smart
- HQ 3x
- HQ 3x Bold
- HQ 3x Smart
- HQ 4x
- HQ 4x Bold
- HQ 4x Smart
- LQ 2x
- LQ 2x Bold
- LQ 2x Smart
- LQ 2x3
- LQ 2x3 Bold
- LQ 2x3 Smart
- LQ 2x4
- LQ 2x4 Bold
- LQ 2x4 Smart
- LQ 3x
- LQ 3x Bold
- LQ 3x Smart
- LQ 4x
- LQ 4x Bold
- LQ 4x Smart
- Red
- Green
- Blue
- Alpha
- Luminance
- ChrominanceU
- ChrominanceV
- u
- v
- Hue
- Hue Colored
- Brightness
- Min
- Max
- ExtractColors
- ExtractDeltas 

## Pixel Upscaler credits 
-   Eagle (the godfather himself)
-   Super Eagle (thanks Kreed and ZSNES)
-   SaI2x, Super2xSaI (also Kreed and DOSBox)
-   Scale2x, Scale3x (thanks MAME for these)
-   AdvInterp2x, AdvInterp3x (also MAME)
-   HQ2x, HQ3x, HQ4x (Maxim Stepin)
-   LQ2x, LQ3x, LQ4x (AFAIK SNES9x but AdvMAME also)
-   HQ2x3, HQ2x4, LQ2x3, LQ2x4 (AdvMAME again)
-   nQx Bold and Smart Version (SNES9x, VirtualBoyAdvance)
-   Bilinear Plus Original and Modified (VBA-rr)
-   XBR2x, XBR3x, XBR4x Normal and NonBlend (thanks Hyllian)
-   Resampling kernels (Pascal Getreuer)
-   XBRz (Zenju)
-   SCL, DES (FNES)

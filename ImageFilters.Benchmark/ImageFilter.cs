using System.Drawing;
using System.Drawing.Imaging;
using BenchmarkDotNet.Attributes;
using ImageFilters;
using ImageFilters.Core;
using ImageFilters.Core.Imager.Interface;
using ImageFilters.Core.Scripting;
using ImageFilters.Core.Scripting.ScriptActions;

namespace ImageFilters.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
 //   [ShortRunJob]
    public class ImageFilter
    {
        //Props and Fiels :
        readonly ScriptEngine scriptEngine = new();
        Image sourceImage;
        Image resultImage;

        public ImageFilter()
        {
            LoadImage("Images/1k.png");
        }

        private void LoadImage(string fileName)
        {
            try
            {
                scriptEngine.ExecuteAction(new LoadFileCommand(fileName));
                sourceImage = scriptEngine.GdiSource;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Resources.txCouldNotLoadImage", fileName, exception.Message);
            }

        }

        private void SaveImage(string method)
        {
            var getDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            method = invalidChars.Aggregate(method, (current, c) => current.Replace(c.ToString(), ""));

            string fileName = getDirectory + @"\Images\Results\" + method + ".png";

            Directory.CreateDirectory(getDirectory + @"\Images\Results");
            resultImage.Save(fileName, ImageFormat.Png);
        }

        private Image ScaleImage(int methodIndex, bool saveImage = false, float factor = 2)
        {
            bool applyToTarget = false;
            string methodName = SupportedManipulators.MANIPULATORS[methodIndex].Key;
            IImageManipulator method = SupportedManipulators.MANIPULATORS[methodIndex].Value;
            ushort targetWidth = Convert.ToUInt16(factor * sourceImage.Width);
            ushort targetHeight = Convert.ToUInt16(factor * sourceImage.Height);
            bool maintainAspect = false;
            bool useThresholds = true;
            bool useCenteredGrid = false;
            byte repetitionCount = 1;
            const OutOfBoundsMode horizontalBph = OutOfBoundsMode.HalfSampleSymmetric;
            const OutOfBoundsMode verticalBph = OutOfBoundsMode.HalfSampleSymmetric;
            float radius = 1;

            var command = new ResizeCommand(applyToTarget, method, targetWidth, targetHeight, 0, maintainAspect, horizontalBph, verticalBph, repetitionCount, useThresholds, useCenteredGrid, radius);

            scriptEngine.ExecuteAction(command);
            resultImage = scriptEngine.GdiTarget;

            if (saveImage)
                SaveImage(methodName);

            return resultImage;
        }

        #region MethodsBenchmark 

        [Benchmark] public Image NearestNeighborGDI() { return ScaleImage(0); }
        //[Benchmark] public Image BilinearGDI() { return ScaleImage(1); }
        //[Benchmark] public Image BicubicGDI() { return ScaleImage(2); }
        [Benchmark] public Image HighQualityBilinearGDI() { return ScaleImage(3); }
        [Benchmark] public Image HighQualityBicubicGDI() { return ScaleImage(4); }
        [Benchmark] public Image Rectangular() { return ScaleImage(5); }
        //[Benchmark] public Image Bicubic() { return ScaleImage(6); }
        //[Benchmark] public Image Schaum2() { return ScaleImage(7); }
        //[Benchmark] public Image Schaum3() { return ScaleImage(8); }
        //[Benchmark] public Image BSpline2() { return ScaleImage(9); }
        //[Benchmark] public Image BSpline3() { return ScaleImage(10); }
        //[Benchmark] public Image BSpline5() { return ScaleImage(11); }
        //[Benchmark] public Image BSpline7() { return ScaleImage(12); }
        //[Benchmark] public Image BSpline9() { return ScaleImage(13); }
        //[Benchmark] public Image BSpline11() { return ScaleImage(14); }
        //[Benchmark] public Image OMoms3() { return ScaleImage(15); }
        //[Benchmark] public Image OMoms5() { return ScaleImage(16); }
        //[Benchmark] public Image OMoms7() { return ScaleImage(17); }
        [Benchmark] public Image Triangular() { return ScaleImage(18); }
        [Benchmark] public Image Welch() { return ScaleImage(19); }
        [Benchmark] public Image Hann() { return ScaleImage(20); }
        [Benchmark] public Image Hamming() { return ScaleImage(21); }
        [Benchmark] public Image Blackman() { return ScaleImage(22); }
        //[Benchmark] public Image Nuttal() { return ScaleImage(23); }
        //[Benchmark] public Image BlackmanNuttal() { return ScaleImage(24); }
        //[Benchmark] public Image BlackmanHarris() { return ScaleImage(25); }
        //[Benchmark] public Image FlatTop() { return ScaleImage(26); }
        //[Benchmark] public Image PowerOfCosine() { return ScaleImage(27); }
        //[Benchmark] public Image Cosine() { return ScaleImage(28); }
        //[Benchmark] public Image Gauss() { return ScaleImage(29); }
        //[Benchmark] public Image Tukey() { return ScaleImage(30); }
        //[Benchmark] public Image Poisson() { return ScaleImage(31); }
        //[Benchmark] public Image BartlettHann() { return ScaleImage(32); }
        //[Benchmark] public Image HanningPoisson() { return ScaleImage(33); }
        //[Benchmark] public Image Bohman() { return ScaleImage(34); }
        //[Benchmark] public Image Cauchy() { return ScaleImage(35); }
        [Benchmark] public Image Lanczos() { return ScaleImage(36); }
        //[Benchmark] public Image Scanlines50Minus() { return ScaleImage(37); }
        //[Benchmark] public Image Scanlines50Plus() { return ScaleImage(38); }
        //[Benchmark] public Image Scanlines100Plus() { return ScaleImage(39); }
        //[Benchmark] public Image VScanlines50Minus() { return ScaleImage(40); }
        //[Benchmark] public Image VScanlines50Plus() { return ScaleImage(41); }
        //[Benchmark] public Image VScanlines100Plus() { return ScaleImage(42); }
        //[Benchmark] public Image MAMETV2x() { return ScaleImage(43); }
        //[Benchmark] public Image MAMETV3x() { return ScaleImage(44); }
        //[Benchmark] public Image MAMERGB2x() { return ScaleImage(45); }
        //[Benchmark] public Image MAMERGB3x() { return ScaleImage(46); }
        //[Benchmark] public Image HawkyntTV2x() { return ScaleImage(47); }
        //[Benchmark] public Image HawkyntTV3x() { return ScaleImage(48); }
        //[Benchmark] public Image BilinearPlusOriginal() { return ScaleImage(49); }
        //[Benchmark] public Image BilinearPlus() { return ScaleImage(50); }
        [Benchmark] public Image Eagle2x() { return ScaleImage(51); }
        //[Benchmark] public Image Eagle3x() { return ScaleImage(52); }
        //[Benchmark] public Image Eagle3xB() { return ScaleImage(53); }
        [Benchmark] public Image SuperEagle() { return ScaleImage(54); }
        [Benchmark] public Image SaI2x() { return ScaleImage(55); }
        [Benchmark] public Image SuperSaI() { return ScaleImage(56); }
        //[Benchmark] public Image AdvInterp2x() { return ScaleImage(57); }
        //[Benchmark] public Image AdvInterp3x() { return ScaleImage(58); }
        [Benchmark] public Image Scale2x() { return ScaleImage(59); }
        [Benchmark] public Image Scale3x() { return ScaleImage(60); }
        [Benchmark] public Image EPXB() { return ScaleImage(61); }
        [Benchmark] public Image EPXC() { return ScaleImage(62); }
        [Benchmark] public Image EPX3() { return ScaleImage(63); }
        [Benchmark] public Image ReverseAA() { return ScaleImage(64); }
        [Benchmark] public Image DES() { return ScaleImage(65); }
        [Benchmark] public Image DESII() { return ScaleImage(66); }
        [Benchmark] public Image SCL2x() { return ScaleImage(67); }
        [Benchmark] public Image Super2xSCL() { return ScaleImage(68); }
        [Benchmark] public Image Ultra2xSCL() { return ScaleImage(69); }
        [Benchmark] public Image XBR2xNoBlend() { return ScaleImage(70); }
        [Benchmark] public Image XBR3xNoBlend() { return ScaleImage(71); }
        [Benchmark] public Image XBR3xmodifiedNoBlend() { return ScaleImage(72); }
        [Benchmark] public Image XBR4xNoBlend() { return ScaleImage(73); }
        [Benchmark] public Image XBR2x() { return ScaleImage(74); }
        [Benchmark] public Image XBR3x() { return ScaleImage(75); }
        [Benchmark] public Image XBR3xmodified() { return ScaleImage(76); }
        [Benchmark] public Image XBR4x() { return ScaleImage(77); }
        //[Benchmark] public Image XBR5xlegacy() { return ScaleImage(78); }
        [Benchmark] public Image XBRz2x() { return ScaleImage(79); }
        [Benchmark] public Image XBRz3x() { return ScaleImage(80); }
        [Benchmark] public Image XBRz4x() { return ScaleImage(81); }
        [Benchmark] public Image XBRz5x() { return ScaleImage(82); }
        [Benchmark] public Image HQ2x() { return ScaleImage(83); }
        [Benchmark] public Image HQ2xBold() { return ScaleImage(84); }
        [Benchmark] public Image HQ2xSmart() { return ScaleImage(85); }
        [Benchmark] public Image HQ2x3() { return ScaleImage(86); }
        [Benchmark] public Image HQ2x3Bold() { return ScaleImage(87); }
        [Benchmark] public Image HQ2x3Smart() { return ScaleImage(88); }
        [Benchmark] public Image HQ2x4() { return ScaleImage(89); }
        [Benchmark] public Image HQ2x4Bold() { return ScaleImage(90); }
        [Benchmark] public Image HQ2x4Smart() { return ScaleImage(91); }
        [Benchmark] public Image HQ3x() { return ScaleImage(92); }
        [Benchmark] public Image HQ3xBold() { return ScaleImage(93); }
        [Benchmark] public Image HQ3xSmart() { return ScaleImage(94); }
        [Benchmark] public Image HQ4x() { return ScaleImage(95); }
        [Benchmark] public Image HQ4xBold() { return ScaleImage(96); }
        [Benchmark] public Image HQ4xSmart() { return ScaleImage(97); }
        [Benchmark] public Image LQ2x() { return ScaleImage(98); }
        [Benchmark] public Image LQ2xBold() { return ScaleImage(99); }
        [Benchmark] public Image LQ2xSmart() { return ScaleImage(100); }
        [Benchmark] public Image LQ2x3() { return ScaleImage(101); }
        [Benchmark] public Image LQ2x3Bold() { return ScaleImage(102); }
        [Benchmark] public Image LQ2x3Smart() { return ScaleImage(103); }
        [Benchmark] public Image LQ2x4() { return ScaleImage(104); }
        [Benchmark] public Image LQ2x4Bold() { return ScaleImage(105); }
        [Benchmark] public Image LQ2x4Smart() { return ScaleImage(106); }
        [Benchmark] public Image LQ3x() { return ScaleImage(107); }
        [Benchmark] public Image LQ3xBold() { return ScaleImage(108); }
        [Benchmark] public Image LQ3xSmart() { return ScaleImage(109); }
        [Benchmark] public Image LQ4x() { return ScaleImage(110); }
        [Benchmark] public Image LQ4xBold() { return ScaleImage(111); }
        [Benchmark] public Image LQ4xSmart() { return ScaleImage(112); }
        //[Benchmark] public Image Red() { return ScaleImage(113); }
        //[Benchmark] public Image Green() { return ScaleImage(114); }
        //[Benchmark] public Image Blue() { return ScaleImage(115); }
        //[Benchmark] public Image Alpha() { return ScaleImage(116); }
        //[Benchmark] public Image Luminance() { return ScaleImage(117); }
        //[Benchmark] public Image ChrominanceU() { return ScaleImage(118); }
        //[Benchmark] public Image ChrominanceV() { return ScaleImage(119); }
        //[Benchmark] public Image u() { return ScaleImage(120); }
        //[Benchmark] public Image v() { return ScaleImage(121); }
        //[Benchmark] public Image Hue() { return ScaleImage(122); }
        //[Benchmark] public Image HueColored() { return ScaleImage(123); }
        //[Benchmark] public Image Brightness() { return ScaleImage(124); }
        //[Benchmark] public Image Min() { return ScaleImage(125); }
        //[Benchmark] public Image Max() { return ScaleImage(126); }
        //[Benchmark] public Image ExtractColors() { return ScaleImage(127); }
        //[Benchmark] public Image ExtractDeltas() { return ScaleImage(128); }


        #endregion


}
}

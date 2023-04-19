using System.Drawing.Text;
using ImageMagick;
using MathNet.Numerics.Statistics;
using IImageEncoder = SixLabors.ImageSharp.Formats.IImageEncoder;
using PngEncoder = SixLabors.ImageSharp.Formats.Png.PngEncoder;

namespace GraphicsComposerLib.Rendering.Images
{
    public static class GrImageUtils
    {
        public static Rectangle GetPngWhiteSpace(this Image<Rgba32> image, Rgba32 whiteSpaceColor)
        {
            var topRowCount = 0;
            for (var y = 0; y < image.Height; y++)
            {
                var isWhitespaceRow = true;
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[x, y] == whiteSpaceColor) 
                        continue;

                    isWhitespaceRow = false;
                    break;
                }

                if (isWhitespaceRow)
                    topRowCount++;
                else
                    break;
            }

            var bottomRowCount = 0;
            for (var y = image.Height - 1; y >= 0; y--)
            {
                var isWhitespaceRow = true;
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[x, y] == whiteSpaceColor) 
                        continue;

                    isWhitespaceRow = false;
                    break;
                }

                if (isWhitespaceRow)
                    bottomRowCount++;
                else
                    break;
            }

            var leftColumnCount = 0;
            for (var x = 0; x < image.Width; x++)
            {
                var isWhitespaceColumn = true;
                for (var y = 0; y < image.Height; y++)    
                {
                    if (image[x, y] == whiteSpaceColor) 
                        continue;

                    isWhitespaceColumn = false;
                    break;
                }

                if (isWhitespaceColumn)
                    leftColumnCount++;
                else
                    break;
            }

            var rightColumnCount = 0;
            for (var x = image.Width - 1; x >= 0; x--)
            {
                var isWhitespaceColumn = true;
                for (var y = 0; y < image.Height; y++)
                {
                    if (image[x, y] == whiteSpaceColor) 
                        continue;

                    isWhitespaceColumn = false;
                    break;
                }

                if (isWhitespaceColumn)
                    rightColumnCount++;
                else
                    break;
            }

            var cropWidth = image.Width - (rightColumnCount + leftColumnCount);
            var cropHeight = image.Height - (topRowCount + bottomRowCount);

            return new Rectangle(
                leftColumnCount,
                topRowCount,
                cropWidth,
                cropHeight
            );
        }

        public static Image<Rgba32> CropPngWhiteSpace(this Image<Rgba32> image, Rgba32 whiteSpaceColor, int marginSize)
        {
            var cropRectangle = 
                image.GetPngWhiteSpace(whiteSpaceColor);

            if (cropRectangle.Width <= 0 || cropRectangle.Height <= 0)
                return image;

            if (marginSize > 0)
                return image.Clone(context => 
                    context.Crop(cropRectangle).Resize(
                        new ResizeOptions
                        {
                            PadColor = whiteSpaceColor,
                            Compand = false,
                            Mode = ResizeMode.BoxPad,
                            Size = new Size(cropRectangle.Width + 2 * marginSize, cropRectangle.Height + 2 * marginSize),
                            TargetRectangle = new Rectangle(marginSize, marginSize, cropRectangle.Width, cropRectangle.Height)
                        }
                    )
                );

            return image.Clone(context =>
                context.Crop(cropRectangle)
            );
        }

        public static Image GetImageFromByteArray(this byte[] byteArray)
        {
            using var stream = new MemoryStream();

            stream.Write(byteArray);

            return Image.Load(stream);
        }

        ///// <summary>
        ///// Method that uses the ImageConverter object in .Net Framework to convert a byte array, 
        ///// presumably containing a JPEG or PNG file image, into a Bitmap object, which can also be 
        ///// used as an Image object.
        ///// </summary>
        ///// <param name="byteArray">byte array containing JPEG or PNG file image or similar</param>
        ///// <returns>Bitmap object if it works, else exception is thrown</returns>
        //public static System.Drawing.Bitmap GetImageFromByteArray(byte[] byteArray)
        //{
        //    var tc = TypeDescriptor.GetConverter(typeof(System.Drawing.Bitmap));

        //    //var bm = tc.ConvertFrom(byteArray);
            
        //    var bm = (System.Drawing.Bitmap) System.Drawing.ImageConverter.ConvertFrom(byteArray);

        //    if (bm != null && (
        //            bm.HorizontalResolution != (int)bm.HorizontalResolution ||
        //            bm.VerticalResolution != (int)bm.VerticalResolution
        //        ))
        //    {
        //        // Correct a strange glitch that has been observed in the test program when converting 
        //        //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
        //        //  slightly away from the nominal integer value
        //        bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
        //            (int)(bm.VerticalResolution + 0.5f));
        //    }

        //    return bm;
        //}

        public static string ImageToBase64String(this Image image, IImageEncoder format)
        {
            using var stream = new MemoryStream();

            image.Save(stream, format);

            return Convert.ToBase64String(
                stream.GetBuffer(), 
                Base64FormattingOptions.None
            );
        }
        
        public static string PngToBase64String(this Image image)
        {
            return image.ImageToBase64String(new PngEncoder());
        }

        public static string PngToBase64HtmlString(this Image image)
        {
            var imageString = image.ImageToBase64String(new PngEncoder());

            return @$"'data:image/png;base64,{imageString}'";
        }
        
        public static string FileToBase64String(this string filePath)
        {
            using var inFile = 
                new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var binaryData = 
                new byte[inFile.Length];

            long bytesRead = 
                inFile.Read(binaryData, 0, (int) inFile.Length);
            
            inFile.Close();

            return Convert.ToBase64String(binaryData, 0, binaryData.Length);
        }
        
        public static string PngFileToBase64HtmlString(this string filePath)
        {
            var base64String =
                filePath.FileToBase64String();

            return @$"'data:image/svg+xml;base64,{base64String}'";
        }

        public static string SvgFileToBase64HtmlString(this string filePath)
        {
            var base64String =
                filePath.FileToBase64String();

            return @$"'data:image/svg+xml;base64,{base64String}'";
        }

        public static Image<Rgba32> ToImageSharpPng(this IMagickImage<ushort> image)
        {
            using var stream = new MemoryStream();
            image.Write(stream, MagickFormat.Png32);
            stream.Position = 0;

            return Image.Load<Rgba32>(stream);
        }
        
        
        public static void SaveHistogramImage(this Histogram hist, string fileName, double yMax)
        {
            var imageSize = hist.BucketCount;

            var image = new Image<Rgba32>(imageSize, imageSize);

            for (var j = 0; j < imageSize; j++)
            {
                var y = imageSize * (hist[j].Count / yMax);

                for (var i = 0; i < y; i++)
                    image[j, imageSize - i - 1] = Color.DarkBlue;
            }

            image.Save(fileName);
        }

        public static List<string>? GetLocalFilesForFont(this string fontName)
        {
            var fontNameToFiles = new Dictionary<string, List<string>>();

            var fontFileList = Directory.GetFiles(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts)
            );

            foreach (var fontFile in fontFileList)
            {
                var fc = new PrivateFontCollection();

                if (File.Exists(fontFile))
                    fc.AddFontFile(fontFile);

                if (!fc.Families.Any())
                    continue;

                var name = fc.Families[0].Name;

                // If you care about bold, italic, etc, you can filter here.
                if (! fontNameToFiles.TryGetValue(name, out var files))
                {
                    files = new List<string>();
                    fontNameToFiles[name] = files;
                }

                files.Add(fontFile);
            }

            return fontNameToFiles.TryGetValue(fontName, out var result) 
                ? result 
                : null;
        }

    }
}

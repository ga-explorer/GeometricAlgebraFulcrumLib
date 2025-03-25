using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images
{
    public static class GrVisualImageUtils
    {
        public static double WidthToHeight(this Image image)
        {
            return image.Width / (double)image.Height;
        }

        public static double HeightToWidth(this Image image)
        {
            return image.Height / (double)image.Width;
        }


        public static Image Create(int width, int height, Color baseColor)
        {
            return new Image<Rgba32>(width, height, baseColor);
        }

        public static Image Create(int width, int height, Func<int, int, Color> imageFunc)
        {
            var bitmap = new Image<Rgba32>(width, height);

            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    bitmap[i, j] = imageFunc(i, j);
                }

            return bitmap;
        }

        public static Image CreateColorGrid(int squareSize, Color[,] colorArray)
        {
            var rowCount = colorArray.GetLength(0);
            var columnCount = colorArray.GetLength(1);
            var width = squareSize * columnCount;
            var height = squareSize * rowCount;
            var bitmap = new Image<Rgba32>(width, height);

            for (var i = 0; i < rowCount; i++)
            {
                var i1 = i * squareSize;

                for (var j = 0; j < columnCount; j++)
                {
                    var j1 = j * squareSize;

                    var color = colorArray[i, j];

                    for (var x = 0; x < squareSize; x++)
                        for (var y = 0; y < squareSize; y++)
                            bitmap[x + i1, y + j1] = color;
                }
            }

            return bitmap;
        }

        public static GrVisualStoredImage ToStoredImageTexture(this Image image)
        {
            return new GrVisualStoredImage(image);
        }

        public static GrVisualStoredImage GetImageAsTexture(this IGrVisualImageSource composer)
        {
            return new GrVisualStoredImage(composer.GetImage());
        }

        
        ///// <summary>
        ///// imageToProcess.Mutate(i => i.Clear(Color.Transparent);
        ///// https://github.com/SixLabors/ImageSharp.Drawing/issues/26
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="color"></param>
        //public static void Clear(this IImageProcessingContext source, Color color)
        //{
        //    var drawingOptions = new DrawingOptions
        //    {
        //        GraphicsOptions = new GraphicsOptions()
        //        {
        //            //AlphaCompositionMode = PixelAlphaCompositionMode.Clear
        //            AlphaCompositionMode = PixelAlphaCompositionMode.Src,
        //            ColorBlendingMode = PixelColorBlendingMode.Normal
        //        }
        //    };

        //    var size = source.GetCurrentSize();

        //    source.Fill(
        //        drawingOptions,
        //        color,
        //        new RectangleF(0, 0, size.Width, size.Height)
        //    );
        //}

        public static Image MutateClear(this Image baseImage, Color color)
        {
            baseImage.Mutate(x => x.Clear(color));

            return baseImage;
        }

        public static Image MutateScaleBy(this Image baseImage, double scalingFactor, IResampler resamplingMethod)
        {
            var width = (int)Math.Floor(scalingFactor * baseImage.Width);
            var height = (int)Math.Floor(scalingFactor * baseImage.Height);
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage;

            baseImage.Mutate(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );

            return baseImage;
        }

        public static Image MutateSetWidth(this Image baseImage, int width, IResampler resamplingMethod)
        {
            var height = (int)Math.Floor(width * baseImage.HeightToWidth());
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage;

            baseImage.Mutate(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );

            return baseImage;
        }

        public static Image MutateSetHeight(this Image baseImage, int height, IResampler resamplingMethod)
        {
            var width = (int)Math.Floor(height * baseImage.WidthToHeight());
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage;

            baseImage.Mutate(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );

            return baseImage;
        }

        
        public static Image MutateDrawImage(this Image baseImage, Image image, int top, int left, double opacity = 1)
        {
            baseImage.Mutate(x => 
                x.DrawImage(
                    image,
                    new Point(left, top),
                    (float)opacity
                )
            );

            return baseImage;
        }
        
        public static Image MutateSetPixelsAlphaInverseRgbMin(this Image<Rgba32> baseImage)
        {
            // https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html

            baseImage.ProcessPixelRows(
                accessor =>
                {
                    for (var y = 0; y < accessor.Height; y++)
                    {
                        var pixelRow = accessor.GetRowSpan(y);

                        // pixelRow.Length has the same value as accessor.Width,
                        // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                        for (var x = 0; x < pixelRow.Length; x++)
                        {
                            // Get a reference to the pixel at position x
                            ref var pixel = ref pixelRow[x];

                            pixel.A = (byte)(255 - pixel.RgbMinComponent());
                        }
                    }
                }
            );

            return baseImage;
        }

        public static Image<Rgba32> MutateSetPixelsAlpha(this Image<Rgba32> baseImage, Func<Rgba32, byte> colorMap)
        {
            // https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html

            baseImage.ProcessPixelRows(
                accessor =>
                {
                    for (var y = 0; y < accessor.Height; y++)
                    {
                        var pixelRow = accessor.GetRowSpan(y);

                        // pixelRow.Length has the same value as accessor.Width,
                        // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                        for (var x = 0; x < pixelRow.Length; x++)
                        {
                            // Get a reference to the pixel at position x
                            ref var pixel = ref pixelRow[x];

                            pixel.A = colorMap(pixel);
                        }
                    }
                }
            );

            return baseImage;
        }


        public static Image Clone(this Image baseImage)
        {
            return baseImage.CloneAs<Rgba32>();
        }

        public static Image CloneExpand(this Image baseImage, int deltaLeft, int deltaRight, int deltaTop, int deltaBottom, Color baseColor)
        {
            if (deltaLeft < 0 || deltaRight < 0 || deltaTop < 0 || deltaBottom < 0)
                throw new InvalidOperationException();

            var width = baseImage.Width + deltaLeft + deltaRight;
            var height = baseImage.Height + deltaTop + deltaBottom;
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage.Clone();

            var image = new Image<Rgba32>(width, height, baseColor);

            image.Mutate(x =>
                x.DrawImage(
                    baseImage,
                    new Rectangle(deltaLeft, deltaTop, baseImage.Width, image.Height),
                    1f
                )
            );


            return image;
        }

        public static Image CloneExpandFromLeft(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(delta, 0, 0, 0, baseColor);
        }

        public static Image CloneExpandFromRight(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(0, delta, 0, 0, baseColor);
        }

        public static Image CloneExpandFromTop(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(0, 0, delta, 0, baseColor);
        }

        public static Image CloneExpandFromBottom(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(0, 0, 0, delta, baseColor);
        }

        public static Image CloneExpandFromLeftRight(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(delta, delta, 0, 0, baseColor);
        }

        public static Image CloneExpandFromTopBottom(this Image baseImage, int delta, Color baseColor)
        {
            return baseImage.CloneExpand(0, 0, delta, delta, baseColor);
        }


        public static Image CloneScaleBy(this Image baseImage, double scalingFactor, IResampler resamplingMethod)
        {
            var width = (int)Math.Floor(scalingFactor * baseImage.Width);
            var height = (int)Math.Floor(scalingFactor * baseImage.Height);
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage.Clone();

            return baseImage.Clone(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );
        }

        public static Image CloneSetWidth(this Image baseImage, int width, IResampler resamplingMethod)
        {
            var height = (int)Math.Floor(width * baseImage.HeightToWidth());
            
            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage.Clone();

            return baseImage.Clone(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );
        }

        public static Image CloneSetHeight(this Image baseImage, int height, IResampler resamplingMethod)
        {
            var width = (int)Math.Floor(height * baseImage.WidthToHeight());

            if (width == baseImage.Width && height == baseImage.Height)
                return baseImage.Clone();

            return baseImage.Clone(x =>
                x.Resize(
                    width,
                    height,
                    resamplingMethod
                )
            );
        }

        
        public static Image CloneExtendFromTop(this Image baseImage, Image extImage, int space, Color baseColor)
        {
            return baseImage
                .CloneExpandFromTop(extImage.Height + space, baseColor)
                .MutateDrawImage(extImage, 0, 0);
        }
        
        public static Image CloneExtendFromBottom(this Image baseImage, Image extImage, int space, Color baseColor)
        {
            return baseImage
                .CloneExpandFromBottom(extImage.Height + space, baseColor)
                .MutateDrawImage(extImage, baseImage.Height + space, 0);
        }

        public static Image CloneExtendFromLeft(this Image baseImage, Image extImage, int space, Color baseColor)
        {
            return baseImage
                .CloneExpandFromLeft(extImage.Width + space, baseColor)
                .MutateDrawImage(extImage, 0, 0);
        }
        
        public static Image CloneExtendFromRight(this Image baseImage, Image extImage, int space, Color baseColor)
        {
            return baseImage
                .CloneExpandFromRight(extImage.Width + space, baseColor)
                .MutateDrawImage(extImage, 0, baseImage.Width + space);
        }

        
        public static Image CloneDrawImage(this Image baseImage, Image image, int top, int left, double opacity = 1)
        {
            return baseImage.Clone(x => 
                x.DrawImage(
                    image,
                    new Point(left, top),
                    (float)opacity
                )
            );
        }


    }
}

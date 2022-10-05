using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.Animations
{
    public static class Sample1
    {
        //public static void Example1()
        //{
        //    // 2ms delay (~30fps)
        //    using var gif = AnimatedGif.Create(
        //        @"D:\Projects\Study\Babylon.js\3-phase4.gif", 
        //        2
        //    );

        //    for (var i = 0; i < 500; i++)
        //    {
        //        var img1 = Image.FromFile(
        //            @$"D:\Projects\Study\Babylon.js\Frames\Frame-{i:D6}.png"
        //        );

        //        gif.AddFrame(img1, delay: -1, quality: GifQuality.Bit8);
        //    }
        //}

        private static void Cleanup(IEnumerable<string> pathList)
        {
            foreach (var path in pathList)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        /// <summary>
        /// Converts an image sequence to a video.
        /// </summary>
        /// <param name="output">Output video file.</param>
        /// <param name="frameRate">FPS</param>
        /// <param name="images">Image sequence collection</param>
        /// <returns>Output video information.</returns>
        public static bool JoinImageSequence(string output, double frameRate = 30, params ImageInfo[] images)
        {
            var tempFolderName = Path.Combine(GlobalFFOptions.Current.TemporaryFilesFolder, Guid.NewGuid().ToString());
            var temporaryImageFiles = images.Select((imageInfo, index) =>
            {
                using var image = Image.FromFile(imageInfo.FullName);
                FFMpegHelper.ConversionSizeExceptionCheck(image);
                var destinationPath = Path.Combine(tempFolderName, $"{index.ToString().PadLeft(9, '0')}{imageInfo.Extension}");
                Directory.CreateDirectory(tempFolderName);
                File.Copy(imageInfo.FullName, destinationPath);
                return destinationPath;
            }).ToArray();

            var firstImage = images.First();
            try
            {
                return FFMpegArguments
                    .FromFileInput(Path.Combine(tempFolderName, "%09d.png"), false)
                    .OutputToFile(
                        output, 
                        true, 
                        options => options
                            //.ForcePixelFormat("yuv420p")
                            .WithVideoCodec(VideoCodec.Png)
                            //.Resize(firstImage.Width, firstImage.Height)
                            .WithVideoFilters(filterOptions => 
                                filterOptions.Scale(VideoSize.FullHd)
                            )
                            .WithFastStart()
                            .WithFramerate(frameRate)
                        )
                    .ProcessSynchronously();
            }
            finally
            {
                Cleanup(temporaryImageFiles);
                Directory.Delete(tempFolderName);
            }
        }


        public static void Example2()
        {
            
            var imageFilePathList = 
                Enumerable
                    .Range(0, 500)
                    .Select(i => @$"D:\Projects\Study\Babylon.js\Frames\Frame-{i:D6}.png")
                    .ToArray();

            // Make sure image width and height are even
            //foreach (var imageFilePath in imageFilePathList)
            //{
            //    var image = new Bitmap(Image.FromFile(imageFilePath));

            //    var imageSize = image.Size;

            //    if (imageSize.Width.IsOdd() || imageSize.Height.IsOdd())
            //    {
            //        var width =
            //            imageSize.Width.IsOdd()
            //                ? imageSize.Width - 1
            //                : imageSize.Width;

            //        var height =
            //            imageSize.Height.IsOdd()
            //                ? imageSize.Height - 1
            //                : imageSize.Height;

            //        image = image.Clone(
            //            new Rectangle(0, 0, width, height),
            //            image.PixelFormat
            //        );

            //        image.Save(
            //            imageFilePath.Replace("Frames", "Frames1"),
            //            ImageFormat.Png
            //        );
            //    }
            //}

            //return;

            GlobalFFOptions.Configure(
                new FFOptions
                {
                    BinaryFolder = @"D:\Projects\Tools\ffmpeg\bin\", 
                    TemporaryFilesFolder = Path.GetTempPath()
                }
            );

            //var pixelFormatList = 
            //    FFMpeg.GetPixelFormats();

            //foreach (var pixelFormat in pixelFormatList)
            //{
            //    Console.WriteLine($"{pixelFormat.Name}: {pixelFormat.BitsPerPixel} bpp");
            //}

            //return;

            //var codecList = 
            //    FFMpeg.GetCodecs(CodecType.Video).Where(c => c.IsLossless);

            //foreach (var codec in codecList)
            //{
            //    Console.WriteLine($"{codec.Name}: {codec.Description}");
            //}

            //return;

            var images = 
                imageFilePathList
                    .Select(ImageInfo.FromPath)
                    .ToArray();

            JoinImageSequence(
                @"D:\Projects\Study\Babylon.js\joined_video.mp4", 
                frameRate: 50, 
                images
            );
        }
    }
    
}

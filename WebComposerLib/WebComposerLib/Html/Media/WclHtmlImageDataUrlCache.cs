using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;
using DataStructuresLib.Files;
using ImageMagick;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using TextComposerLib.Text.Linear;
using WebComposerLib.LaTeX.CodeComposer;
using WebComposerLib.LaTeX.ImageComposers;
using WebComposerLib.LaTeX.KaTeX;

namespace WebComposerLib.Html.Media
{
    public sealed class WclHtmlImageDataUrlCache :
        IWclHtmlImageDataUrlCache
    {
        private readonly Dictionary<string, WclHtmlImageUrl> _cacheDictionary 
            = new Dictionary<string, WclHtmlImageUrl>();


        public Color WhitespaceColor { get; set; }
            = new Rgba32(255, 255, 255, 0);

        public int MarginSize { get; set; }

        public Color BackgroundColor { get; set; } 
            = Color.FromRgba(255, 255, 255, 0);

        public FlipMode LaTeXFlipMode { get; set; } 
            = FlipMode.None;

        public WclHtmlImageUrl this[string key]
            => _cacheDictionary[key];

        public IEnumerable<string> Keys
            => _cacheDictionary.Keys;
    
        public IEnumerable<string> DataUriBase64Strings
            => _cacheDictionary.Values.Select(v => v.UrlString);
    
        public IEnumerable<string> Base64HtmlStrings
            => _cacheDictionary.Values.Select(v => v.GetUrl());


        public WclHtmlImageDataUrlCache Clear()
        {
            _cacheDictionary.Clear();

            return this;
        }

        public bool Remove(string key)
        {
            return _cacheDictionary.Remove(key);
        }

        
        public WclHtmlImageUrl AddImageDataUrl(WclHtmlImageUrl imageDataUrl)
        {
            _cacheDictionary.AddOrSet(imageDataUrl.Key, imageDataUrl);

            return imageDataUrl;
        }

        public WclHtmlImageUrl AddImageFromFile(string key, string filePath, WclHtmlImageMediaType mimeType)
        {
            var imageDataUrl = new WclHtmlImageUrl(
                key, 
                MarginSize, 
                BackgroundColor
            );

            imageDataUrl.SetDataUrlBase64FromImageFile(filePath, mimeType);

            return AddImageDataUrl(imageDataUrl);
        }
    
        public WclHtmlImageUrl AddBmpFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Bmp);
        }
    
        public WclHtmlImageUrl AddGifFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Gif);
        }
    
        public WclHtmlImageUrl AddIcoFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Ico);
        }

        public WclHtmlImageUrl AddPngFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Png);
        }
    
        public WclHtmlImageUrl AddJpegFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Jpeg);
        }
    
        public WclHtmlImageUrl AddSvgFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Svg);
        }
    
        public WclHtmlImageUrl AddSvgFromFile(string key, string filePath, int width, int height)
        {
            var imageDataUrl = new WclHtmlImageUrl(
                key, 
                MarginSize, 
                BackgroundColor
            );

            imageDataUrl.SetDataUrlFromSvgFile(filePath, width, height, false);

            return AddImageDataUrl(imageDataUrl);
        }

        public WclHtmlImageUrl AddTiffFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Tiff);
        }
    
        public WclHtmlImageUrl AddWebpFromFile(string key, string filePath)
        {
            return AddImageFromFile(key, filePath, WclHtmlImageMediaType.Webp);
        }
        
        public WclHtmlImageUrl AddImage(string key, Image image, WclHtmlImageMediaType mimeType)
        {
            var imageDataUrl = new WclHtmlImageUrl(
                key, 
                MarginSize, 
                BackgroundColor
            );

            imageDataUrl.SetDataUrlBase64FromImage(image, mimeType);

            return AddImageDataUrl(imageDataUrl);
        }
        
        public WclHtmlImageUrl AddImage(string key, IMagickImage<ushort> image, WclHtmlImageMediaType mimeType)
        {
            var imageDataUrl = new WclHtmlImageUrl(
                key, 
                MarginSize, 
                BackgroundColor
            );

            imageDataUrl.SetDataUrlBase64FromImage(image, mimeType);

            return AddImageDataUrl(imageDataUrl);
        }

        public WclHtmlImageUrl AddBmp(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Bmp);
        }

        public WclHtmlImageUrl AddBmp(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Bmp);
        }
    
        public WclHtmlImageUrl AddPng(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Png);
        }

        public WclHtmlImageUrl AddPng(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Png);
        }
    
        public WclHtmlImageUrl AddGif(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Gif);
        }

        public WclHtmlImageUrl AddGif(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Gif);
        }
    
        public WclHtmlImageUrl AddIco(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Ico);
        }
    
        public WclHtmlImageUrl AddJpeg(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Jpeg);
        }

        public WclHtmlImageUrl AddJpeg(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Jpeg);
        }

        public WclHtmlImageUrl AddSvg(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Svg);
        }
    
        public WclHtmlImageUrl AddSvgFromCode(string key, string svgCode)
        {
            var image = svgCode.SvgCodeToMagickImage();

            return AddSvg(key, image);
        }

        public WclHtmlImageUrl AddTiff(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Tiff);
        }

        public WclHtmlImageUrl AddTiff(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Tiff);
        }
    
        public WclHtmlImageUrl AddWebp(string key, Image image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Webp);
        }

        public WclHtmlImageUrl AddWebp(string key, IMagickImage<ushort> image)
        {
            return AddImage(key, image, WclHtmlImageMediaType.Webp);
        }


        public WclHtmlImageDataUrlFromLaTeX AddLaTeXCode(string key, string? latexCode)
        {
            var imageDataUrl = new WclHtmlImageDataUrlFromLaTeX(
                key, 
                latexCode, 
                MarginSize, 
                BackgroundColor
            );

            AddImageDataUrl(imageDataUrl);

            return imageDataUrl;
        }

        public WclHtmlImageDataUrlFromLaTeX AddLaTeXEquation(string key, string latexCode)
        {
            return AddLaTeXCode(
                key, 
                latexCode.GetLaTeXDisplayEquation()
            );
        }
    
        public WclHtmlImageDataUrlFromLaTeX AddLaTeXAlignedEquations(string key, string latexCode, params Pair<string>[] latexCodeArray)
        {
            return AddLaTeXCode(
                key, 
                latexCode.Trim() +
                Environment.NewLine +
                latexCodeArray.GetLaTeXAlignedEquations(false)
            );
        }

        public WclHtmlImageDataUrlFromLaTeX AddLaTeXAlignedEquations(string key, params Pair<string>[] latexCodeArray)
        {
            return AddLaTeXCode(
                key, 
                latexCodeArray.GetLaTeXAlignedEquations(false)
            );
        }
    
        public WclHtmlImageDataUrlFromLaTeX AddLaTeXAlignedEquations(string key, IEnumerable<Pair<string>> latexCodeArray)
        {
            return AddLaTeXCode(
                key, 
                latexCodeArray.GetLaTeXAlignedEquations(false)
            );
        }


        public string? GetLaTeXCode(string key)
        {
            return ((WclHtmlImageDataUrlFromLaTeX) _cacheDictionary[key]).LaTeXCode;
        }
    
        private string GetFinalLaTeXCode(IEnumerable<string> keyArray)
        {
            var composer = new LinearTextComposer();

            // https://texblog.org/2012/09/12/cropping-the-output-file-to-its-content-in-latex/
            // Method 1: using standalone document class
            composer
                .AppendLine(@"\documentclass[varwidth=true, border=1pt, multi=true, crop=true, ignorerest=true]{standalone}")
                .AppendLine()
                .AppendLine(@"\standaloneenv{flushleft}")
                .AppendLine()
                .AppendLine(@"\usepackage{amsmath}")
                .AppendLine(@"\usepackage{amsfonts}")
                .AppendLine()
                .AppendLine(@"\begin{document}")
                .AppendLine()
                .IncreaseIndentation();
        
            // Method 2: using preview package
            //composer
            //    .AppendLine(@"\documentclass[16pt]{article}")
            //    .AppendLine()
            //    .AppendLine(@"\usepackage{amsmath}")
            //    .AppendLine(@"\usepackage{amsfonts}")
            //    .AppendLine(@"\usepackage[active,tightpage]{preview}")
            //    .AppendLine(@"\setlength\PreviewBorder{10pt}")
            //    .AppendLine()
            //    .AppendLine(@"\begin{document}")
            //    .AppendLine()
            //    .IncreaseIndentation();

            foreach (var key in keyArray)
            {
                var latexCode = GetLaTeXCode(key);

                // Method 1: using standalone document class
                composer
                    .AppendLine(@"\begin{flushleft}")
                    .IncreaseIndentation()
                    .Append(latexCode)
                    .DecreaseIndentation()
                    .AppendLineAtNewLine(@"\end{flushleft}")
                    //.AppendLine(@"\newpage")
                    .AppendLine();

                // Method 2: using preview package
                //composer
                //    .AppendLine(@"\begin{preview}")
                //    .IncreaseIndentation()
                //    .AppendLine(latexCode)
                //    .DecreaseIndentation()
                //    .AppendLine(@"\end{preview}")
                //    .AppendLine(@"\newpage")
                //    .AppendLine();
            }

            composer
                .DecreaseIndentation()
                .AppendLine(@"\end{document}");

            return composer.ToString();
        }
        

        public void GeneratePngDataUrlStrings(string workingFolder)
        {
            var keyArray = 
                _cacheDictionary
                    .Where(p => p.Value is WclHtmlImageDataUrlFromLaTeX)
                    .Select(p => p.Key)
                    .ToImmutableArray();

            var katexComposer = 
                new WclKaTeXComposer(workingFolder)
                {
                    FontSizeEm = 2,
                    Output = WclKaTeXComposer.OutputKind.Html,
                    ThrowOnError = false,
                    SaveImages = false
                };

            var latexCodeArray =
                keyArray
                    .Select(key => GetLaTeXCode(key) ?? string.Empty)
                    .ToImmutableArray();
            
            katexComposer.RenderKaTeX(latexCodeArray);

            for (var i = 0; i < keyArray.Length; i++)
            {
                AddPng(
                    keyArray[i],
                    katexComposer.KaTeXPngImageList[i]
                );
            }
        }
    
        public void GenerateSvgDataUrlStrings(string workingPath)
        {
            var keyArray = 
                _cacheDictionary
                    .Where(p => p.Value is WclHtmlImageDataUrlFromLaTeX)
                    .Select(p => p.Key)
                    .ToImmutableArray();

            var katexComposer = 
                new WclKaTeXComposer(workingPath)
                {
                    FontSizeEm = 1,
                    Output = WclKaTeXComposer.OutputKind.MathMl,
                    ThrowOnError = false,
                    SaveImages = true
                };

            var latexCodeArray =
                keyArray
                    .Select(key => GetLaTeXCode(key) ?? string.Empty)
                    .ToImmutableArray();
            
            katexComposer.RenderKaTeX(latexCodeArray);

            for (var i = 0; i < keyArray.Length; i++)
            {
                var svgDataUrl = new WclHtmlImageUrl(
                    keyArray[i],
                    MarginSize,
                    BackgroundColor
                );

                var (width, height) = 
                    katexComposer.KaTeXPngImageList[i].Size;
                
                svgDataUrl.SetFileUrl(
                    WclHtmlImageMediaType.Svg,
                    width,
                    height,
                    katexComposer.KaTeXImageFileName[i] + ".svg"
                );

                //svgDataUrl.SetDataUrl(
                //    WclHtmlImageMediaType.Svg,
                //    width,
                //    height,
                //    //katexComposer.KaTeXSvgCodeList[i].OptimizeSvgCode(false)
                //    //katexComposer.KaTeXSvgCodeList[i].SvgCodeToBase64String()
                //    katexComposer.KaTeXSvgDataUriList[i],
                //    false
                //);

                AddImageDataUrl(svgDataUrl);
            }
        }


        public void GeneratePngDataUrlStrings(WclLaTeXImageComposer imageComposer)
        {
            var keyArray = 
                _cacheDictionary
                    .Where(p => p.Value is WclHtmlImageDataUrlFromLaTeX)
                    .Select(p => p.Key)
                    .ToImmutableArray();

            //pdfFilePath = @"D:\Projects\Study\Web\Babylon.js\latex.pdf";
            var pdfFilePath = "pdf".GetTempFilePath();
            var latexCode = GetFinalLaTeXCode(keyArray);
        
            //Console.WriteLine(latexCode);
            //Console.WriteLine();

            imageComposer.RenderToPdfFile(
                pdfFilePath, 
                latexCode
            );

            var settings = new MagickReadSettings
            {
                AntiAlias = true,
                TextAntiAlias = true,
                Density = new Density(
                    imageComposer.Resolution, 
                    imageComposer.Resolution
                )
            };

            using var images = new MagickImageCollection();

            // Add all the pages of the pdf file to the collection
            images.Read(pdfFilePath, settings);
        
            var pageIndex = 0;
            foreach (var image in images)
            {
                var key = keyArray[pageIndex];
                var imageData = (WclHtmlImageDataUrlFromLaTeX) _cacheDictionary[key];
                image.Format = MagickFormat.Png32;

                var flipProcessor = 
                    new FlipProcessor(LaTeXFlipMode);

                var croppedImage = 
                    image
                        .ToImageSharpPng()
                        .CropPngWhiteSpace(WhitespaceColor, imageData.MarginSize)
                        .Clone(context =>
                            context.BackgroundColor(
                                new GraphicsOptions
                                {
                                    Antialias = false,
                                    ColorBlendingMode = PixelColorBlendingMode.Normal
                                },
                                imageData.BackgroundColor
                            ).ApplyProcessor(flipProcessor)
                        );

                AddPng(key, croppedImage);

                pageIndex++;
            }
        }
    
        public void GenerateSvgDataUrlBase64Strings(WclLaTeXImageComposer imageComposer)
        {
            var keyArray = 
                _cacheDictionary
                    .Where(p => p.Value is WclHtmlImageDataUrlFromLaTeX)
                    .Select(p => p.Key)
                    .ToImmutableArray();

            //pdfFilePath = @"D:\Projects\Study\Web\Babylon.js\latex.pdf";
            var pdfFilePath = "pdf".GetTempFilePath();
            var latexCode = GetFinalLaTeXCode(keyArray);
        
            //Console.WriteLine(latexCode);
            //Console.WriteLine();

            imageComposer.RenderToPdfFile(
                pdfFilePath, 
                latexCode
            );

            var settings = new MagickReadSettings
            {
                Density = new Density(
                    imageComposer.Resolution, 
                    imageComposer.Resolution
                )
            };

            using var images = new MagickImageCollection();

            // Add all the pages of the pdf file to the collection
            images.Read(pdfFilePath, settings);
        
            var pageIndex = 0;
            foreach (var image in images)
            {
                var key = keyArray[pageIndex];
                var imageData = _cacheDictionary[key];
                image.Format = MagickFormat.Svg;
                image.BackgroundColor = MagickColors.Transparent;
            
                //var croppedImage = 
                //    image
                //        .ToImageSharpPng()
                //        .CropPngWhiteSpace(WhitespaceColor, imageData.MarginSize)
                //        .Clone(context =>
                //            context.BackgroundColor(
                //                new GraphicsOptions
                //                {
                //                    Antialias = true,
                //                    ColorBlendingMode = PixelColorBlendingMode.Normal
                //                },
                //                imageData.BackgroundColor
                //            )
                //        );

                AddSvg(key, image);

                pageIndex++;
            }
        }


        public bool ContainsKey(string key)
        {
            return _cacheDictionary.ContainsKey(key);
        }

        public string GetHtmlDataString(string key)
        {
            return _cacheDictionary[key].UrlString;
        }
    
        public string GetHtmlDataUrlString(string key)
        {
            return _cacheDictionary[key].GetUrl();
        }
    }
}
using System.Collections.Immutable;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;
using DataStructuresLib.Files;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer;
using GraphicsComposerLib.Rendering.LaTeX.ImageComposers;
using GraphicsComposerLib.Rendering.Svg;
using ImageMagick;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Images;

public sealed class GrImageBase64StringCache :
    IGrImageBase64StringCache
{
    private readonly Dictionary<string, GrImageBase64String> _cacheDictionary 
        = new Dictionary<string, GrImageBase64String>();


    public Color WhitespaceColor { get; set; }
        = new Rgba32(255, 255, 255, 0);

    public int MarginSize { get; set; }

    public Color BackgroundColor { get; set; } 
        = Color.FromRgba(255, 255, 255, 0);

    public GrImageBase64String this[string key]
        => _cacheDictionary[key];

    public IEnumerable<string> Keys
        => _cacheDictionary.Keys;
    
    public IEnumerable<string> Base64Strings
        => _cacheDictionary.Values.Select(v => v.Base64String);
    
    public IEnumerable<string> Base64HtmlStrings
        => _cacheDictionary.Values.Select(v => v.GetBase64HtmlString());


    public GrImageBase64StringCache Clear()
    {
        _cacheDictionary.Clear();

        return this;
    }

    public bool Remove(string key)
    {
        return _cacheDictionary.Remove(key);
    }


    public GrImageBase64String AddImageFromFile(string key, string filePath, GrImageHtmlMimeType mimeType)
    {
        var imageItem = new GrImageBase64String(
            key, 
            MarginSize, 
            BackgroundColor
        );

        imageItem.SetImageFromFile(filePath, mimeType);

        _cacheDictionary.AddOrSet(key, imageItem);

        return imageItem;
    }
    
    public GrImageBase64String AddBmpFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Bmp);
    }
    
    public GrImageBase64String AddGifFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Gif);
    }
    
    public GrImageBase64String AddIcoFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Ico);
    }

    public GrImageBase64String AddPngFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Png);
    }
    
    public GrImageBase64String AddJpegFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Jpeg);
    }
    
    public GrImageBase64String AddSvgFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Svg);
    }
    
    public GrImageBase64String AddSvgFromFile(string key, string filePath, int width, int height)
    {
        var imageItem = new GrImageBase64String(
            key, 
            MarginSize, 
            BackgroundColor
        );

        imageItem.SetSvgImageFromFile(filePath, width, height);

        _cacheDictionary.AddOrSet(key, imageItem);

        return imageItem;
    }

    public GrImageBase64String AddTiffFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Tiff);
    }
    
    public GrImageBase64String AddWebpFromFile(string key, string filePath)
    {
        return AddImageFromFile(key, filePath, GrImageHtmlMimeType.Webp);
    }

    public GrImageBase64String AddImage(string key, Image image, GrImageHtmlMimeType mimeType)
    {
        var imageItem = new GrImageBase64String(
            key, 
            MarginSize, 
            BackgroundColor
        );

        imageItem.SetImage(image, mimeType);

        _cacheDictionary.AddOrSet(key, imageItem);

        return imageItem;
    }
    
    public GrImageBase64String AddImage(string key, IMagickImage<ushort> image, GrImageHtmlMimeType mimeType)
    {
        var imageItem = new GrImageBase64String(
            key, 
            MarginSize, 
            BackgroundColor
        );

        imageItem.SetImage(image, mimeType);

        _cacheDictionary.AddOrSet(key, imageItem);

        return imageItem;
    }

    public GrImageBase64String AddBmp(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Bmp);
    }

    public GrImageBase64String AddBmp(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Bmp);
    }
    
    public GrImageBase64String AddPng(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Png);
    }

    public GrImageBase64String AddPng(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Png);
    }
    
    public GrImageBase64String AddGif(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Gif);
    }

    public GrImageBase64String AddGif(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Gif);
    }
    
    public GrImageBase64String AddIco(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Ico);
    }
    
    public GrImageBase64String AddJpeg(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Jpeg);
    }

    public GrImageBase64String AddJpeg(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Jpeg);
    }

    public GrImageBase64String AddSvg(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Svg);
    }
    
    public GrImageBase64String AddSvgFromCode(string key, string svgCode)
    {
        var image = svgCode.SvgCodeToMagickImage();

        return AddSvg(key, image);
    }

    public GrImageBase64String AddTiff(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Tiff);
    }

    public GrImageBase64String AddTiff(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Tiff);
    }
    
    public GrImageBase64String AddWebp(string key, Image image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Webp);
    }

    public GrImageBase64String AddWebp(string key, IMagickImage<ushort> image)
    {
        return AddImage(key, image, GrImageHtmlMimeType.Webp);
    }


    public GrImageBase64StringFromLaTeX AddLaTeXCode(string key, string? latexCode)
    {
        var latexItem = new GrImageBase64StringFromLaTeX(
            key, 
            latexCode, 
            MarginSize, 
            BackgroundColor
        );

        _cacheDictionary.AddOrSet(key, latexItem);

        return latexItem;
    }

    public GrImageBase64StringFromLaTeX AddLaTeXEquation(string key, string latexCode)
    {
        return AddLaTeXCode(
            key, 
            latexCode.GetLaTeXDisplayEquation()
        );
    }
    
    public GrImageBase64StringFromLaTeX AddLaTeXAlignedEquations(string key, string latexCode, params Pair<string>[] latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCode.Trim() +
            Environment.NewLine +
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
    }

    public GrImageBase64StringFromLaTeX AddLaTeXAlignedEquations(string key, params Pair<string>[] latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
    }
    
    public GrImageBase64StringFromLaTeX AddLaTeXAlignedEquations(string key, IEnumerable<Pair<string>> latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
    }


    public string? GetLaTeXCode(string key)
    {
        return ((GrImageBase64StringFromLaTeX) _cacheDictionary[key]).LaTeXCode;
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
    
    public void GeneratePngBase64Strings(GrLaTeXImageComposer imageComposer)
    {
        var keyArray = 
            _cacheDictionary
                .Where(p => p.Value is GrImageBase64StringFromLaTeX)
                .Select(p => p.Key)
                .ToImmutableArray();

        //pdfFilePath = @"D:\Projects\Study\Babylon.js\latex.pdf";
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
            var imageData = (GrImageBase64StringFromLaTeX) _cacheDictionary[key];
            image.Format = MagickFormat.Png32;
            
            var croppedImage = 
                image
                    .ToImageSharpPng()
                    .CropPngWhiteSpace(WhitespaceColor, imageData.MarginSize)
                    .Clone(context =>
                        context.BackgroundColor(
                            new GraphicsOptions
                            {
                                Antialias = true,
                                ColorBlendingMode = PixelColorBlendingMode.Normal
                            },
                            imageData.BackgroundColor
                        )
                    );

            AddPng(key, croppedImage);

            pageIndex++;
        }
    }
    
    public void GenerateSvgBase64Strings(GrLaTeXImageComposer imageComposer)
    {
        var keyArray = 
            _cacheDictionary
                .Where(p => p.Value is GrImageBase64StringFromLaTeX)
                .Select(p => p.Key)
                .ToImmutableArray();

        //pdfFilePath = @"D:\Projects\Study\Babylon.js\latex.pdf";
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


    public string GetBase64String(string key)
    {
        return _cacheDictionary[key].Base64String;
    }
    
    public string GetBase64HtmlString(string key)
    {
        return _cacheDictionary[key].GetBase64HtmlString();
    }
}
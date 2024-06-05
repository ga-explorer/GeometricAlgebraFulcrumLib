using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using ImageMagick;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Pdf;

public static class WclPdfUtils
{
    public static bool PdfFileToPngFile(this string pdfFilePath, string pngFilePath, double resolution = 300d)
    {
        var settings = new MagickReadSettings
        {
            // Setting the density to 300 dpi will create an image with a better quality
            Density = new Density(resolution, resolution)
        };

        using var images = new MagickImageCollection();

        // Add all the pages of the pdf file to the collection
        images.Read(pdfFilePath, settings);
        
        // Create new image that appends all the pages vertically
        using var verticalImage = images.AppendVertically();

        // Save result as a png
        verticalImage.Format = MagickFormat.Png32;
        verticalImage.Write(pngFilePath);

        return true;
    }

    public static bool PdfFileToPngFiles(this string pdfFilePath, double resolution, Func<int, string> imageFileNamesFunc)
    {
        var settings = new MagickReadSettings
        {
            Density = new Density(resolution, resolution)
        };

        using var images = new MagickImageCollection();

        // Add all the pages of the pdf file to the collection
        images.Read(pdfFilePath, settings);
                
        var pageIndex = 0;
        foreach (var image in images)
        {
            // Write page to file that contains the page number
            image.Format = MagickFormat.Png32;
            image.Write(
                $"{imageFileNamesFunc(pageIndex)}.png"
            );
            
            pageIndex++;
        }

        return true;
    }

    public static bool PdfFileToPngFiles(this string pdfFilePath, double resolution, IReadOnlyList<string> imageFileNames)
    {
        var (workingFolderPath, pdfFileName, _) = pdfFilePath.GetFilePathParts();

        var settings = new MagickReadSettings
        {
            // Setting the density to 300 dpi will create an image with a better quality
            Density = new Density(resolution, resolution)
        };

        using var images = new MagickImageCollection();

        // Add all the pages of the pdf file to the collection
        images.Read(pdfFilePath, settings);
                
        var pageIndex = 0;
        foreach (var image in images)
        {
            var pngFilePath = workingFolderPath.GetFilePath(
                imageFileNames[pageIndex], 
                "png"
            );

            // Write page to file that contains the page number
            image.Format = MagickFormat.Png32;
            image.Write(pngFilePath);
            
            pageIndex++;
        }

        return true;
    }

    public static IEnumerable<Image> PdfFileToImages(this string pdfFilePath, double resolution)
    {
        var settings = new MagickReadSettings
        {
            // Setting the density to 300 dpi will create an image with a better quality
            Density = new Density(resolution, resolution)
        };

        using var images = new MagickImageCollection();

        // Add all the pages of the pdf file to the collection
        images.Read(pdfFilePath, settings);
            
        foreach (var image in images)
        {
            //image.Resize(new Percentage(50));

            using var memStream = new MemoryStream();
            image.Format = MagickFormat.Png32;
            image.Write(memStream);
            memStream.Position = 0;
                
            yield return Image.Load(memStream);
        }
    }

    public static IEnumerable<byte[]> PdfFileToPngByteArrays(this string pdfFilePath, double resolution)
    {
        var settings = new MagickReadSettings
        {
            // Setting the density to 300 dpi will create an image with a better quality
            Density = new Density(resolution, resolution)
        };

        using var images = new MagickImageCollection();

        // Add all the pages of the pdf file to the collection
        images.Read(pdfFilePath, settings);
            
        foreach (var image in images)
        {
            //image.Resize(new Percentage(50));

            using var memStream = new MemoryStream();
            image.Format = MagickFormat.Png32;
            image.Write(memStream);

            yield return memStream.GetByteArray(true);
        }
    }
}
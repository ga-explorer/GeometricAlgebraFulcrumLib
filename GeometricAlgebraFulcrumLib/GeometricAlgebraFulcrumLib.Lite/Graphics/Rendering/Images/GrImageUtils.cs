﻿using MathNet.Numerics.Statistics;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Images;

public static class GrImageUtils
{
        
        
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

}
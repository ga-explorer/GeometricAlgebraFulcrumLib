namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.Animations;

public static class Sample1
{
    //public static void Example1()
    //{
    //    // 2ms delay (~30fps)
    //    using var gif = AnimatedGif.Create(
    //        @"D:\Projects\Study\Web\Babylon.js\3-phase4.gif", 
    //        2
    //    );

    //    for (var i = 0; i < 500; i++)
    //    {
    //        var img1 = Image.FromFile(
    //            @$"D:\Projects\Study\Web\Babylon.js\Frames\Frame-{i:D6}.png"
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


}
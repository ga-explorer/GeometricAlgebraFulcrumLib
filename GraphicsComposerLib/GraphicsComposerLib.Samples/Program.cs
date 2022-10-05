using System;
using GraphicsComposerLib.Samples.SkiaSharp;
using GraphicsComposerLib.Samples.Textures;

namespace GraphicsComposerLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            LaTeXSamples.Example1();

            GrVisualImage3DSamples.Example1();
            GrVisualImage3DSamples.Example2();
            GrVisualImage3DSamples.Example3();
            GrVisualImage3DSamples.Example4();

            //Sample3.Execute();
            //Sample6.Generate(@"D:\Projects\Study\Xeogl\MyWork\Sample6.html");

            Console.WriteLine();
            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}

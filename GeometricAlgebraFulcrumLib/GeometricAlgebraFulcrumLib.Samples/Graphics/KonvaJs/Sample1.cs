using System;
using System.Collections.Immutable;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Containers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Samples.Graphics.KonvaJs;

public static class Sample1
{
    public static void Example1()
    {
        const double stageWidth = 1024;
        const double stageHeight = 768;

        var stage = new GrKonvaJsStage(
            "stage",
            "'renderDiv'",
            stageWidth,
            stageHeight
        );

        //stage.UpdateOptions(
        //    new GrKonvaJsStage.StageOptions
        //    {
        //        X = stageWidth / 2, 
        //        Y = stageHeight / 2,
        //        ScaleY = -1
        //    }
        //);

        stage
            .LayerComposers[0]
            .SetDashedColorStyle(1, new float[]{2, 5}, Color.Blue)
            .AddLine(
                "line01",
                Float64Vector2D.Create(0, 0),
                Float64Vector2D.Create(100, 150)
            )
            .SwitchLayer(1)
            .SetColorStyle(2, Color.Black, Color.Beige)
            .AddPoint("point0", 0, 0, 5)
            .AddPoint("point1", 100, 150, 5);
            
        Console.WriteLine(stage.GetCode());

        stage.SaveHtmlCode(@"D:\Projects\Study\Web\Konva\Example1.html");
    }

    public static void Example2()
    {
        var random = new Random(10);

        var stage = new GrKonvaJsStage(
            "stage", 
            "'renderDiv'",
            1024,
            768
        );

        var pointArray = 
            7.GetRange(i => random.GetVector2D() * 200).ToImmutableArray();

        stage
            .LayerComposers[0]
            .SetDashedColorStyle(
                1, 
                new float[]{2, 5}, 
                Color.Blue
            )
            .AddLine(
                "line",
                pointArray,
                true,
                1
            )
            .SwitchLayer(1)
            .SetColorStyle(2, Color.Black, Color.Beige)
            .AddPoints("linePoint", pointArray, 5);
            
        Console.WriteLine(stage.GetCode());

        stage.SaveHtmlCode(@"D:\Projects\Study\Web\Konva\Example2.html");
    }
}
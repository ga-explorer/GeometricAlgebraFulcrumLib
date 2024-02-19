﻿using System;
using GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.BSplineCurveBasis;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

public static class NumericPhSplineCurveSamples
{
    public static void Example1()
    {
        const int nMax = 2;
        var knotVector = BSplineKnotVector.CreateSimpleClamped(0, 1, 7, nMax);
        //var knotVector = BSplineKnotVector.CreateUniform(0, 1, 8);
            
        Console.WriteLine("Knot values: " + knotVector.Concatenate(", "));
        Console.WriteLine();

        for (var n = 0; n <= nMax; n++)
        {
            var basisSet = knotVector.CreateBSplineBasisSet(n);

            basisSet.PlotBasisSet(@$"D:\Downloads\Splines\B_{n}.pdf", 1024, 768);
            basisSet.PlotBasisSetPairProducts(@$"D:\Downloads\Splines\BB1_{n}.pdf", 1024, 768);

            var basisPairProductSet = basisSet.CreatePairProductSet();
            //var q = basisPairProductSet.BasisSet2.ControlPointsCount;

            basisPairProductSet.PlotBasisSet(@$"D:\Downloads\Splines\BB2_{n}.pdf", 1024, 768);
            basisPairProductSet.BasisSet2.PlotBasisSet(@$"D:\Downloads\Splines\B2_{n}.pdf", 1024, 768);
        }

        //var pm = new PlotModel { Title = $"B-Spline Basis of Degree {n}" };
        //pm.Background = OxyColor.FromRgb(255,255,255);


        //for (var index = 0; index < p; index++)
        //    pm.Series.Add(new FunctionSeries(
        //        t => basisSet.GetValue(index, t), 
        //        0, 1, 250
        //    ));

        //PdfExporter.Export(pm, @"D:\Downloads\Splines\B-Spline_Basis.pdf", 1024, 768);
        //PngExporter.Export(pm, @"D:\Downloads\Splines\B-Spline_Basis.png", 1024, 768, 300);
    }
}
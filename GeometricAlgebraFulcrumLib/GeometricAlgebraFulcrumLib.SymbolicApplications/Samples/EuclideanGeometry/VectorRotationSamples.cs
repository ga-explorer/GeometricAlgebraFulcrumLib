﻿using System.Diagnostics;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.EuclideanGeometry;

public static class VectorRotationSamples
{
    public static XGaVector<T> RotateUsingUnitVectors<T>(this XGaVector<T> x, XGaVector<T> u, XGaVector<T> v)
    {
        var uvDot = u.Sp(v);
        var xuDot = x.Sp(u);
        var xvDot = x.Sp(v);
        var xNorm = x.Norm();

        //var d = xNorm * (1 + uvDot);
        //var a = (uvDot + xNorm) / d;
        //var b = (xvDot + xNorm * xuDot) / d;
        //var c = (xNorm * xvDot + (1 - 2 * d) * xuDot) / d;

        //var d = 1 + uvDot;
        //var a = (1 + uvDot / xNorm) / d;
        //var b = (xuDot + xvDot / xNorm) / d;
        //var c = (xvDot - (2 * d - 1 / xNorm) * xuDot) / d;

        //var xUnit = x / xNorm;
        var a =  x.Sp(u + v) / (1 + u.Sp(v));
        var b = a - 2 * x.Sp(u);

        return (x - a * u - b * v);
    }

    public static void NumericExample1()
    {
        const int n = 5;
            
        var scalarProcessor = ScalarProcessorOfWolframExpr.DefaultProcessor;
        var metric = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerFloat64.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerExpr.DefaultComposer;

        laTeXComposer.BasisName = @"\boldsymbol{e}";
            
        var random = 
            metric.CreateXGaRandomComposer(n, 10);

        var u = random.GetSparseVector(n);
        var v = random.GetSparseVector(n);

        var uvRotor = u.GetEuclideanRotorTo(v);

        for (var i = 0; i < 100; i++)
        {
            var x = random.GetVector();

            var y1 = uvRotor.OmMap(x);
            var y2 = x.RotateUsingUnitVectors(u, v);
            var yDiff = y1 - y2;

            if (yDiff.IsNearZero()) continue;

            Console.WriteLine($"${laTeXComposer.GetMultivectorText(yDiff)}$");
            Console.WriteLine();
        }
    }

    public static void SymbolicExample1()
    {
        const int n = 5;
            
        var scalarProcessor = ScalarProcessorOfWolframExpr.DefaultProcessor;
        var metric = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerExpr.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerExpr.DefaultComposer;

        laTeXComposer.BasisName = @"\boldsymbol{e}";
            
        var assumeExpr1 =
            n.GetRange(1)
                .Select(i => $"Subscript[x, {i}] | Subscript[u, {i}] | Subscript[v, {i}]")
                .Concatenate(" | ");

        var assumeExpr2 =
            n.GetRange(1)
                .Select(i => $"Subscript[u, {i}]^2")
                .Concatenate(" + ");

        var assumeExpr3 =
            n.GetRange(1)
                .Select(i => $"Subscript[v, {i}]^2")
                .Concatenate(" + ");

        var assumeExpr =
            $@"And[Element[{assumeExpr1}, Reals], {assumeExpr2} == 1, {assumeExpr3} == 1]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var x = metric.CreateVector(
            n,
            i => $"Subscript[x, {i + 1}]".ToExpr()
        );

        //var x = 
        //    metric.CreateVector(1);

        var u = metric.CreateVector(
            n,
            i => $"Subscript[u, {i + 1}]".ToExpr()
        ).DivideByENorm();

        var v = metric.CreateVector(
            n,
            i => $"Subscript[v, {i + 1}]".ToExpr()
        ).DivideByENorm();

        var uvDot = u.Sp(v);
        var xuDot = x.Sp(u);
        var xvDot = x.Sp(v);
        var xNorm = x.Norm();

        //var w = (u + v).DivideByNorm()
        var w = 
            (u + v) / (2 + 2 * uvDot).Sqrt();

        //var uvRotor = 
        //    u.GetEuclideanRotorTo(v, true);

        //var y = uvRotor.OmMap(x);

        var x1 =
            u.ToPureReflector().OmMap(x);

        //var x2 = 
        //    2 * xuDot * u - x;

        //Debug.Assert(
        //    (x2 - x1).FullSimplifyScalars().IsZero()
        //);

        //var uvRotor = u.GetEuclideanRotorTo(v, true);
        //var y1 = uvRotor.OmMap(x);

        var y1 =
            w.ToPureReflector().OmMap(x1);

        //var uxv = u.Gp(x).Gp(v);
        //var vxu = v.Gp(x).Gp(u);

        //var ux1u = x; //u.Gp(x1).Gp(u);
        //var ux1v = 2 * xuDot * v - uxv; //u.Gp(x1).Gp(v);
        //var vx1u = 2 * xuDot * v - vxu; //v.Gp(x1).Gp(u);
        //var vx1v = x - 2 * xuDot * u - 2 * (xvDot - 2 * xuDot * uvDot) * v;//v.Gp(x1).Gp(v);

        //var y2 = 
        //    (ux1u + ux1v + vx1u + vx1v) / (2 + 2 * uvDot);

        //var d = xNorm * (1 + uvDot);
        //var a = (uvDot + xNorm) / d;
        //var b = (xvDot + xNorm * xuDot) / d;
        //var c = (xNorm * xvDot + (1 - 2 * d) * xuDot) / d;

        //var y2 = a * x - b * u - c * v;

        var y2 = 
            x.RotateUsingUnitVectors(u, v);

        var yDiff = 
            (y2 - y1).SimplifyScalars();

        //var vxu1 = 
        //    ((v.Gp(x).Gp(u) + u.Gp(x).Gp(v)) / 2).SimplifyScalars();

        //var vxu2 = 
        //    (x.Sp(v) * u + x.Sp(u) * v - u.Sp(v) * x) / x.Norm();

        //var vxuDiff = 
        //    (vxu1 - vxu2).FullSimplifyScalars();
        
        Console.WriteLine($"${laTeXComposer.GetMultivectorText(yDiff)}$");
        Console.WriteLine();
    }

    public static void Example2()
    {
        const int n = 5;
            
        var scalarProcessor = ScalarProcessorOfWolframExpr.DefaultProcessor;
        var geometricProcessor = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerExpr.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerExpr.DefaultComposer;

        laTeXComposer.BasisName = @"\boldsymbol{e}";
            
        var assumeExpr1 =
            n.GetRange(1)
                .Select(i => $"Subscript[x, {i}] | Subscript[u, {i}] | Subscript[v, {i}]")
                .Concatenate(" | ");
        
        var assumeExpr2 =
            n.GetRange(1)
                .Select(i => $"Subscript[x, {i}]^2")
                .Concatenate(" + ");

        var assumeExpr3 =
            n.GetRange(1)
                .Select(i => $"Subscript[u, {i}]^2")
                .Concatenate(" + ");

        var assumeExpr4 =
            n.GetRange(1)
                .Select(i => $"Subscript[v, {i}]^2")
                .Concatenate(" + ");

        var assumeExpr =
            $@"And[Element[{assumeExpr1}, Reals], {assumeExpr2} == 1, {assumeExpr3} == 1, {assumeExpr4} == 1]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var x = geometricProcessor.CreateVector(
            n,
            i => $"Subscript[x, {i + 1}]".ToExpr()
        );

        var xNorm =
            x.Norm();

        for (var m = 0; m < n; m++)
        {
            var u = 
                geometricProcessor.CreateTermVector(m);

            var v = geometricProcessor.CreateVector(
                n,
                idx => $"Subscript[v, {idx + 1}]".ToExpr()
            ).DivideByNorm();

            var y1 = 
                x.RotateUsingUnitVectors(u, v);

            var uvDot = $"Subscript[v, {m + 1}]".CreateScalar(scalarProcessor);
            var xuDot = $"Subscript[x, {m + 1}]".CreateScalar(scalarProcessor);
            var xvDot = x.Sp(v).Scalar();

            Debug.Assert(
                (uvDot - u.Sp(v).Scalar()).FullSimplifyScalar().IsZero()
            );
            
            Debug.Assert(
                (xuDot - x.Sp(u)).FullSimplifyScalars().IsZero
            );
            
            Debug.Assert(
                (xvDot - x.Sp(v)).FullSimplifyScalars().IsZero
            );

            var d = 1 + uvDot;
            var a = (1 + uvDot / xNorm) / d;
            var b = (xuDot + xvDot / xNorm) / d;
            var c = (xvDot - (2 * d - 1 / xNorm) * xuDot) / d;

            var y2 = 
                a * x - b * u - c * v;
            
            var yDiff = 
                (y2 - y1).FullSimplifyScalars();

            Console.WriteLine($@"$m = {m}: \boldsymbol{{y}}_{{diff}} = {laTeXComposer.GetMultivectorText(yDiff)}$");
            Console.WriteLine();
        }
    }
}
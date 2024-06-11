using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Algebra.GeometricAlgebra;

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
        var a = x.Sp(u + v) / (1 + u.Sp(v));
        var b = a - 2 * x.Sp(u);

        return x - a * u - b * v;
    }

    public static void NumericExample1()
    {
        const int n = 5;

        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;
        var metric = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerFloat64.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        laTeXComposer.BasisName = @"\boldsymbol{e}";

        var random =
            metric.CreateXGaRandomComposer(n, 10);

        var u = random.GetXGaSparseVector(n);
        var v = random.GetXGaSparseVector(n);

        var uvRotor = u.GetEuclideanRotorTo(v);

        for (var i = 0; i < 100; i++)
        {
            var x = random.GetXGaVector();

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

        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;
        var metric = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerExpr.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

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

        var x = metric.Vector(
            n,
            i => $"Subscript[x, {i + 1}]".ToExpr()
        );

        //var x = 
        //    metric.Vector(1);

        var u = metric.Vector(
            n,
            i => $"Subscript[u, {i + 1}]".ToExpr()
        ).DivideByENorm();

        var v = metric.Vector(
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

        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;
        var geometricProcessor = XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var textComposer =
            TextComposerExpr.DefaultComposer;

        var laTeXComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

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

        var x = geometricProcessor.Vector(
            n,
            i => $"Subscript[x, {i + 1}]".ToExpr()
        );

        var xNorm =
            x.Norm();

        for (var m = 0; m < n; m++)
        {
            var u =
                geometricProcessor.VectorTerm(m);

            var v = geometricProcessor.Vector(
                n,
                idx => $"Subscript[v, {idx + 1}]".ToExpr()
            ).DivideByNorm();

            var y1 =
                x.RotateUsingUnitVectors(u, v);

            var uvDot = $"Subscript[v, {m + 1}]".ScalarFromText(scalarProcessor);
            var xuDot = $"Subscript[x, {m + 1}]".ScalarFromText(scalarProcessor);
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
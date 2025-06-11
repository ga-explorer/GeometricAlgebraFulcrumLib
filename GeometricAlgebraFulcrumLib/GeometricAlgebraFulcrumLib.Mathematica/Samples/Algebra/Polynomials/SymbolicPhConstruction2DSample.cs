using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.PhCurves;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Samples.Algebra.Polynomials;

public static class SymbolicPhConstruction2DSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static XGaProcessor<Expr> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanXGaProcessor();

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;


    public static void Example1()
    {
        const int degree = 2;
        var parameterValue = "t".ToSymbolExpr();
        var processor = GeometricProcessor;

        var sqrt2 = "Sqrt[2]".ToExpr().ScalarFromValue(ScalarProcessor);

        //var e1 = GeometricProcessor.Vector(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e12 = GeometricProcessor.BivectorTerm((IndexSet)3);

        var p =
            GeometricProcessor.Vector(1, 1);
        //GeometricProcessor.Vector("Subscript[p, 1]", "Subscript[p, 2]");

        var d =
            GeometricProcessor.Vector(1, 1).DivideByENorm();
        //GeometricProcessor.Vector("Subscript[d, 1]", "Subscript[d, 2]");

        var basisSet = BernsteinBasisSet<Expr>.Create(ScalarProcessor, 2);
        var basisPairProductSet = BernsteinBasisPairProductSet<Expr>.Create(basisSet);
        var basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<Expr>.Create(basisPairProductSet);

        var e1 = processor.VectorTerm(0);

        var scaledRotor0 = processor.CreateScaledIdentityRotor();

        var f011 = basisPairProductIntegralSet.GetValueAt1(0, 1);
        var f111 = basisPairProductIntegralSet.GetValueAt1(1, 1);
        var f121 = basisPairProductIntegralSet.GetValueAt1(1, 2);

        var dNorm = d.ENorm();
        var dUnit = d / dNorm;

        var scaledRotor2 = e1.CreateScaledParametricPureRotor3D(
            dUnit,
            ScalarProcessor.Angle0Radians().RadiansToPolarAngle(),
            dNorm.ScalarValue
        );

        var vector00 = e1;
        var vector22 = d;
        var vector02 = (e1.Gp(scaledRotor2.MultivectorReverse) + scaledRotor2.Multivector.Gp(e1)).GetVectorPart().FullSimplifyScalars();

        var u = p - (e1 + d) / 8 + vector02 / 24;
        var uNorm = u.ENorm();
        var uUnit = u / uNorm;

        var v1 = f111.Sqrt();
        var v0 = f011 / v1;
        var v2 = f121 / v1;

        var v = e1.CreateScaledParametricPureRotor3D(
            uUnit,
            ScalarProcessor.Zero.RadiansToPolarAngle(),
            uNorm.ScalarValue
        ).Multivector;

        var a1 = (v - v0 - v2 * scaledRotor2.Multivector) / v1;

        var scaledRotor1 = processor.CreatePureScalingRotor3D(
            a1.Scalar(),
            a1[0, 1],
            a1[0, 2],
            a1[2, 3]
        );

        var vector01 = (e1.Gp(scaledRotor1.MultivectorReverse) + scaledRotor1.Multivector.Gp(e1)).GetVectorPart().FullSimplifyScalars();
        var vector12 = (scaledRotor1.Multivector.Gp(e1).Gp(scaledRotor2.MultivectorReverse) + scaledRotor2.Multivector.Gp(e1).Gp(scaledRotor1.MultivectorReverse)).GetVectorPart().FullSimplifyScalars();
        var vector11 = scaledRotor1.Multivector.Gp(e1).Gp(scaledRotor1.MultivectorReverse).GetVectorPart().FullSimplifyScalars();

        var scalar00 = scaledRotor0.Multivector.ESp(scaledRotor0.MultivectorReverse).FullSimplifyScalar();
        var scalar11 = scaledRotor1.Multivector.ESp(scaledRotor1.MultivectorReverse).FullSimplifyScalar();
        var scalar22 = scaledRotor2.Multivector.ESp(scaledRotor2.MultivectorReverse).FullSimplifyScalar();

        var scalar01 = (scaledRotor0.Multivector.ESp(scaledRotor1.MultivectorReverse) +
                        scaledRotor1.Multivector.ESp(scaledRotor0.MultivectorReverse)).FullSimplifyScalar();

        var scalar02 = (scaledRotor0.Multivector.ESp(scaledRotor2.MultivectorReverse) +
                        scaledRotor2.Multivector.ESp(scaledRotor0.MultivectorReverse)).FullSimplifyScalar();

        var scalar12 = (scaledRotor1.Multivector.ESp(scaledRotor2.MultivectorReverse) +
                        scaledRotor2.Multivector.ESp(scaledRotor1.MultivectorReverse)).FullSimplifyScalar();

        Console.WriteLine($@"Vector00 = ${LaTeXComposer.GetMultivectorText(vector00)}$");
        Console.WriteLine($@"Vector01 = ${LaTeXComposer.GetMultivectorText(vector01)}$");
        Console.WriteLine($@"Vector02 = ${LaTeXComposer.GetMultivectorText(vector02)}$");
        Console.WriteLine($@"Vector11 = ${LaTeXComposer.GetMultivectorText(vector11)}$");
        Console.WriteLine($@"Vector12 = ${LaTeXComposer.GetMultivectorText(vector12)}$");
        Console.WriteLine($@"Vector22 = ${LaTeXComposer.GetMultivectorText(vector22)}$");

        Console.WriteLine($@"Scalar00 = ${LaTeXComposer.GetScalarText(scalar00)}$");
        Console.WriteLine($@"Scalar01 = ${LaTeXComposer.GetScalarText(scalar01)}$");
        Console.WriteLine($@"Scalar02 = ${LaTeXComposer.GetScalarText(scalar02)}$");
        Console.WriteLine($@"Scalar11 = ${LaTeXComposer.GetScalarText(scalar11)}$");
        Console.WriteLine($@"Scalar12 = ${LaTeXComposer.GetScalarText(scalar12)}$");
        Console.WriteLine($@"Scalar22 = ${LaTeXComposer.GetScalarText(scalar22)}$");

        parameterValue = ScalarProcessor.ZeroValue;
        var f00 = basisPairProductSet.GetValue(0, 0, parameterValue);
        var f01 = basisPairProductSet.GetValue(0, 1, parameterValue);
        var f02 = basisPairProductSet.GetValue(0, 2, parameterValue);
        var f11 = basisPairProductSet.GetValue(1, 1, parameterValue);
        var f12 = basisPairProductSet.GetValue(1, 2, parameterValue);
        var f22 = basisPairProductSet.GetValue(2, 2, parameterValue);

        var cd =
            f00 * vector00 + f01 * vector01 + f02 * vector02 +
            f11 * vector11 + f12 * vector12 + f22 * vector22;


        f00 = basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
        f01 = basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
        f02 = basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
        f11 = basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
        f12 = basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
        f22 = basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

        var c =
            f00 * vector00 + f01 * vector01 + f02 * vector02 +
            f11 * vector11 + f12 * vector12 + f22 * vector22;

        Console.WriteLine($@"$\boldsymbol{{c}}\left({LaTeXComposer.GetScalarText(parameterValue)}\right) = {LaTeXComposer.GetMultivectorText(c)}$");
        Console.WriteLine($@"$\boldsymbol{{c}}^{{\prime}}\left({LaTeXComposer.GetScalarText(parameterValue)}\right) = {LaTeXComposer.GetMultivectorText(cd)}$");
    }

    public static void Example2()
    {
        var parameterValue = "t".ToSymbolExpr();
        var processor = GeometricProcessor;

        var sqrt2 = "Sqrt[2]".ToExpr().ScalarFromValue(ScalarProcessor);

        //var e1 = GeometricProcessor.Vector(0);
        var e2 = processor.VectorTerm(1);
        var e12 = processor.BivectorTerm((IndexSet)3);

        var p =
            GeometricProcessor.Vector(1d, 1d);
        //GeometricProcessor.Vector("Subscript[p, 1]", "Subscript[p, 2]");

        var d =
            GeometricProcessor.Vector(1.5d, 2d);
        //GeometricProcessor.Vector("Subscript[d, 1]", "Subscript[d, 2]");

        var phCurve = PhCurve2DDegree5Canonical<Expr>.Create(processor, p, d);

        var c0 = phCurve.GetCurvePoint(ScalarProcessor.ZeroValue);
        var c1 = phCurve.GetCurvePoint(ScalarProcessor.OneValue);

        var cd0 = phCurve.GetHodographPoint(ScalarProcessor.ZeroValue);
        var cd1 = phCurve.GetHodographPoint(ScalarProcessor.OneValue);

        Console.WriteLine($@"c0 = ${LaTeXComposer.GetMultivectorText(c0)}$");
        Console.WriteLine($@"c1 = ${LaTeXComposer.GetMultivectorText(c1)}$");
        Console.WriteLine($@"cd0 = ${LaTeXComposer.GetMultivectorText(cd0)}$");
        Console.WriteLine($@"cd1 = ${LaTeXComposer.GetMultivectorText(cd1)}$");
        Console.WriteLine();

        Console.WriteLine($@"ScalingRotorV = ${LaTeXComposer.GetMultivectorText(phCurve.ScalingRotorV)}$");
        Console.WriteLine($@"ScalingRotor0 = ${LaTeXComposer.GetMultivectorText(phCurve.ScalingRotor0)}$");
        Console.WriteLine($@"ScalingRotor1 = ${LaTeXComposer.GetMultivectorText(phCurve.ScalingRotor1)}$");
        Console.WriteLine($@"ScalingRotor2 = ${LaTeXComposer.GetMultivectorText(phCurve.ScalingRotor2)}$");
        Console.WriteLine();

        Console.WriteLine($@"VectorU = ${LaTeXComposer.GetMultivectorText(phCurve.VectorU)}$");
        Console.WriteLine($@"Vector00 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector00)}$");
        Console.WriteLine($@"Vector01 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector01)}$");
        Console.WriteLine($@"Vector02 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector02)}$");
        Console.WriteLine($@"Vector11 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector11)}$");
        Console.WriteLine($@"Vector12 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector12)}$");
        Console.WriteLine($@"Vector22 = ${LaTeXComposer.GetMultivectorText(phCurve.Vector22)}$");
        Console.WriteLine();

        Console.WriteLine($@"Scalar00 = ${LaTeXComposer.GetScalarText(phCurve.Scalar00)}$");
        Console.WriteLine($@"Scalar01 = ${LaTeXComposer.GetScalarText(phCurve.Scalar01)}$");
        Console.WriteLine($@"Scalar02 = ${LaTeXComposer.GetScalarText(phCurve.Scalar02)}$");
        Console.WriteLine($@"Scalar11 = ${LaTeXComposer.GetScalarText(phCurve.Scalar11)}$");
        Console.WriteLine($@"Scalar12 = ${LaTeXComposer.GetScalarText(phCurve.Scalar12)}$");
        Console.WriteLine($@"Scalar22 = ${LaTeXComposer.GetScalarText(phCurve.Scalar22)}$");
        Console.WriteLine();
    }

    public static void Example3()
    {
        const int degree = 2;
        var t = "t".ToSymbolExpr();

        var basisSet = BernsteinBasisSet<Expr>.Create(ScalarProcessor, degree);
        var basisPairProductSet = basisSet.CreatePairProductSet();
        var basisPairProductIntegralSet = basisPairProductSet.CreateIntegralSet();

        var sqrt2 = "Sqrt[2]".ToExpr().ScalarFromValue(ScalarProcessor);

        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e12 = GeometricProcessor.BivectorTerm((IndexSet)3);

        var p =
            GeometricProcessor.Vector(1, 1);
        //GeometricProcessor.Vector("Subscript[p, 1]", "Subscript[p, 2]");

        var d =
            GeometricProcessor.Vector(1, 1).DivideByENorm();
        //GeometricProcessor.Vector("Subscript[d, 1]", "Subscript[d, 2]");

        var d1 = d.Scalar(0);
        var d2 = d.Scalar(1);
        var dNorm1 = d.Norm() + d1;
        var dNorm1Sqrt = dNorm1.Sqrt();

        var u =
            p - (e1 + d) / 8 + (dNorm1Sqrt * e1 + d2 / dNorm1Sqrt * e2) / 12 / sqrt2;

        var u1 = u.Scalar(0);
        var u2 = u.Scalar(1);
        var uNorm1 = u.Norm() + u1;
        var uNorm1Sqrt = uNorm1.Sqrt();

        var v = (uNorm1Sqrt - u2 / uNorm1Sqrt * e12) / sqrt2;
        //var v0 = GeometricProcessor.RationalSqrt(3, 40);
        var v1 = ScalarProcessor.RationalSqrt(2, 10);
        //var v2 = GeometricProcessor.RationalSqrt(3, 40);

        var value3by4 = ScalarProcessor.Rational(3, 4);
        var a2 = (dNorm1Sqrt - d2 / dNorm1Sqrt * e12) / sqrt2;
        var a1 = v / v1 - value3by4 - value3by4 * a2;

        var vector00 = e1;
        var vector01 = 2 * a1.Gp(e1);
        var vector02 = sqrt2 * (dNorm1Sqrt * e1 + d2 / dNorm1Sqrt * e2);
        var vector11 = a1.Gp(a2).Gp(e1);
        var vector12 = 2 * a1.Gp(a2).Gp(e1);
        var vector22 = d;

        var fd00 = basisPairProductSet.GetValue(0, 0, t);
        var fd01 = basisPairProductSet.GetValue(0, 1, t);
        var fd02 = basisPairProductSet.GetValue(0, 2, t);
        var fd11 = basisPairProductSet.GetValue(1, 1, t);
        var fd12 = basisPairProductSet.GetValue(1, 2, t);
        var fd22 = basisPairProductSet.GetValue(2, 2, t);

        var cd =
            (fd00 * vector00 + fd01 * vector01 + fd02 * vector02 +
             fd11 * vector11 + fd12 * vector12 + fd22 * vector22).FullSimplifyScalars();


        var f00 = basisPairProductIntegralSet.GetValue(0, 0, t);
        var f01 = basisPairProductIntegralSet.GetValue(0, 1, t);
        var f02 = basisPairProductIntegralSet.GetValue(0, 2, t);
        var f11 = basisPairProductIntegralSet.GetValue(1, 1, t);
        var f12 = basisPairProductIntegralSet.GetValue(1, 2, t);
        var f22 = basisPairProductIntegralSet.GetValue(2, 2, t);

        var c =
            (f00 * vector00 + f01 * vector01 + f02 * vector02 +
             f11 * vector11 + f12 * vector12 + f22 * vector22).FullSimplifyScalars();


        Console.WriteLine($@"$\boldsymbol{{c}}\left(t\right) = {LaTeXComposer.GetMultivectorText(c)}$");
        Console.WriteLine($@"$\boldsymbol{{c}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(cd)}$");
    }
}
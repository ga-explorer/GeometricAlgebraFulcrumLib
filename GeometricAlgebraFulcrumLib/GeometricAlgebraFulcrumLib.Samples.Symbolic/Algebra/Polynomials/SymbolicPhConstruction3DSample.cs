using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.PhCurves;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Algebra.Polynomials;

public static class SymbolicPhConstruction3DSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaProcessor<Expr> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanRGaProcessor();

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;


    ///// <summary>
    ///// Define a canonical 3D quintic PH curve using 2nd degree Bernstein Basis polynomials
    ///// </summary>
    //public static void Example1()
    //{
    //    const int degree = 2;

    //    var t = "t".ToSymbolExpr();

    //    var basisSet = BernsteinBasisSet<Expr>.Create(ScalarProcessor, degree);
    //    var basisSet2 = basisSet.CreatePairProductSet();
    //    var basisSet2Integral = basisSet2.CreateIntegralSet();

    //    var e1 = 
    //        GeometricProcessor.Vector(0);

    //    var p1 =
    //        //GeometricProcessor.Vector(1, 1, 1);
    //        GeometricProcessor.Vector(
    //            "Subscript[p, 11]",
    //            "Subscript[p, 12]",
    //            "Subscript[p, 13]"
    //        );

    //    var d1 =
    //        //GeometricProcessor.Vector(1, 1, 1);
    //        GeometricProcessor.Vector(
    //            "Subscript[d, 11]",
    //            "Subscript[d, 12]",
    //            "Subscript[d, 13]"
    //        );

    //    //var d1Norm = d1.ENorm();
    //    //var d1Unit = d1 / d1Norm;

    //    var f00 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(0, 0));
    //    var f01 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(0, 1));
    //    var f02 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(0, 2));
    //    var f11 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(1, 1));
    //    var f12 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(1, 2));
    //    var f22 = GeometricProcessor.CreateScalar(basisSet2Integral.GetValueAt1(2, 2));

    //    var a00 = e1;
    //    var a22 = d1;
    //    var a01 = ((1 - f11) * p1 - (f00 - f11) * e1) / f01;

    //    var f11By01 = (f11 - 1) / (2 * f01);

    //    var aa0 = -f11By01 * p1[0] + (f11 - f00) / (2 * f01);
    //    var aa12 = f11By01 * p1[1];
    //    var aa13 = f11By01 * p1[2];

    //    var aa23_1 = (p1[0] - 1 - aa0.Square() + aa12.Square() + aa13.Square()).Sqrt();
    //    var aa23_2 = -(p1[1] / 2 + aa0 * aa12) / aa13;
    //    var aa23_3 = (p1[2] / 2 + aa0 * aa13) / aa12;

    //    var a0 = GeometricProcessor.CreateIdentityRotor();
    //    var a1 = GeometricProcessor.CreateScaledPureRotor3D(
    //        aa0,
    //        aa12,
    //        aa13,
    //        "Subscript[a,23]".ToExpr()
    //    );

    //    var a11a =
    //        a1.Multivector.Gp(e1).Gp(a1.MultivectorReverse);

    //    var a01a =
    //        a0.Multivector.Gp(e1).Gp(a1.MultivectorReverse) +
    //        a1.Multivector.Gp(e1).Gp(a0.MultivectorReverse);

    //    var a =
    //        a0.Multivector * basisSet.GetValue(0, t).Expand() +
    //        a1.Multivector * basisSet.GetValue(1, t).Expand();

    //    var aNormSquared =
    //        a.ENormSquared().Square().ScalarValue.Expand();

    //    var cda = a.Gp(e1).Gp(a.Reverse()).MapScalars(s => s.Expand());
    //    var ca = cda.MapScalars(s => s.IntegrateScalar("t").Expand());

    //    var cd0a = cda.MapScalars(s => s.ReplaceAll(t, Expr.INT_ZERO));
    //    var cd1a = cda.MapScalars(s => s.ReplaceAll(t, Expr.INT_ONE));

    //    var c0a = ca.MapScalars(s => s.ReplaceAll(t, Expr.INT_ZERO));
    //    var c1a = ca.MapScalars(s => s.ReplaceAll(t, Expr.INT_ONE));

    //    Console.WriteLine($@"$A_{{0}} = {LaTeXComposer.GetMultivectorText(a0.Multivector)}$");
    //    Console.WriteLine($@"$A_{{1}} = {LaTeXComposer.GetMultivectorText(a1.Multivector)}$");
    //    Console.WriteLine($@"$A_{{1}}^{{\sim}} = {LaTeXComposer.GetMultivectorText(a1.MultivectorReverse)}$");
    //    Console.WriteLine($@"$A_{{1,1}} = {LaTeXComposer.GetMultivectorText(a11)}$");
    //    Console.WriteLine($@"$A_{{1,1}} = {LaTeXComposer.GetMultivectorText(a11a)}$");
    //    Console.WriteLine($@"$A_{{0,1}} = {LaTeXComposer.GetMultivectorText(a01)}$");
    //    Console.WriteLine($@"$A_{{0,1}} = {LaTeXComposer.GetMultivectorText(a01a)}$");
    //    Console.WriteLine($@"$a_{{0}} = {LaTeXComposer.GetScalarText(aa0)}$");
    //    Console.WriteLine($@"$a_{{12}} = {LaTeXComposer.GetScalarText(aa12)}$");
    //    Console.WriteLine($@"$a_{{13}} = {LaTeXComposer.GetScalarText(aa13)}$");
    //    Console.WriteLine($@"$a_{{23}} = {LaTeXComposer.GetScalarText(aa23_1)}$");
    //    Console.WriteLine($@"$a_{{23}} = {LaTeXComposer.GetScalarText(aa23_2)}$");
    //    Console.WriteLine($@"$a_{{23}} = {LaTeXComposer.GetScalarText(aa23_3)}$");
    //    Console.WriteLine($@"$A \left( t \right) = {LaTeXComposer.GetMultivectorText(a)}$");
    //    Console.WriteLine($@"$A \left( t \right) e_1 A^{{\sim}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cda)}$");
    //    Console.WriteLine($@"$A \left( 0 \right) e_1 A^{{\sim}} \left( 0 \right) = {LaTeXComposer.GetMultivectorText(cd0a)}$");
    //    Console.WriteLine($@"$A \left( 1 \right) e_1 A^{{\sim}} \left( 1 \right) = {LaTeXComposer.GetMultivectorText(cd1a)}$");
    //    Console.WriteLine($@"$\int_{{0}}^{{t}} A \left( x \right) e_1 A^{{\sim}} \left( x \right) dx = {LaTeXComposer.GetMultivectorText(ca)}$");
    //    Console.WriteLine($@"$\int_{{0}}^{{0}} A \left( x \right) e_1 A^{{\sim}} \left( x \right) dx = {LaTeXComposer.GetMultivectorText(c0a)}$");
    //    Console.WriteLine($@"$\int_{{0}}^{{1}} A \left( x \right) e_1 A^{{\sim}} \left( x \right) dx = {LaTeXComposer.GetMultivectorText(c1a)}$");
    //    Console.WriteLine();

    //    //var scaledRotor0 = 
    //    //    GeometricProcessor.CreateIdentityRotor();

    //    //var a0 = 
    //    //    GeometricProcessor.Multivector(scaledRotor0.Multivector);

    //    //var scaledRotor1 = 
    //    //    GeometricProcessor.CreateScaledParametricPureRotor3D(
    //    //        e1, 
    //    //        d1Unit,
    //    //        0.ToExpr(), //@"Subscript[\[CurlyPhi], 1]".ToExpr(),
    //    //        d1Norm
    //    //    );

    //    //var a1 = 
    //    //    GeometricProcessor.Multivector(scaledRotor1.Multivector);

    //    //var e1Rotated0 = scaledRotor0.OmMapVector(e1).FullSimplifyScalars();
    //    //Console.WriteLine($@"$A_0 = {LaTeXComposer.GetMultivectorText(a0)}$");
    //    //Console.WriteLine($@"$A_0 e_1 A_0^{{\sim}} = {LaTeXComposer.GetMultivectorText(e1Rotated0)}$");
    //    //Console.WriteLine();

    //    //var e1Rotated1 = scaledRotor1.OmMapVector(e1).FullSimplifyScalars();
    //    //Console.WriteLine($@"$A_1 = {LaTeXComposer.GetMultivectorText(a1)}$");
    //    //Console.WriteLine($@"$d_1 = {LaTeXComposer.GetMultivectorText(d1)}$");
    //    //Console.WriteLine($@"$\left\Vert d_{{1}}\right\Vert = {LaTeXComposer.GetScalarText(d1Norm)}$");
    //    //Console.WriteLine($@"$\hat{{d}}_{{1}} = {LaTeXComposer.GetMultivectorText(d1Unit)}$");
    //    //Console.WriteLine($@"$A_1 e_1 A_1^{{\sim}} = {LaTeXComposer.GetMultivectorText(e1Rotated1)}$");
    //    //Console.WriteLine();

    //    //var a = 
    //    //    a0 * basisSet.GetValue(0, t).Expand() + 
    //    //    a1 * basisSet.GetValue(1, t).Expand();

    //    //var aNormSquared = 
    //    //    a.ENormSquared().Square().ScalarValue.Expand();

    //    //var cda = a.Gp(e1).Gp(a.Reverse()).MapScalars(s => s.Expand());
    //    //var ca = cda.MapScalars(s => s.IntegrateScalar("t").Expand());

    //    var cd = (
    //        a00 * basisSet2.GetValue(0, 0, t) + 
    //        a01 * basisSet2.GetValue(0, 1, t) +
    //        a11 * basisSet2.GetValue(1, 1, t)
    //    ).MapScalars(s => s.Expand());

    //    var cdNormSquared = 
    //        cd.ENormSquared().ScalarValue.Expand();

    //    //var diff = Mfs.Subtract[aNormSquared, cdNormSquared].FullSimplify();
    //    //Console.WriteLine($@"$A \left( t \right) = {LaTeXComposer.GetMultivectorText(a)}$");
    //    //Console.WriteLine($@"$\left( A \left( t \right) A^{{\sim}} \left( t \right) \right)^2 = {LaTeXComposer.GetScalarText(aNormSquared)}$");
    //    Console.WriteLine($@"$\left\Vert c^{{\prime}} \left( t \right) \right\Vert ^2 = {LaTeXComposer.GetScalarText(cdNormSquared)}$");
    //    //Console.WriteLine($@"$\left\Vert c^{{\prime}} \left( t \right) \right\Vert ^2 - \left( A \left( t \right) A^{{\sim}} \left( t \right) \right)^2 = {LaTeXComposer.GetScalarText(diff)}$");
    //    Console.WriteLine();

    //    var cd0 =
    //        a00 * basisSet2.GetValue(0, 0, Expr.INT_ZERO) +
    //        a01 * basisSet2.GetValue(0, 1, Expr.INT_ZERO) +
    //        a11 * basisSet2.GetValue(1, 1, Expr.INT_ZERO);

    //    var cd1 = 
    //        a00 * basisSet2.GetValue(0, 0, Expr.INT_ONE) +
    //        a01 * basisSet2.GetValue(0, 1, Expr.INT_ONE) +
    //        a11 * basisSet2.GetValue(1, 1, Expr.INT_ONE);

    //    var c = (
    //        a00 * basisSet2Integral.GetValue(0, 0, t) + 
    //        a01 * basisSet2Integral.GetValue(0, 1, t) +
    //        a11 * basisSet2Integral.GetValue(1, 1, t)
    //    ).MapScalars(s => s.Expand());

    //    var c0 = 
    //        a00 * basisSet2Integral.GetValue(0, 0, Expr.INT_ZERO) +
    //        a01 * basisSet2Integral.GetValue(0, 1, Expr.INT_ZERO) +
    //        a11 * basisSet2Integral.GetValue(1, 1, Expr.INT_ZERO);

    //    var c1 = 
    //        a00 * basisSet2Integral.GetValue(0, 0, Expr.INT_ONE) +
    //        a01 * basisSet2Integral.GetValue(0, 1, Expr.INT_ONE) +
    //        a11 * basisSet2Integral.GetValue(1, 1, Expr.INT_ONE);

    //    //Console.WriteLine($@"$c^{{\prime}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cda)}$");
    //    Console.WriteLine($@"$c^{{\prime}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cd)}$");
    //    Console.WriteLine($@"$c^{{\prime}} \left( 0 \right) = {LaTeXComposer.GetMultivectorText(cd0)}$");
    //    Console.WriteLine($@"$c^{{\prime}} \left( 1 \right) = {LaTeXComposer.GetMultivectorText(cd1)}$");
    //    Console.WriteLine();

    //    //Console.WriteLine($@"$c \left( t \right) = {LaTeXComposer.GetMultivectorText(ca)}$");
    //    Console.WriteLine($@"$c \left( t \right) = {LaTeXComposer.GetMultivectorText(c)}$");
    //    Console.WriteLine($@"$c \left( 0 \right) = {LaTeXComposer.GetMultivectorText(c0)}$");
    //    Console.WriteLine($@"$c \left( 1 \right) = {LaTeXComposer.GetMultivectorText(c1)}$");
    //    Console.WriteLine();
    //}

    /// <summary>
    /// Define a canonical 3D cubic PH curve using 1st degree Bernstein Basis polynomials
    /// </summary>
    public static void Example2()
    {
        var t = "t".ToSymbolExpr();
        //var e1 = GeometricProcessor.Vector(0);

        var p = GeometricProcessor.Vector(10d, -2d, 1d);
        var d = GeometricProcessor.Vector(0.9d, -0.5d, 1.2d);
        var theta1 = Expr.INT_ZERO.DegreesToPolarAngle(ScalarProcessor);
        var theta2 = Expr.INT_ZERO.DegreesToPolarAngle(ScalarProcessor);

        //var p = GeometricProcessor.Vector(
        //    "Subscript[p, 1]", 
        //    "Subscript[p, 2]", 
        //    "Subscript[p, 3]"
        //);

        //var d = GeometricProcessor.Vector(
        //    "Subscript[d, 1]", 
        //    "Subscript[d, 2]", 
        //    "Subscript[d, 3]"
        //);

        //var theta1 = @"Subscript[\[Theta], 1]".ToExpr();
        //var theta2 = @"Subscript[\[Theta], 2]".ToExpr();

        var phCurve = PhCurve3DDegree5Canonical<Expr>.Create(
            GeometricProcessor,
            p,
            d,
            theta1,
            theta2
        );

        //var b0 = phCurve.BasisSet.GetValue(0, t);
        //var b1 = phCurve.BasisSet.GetValue(1, t);
        //var b2 = phCurve.BasisSet.GetValue(2, t);
        //var a = b0 + b1 * phCurve.ScaledRotor1.Multivector + b2 * phCurve.ScaledRotor2.Multivector;

        //var a = GeometricProcessor.CreateScaledPureRotor3D(
        //    aMultivector[0],
        //    aMultivector[3],
        //    aMultivector[5],
        //    aMultivector[6]
        //);

        //var cda = a.OmMap(e1);
        //var ca = cda.MapScalars(s => s.IntegrateScalar("t").Expand());
        //var sigma1 = a.Sp(a.Reverse()).ScalarValue.Expand();

        //Console.WriteLine($@"$A = {LaTeXComposer.GetMultivectorText(a)}$");
        //Console.WriteLine();

        //Console.WriteLine($@"$A_1 = {LaTeXComposer.GetMultivectorText(phCurve.A1)}$");
        //Console.WriteLine($@"$A_2 = {LaTeXComposer.GetMultivectorText(phCurve.A2)}$");
        //Console.WriteLine();

        //Console.WriteLine($@"$A_{{0,0}} = {LaTeXComposer.GetMultivectorText(phCurve.A00)}$");
        //Console.WriteLine($@"$A_{{0,1}} = {LaTeXComposer.GetMultivectorText(phCurve.A01)}$");
        //Console.WriteLine($@"$A_{{0,2}} = {LaTeXComposer.GetMultivectorText(phCurve.A02)}$");
        //Console.WriteLine($@"$A_{{1,1}} = {LaTeXComposer.GetMultivectorText(phCurve.A11)}$");
        //Console.WriteLine($@"$A_{{1,2}} = {LaTeXComposer.GetMultivectorText(phCurve.A12)}$");
        //Console.WriteLine($@"$A_{{2,2}} = {LaTeXComposer.GetMultivectorText(phCurve.A22)}$");
        //Console.WriteLine();

        var cd = phCurve.GetHodographPoint(t);
        var cd0 = phCurve.GetHodographPoint(Expr.INT_ZERO);
        var cd1 = phCurve.GetHodographPoint(Expr.INT_ONE);
        //var cdNormSquared = cd.ENormSquared();

        var c = phCurve.GetCurvePoint(t).MapScalars(s => s.Expand());
        var c0 = phCurve.GetCurvePoint(Expr.INT_ZERO);
        var c1 = phCurve.GetCurvePoint(Expr.INT_ONE);

        var sigma = phCurve.GetSigmaValue(t).ScalarValue.Expand();
        var sigma0 = phCurve.GetSigmaValue(Expr.INT_ZERO).ScalarValue;
        var sigma1 = phCurve.GetSigmaValue(Expr.INT_ONE).ScalarValue;

        var length = phCurve.GetLength("0.5".ToExpr(), "1.".ToExpr());

        //Console.WriteLine($@"$c^{{\prime}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cda)}$");
        Console.WriteLine($@"$c^{{\prime}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cd)}$");
        Console.WriteLine($@"$c^{{\prime}} \left( 0 \right) = {LaTeXComposer.GetMultivectorText(cd0)}$");
        Console.WriteLine($@"$c^{{\prime}} \left( 1 \right) = {LaTeXComposer.GetMultivectorText(cd1)}$");
        Console.WriteLine();

        //Console.WriteLine($@"$\left\Vert \boldsymbol{{c}}^{{\prime}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(cdNormSquared)}$");
        //Console.WriteLine($@"$\left\Vert \boldsymbol{{c}}^{{\prime}}\left(t\right)\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText((sigma * sigma).ScalarValue.Expand())}$");
        //Console.WriteLine($@"$\sigma \left( t \right) = {LaTeXComposer.GetScalarText(sigma1)}$");
        Console.WriteLine($@"$\sigma \left( t \right) = {LaTeXComposer.GetScalarText(sigma)}$");
        Console.WriteLine($@"$\sigma \left( 0 \right) = {LaTeXComposer.GetScalarText(sigma0)}$");
        Console.WriteLine($@"$\sigma \left( 1 \right) = {LaTeXComposer.GetScalarText(sigma1)}$");
        Console.WriteLine();

        Console.WriteLine($@"$c \left( t \right) = {LaTeXComposer.GetMultivectorText(c)}$");
        //Console.WriteLine($@"$c \left( t \right) = {LaTeXComposer.GetMultivectorText(ca)}$");
        Console.WriteLine($@"$c \left( 0 \right) = {LaTeXComposer.GetMultivectorText(c0)}$");
        Console.WriteLine($@"$c \left( 1 \right) = {LaTeXComposer.GetMultivectorText(c1)}$");
        Console.WriteLine();

        Console.WriteLine($@"Length = ${LaTeXComposer.GetScalarText(length)}$");
        Console.WriteLine();
    }
}
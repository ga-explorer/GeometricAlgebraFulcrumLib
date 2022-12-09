using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.PhCurves;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class SymbolicPhConstruction2DSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Example1()
        {
            const int degree = 2;
            var parameterValue = "t".ToSymbolExpr();
            var processor = GeometricProcessor;
            
            var sqrt2 = "Sqrt[2]".ToExpr().CreateScalar(GeometricProcessor);

            //var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e12 = GeometricProcessor.CreateBivectorBasis(0);

            var p =
                GeometricProcessor.CreateVector(1, 1);
            //GeometricProcessor.CreateVector("Subscript[p, 1]", "Subscript[p, 2]");

            var d =
                GeometricProcessor.CreateVector(1, 1).DivideByENorm();
            //GeometricProcessor.CreateVector("Subscript[d, 1]", "Subscript[d, 2]");

            var BasisSet = BernsteinBasisSet<Expr>.Create(processor, 2);
            var _basisPairProductSet = BernsteinBasisPairProductSet<Expr>.Create(BasisSet);
            var _basisPairProductIntegralSet = BernsteinBasisPairProductIntegralSet<Expr>.Create(_basisPairProductSet);

            var e1 = processor.CreateVectorBasis(0);

            var ScaledRotor0 = processor.CreateScaledIdentityRotor();

            var f01_1 = _basisPairProductIntegralSet.GetValueAt1(0, 1);
            var f11_1 = _basisPairProductIntegralSet.GetValueAt1(1, 1);
            var f12_1 = _basisPairProductIntegralSet.GetValueAt1(1, 2);

            var dNorm = d.ENorm();
            var dUnit = d / dNorm;
            
            var ScaledRotor2 = GeometricProcessor.CreateScaledParametricPureRotor3D(
                e1,
                dUnit,
                processor.ScalarZero,
                dNorm
            );

            var Vector00 = e1;
            var Vector22 = d;
            var Vector02 = ((e1.Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1)).GetVectorPart()).FullSimplifyScalars();

            var u = p - (e1 + d) / 8 + Vector02 / 24;
            var uNorm = u.ENorm();
            var uUnit = u / uNorm;

            var v1 = f11_1.Sqrt();
            var v0 = f01_1 / v1;
            var v2 = f12_1 / v1;
            
            var v = GeometricProcessor.CreateScaledParametricPureRotor3D(
                e1,
                uUnit,
                processor.ScalarZero,
                uNorm
            ).Multivector;

            var a1 = (v - v0 - v2 * ScaledRotor2.Multivector) / v1;

            var ScaledRotor1 = processor.CreateScaledPureRotor3D(
                a1[0],
                a1[3],
                a1[5],
                a1[6]
            );

            var Vector01 = (e1.Gp(ScaledRotor1.MultivectorReverse) + ScaledRotor1.Multivector.Gp(e1)).GetVectorPart().FullSimplifyScalars();
            var Vector12 = (ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor2.MultivectorReverse) + ScaledRotor2.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse)).GetVectorPart().FullSimplifyScalars();
            var Vector11 = ScaledRotor1.Multivector.Gp(e1).Gp(ScaledRotor1.MultivectorReverse).GetVectorPart().FullSimplifyScalars();

            var Scalar00 = ScaledRotor0.Multivector.ESp(ScaledRotor0.MultivectorReverse).FullSimplify();
            var Scalar11 = ScaledRotor1.Multivector.ESp(ScaledRotor1.MultivectorReverse).FullSimplify();
            var Scalar22 = ScaledRotor2.Multivector.ESp(ScaledRotor2.MultivectorReverse).FullSimplify();

            var Scalar01 = (ScaledRotor0.Multivector.ESp(ScaledRotor1.MultivectorReverse) +
                           ScaledRotor1.Multivector.ESp(ScaledRotor0.MultivectorReverse)).FullSimplify();

            var Scalar02 = (ScaledRotor0.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                           ScaledRotor2.Multivector.ESp(ScaledRotor0.MultivectorReverse)).FullSimplify();

            var Scalar12 = (ScaledRotor1.Multivector.ESp(ScaledRotor2.MultivectorReverse) +
                           ScaledRotor2.Multivector.ESp(ScaledRotor1.MultivectorReverse)).FullSimplify();

            Console.WriteLine($@"Vector00 = ${LaTeXComposer.GetMultivectorText(Vector00)}$");
            Console.WriteLine($@"Vector01 = ${LaTeXComposer.GetMultivectorText(Vector01)}$");
            Console.WriteLine($@"Vector02 = ${LaTeXComposer.GetMultivectorText(Vector02)}$");
            Console.WriteLine($@"Vector11 = ${LaTeXComposer.GetMultivectorText(Vector11)}$");
            Console.WriteLine($@"Vector12 = ${LaTeXComposer.GetMultivectorText(Vector12)}$");
            Console.WriteLine($@"Vector22 = ${LaTeXComposer.GetMultivectorText(Vector22)}$");

            Console.WriteLine($@"Scalar00 = ${LaTeXComposer.GetScalarText(Scalar00)}$");
            Console.WriteLine($@"Scalar01 = ${LaTeXComposer.GetScalarText(Scalar01)}$");
            Console.WriteLine($@"Scalar02 = ${LaTeXComposer.GetScalarText(Scalar02)}$");
            Console.WriteLine($@"Scalar11 = ${LaTeXComposer.GetScalarText(Scalar11)}$");
            Console.WriteLine($@"Scalar12 = ${LaTeXComposer.GetScalarText(Scalar12)}$");
            Console.WriteLine($@"Scalar22 = ${LaTeXComposer.GetScalarText(Scalar22)}$");

            parameterValue = 0.CreateScalar(processor);
            var f00 = _basisPairProductSet.GetValue(0, 0, parameterValue);
            var f01 = _basisPairProductSet.GetValue(0, 1, parameterValue);
            var f02 = _basisPairProductSet.GetValue(0, 2, parameterValue);
            var f11 = _basisPairProductSet.GetValue(1, 1, parameterValue);
            var f12 = _basisPairProductSet.GetValue(1, 2, parameterValue);
            var f22 = _basisPairProductSet.GetValue(2, 2, parameterValue);

            var cd =  
                f00 * Vector00 + f01 * Vector01 + f02 * Vector02 +
                f11 * Vector11 + f12 * Vector12 + f22 * Vector22;


            f00 = _basisPairProductIntegralSet.GetValue(0, 0, parameterValue);
            f01 = _basisPairProductIntegralSet.GetValue(0, 1, parameterValue);
            f02 = _basisPairProductIntegralSet.GetValue(0, 2, parameterValue);
            f11 = _basisPairProductIntegralSet.GetValue(1, 1, parameterValue);
            f12 = _basisPairProductIntegralSet.GetValue(1, 2, parameterValue);
            f22 = _basisPairProductIntegralSet.GetValue(2, 2, parameterValue);

            var c =  
                f00 * Vector00 + f01 * Vector01 + f02 * Vector02 +
                f11 * Vector11 + f12 * Vector12 + f22 * Vector22;

            Console.WriteLine($@"$\boldsymbol{{c}}\left({LaTeXComposer.GetScalarText(parameterValue)}\right) = {LaTeXComposer.GetMultivectorText(c)}$");
            Console.WriteLine($@"$\boldsymbol{{c}}^{{\prime}}\left({LaTeXComposer.GetScalarText(parameterValue)}\right) = {LaTeXComposer.GetMultivectorText(cd)}$");
        }

        public static void Example2()
        {
            var parameterValue = "t".ToSymbolExpr();
            var processor = GeometricProcessor;
            
            var sqrt2 = "Sqrt[2]".ToExpr().CreateScalar(GeometricProcessor);

            //var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = processor.CreateVectorBasis(1);
            var e12 = processor.CreateBivectorBasis(0);

            var p =
                GeometricProcessor.CreateVector(1d, 1d);
                //GeometricProcessor.CreateVector("Subscript[p, 1]", "Subscript[p, 2]");

            var d =
                GeometricProcessor.CreateVector(1.5d, 2d);
                //GeometricProcessor.CreateVector("Subscript[d, 1]", "Subscript[d, 2]");

            var phCurve = PhCurve2DDegree5Canonical<Expr>.Create(processor, p, d);

            var c0 = phCurve.GetCurvePoint(processor.ScalarZero);
            var c1 = phCurve.GetCurvePoint(processor.ScalarOne);

            var cd0 = phCurve.GetHodographPoint(processor.ScalarZero);
            var cd1 = phCurve.GetHodographPoint(processor.ScalarOne);

            Console.WriteLine($@"c0 = ${LaTeXComposer.GetMultivectorText(c0)}$");
            Console.WriteLine($@"c1 = ${LaTeXComposer.GetMultivectorText(c1)}$");
            Console.WriteLine($@"cd0 = ${LaTeXComposer.GetMultivectorText(cd0)}$");
            Console.WriteLine($@"cd1 = ${LaTeXComposer.GetMultivectorText(cd1)}$");
            Console.WriteLine();

            Console.WriteLine($@"ScaledRotorV = ${LaTeXComposer.GetMultivectorText(phCurve.ScaledRotorV)}$");
            Console.WriteLine($@"ScaledRotor0 = ${LaTeXComposer.GetMultivectorText(phCurve.ScaledRotor0)}$");
            Console.WriteLine($@"ScaledRotor1 = ${LaTeXComposer.GetMultivectorText(phCurve.ScaledRotor1)}$");
            Console.WriteLine($@"ScaledRotor2 = ${LaTeXComposer.GetMultivectorText(phCurve.ScaledRotor2)}$");
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

            var sqrt2 = "Sqrt[2]".ToExpr().CreateScalar(GeometricProcessor);

            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e12 = GeometricProcessor.CreateBivectorBasis(0);

            var p =
                GeometricProcessor.CreateVector(1, 1);
                //GeometricProcessor.CreateVector("Subscript[p, 1]", "Subscript[p, 2]");

            var d =
                GeometricProcessor.CreateVector(1, 1).DivideByENorm();
                //GeometricProcessor.CreateVector("Subscript[d, 1]", "Subscript[d, 2]");

            var d1 = d[0];
            var d2 = d[1];
            var dNorm1 = d.Norm() + d1;
            var dNorm1Sqrt = dNorm1.Sqrt();

            var u = 
                p - (e1 + d) / 8 + (dNorm1Sqrt * e1 + d2 / dNorm1Sqrt * e2) / 12 / sqrt2;

            var u1 = u[0];
            var u2 = u[1];
            var uNorm1 = u.Norm() + u1;
            var uNorm1Sqrt = uNorm1.Sqrt();

            var v = (uNorm1Sqrt - u2 / uNorm1Sqrt * e12) / sqrt2;
            //var v0 = GeometricProcessor.SqrtRational(3, 40);
            var v1 = GeometricProcessor.SqrtRational(2, 10);
            //var v2 = GeometricProcessor.SqrtRational(3, 40);

            var value3by4 = GeometricProcessor.Rational(3, 4);
            var a2 = (dNorm1Sqrt - d2 / dNorm1Sqrt * e12) / sqrt2;
            var a1 = v / v1 - value3by4 - value3by4 * a2;

            var vector00 = e1;
            var vector01 = 2 * a1 * e1;
            var vector02 = sqrt2 * (dNorm1Sqrt * e1 + d2 / dNorm1Sqrt * e2);
            var vector11 = a1 * a2 * e1;
            var vector12 = 2 * a1 * a2 * e1;
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
}
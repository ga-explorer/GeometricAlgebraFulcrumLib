using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry;

public static class RotationSamples
{

    private static GaVector<Expr> CreateSymbolicVector(this IGeometricAlgebraProcessor<Expr> geometricProcessor, string name, string subscript, int termsCount)
    {
        var vector =
            $"Subscript[{name},{subscript}1]".ToExpr() * geometricProcessor.CreateVectorBasis(0);

        for (var i = 2; i <= termsCount; i++)
            vector += $"Subscript[{name},{subscript}{i}]".ToExpr() * geometricProcessor.CreateVectorBasis(i - 1);

        return vector;
    }

    public static void ValidationExample1()
    {
        const int n = 10;

        var scalarProcessor =
            ScalarAlgebraFloat64Processor.DefaultProcessor;

        var geometricProcessor =
            scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(n);

        var textComposer =
            TextFloat64Composer.DefaultComposer;

        var laTeXComposer =
            LaTeXFloat64Composer.DefaultComposer;

        var random = 
            geometricProcessor.CreateGeometricRandomComposer(10);

        for (var j = 0; j < 10; j++)
        {
            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                new VectorToVectorRotation(
                    u.GetDenseTuple(),
                    v.GetDenseTuple()
                );

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).GetDenseTuple();
                var y2 = uvVectorRotation.Rotate(x.GetDenseTuple());

                Debug.Assert(
                    (y1 - y2).GetLength().IsNearZero()
                );
            }
        }
    }

    public static void ValidationExample2()
    {
        const int n = 10;

        var scalarProcessor =
            ScalarAlgebraFloat64Processor.DefaultProcessor;

        var geometricProcessor =
            scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(n);

        var textComposer =
            TextFloat64Composer.DefaultComposer;

        var laTeXComposer =
            LaTeXFloat64Composer.DefaultComposer;

        var random = 
            geometricProcessor.CreateGeometricRandomComposer(10);

        for (var j = 0; j < 10; j++)
        {
            var uAxisIndex =
                (int) random.GetBasisVectorIndex();

            var u = 
                geometricProcessor.CreateVectorBasis(uAxisIndex);
            
            var v =
                random.GetVector(-1, 1).DivideByNorm();

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                new AxisToVectorRotation(
                    uAxisIndex,
                    false,
                    v.GetDenseTuple()
                );

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).GetDenseTuple();
                var y2 = uvVectorRotation.Rotate(x.GetDenseTuple());

                Debug.Assert(
                    (y1 - y2).GetLength().IsNearZero()
                );
            }
        }
    }
    
    public static void ValidationExample3()
    {
        const int n = 10;

        var scalarProcessor =
            ScalarAlgebraFloat64Processor.DefaultProcessor;

        var geometricProcessor =
            scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(n);

        var textComposer =
            TextFloat64Composer.DefaultComposer;

        var laTeXComposer =
            LaTeXFloat64Composer.DefaultComposer;

        var random = 
            geometricProcessor.CreateGeometricRandomComposer(10);

        for (var j = 0; j < 10; j++)
        {
            var vAxisIndex =
                (int) random.GetBasisVectorIndex();

            var u =
                random.GetVector(-1, 1).DivideByNorm();

            var v = 
                geometricProcessor.CreateVectorBasis(vAxisIndex);

            var uvRotor =
                u.GetEuclideanRotorTo(v);

            var uvVectorRotation =
                new VectorToAxisRotation(
                    u.GetDenseTuple(),
                    vAxisIndex,
                    false
                );

            for (var i = 0; i < 100; i++)
            {
                var x =
                    random.GetVector(-1, 1);

                var y1 = uvRotor.OmMap(x).GetDenseTuple();
                var y2 = uvVectorRotation.Rotate(x.GetDenseTuple());

                Debug.Assert(
                    (y1 - y2).GetLength().IsNearZero()
                );
            }
        }
    }

    public static void Example1()
    {
        for (var n = 3; n <= 6; n++)
        {
            Console.WriteLine($"{n}-dimensions");

            var scalarProcessor =
                ScalarAlgebraMathematicaProcessor.DefaultProcessor;

            var geometricProcessor =
                scalarProcessor.CreateGeometricAlgebraEuclideanProcessor((uint)n);

            var textComposer =
                TextMathematicaComposer.DefaultComposer;

            var laTeXComposer =
                LaTeXMathematicaComposer.DefaultComposer;

            var u =
                geometricProcessor.CreateSymbolicVector("u", "", n);


            var v =
                geometricProcessor.CreateSymbolicVector("u", "", n);


            var uvRotor =
                geometricProcessor.CreatePureRotor(u, v);

            var uvRotorMatrix =
                uvRotor.GetMultivectorsMappingArray(n, n);

            Console.WriteLine($@"$R_{{u,v}} = {laTeXComposer.GetMultivectorText(uvRotor.Multivector)}$");
            Console.WriteLine($@"$M_{{u,v}} = {laTeXComposer.GetArrayText(uvRotorMatrix)}$");
            Console.WriteLine();

            for (var k = 0; k < n; k++)
            {
                var ep = geometricProcessor.CreateVectorBasis(k);
                var en = -ep;

                var uepRotor =
                    geometricProcessor.CreatePureRotor(u, ep);
                
                var uepRotorMatrix =
                    uepRotor.GetMultivectorsMappingArray(n, n);
                
                Console.WriteLine($@"$R_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetMultivectorText(uepRotor.Multivector)}$");
                Console.WriteLine($@"$M_{{u,e_{{{k + 1}}}}} = {laTeXComposer.GetArrayText(uepRotorMatrix)}$");
                Console.WriteLine();


                var uenRotor =
                    geometricProcessor.CreatePureRotor(u, en);
                
                var uenRotorMatrix =
                    uenRotor.GetMultivectorsMappingArray(n, n);
                
                Console.WriteLine($@"$R_{{u,-e_{{{k + 1}}}}} = {laTeXComposer.GetMultivectorText(uenRotor.Multivector)}$");
                Console.WriteLine($@"$M_{{u,-e_{{{k + 1}}}}} = {laTeXComposer.GetArrayText(uenRotorMatrix)}$");
                Console.WriteLine();
            }
        }
    }
}
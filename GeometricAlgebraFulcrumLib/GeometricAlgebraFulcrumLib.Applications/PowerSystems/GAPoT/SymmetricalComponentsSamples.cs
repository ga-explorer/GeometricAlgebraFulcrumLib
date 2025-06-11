using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GAPoT;

public static class SymmetricalComponentsSamples
{
    public static IXGaOutermorphism<T> CreateClarkePhasorMap<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        Debug.Assert(
            vSpaceDimensions.IsEven()
        );

        var scalarProcessor = processor.ScalarProcessor;

        var phasorCount = vSpaceDimensions / 2;

        var clarkeArray =
            scalarProcessor.CreateClarkeRotationArray(phasorCount);

        var clarkePhasorMapArray =
            scalarProcessor.CreateArrayZero2D(phasorCount * 2);

        for (var i = 0; i < phasorCount; i++)
        for (var j = 0; j < phasorCount; j++)
        {
            clarkePhasorMapArray[i, j] = clarkeArray[i, j];
            clarkePhasorMapArray[i + phasorCount, j + phasorCount] = clarkeArray[i, j];
        }

        var basisVectorImagesDictionary =
            new Dictionary<int, LinVector<T>>();

        for (var i = 0; i < phasorCount * 2; i++)
            basisVectorImagesDictionary.Add(
                i,
                clarkePhasorMapArray.ColumnToLinVector(scalarProcessor, i)
            );

        return scalarProcessor.CreateLinUnilinearMap(
            basisVectorImagesDictionary
        ).ToOutermorphism(processor);
    }

        
    public static T[,] GetPMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int a, int b, int c, int d)
    {
        var scalarProcessor = processor.ScalarProcessor;

        var ea = processor.VectorTerm(a);
        var eb = processor.VectorTerm(b);
        var ec = processor.VectorTerm(c);
        var ed = processor.VectorTerm(d);

        var pArray = ea.CreatePureRotorSequence(
            eb,
            ec, 
            ed,
            true
        ).GetVectorMapArray(vSpaceDimensions, vSpaceDimensions);

        for (var j = 0; j < pArray.GetLength(1); j++)
        {
            if (j == a || j == b)
                continue;

            for (var i = 0; i < pArray.GetLength(0); i++)
                pArray[i, j] = scalarProcessor.ZeroValue;
        }

        return pArray;
    }

    public static T[,] GetNMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int a, int b, int c, int d)
    {
        var scalarProcessor = processor.ScalarProcessor;

        var ea = processor.VectorTerm(a);
        var eb = processor.VectorTerm(b);
        var ec = processor.VectorTerm(c);
        var ed = processor.VectorTerm(d);

        var nArray = ea.CreatePureRotorSequence(
            eb,
            ec, 
            -ed,
            true
        ).GetVectorMapArray(vSpaceDimensions, vSpaceDimensions);

        for (var j = 0; j < nArray.GetLength(1); j++)
        {
            if (j == a || j == b)
                continue;

            for (var i = 0; i < nArray.GetLength(0); i++)
                nArray[i, j] = scalarProcessor.ZeroValue;
        }

        return nArray;
    }

    public static T[,] GetTMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int k)
    {
        var n = vSpaceDimensions / 2;

        if (k == 0)
            return processor.GetPMatrix(vSpaceDimensions, 0, n, n - 1, 2 * n - 1);

        if (n.IsEven() && k == n / 2)
            return processor.GetPMatrix(vSpaceDimensions, n / 2, n / 2 + n, n - 2, 2 * n - 2);

        var a = k;
        var b = k + n;
        int cp;
        int dp;
        int cn;
        int dn;
        var nNegative = false;

        if (n.IsOdd())
        {
            if (k <= (n - 1) / 2)
            {
                k = k - 1;

                cn = k * 2;
                dn = k * 2 + 1;

                cp = dn + n;
                dp = cn + n;
            }
            else
            {
                k = n - k - 1;
                cp = k * 2;
                dp = k * 2 + 1;

                cn = dp + n;
                dn = cp + n;

                nNegative = true;
            }
        }
        else
        {
            if (k < n / 2)
            {
                k = k - 1;

                cn = k * 2;
                dn = k * 2 + 1;

                cp = dn + n;
                dp = cn + n;
            }
            else
            {
                k = n - k - 1;
                cp = k * 2;
                dp = k * 2 + 1;

                cn = dp + n;
                dn = cp + n;

                nNegative = true;
            }
        }

        var pMatrix = processor.GetPMatrix(vSpaceDimensions, a, b, cp, dp);
        var nMatrix = processor.GetNMatrix(vSpaceDimensions, a, b, cn, dn);

        var scalarProcessor = processor.ScalarProcessor;

        var sqrt2 = scalarProcessor.Sqrt(scalarProcessor.TwoValue).ScalarValue;

        var tMatrix = nNegative
            ? scalarProcessor.Subtract(pMatrix, nMatrix)
            : scalarProcessor.Add(pMatrix, nMatrix);

        return scalarProcessor.Divide(tMatrix, sqrt2);
    }
        
    public static void EigenDecompositionSample()
    {
        const int n = 9;

        var metric = XGaFloat64Processor.Euclidean;
        var matrix = n.CreateUnitaryDftMatrix();
        var matrixInv = n.CreateUnitaryDftMatrix(true);

        Debug.Assert(
            (matrixInv * matrix - n.CreateDenseIdentityMatrix().ToComplex()).L2Norm().IsNearZero()
        );

        var eigenPairList =
            matrix
                .GetComplexEigenPairs()
                .OrderByDescending(p => p.Item1.Magnitude);

        foreach (var (value, vector) in eigenPairList)
        {
            var angle = value.GetPhaseAsPolarAngle();
            var blade = metric.Vector(vector.Imaginary().ToArray().MapItems(d => d.Round(6))).Op(
                metric.Vector(vector.Real().ToArray().MapItems(d => d.Round(6)))
            );

            blade = blade.Divide(blade.ENorm().ScalarValue);

            //Console.WriteLine($" Eigen Value {i}: {value}");
            //Console.WriteLine($"Eigen Vector {i}: {vector}");
            Console.WriteLine($"           Angle: {angle}");
            Console.WriteLine($"     Eigen Blade: {blade}");
            Console.WriteLine();
        }
    }
}
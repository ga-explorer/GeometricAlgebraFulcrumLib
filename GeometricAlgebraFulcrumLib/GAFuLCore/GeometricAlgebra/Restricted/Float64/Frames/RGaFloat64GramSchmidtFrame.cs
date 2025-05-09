using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public class RGaFloat64GramSchmidtFrame
{
    public static RGaFloat64GramSchmidtFrame Create(LinFloat64Vector3D v1, LinFloat64Vector3D v2, LinFloat64Vector3D v3)
    {
        var array = new double[,]
        {
            { v1.X, v2.X, v3.X },
            { v1.Y, v2.Y, v3.Y },
            { v1.Z, v2.Z, v3.Z }
        };

        var matrix = Matrix<double>.Build.DenseOfArray(array);

        var qr = matrix.QR();

        var n1 = qr.R[0, 0];
        var n2 = qr.R[1, 1];
        var n3 = qr.R[2, 2];

        var e1 = n1 >= 0
            ? new[] { qr.Q[0, 0], qr.Q[1, 0], qr.Q[2, 0] }
            : new[] { -qr.Q[0, 0], -qr.Q[1, 0], -qr.Q[2, 0] };

        var e2 = n2 >= 0
            ? new[] { qr.Q[0, 1], qr.Q[1, 1], qr.Q[2, 1] }
            : new[] { -qr.Q[0, 1], -qr.Q[1, 1], -qr.Q[2, 1] };

        var e3 = n3 >= 0
            ? new[] { qr.Q[0, 2], qr.Q[1, 2], qr.Q[2, 2] }
            : new[] { -qr.Q[0, 2], -qr.Q[1, 2], -qr.Q[2, 2] };

        var directionNorms = new[] { n1.Abs(), n2.Abs(), n3.Abs() };
        var unitDirections = new[]
        {
            RGaFloat64Processor.Euclidean.Vector(e1),
            RGaFloat64Processor.Euclidean.Vector(e2),
            RGaFloat64Processor.Euclidean.Vector(e3)
        };

        return new RGaFloat64GramSchmidtFrame(directionNorms, unitDirections);
    }

    //public static RGaFloat64GramSchmidtFrame Create(params LinFloat64Vector[] vArray)
    //{
    //    var n = Math.Max(
    //        vArray.Length,
    //        vArray.Max(v => v.Count)
    //    );

    //    var array = new double[n, n];

    //    for (var c = 0; c < vArray.Length; c++)
    //    {
    //        var v = vArray[c];

    //        for (var r = 0; r < v.Count; r++)
    //        {
    //            array[r, c] = v[r];
    //        }
    //    }

    //    var matrix = Matrix<double>.Build.DenseOfArray(array);

    //    var qr = matrix.QR();

    //    var directionNorms = new double[n];
    //    var unitDirections = new RGaFloat64Vector[n];

    //    for (var c = 0; c < n; c++)
    //    {
    //        var directionNorm = qr.R[c, c];
    //        var unitDirection = new double[n];

    //        if (directionNorm >= 0)
    //        {
    //            for (var r = 0; r < n; r++)
    //                unitDirection[r] = qr.Q[r, c];
    //        }
    //        else
    //        {
    //            directionNorm = -directionNorm;

    //            for (var r = 0; r < n; r++)
    //                unitDirection[r] = -qr.Q[r, c];
    //        }

    //        directionNorms[c] = directionNorm;
    //        unitDirections[c] = RGaFloat64Processor.Euclidean.Vector(unitDirection);
    //    }

    //    return new RGaFloat64GramSchmidtFrame(directionNorms, unitDirections);
    //}

    public static RGaFloat64GramSchmidtFrame Create(params LinFloat64Vector[] vArray)
    {
        var n = Math.Max(
            vArray.Length,
            vArray.Max(v => v.VSpaceDimensions)
        );

        var array = new double[n, n];

        for (var c = 0; c < vArray.Length; c++)
        {
            var v = vArray[c];

            foreach (var (r, s) in v.IndexScalarPairs)
            {
                array[r, c] = s;
            }
        }

        var matrix = Matrix<double>.Build.DenseOfArray(array);

        var qr = matrix.QR();

        var directionNorms = new double[n];
        var unitDirections = new RGaFloat64Vector[n];

        for (var c = 0; c < n; c++)
        {
            var directionNorm = qr.R[c, c];
            var unitDirection = new double[n];

            if (directionNorm >= 0)
            {
                for (var r = 0; r < n; r++)
                    unitDirection[r] = qr.Q[r, c];
            }
            else
            {
                directionNorm = -directionNorm;

                for (var r = 0; r < n; r++)
                    unitDirection[r] = -qr.Q[r, c];
            }

            directionNorms[c] = directionNorm;
            unitDirections[c] = RGaFloat64Processor.Euclidean.Vector(unitDirection);
        }

        return new RGaFloat64GramSchmidtFrame(directionNorms, unitDirections);
    }

    public static RGaFloat64GramSchmidtFrame Create(params RGaFloat64Vector[] vArray)
    {
        var n = Math.Max(
            vArray.Length,
            vArray.Max(v => v.Count)
        );

        var array = new double[n, n];

        for (var c = 0; c < vArray.Length; c++)
        {
            var v = vArray[c];

            foreach (var (r, s) in v.IndexScalarPairs)
            {
                array[r, c] = s;
            }
        }

        var matrix = Matrix<double>.Build.DenseOfArray(array);

        var qr = matrix.QR();

        var directionNorms = new double[n];
        var unitDirections = new RGaFloat64Vector[n];

        for (var c = 0; c < n; c++)
        {
            var directionNorm = qr.R[c, c];
            var unitDirection = new double[n];

            if (directionNorm >= 0)
            {
                for (var r = 0; r < n; r++)
                    unitDirection[r] = qr.Q[r, c];
            }
            else
            {
                directionNorm = -directionNorm;

                for (var r = 0; r < n; r++)
                    unitDirection[r] = -qr.Q[r, c];
            }

            directionNorms[c] = directionNorm;
            unitDirections[c] = RGaFloat64Processor.Euclidean.Vector(unitDirection);
        }

        return new RGaFloat64GramSchmidtFrame(directionNorms, unitDirections);
    }


    private readonly double[] _directionNormsArray;
    public IReadOnlyList<double> DirectionNorms
        => _directionNormsArray;

    public IReadOnlyList<RGaFloat64Vector> UnitDirections { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaFloat64GramSchmidtFrame(double[] directionNorms, IReadOnlyList<RGaFloat64Vector> unitDirections)
    {
        _directionNormsArray = directionNorms;
        UnitDirections = unitDirections;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64GramSchmidtFrame CleanNorms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        for (var i = 0; i < _directionNormsArray.Length; i++)
        {
            if (_directionNormsArray[i].IsNearZero())
                _directionNormsArray[i] = 0d;
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetDirection(int index)
    {
        return _directionNormsArray[index] * UnitDirections[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaFloat64Vector> GetDirections()
    {
        return _directionNormsArray.Select(
            (t, i) => t * UnitDirections[i]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetCurvature(int index)
    {
        return _directionNormsArray[index + 1] / _directionNormsArray[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<double> GetCurvatures()
    {
        for (var i = 0; i < _directionNormsArray.Length - 1; i++)
            yield return _directionNormsArray[i + 1] / _directionNormsArray[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector GetDarbouxBlade(int index)
    {
        var k = GetCurvature(index);
        var e1 = UnitDirections[index];
        var e2 = UnitDirections[index + 1];

        return k * e1.Op(e2).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaFloat64Bivector> GetDarbouxBlades()
    {
        for (var i = 0; i < _directionNormsArray.Length - 1; i++)
            yield return GetDarbouxBlade(i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector GetDarbouxBivector()
    {
        var metric = RGaFloat64Processor.Euclidean;

        return GetDarbouxBlades().Aggregate(
            metric.BivectorZero,
            (a, b) => a + b
        );
    }
}
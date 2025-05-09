using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Frames;

public class RGaGramSchmidtFrameFloat64
{
    public static RGaGramSchmidtFrameFloat64 Create(params RGaVector<double>[] vArray)
    {
        var processor = vArray[0].Processor;

        var n = Math.Max(
            vArray.Length,
            vArray.GetVSpaceDimensions()
        );

        var array = new double[n, n];

        for (var c = 0; c < vArray.Length; c++)
        {
            var v = vArray[c];

            foreach (var (r, s) in v.IdScalarPairs)
            {
                array[r.FirstOneBitPosition(), c] = s;
            }
        }

        var matrix = Matrix<double>.Build.DenseOfArray(array);

        var qr = matrix.QR();

        var directionNorms = new double[n];
        var unitDirections = new RGaVector<double>[n];

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
            unitDirections[c] = processor.Vector(unitDirection);
        }
        
        return new RGaGramSchmidtFrameFloat64(directionNorms, unitDirections);
    }


        
    public RGaProcessor<double> Processor 
        => UnitDirections[0].Processor;

    public RGaMetric Metric 
        => UnitDirections[0].Metric;

    public IScalarProcessor<double> ScalarProcessor
        => UnitDirections[0].ScalarProcessor;

    private readonly double[] _directionNormsArray;
    public IReadOnlyList<double> DirectionNorms 
        => _directionNormsArray;

    public IReadOnlyList<RGaVector<double>> UnitDirections { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaGramSchmidtFrameFloat64(double[] directionNorms, IReadOnlyList<RGaVector<double>> unitDirections)
    {
        _directionNormsArray = directionNorms;
        UnitDirections = unitDirections;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGramSchmidtFrameFloat64 CleanNorms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        for (var i = 0; i < _directionNormsArray.Length; i++)
        {
            if (_directionNormsArray[i].IsNearZero())
                _directionNormsArray[i] = 0d;
        }

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<double> GetDirection(int index)
    {
        return _directionNormsArray[index] * UnitDirections[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaVector<double>> GetDirections()
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
    public RGaBivector<double> GetDarbouxBlade(int index)
    {
        var k = GetCurvature(index);
        var e1 = UnitDirections[index];
        var e2 = UnitDirections[index + 1];

        return k * e1.Op(e2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaBivector<double>> GetDarbouxBlades()
    {
        for (var i = 0; i < _directionNormsArray.Length - 1; i++)
            yield return GetDarbouxBlade(i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<double> GetDarbouxBivector()
    {
        return GetDarbouxBlades().Aggregate(
            Processor.BivectorZero,
            (a, b) => a + b
        );
    }
}
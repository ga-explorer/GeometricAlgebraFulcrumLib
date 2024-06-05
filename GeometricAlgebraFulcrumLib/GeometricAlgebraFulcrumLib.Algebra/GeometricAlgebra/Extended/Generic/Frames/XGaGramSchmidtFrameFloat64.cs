using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;

public class XGaGramSchmidtFrameFloat64
{
    public static XGaGramSchmidtFrameFloat64 Create(params XGaVector<double>[] vArray)
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
                array[r.FirstIndex, c] = s;
            }
        }

        var matrix = Matrix<double>.Build.DenseOfArray(array);

        var qr = matrix.QR();

        var directionNorms = new double[n];
        var unitDirections = new XGaVector<double>[n];

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
        
        return new XGaGramSchmidtFrameFloat64(directionNorms, unitDirections);
    }


    public XGaProcessor<double> Processor
        => UnitDirections[0].Processor;

    public XGaMetric Metric 
        => UnitDirections[0].Metric;

    public IScalarProcessor<double> ScalarProcessor
        => UnitDirections[0].ScalarProcessor;

    private readonly double[] _directionNormsArray;
    public IReadOnlyList<double> DirectionNorms 
        => _directionNormsArray;

    public IReadOnlyList<XGaVector<double>> UnitDirections { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaGramSchmidtFrameFloat64(double[] directionNorms, IReadOnlyList<XGaVector<double>> unitDirections)
    {
        _directionNormsArray = directionNorms;
        UnitDirections = unitDirections;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGramSchmidtFrameFloat64 CleanNorms(double epsilon = 1e-12)
    {
        for (var i = 0; i < _directionNormsArray.Length; i++)
        {
            if (_directionNormsArray[i].IsNearZero())
                _directionNormsArray[i] = 0d;
        }

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<double> GetDirection(int index)
    {
        return _directionNormsArray[index] * UnitDirections[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaVector<double>> GetDirections()
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
    public XGaBivector<double> GetDarbouxBlade(int index)
    {
        var k = GetCurvature(index);
        var e1 = UnitDirections[index];
        var e2 = UnitDirections[index + 1];

        return k * e1.Op(e2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaBivector<double>> GetDarbouxBlades()
    {
        for (var i = 0; i < _directionNormsArray.Length - 1; i++)
            yield return GetDarbouxBlade(i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<double> GetDarbouxBivector()
    {
        return GetDarbouxBlades().Aggregate(
            Processor.BivectorZero,
            (a, b) => a + b
        );
    }
}
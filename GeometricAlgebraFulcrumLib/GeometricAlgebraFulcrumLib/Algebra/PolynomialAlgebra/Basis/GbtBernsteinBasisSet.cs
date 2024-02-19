using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;

/// <summary>
/// Defines a set of Generalized Blended Trigonometric Bernstein Basis
/// polynomials.
/// See paper "Geometric modeling and applications of generalized
/// blended trigonometric Bézier curves with shape parameters"
/// https://link.springer.com/article/10.1186/s13662-020-03001-4
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class GbtBernsteinBasisSet<T> :
    GbBernsteinBasisSetBase<T>
{
    private T _alpha;
    public T Alpha
    {
        get => _alpha;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            _alpha = value;
        }
    }

    private T _beta;
    public T Beta
    {
        get => _beta;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            _beta = value;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GbtBernsteinBasisSet(IScalarProcessor<T> scalarProcessor, int degree)
        : base(scalarProcessor, degree)
    {
        _alpha = scalarProcessor.ScalarZero;
        _beta = scalarProcessor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetValueDegree20(T parameterValue)
    {
        var sin = ScalarProcessor.Sin(
            ScalarProcessor.Times(ScalarProcessor.ScalarPiOver2, parameterValue)
        );

        return ScalarProcessor.Times(
            ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, sin), 
            ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, ScalarProcessor.Times(_alpha, sin))
        ).CreateScalar(ScalarProcessor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetValueDegree22(T parameterValue)
    {
        var cos = ScalarProcessor.Cos(
            ScalarProcessor.Times(ScalarProcessor.ScalarPiOver2, parameterValue)
        );

        return ScalarProcessor.Times(
            ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, cos), 
            ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, ScalarProcessor.Times(_beta, cos))
        ).CreateScalar(ScalarProcessor);
    }
}
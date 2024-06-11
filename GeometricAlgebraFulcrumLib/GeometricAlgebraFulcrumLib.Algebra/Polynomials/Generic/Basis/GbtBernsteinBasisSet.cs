using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.Basis;

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
        _alpha = scalarProcessor.ZeroValue;
        _beta = scalarProcessor.ZeroValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetValueDegree20(T parameterValue)
    {
        var sin = (ScalarProcessor.PiOver2 * parameterValue).Sin();

        return (ScalarProcessor.One - sin) * (ScalarProcessor.One - _alpha * sin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetValueDegree22(T parameterValue)
    {
        var cos = (ScalarProcessor.PiOver2 * parameterValue).Cos();

        return (ScalarProcessor.One - cos) * (ScalarProcessor.One - _beta * cos);
    }
}
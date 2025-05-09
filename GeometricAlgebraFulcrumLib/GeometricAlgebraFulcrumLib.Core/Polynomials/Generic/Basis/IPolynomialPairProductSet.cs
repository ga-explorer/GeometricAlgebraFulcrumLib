using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Generic.Basis;

/// <summary>
/// This interface defines a set of basis polynomials of arbitrary scalar type T
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPolynomialPairProductSet<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    /// <summary>
    /// The degree of the polynomials in this basis set
    /// </summary>
    public int Degree { get; }

    /// <summary>
    /// Compute the value of basis polynomial 'index' at
    /// 'parameterValue'
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    Scalar<T> GetValue(int index1, int index2, T parameterValue);

    /// <summary>
    /// Compute the value of basis polynomial 'index' at
    /// 'parameterValue' scaled by 'termScalar'
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="parameterValue"></param>
    /// <param name="termScalar"></param>
    /// <returns></returns>
    Scalar<T> GetValue(int index1, int index2, T parameterValue, T termScalar);

    /// <summary>
    /// Compute a linear combination of the basis polynomials with
    /// coefficients given in 'termScalarsList'
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <param name="termScalarsList"></param>
    /// <returns></returns>
    Scalar<T> GetValue(T parameterValue, T[,] termScalarsList);

    /// <summary>
    /// Compute the values of basis polynomials at 'parameterValue'
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    T[,] GetValues(T parameterValue);
}
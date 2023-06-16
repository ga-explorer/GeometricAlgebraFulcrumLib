using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public interface IPolynomialPairProductIntegralSet<T> :
        IPolynomialPairProductSet<T>
    {
        /// <summary>
        /// Compute the value of basis polynomial 'index1', 'index2' at parameterValue = 1
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        Scalar<T> GetValueAt1(int index1, int index2);

        /// <summary>
        /// Compute the value of polynomial 'index1', 'index2' at parameterValue = 1
        /// scaled by 'termScalar'
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="termScalar"></param>
        /// <returns></returns>
        Scalar<T> GetValueAt1(int index1, int index2, T termScalar);

        /// <summary>
        /// Compute a linear combination of the polynomials in this set with
        /// coefficients given in 'termScalarsList' at parameterValue = 1
        /// </summary>
        /// <param name="termScalarsList"></param>
        /// <returns></returns>
        Scalar<T> GetValueAt1(T[,] termScalarsList);

        /// <summary>
        /// Compute the values of polynomials in this set at parameterValue = 1
        /// </summary>
        /// <returns></returns>
        T[,] GetValuesAt1();
    }
}
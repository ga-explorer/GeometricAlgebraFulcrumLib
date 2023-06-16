namespace GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.CurveBasis
{
    public interface IPolynomialPairProductIntegralSet :
        IPolynomialPairProductSet
    {
        /// <summary>
        /// Compute the value of basis polynomial 'index1', 'index2' at parameterValue = 1
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        double GetValueAt1(int index1, int index2);

        /// <summary>
        /// Compute the value of polynomial 'index1', 'index2' at parameterValue = 1
        /// scaled by 'termScalar'
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="termScalar"></param>
        /// <returns></returns>
        double GetValueAt1(int index1, int index2, double termScalar);

        /// <summary>
        /// Compute a linear combination of the polynomials in this set with
        /// coefficients given in 'termScalarsList' at parameterValue = 1
        /// </summary>
        /// <param name="termScalarsList"></param>
        /// <returns></returns>
        double GetValueAt1(double[,] termScalarsList);

        /// <summary>
        /// Compute the values of polynomials in this set at parameterValue = 1
        /// </summary>
        /// <returns></returns>
        double[,] GetValuesAt1();
    }
}
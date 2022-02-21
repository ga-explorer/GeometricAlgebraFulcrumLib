namespace NumericalGeometryLib.Polynomials.CurveBasis
{
    /// <summary>
    /// This interface defines a set of basis polynomials of arbitrary scalar type double
    /// </summary>
    public interface IPolynomialPairProductSet
    {
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
        double GetValue(int index1, int index2, double parameterValue);

        /// <summary>
        /// Compute the value of basis polynomial 'index' at
        /// 'parameterValue' scaled by 'termScalar'
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="parameterValue"></param>
        /// <param name="termScalar"></param>
        /// <returns></returns>
        double GetValue(int index1, int index2, double parameterValue, double termScalar);

        /// <summary>
        /// Compute a linear combination of the basis polynomials with
        /// coefficients given in 'termScalarsList'
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <param name="termScalarsList"></param>
        /// <returns></returns>
        double GetValue(double parameterValue, double[,] termScalarsList);

        /// <summary>
        /// Compute the values of basis polynomials at 'parameterValue'
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        double[,] GetValues(double parameterValue);
    }
}
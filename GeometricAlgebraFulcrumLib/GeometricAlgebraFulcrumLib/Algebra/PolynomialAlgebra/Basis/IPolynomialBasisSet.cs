using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public interface IPolynomialBasisSet<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Degree { get; }

        T GetValue(int index, T parameterValue);

        T GetValue(int index, T parameterValue, T termScalar);

        T GetValue(T parameterValue, params T[] termScalarsList);

        IReadOnlyList<T> GetValues(T parameterValue);
    }
}
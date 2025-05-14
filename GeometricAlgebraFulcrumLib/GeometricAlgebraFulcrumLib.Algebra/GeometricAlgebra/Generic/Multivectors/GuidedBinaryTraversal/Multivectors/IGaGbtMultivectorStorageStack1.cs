using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



public interface IXGaGbtMultivectorStorageStack1<T> : 
    IXGaGbtStack1<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    XGaMultivector<T> Multivector { get; }
}
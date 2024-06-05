using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



public interface IRGaGbtMultivectorStorageStack1<T> : 
    IRGaGbtStack1<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    RGaMultivector<T> Multivector { get; }
}
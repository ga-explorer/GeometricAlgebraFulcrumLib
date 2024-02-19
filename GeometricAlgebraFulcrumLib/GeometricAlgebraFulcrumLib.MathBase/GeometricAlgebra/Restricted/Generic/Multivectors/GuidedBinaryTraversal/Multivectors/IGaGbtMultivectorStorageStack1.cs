using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



public interface IRGaGbtMultivectorStorageStack1<T> : 
    IRGaGbtStack1<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    RGaMultivector<T> Multivector { get; }
}
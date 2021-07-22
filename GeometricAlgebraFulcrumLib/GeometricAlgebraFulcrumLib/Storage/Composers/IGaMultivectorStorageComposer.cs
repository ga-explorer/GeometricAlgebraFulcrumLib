using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaMultivectorStorageComposer<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        IGasMultivector<T> GetCompactMultivector();

        IGasGradedMultivector<T> GetCompactGradedMultivector();

        IGasMultivector<T> GetMultivectorCopy();
        
        IGasMultivector<T> GetMultivectorCopy(Func<T, T> scalarMapping);

        IGasGradedMultivector<T> GetGradedMultivectorCopy();

        IGasTermsMultivector<T> GetTermsMultivectorCopy();

        GasTreeMultivector<T> GetTreeMultivectorCopy();
    }
}
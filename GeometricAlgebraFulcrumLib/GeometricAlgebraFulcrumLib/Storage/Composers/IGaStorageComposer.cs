using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaStorageComposer<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        GaStorageScalar<T> GetScalar();

        GaStorageVector<T> GetVector();

        GaStorageVector<T> GetVector(bool copyFlag);

        GaStorageBivector<T> GetBivector();

        GaStorageBivector<T> GetBivector(bool copyFlag);

        IGaStorageKVector<T> GetKVector(uint grade);

        IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag);

        IGaStorageMultivector<T> GetMultivector();

        IGaStorageMultivector<T> GetMultivector(bool copyFlag);

        IGaStorageMultivectorGraded<T> GetGradedMultivector();

        IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag);

        GaStorageMultivectorSparse<T> GetSparseMultivector();

        GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag);

        GaStorageMultivectorSparse<T> GetTreeMultivector();

        GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag);

        GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth);

        GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag);
    }
}
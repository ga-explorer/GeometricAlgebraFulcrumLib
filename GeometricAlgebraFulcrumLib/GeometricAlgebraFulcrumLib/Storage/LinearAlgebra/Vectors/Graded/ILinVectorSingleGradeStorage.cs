namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public interface ILinVectorSingleGradeStorage<T> :
        ILinVectorGradedStorage<T>
    {
        uint Grade { get; }
        
        ILinVectorStorage<T> VectorStorage { get; }
    }
}
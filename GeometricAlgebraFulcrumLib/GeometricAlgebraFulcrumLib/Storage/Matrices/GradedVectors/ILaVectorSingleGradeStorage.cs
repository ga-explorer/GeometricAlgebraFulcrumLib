using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public interface ILaVectorSingleGradeStorage<T> :
        ILaVectorGradedStorage<T>
    {
        uint Grade { get; }
        
        ILaVectorEvenStorage<T> EvenStorage { get; }
    }
}
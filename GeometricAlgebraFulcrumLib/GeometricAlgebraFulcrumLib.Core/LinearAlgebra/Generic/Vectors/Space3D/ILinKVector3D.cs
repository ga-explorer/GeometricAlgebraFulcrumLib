namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinKVector3D<T> :
    ILinMultivector3D<T>
{
    int Grade { get; }
}
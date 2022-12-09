using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;

public sealed class LinVectorToVectorRotation<T> :
    ILinUnilinearMap<T>
{
    public IScalarAlgebraProcessor<T> ScalarProcessor 
        => SourceVector.LinearProcessor;

    public ILinearAlgebraProcessor<T> LinearProcessor 
        => SourceVector.LinearProcessor;

    public LinVector<T> SourceVector { get; }

    public LinVector<T> TargetOrthogonalVector { get; }

    public LinVector<T> TargetVector { get; }
    
    public Scalar<T> AngleCos { get; }

    public Scalar<T> Angle
        => AngleCos.ArcCos();


    public LinVectorToVectorRotation(LinVector<T> u, LinVector<T> v)
    {
        SourceVector = u;
        TargetVector = v;

        AngleCos = TargetVector.InnerProduct(SourceVector).CreateScalar(ScalarProcessor);

        Debug.Assert(
            !(AngleCos + 1d).IsNearZero()
        );

        TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
    }


    public ILinUnilinearMap<T> GetLinAdjoint()
    {
        throw new System.NotImplementedException();
    }

    public ILinVectorStorage<T> LinMapBasisVector(ulong index)
    {
        throw new System.NotImplementedException();
    }

    public LinVector<T> LinMapVector(LinVector<T> vector)
    {
        var r = vector.InnerProduct(TargetOrthogonalVector);
        var s = vector.InnerProduct(SourceVector);

        return vector - (r + s) * SourceVector - (r - s) * TargetVector;
    }

    public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
    {
        throw new System.NotImplementedException();
    }

    public ILinMatrixStorage<T> GetLinMappingMatrix()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
    {
        throw new System.NotImplementedException();
    }
}
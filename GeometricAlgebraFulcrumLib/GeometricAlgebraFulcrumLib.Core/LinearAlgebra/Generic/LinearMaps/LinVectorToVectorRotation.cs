using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;

public sealed class LinVectorToVectorRotation<T>
{
    public IScalarProcessor<T> ScalarProcessor
        => SourceVector.ScalarProcessor;

    public LinVector<T> SourceVector { get; }

    public LinVector<T> TargetOrthogonalVector { get; }

    public LinVector<T> TargetVector { get; }

    public Scalar<T> AngleCos { get; }

    public LinAngle<T> Angle
        => AngleCos.ArcCos();


    public LinVectorToVectorRotation(LinVector<T> u, LinVector<T> v)
    {
        SourceVector = u;
        TargetVector = v;

        AngleCos = TargetVector.ESp(SourceVector);

        Debug.Assert(
            !(AngleCos + 1d).IsNearZero()
        );

        TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
    }


    public LinUnilinearMap<T> GetAdjoint()
    {
        throw new NotImplementedException();
    }

    public LinVector<T> MapBasisVector(ulong index)
    {
        throw new NotImplementedException();
    }

    public LinVector<T> LinMapVector(LinVector<T> vector)
    {
        var r = vector.ESp(TargetOrthogonalVector);
        var s = vector.ESp(SourceVector);

        return vector - (r + s) * SourceVector - (r - s) * TargetVector;
    }
        
}
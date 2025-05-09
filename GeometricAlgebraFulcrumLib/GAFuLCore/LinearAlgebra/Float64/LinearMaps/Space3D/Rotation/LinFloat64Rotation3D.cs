using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;

public abstract class LinFloat64Rotation3D :
    LinFloat64ReflectionBase3D
{
    public override bool SwapsHandedness
        => false;

    public abstract LinFloat64Quaternion GetQuaternion();

    public abstract LinFloat64Rotation3D GetInverseRotation();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
    {
        return GetInverseRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64RotationComposer3D ToRotationComposer()
    {
        return LinFloat64RotationComposer3D.CreateFromRotation(this);
    }
}
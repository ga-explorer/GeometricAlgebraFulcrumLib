using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation;

public abstract class LinFloat64Rotation :
    LinFloat64ReflectionBase
{
    public override bool SwapsHandedness
        => false;

    public abstract LinFloat64Rotation GetInverseRotation();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return GetInverseRotation();
    }

    public abstract LinFloat64PlanarRotationSequence ToVectorToVectorRotationSequence();

    //public abstract SimpleRotationSequence ToSimpleVectorRotationSequence();
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public abstract class LinFloat64RotationBase4D :
    LinFloat64ReflectionBase4D
{
    public override bool SwapsHandedness
        => false;

    public abstract LinFloat64RotationBase4D GetVectorRotationInverse();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return GetVectorRotationInverse();
    }

    public abstract LinFloat64VectorToVectorRotationSequence4D ToVectorToVectorRotationSequence();

    //public abstract SimpleRotationSequence ToSimpleVectorRotationSequence();
}
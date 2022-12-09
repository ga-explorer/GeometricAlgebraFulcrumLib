using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public abstract class SimpleRotationLinearMap :
    RotationLinearMap
{
    public abstract SimpleRotationLinearMap GetSimpleVectorRotationInverse();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RotationLinearMap GetVectorRotationInverse()
    {
        return GetSimpleVectorRotationInverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
    {
        if (this is IdentityLinearMap)
            return VectorToVectorRotationSequence.Create(Dimensions);

        if (this is VectorToVectorRotationLinearMap r1)
            return VectorToVectorRotationSequence.Create(r1);

        return VectorToVectorRotationSequence.CreateFromRotationMatrix(
            GetMatrix()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
    //{
    //    var rotation = SimpleRotationSequence.Create(Dimensions);

    //    rotation.AppendRotation(this);

    //    return rotation;
    //}
}
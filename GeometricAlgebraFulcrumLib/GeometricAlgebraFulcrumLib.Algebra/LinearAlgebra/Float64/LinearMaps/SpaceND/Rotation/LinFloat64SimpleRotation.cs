using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public abstract class LinFloat64SimpleRotation :
    LinFloat64Rotation
{
    public abstract LinFloat64SimpleRotation GetInverseSimpleRotation();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation GetInverseRotation()
    {
        return GetInverseSimpleRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotationSequence ToVectorToVectorRotationSequence()
    {
        if (this is LinFloat64IdentityLinearMap)
            return LinFloat64PlanarRotationSequence.Create(VSpaceDimensions);

        if (this is LinFloat64PlanarRotation r1)
            return LinFloat64PlanarRotationSequence.Create(r1);

        return LinFloat64PlanarRotationSequence.CreateFromRotationMatrix(
            ToMatrix(VSpaceDimensions, VSpaceDimensions)
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
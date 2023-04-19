using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public abstract class LinFloat64SimpleRotationBase :
        LinFloat64RotationBase
    {
        public abstract LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse();
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64RotationBase GetVectorRotationInverse()
        {
            return GetSimpleVectorRotationInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
        {
            if (this is LinFloat64IdentityLinearMap)
                return LinFloat64VectorToVectorRotationSequence.Create(VSpaceDimensions);

            if (this is LinFloat64VectorToVectorRotationBase r1)
                return LinFloat64VectorToVectorRotationSequence.Create(r1);

            return LinFloat64VectorToVectorRotationSequence.CreateFromRotationMatrix(
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
}
namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public abstract class LinFloat64SimpleRotationBase4D :
    LinFloat64RotationBase4D
{
    public abstract LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse();

    
    public override LinFloat64RotationBase4D GetVectorRotationInverse()
    {
        return GetSimpleVectorRotationInverse();
    }

    
    public override LinFloat64VectorToVectorRotationSequence4D ToVectorToVectorRotationSequence()
    {
        if (this is LinFloat64IdentityLinearMap4D)
            return LinFloat64VectorToVectorRotationSequence4D.Create();

        if (this is LinFloat64VectorToVectorRotationBase4D r1)
            return LinFloat64VectorToVectorRotationSequence4D.Create(r1);

        return LinFloat64VectorToVectorRotationSequence4D.CreateFromRotationMatrix(
            this.ToSquareMatrix3()
        );
    }

    //
    //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
    //{
    //    var rotation = SimpleRotationSequence.Create(Dimensions);

    //    rotation.AppendRotation(this);

    //    return rotation;
    //}
}
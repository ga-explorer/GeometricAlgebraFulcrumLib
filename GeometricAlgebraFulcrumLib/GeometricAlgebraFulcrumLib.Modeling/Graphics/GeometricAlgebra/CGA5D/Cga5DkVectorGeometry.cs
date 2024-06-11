namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.GeometricAlgebra.CGA5D;

public abstract class Cga5DkVectorGeometry 
    : Cga5DMultivectorGeometry
{
    public abstract int Grade { get; }


    protected Cga5DkVectorGeometry(double[] scalars, MultivectorNullSpaceKind nullSpaceKind)
        : base(scalars, nullSpaceKind)
    {
    }
}
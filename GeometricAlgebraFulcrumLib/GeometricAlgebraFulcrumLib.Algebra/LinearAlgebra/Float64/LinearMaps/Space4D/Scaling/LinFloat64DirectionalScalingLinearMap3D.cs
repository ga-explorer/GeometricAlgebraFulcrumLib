using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;

/// <summary>
/// This class represents the most basic kind of linear operations:
/// Scaling by a factor in a given direction.
/// </summary>
public abstract class LinFloat64DirectionalScalingLinearMap4D :
    ILinFloat64DirectionalScalingLinearMap4D
{
    public int VSpaceDimensions
        => 3;

    public bool SwapsHandedness
        => ScalingFactor < 0;

    public abstract double ScalingFactor { get; }

    public abstract LinFloat64Vector4D ScalingVector { get; }


    public abstract bool IsValid();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return ScalingFactor.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double epsilon = 1E-12)
    {
        return ScalingFactor.IsNearOne(epsilon);
    }

    public abstract LinFloat64Vector4D MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector);

    public abstract ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse();

    public abstract LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return GetDirectionalScalingInverse();
    }
}
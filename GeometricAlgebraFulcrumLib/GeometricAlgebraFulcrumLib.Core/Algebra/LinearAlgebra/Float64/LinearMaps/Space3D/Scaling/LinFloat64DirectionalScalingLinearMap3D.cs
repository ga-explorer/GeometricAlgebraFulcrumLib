using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;

/// <summary>
/// This class represents the most basic kind of linear operations:
/// Scaling by a factor in a given direction.
/// </summary>
public abstract class LinFloat64DirectionalScalingLinearMap3D :
    ILinFloat64DirectionalScalingLinearMap3D
{
    public int VSpaceDimensions
        => 3;

    public bool SwapsHandedness
        => ScalingFactor < 0;

    public abstract double ScalingFactor { get; }

    public abstract LinFloat64Vector3D ScalingVector { get; }


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

    public abstract LinFloat64Vector3D MapBasisVector(int basisIndex);

    public abstract LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector);

    public abstract ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse();

    public abstract LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return GetDirectionalScalingInverse();
    }
}
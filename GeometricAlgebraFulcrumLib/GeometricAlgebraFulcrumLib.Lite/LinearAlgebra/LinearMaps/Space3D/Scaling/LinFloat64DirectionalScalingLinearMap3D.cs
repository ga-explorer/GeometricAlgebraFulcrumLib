using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Scaling;

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

    public abstract Float64Vector3D ScalingVector { get; }

    
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

    public abstract Float64Vector3D MapBasisVector(int basisIndex);

    public abstract Float64Vector3D MapVector(IFloat64Vector3D vector);
        
    public abstract ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse();

    public abstract LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return GetDirectionalScalingInverse();
    }
}
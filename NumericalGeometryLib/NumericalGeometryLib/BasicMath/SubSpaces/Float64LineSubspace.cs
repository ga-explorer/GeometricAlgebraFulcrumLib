using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.SubSpaces;

public sealed class Float64LineSubspace :
    IFloat64Subspace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSubspace CreateFromVector(Float64Tuple vector)
    {
        var length = 
            vector.GetVectorNorm();
        
        var unitVector =
            length.IsNearOne() ? vector : vector / length;

        return new Float64LineSubspace(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSubspace CreateFromUnitVector(Float64Tuple vector)
    {
        return new Float64LineSubspace(vector);
    }


    public int Dimensions 
        => BasisVector.Dimensions;

    public int SubspaceDimensions 
        => 1;
    
    public Float64Tuple BasisVector { get; }

    public IEnumerable<Float64Tuple> BasisVectors
    {
        get
        {
            yield return BasisVector;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64LineSubspace(Float64Tuple vector)
    {
        Debug.Assert(
            vector.IsNearUnit()
        );

        BasisVector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(Float64Tuple vector, double epsilon = 1E-12)
    {
        return vector.IsNearZero(epsilon) || 
               vector.IsNearParallelToUnit(BasisVector, epsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.Dimensions <= Dimensions && 
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }
}
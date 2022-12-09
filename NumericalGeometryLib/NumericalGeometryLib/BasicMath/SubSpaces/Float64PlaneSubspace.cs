using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.SubSpaces;

public sealed class Float64PlaneSubspace :
    IFloat64Subspace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlaneSubspace CreateFromVectors(Float64Tuple vector1, Float64Tuple vector2)
    {
        var u = vector1.ToUnitVector();
        var v = vector2.RejectOnUnitVector(u).InPlaceNormalize();
        
        return new Float64PlaneSubspace(u, v);
        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlaneSubspace CreateFromUnitVectors(Float64Tuple vector1, Float64Tuple vector2)
    {
        return new Float64PlaneSubspace(
            vector1, 
            vector2.RejectOnUnitVector(vector1).InPlaceNormalize()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlaneSubspace CreateFromOrthogonalVectors(Float64Tuple vector1, Float64Tuple vector2)
    {
        return new Float64PlaneSubspace(
            vector1.ToUnitVector(), 
            vector2.ToUnitVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PlaneSubspace CreateFromOrthonormalVectors(Float64Tuple vector1, Float64Tuple vector2)
    {
        return new Float64PlaneSubspace(
            vector1, 
            vector2
        );
    }


    public int Dimensions 
        => BasisVector1.Dimensions;

    public int SubspaceDimensions 
        => 2;

    public IEnumerable<Float64Tuple> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    public Float64Tuple BasisVector1 { get; }

    public Float64Tuple BasisVector2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64PlaneSubspace(Float64Tuple vector1, Float64Tuple vector2)
    {
        Debug.Assert(
            vector1.IsNearOrthonormalWith(vector2)
        );

        BasisVector1 = vector1;
        BasisVector2 = vector2;
    }


    public bool NearContains(Float64Tuple vector, double epsilon = 1e-12)
    {
        if (vector.IsNearZero(epsilon)) 
            return true;

        // Project vector on subspace plane and compare with original vector
        var x = vector.ScalarArray;
        var u = BasisVector1.ScalarArray;
        var v = BasisVector2.ScalarArray;

        var (xuDot, xvDot) = x.VectorDot(u, v);

        var diffNorm = 0d;

        for (var i = 0; i < Dimensions; i++)
        {
            diffNorm += (x[i] - (xuDot * u[i] + xvDot * v[i])).Square();

            if (diffNorm > epsilon)
                return false;
        }

        return true;

        //var rank = Matrix<double>.Build.DenseOfColumnArrays(
        //    vector.ScalarArray,
        //    BasisVector1.ScalarArray,
        //    BasisVector2.ScalarArray
        //).Rank();

        //Debug.Assert(
        //    rank is 2 or 3
        //);

        //return rank == 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.Dimensions <= Dimensions && 
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;

public sealed record LinFloat64NullSubspace4D :
    ILinFloat64Subspace4D
{
    public static LinFloat64NullSubspace4D Instance { get; }
        = new LinFloat64NullSubspace4D();


    public int VSpaceDimensions
        => 4;

    public int SubspaceDimensions
        => 0;

    public IEnumerable<LinFloat64Vector4D> BasisVectors
        => [];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64NullSubspace4D()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Vector4D vector, double tolerance = 1E-12D)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace4D subspace, double tolerance = Float64Utils.DefaultTolerance)
    {
        return subspace.SubspaceDimensions == 0;
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

public sealed class LinFloat64Bivector :
    IFloat64LinearAlgebraElement
{
    public LinFloat64Vector Vector1 { get; }

    public LinFloat64Vector Vector2 { get; }

    public int VSpaceDimensions { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector(LinFloat64Vector v1, LinFloat64Vector v2)
    {
        Vector1 = v1;
        Vector2 = v2;

        VSpaceDimensions = Math.Max(v1.VSpaceDimensions, v2.VSpaceDimensions);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Vector1.IsValid() &&
               Vector2.IsValid() &&
               Vector1.ESp(Vector2).IsNearZero();
    }


}
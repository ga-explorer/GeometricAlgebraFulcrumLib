using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class SdfRay3D :
    IAlgebraicElement
{
    public LinFloat64Vector3D Origin { get; }

    public LinFloat64Vector3D Direction { get; }

    public LinFloat64Vector3D DirectionInv { get; }

    public Triplet<int> DirectionInvSign { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfRay3D(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        Origin = origin.ToLinVector3D();
        Direction = direction.ToLinVector3D();

        DirectionInv = LinFloat64Vector3D.Create(1d / direction.X,
            1d / direction.Y,
            1d / direction.Z);

        DirectionInvSign = new Triplet<int>(
            DirectionInv.X.IsNegative() ? 1 : 0,
            DirectionInv.Y.IsNegative() ? 1 : 0,
            DirectionInv.Z.IsNegative() ? 1 : 0
        );

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double t)
    {
        return LinFloat64Vector3D.Create(Origin.X + t * Direction.X,
            Origin.Y + t * Direction.Y,
            Origin.Z + t * Direction.Z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Origin.IsValid() && Direction.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersect(IFloat64BoundingBox3D box, out double tMin, out double tMax)
    {
        var bounds = new LinFloat64Vector3D[]
        {
            LinFloat64Vector3D.Create(box.MinX, box.MinY, box.MinZ),
            LinFloat64Vector3D.Create(box.MinX, box.MinY, box.MinZ)
        };

        tMin = (bounds[DirectionInvSign.Item1].X - Origin.X) * DirectionInv.X; 
        tMax = (bounds[1 - DirectionInvSign.Item1].X - Origin.X) * DirectionInv.X; 

        var tYMin = (bounds[DirectionInvSign.Item2].Y - Origin.Y) * DirectionInv.Y; 
        var tYMax = (bounds[1 - DirectionInvSign.Item2].Y - Origin.Y) * DirectionInv.Y; 
 
        if (tMin > tYMax || tYMin > tMax) 
            return false;

        if (tYMin > tMin) 
            tMin = tYMin; 

        if (tYMax < tMax) 
            tMax = tYMax; 
 
        var tZMin = (bounds[DirectionInvSign.Item3].Z - Origin.Z) * DirectionInv.Z; 
        var tZMax = (bounds[1 - DirectionInvSign.Item3].Z - Origin.Z) * DirectionInv.Z; 
 
        if (tMin > tZMax || tZMin > tMax) 
            return false; 

        if (tZMin > tMin) 
            tMin = tZMin; 

        if (tZMax < tMax) 
            tMax = tZMax; 
 
        return true; 
    }
}
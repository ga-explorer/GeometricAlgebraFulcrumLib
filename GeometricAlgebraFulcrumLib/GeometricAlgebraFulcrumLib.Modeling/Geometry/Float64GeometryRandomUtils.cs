using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;

using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public static class Float64GeometryRandomUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointOnCircle(this Random randomGenerator, LinFloat64Vector2D center, double radius)
    {
        var (cos, sin) = randomGenerator.GetPolarAngle();

        return LinFloat64Vector2D.Create(
            center.X + radius * cos, 
            center.Y + radius * sin
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointOnSphere(this Random randomGenerator, LinFloat64Vector3D center, double radius)
    {
        return new LinFloat64SphericalVector3D(
            randomGenerator.GetPolarAngleFromArcRatio(0.5),
            randomGenerator.GetPolarAngle(),
            radius
        ).ToLinVector3D() + center;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInsideCircle(this Random randomGenerator, LinFloat64Vector2D center, double maxRadius)
    {
        var r = randomGenerator.GetFloat64(0, maxRadius.Abs());
        var (cos, sin) = randomGenerator.GetPolarAngle();

        return LinFloat64Vector2D.Create(
            center.X + r * cos, 
            center.Y + r * sin
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInsideSphere(this Random randomGenerator, LinFloat64Vector3D center, double maxRadius)
    {
        return new LinFloat64SphericalVector3D(
            randomGenerator.GetPolarAngleFromArcRatio(0.5),
            randomGenerator.GetPolarAngle(),
            randomGenerator.GetFloat64(0, maxRadius.Abs())
        ).ToLinVector3D() + center;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInsideBox(this Random randomGenerator, Float64BoundingBox2D limitsBoundingBox)
    {
        return LinFloat64Vector2D.Create(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointOnSegment(this Random randomGenerator, IFloat64LineSegment2D lineSegment)
    {
        return lineSegment.GetPointAt(
            randomGenerator.GetFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInsideBox(this Random randomGenerator, Float64BoundingBox3D limitsBoundingBox)
    {
        return LinFloat64Vector3D.Create(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInsideTriangle(this Random randomGenerator, IFloat64Triangle3D triangle)
    {
        return triangle.GetPointAt(
            randomGenerator.GetFloat64(),
            randomGenerator.GetFloat64(),
            randomGenerator.GetFloat64()
        );
    }

    public static IReadOnlyList<LinFloat64Vector2D> GetPointsInsideBox(this Random randomGenerator, Float64BoundingBox2D limitsBoundingBox, int pointsCount)
    {
        var pointsArray = new LinFloat64Vector2D[pointsCount];

        for (var i = 0; i < pointsCount; i++)
            pointsArray[i] = randomGenerator.GetPointInsideBox(limitsBoundingBox);

        return pointsArray;
    }

}
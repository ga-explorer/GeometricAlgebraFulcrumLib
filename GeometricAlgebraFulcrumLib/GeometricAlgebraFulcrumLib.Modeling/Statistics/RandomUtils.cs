using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Modeling.Statistics;

public static class RandomUtils
{
    internal static SortedDictionary<int, double> NormalizeProbabilities(this SortedDictionary<int, double> indexHeightDictionary)
    {
        return ScaleProbabilities(
            indexHeightDictionary,
            1d / indexHeightDictionary.Values.Sum()
        );
    }

    internal static SortedDictionary<int, double> ScaleProbabilities(this SortedDictionary<int, double> indexHeightDictionary, double scalingFactor)
    {
        var indexHeightDictionary1 = new SortedDictionary<int, double>();

        foreach (var (i, p) in indexHeightDictionary) 
            indexHeightDictionary1.Add(i, p * scalingFactor);

        return indexHeightDictionary1;
    }
    
        
    public static LinFloat64Vector3D GetUnitVector3D(this Random randomGenerator)
    {
        var phi = randomGenerator.GetPolarAngle();
        var theta = randomGenerator.GetPolarAngleFromArcRatio(0.5);

        return new LinFloat64SphericalUnitVector3D(theta, phi).ToLinVector3D();
    }


    public static Float64LineSegment2D GetLineSegmentInside(this Random randomGenerator, IFloat64BoundingBox2D limitsBoundingBox)
    {
        return new Float64LineSegment2D(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    public static Float64Triangle2D GetTriangleInside(this Random randomGenerator, IFloat64BoundingBox2D limitsBoundingBox)
    {
        return new Float64Triangle2D(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    public static List<Float64Triangle2D> GetTrianglesInside(this Random randomGenerator, int trianglesCount, IFloat64BoundingBox2D limitsBoundingBox)
    {
        var result = new List<Float64Triangle2D>(trianglesCount);

        for (var i = 0; i < trianglesCount; i++)
            result.Add(
                new Float64Triangle2D(
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
                )
            );

        return result;
    }
}
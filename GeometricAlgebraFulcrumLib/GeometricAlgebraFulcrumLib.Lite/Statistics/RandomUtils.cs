using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Statistics;

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
    
        
    public static Float64Vector3D GetUnitVector3D(this System.Random randomGenerator)
    {
        var phi = randomGenerator.GetAngle();
        var theta = randomGenerator.GetNumber() * Math.PI;

        return new Float64SphericalUnitVector3D(theta, phi).ToVector3D();
    }


    public static LineSegment2D GetLineSegmentInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
    {
        return new LineSegment2D(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    public static Triangle2D GetTriangleInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
    {
        return new Triangle2D(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    public static List<Triangle2D> GetTrianglesInside(this System.Random randomGenerator, int trianglesCount, IBoundingBox2D limitsBoundingBox)
    {
        var result = new List<Triangle2D>(trianglesCount);

        for (var i = 0; i < trianglesCount; i++)
            result.Add(
                new Triangle2D(
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                    randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
                )
            );

        return result;
    }
}
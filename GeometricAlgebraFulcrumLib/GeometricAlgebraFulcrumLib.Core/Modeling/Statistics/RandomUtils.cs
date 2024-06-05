using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Statistics;

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


    public static LineSegment2D GetLineSegmentInside(this Random randomGenerator, IBoundingBox2D limitsBoundingBox)
    {
        return new LineSegment2D(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    public static Triangle2D GetTriangleInside(this Random randomGenerator, IBoundingBox2D limitsBoundingBox)
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

    public static List<Triangle2D> GetTrianglesInside(this Random randomGenerator, int trianglesCount, IBoundingBox2D limitsBoundingBox)
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
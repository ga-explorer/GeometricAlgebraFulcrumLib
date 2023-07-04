using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Random
{
    public static class RandomUtils
    {
        

        
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
}

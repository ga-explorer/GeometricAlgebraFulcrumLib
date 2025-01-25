using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Regular_polygon
    /// </summary>
    public sealed record Float64RegularPolygon2D :
        IReadOnlyList<LinFloat64Vector2D>
    {
        public static Float64RegularPolygon2D CreateFromOuterRadius(int sideCount, double outerRadius = 1)
        {
            return new Float64RegularPolygon2D(sideCount, outerRadius);
        }

        public static Float64RegularPolygon2D CreateFromInnerRadius(int sideCount, double innerRadius = 1)
        {
            var outerRadius = innerRadius / (2 * Math.Sin(Math.PI / sideCount));

            return new Float64RegularPolygon2D(sideCount, outerRadius);
        }

        public static Float64RegularPolygon2D CreateFromSideLength(int sideCount, double sideLength = 1)
        {
            var outerRadius = sideLength / Math.Cos(Math.PI / sideCount);

            return new Float64RegularPolygon2D(sideCount, outerRadius);
        }


        public int Count { get; }

        public double OuterRadius { get; }

        public double InnerRadius
            => OuterRadius * Math.Cos(Math.PI / Count);

        public double SideLength
            => 2 * OuterRadius * Math.Sin(Math.PI / Count);

        public int DiagonalCount
            => Count * (Count - 3) / 2;

        public LinFloat64PolarAngle InteriorAngle
            => ((1d - 2d / Count) * 180).DegreesToPolarAngle();

        public LinFloat64PolarAngle ExteriorAngle
            => ((1d - 1d / Count) * 360).DegreesToPolarAngle();

        public IEnumerable<LinFloat64PolarAngle> CentralAngles
            => Enumerable
                .Range(0, Count)
                .Select(i =>
                    (360d * i / Count).DegreesToPolarAngle()
                );

        public IEnumerable<LinFloat64Vector2D> Vertices
            => CentralAngles.Select(angle =>
                angle.Rotate(
                    LinFloat64Vector2D.Create(OuterRadius, 0)
                )
            );

        public IEnumerable<LinFloat64Vector2D> SideMidPoints
        {
            get
            {
                var v1 = Vertex(0);
                for (var i = 1; i <= Count; i++)
                {
                    var v2 = Vertex(i);

                    yield return (v1 + v2) / 2;

                    v1 = v2;
                }
            }
        }

        public IEnumerable<LinFloat64Vector2D> SideUnitNormals
        {
            get
            {
                var v1 = Vertex(0);
                for (var i = 1; i <= Count; i++)
                {
                    var v2 = Vertex(i);

                    yield return (v1 - v2).GetUnitNormal();

                    v1 = v2;
                }
            }
        }

        public LinFloat64Vector2D this[int index]
            => CentralAngle(index).Rotate(
                LinFloat64Vector2D.Create(OuterRadius, 0)
            );


        private Float64RegularPolygon2D(int sideCount, double outerRadius)
        {
            if (sideCount < 3)
                throw new ArgumentOutOfRangeException(nameof(sideCount));

            if (!outerRadius.IsFinite() || outerRadius < 0)
                throw new ArgumentOutOfRangeException(nameof(outerRadius));

            Count = sideCount;
            OuterRadius = outerRadius;
        }

        public void Deconstruct(out int sideCount, out double outerRadius)
        {
            sideCount = Count;
            outerRadius = OuterRadius;
        }


        public LinFloat64Vector2D Vertex(int index = 1)
        {
            return CentralAngle(index).Rotate(
                LinFloat64Vector2D.Create(OuterRadius, 0)
            );
        }

        public LinFloat64PolarAngle CentralAngle(int index = 1)
        {
            return (360d * index.Mod(Count) / Count).DegreesToPolarAngle();
        }

        public Pair<LinFloat64Vector2D> SideVertexPairs(int index = 1)
        {
            return new Pair<LinFloat64Vector2D>(
                Vertex(index),
                Vertex(index + 1)
            );
        }

        public LinFloat64Vector2D SideMidPoint(int index = 1)
        {
            var (v1, v2) =
                SideVertexPairs(index);

            return (v1 + v2) / 2;
        }

        public LinFloat64Vector2D SideUnitNormal(int index = 1)
        {
            var (v1, v2) =
                SideVertexPairs(index);

            return (v1 - v2).GetUnitNormal();
        }

        public IEnumerator<LinFloat64Vector2D> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}

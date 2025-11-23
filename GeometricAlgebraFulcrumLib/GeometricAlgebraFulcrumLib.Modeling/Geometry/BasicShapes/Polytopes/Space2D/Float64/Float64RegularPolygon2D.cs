using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space2D.Float64
{
    /// <summary>
    /// This class represents a regular convex polygon in 2D centered
    /// at the origin with a given outer radius and number of sides >= 3
    /// https://en.wikipedia.org/wiki/Regular_polygon
    /// </summary>
    public sealed record Float64RegularPolygon2D :
        IReadOnlyList<LinFloat64Vector2D>
    {
        /// <summary>
        /// Regular convex polygon with 3 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Triangle(double outerRadius = 1d)
            => new Float64RegularPolygon2D(3, outerRadius);

        /// <summary>
        /// Regular convex polygon with 4 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Square(double outerRadius = 1d)
            => new Float64RegularPolygon2D(4, outerRadius);

        /// <summary>
        /// Regular convex polygon with 5 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Pentagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(5, outerRadius);

        /// <summary>
        /// Regular convex polygon with 6 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Hexagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(6, outerRadius);

        /// <summary>
        /// Regular convex polygon with 7 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Heptagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(7, outerRadius);

        /// <summary>
        /// Regular convex polygon with 8 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Octagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(8, outerRadius);

        /// <summary>
        /// Regular convex polygon with 9 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Enneagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(9, outerRadius);

        /// <summary>
        /// Regular convex polygon with 10 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Decagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(10, outerRadius);

        /// <summary>
        /// Regular convex polygon with 11 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Hendecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(11, outerRadius);

        /// <summary>
        /// Regular convex polygon with 12 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Dodecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(12, outerRadius);

        /// <summary>
        /// Regular convex polygon with 13 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Tridecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(13, outerRadius);

        /// <summary>
        /// Regular convex polygon with 14 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Tetradecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(14, outerRadius);

        /// <summary>
        /// Regular convex polygon with 15 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Pentadecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(15, outerRadius);

        /// <summary>
        /// Regular convex polygon with 16 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Hexadecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(16, outerRadius);

        /// <summary>
        /// Regular convex polygon with 17 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Heptadecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(17, outerRadius);

        /// <summary>
        /// Regular convex polygon with 18 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Octadecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(18, outerRadius);

        /// <summary>
        /// Regular convex polygon with 19 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Enneadecagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(19, outerRadius);

        /// <summary>
        /// Regular convex polygon with 20 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(20, outerRadius);

        /// <summary>
        /// Regular convex polygon with 21 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaihenagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(21, outerRadius);

        /// <summary>
        /// Regular convex polygon with 22 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns>A regular icosikaidigon</returns>
        public static Float64RegularPolygon2D Icosikaidigon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(22, outerRadius);

        /// <summary>
        /// Regular convex polygon with 23 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaitrigon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(23, outerRadius);

        /// <summary>
        /// Regular convex polygon with 24 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaitetragon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(24, outerRadius);

        /// <summary>
        /// Regular convex polygon with 25 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaipentagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(25, outerRadius);

        /// <summary>
        /// Regular convex polygon with 26 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaihexagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(26, outerRadius);

        /// <summary>
        /// Regular convex polygon with 27 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaiheptagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(27, outerRadius);

        /// <summary>
        /// Regular convex polygon with 28 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaioctagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(28, outerRadius);

        /// <summary>
        /// Regular convex polygon with 29 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Icosikaienneagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(29, outerRadius);

        /// <summary>
        /// Regular convex polygon with 30 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns>A regular triacontagon</returns>
        public static Float64RegularPolygon2D Triacontagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(30, outerRadius);

        /// <summary>
        /// Regular convex polygon with 31 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Triacontakaihenagon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(31, outerRadius);

        /// <summary>
        /// Regular convex polygon with 32 sides
        /// </summary>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        public static Float64RegularPolygon2D Triacontakaidigon(double outerRadius = 1d)
            => new Float64RegularPolygon2D(32, outerRadius);


        /// <summary>
        /// Regular convex polygon with given number of sides and outer radius
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D Convex(int sideCount, double outerRadius = 1)
            => new Float64RegularPolygon2D(sideCount, outerRadius);
        
        /// <summary>
        /// Regular convex polygon with given number of sides and associated similarity transform
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="transform">The associated similarity transform</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D Convex(int sideCount, Float64SimilarityAffineMap2D transform)
            => new Float64RegularPolygon2D(sideCount, transform);
        
        /// <summary>
        /// Create a convex regular n-gon (m = 1) from its inner radius (apothem).
        /// This factory does not apply to star polygons, since they
        /// generally lack a circle tangent to all sides.
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="innerRadius">The radius of the inscribed circle (default is 1.0)</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D ConvexFromInnerRadius(int sideCount, double innerRadius = 1)
            => new Float64RegularPolygon2D(
                sideCount,
                innerRadius / Math.Cos(Math.PI / sideCount)
            );

        /// <summary>
        /// Regular convex polygon with given number of sides and side length
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="sideLength">The length of a polygon side (default is 1.0)</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D ConvexFromSideLength(int sideCount, double sideLength = 1)
            => new Float64RegularPolygon2D(
                sideCount,
                sideLength / (2d * Math.Sin(Math.PI / sideCount))
            );


        /// <summary>
        /// Regular polygon with given number of sides and outer radius
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="starStep"></param>
        /// <param name="outerRadius">The radius of the circumscribed circle (default is 1.0)</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D Create(int sideCount, int starStep, double outerRadius = 1)
            => new Float64RegularPolygon2D(sideCount, starStep, outerRadius);

        /// <summary>
        /// Regular polygon with given number of sides and associated similarity transform
        /// </summary>
        /// <param name="sideCount">Number of sides</param>
        /// <param name="starStep"></param>
        /// <param name="transform">The associated similarity transform</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64RegularPolygon2D Create(int sideCount, int starStep, Float64SimilarityAffineMap2D transform)
            => new Float64RegularPolygon2D(sideCount, starStep, transform);


        public Float64SimilarityAffineMap2D Transform { get; set; } 
            = Float64SimilarityAffineMap2D.Identity;

        /// <summary>
        /// Number of vertices\sides of the polygon
        /// </summary>
        public int Count { get; }
        
        /// <summary>
        /// Integer parameter used for constructing regular stars.
        /// For regular convex polygons this is always 1
        /// </summary>
        public int StarStep { get; }
        
        /// <summary>
        /// Winding density of the star polygon; for {n/m} with gcd(n,m)=1 the density equals m.
        /// </summary>
        public int StarWindingDensity 
            => StarStep;

        /// <summary>
        /// Number of sides
        /// </summary>
        public int SideCount
            => Count;

        /// <summary>
        /// Number of diagonals (for a convex polygon only).
        /// For stars, this returns zero.
        /// </summary>
        public int DiagonalCount
            => IsConvex ? Count * (Count - 3) / 2 : 0;

        /// <summary>
        /// Number of chords (sides and diagonals)
        /// </summary>
        public int ChordCount
            => Count * (Count - 1) / 2;
        
        public bool IsConvex 
            => StarStep == 1;
        
        public bool IsStar 
            => StarStep > 1;
        
        public string SchlafliSymbol 
            => $"{{{Count}/{StarStep}}}";

        /// <summary>
        /// The center of the polygon
        /// </summary>
        public LinFloat64Vector2D Center 
            => Transform.Translate;

        /// <summary>
        /// The radius of the circumscribed circle
        /// </summary>
        public double OuterRadius 
            => Transform.ScalingFactor;

        /// <summary>
        /// Distance from the polygon center to the midpoint of any side.
        /// For convex polygons (m = 1), this is the apothem (radius of the
        /// inscribed circle).
        /// For star polygons (m > 1), this value is still R·cos(π·m/n),
        /// but in general no single circle is tangent to all sides —
        /// so this should not be interpreted as a true inscribed circle radius.
        /// https://en.wikipedia.org/wiki/Apothem
        /// </summary>
        public double InnerRadius
            => OuterRadius * Math.Cos(Math.PI * StarStep / Count);

        /// <summary>
        /// Side length of the polygon.
        /// </summary>
        public double SideLength 
            => 2d * OuterRadius * Math.Sin(Math.PI * StarStep / Count);

        /// <summary>
        /// Perimeter of polygon
        /// </summary>
        public double Perimeter 
            => Count * SideLength;
        
        /// <summary>
        /// Algebraic (density-weighted) area of the regular polygon {n/m}.
        /// For convex polygons this equals the usual enclosed area.
        /// For stars, edges may cross and the boundary winds m times;
        /// this formula returns the signed area consistent with that winding.
        /// </summary>
        public double Area 
            => Count * CentralTriangleArea;

        /// <summary>
        /// Interior angle (angle between any two adjacent sides)
        /// </summary>
        public LinFloat64PolarAngle InteriorAngle
            => (180d * (1d - 2d * StarStep / Count)).DegreesToPolarAngle();
        
        /// <summary>
        /// Sum of interior angles (angles between any two adjacent sides)
        /// </summary>
        public double InteriorAngleSumRadians
            => (Count - 2d * StarStep) * Math.PI;
        
        /// <summary>
        /// Sum of interior angles (angles between any two adjacent sides)
        /// </summary>
        public double InteriorAngleSumDegrees
            => (Count - 2d * StarStep) * 180d;

        /// <summary>
        /// Exterior turning angle at each vertex.
        /// For convex polygons this equals 2π/n (the usual exterior angle).
        /// For star polygons {n/m} it generalizes to 2π·m/n, which is the
        /// per-vertex turning angle as the boundary winds m times around the center.
        /// </summary>
        public LinFloat64PolarAngle ExteriorAngle
            => (360d * StarStep / Count).DegreesToPolarAngle();
        
        /// <summary>
        /// Central angle subtended at the center between two consecutive
        /// connected vertices of {n/m}.
        /// For convex polygons this equals 2π/n; for stars it is 2π·m/n.
        /// </summary>
        public LinFloat64PolarAngle CentralAngle
            => (360d * StarStep / Count).DegreesToPolarAngle();

        /// <summary>
        /// Area of a single central triangle of the polygon
        /// </summary>
        public double CentralTriangleArea 
            => OuterRadius * OuterRadius * Math.Sin(2d * Math.PI * StarStep / Count) / 2d;
        
        /// <summary>
        /// Perimeter of a single central triangle of the polygon
        /// </summary>
        public double CentralTrianglePerimeter 
            => 2d * OuterRadius * (1 + Math.Sin(Math.PI * StarStep / Count));
        

        /// <summary>
        /// All vertex central angles measured from the x-axis with origin
        /// at the center of the polygon
        /// </summary>
        public IEnumerable<LinFloat64PolarAngle> VertexCentralAngles
            => Enumerable
                .Range(0, Count)
                .Select(i => 
                    Transform.MapAngle(
                        2d * Math.PI * GetVertexTurnRatio(i)
                    )
                );
        
        /// <summary>
        /// All vertex directions from the center of the polygon
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> VertexDirections
            => Enumerable
                .Range(0, Count)
                .Select(i =>
                    {
                        var angle = 
                            2d * Math.PI * GetVertexTurnRatio(i);

                        return Transform.MapVector(
                            Math.Cos(angle), 
                            Math.Sin(angle)
                        );
                    }
                );

        /// <summary>
        /// All vertex positions of polygon
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> VertexPoints
            => Enumerable
                .Range(0, Count)
                .Select(i =>
                    {
                        var angle = 
                            2d * Math.PI * GetVertexTurnRatio(i);

                        return Transform.MapPoint(
                            Math.Cos(angle), 
                            Math.Sin(angle)
                        );
                    }
                );

        /// <summary>
        /// All side mid-points of polygon
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> SideMidPoints
            => Enumerable
                .Range(0, Count)
                .Select(i =>
                    {
                        var r = Math.Cos(Math.PI * StarStep / Count);
                        var angle = Math.PI * ((2 * i + 1) * StarStep) / Count;

                        return Transform.MapPoint(
                            r * Math.Cos(angle),
                            r * Math.Sin(angle)
                        );
                    }
                );

        /// <summary>
        /// All side unit-normals of polygon
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> SideUnitNormals
            => Enumerable
                .Range(0, Count)
                .Select(i =>
                    {
                        var angle = Math.PI * ((2 * i.Mod(Count) + 1) * StarStep) / Count;

                        return Transform.MapNormal(
                            Math.Cos(angle),
                            Math.Sin(angle), 
                            true
                        );
                    }
                );

        /// <summary>
        /// All sides of the polygon as pairs of points
        /// </summary>
        public IEnumerable<Pair<LinFloat64Vector2D>> Sides
        {
            get
            {
                for (var i = 0; i < Count; i++)
                    yield return new Pair<LinFloat64Vector2D>(
                        GetVertexPoint(i),
                        GetVertexPoint(i + 1)
                    );
            }
        }
        
        /// <summary>
        /// All diagonals of the polygon as pairs of points
        /// </summary>
        public IEnumerable<Pair<LinFloat64Vector2D>> Diagonals
        {
            get
            {
                if (IsStar) yield break;

                for (var i = 0; i < Count; i++)
                {
                    var jMax = i == 0 ? Count - 2 : Count - 1;

                    for (var j = i + 2; j <= jMax; j++)
                        yield return new Pair<LinFloat64Vector2D>(
                            GetVertexPoint(i), 
                            GetVertexPoint(j)
                        );
                }
            }
        }
        
        /// <summary>
        /// All chords (sides and diagonals) of the polygon as pairs of points
        /// </summary>
        public IEnumerable<Pair<LinFloat64Vector2D>> Chords
        {
            get
            {
                for (var i = 0; i < Count; i++)
                for (var j = i + 1; j < Count; j++)
                    yield return new Pair<LinFloat64Vector2D>(
                        GetVertexPoint(i, true),
                        GetVertexPoint(j, true)
                    );
            }
        }

        /// <summary>
        /// Get a vertex position of the polygon
        /// </summary>
        /// <param name="index">index of vertex; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        public LinFloat64Vector2D this[int index]
            => GetVertexPoint(index);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64RegularPolygon2D(int sideCount, double outerRadius)
            : this(
                sideCount, 
                Float64SimilarityAffineMap2D.Scaling(outerRadius)
            )
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64RegularPolygon2D(int sideCount, Float64SimilarityAffineMap2D transform)
        {
            if (sideCount < 3)
                throw new ArgumentOutOfRangeException(nameof(sideCount));

            Count = sideCount;
            StarStep = 1;
            Transform = transform;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64RegularPolygon2D(int sideCount, int starStep, double outerRadius)
            : this(
                sideCount, 
                starStep, 
                Float64SimilarityAffineMap2D.Scaling(outerRadius)
            )
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64RegularPolygon2D(int sideCount, int starStep, Float64SimilarityAffineMap2D transform)
        {
            var gcd = sideCount.Gcd(starStep);
            sideCount /= gcd;
            starStep /= gcd;

            if (starStep > sideCount / 2)
                starStep = sideCount - starStep; // accept e.g. {5/3} as {5/2}
            
            if (sideCount < 3)
                throw new ArgumentOutOfRangeException(nameof(sideCount));

            if (starStep < 1 || starStep > sideCount / 2)
                throw new ArgumentOutOfRangeException(nameof(starStep));
            
            Count = sideCount;
            StarStep = starStep;
            Transform = transform;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out int sideCount, out int starStep, out Float64SimilarityAffineMap2D transform)
        {
            sideCount = Count;
            starStep = StarStep;
            transform = Transform;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetVertexTurnRatio(int index, bool ignoreStarStep = false)
        {
            var i = ignoreStarStep ? index : index * StarStep;

            return i.Mod(Count) / (double)Count;
        }

        /// <summary>
        /// Get a vertex position on the polygon
        /// </summary>
        /// <param name="index">index of vertex; periodic in range from 0 to Count - 1</param>
        /// <param name="ignoreStarStep"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetVertexPoint(int index, bool ignoreStarStep = false)
        {
            var angle = 
                2d * Math.PI * GetVertexTurnRatio(index, ignoreStarStep);

            return Transform.MapPoint(
                Math.Cos(angle),
                Math.Sin(angle)
            );
        }
        
        /// <summary>
        /// Get a vertex direction relative to the center of the polygon
        /// </summary>
        /// <param name="index">index of vertex; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetVertexDirection(int index)
        {
            var angle = 
                2d * Math.PI * GetVertexTurnRatio(index);

            return Transform.MapVector(
                Math.Cos(angle),
                Math.Sin(angle)
            );
        }

        /// <summary>
        /// Get the angle of the vertex direction measured from the x-axis
        /// where the origin is at the center of the polygon
        /// </summary>
        /// <param name="index">index of side; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64PolarAngle GetVertexCentralAngle(int index)
        {
            return Transform.MapAngle(
                2 * Math.PI * GetVertexTurnRatio(index)
            );
        }

        /// <summary>
        /// Get the two vertices of a side of the polygon
        /// </summary>
        /// <param name="index">index of vertex; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<LinFloat64Vector2D> GetSideVertices(int index)
        {
            return GetChordVertices(index, index + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<LinFloat64Vector2D> GetChordVertices(int index1, int index2)
        {
            Debug.Assert(
                Math.Abs(GetVertexTurnRatio(index1, true) - GetVertexTurnRatio(index2, true)) > Float64Utils.DefaultTolerance
            );

            var v1 = GetVertexPoint(index1, true);
            var v2 = GetVertexPoint(index2, true);

            return new Pair<LinFloat64Vector2D>(v1, v2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetChordLength(int index1, int index2)
        {
            var k = Math.Abs(index1.Mod(Count) - index2.Mod(Count));
            k = Math.Min(k, Count - k);

            return 2d * OuterRadius * Math.Sin(Math.PI * k / Count);
        }

        /// <summary>
        /// Get the mid-point of a side of the polygon
        /// </summary>
        /// <param name="index">index of side; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetSideMidPoint(int index)
        {
            var r = 
                OuterRadius * Math.Cos(Math.PI * StarStep / Count);
            
            var angle = 
                Math.PI * ((2 * index.Mod(Count) + 1) * StarStep) / Count;
            
            return Transform.MapPoint(
                r * Math.Cos(angle), 
                r * Math.Sin(angle)
            );
        }

        /// <summary>
        /// Get a point on a side of the polygon using lerp of the side's two vertices
        /// </summary>
        /// <param name="index">index of side; periodic in range from 0 to Count - 1</param>
        /// <param name="t">Lerp parameter</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetSidePoint(int index, double t)
        {
            var (v1, v2) =
                GetSideVertices(index);

            return t.Lerp(v1, v2);
        }

        /// <summary>
        /// Get the unit-normal to a side of the polygon
        /// </summary>
        /// <param name="index">index of side; periodic in range from 0 to Count - 1</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetSideUnitNormal(int index)
        {
            var angle = 
                Math.PI * ((2 * index.Mod(Count) + 1) * StarStep) / Count;
            
            return Transform.MapNormal(
                Math.Cos(angle), 
                Math.Sin(angle),
                true
            );
        }
        
        /// <summary>
        /// Normal points 90° CCW from vector v1 - v2
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D GetChordUnitNormal(int index1, int index2)
        {
            var v1 = GetVertexPoint(index1, true);
            var v2 = GetVertexPoint(index2, true);

            return (v1 - v2).GetUnitNormal();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64Vector2D> GetPerimeterPoints(int samplingCount)
        {
            samplingCount = Math.Max(samplingCount, 0);

            var tDelta = 1d / (samplingCount + 1);

            foreach (var (p1, p2) in Sides)
                for (var i = 0; i <= samplingCount; i++)
                    yield return (i * tDelta).Lerp(p1, p2);
        }

        
        /// <summary>
        /// Derive a convex polygon {n/1} from this regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64RegularPolygon2D GetConvex()
        {
            return new Float64RegularPolygon2D(Count, Transform);
        }
        
        /// <summary>
        /// Derive a convex polygon {n/1} from this regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64RegularPolygon2D GetConvex(Float64SimilarityAffineMap2D transform)
        {
            return new Float64RegularPolygon2D(Count, transform);
        }

        /// <summary>
        /// Derive a star polygon {n/m} from this regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64RegularPolygon2D GetStar(int starStep)
        {
            return new Float64RegularPolygon2D(Count, starStep, Transform);
        }
        
        /// <summary>
        /// Derive a star polygon {n/m} from this regular polygon.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64RegularPolygon2D GetStar(int starStep, Float64SimilarityAffineMap2D transform)
        {
            return new Float64RegularPolygon2D(Count, starStep, transform);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<LinFloat64Vector2D> GetEnumerator()
        {
            return VertexPoints.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}

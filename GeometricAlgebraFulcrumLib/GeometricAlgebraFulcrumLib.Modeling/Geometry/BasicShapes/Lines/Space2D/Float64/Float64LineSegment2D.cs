using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public readonly record struct Float64LineSegment2D :
    IFloat64LineSegment2D
{
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed class ToleranceComparer(int decimalPlaces = 12) :
        IEqualityComparer<Float64LineSegment2D>
    {
        private readonly LinFloat64Vector2D.ToleranceComparer _vectorComparer 
            = new(decimalPlaces);

        public int DecimalPlaces 
            => _vectorComparer.DecimalPlaces;

        public double Tolerance 
            => _vectorComparer.Tolerance;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ToleranceComparer(double tolerance = Float64Utils.DefaultTolerance)
            : this((int)Math.Ceiling(-Math.Abs(tolerance).Log10()))
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Float64LineSegment2D a, Float64LineSegment2D b)
        {
            var (a1, a2) = a.GetEndPoints();
            var (b1, b2) = b.GetEndPoints();

            var equals = (
                _vectorComparer.Equals(a1, b1) &&
                _vectorComparer.Equals(a2, b2)
            ) || (
                _vectorComparer.Equals(a2, b1) &&
                _vectorComparer.Equals(a1, b2)
            );
            
            return equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(Float64LineSegment2D s)
        {
            var (p1, p2) = s.GetEndPoints();

            return HashCode.Combine(
                p1.GetHashCode(), 
                p2.GetHashCode()
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<Float64LineSegment2D, T> CreateToleranceDictionary<T>(int decimalPlaces = 12)
    {
        return new Dictionary<Float64LineSegment2D, T>(
            new ToleranceComparer(decimalPlaces)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<Float64LineSegment2D, T> CreateToleranceDictionary<T>(double tolerance = Float64Utils.DefaultTolerance)
    {
        return new Dictionary<Float64LineSegment2D, T>(
            new ToleranceComparer(tolerance)
        );
    }


    public static Float64LineSegment2D ZeroSegment { get; }
        = new Float64LineSegment2D(0, 0, 0, 0);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment2D Create(double point1X, double point1Y, double point2X, double point2Y)
    {
        return new Float64LineSegment2D(
            point1X,
            point1Y,
            point2X,
            point2Y
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment2D Create(IPair<double> point1, IPair<double> point2)
    {
        return new Float64LineSegment2D(point1, point2);
    }

    
    public bool IntersectionTestsEnabled => true;

    public LinFloat64Vector2D Point1 { get; }

    public LinFloat64Vector2D Point2 { get; }

    public double Point1X 
        => Point1.Item1;

    public double Point1Y 
        => Point1.Item2;

    public double Point2X 
        => Point2.Item1;

    public double Point2Y 
        => Point2.Item2;
    
    public LinFloat64Vector2D Item1 
        => Point1;

    public LinFloat64Vector2D Item2 
        => Point2;

    public LinFloat64Vector2D MidPoint 
        => (Point1 + Point2) / 2;
    
    public LinFloat64Vector2D Direction12 
        => Point2 - Point1;
    
    public LinFloat64Vector2D UnitDirection12 
        => (Point2 - Point1).ToUnitLinVector2D(false);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64LineSegment2D(IPair<double> point1, IPair<double> point2)
    {
        Point1 = point1.ToLinVector2D();
        Point2 = point2.ToLinVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64LineSegment2D(double p1X, double p1Y, double p2X, double p2Y)
    {
        Point1 = LinFloat64Vector2D.Create(p1X, p1Y);
        Point2 = LinFloat64Vector2D.Create(p2X, p2Y);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out LinFloat64Vector2D point1, out LinFloat64Vector2D point2)
    {
        point1 = Point1;
        point2 = Point2;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Containing(Point1X, Point1Y, Point2X, Point2Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.Containing(Point1X, Point1Y, Point2X, Point2Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return new StringBuilder()
            .Append("Line Segment <")
            .Append("(")
            .Append(Point1X)
            .Append(", ")
            .Append(Point1Y)
            .Append(") - (")
            .Append(Point2X)
            .Append(", ")
            .Append(Point2Y)
            .Append(")>")
            .ToString();
    }
}
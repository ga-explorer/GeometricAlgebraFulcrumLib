using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LineSegment3D :
    IFloat64LineSegment3D
{
    public sealed class ToleranceComparer(int decimalPlaces = 12) :
        IEqualityComparer<Float64LineSegment3D>
    {
        private readonly LinFloat64Vector3D.ToleranceComparer _vectorComparer 
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
        public bool Equals(Float64LineSegment3D? a, Float64LineSegment3D? b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

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
        public int GetHashCode(Float64LineSegment3D s)
        {
            var (p1, p2) = s.GetEndPoints();

            return HashCode.Combine(
                p1.GetHashCode(), 
                p2.GetHashCode()
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<Float64LineSegment3D, T> CreateToleranceDictionary<T>(int decimalPlaces = 12)
    {
        return new Dictionary<Float64LineSegment3D, T>(
            new ToleranceComparer(decimalPlaces)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<Float64LineSegment3D, T> CreateToleranceDictionary<T>(double tolerance = Float64Utils.DefaultTolerance)
    {
        return new Dictionary<Float64LineSegment3D, T>(
            new ToleranceComparer(tolerance)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment3D Create(ITriplet<double> point1, ITriplet<double> point2)
    {
        return new Float64LineSegment3D(point1, point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment3D CreateFromPointAndVector(ITriplet<double> point1, ITriplet<double> direction)
    {
        return new Float64LineSegment3D(
            point1.Item1,
            point1.Item2,
            point1.Item3,
            point1.Item1 + direction.Item1,
            point1.Item2 + direction.Item2,
            point1.Item3 + direction.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64LineSegment3D CreateFromPointAndScaledVector(ITriplet<double> point1, ITriplet<double> direction, double scaleFactor)
    {
        return new Float64LineSegment3D(
            point1.Item1,
            point1.Item2,
            point1.Item3,
            point1.Item1 + scaleFactor * direction.Item1,
            point1.Item2 + scaleFactor * direction.Item2,
            point1.Item3 + scaleFactor * direction.Item3
        );
    }

    
    public bool IntersectionTestsEnabled { get; } = false;

    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }

    public LinFloat64Vector3D Item1 
        => Point1;
    
    public LinFloat64Vector3D Item2 
        => Point2;

    public double Point1X 
        => Point1.X;

    public double Point1Y 
        => Point1.Y;

    public double Point1Z 
        => Point1.Z;


    public double Point2X 
        => Point2.X;

    public double Point2Y 
        => Point2.Y;

    public double Point2Z 
        => Point2.Z;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64LineSegment3D(ITriplet<double> point1, ITriplet<double> point2)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64LineSegment3D(double p1X, double p1Y, double p1Z, double p2X, double p2Y, double p2Z)
    {
        Point1 = LinFloat64Vector3D.Create(p1X, p1Y, p1Z);
        Point2 = LinFloat64Vector3D.Create(p2X, p2Y, p2Z);

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out LinFloat64Vector3D point1, out LinFloat64Vector3D point2)
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
    public Float64BoundingBox3D GetBoundingBox()
    {
        return Float64BoundingBox3D.Containing(
            this.GetPoint1(),
            this.GetPoint2()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64BoundingBoxComposer3D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer3D.CreateFromPoints(
            this.GetPoint1(),
            this.GetPoint2()
        );
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
            .Append(", ")
            .Append(Point1Z)
            .Append(") - (")
            .Append(Point2X)
            .Append(", ")
            .Append(Point2Y)
            .Append(", ")
            .Append(Point2Z)
            .Append(")>")
            .ToString();
    }
}
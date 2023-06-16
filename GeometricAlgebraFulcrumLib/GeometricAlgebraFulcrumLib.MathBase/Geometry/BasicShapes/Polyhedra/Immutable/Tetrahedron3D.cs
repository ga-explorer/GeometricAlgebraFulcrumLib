using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Polyhedra.Immutable
{
    public sealed class Tetrahedron3D
    {
        public static Tetrahedron3D CreateFromPoints(Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3, Float64Vector3D point4)
        {
            return new Tetrahedron3D(
                point1.X, point1.Y, point1.Z,
                point2.X, point2.Y, point2.Z,
                point3.X, point3.Y, point3.Z,
                point4.X, point4.Y, point4.Z
            );
        }


        public double Point1X { get; }

        public double Point1Y { get; }

        public double Point1Z { get; }


        public double Point2X { get; }

        public double Point2Y { get; }

        public double Point2Z { get; }


        public double Point3X { get; }

        public double Point3Y { get; }

        public double Point3Z { get; }


        public double Point4X { get; }

        public double Point4Y { get; }

        public double Point4Z { get; }


        public Float64Vector3D Point1
        {
            get { return Float64Vector3D.Create(Point1X, Point1Y, Point1Z); }
        }

        public Float64Vector3D Point2
        {
            get { return Float64Vector3D.Create(Point2X, Point2Y, Point2Z); }
        }

        public Float64Vector3D Point3
        {
            get { return Float64Vector3D.Create(Point3X, Point3Y, Point3Z); }
        }

        public Float64Vector3D Direction12
        {
            get { return Float64Vector3D.Create(Point2X - Point1X, Point2Y - Point1Y, Point2Z - Point1Z); }
        }

        public Float64Vector3D Direction21
        {
            get { return Float64Vector3D.Create(Point1X - Point2X, Point1Y - Point2Y, Point1Z - Point2Z); }
        }

        public Float64Vector3D Direction23
        {
            get { return Float64Vector3D.Create(Point3X - Point2X, Point3Y - Point2Y, Point3Z - Point2Z); }
        }

        public Float64Vector3D Direction32
        {
            get { return Float64Vector3D.Create(Point2X - Point3X, Point2Y - Point3Y, Point2Z - Point3Z); }
        }

        public Float64Vector3D Direction31
        {
            get { return Float64Vector3D.Create(Point1X - Point3X, Point1Y - Point3Y, Point1Z - Point3Z); }
        }

        public Float64Vector3D Direction13
        {
            get { return Float64Vector3D.Create(Point3X - Point1X, Point3Y - Point1Y, Point3Z - Point1Z); }
        }

        public Float64Vector3D Direction14
        {
            get { return Float64Vector3D.Create(Point4X - Point1X, Point4Y - Point1Y, Point4Z - Point1Z); }
        }

        public Float64Vector3D Direction41
        {
            get { return Float64Vector3D.Create(Point1X - Point4X, Point1Y - Point4Y, Point1Z - Point4Z); }
        }

        public Float64Vector3D Direction24
        {
            get { return Float64Vector3D.Create(Point4X - Point2X, Point4Y - Point2Y, Point4Z - Point2Z); }
        }

        public Float64Vector3D Direction42
        {
            get { return Float64Vector3D.Create(Point2X - Point4X, Point2Y - Point4Y, Point2Z - Point4Z); }
        }

        public Float64Vector3D Direction34
        {
            get { return Float64Vector3D.Create(Point4X - Point3X, Point4Y - Point3Y, Point4Z - Point3Z); }
        }

        public Float64Vector3D Direction43
        {
            get { return Float64Vector3D.Create(Point3X - Point4X, Point3Y - Point4Y, Point3Z - Point4Z); }
        }

        public Float64Vector3D Normal123
        {
            get { return Direction12.VectorCross(Direction23); }
        }

        public Float64Vector3D UnitNormal123
        {
            get { return Direction12.VectorUnitCross(Direction23); }
        }

        public Float64Vector3D Normal321
        {
            get { return Direction32.VectorCross(Direction21); }
        }

        public Float64Vector3D UnitNormal321
        {
            get { return Direction32.VectorUnitCross(Direction21); }
        }

        public Float64Vector3D Normal124
        {
            get { return Direction12.VectorCross(Direction24); }
        }

        public Float64Vector3D UnitNormal124
        {
            get { return Direction12.VectorUnitCross(Direction24); }
        }

        public Float64Vector3D Normal421
        {
            get { return Direction42.VectorCross(Direction21); }
        }

        public Float64Vector3D UnitNormal421
        {
            get { return Direction42.VectorUnitCross(Direction21); }
        }

        public Float64Vector3D Normal134
        {
            get { return Direction13.VectorCross(Direction34); }
        }

        public Float64Vector3D UnitNormal134
        {
            get { return Direction13.VectorUnitCross(Direction34); }
        }

        public Float64Vector3D Normal431
        {
            get { return Direction43.VectorCross(Direction31); }
        }

        public Float64Vector3D UnitNormal431
        {
            get { return Direction43.VectorUnitCross(Direction31); }
        }

        public Float64Vector3D Normal234
        {
            get { return Direction23.VectorCross(Direction34); }
        }

        public Float64Vector3D UnitNormal234
        {
            get { return Direction23.VectorUnitCross(Direction34); }
        }

        public Float64Vector3D Normal432
        {
            get { return Direction43.VectorCross(Direction32); }
        }

        public Float64Vector3D UnitNormal432
        {
            get { return Direction43.VectorUnitCross(Direction32); }
        }

        public bool HasNaNComponent
        {
            get
            {
                return double.IsNaN(Point1X) || double.IsNaN(Point1Y) || double.IsNaN(Point1Z) ||
                       double.IsNaN(Point2X) || double.IsNaN(Point2Y) || double.IsNaN(Point2Z) ||
                       double.IsNaN(Point3X) || double.IsNaN(Point3Y) || double.IsNaN(Point3Z) ||
                       double.IsNaN(Point4X) || double.IsNaN(Point4Y) || double.IsNaN(Point4Z);
            }
        }


        internal Tetrahedron3D(double p1X, double p1Y, double p1Z, double p2X, double p2Y, double p2Z, double p3X, double p3Y, double p3Z, double p4X, double p4Y, double p4Z)
        {
            Point1X = p1X;
            Point1Y = p1Y;
            Point1Z = p1Z;

            Point2X = p2X;
            Point2Y = p2Y;
            Point2Z = p2Z;

            Point3X = p3X;
            Point3Y = p3Y;
            Point3Z = p3Z;

            Point4X = p4X;
            Point4Y = p4Y;
            Point4Z = p4Z;

            Debug.Assert(!HasNaNComponent);
        }


        public Float64Vector3D GetPointAt(double w1, double w2, double w3)
        {
            var w4 = 1.0d - (w1 + w2 + w3);

            return Float64Vector3D.Create(w1 * Point1X + w2 * Point2X + w3 * Point3X + w4 * Point4X,
                w1 * Point1Y + w2 * Point2Y + w3 * Point3Y + w4 * Point4Y,
                w1 * Point1Z + w2 * Point2Z + w3 * Point3Z + w4 * Point4Z);
        }

        public IEnumerable<Float64Vector3D> GetPointsAt(IEnumerable<Float64Vector3D> parametersList)
        {
            return parametersList.Select(p => GetPointAt(p.X, p.Y, p.Z));
        }
    }
}

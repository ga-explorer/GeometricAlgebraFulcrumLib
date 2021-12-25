using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public sealed record GrParametricCurveLocalFrame3D :
        IGraphicsCurveLocalFrame3D
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Frenet%E2%80%93Serret_formulas
        /// </summary>
        /// <param name="point"></param>
        /// <param name="firstDerivativeVector"></param>
        /// <param name="secondDerivativeVector"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public static GrParametricCurveLocalFrame3D CreateFrenetFrame(double parameterValue, ITuple3D point, ITuple3D firstDerivativeVector, ITuple3D secondDerivativeVector)
        {
            var tangent = firstDerivativeVector;
            var normal1 = secondDerivativeVector.VectorCross(firstDerivativeVector);
            var normal2 = firstDerivativeVector.VectorCross(normal1);

            return new GrParametricCurveLocalFrame3D(
                parameterValue,
                point.ToTuple3D(),
                normal1.ToUnitVector(),
                normal2.ToUnitVector(),
                tangent.ToUnitVector()
            );
        }

        /// <summary>
        /// Create a local frame based on the tangent only
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <param name="point"></param>
        /// <param name="tangentVector"></param>
        /// <returns></returns>
        public static GrParametricCurveLocalFrame3D CreateFrame(double parameterValue, ITuple3D point, ITuple3D tangentVector)
        {
            var tangent = tangentVector.ToUnitVector();
            var normal1 = tangentVector.GetUnitNormal();
            var normal2 = tangent.VectorCross(normal1).ToUnitVector();

            return new GrParametricCurveLocalFrame3D(
                parameterValue,
                point,
                normal1,
                normal2,
                tangent
            );
        }

        public static GrParametricCurveLocalFrame3D CreateFrame(double parameterValue, ITuple3D point, ITuple3D normal1, ITuple3D normal2, ITuple3D tangent)
        {
            return new GrParametricCurveLocalFrame3D(
                parameterValue,
                point.ToTuple3D(),
                normal1.ToTuple3D(),
                normal2.ToTuple3D(),
                tangent.ToTuple3D()
            );
        }


        /// <summary>
        /// The curve parameter value at the given curve point
        /// </summary>
        public double ParameterValue { get; }

        public int Index { get; internal set; } = -1;

        /// <summary>
        /// A point on the curve
        /// </summary>
        public Tuple3D Point { get; }

        public double Item1 
            => Point.X;
        
        public double Item2 
            => Point.Y;
        
        public double Item3 
            => Point.Z;
        
        public double X 
            => Point.X;
        
        public double Y 
            => Point.Y;
        
        public double Z 
            => Point.Z;
        
        public Color Color { get; set; }

        /// <summary>
        /// The first unit vector orthogonal to the tangent and normal at the given curve point
        /// </summary>
        public GrNormal3D Normal1 { get; }
            = new GrNormal3D();

        /// <summary>
        /// The second unit vector orthogonal to the tangent and normal at the given curve point
        /// </summary>
        public GrNormal3D Normal2 { get; }
            = new GrNormal3D();

        /// <summary>
        /// The tangent unit vector to the curve at the given curve point
        /// </summary>
        public Tuple3D Tangent { get; }

        public bool IsValid()
        {
            var length1 = Normal1.GetLengthSquared();
            var length2 = Normal2.GetLengthSquared();
            var length3 = Tangent.GetLengthSquared();

            var cosAngle1 = Normal1.VectorDot(Normal2);
            var cosAngle2 = Normal2.VectorDot(Tangent);
            var cosAngle3 = Tangent.VectorDot(Normal1);

            var isValid = 
                !double.IsNaN(ParameterValue) &&
                Point.IsValid() &&
                Tangent.IsValid() &&
                Normal1.IsValid() &&
                Normal2.IsValid() && 
                length1.IsNearEqual(1) &&
                length2.IsNearEqual(1) &&
                length3.IsNearEqual(1) &&
                cosAngle1.IsNearZero() &&
                cosAngle2.IsNearZero() &&
                cosAngle3.IsNearZero();

            return isValid;
        }
        

        private GrParametricCurveLocalFrame3D(double parameterValue, [NotNull] ITuple3D point, [NotNull] ITuple3D normal1, [NotNull] ITuple3D normal2, [NotNull] ITuple3D tangent)
        {
            ParameterValue = parameterValue;
            Point = point.ToTuple3D();
            Tangent = tangent.ToTuple3D();
            Normal1.Set(normal1);
            Normal2.Set(normal2);

            Debug.Assert(IsValid());
        }


        internal GrParametricCurveLocalFrame3D UpdateNormals([NotNull] ITuple3D normal1, [NotNull] ITuple3D normal2)
        {
            Normal1.Set(normal1);
            Normal2.Set(normal2);

            Debug.Assert(IsValid());

            return this;
        }

        public GrParametricCurveLocalFrame3D SetFrenetNormals(ITuple3D secondDerivativeVector)
        {
            var normal1 = secondDerivativeVector.VectorCross(Tangent).ToUnitVector();
            var normal2 = Tangent.VectorCross(normal1);
            
            Normal1.Set(normal1);
            Normal2.Set(normal2);

            Debug.Assert(IsValid());

            return this;
        }

        public GrParametricCurveLocalFrame3D SetSimpleRotationNormals(GrParametricCurveLocalFrame3D sourceFrame)
        {
            var (normal1, normal2) = 
                sourceFrame.RotateNormalsByTangent(Tangent);
            
            Normal1.Set(normal1);
            Normal2.Set(normal2);

            Debug.Assert(IsValid());

            return this;
        }

        /// <summary>
        /// See paper "Computation of Rotation Minimizing Frames"
        /// https://www.microsoft.com/en-us/research/wp-content/uploads/2016/12/Computation-of-rotation-minimizing-frames.pdf
        /// </summary>
        /// <param name="sourceFrame"></param>
        /// <returns></returns>
        public GrParametricCurveLocalFrame3D SetMinimizedRotationNormals(GrParametricCurveLocalFrame3D sourceFrame)
        {
            var planeNormal1 = 
                Point - sourceFrame.Point;

            var (normal1L, normal2L, tangentL) = 
                planeNormal1.ReflectVectorsOnVector(
                    sourceFrame.Normal1,
                    sourceFrame.Normal2,
                    sourceFrame.Tangent
                );


            var planeNormal2 = 
                Tangent - tangentL;

            var (normal1, normal2) = 
                planeNormal2.ReflectVectorsOnVector(
                    normal1L,
                    normal2L
                );

            Normal1.Set(normal1);
            Normal2.Set(normal2);

            Debug.Assert(IsValid());

            return this;
        }


        public double GetMaxDirectionAngleWithFrame(GrParametricCurveLocalFrame3D frame2)
        {
            var maxAngle = 0d;

            var angle = Normal1.GetVectorsAngle(frame2.Normal1);
            if (maxAngle < angle) maxAngle = angle;

            angle = Normal2.GetVectorsAngle(frame2.Normal2);
            if (maxAngle < angle) maxAngle = angle;

            angle = Tangent.GetVectorsAngle(frame2.Tangent);
            if (maxAngle < angle) maxAngle = angle;

            return maxAngle;
        }

        public Triplet<Tuple3D> RotateDirectionsByTangent(ITuple3D newTangent)
        {
            var matrix = 
                SquareMatrix3.CreateVectorToVectorRotationMatrix3D(Tangent, newTangent);

            var newNormal1 = matrix * Normal1;
            var newNormal2 = matrix * Normal2;

            return new Triplet<Tuple3D>(newNormal1, newNormal2, newTangent.ToTuple3D());

            //var x = Tangent;
            //var y = Normal1;
            //var z = Normal2;
            //var xRotated = newTangent.ToTuple3D();

            ////Begin GA-FuL Symbolic Context Code Generation, 2021-11-20T13:06:13.1183386+02:00
            ////SymbolicContext: TestCode
            ////Input Variables: 12 used, 0 not used, 12 total.
            ////Temp Variables: 138 sub-expressions, 0 generated temps, 138 total.
            ////Target Temp Variables: 14 total.
            ////Output Variables: 6 total.
            ////Computations: 1 average, 144 total.
            ////Memory Reads: 1.7222222222222223 average, 248 total.
            ////Memory Writes: 144 total.
            ////
            ////SymbolicContext Binding Data:
            ////   1 = constant: '1'
            ////   -1 = constant: '-1'
            ////   2 = constant: '2'
            ////   Rational[1,2] = constant: 'Rational[1,2]'
            ////   x1 = parameter: x.X
            ////   x2 = parameter: x.Y
            ////   x3 = parameter: x.Z
            ////   xRotated1 = parameter: xRotated.X
            ////   xRotated2 = parameter: xRotated.Y
            ////   xRotated3 = parameter: xRotated.Z
            ////   y1 = parameter: y.X
            ////   y2 = parameter: y.Y
            ////   y3 = parameter: y.Z
            ////   z1 = parameter: z.X
            ////   z2 = parameter: z.Y
            ////   z3 = parameter: z.Z

            //var temp0 = x.X * xRotated.X;
            //var temp1 = x.Y * xRotated.Y;
            //temp0 += temp1;
            //temp1 = x.Z * xRotated.Z;
            //temp0 += temp1;
            //temp1 = -temp0;
            //temp1 = 1 + temp1;
            //temp1 = 0.5d * temp1;
            //temp1 = Math.Pow(temp1, 0.5d);
            //var temp2 = x.Y * xRotated.X;
            //var temp3 = x.X * xRotated.Y;
            //temp3 = -temp3;
            //temp2 += temp3;
            //temp3 = temp2 * temp2;
            //temp3 = -temp3;
            //var temp4 = x.Z * xRotated.X;
            //var temp5 = x.X * xRotated.Z;
            //temp5 = -temp5;
            //temp4 += temp5;
            //temp5 = temp4 * temp4;
            //temp5 = -temp5;
            //temp3 += temp5;
            //temp5 = x.Z * xRotated.Y;
            //var temp6 = x.Y * xRotated.Z;
            //temp6 = -temp6;
            //temp5 += temp6;
            //temp6 = temp5 * temp5;
            //temp6 = -temp6;
            //temp3 += temp6;
            //temp3 = -temp3;
            //temp3 = Math.Pow(temp3, 0.5d);
            //temp3 = 1 / temp3;
            //temp2 *= temp3;
            //temp2 = temp1 * temp2;
            //temp6 = -temp2;
            //var temp7 = temp2 * y.Z;
            //temp4 *= temp3;
            //temp4 = temp1 * temp4;
            //var temp8 = temp4 * y.Y;
            //temp8 = -temp8;
            //temp7 += temp8;
            //temp3 = temp5 * temp3;
            //temp1 *= temp3;
            //temp3 = temp1 * y.X;
            //temp3 = temp7 + temp3;
            //temp5 = temp6 * temp3;
            //temp5 = -temp5;
            //temp7 = -temp4;
            //temp0 = 1 + temp0;
            //temp0 = 0.5d * temp0;
            //temp0 = Math.Pow(temp0, 0.5d);
            //temp8 = temp0 * y.X;
            //var temp9 = temp2 * y.Y;
            //temp8 += temp9;
            //temp9 = temp4 * y.Z;
            //temp8 += temp9;
            //temp9 = temp7 * temp8;
            //var temp10 = -temp1;
            //var temp11 = temp0 * y.Y;
            //var temp12 = temp2 * y.X;
            //temp12 = -temp12;
            //temp11 += temp12;
            //temp12 = temp1 * y.Z;
            //temp11 += temp12;
            //temp12 = temp10 * temp11;
            //temp9 += temp12;
            //temp12 = temp0 * y.Z;
            //var temp13 = temp4 * y.X;
            //temp13 = -temp13;
            //temp12 += temp13;
            //temp13 = temp1 * y.Y;
            //temp13 = -temp13;
            //temp12 += temp13;
            //temp13 = temp0 * temp12;
            //temp9 += temp13;
            //var yRotatedZ = temp5 + temp9;

            //temp5 = temp3 * temp7;
            //temp9 = temp6 * temp8;
            //temp13 = temp0 * temp11;
            //temp9 += temp13;
            //temp13 = temp10 * temp12;
            //temp13 = -temp13;
            //temp9 += temp13;
            //var yRotatedY = temp5 + temp9;

            //temp3 *= temp10;
            //temp3 = -temp3;
            //temp5 = temp0 * temp8;
            //temp8 = temp6 * temp11;
            //temp8 = -temp8;
            //temp5 += temp8;
            //temp8 = temp7 * temp12;
            //temp8 = -temp8;
            //temp5 += temp8;
            //var yRotatedX = temp3 + temp5;

            //temp3 = temp0 * z.X;
            //temp5 = temp2 * z.Y;
            //temp3 += temp5;
            //temp5 = temp4 * z.Z;
            //temp3 += temp5;
            //temp5 = temp7 * temp3;
            //temp8 = temp0 * z.Y;
            //temp9 = temp2 * z.X;
            //temp9 = -temp9;
            //temp8 += temp9;
            //temp9 = temp1 * z.Z;
            //temp8 += temp9;
            //temp9 = temp10 * temp8;
            //temp5 += temp9;
            //temp9 = temp0 * z.Z;
            //temp11 = temp4 * z.X;
            //temp11 = -temp11;
            //temp9 += temp11;
            //temp11 = temp1 * z.Y;
            //temp11 = -temp11;
            //temp9 += temp11;
            //temp11 = temp0 * temp9;
            //temp5 += temp11;
            //temp2 *= z.Z;
            //temp4 *= z.Y;
            //temp4 = -temp4;
            //temp2 += temp4;
            //temp1 *= z.X;
            //temp1 = temp2 + temp1;
            //temp2 = temp6 * temp1;
            //temp2 = -temp2;
            //var zRotatedZ = temp5 + temp2;

            //temp2 = temp6 * temp3;
            //temp4 = temp0 * temp8;
            //temp2 += temp4;
            //temp4 = temp10 * temp9;
            //temp4 = -temp4;
            //temp2 += temp4;
            //temp4 = temp7 * temp1;
            //var zRotatedY = temp2 + temp4;

            //temp0 *= temp3;
            //temp2 = temp6 * temp8;
            //temp2 = -temp2;
            //temp0 += temp2;
            //temp2 = temp7 * temp9;
            //temp2 = -temp2;
            //temp0 += temp2;
            //temp1 = temp10 * temp1;
            //temp1 = -temp1;
            //var zRotatedX = temp0 + temp1;

            ////Finish GA-FuL Symbolic Context Code Generation, 2021-11-20T13:06:13.2049363+02:00

            //var newNormal1 = new Tuple3D(yRotatedX, yRotatedY, yRotatedZ);
            //var newNormal2 = new Tuple3D(zRotatedX, zRotatedY, zRotatedZ);

            //return new Triplet<Tuple3D>(newNormal1, newNormal2, newTangent.ToTuple3D());
        }

        public Pair<Tuple3D> RotateNormalsByTangent(ITuple3D newTangent)
        {
            var matrix = 
                SquareMatrix3.CreateVectorToVectorRotationMatrix3D(Tangent, newTangent);

            var newNormal1 = matrix * Normal1;
            var newNormal2 = matrix * Normal2;

            return new Pair<Tuple3D>(newNormal1, newNormal2);
        }


        public GrParametricCurveLocalFrame3D TranslateBy(ITuple3D translationVector)
        {
            Debug.Assert(translationVector.IsValid());

            return new GrParametricCurveLocalFrame3D(
                ParameterValue,
                new Tuple3D(
                    Point.X + translationVector.X,
                    Point.Y + translationVector.Y,
                    Point.Z + translationVector.Z
                ),
                Normal1,
                Normal2,
                Tangent
            );
        }
    }
}
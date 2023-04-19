using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Volumes;

namespace GraphicsComposerLib.Geometry.SdfShapes
{
    public abstract class ScalarDistanceFunction : 
        ISdfGeometry3D
    {
        private double _distanceDeltaInv = 1 << 13;
        private double _distanceDelta = 1.0d / (1 << 13);
        

        public double SdfDistanceDelta
        {
            get => _distanceDelta;
            set
            {
                _distanceDelta = value;
                _distanceDeltaInv = 1 / value;
            }
        }

        public double SdfDistanceDeltaInv
        {
            get => _distanceDeltaInv;
            set
            {
                _distanceDeltaInv = value;
                _distanceDelta = 1 / value;
            }
        }

        public double SdfAlpha { get; set; }
             = 1.0d;

        public double SdfDelta { get; set; } 
            = 0.01d;

        

        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public Float64Tuple3D GetPoint(IFloat64Tuple3D parameterValue)
        {
            return parameterValue.ToTuple3D();
        }

        public Float64Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return new Float64Tuple3D(parameterValue1, parameterValue2, parameterValue3);
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public abstract double GetScalarDistance(IFloat64Tuple3D point);

        public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetScalarDistance(
                new Float64Tuple3D(parameterValue1, parameterValue2, parameterValue3)
            );
        }

        public GrParametricVolumeLocalFrame3D GetFrame(IFloat64Tuple3D parameterValue)
        {
            return new GrParametricVolumeLocalFrame3D(
                parameterValue,
                parameterValue,
                GetScalarDistance(parameterValue)
            );
        }

        public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return new GrParametricVolumeLocalFrame3D(
                parameterValue1,
                parameterValue2,
                parameterValue3,
                new Float64Tuple3D(parameterValue1, parameterValue2, parameterValue3),
                GetScalarDistance(parameterValue1, parameterValue2, parameterValue3)
            );
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Newton%27s_method
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t0"></param>
        /// <returns></returns>
        public virtual double ComputeSdfRayStep(Line3D ray, double t0)
        {
            var f0 = GetScalarDistance(ray.GetPointAt(t0));
            var f1 = GetScalarDistance(ray.GetPointAt(t0 + _distanceDelta));

            return _distanceDelta * f0 / (f0 - f1);
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual Float64Tuple3D ComputeSdfNormal(IFloat64Tuple3D point)
        {
            var d1 = GetScalarDistance(new Float64Tuple3D(
                point.X + SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d2 = GetScalarDistance(new Float64Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z + SdfDistanceDelta
            ));

            var d3 = GetScalarDistance(new Float64Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d4 = GetScalarDistance(new Float64Tuple3D(
                point.X + SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z + SdfDistanceDelta
            ));


            return Float64Tuple3D.CreateUnitVector(
                d4 + d1 - d2 - d3,
                d4 - d1 - d2 + d3,
                d4 - d1 + d2 - d3
            );
        }
    }
}

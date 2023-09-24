using System.Collections;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.ParametricShapes.Volumes;
using GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.GeometricAlgebra
{
    public abstract class MultivectorGeometry3D : 
        ISdfGeometry3D, 
        IReadOnlyList<double>
    {
    
        private double _distanceDeltaInv = 1 << 13;
        private double _distanceDelta = 1.0d / (1 << 13);

        
        protected double[] Scalars { get; }

        public int Count => Scalars.Length;

        public double this[int index] => Scalars[index];

        public MultivectorNullSpaceKind NullSpaceKind { get; } 
        

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


        protected MultivectorGeometry3D(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
        {
            Scalars = bladeScalars;
            NullSpaceKind = nullSpaceKind;
        }


        protected abstract double ComputeSdfOpns(IFloat64Vector3D point);

        protected abstract double ComputeSdfIpns(IFloat64Vector3D point);


        protected double CorrectSdf(double sdf)
        {
            sdf = sdf < 0 ? Math.Sqrt(-sdf) : Math.Sqrt(sdf);
            return SdfAlpha * sdf - SdfDelta;
        }
        

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Float64Vector3D GetPoint(IFloat64Vector3D parameterValue)
        {
            return parameterValue.ToVector3D();
        }

        public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return Float64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3);
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual double GetScalarDistance(IFloat64Vector3D point)
        {
            var sdf = NullSpaceKind == MultivectorNullSpaceKind.OuterProductNullSpace
                ? ComputeSdfOpns(point)
                : ComputeSdfIpns(point);

            return CorrectSdf(sdf);
        }

        public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetScalarDistance(Float64Vector3D.Create(parameterValue1,
                parameterValue2,
                parameterValue3));
        }

        public GrParametricVolumeLocalFrame3D GetFrame(IFloat64Vector3D parameterValue)
        {
            return new GrParametricVolumeLocalFrame3D(
                parameterValue,
                parameterValue,
                GetScalarDistance(parameterValue)
            );
        }

        public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetFrame(
                Float64Vector3D.Create(parameterValue1, parameterValue2, parameterValue3)
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
        public virtual Float64Vector3D ComputeSdfNormal(IFloat64Vector3D point)
        {
            var d1 = GetScalarDistance(Float64Vector3D.Create(point.X + SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z - SdfDistanceDelta));

            var d2 = GetScalarDistance(Float64Vector3D.Create(point.X - SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z + SdfDistanceDelta));

            var d3 = GetScalarDistance(Float64Vector3D.Create(point.X - SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z - SdfDistanceDelta));

            var d4 = GetScalarDistance(Float64Vector3D.Create(point.X + SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z + SdfDistanceDelta));


            return Float64Vector3D.Create(d4 + d1 - d2 - d3,
                d4 - d1 - d2 + d3,
                d4 - d1 + d2 - d3).ToUnitVector();
        }


        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)Scalars).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)Scalars).GetEnumerator();
        }
    }
}

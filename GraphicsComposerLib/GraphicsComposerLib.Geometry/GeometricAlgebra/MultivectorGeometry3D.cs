using System;
using System.Collections;
using System.Collections.Generic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.ParametricShapes.Volumes;
using GraphicsComposerLib.Geometry.SdfShapes;

namespace GraphicsComposerLib.Geometry.GeometricAlgebra
{
    public abstract class MultivectorGeometry3D
        : ISdfGeometry3D, IReadOnlyList<double>
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


        protected abstract double ComputeSdfOpns(ITuple3D point);

        protected abstract double ComputeSdfIpns(ITuple3D point);


        protected double CorrectSdf(double sdf)
        {
            sdf = sdf < 0 ? Math.Sqrt(-sdf) : Math.Sqrt(sdf);
            return SdfAlpha * sdf - SdfDelta;
        }
        

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Tuple3D GetPoint(ITuple3D parameterValue)
        {
            return parameterValue.ToTuple3D();
        }

        public Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return new Tuple3D(parameterValue1, parameterValue2, parameterValue3);
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual double GetScalarDistance(ITuple3D point)
        {
            var sdf = NullSpaceKind == MultivectorNullSpaceKind.OuterProductNullSpace
                ? ComputeSdfOpns(point)
                : ComputeSdfIpns(point);

            return CorrectSdf(sdf);
        }

        public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetScalarDistance(new Tuple3D(
                parameterValue1,
                parameterValue2,
                parameterValue3
            ));
        }

        public GrParametricVolumeLocalFrame3D GetFrame(ITuple3D parameterValue)
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
                new Tuple3D(parameterValue1, parameterValue2, parameterValue3)
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
        public virtual Tuple3D ComputeSdfNormal(ITuple3D point)
        {
            var d1 = GetScalarDistance(new Tuple3D(
                point.X + SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d2 = GetScalarDistance(new Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z + SdfDistanceDelta
            ));

            var d3 = GetScalarDistance(new Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d4 = GetScalarDistance(new Tuple3D(
                point.X + SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z + SdfDistanceDelta
            ));


            return new Tuple3D(
                d4 + d1 - d2 - d3,
                d4 - d1 - d2 + d3,
                d4 - d1 + d2 - d3
            ).ToUnitVector();
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

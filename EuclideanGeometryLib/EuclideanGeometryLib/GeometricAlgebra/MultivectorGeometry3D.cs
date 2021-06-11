using System;
using System.Collections;
using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.SdfGeometry;

namespace EuclideanGeometryLib.GeometricAlgebra
{
    public abstract class MultivectorGeometry3D
        : ISdfGeometry3D, IReadOnlyList<double>
    {
    
        private double _distanceDeltaInv = 1 << 13;
        private double _distanceDelta = 1.0d / (1 << 13);

        
        protected double[] Scalars { get; }

        public int Count 
            => Scalars.Length;

        public double this[int index] 
            => Scalars[index];

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


        protected abstract double ComputeSdfOpns(Tuple3D point);

        protected abstract double ComputeSdfIpns(Tuple3D point);


        protected double CorrectSdf(double sdf)
        {
            sdf = sdf < 0 ? Math.Sqrt(-sdf) : Math.Sqrt(sdf);
            return SdfAlpha * sdf - SdfDelta;
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual double ComputeSdf(Tuple3D point)
        {
            var sdf = NullSpaceKind == MultivectorNullSpaceKind.OuterProductNullSpace
                ? ComputeSdfOpns(point)
                : ComputeSdfIpns(point);

            return CorrectSdf(sdf);
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Newton%27s_method
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t0"></param>
        /// <returns></returns>
        public virtual double ComputeSdfRayStep(Line3D ray, double t0)
        {
            var f0 = ComputeSdf(ray.GetPointAt(t0));
            var f1 = ComputeSdf(ray.GetPointAt(t0 + _distanceDelta));

            return _distanceDelta * f0 / (f0 - f1);
        }

        /// <summary>
        /// http://iquilezles.org/www/articles/normalsSDF/normalsSDF.htm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual Tuple3D ComputeSdfNormal(Tuple3D point)
        {
            var d1 = ComputeSdf(new Tuple3D(
                point.X + SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d2 = ComputeSdf(new Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y - SdfDistanceDelta,
                point.Z + SdfDistanceDelta
            ));

            var d3 = ComputeSdf(new Tuple3D(
                point.X - SdfDistanceDelta,
                point.Y + SdfDistanceDelta,
                point.Z - SdfDistanceDelta
            ));

            var d4 = ComputeSdf(new Tuple3D(
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

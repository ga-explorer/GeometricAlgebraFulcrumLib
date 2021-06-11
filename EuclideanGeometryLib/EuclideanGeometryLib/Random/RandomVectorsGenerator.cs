using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.Random
{
    /// <summary>
    /// A factory for generating randomly 2D\3D vectors using polar\spherical coordinates with 
    /// parametric angles uniformly distributed
    /// </summary>
    public class RandomVectorsGenerator : RandomGeometryGenerator
    {
        //TODO: Create a BoundingSphericalBox class to hold this information
        public double RMin { get; set; } 
            = 0.0d;

        public double RMax { get; set; } 
            = 1.0d;

        public double RRange 
            => RMax - RMin;

        public double ThetaMin { get; set; } 
            = 0.0d;

        public double ThetaMax { get; set; } 
            = MathNet.Numerics.Constants.Pi;

        public double ThetaRange 
            => ThetaMax - ThetaMin;

        public double PhiMin { get; set; } 
            = 0.0d;

        public double PhiMax { get; set; } 
            = MathNet.Numerics.Constants.Pi2;

        public double PhiRange 
            => PhiMax - PhiMin;


        public RandomVectorsGenerator() 
        {
        }

        public RandomVectorsGenerator(int seed) : base(seed)
        {
        }


        public Tuple2D GetRandomUnitVector2Dp()
        {
            var phi = PhiMin + GetNumber() * PhiRange;
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple2D(
                cosPhi,
                sinPhi
            );
        }

        public Tuple2D GetRandomVector2Dp()
        {
            var r = RMin + GetNumber() * RRange;
            var phi = PhiMin + GetNumber() * PhiRange;
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple2D(
                r * cosPhi,
                r * sinPhi
            );
        }

        public Tuple3D GetRandomUnitVector3Dp()
        {
            var theta = ThetaMin + GetNumber() * ThetaRange;
            var phi = PhiMin + GetNumber() * PhiRange;
            var cosTheta = Math.Cos(theta);
            var sinTheta = Math.Sin(theta);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple3D(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        public Tuple3D GetRandomVector3Dp()
        {
            var r = RMin + GetNumber() * RRange;
            var theta = ThetaMin + GetNumber() * ThetaRange;
            var phi = PhiMin + GetNumber() * PhiRange;
            var cosTheta = Math.Cos(theta);
            var sinTheta = Math.Sin(theta);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);
            
            return new Tuple3D(
                r * sinTheta * cosPhi,
                r * sinTheta * sinPhi,
                r * cosTheta
            );
        }


        //public VectorFrame3Dp GetRandomFrame3Dp()
        //{
        //    var rotationVector = GetRandomUnitVector3Dp();
        //    var rotationAngle = GetNumber() * FloatDpUtils.TwoPi;
        //    var quaternion = rotationVector.ToRotationQuaternion(rotationAngle);

        //}


        public Tuple2D[] GetRandomUnitVectors2Dp(int count)
        {
            var vectorsList = new Tuple2D[count];

            for (var i = 0; i < count; i++)
                vectorsList[i] = GetRandomUnitVector2Dp();

            return vectorsList;
        }

        public Tuple2D[] GetRandomVectors2Dp(int count)
        {
            var vectorsList = new Tuple2D[count];

            for (var i = 0; i < count; i++)
                vectorsList[i] = GetRandomVector2Dp();

            return vectorsList;
        }

        public Tuple3D[] GetRandomUnitVectors3Dp(int count)
        {
            var vectorsList = new Tuple3D[count];

            for (var i = 0; i < count; i++)
                vectorsList[i] = GetRandomUnitVector3Dp();

            return vectorsList;
        }

        public Tuple3D[] GetRandomVectors3Dp(int count)
        {
            var vectorsList = new Tuple3D[count];

            for (var i = 0; i < count; i++)
                vectorsList[i] = GetRandomVector3Dp();

            return vectorsList;
        }
    }
}

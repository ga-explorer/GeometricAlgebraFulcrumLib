using System;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class QuaternionSamples
    {
        public static void ValidateNearZero(this string textDescription, double value, double epsilon = 1e-12d)
        {
            if (value.IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value: {value:G}");
            Console.WriteLine();
        }
        
        public static void ValidateNearZero(this string textDescription, Float64Bivector3D value, double epsilon = 1e-12d)
        {
            if (value.IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value}");
            Console.WriteLine();
        }


        public static void ValidateNearEqual(this string textDescription, double value1, double value2, double epsilon = 1e-12d)
        {
            if (value1.IsNearEqual(value2, epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1:G}");
            Console.WriteLine($"   Value2: {value2:G}");
            Console.WriteLine();
        }
        
        public static void ValidateNearEqual(this string textDescription, Float64PlanarAngle value1, Float64PlanarAngle value2, double epsilon = 1e-12d)
        {
            if (value1.IsNearEqual(value2, epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1}");
            Console.WriteLine($"   Value2: {value2}");
            Console.WriteLine();
        }

        public static void ValidateNearEqual(this string textDescription, Float64Vector3D value1, Float64Vector3D value2, double epsilon = 1e-12d)
        {
            if ((value1 - value2).IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1}");
            Console.WriteLine($"   Value2: {value2}");
            Console.WriteLine();
        }

        public static void ValidateNearEqual(this string textDescription, Float64Bivector3D value1, Float64Bivector3D value2, double epsilon = 1e-12d)
        {
            if ((value1 - value2).IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1}");
            Console.WriteLine($"   Value2: {value2}");
            Console.WriteLine();
        }
        
        public static void ValidateNearEqual(this string textDescription, Float64Quaternion value1, Float64Quaternion value2, double epsilon = 1e-12d)
        {
            if ((value1 - value2).IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1}");
            Console.WriteLine($"   Value2: {value2}");
            Console.WriteLine();
        }
        
        public static void ValidateNearEqual(this string textDescription, Float64Multivector3D value1, Float64Multivector3D value2, double epsilon = 1e-12d)
        {
            if ((value1 - value2).IsNearZero(epsilon))
                return;

            Console.WriteLine(textDescription);
            Console.WriteLine($"   Value1: {value1}");
            Console.WriteLine($"   Value2: {value2}");
            Console.WriteLine();
        }


        public static void RotationAngleValidations()
        {
            var randomGen = new Random(10);

            for (var j = 0; j < 100; j++)
            {
                var angleFullValue =
                    randomGen.NextDouble() * 720d - 360d;

                var anglePositiveValue =
                    angleFullValue < 0 ? angleFullValue + 360 : angleFullValue;

                var angleNegativeValue =
                    angleFullValue > 0 ? angleFullValue - 360 : angleFullValue;

                var angleSymmetricValue = angleFullValue switch
                {
                    < -180 => angleFullValue + 360,
                    > 180 => angleFullValue - 360,
                    _ => angleFullValue
                };

                //Console.WriteLine($"     Angle in Full Range: {angleFullValue:G} degrees");
                //Console.WriteLine($" Angle in Positive Range: {anglePositiveValue:G} degrees");
                //Console.WriteLine($" Angle in Negative Range: {angleNegativeValue:G} degrees");
                //Console.WriteLine($"Angle in Symmetric Range: {angleSymmetricValue:G} degrees");

                for (var i = -10; i <= 10; i++)
                {
                    var angleValue = 360 * i + anglePositiveValue;

                    //var angleFull = angleValue.DegreesToAngle(Float64PlanarAngleRange.Full);
                    var anglePositive = angleValue.DegreesToAngle(Float64PlanarAngleRange.Positive);
                    var angleNegative = angleValue.DegreesToAngle(Float64PlanarAngleRange.Negative);
                    var angleSymmetric = angleValue.DegreesToAngle(Float64PlanarAngleRange.Symmetric);

                    "Angle in Positive Range".ValidateNearEqual(anglePositiveValue, anglePositive.Degrees);
                    "Angle in Negative Range".ValidateNearEqual(angleNegativeValue, angleNegative.Degrees);
                    "Angle in Symmetric Range".ValidateNearEqual(angleSymmetricValue, angleSymmetric.Degrees);

                    //Console.WriteLine(angleFull.ToString());
                    //Console.WriteLine(anglePositive.ToString());
                    //Console.WriteLine(angleNegative.ToString());
                    //Console.WriteLine(angleSymmetric.ToString());
                }
            }
        }
        
        public static void RotationPlaneValidations()
        {
            var randomGen = new Random(10);

            var axisArray = new []
            {
                LinUnitBasisVector3D.PositiveX,
                LinUnitBasisVector3D.NegativeX,

                LinUnitBasisVector3D.PositiveY,
                LinUnitBasisVector3D.NegativeY,

                LinUnitBasisVector3D.PositiveZ,
                LinUnitBasisVector3D.NegativeZ,
            };

            foreach (var axis in axisArray)
            {
                var axisVector = axis.ToVector3D();

                var bivector1 = axis.UnDual3D();
                var bivector2 = axisVector.UnDual3D();

                "Axis UnDual".ValidateNearEqual(bivector1, bivector2);

                bivector1 = axis.Dual3D();
                bivector2 = axisVector.Dual3D();

                "Axis Dual".ValidateNearEqual(bivector1, bivector2);
            }

            for (var i = 0; i < 100; i++)
            {
                var b1v1 = randomGen.GetVector3D().ToUnitVector();
                var b1v2 = randomGen.GetVector3D().RejectOnUnitVector(b1v1).ToUnitVector();
                var b1 = b1v1.Op(b1v2);

                var n1 = b1v1.VectorCross(b1v2);
                var n2 = b1.Dual3D();

                var b2 = n2.UnDual3D();

                // Validate bivector normal using duality
                "Bivector Normal using Duality".ValidateNearEqual(n1, n2);
                "Vector Normal using Duality".ValidateNearEqual(b1, b2);
                

                // Validate vB = -Bv for bivector B and vector v in the
                // plane of the bivector
                var (v1, v2) = 
                    randomGen.GetOrthonormalVectorPair3D();

                var b = v1.Op(v2);

                var v = 
                    randomGen.GetLinearCombination(v1, v2);

                var vb = v.Gp(b);
                var bv = b.Gp(v);

                "Vector-Bivector Product".ValidateNearEqual(-vb, bv);
            }
        }

        public static void ConstructionValidations()
        {
            var randomGen = new Random(10);

            for (var i = 0; i < 100; i++)
            {
                // Validate quaternion construction from\to a scalar and bivector
                var q1 = randomGen.GetQuaternion();
                var (s1, b1) = q1.GetScalarAndBivector();

                "Quaternion Scalar".ValidateNearEqual(q1.Scalar, s1.Scalar);
                "Quaternion Scalar i".ValidateNearEqual(q1.ScalarI, -b1.Scalar23);
                "Quaternion Scalar j".ValidateNearEqual(q1.ScalarJ, b1.Scalar13);
                "Quaternion Scalar k".ValidateNearEqual(q1.ScalarK, -b1.Scalar12);

                var s2 = randomGen.GetScalar3D();
                var b2 = randomGen.GetBivector3D();

                var q2 = Float64Quaternion.Create(s2.Scalar, b2);

                "Quaternion Scalar".ValidateNearEqual(q2.Scalar, s2.Scalar);
                "Quaternion Bivector".ValidateNearEqual(q2.GetBivector(), b2);


                // Validate quaternion conjugate (i.e. reverse)
                q2 = q1.Conjugate();

                "Quaternion Conjugate Scalar".ValidateNearEqual(q1.Scalar, q2.Scalar);
                "Quaternion Conjugate Bivector".ValidateNearEqual(q1.GetBivector(), -q2.GetBivector());


                // Validate the square norm
                var q1Norm1 = q1.NormSquared();
                var q1Norm2 = q1 * q1.Conjugate();

                "Quaternion Norm Scalar".ValidateNearEqual(q1Norm1, q1Norm2.Scalar);
                "Quaternion Norm Bivector".ValidateNearZero(q1Norm2.GetBivector());

                q1Norm2 = q1.Conjugate() * q1;
                
                "Quaternion Norm Scalar".ValidateNearEqual(q1Norm1, q1Norm2.Scalar);
                "Quaternion Norm Bivector".ValidateNearZero(q1Norm2.GetBivector());


                // Validate the inverse
                var q1Inv1 = q1.Inverse();
                var q1Inv2 = q1.Reverse() / q1.NormSquared();

                "Quaternion Inverse".ValidateNearEqual(q1Inv1, q1Inv2);


                // Validate normalized quaternion
                q2 = q1.Normalize();

                "Quaternion Normalized".ValidateNearEqual(q2.Norm(), 1d);

                // Validate quaternion construction from\to a scalar and bivector
                q1 = randomGen.GetQuaternion().Normalize();

                var (angle, normal) = q1.GetAngleAndNormal();

                q2 = Float64Quaternion.CreateFromNormalAndAngle(normal, angle);

                "Quaternion From Normal and Angle".ValidateNearEqual(q1, q2);
                
                q2 = Float64Quaternion.CreateFromNormalAndAngle(
                    -normal, 
                    Float64PlanarAngle.Angle360 - angle
                );

                "Quaternion From Negative Normal and Angle".ValidateNearEqual(q1, -q2);

                var (angle1, bivector1) = q1.GetAngleAndBivector();

                q2 = Float64Quaternion.CreateFromPlaneAndAngle(bivector1, angle1);

                "Quaternion From Plane and Angle".ValidateNearEqual(q1, q2);
                
                q2 = Float64Quaternion.CreateFromPlaneAndAngle(
                    -bivector1, 
                    Float64PlanarAngle.Angle360 - angle1
                );

                "Quaternion From Negative Plane and Angle".ValidateNearEqual(q1, -q2);


            }
        }

        public static void RotationValidations()
        {
            var randomGen = new Random(10);

            for (var i = 0; i < 100; i++)
            {
                var (basisVector1, basisVector2) = 
                    randomGen.GetOrthonormalVectorPair3D();

                var angle = randomGen.GetAngle();
                var bivector = basisVector1.Op(basisVector2);

                var length = randomGen.GetNumber(10);
                var v1 = length * basisVector1;
                var v2 = 
                    length * angle.Cos() * basisVector1 + 
                    length * angle.Sin() * basisVector2;

                var quaternion = Float64Quaternion.CreateFromPlaneAndAngle(
                    bivector,
                    angle
                );

                var v3 = 
                    quaternion.RotateVector(v1);

                "Vector Rotation".ValidateNearEqual(v2, v3);

                var (e1, e2, e3) = 
                    quaternion.RotateBasisVectors();

                v3 = v1.X * e1 + v1.Y * e2 + v1.Z * e3;
                
                "Basis Vectors Rotation".ValidateNearEqual(v2, v3);
            }
        }

        public static void Validations()
        {
            RotationAngleValidations();
            ConstructionValidations();
            RotationPlaneValidations();
            RotationValidations();
        }
    }
}

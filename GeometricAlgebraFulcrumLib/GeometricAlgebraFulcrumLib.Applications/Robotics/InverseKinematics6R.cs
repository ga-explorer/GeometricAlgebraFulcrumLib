using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Applications.Robotics
{
    public static class InverseKinematics6RSamples
    {
        public static void Example1()
        {
            var cga = CGaFloat64GeometricSpace5D.Instance;

            // CGA null basis vectors
            var eo = cga.Eo;
            var ei = cga.Ei;

            // define the Staübli robot link lengths
            const double d1 = 480d;
            const double a3 = 425d;
            const double d4 = 425d;

            // desired configuration
            var (theta1d, theta2d, theta3d) = (0.4375, 0.8590, 1.5040);

            // CGA null vector of the desired position of end-effector
            var cgaP = cga.Encode.IpnsRound.Point(561.8479, 262.7685, 455.0104);

            // point at the origin
            var cgaP0 = eo;

            // compute p1 (translation of p0)
            var cgaP1 = cgaP0.TranslateBy(0, 0, d1);

            // plane passing by p0, p1 and desired position of end-effector
            var plane = cgaP0.Op(cgaP1).Op(cgaP).Op(ei);

            // sphere center = p1 and radius = a3
            var cgaSphere1 = cgaP1 - 0.5 * a3.Square() * ei;

            // sphere center = position and radius = d4
            var cgaSphere2 = cgaP - 0.5 * d4.Square() * ei;

            // intersection of the two spheres results in a circle
            var cgaCircle = cgaSphere1.Op(cgaSphere2); //.RemoveNearZeroTerms();

            // intersection of the plane and circle results in a pair of points
            var cgaPointPair = plane.CGaDual().Op(cgaCircle);

            // extract one point from point pair, the other is completely analogous
            var cgaP2 = cgaPointPair.DecodeIpnsRound.PointPairIpnsPoint1();

            // normal vector to plane
            var normal = plane.DecodeOpnsFlat.VGaNormalVector3D();

            // first joint angle
            var theta1 = normal.GetAngleWithUnit(LinFloat64Vector3D.E1) - LinFloat64PolarAngle.Angle90;

            // auxiliary lines l1, l2, l3
            var cgaLine1 = cgaP0.Op(cgaP1).Op(ei);
            var cgaLine2 = cgaP1.Op(cgaP2).Op(ei);
            var cgaLine3 = cgaP2.Op(cgaP).Op(ei);

            // square roots of modules of auxiliary lines l1, l2, l3
            var l11 = cgaLine1.SpSquared().Sqrt();
            var l22 = cgaLine2.SpSquared().Sqrt();
            var l33 = cgaLine3.SpSquared().Sqrt();

            // cos angle between lines l1 and l2
            var theta2Cos = cgaLine2.Sp(cgaLine1) / (l11 * l22);

            // cos angle between lines l2 and l3
            var theta3Cos = cgaLine2.Sp(cgaLine3) / (l22 * l33);

            // second and third joint angles
            var theta2 = LinFloat64PolarAngle.CreateFromCos(theta2Cos);
            var theta3 = LinFloat64PolarAngle.CreateFromCos(theta3Cos);

            Console.WriteLine(theta1.RadiansValue);
            Console.WriteLine(theta2.RadiansValue);
            Console.WriteLine(theta3.RadiansValue);
        }
    }
}

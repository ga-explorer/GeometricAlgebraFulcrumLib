using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Geometry.Conformal;

public static class CGa4DBasicSamples
{
    public static RGaConformalSpace4D Ga
        => RGaConformalSpace4D.Instance;

    public static void EncodingSamples()
    {
        // Encoding a geometric object is to convert the components of the object
        // into a blade in CGA space.
        // Decoding the object is to convert the CGA blade into the object
        // components.
        // Components of an object are, for example, the radius and center of a circle
        // in 2D, or the weight, normal, and position of a weighted flat plane in 3D.

        var cga = RGaConformalSpace4D.Instance;

        var cgaScalar = cga.EncodeScalar(2.5);

        Console.WriteLine("Original Scalar: 2.5");
        Console.WriteLine($"Encoded Scalar : {cgaScalar}");
        Console.WriteLine($"Decoded Scalar : {cgaScalar.DecodeScalar()}");
        Console.WriteLine();

        var cgaVector = cga.EncodeEGaVector(-1.3, 2.8);

        Console.WriteLine("Original Vector: (-1.3)<1> + (2.8)<2>");
        Console.WriteLine($"Encoded Vector : {cgaVector.ToLaTeX()}");
        Console.WriteLine($"Decoded Vector : {cgaVector.DecodeEGaVector2D()}");
        Console.WriteLine();

        var cgaBivector = cga.EncodeEGaBivector(-3.4);

        Console.WriteLine("Original Bivector: (-3.4)<1,2>");
        Console.WriteLine($"Encoded Bivector : {cgaBivector.ToLaTeX()}");
        Console.WriteLine($"Decoded Bivector : {cgaBivector.DecodeEGaBivector2D()}");
        Console.WriteLine();

        //var cgaPoint = 1 * cga.EncodePoint(-4, 2);
        var cgaPoint = 3.7 * cga.EncodeIpnsRoundPoint(-1.3, 2.8);
        var (w1, p1) = cgaPoint.DecodeIpnsCircleWeightEGaCenter2D();
        var point = cgaPoint.DecodeIpnsCircleEGaCenter2D();

        Console.WriteLine("Original Point: Weight: 3.7, Position: (-1.3)<1> + (2.8)<2>");
        Console.WriteLine($"Encoded Point : {cgaPoint.ToLaTeX()}");
        Console.WriteLine($"Decoded Point : {point}");
        Console.WriteLine($"Decoded Weighted Point: Weight: {w1:G10}, Position: {p1}");
        Console.WriteLine();

        var cgaSphere = 2.4 * cga.EncodeIpnsRealRoundCircle(4, -1.3, 2.8);
        var sphere = cgaSphere.DecodeIpnsRound();
        var radius = cgaSphere.DecodeIpnsRoundRadius();
        var center = cgaSphere.DecodeIpnsCircleEGaCenter2D();

        Console.WriteLine("Original Sphere: Weight: 2.4, Radius: 4, Center: (-1.3, 2.8)");
        Console.WriteLine($"Encoded Sphere : {cgaSphere.ToLaTeX()}");
        Console.WriteLine($"Decoded Sphere : Radius: {radius:G10}, Center: {center}");
        Console.WriteLine($"Decoded Weighted Sphere: Weight: {sphere.Weight:G10}, Radius: {sphere.RealRadius:G10}, Center: {sphere.CenterToVector2D()}");
        Console.WriteLine();

        var n = LinFloat64Vector2D.CreateUnitVector(-1.2, -3.4);
        var cgaLine = -4.1 * cga.EncodeIpnsFlatLine(3.5, n);
        var line = cgaLine.DecodeIpnsFlat();
        var distance = cgaLine.DecodeIpnsHyperPlaneEGaPosition().Norm();
        var normal = cgaLine.DecodeIpnsHyperPlaneEGaNormalDirection();

        Console.WriteLine($"Original Plane: Weight: -4.1, Distance: 3.5, Unit Normal: {n}");
        Console.WriteLine($"Encoded Plane : {cgaLine.ToLaTeX()}");
        Console.WriteLine($"Decoded Plane : Distance: {distance:G10}, Normal: {normal}");
        Console.WriteLine($"Decoded Weighted Plane: Weight: {line.Weight:G10}, Distance: {line.OriginToHyperPlaneDistance}, Normal: {line.NormalDirectionToVector2D()}");
        Console.WriteLine();

    }

    public static void PointsExample()
    {
        var cga = RGaConformalSpace4D.Instance;

        var cgaPoint1 =
            cga.EncodeIpnsRoundPoint(-1, -1);

        var cgaPoint2 =
            cga.EncodeIpnsRoundPoint(2, 3);

        var squaredDistance =
            cgaPoint1.GetIpnsDistance(cgaPoint2);

        Console.WriteLine("CGA Point 1:" + cgaPoint1.ToLaTeX());
        Console.WriteLine("CGA Point 2:" + cgaPoint2.ToLaTeX());
        Console.WriteLine("Squared Distance: " + squaredDistance);
        Console.WriteLine();

        var egaPoint1 = cgaPoint1.DecodeIpnsCircleEGaCenter2D();
        var egaPoint2 = cgaPoint2.DecodeIpnsCircleEGaCenter2D();

        Console.WriteLine("EGA Point 1:" + egaPoint1);
        Console.WriteLine("EGA Point 2:" + egaPoint2);
        Console.WriteLine();

    }

    public static void CirclesExample()
    {
        var cga = RGaConformalSpace4D.Instance;

        var cgaCircle1 =
            cga.EncodeIpnsRealRoundCircle(
                4,
                LinFloat64Vector2D.Create(-1, 2)
            );

        var cgaLine =
            cga.EncodeOpnsFlatLine(
                LinFloat64Vector2D.Create(-1, 1),
                LinFloat64Vector2D.Create(1, -1)
            );

        var cgaCircle2 =
            cgaLine.Gp(cgaCircle1).Gp(cgaLine)
                .VectorPartToConformalBlade(cga)
                .NormalizeIpnsVector();

        Console.WriteLine("CGA Circle 1:" + cgaCircle1.ToLaTeX());
        Console.WriteLine("CGA Line:" + cgaLine.ToLaTeX());
        Console.WriteLine("CGA Circle 2:" + cgaCircle2.ToLaTeX());
        Console.WriteLine();

        var radius1 = cgaCircle1.DecodeIpnsRoundRadius();
        var center1 = cgaCircle1.DecodeIpnsCircleEGaCenter2D();

        var radius2 = cgaCircle2.DecodeIpnsRoundRadius();
        var center2 = cgaCircle2.DecodeIpnsCircleEGaCenter2D();

        Console.WriteLine($"Circle 1: center = {center1}, radius = {radius1}");
        Console.WriteLine($"Circle 2: center = {center2}, radius = {radius2}");
        Console.WriteLine();
    }
}
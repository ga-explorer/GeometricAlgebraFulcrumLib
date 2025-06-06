﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Geometry.CGa;

public static class CGa5DBasicSamples
{
    public static CGaFloat64GeometricSpace5D Ga
        => CGaFloat64GeometricSpace5D.Instance;


    /// <summary>
    /// Display basis blades for CGA and its sub-algebras
    /// </summary>
    public static void CGaBasisExample()
    {
        var egaBasisBlades =
            Ga.GetBasisBladesVGa();

        var pgaBasisBlades =
            Ga.GetBasisBladesPGa();

        var cgaInfBasisBlades =
            Ga.GetBasisBladesCGaInf();

        var cgaBasisBlades =
            Ga.GetBasisBladesCGa();

        Console.WriteLine("EGA Basis Blades:");

        foreach (var kv in egaBasisBlades)
            Console.WriteLine(kv.ToLaTeX());

        Console.WriteLine();

        Console.WriteLine("PGA (= CGAo) Basis Blades:");

        foreach (var kv in pgaBasisBlades)
            Console.WriteLine($"{kv.ToLaTeX()} (PGA Dual = {kv.PGaDual().ToLaTeX()})");

        Console.WriteLine();

        Console.WriteLine("CGAi Basis Blades:");

        foreach (var kv in cgaInfBasisBlades)
            Console.WriteLine(kv.ToLaTeX());

        Console.WriteLine();

        Console.WriteLine("CGA Basis Blades:");

        foreach (var kv in cgaBasisBlades)
            Console.WriteLine(kv.ToLaTeX());

        Console.WriteLine();
    }
    
    
    public static void BasisInfoTableExample()
    {
        var cgaBasisSpecs = GaFloat64GeometricSpaceBasisSpecs.CreateCGa(5);

        var basisInfoTable = cgaBasisSpecs.GetBasisInfoMarkdownTable();

        Console.WriteLine(basisInfoTable);
        Console.WriteLine();
    }

    public static void ProductTablesExample()
    {
        var cgaBasisSpecs = GaFloat64GeometricSpaceBasisSpecs.CreateVGa(4);

        var gpTable = cgaBasisSpecs.GetBasisMapMarkdownTable(
            (a, b) => a.Cp(b)
        );

        Console.WriteLine(gpTable);
        Console.WriteLine();
    }

    public static void TranslationExamples()
    {
        var ga = CGaFloat64GeometricSpace5D.Instance;

        var point1 =
            ga.Encode.IpnsRound.Point(-1.4, 2.4, 5.2);

        var point2 =
            point1.TranslateBy(
                LinFloat64Vector3D.Create(1, -2, -5)
            );

        Console.WriteLine($"${point1.DecodeIpnsRound.HyperSphereVGaCenter().ToLaTeX()}$");
        Console.WriteLine($"${point2.DecodeIpnsRound.HyperSphereVGaCenter().ToLaTeX()}$");
        Console.WriteLine();
    }


    public static void CGaEncodingExamples()
    {
        // Encoding a geometric object is to convert the components of the object
        // into a multivector in CGA space.
        // Decoding the object is to convert the CGA multivector into the object
        // components.
        // Components of an object are, for example, the radius and center of a sphere
        // or the weight and coordinates of a weighted point.

        var ga = CGaFloat64GeometricSpace5D.Instance;

        var cgaScalar = ga.Encode.Scalar(2.5);

        Console.WriteLine("Original Scalar: 2.5");
        Console.WriteLine($"Encoded Scalar : {cgaScalar}");
        Console.WriteLine($"Decoded Scalar : {cgaScalar.DecodeScalar()}");
        Console.WriteLine();

        var cgaVector =
            ga.Encode.VGa.Vector(-1.3, 2.8, 1.4);

        Console.WriteLine("Original Vector: (-1.3)<1> + (2.8)<2> + (1.4)<3>");
        Console.WriteLine($"Encoded Vector : {cgaVector}");
        Console.WriteLine($"Decoded Vector : {cgaVector.DecodeVGaDirection.Vector3D()}");
        Console.WriteLine();

        var cgaBivector =
            ga.Encode.VGa.Bivector(-3.4, -1.4, 2.5);

        Console.WriteLine("Original Bivector: (-3.4)<1,2> + (-1.4)<1,3> + (2.5)<2,3>");
        Console.WriteLine($"Encoded Bivector : {cgaBivector}");
        Console.WriteLine($"Decoded Bivector : {cgaBivector.DecodeVGaDirection.Bivector3D()}");
        Console.WriteLine();

        var cgaPoint = 3.7 * ga.Encode.IpnsRound.Point(-1.3, 2.8, 1.4);
        var (w1, p1) = cgaPoint.DecodeIpnsRound.SphereWeightVGaCenter3D();
        var point = cgaPoint.DecodeIpnsRound.HyperSphereVGaCenter();

        Console.WriteLine("Original Point: Weight: 3.7, Position: (-1.3)<1> + (2.8)<2> + (1.4)<3>");
        Console.WriteLine($"Encoded Point : {cgaPoint}");
        Console.WriteLine($"Decoded Point : {point}");
        Console.WriteLine($"Decoded Weighted Point: Weight: {w1:G10}, Position: {p1}");
        Console.WriteLine();

        var cgaSphere = 2.4 * ga.Encode.IpnsRound.RealSphere(4, -1.3, 2.8, 1.4);
        //var sphere = ga.DecodeIpnsSphere3D(cgaSphere);
        var sphere = cgaSphere.DecodeIpnsRound.Element();

        Console.WriteLine("Original Sphere: Weight: 2.4, Radius: 4, Center: (-1.3, 2.8, 1.4)");
        Console.WriteLine($"Encoded Sphere: {cgaSphere}");
        Console.WriteLine($"Decoded Sphere: Weight: {sphere.Weight:G10}, Radius: {sphere.RealRadius:G10}, Center: {sphere.CenterToVector3D()}");
        Console.WriteLine();

        var n = LinFloat64Vector3D.CreateUnitVector(-1.2, -3.4, 1.4);
        var cgaPlane = -4.1 * ga.Encode.IpnsFlat.Plane(3.5, n);
        var plane = cgaPlane.DecodeIpnsFlat.Plane3D();

        Console.WriteLine($"Original Plane: Weight: -4.1, Distance: 3.5, Unit Normal: {n}");
        Console.WriteLine($"Encoded Plane : {cgaPlane}");
        Console.WriteLine($"Decoded Weighted Plane: Weight: {plane.Weight:G10}, Distance: {plane.OriginToHyperPlaneDistance:G10}, Normal: {plane.NormalDirectionToVector3D()}");
        Console.WriteLine();

        var cgaLine = ga.Encode.IpnsFlat.Line(
            LinFloat64Vector3D.Create(-1.2, -3.4, 1.4),
            LinFloat64Vector3D.Create(-1, 1, 1)
        );

        Console.WriteLine($"Original Line: Weight: 1, Point: (-1.2, -3.4, 1.4), Direction: (-1, 1, 1)");
        Console.WriteLine($"Encoded Line : {cgaLine}");
        //Console.WriteLine($"Decoded Plane : Distance: {distance:G10}, Normal: {normal}");
        //Console.WriteLine($"Decoded Weighted Plane: Weight: {w3:G10}, Distance: {d3:G10}, Normal: {n3}");
        Console.WriteLine();
    }

    public static void SphereExample()
    {
        var ga = CGaFloat64GeometricSpace5D.Instance;

        // Create 4 position vectors (points)
        var p1 = LinFloat64Vector3D.Create(0.5, 0, -1.5);
        var p2 = LinFloat64Vector3D.Create(1, 0, 0);
        var p3 = LinFloat64Vector3D.Create(0, 1, 0);
        var p4 = LinFloat64Vector3D.Create(0, 0, -1);

        // Encode the real sphere passing through the 4 points as a
        // OPNS blade
        var opnsSphere1 =
            ga.Encode.OpnsRound.Sphere(p1, p2, p3, p4);

        // Convert the OPNS blade into IPNS vector
        var ipnsSphere1 =
            opnsSphere1.OpnsToIpns();

        // Decode the sphere parameters of the IPNS
        var sphere1 =
            ipnsSphere1.DecodeIpnsRound.Sphere3D();

        Console.WriteLine($"OPNS Sphere: {opnsSphere1}");
        Console.WriteLine($"IPNS Sphere: {ipnsSphere1}");
        Console.WriteLine($"     Weight: {sphere1.Weight}");
        Console.WriteLine($"     Radius: {sphere1.RealRadius}");
        Console.WriteLine($"     Center: {sphere1.CenterToVector3D()}");
        Console.WriteLine();

        Console.WriteLine($"  Distance1: {sphere1.CenterToVector3D().GetDistanceToPoint(p1)}");
        Console.WriteLine($"  Distance2: {sphere1.CenterToVector3D().GetDistanceToPoint(p2)}");
        Console.WriteLine($"  Distance3: {sphere1.CenterToVector3D().GetDistanceToPoint(p3)}");
        Console.WriteLine($"  Distance3: {sphere1.CenterToVector3D().GetDistanceToPoint(p4)}");
        Console.WriteLine();
    }

    public static void PGaExamples()
    {
        var ga = CGaFloat64GeometricSpace5D.Instance;

        var io = ga.EoIe;
        var ii = ga.IeEi;
        var ios = io.MapUsingMusicalIsomorphism();
        var iis = ii.MapUsingMusicalIsomorphism();

        Console.WriteLine($"Io123 = {io}");
        Console.WriteLine($"I123i = {ii}");
        Console.WriteLine($"#[Io123] = {ios}");
        Console.WriteLine($"#[I123i] = {iis}");
        Console.WriteLine();


    }

    public static void CGaPGaExample()
    {
        var ga = CGaFloat64GeometricSpace5D.Instance;

        // Create 4 3D position vectors (points)
        var p = LinFloat64Vector3D.Create(0.5, 0, -1.5);
        var p1 = LinFloat64Vector3D.Create(1, 0, 0);
        var p2 = LinFloat64Vector3D.Create(0, 1, 0);
        var p3 = LinFloat64Vector3D.Create(0, 0, -1);

        // Encode a point as a PGA blade
        var pgaPoint = ga.Encode.PGa.Point(p);

        // Encode a line passing through two points as a PGA blade
        var pgaLine =
            ga.Encode.PGa.LineFromPoints(p, p2);

        // Encode a plane passing through three points as a PGA blade
        var pgaPlane =
            ga.Encode.PGa.PlaneFromPoints(p1, p2, p3);

        // Encode a real sphere as an IPNS CGA vector
        var ipnsSphere =
            ga.Encode.IpnsRound.RealSphere(0.25, 1, 0.5, 0);

        // PGA Intersection of line and plane
        var pgaLinePlaneIntersection =
            pgaLine.MeetPGa(pgaPlane).DecodePGaFlat.Point3D();

        // PGA Projection of point on plane
        var pgaPointOnPlaneProjection =
            pgaPoint.ProjectPGaOn(pgaPlane);

        // CGA Intersection of plane and sphere
        var ipnsSpherePlaneIntersection =
            ipnsSphere.MeetIpns(pgaPlane.PGaToIpns());

        // CGA Projection of line on sphere
        var ipnsLineOnSphereProjection =
            pgaLine.PGaToIpns().ProjectIpnsOn(ipnsSphere);

        Console.WriteLine($"PGA Point : ${pgaPoint}$");
        Console.WriteLine($"PGA Line  : ${pgaLine}$");
        Console.WriteLine($"PGA Plane : ${pgaPlane}$");
        Console.WriteLine($"CGA Sphere: ${ipnsSphere}$");
        Console.WriteLine();

        Console.WriteLine($"PGA Line-Plane intersection  : ${pgaLinePlaneIntersection}$");
        Console.WriteLine($"PGA Point on Plane projection: ${pgaPointOnPlaneProjection}$");
        Console.WriteLine($"CGA Plane-Sphere intersection: ${ipnsSpherePlaneIntersection}$");
        Console.WriteLine($"CGA Line on Sphere projection: ${ipnsLineOnSphereProjection}$");
        Console.WriteLine();
    }
}
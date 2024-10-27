using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Geometry.CGa;

public static class CGa5DElementsSamples
{
    public static CGaFloat64GeometricSpace5D CGa
        => CGaFloat64GeometricSpace5D.Instance;


    public static void Example1()
    {
        var cga = CGaFloat64GeometricSpace.Create(5);

        // Encode a plane in 3-dimensions using as a IPNS blade
        var planeBlade = cga.Encode.IpnsFlat.Plane(
            3,
            LinFloat64Vector3D.Create(1, 2, -1)
        );

        // Define a real sphere with radius 5
        var sphereBlade = cga.Encode.IpnsRound.RealSphere(
            5,
            LinFloat64Vector3D.Create(1, 1, 1)
        );

        // Compute the IPNS blade of intersection using the outer product
        var intersectionBlade = sphereBlade.Op(planeBlade);

        // Decode the blade of intersection into a CGA element
        var intersectionElement = intersectionBlade.Decode.IpnsElement();


    }

    /// <summary>
    /// Illustrate how to define CGA direction elements, and encode\decode them as
    /// CGA OPNS and IPNS blades
    /// </summary>
    public static void CGaDirectionsExample()
    {
        // A CGA direction element is a simple data structure containing Euclidean
        // information (weight + normalized Euclidean direction blade). This information
        // can be used for interfacing, but actual CGA computations need a CGA blade
        // which can be obtained by encoding the data structure into a OPNS\IPNS CGA
        // blade. We can also decode a OPNS\IPNS CGA blade into a direction data
        // structure to obtain the details of its two components (weight + normalized
        // euclidean blade). See table 14.1 in Geometric Algebra for Computer Science.

        // Create a 5-dimensional CGA space
        var cga = CGaFloat64GeometricSpace.Create(5);

        // Define a weighted direction from a scalar, only the sign of the scalar is used
        // This is a kind of "signed point at the origin" or "signed 0D subspace" geometry
        var d0 = cga.DefineDirectionScalar(
            2.1,
            -3.4
        );

        // Define a weighted 1D direction based on a 3D vector. Internally, the
        // vector is normalized to a unit vector
        var d1 = cga.DefineDirectionLine(
            2.1,
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        // Define a weighted 2D direction from a 3D bivector
        var d2 = cga.DefineDirectionPlane(
            2.1,
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        // Define a weighted 3D direction from a 3D trivector
        var d3 = cga.DefineDirectionVolume(
            2.1,
            LinFloat64Trivector3D.Create(-3.4)
        );

        // Group the elements into an array
        var cgaElementArray = new CGaFloat64Element[]
        {
            d0, d1, d2, d3
        };

        // For each of the defined direction elements, display some information
        foreach (var cgaElement in cgaElementArray)
        {
            // Encode the direction element into a CGA OPNS blade
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();

            // Encode the direction element into a CGA IPNS blade
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();


            // Make sure that decoding the OPNS\IPNS blade gives the same direction element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsDirection.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsDirection.Element().IsSameElement(cgaElement)
            );


            // We can partially decode the OPNS\IPNS blade to only get the weight of
            // the direction element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsDirection.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsDirection.Weight().IsNearEqual(cgaElement.Weight)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean
            // Subspace blade
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsDirection.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsDirection.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Console.WriteLine($"Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Direction:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Direction:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsDirection.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Direction:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsDirection.Element().ToString());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Illustrate how to define CGA tangent elements, and encode\decode them as
    /// CGA OPNS and IPNS blades
    /// </summary>
    public static void CGaTangentsExample()
    {
        // A CGA tangent element is a simple data structure containing Euclidean
        // information (weight + normalized Euclidean direction blade + Euclidean position vector).
        // This information can be used for interfacing, but actual CGA computations need
        // a CGA blade which can be obtained by encoding the data structure into a OPNS\IPNS
        // CGA blade. We can also decode a OPNS\IPNS CGA blade into a tangent element
        // data structure to obtain the details of its 3 components (weight + normalized
        // euclidean blade + Euclidean position vector). See table 14.1 in Geometric Algebra
        // for Computer Science.

        // Define a weighted tangent element from a scalar and position, only the sign of the
        // scalar is used. This is a kind of "signed point" geometry.
        var d0 = CGa.DefineTangentPoint(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1)
        );

        // Define a weighted 1D tangent based on a 3D direction vector and a 3D position.
        // Internally, the vector is normalized to a unit vector
        var d1 = CGa.DefineTangentLine(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        // Define a weighted 2D tangent based on a 3D direction bivector and a 3D position.
        // Internally, the bivector is normalized to a unit bivector
        var d2 = CGa.DefineTangentPlane(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        // Define a weighted 3D tangent based on a 3D direction trivector and a 3D position.
        var d3 = CGa.DefineTangentVolume(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Trivector3D.Create(-3.4)
        );

        var cgaElementArray = new CGaFloat64Tangent[]
        {
            d0, d1, d2, d3
        };

        // For each of the defined elements, display some information
        foreach (var cgaElement in cgaElementArray)
        {
            // Encode the tangent element into a CGA OPNS blade
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();

            // Encode the tangent element into a CGA IPNS blade
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();


            // Make sure that decoding the OPNS\IPNS blade gives the same tangent element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsTangent.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsTangent.Element().IsSameElement(cgaElement)
            );


            // We can partially decode the OPNS\IPNS blade to only get the weight of
            // the tangent element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsTangent.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsTangent.Weight().IsNearEqual(cgaElement.Weight)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean
            // direction blade of the tangent element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsTangent.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsTangent.VGaDirection().IsNearEqual(cgaElement.Direction)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean
            // position vector of the tangent element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsTangent.VGaPosition().IsNearEqual(cgaElement.Position)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsTangent.VGaPosition().IsNearEqual(cgaElement.Position)
            );

            Console.WriteLine($"Tangent Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Tangent:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Tangent:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsTangent.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Tangent:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsTangent.Element().ToString());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Illustrate how to define CGA flat elements, and encode\decode them as
    /// CGA OPNS\IPNS blades and PGA blades
    /// </summary>
    public static void CGaFlatsExample()
    {
        // A CGA flat element is a simple data structure containing Euclidean
        // information (weight + normalized Euclidean direction blade + Euclidean position vector).
        // This information can be used for interfacing, but actual CGA computations need
        // a CGA blade which can be obtained by encoding the data structure into a OPNS\IPNS
        // CGA blade, or a PGA blade. We can also decode a OPNS\IPNS\PGA blade into a flat element
        // data structure to obtain the details of its 3 components (weight + normalized
        // Euclidean blade + Euclidean position vector). See table 14.1 in Geometric Algebra
        // for Computer Science.

        // Define a weighted flat point element.
        var d0 = CGa.DefineFlatPoint(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1)
        );

        // Define a weighted flat line element.
        var d1 = CGa.DefineFlatLine(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        // Define a weighted plane line element.
        var d2 = CGa.DefineFlatPlane(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        // Define a weighted flat volume element.
        var d3 = CGa.DefineFlatVolume(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Trivector3D.Create(-3.4)
        );

        var cgaElementArray = new CGaFloat64Flat[]
        {
            d0, d1, d2, d3
        };

        // For each of the defined elements, display some information
        foreach (var cgaElement in cgaElementArray)
        {
            // Encode the flat element into a CGA OPNS blade
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();

            // Encode the flat element into a CGA IPNS blade
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();

            // Encode the flat element into a PGA blade
            var cgaElementPGaBlade = cgaElement.EncodePGaBlade();


            // Make sure that decoding the OPNS\IPNS\PGA blade gives the same flat element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsFlat.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsFlat.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementPGaBlade.DecodePGaFlat.Element().IsSameElement(cgaElement)
            );


            // We can partially decode the OPNS\IPNS\PGA blade to only get the weight of
            // the flat element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsFlat.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsFlat.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementPGaBlade.DecodePGaFlat.Weight().IsNearEqual(cgaElement.Weight)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean direction of
            // the flat element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsFlat.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsFlat.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementPGaBlade.DecodePGaFlat.VGaDirection().IsNearEqual(cgaElement.Direction)
            );


            // We can partially decode the OPNS\IPNS\PGA blade to only get the position of
            // the flat element
            Debug.Assert(
                cgaElement.SurfaceNearContainsPoint(
                    cgaElementOpnsBlade.DecodeOpnsFlat.VGaPosition()
                )
            );

            Debug.Assert(
                cgaElement.SurfaceNearContainsPoint(
                    cgaElementIpnsBlade.DecodeIpnsFlat.VGaPosition()
                )
            );

            Debug.Assert(
                cgaElement.SurfaceNearContainsPoint(
                    cgaElementPGaBlade.DecodePGaFlat.VGaPosition()
                )
            );

            Console.WriteLine($"Flat Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Flat:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Flat:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsFlat.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Flat:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsFlat.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded PGA Flat:");
            Console.WriteLine(cgaElementPGaBlade.DecodePGaFlat.Element().ToString());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Illustrate how to create rounds, and encode\decode them as
    /// OPNS and IPNS blades
    /// </summary>
    public static void CGaRoundsExample()
    {
        // A CGA round element is a simple data structure containing Euclidean
        // information (weight + normalized Euclidean direction blade + Euclidean position vector + squared radius).
        // This information can be used for interfacing, but actual CGA computations need
        // a CGA blade which can be obtained by encoding the data structure into a OPNS\IPNS
        // CGA blade. We can also decode a OPNS\IPNS CGA blade into a round element
        // data structure to obtain the details of its components (weight + normalized
        // Euclidean blade + Euclidean position vector + squared radius). See table 14.1 in Geometric Algebra
        // for Computer Science.

        // Define a weighted round point element.
        var d0 = CGa.DefineRoundPoint(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1)
        );

        // Define a weighted round point-pair element.
        var d1 = CGa.DefineRoundPointPair(
            2.1,
            4,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        // Define a weighted round circle element.
        var d2 = CGa.DefineRoundCircle(
            2.1,
            4,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        // Define a weighted round sphere element.
        var d3 = CGa.DefineRoundSphere(
            2.1,
            4,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Trivector3D.Create(-3.4)
        );

        var cgaElementArray = new CGaFloat64Round[]
        {
            d0, d1, d2, d3
        };

        foreach (var cgaElement in cgaElementArray)
        {
            // Encode the flat element into a CGA OPNS blade
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();

            // Encode the flat element into a CGA IPNS blade
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();


            // Make sure that decoding the OPNS\IPNS blade gives the same round element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Element().IsSameElement(cgaElement)
            );


            // We can partially decode the OPNS\IPNS blade to only get the weight of
            // the round element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean direction of
            // the round element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );


            // We can partially decode the OPNS\IPNS blade to only get the Euclidean center of
            // the round element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );


            // We can partially decode the OPNS\IPNS blade to only get the squared radius of
            // the round element
            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Console.WriteLine($"Round Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Round:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Round:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsRound.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Round:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsRound.Element().ToString());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Illustrate how to create real rounds, and encode\decode them as
    /// OPNS and IPNS blades. A real round has a positive squared radius
    /// </summary>
    public static void CGaRealRoundsExample()
    {
        var d0 = CGa.DefineRoundPoint(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1)
        );

        var d1 = CGa.DefineRealRoundPointPair(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        var d2 = CGa.DefineRealRoundCircle(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        var d3 = CGa.DefineRealRoundSphere(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Trivector3D.Create(-3.4)
        );

        var cgaElementArray = new CGaFloat64Round[]
        {
            d0, d1, d2, d3
        };

        foreach (var cgaElement in cgaElementArray)
        {
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Console.WriteLine($"Round Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Round:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Round:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsRound.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Round:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsRound.Element().ToString());
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Illustrate how to create imaginary rounds, and encode\decode them as
    /// OPNS and IPNS blades. An imaginary round has a negative squared radius
    /// </summary>
    public static void CGaImaginaryRoundsExample()
    {
        var d0 = CGa.DefineRoundPoint(
            2.1,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1)
        );

        var d1 = CGa.DefineImaginaryRoundPointPair(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Vector3D.Create(-1, -1.2, -3)
        );

        var d2 = CGa.DefineImaginaryRoundCircle(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Bivector3D.Create(-1.4, -1.3, -2.1)
        );

        var d3 = CGa.DefineImaginaryRoundSphere(
            2.1,
            2,
            LinFloat64Vector3D.Create(-1.3, -2.1, -1.1),
            LinFloat64Trivector3D.Create(-3.4)
        );

        var cgaElementArray = new CGaFloat64Round[]
        {
            d0, d1, d2, d3
        };

        foreach (var cgaElement in cgaElementArray)
        {
            var cgaElementOpnsBlade = cgaElement.EncodeOpnsBlade();
            var cgaElementIpnsBlade = cgaElement.EncodeIpnsBlade();

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Element().IsSameElement(cgaElement)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.Weight().IsNearEqual(cgaElement.Weight)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaDirection().IsNearEqual(cgaElement.Direction)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.VGaCenter().IsNearEqual(cgaElement.Center)
            );

            Debug.Assert(
                cgaElementOpnsBlade.DecodeOpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Debug.Assert(
                cgaElementIpnsBlade.DecodeIpnsRound.RadiusSquared().IsNearEqual(cgaElement.RadiusSquared)
            );

            Console.WriteLine($"Round Direction Grade: {cgaElement.Direction.Grade}");
            Console.WriteLine("Original Round:");
            Console.WriteLine(cgaElement.ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded OPNS Round:");
            Console.WriteLine(cgaElementOpnsBlade.DecodeOpnsRound.Element().ToString());
            Console.WriteLine();

            Console.WriteLine("Decoded IPNS Round:");
            Console.WriteLine(cgaElementIpnsBlade.DecodeIpnsRound.Element().ToString());
            Console.WriteLine();
        }
    }
}
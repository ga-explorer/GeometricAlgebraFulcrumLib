using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class NumericRotorsSample
    {
        // This is a pre-defined scalar processor for float 64
        // numerical scalars
        public static ScalarAlgebraFloat64Processor ScalarProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor;

        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        public static TextFloat64Composer TextComposer { get; }
            = TextFloat64Composer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        public static LaTeXFloat64Composer LaTeXComposer { get; }
            = LaTeXFloat64Composer.DefaultComposer;

        // A random number generator for creating random numerical multivectors
        public static GeometricAlgebraRandomComposer<double> Random { get; }
            = GeometricProcessor.CreateGeometricRandomComposer(10);


        /// <summary>
        /// Define a rotor in 3D using a 2-blade of rotation and an angle
        /// </summary>
        public static void Example1()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);

            // Define angle of rotation
            var angle = Random.GetScalar(0, 2 * Math.PI);

            // Define 2-blade of rotation
            var rotationBlade =
                (Random.GetScalar() +
                 Random.GetScalar() * e1.Gp(e2) +
                 Random.GetScalar() * e2.Gp(e3) +
                 Random.GetScalar() * e1.Gp(e3)).GetBivectorPart();

            // The rotation axis is only required here for validation
            var rotationAxis = rotationBlade.Gp(pseudoScalar.Inverse()).GetVectorPart();

            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = GeometricProcessor.CreatePureRotor(angle, rotationBlade);

            // Make sure the eigen vector and eigen 2-blade of rotation are correct

            var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a = Random.GetVector();
            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(rotationBlade.GetSubspace());
            var pb = b.ProjectOn(rotationBlade.GetSubspace());

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff3 =
                ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle);

            // Make sure the projection of a on the rotation 2-blade is rotated
            // correctly into the projection of b on the rotation 2-blade
            var diff4 = rotor.OmMap(pa) - pb;

            Console.WriteLine($@"Rotor $R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
            Console.WriteLine($@"$R v R^{{\sim}} - v= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R B R^{{\sim}} - B= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine($@"$a = {LaTeXComposer.GetMultivectorText(a)}$");
            Console.WriteLine($@"$b = R a R^{{\sim}} = {LaTeXComposer.GetMultivectorText(b)}$");
            Console.WriteLine($@"$a_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pa)}$");
            Console.WriteLine($@"$b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pb)}$");
            Console.WriteLine(
                $@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff3)}$");
            Console.WriteLine($@"$R a_{{\Vert}} R^{{\sim}} - b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(diff4)}$");
            Console.WriteLine();
        }

        /// <summary>
        /// Define a rotor in 3D using an axis of rotation and an angle
        /// </summary>
        public static void Example2()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);

            var angle = Random.GetScalar(0, 2 * Math.PI);

            var rotationAxis =
                Random.GetScalar() * e1 +
                Random.GetScalar() * e2 +
                Random.GetScalar() * e3;

            var rotationBlade =
                rotationAxis.Gp(pseudoScalar.Inverse()).GetBivectorPart();

            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = GeometricProcessor.CreatePureRotor(angle, rotationBlade);

            // Make sure the eigen vector and eigen 2-blade of rotation are correct
            var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a =
                Random.GetScalar() * e1 +
                Random.GetScalar() * e2 +
                Random.GetScalar() * e3;

            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(rotationBlade.GetSubspace());
            var pb = b.ProjectOn(rotationBlade.GetSubspace());

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff3 =
                ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle);

            // Make sure the projection of a on the rotation 2-blade is rotated
            // correctly into the projection of b on the rotation 2-blade
            var diff4 = rotor.OmMap(pa) - pb;

            Console.WriteLine($@"Rotor $R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
            Console.WriteLine($@"$R v R^{{\sim}} - v= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R B R^{{\sim}} - B= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine($@"$a = {LaTeXComposer.GetMultivectorText(a)}$");
            Console.WriteLine($@"$b = R a R^{{\sim}} = {LaTeXComposer.GetMultivectorText(b)}$");
            Console.WriteLine($@"$a_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pa)}$");
            Console.WriteLine($@"$b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pb)}$");
            Console.WriteLine(
                $@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff3)}$");
            Console.WriteLine($@"$R a_{{\Vert}} R^{{\sim}} - b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(diff4)}$");
            Console.WriteLine();
        }

        /// <summary>
        /// Define a rotor in 3D that rotates a unit vector to another
        /// </summary>
        public static void Example3()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);

            var v1 = Random.GetVector(3, true);
            var v2 = Random.GetVector(3, true);

            // Rotation angle
            var angle =
                v1.ESp(v2).ArcCos().ScalarValue;

            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = GeometricProcessor.CreatePureRotor(
                v1,
                v2,
                true
            );

            var rotationBlade =
                GeometricProcessor.CreateBivector(rotor.Multivector.GetBivectorPart());

            var rotationAxis =
                rotationBlade.Gp(pseudoScalar.Inverse()).GetVectorPart();

            // Make sure v1 rotates into v2
            var diff1 = rotor.OmMap(v1) - v2;

            // Make sure the eigen vector and eigen 2-blade of rotation are correct
            var diff2 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff3 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a =
                Random.GetScalar() * e1 +
                Random.GetScalar() * e2 +
                Random.GetScalar() * e3;

            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(rotationBlade.GetSubspace());
            var pb = b.ProjectOn(rotationBlade.GetSubspace());

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff4 =
                (pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle;

            // Make sure the projection of a on the rotation 2-blade is rotated
            // correctly into the projection of b on the rotation 2-blade
            var diff5 = rotor.OmMap(pa) - pb;

            Console.WriteLine($@"Rotor $R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
            Console.WriteLine($@"$R v1 R^{{\sim}} - v2= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R v R^{{\sim}} - v= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine($@"$R B R^{{\sim}} - B= {LaTeXComposer.GetMultivectorText(diff3)}$");
            Console.WriteLine($@"$a = {LaTeXComposer.GetMultivectorText(a)}$");
            Console.WriteLine($@"$b = R a R^{{\sim}} = {LaTeXComposer.GetMultivectorText(b)}$");
            Console.WriteLine($@"$a_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pa)}$");
            Console.WriteLine($@"$b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pb)}$");
            Console.WriteLine(
                $@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff4)}$");
            Console.WriteLine($@"$R a_{{\Vert}} R^{{\sim}} - b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(diff5)}$");
            Console.WriteLine();
        }


        /// <summary>
        /// Define the family of one-parameter rotations between two unit vectors in 3D
        /// </summary>
        public static void Example4()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
            var pseudoScalarInverse = pseudoScalar.Inverse();

            // Define two random unit vectors
            var v1 = Random.GetVector(3, true);
            var v2 = Random.GetVector(3, true);

            Console.WriteLine($@"$v_1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$v_2 = {LaTeXComposer.GetMultivectorText(v2)}$");

            foreach (var angleTheta in 0d.GetLinearRange(Math.PI, 19))
            {
                // Define a rotor with angle theta in the plane orthogonal to v2 - v1
                var rotorSBlade = (v2 - v1).Gp(pseudoScalarInverse).GetBivectorPart();
                var rotorS = GeometricProcessor.CreatePureRotor(angleTheta, rotorSBlade);

                // Create pure rotor that rotates v1 to v2 at theta = 0
                var rotor0 = GeometricProcessor.CreatePureRotor(
                    v1,
                    v2,
                    true
                );

                // The actual plane of rotation is made by rotating the plane of v1,v2
                // by angle theta in the plane orthogonal to v2 - v1
                var rotationBlade = rotorS.OmMap(v2.Op(v1));

                // Project v1, v2 into the actual plane of rotation
                var rotationBladeSubspace =
                    rotationBlade.GetSubspace();

                var u1 = v1.ProjectOn(rotationBladeSubspace);
                var u2 = v2.ProjectOn(rotationBladeSubspace);

                // Define the actual rotor taking v1 into v2
                var rotor = GeometricProcessor.CreatePureRotor(
                    u1,
                    u2,
                    false
                );

                var rotationAxis =
                    rotationBlade.Gp(pseudoScalarInverse).GetVectorPart();

                var rotationAngle =
                    u1.GetEuclideanAngle(u2).ScalarValue;

                // Make sure v1 rotates into v2 using the pure rotor at theta = 0
                var diff1 = (rotor0.OmMap(v1) - v2);

                // Make sure u1 rotates into u2 using the actual rotor
                var diff2 = (rotor.OmMap(u1) - u2);

                // Make sure v1 rotates into v2 using the actual rotor
                var diff3 = (rotor.OmMap(v1) - v2);

                Console.WriteLine($@"Parameter Angle $\theta = {LaTeXComposer.GetAngleText(angleTheta)}$");
                Console.WriteLine($@"Rotation Angle $\varphi = {LaTeXComposer.GetAngleText(rotationAngle)}$");
                Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
                Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
                Console.WriteLine($@"$u_1 = {LaTeXComposer.GetMultivectorText(u1)}$");
                Console.WriteLine($@"$u_2 = {LaTeXComposer.GetMultivectorText(u2)}$");
                Console.WriteLine($@"$u_1 \wedge B = {LaTeXComposer.GetMultivectorText(u1.Op(rotationBlade))}$");
                Console.WriteLine($@"$u_2 \wedge B = {LaTeXComposer.GetMultivectorText(u2.Op(rotationBlade))}$");
                Console.WriteLine($@"Rotor $S = {LaTeXComposer.GetMultivectorText(rotorS)}$");
                Console.WriteLine($@"Rotor $R \left( 0 \right) = {LaTeXComposer.GetMultivectorText(rotor0)}$");
                Console.WriteLine($@"Rotor $R \left( \theta \right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
                Console.WriteLine($@"$R \left( 0 \right) v_1 R^{{\sim}}\left( 0 \right) - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
                Console.WriteLine($@"$R \left( \theta \right) u_1 R^{{\sim}}\left( \theta \right) - u_2= {LaTeXComposer.GetMultivectorText(diff2)}$");
                Console.WriteLine($@"$R \left( \theta \right) v_1 R^{{\sim}}\left( \theta \right) - v_2= {LaTeXComposer.GetMultivectorText(diff3)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Algebraically define the family of one-parameter rotations
        /// between two unit vectors in 3D
        /// </summary>
        public static void Example5()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Op(e2).Op(e3);
            var pseudoScalarInverse = pseudoScalar.Inverse();

            // Define two random unit vectors
            var v1 = Random.GetVector(3, true);
            var v2 = Random.GetVector(3, true);
            var v1v2Dot = v1.ESp(v2);

            Console.WriteLine($@"$v_1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$v_2 = {LaTeXComposer.GetMultivectorText(v2)}$");

            var angleThetaRange = 
                (-0.5d * Math.PI).GetLinearRange(0.5d * Math.PI, 19);

            foreach (var angleTheta in angleThetaRange)
            {
                // Define a rotor with angle theta in the plane orthogonal to v2 - v1
                var rotorSBlade = (v2 - v1).Gp(pseudoScalarInverse).GetBivectorPart();
                var rotorS = GeometricProcessor.CreatePureRotor(angleTheta, rotorSBlade);

                // Create pure rotor that rotates v1 to v2 at theta = 0
                var rotor0 = GeometricProcessor.CreatePureRotor(
                    v1,
                    v2,
                    true
                );

                // The actual plane of rotation is made by rotating the plane of v1,v2
                // by angle theta in the plane orthogonal to v2 - v1
                var rotorBlade = rotorS.OmMap(v2.Op(v1));
                
                // Define parametric angle of rotation
                var rotorAngle = 
                    (1 + 2 * (v1v2Dot - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (v1v2Dot + 1))).ArcCos().ScalarValue;
                
                // Define the actual rotor taking v1 into v2
                var rotor = GeometricProcessor.CreatePureRotor(
                    rotorAngle,
                    rotorBlade
                );

                var rotorAxis =
                    rotorBlade.Gp(pseudoScalarInverse).GetVectorPart();
                
                // Make sure v1 rotates into v2 using the pure rotor at theta = 0
                var diff1 = (rotor0.OmMap(v1) - v2);
                
                // Make sure v1 rotates into v2 using the actual rotor
                var diff2 = (rotor.OmMap(v1) - v2);

                var rotor0A = 
                    GeometricProcessor.CreateParametricPureRotor3D(
                        v1, 
                        v2, 
                        0
                    ).Multivector;

                var rotorA = 
                    GeometricProcessor.CreateParametricPureRotor3D(
                        v1, 
                        v2, 
                        angleTheta
                    ).Multivector;

                // This should be 0
                var diff3 =
                    (rotor.Multivector - rotorA);

                // This should be 0
                var diff4 =
                    (rotor0.Multivector - rotor0A);

                Console.WriteLine($@"Parameter Angle $\theta = {LaTeXComposer.GetAngleText(angleTheta)}$");
                Console.WriteLine($@"Rotation Angle $\varphi = {LaTeXComposer.GetAngleText(rotorAngle)}$");
                Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotorAxis)}$");
                Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotorBlade)}$");
                Console.WriteLine($@"Rotor $S = {LaTeXComposer.GetMultivectorText(rotorS)}$");
                Console.WriteLine($@"Rotor $R \left( 0 \right) = {LaTeXComposer.GetMultivectorText(rotor0)}$");
                Console.WriteLine($@"Rotor $R \left( \theta \right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
                Console.WriteLine($@"$R \left( 0 \right) v_1 R^{{\sim}}\left( 0 \right) - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
                Console.WriteLine($@"$R \left( \theta \right) v_1 R^{{\sim}}\left( \theta \right) - v_2= {LaTeXComposer.GetMultivectorText(diff2)}$");
                Console.WriteLine($@"Diff3 = ${LaTeXComposer.GetMultivectorText(diff3)}$");
                Console.WriteLine($@"Diff4 = ${LaTeXComposer.GetMultivectorText(diff4)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Create a pure scaled rotor based on two general vectors
        /// </summary>
        public static void Example7()
        {
            var u = 
                GeometricProcessor.CreateVector(1, 1, 1);

            var v = 
                GeometricProcessor.CreateVector(2, 1, -1);

            var scaledRotor = GeometricProcessor.CreateScaledPureRotor(
                u, 
                v
            );

            var scaledRotorInv = 
                scaledRotor.GetPureScaledRotorInverse();

            var v1 = 
                scaledRotor.OmMap(u);

            var u1 = 
                scaledRotorInv.OmMap(v);

            Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
            Console.WriteLine($@"$v = {LaTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(scaledRotor)}$");
            Console.WriteLine($@"$R u R^{{\sim}} = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$\frac{{1}}{{\left(RR^{{\sim}}\right)^{{2}}}}R^{{\sim}} v R = {LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine();
        }

        
        /// <summary>
        /// Create pure rotors from and to basis vectors
        /// </summary>
        public static void Example9()
        {
            var axis = Axis.NegativeZ;
            var u = Random.GetVector(-10, 10);
            var u2 = u.Sp(u).Sqrt();

            var rotor1 = u.CreateScaledPureRotorFromAxis(axis, false);
            var rotor2 = u.CreateScaledPureRotorToAxis(axis, false);

            var r2 = rotor1.Multivector.Gp(rotor1.MultivectorReverse);

            var v1 = rotor1.OmMap(axis);
            var v2 = rotor2.OmMap(u);

            Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
            Console.WriteLine($@"$u^2 = {LaTeXComposer.GetScalarText(u2)}$");
            Console.WriteLine($@"$R_1 R_1^{{\sim}} = {LaTeXComposer.GetMultivectorText(r2)}$");

            Console.WriteLine($@"$R_1 = {LaTeXComposer.GetMultivectorText(rotor1)}$");
            Console.WriteLine($@"$R_2 = {LaTeXComposer.GetMultivectorText(rotor2)}$");
            Console.WriteLine($@"$R_1 e_1 R_1^{{\sim}} = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$R_2 u R_2^{{\sim}} = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine();
        }
        
        public static void Example10()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);

            var u = Random.GetVector(0, 10).DivideByENorm();

            var rotor = GeometricProcessor.CreateScaledPureRotor(e1, u);

            var u1 = rotor.OmMap(e1);
            var u2 = rotor.OmMap(e2);
            var u3 = rotor.OmMap(e3);

            var e1Inv = e1.Inverse();
            var e2Inv = e2.Inverse();
            var e3Inv = e3.Inverse();

            var e12Inv = e1.Gp(e2).Inverse();
            var e13Inv = e1.Gp(e3).Inverse();
            var e23Inv = e2.Gp(e3).Inverse();

            var e123Inv = e1.Gp(e2).Gp(e3).Inverse();

            var u12 = u1.Gp(u2);
            var u13 = u1.Gp(u3);
            var u23 = u2.Gp(u3);

            var u123 = u1.Gp(u2).Gp(u3);

            var rotorM =
                1 +
                u1.Gp(e1Inv) + u2.Gp(e2Inv) + u3.Gp(e3Inv) +
                u12.Gp(e12Inv) + u13.Gp(e13Inv) + u23.Gp(e23Inv) +
                u123.Gp(e123Inv);

            rotorM /= rotorM.Sp(rotorM.Reverse()).Sqrt();

            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"$M = {LaTeXComposer.GetMultivectorText(rotorM)}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{1}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e1Inv)}$");
            Console.WriteLine($@"$e_{{2}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e2Inv)}$");
            Console.WriteLine($@"$e_{{3}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e3Inv)}$");
            Console.WriteLine($@"$e_{{12}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e12Inv)}$");
            Console.WriteLine($@"$e_{{13}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e13Inv)}$");
            Console.WriteLine($@"$e_{{23}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e23Inv)}$");
            Console.WriteLine($@"$e_{{123}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e123Inv)}$");
            Console.WriteLine();

            Console.WriteLine($@"$u_{{1}} = {LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine($@"$u_{{2}} = {LaTeXComposer.GetMultivectorText(u2)}$");
            Console.WriteLine($@"$u_{{3}} = {LaTeXComposer.GetMultivectorText(u3)}$");
            Console.WriteLine($@"$u_{{12}} = {LaTeXComposer.GetMultivectorText(u12)}$");
            Console.WriteLine($@"$u_{{13}} = {LaTeXComposer.GetMultivectorText(u13)}$");
            Console.WriteLine($@"$u_{{23}} = {LaTeXComposer.GetMultivectorText(u23)}$");
            Console.WriteLine($@"$u_{{123}} = {LaTeXComposer.GetMultivectorText(u123)}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{1}}^{{-1}} u_{{1}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u1))}$");
            Console.WriteLine($@"$e_{{2}}^{{-1}} u_{{2}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u2))}$");
            Console.WriteLine($@"$e_{{3}}^{{-1}} u_{{3}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u3))}$");
            Console.WriteLine($@"$e_{{12}}^{{-1}} u_{{12}} = {LaTeXComposer.GetMultivectorText(e12Inv.Gp(u12))}$");
            Console.WriteLine($@"$e_{{13}}^{{-1}} u_{{13}} = {LaTeXComposer.GetMultivectorText(e13Inv.Gp(u13))}$");
            Console.WriteLine($@"$e_{{23}}^{{-1}} u_{{23}} = {LaTeXComposer.GetMultivectorText(e23Inv.Gp(u23))}$");
            Console.WriteLine($@"$e_{{123}}^{{-1}} u_{{123}} = {LaTeXComposer.GetMultivectorText(e123Inv.Gp(u123))}$");
            Console.WriteLine();
        }

        public static void Example11()
        {
            var vectorFrame1 = GeometricProcessor.CreateBasisVectorFrame().MapAsBasisUsing(v => Random.GetScalar(1.1, 3) * v);
            //var vectorFrame1 = GeometricProcessor.CreateBasisVectorFrame(
            //    Random.GetVector(0, 10).DivideByENorm(),
            //    Random.GetVector(0, 10).DivideByENorm(),
            //    Random.GetVector(0, 10).DivideByENorm()
            //);

            var u = Random.GetVector(0, 10).DivideByENorm() * vectorFrame1[0].ENorm();
            var rotor = GeometricProcessor.CreateScaledPureRotor(vectorFrame1[0], u);
            
            var vectorFrame2 = vectorFrame1.MapUsing(rotor);

            //var vectorFrame1Inv = vectorFrame1.MapAsBasisUsing(v => v.Inverse());
            //var mvFrame1 = vectorFrame1.GetReciprocalVectorFrame().CreateBasisMultivectorFrame();
            var mvFrame1 = vectorFrame1.CreateBasisKVectorFrame().MapAsBasisUsing(v => v.Inverse());
            var mvFrame2 = vectorFrame2.CreateBasisKVectorFrame();

            var e1 = vectorFrame1[0];
            var e2 = vectorFrame1[1];
            var e3 = vectorFrame1[2];

            var e1Inv = mvFrame1[1];
            var e2Inv = mvFrame1[2];
            var e3Inv = mvFrame1[4];

            var e12Inv = mvFrame1[3];
            var e13Inv = mvFrame1[5];
            var e23Inv = mvFrame1[6];

            var e123Inv = mvFrame1[7];

            var u1 = mvFrame2[1];
            var u2 = mvFrame2[2];
            var u3 = mvFrame2[4];

            var u12 = mvFrame2[3];
            var u13 = mvFrame2[5];
            var u23 = mvFrame2[6];

            var u123 = mvFrame2[7];

            var rotorM = vectorFrame1.CreateRotorToFrame(vectorFrame2);

            //var rotorM = 
            //    mvFrame2
            //        .Zip(mvFrame1)
            //        .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
            //        .Aggregate(
            //            GeometricProcessor.CreateMultivectorSparseZero(),
            //            (mv1, mv2) => mv1 + mv2
            //        );

            //rotorM /= rotorM.Sp(rotorM.Reverse()).Sqrt();
            
            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"$M = {LaTeXComposer.GetMultivectorText(rotorM)}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{1}} = {LaTeXComposer.GetMultivectorText(e1)}$");
            Console.WriteLine($@"$u_{{1}} = {LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine($@"$R e_{{1}} R^{{-1}} = {LaTeXComposer.GetMultivectorText(rotorM.OmMap(e1))}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{2}} = {LaTeXComposer.GetMultivectorText(e2)}$");
            Console.WriteLine($@"$u_{{2}} = {LaTeXComposer.GetMultivectorText(u2)}$");
            Console.WriteLine($@"$R e_{{2}} R^{{-1}} = {LaTeXComposer.GetMultivectorText(rotorM.OmMap(e2))}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{3}} = {LaTeXComposer.GetMultivectorText(e3)}$");
            Console.WriteLine($@"$u_{{3}} = {LaTeXComposer.GetMultivectorText(u3)}$");
            Console.WriteLine($@"$R e_{{3}} R^{{-1}} = {LaTeXComposer.GetMultivectorText(rotorM.OmMap(e3))}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{1}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e1Inv)}$");
            Console.WriteLine($@"$e_{{2}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e2Inv)}$");
            Console.WriteLine($@"$e_{{3}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e3Inv)}$");
            Console.WriteLine($@"$e_{{12}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e12Inv)}$");
            Console.WriteLine($@"$e_{{13}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e13Inv)}$");
            Console.WriteLine($@"$e_{{23}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e23Inv)}$");
            Console.WriteLine($@"$e_{{123}}^{{-1}} = {LaTeXComposer.GetMultivectorText(e123Inv)}$");
            Console.WriteLine();

            Console.WriteLine($@"$u_{{1}} = {LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine($@"$u_{{2}} = {LaTeXComposer.GetMultivectorText(u2)}$");
            Console.WriteLine($@"$u_{{3}} = {LaTeXComposer.GetMultivectorText(u3)}$");
            Console.WriteLine($@"$u_{{12}} = {LaTeXComposer.GetMultivectorText(u12)}$");
            Console.WriteLine($@"$u_{{13}} = {LaTeXComposer.GetMultivectorText(u13)}$");
            Console.WriteLine($@"$u_{{23}} = {LaTeXComposer.GetMultivectorText(u23)}$");
            Console.WriteLine($@"$u_{{123}} = {LaTeXComposer.GetMultivectorText(u123)}$");
            Console.WriteLine();

            Console.WriteLine($@"$e_{{1}}^{{-1}} u_{{1}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u1))}$");
            Console.WriteLine($@"$e_{{2}}^{{-1}} u_{{2}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u2))}$");
            Console.WriteLine($@"$e_{{3}}^{{-1}} u_{{3}} = {LaTeXComposer.GetMultivectorText(e1Inv.Gp(u3))}$");
            Console.WriteLine($@"$e_{{12}}^{{-1}} u_{{12}} = {LaTeXComposer.GetMultivectorText(e12Inv.Gp(u12))}$");
            Console.WriteLine($@"$e_{{13}}^{{-1}} u_{{13}} = {LaTeXComposer.GetMultivectorText(e13Inv.Gp(u13))}$");
            Console.WriteLine($@"$e_{{23}}^{{-1}} u_{{23}} = {LaTeXComposer.GetMultivectorText(e23Inv.Gp(u23))}$");
            Console.WriteLine($@"$e_{{123}}^{{-1}} u_{{123}} = {LaTeXComposer.GetMultivectorText(e123Inv.Gp(u123))}$");
            Console.WriteLine();
        }
        
        public static void Example12()
        {
            var vectorFrame1 = GeometricProcessor.CreateBasisVectorFrame().MapAsBasisUsing(v => Random.GetScalar(1.1, 3) * v);

            var u = Random.GetVector(0, 10).DivideByENorm() * vectorFrame1[0].ENorm();
            var rotor = GeometricProcessor.CreateScaledPureRotor(vectorFrame1[0], u);
            
            var vectorFrame2 = vectorFrame1.MapUsing(rotor);

            var rotorM = vectorFrame1.CreateRotorToFrame(vectorFrame2);
            
            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"$M = {LaTeXComposer.GetMultivectorText(rotorM)}$");
            Console.WriteLine();
            
        }
    }
}

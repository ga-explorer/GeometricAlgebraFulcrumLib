using System;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class RotorsSample
    {
        // This is a pre-defined scalar processor for float 64
        // numerical scalars
        public static ScalarAlgebraFloat64Processor ScalarProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor;

        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(6);

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
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

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
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

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
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

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
                var rotationBlade =
                    GeometricProcessor.CreateBivector(
                        rotorS
                            .OmMapBivector(v2.Op(v1))
                            .GetBivectorPart()
                    );

                // Project v1, v2 into the actual plane of rotation
                var rotationBladeSubspace =
                    GeometricProcessor.CreateSubspace(rotationBlade);

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
                var diff1 =
                    (rotor0.OmMap(v1) - v2);

                // Make sure u1 rotates into u2 using the actual rotor
                var diff2 =
                    (rotor.OmMap(u1) - u2);

                // Make sure v1 rotates into v2 using the actual rotor
                var diff3 =
                    (rotor.OmMap(v1) - v2);

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
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
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
                var rotorBlade =
                    GeometricProcessor.CreateBivector(
                        rotorS
                            .OmMapBivector(v2.Op(v1))
                            .GetBivectorPart()
                    );
                
                // Define parametric angle of rotation
                var rotorAngle = 
                    Math.Acos(1 + 2 * (v1v2Dot - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (v1v2Dot + 1)));
                
                // Define the actual rotor taking v1 into v2
                var rotor = GeometricProcessor.CreatePureRotor(
                    rotorAngle,
                    rotorBlade
                );

                var rotorAxis =
                    rotorBlade.Gp(pseudoScalarInverse).GetVectorPart();
                
                // Make sure v1 rotates into v2 using the pure rotor at theta = 0
                var diff1 =
                    (rotor0.OmMap(v1) - v2);
                
                // Make sure v1 rotates into v2 using the actual rotor
                var diff2 =
                    (rotor.OmMap(v1) - v2);

                Console.WriteLine($@"Parameter Angle $\theta = {LaTeXComposer.GetAngleText(angleTheta)}$");
                Console.WriteLine($@"Rotation Angle $\varphi = {LaTeXComposer.GetAngleText(rotorAngle)}$");
                Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotorAxis)}$");
                Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotorBlade)}$");
                Console.WriteLine($@"Rotor $S = {LaTeXComposer.GetMultivectorText(rotorS)}$");
                Console.WriteLine($@"Rotor $R \left( 0 \right) = {LaTeXComposer.GetMultivectorText(rotor0)}$");
                Console.WriteLine($@"Rotor $R \left( \theta \right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
                Console.WriteLine($@"$R \left( 0 \right) v_1 R^{{\sim}}\left( 0 \right) - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
                Console.WriteLine($@"$R \left( \theta \right) v_1 R^{{\sim}}\left( \theta \right) - v_2= {LaTeXComposer.GetMultivectorText(diff2)}$");
                Console.WriteLine();
            }
        }
    }
}

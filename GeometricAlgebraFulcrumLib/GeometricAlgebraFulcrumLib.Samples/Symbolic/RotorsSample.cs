using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class RotorsSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(6);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        private static Vector<Expr> CreateVector3D(string name, string subscript)
        {
            return CreateVector(name, subscript, 3);
        }

        private static Vector<Expr> CreateVector3D(string name)
        {
            return CreateVector(name, 3);
        }

        private static Vector<Expr> CreateVector(string name, string subscript, int termsCount)
        {
            var vector =
                $"Subscript[{name},{subscript}1]".ToExpr() * GeometricProcessor.CreateVectorBasis(0);

            for (var i = 2; i <= termsCount; i++)
                vector += $"Subscript[{name},{subscript}{i}]".ToExpr() * GeometricProcessor.CreateVectorBasis(i - 1);

            return vector;
        }

        private static Vector<Expr> CreateVector(string name, int termsCount)
        {
            var vector =
                $"Subscript[{name},1]".ToExpr() * GeometricProcessor.CreateVectorBasis(0);

            for (var i = 2; i <= termsCount; i++)
                vector += $"Subscript[{name},{i}]".ToExpr() * GeometricProcessor.CreateVectorBasis(i - 1);

            return vector;
        }

        /// <summary>
        /// Define a rotor in 3D using a 2-blade of rotation and an angle
        /// </summary>
        public static void Example1()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
            var pseudoScalarInverse = pseudoScalar.Inverse();

            // Define angle of rotation
            var angle = @"\[Theta]".ToExpr();
            
            // Define 2-blade of rotation
            var rotationBlade = 
                ("Subscript[B,12]".ToExpr() * e1.Gp(e2) +
                 "Subscript[B,23]".ToExpr() * e2.Gp(e3) +
                 "Subscript[B,13]".ToExpr() * e1.Gp(e3)).GetBivectorPart();

            // The rotation axis is only required here for validation
            var rotationAxis = rotationBlade.Gp(pseudoScalarInverse).GetVectorPart();

            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = GeometricProcessor.CreatePureRotor(angle, rotationBlade);

            // Make sure the eigen vector and eigen 2-blade of rotation are correct
            
            var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a = CreateVector3D("a");
            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff3 = 
                ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle).ScalarValue.FullSimplify();
            
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
            Console.WriteLine($@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff3)}$");
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
            var pseudoScalarInverse = pseudoScalar.Inverse();

            var angle = @"\[Theta]".ToExpr();

            var rotationAxis = CreateVector3D("v");

            var rotationBlade = 
                rotationAxis.Gp(pseudoScalarInverse).GetBivectorPart();
            
            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = GeometricProcessor.CreatePureRotor(angle, rotationBlade);

            // Make sure the eigen vector and eigen 2-blade of rotation are correct
            var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a = CreateVector3D("a");
            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff3 = 
                ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle).ScalarValue.FullSimplify();
            
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
            Console.WriteLine($@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff3)}$");
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
            var pseudoScalarInverse = pseudoScalar.Inverse();

            var v1 = 
                "Subscript[v,11]".ToExpr() * e1 +
                "Subscript[v,12]".ToExpr() * e2 +
                "Subscript[v,13]".ToExpr() * e3;

            var v2 = 
                "Subscript[v,21]".ToExpr() * e1 +
                "Subscript[v,22]".ToExpr() * e2 +
                "Subscript[v,23]".ToExpr() * e3;

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
                rotationBlade.Gp(pseudoScalarInverse).GetVectorPart();

            // Make sure v1 rotates into v2
            var diff1 = 
                (rotor.OmMap(v1) - v2).VectorStorage.FullSimplifyScalars();

            // Make sure the eigen vector and eigen 2-blade of rotation are correct
            var diff2 = rotor.OmMap(rotationAxis) - rotationAxis;
            var diff3 = rotor.OmMap(rotationBlade) - rotationBlade;

            // Make sure the angle of rotation is correct
            // Create a general vector a and find its rotation b using rotor
            var a = 
                "Subscript[a,1]".ToExpr() * e1 +
                "Subscript[a,2]".ToExpr() * e2 +
                "Subscript[a,3]".ToExpr() * e3;

            var b = rotor.OmMap(a);

            // Compute the projection of a and b on rotation 2-blade
            var pa = a.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));
            var pb = b.ProjectOn(GeometricProcessor.CreateSubspace(rotationBlade));

            // Compute angle between projected vectors, the result must equal
            // the original angle of rotation of the rotor
            var diff4 = 
                ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle).ScalarValue.FullSimplify();
            
            // Make sure the projection of a on the rotation 2-blade is rotated
            // correctly into the projection of b on the rotation 2-blade
            var diff5 = rotor.OmMap(pa) - pb;

            Console.WriteLine($@"first vector  $v_1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"second vector $v_2 = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine($@"Rotor $R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
            Console.WriteLine($@"$R v_1 R^{{\sim}} - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R v R^{{\sim}} - v= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine($@"$R B R^{{\sim}} - B= {LaTeXComposer.GetMultivectorText(diff3)}$");
            Console.WriteLine($@"$a = {LaTeXComposer.GetMultivectorText(a)}$");
            Console.WriteLine($@"$b = R a R^{{\sim}} = {LaTeXComposer.GetMultivectorText(b)}$");
            Console.WriteLine($@"$a_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pa)}$");
            Console.WriteLine($@"$b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(pb)}$");
            Console.WriteLine($@"$\angle \left( a_{{\Vert}},b_{{\Vert}} \right) - \theta = {LaTeXComposer.GetScalarText(diff4)}$");
            Console.WriteLine($@"$R a_{{\Vert}} R^{{\sim}} - b_{{\Vert}} = {LaTeXComposer.GetMultivectorText(diff5)}$");
            Console.WriteLine();
        }

        /// <summary>
        /// Geometrically define the family of one-parameter rotations
        /// between two unit vectors in 3D
        /// </summary>
        public static void Example4()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);
            var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
            var pseudoScalarInverse = pseudoScalar.Inverse();

            // Define two unit vectors with angle phi between them
            var anglePhi = @"\[Phi]".ToExpr();
            var v1 = e1;
            var v2 = Mfs.Cos[anglePhi] * e1 + Mfs.Sin[anglePhi] * e2;

            // The free angle parameter theta
            var angleTheta = @"\[Theta]".ToExpr();

            var assumption = $"Element[{angleTheta} | {anglePhi}, Reals] && {angleTheta} > -Pi / 2 && {angleTheta} < Pi / 2 && {anglePhi} > 0 && {anglePhi} < 2 Pi".ToExpr();

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
                        .FullSimplifyScalars(assumption)
                        .GetBivectorPart()
                );
            
            // Project v1, v2 into the actual plane of rotation
            var rotationBladeSubspace = 
                GeometricProcessor.CreateSubspace(rotationBlade);

            var u1 = v1.ProjectOn(rotationBladeSubspace);
            var u2 = v2.ProjectOn(rotationBladeSubspace);

            // Define the actual rotor taking v1 into v2
            var rotationAngle = 
                u1.GetEuclideanAngle(u2).ScalarValue.Simplify(assumption);

            var rotor = GeometricProcessor.CreatePureRotor(
                rotationAngle,
                rotationBlade
            );

            var rotationAxis =
                rotationBlade.Gp(pseudoScalarInverse).GetVectorPart().FullSimplifyScalars(assumption);

            // Make sure v1 rotates into v2 using the pure rotor at theta = 0
            var diff1 =
                (rotor0.OmMap(v1) - v2).VectorStorage.FullSimplifyScalars();

            // Make sure u1 rotates into u2 using the actual rotor
            var diff2 =
                (rotor.OmMap(u1) - u2).VectorStorage.FullSimplifyScalars();

            // Make sure v1 rotates into v2 using the actual rotor
            var diff3 =
                (rotor.OmMap(v1) - v2).VectorStorage.FullSimplifyScalars();

            Console.WriteLine($@"$v_1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$v_2 = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine($@"Rotor $S = {LaTeXComposer.GetMultivectorText(rotorS)}$");
            Console.WriteLine($@"$R \left( 0 \right) = {LaTeXComposer.GetMultivectorText(rotor0)}$");
            Console.WriteLine($@"Rotor $R \left( \theta \right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"$\varphi \left( \theta \right) = {LaTeXComposer.GetScalarText(rotationAngle)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotationAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotationBlade)}$");
            Console.WriteLine($@"$R \left( 0 \right) v_1 R^{{\sim}}\left( 0 \right) - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R \left( \theta \right) u_1 R^{{\sim}}\left( \theta \right) - u_2= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine($@"$R \left( \theta \right) v_1 R^{{\sim}}\left( \theta \right) - v_2= {LaTeXComposer.GetMultivectorText(diff3)}$");
            Console.WriteLine();
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

            // Define two unit vectors with angle phi between them
            var anglePhi = @"\[Phi]".ToExpr();
            var v1 = e1;
            var v2 = Mfs.Cos[anglePhi] * e1 + Mfs.Sin[anglePhi] * e2;
            var v1v2Dot = v1.ESp(v2);

            // The free angle parameter theta for rotor S
            var angleTheta = @"\[Theta]".ToExpr();

            var assumption = $"Element[{angleTheta} | {anglePhi}, Reals] && {angleTheta} >= -Pi/2 && {angleTheta} <= Pi/2 && {anglePhi} >= 0 && {anglePhi} <= Pi".ToExpr();

            // Define parametric plane of rotation for rotor S
            var rotorSBlade = (v2 - v1).Gp(pseudoScalarInverse).GetBivectorPart();

            // Define parametric rotor S
            var rotorS = GeometricProcessor.CreatePureRotor(angleTheta, rotorSBlade);

            // Create pure rotor that rotates v1 to v2 at theta = 0
            var rotor0 = GeometricProcessor.CreatePureRotor(
                v1, 
                v2,
                true
            );

            
            // Define parametric angle of rotation
            var rotorAngle = 
                (1 + 2 * (v1v2Dot - 1) / (2 - Mfs.Power[Mfs.Sin[angleTheta], 2] * (v1v2Dot + 1))).ArcCos();

            // The actual plane of rotation is made by rotating the plane of v1,v2
            // by angle theta in the plane orthogonal to v2 - v1
            var rotorBlade = 
                GeometricProcessor.CreateBivector(
                    rotorS
                        .OmMapBivector(v2.Op(v1))
                        .FullSimplifyScalars(assumption)
                        .GetBivectorPart()
                );
            
            var rotor = GeometricProcessor.CreatePureRotor(
                rotorAngle,
                rotorBlade
            );

            var rotorAxis =
                rotorBlade.Gp(pseudoScalarInverse).GetVectorPart().FullSimplifyScalars(assumption);

            // Make sure v1 rotates into v2 using the pure rotor at theta = 0
            var diff1 =
                (rotor0.OmMap(v1) - v2).VectorStorage.FullSimplifyScalars();
            
            // Make sure v1 rotates into v2 using the actual rotor
            var diff2 =
                (rotor.OmMap(v1) - v2).VectorStorage.FullSimplifyScalars();

            Console.WriteLine($@"$v_1 = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$v_2 = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine($@"Rotor $S = {LaTeXComposer.GetMultivectorText(rotorS)}$");
            Console.WriteLine($@"$R \left( 0 \right) = {LaTeXComposer.GetMultivectorText(rotor0)}$");
            Console.WriteLine($@"Rotor $R \left( \theta \right) = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine($@"$\varphi \left( \theta \right) = {LaTeXComposer.GetScalarText(rotorAngle)}$");
            Console.WriteLine($@"Rotation Axis $v = {LaTeXComposer.GetMultivectorText(rotorAxis)}$");
            Console.WriteLine($@"Rotation Blade $B = {LaTeXComposer.GetMultivectorText(rotorBlade)}$");
            Console.WriteLine($@"$R \left( 0 \right) v_1 R^{{\sim}}\left( 0 \right) - v_2= {LaTeXComposer.GetMultivectorText(diff1)}$");
            Console.WriteLine($@"$R \left( \theta \right) v_1 R^{{\sim}}\left( \theta \right) - v_2= {LaTeXComposer.GetMultivectorText(diff2)}$");
            Console.WriteLine();
        }

        /// <summary>
        /// Covariance of rotations on blades
        /// </summary>
        public static void Example6()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);

            var a = CreateVector3D("a");
            var b = CreateVector3D("a");
            var c = CreateVector3D("a");

            var rotationBlade = 
                ("Subscript[B,12]".ToExpr() * e1.Gp(e2) +
                 "Subscript[B,23]".ToExpr() * e2.Gp(e3) +
                 "Subscript[B,13]".ToExpr() * e1.Gp(e3)).GetBivectorPart();

            var rotationAngle = @"\[Theta]".ToExpr();

            var rotor = GeometricProcessor.CreatePureRotor(
                rotationAngle, 
                rotationBlade
            );

            var ra = rotor.OmMap(a);
            var rb = rotor.OmMap(b);
            var rc = rotor.OmMap(c);

            var rab1 = ra.Gp(rb);
            var rab2 = rotor.OmMap(a.Gp(b));

            var rabc1 = ra.Gp(rb).Gp(rc);
            var rabc2 = rotor.OmMap(a.Gp(b).Gp(c));

            Console.WriteLine($@"${LaTeXComposer.GetMultivectorText(rab1 - rab2)}$");
            Console.WriteLine($@"${LaTeXComposer.GetMultivectorText(rabc1 - rabc2)}$");
            Console.WriteLine();
        }
    }
}

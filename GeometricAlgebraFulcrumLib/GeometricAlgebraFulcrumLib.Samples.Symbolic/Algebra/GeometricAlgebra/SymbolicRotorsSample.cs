using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Algebra.GeometricAlgebra;

public static class SymbolicRotorsSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;

    // Create a 6-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaProcessor<Expr> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanRGaProcessor();

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;


    private static RGaVector<Expr> CreateVector3D(string name, string subscript)
    {
        return Vector(name, subscript, 3);
    }

    private static RGaVector<Expr> CreateVector3D(string name)
    {
        return Vector(name, 3);
    }

    private static RGaVector<Expr> Vector(string name, string subscript, int termsCount)
    {
        var vector =
            $"Subscript[{name},{subscript}1]".ToExpr() * GeometricProcessor.VectorTerm(0);

        for (var i = 2; i <= termsCount; i++)
            vector += $"Subscript[{name},{subscript}{i}]".ToExpr() * GeometricProcessor.VectorTerm(i - 1);

        return vector;
    }

    private static RGaVector<Expr> Vector(string name, int termsCount)
    {
        var vector =
            $"Subscript[{name},1]".ToExpr() * GeometricProcessor.VectorTerm(0);

        for (var i = 2; i <= termsCount; i++)
            vector += $"Subscript[{name},{i}]".ToExpr() * GeometricProcessor.VectorTerm(i - 1);

        return vector;
    }



    /// <summary>
    /// Define a rotor in 3D using a 2-blade of rotation and an angle
    /// </summary>
    public static void Example1()
    {
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        // Define angle of rotation
        var angle = @"\[Theta]".RadiansToPolarAngle(ScalarProcessor);

        // Define 2-blade of rotation
        var rotationBlade =
            ("Subscript[B,12]".ToExpr() * e1.Gp(e2) +
             "Subscript[B,23]".ToExpr() * e2.Gp(e3) +
             "Subscript[B,13]".ToExpr() * e1.Gp(e3)).GetBivectorPart();

        // The rotation axis is only required here for validation
        var rotationAxis = rotationBlade.Gp(pseudoScalarInverse).GetVectorPart();

        // Compute the rotor from the angle and 2-blade of rotation
        var rotor = rotationBlade.CreatePureRotor(angle);

        // Make sure the eigen vector and eigen 2-blade of rotation are correct

        var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
        var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

        // Make sure the angle of rotation is correct
        // Create a general vector a and find its rotation b using rotor
        var a = CreateVector3D("a");
        var b = rotor.OmMap(a);

        // Compute the projection of a and b on rotation 2-blade
        var pa = a.ProjectOn(rotationBlade.ToSubspace());
        var pb = b.ProjectOn(rotationBlade.ToSubspace());

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
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        var angle = @"\[Theta]".RadiansToPolarAngle(ScalarProcessor);

        var rotationAxis = CreateVector3D("v");

        var rotationBlade =
            rotationAxis.Gp(pseudoScalarInverse).GetBivectorPart();

        // Compute the rotor from the angle and 2-blade of rotation
        var rotor = rotationBlade.CreatePureRotor(angle);

        // Make sure the eigen vector and eigen 2-blade of rotation are correct
        var diff1 = rotor.OmMap(rotationAxis) - rotationAxis;
        var diff2 = rotor.OmMap(rotationBlade) - rotationBlade;

        // Make sure the angle of rotation is correct
        // Create a general vector a and find its rotation b using rotor
        var a = CreateVector3D("a");
        var b = rotor.OmMap(a);

        // Compute the projection of a and b on rotation 2-blade
        var pa = a.ProjectOn(rotationBlade.ToSubspace());
        var pb = b.ProjectOn(rotationBlade.ToSubspace());

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
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        var u =
            "Subscript[u,1]".ToExpr() * e1 +
            "Subscript[u,2]".ToExpr() * e2 +
            "Subscript[u,3]".ToExpr() * e3;

        var v =
            "Subscript[v,1]".ToExpr() * e1 +
            "Subscript[v,2]".ToExpr() * e2 +
            "Subscript[v,3]".ToExpr() * e3;

        // Rotation angle
        var angle =
            u.ESp(v).ArcCos();

        // Compute the rotor from the angle and 2-blade of rotation
        var rotor = u.CreatePureRotor(v, true);

        var rotationBlade =
            rotor.Multivector.GetBivectorPart();

        var rotationAxis =
            rotationBlade.Gp(pseudoScalarInverse).GetVectorPart();

        // Make sure v1 rotates into v2
        var diff1 =
            (rotor.OmMap(u) - v).FullSimplifyScalars();

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
        var pa = a.ProjectOn(rotationBlade.ToSubspace());
        var pb = b.ProjectOn(rotationBlade.ToSubspace());

        // Compute angle between projected vectors, the result must equal
        // the original angle of rotation of the rotor
        var diff4 =
            ((pa.ESp(pb) / (pa.ENorm() * pb.ENorm())).ArcCos() - angle).ScalarValue.FullSimplify();

        // Make sure the projection of a on the rotation 2-blade is rotated
        // correctly into the projection of b on the rotation 2-blade
        var diff5 = rotor.OmMap(pa) - pb;

        Console.WriteLine($@"first vector  $v_1 = {LaTeXComposer.GetMultivectorText(u)}$");
        Console.WriteLine($@"second vector $v_2 = {LaTeXComposer.GetMultivectorText(v)}$");
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
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        // Define two unit vectors with angle phi between them
        var anglePhi = @"\[Phi]".ToExpr();
        var v1 = e1;
        var v2 = Mfs.Cos[anglePhi] * e1 + Mfs.Sin[anglePhi] * e2;

        // The free angle parameter theta
        var angleTheta = @"\[Theta]".RadiansToPolarAngle(ScalarProcessor);

        var assumption = $"Element[{angleTheta} | {anglePhi}, Reals] && {angleTheta} > -Pi / 2 && {angleTheta} < Pi / 2 && {anglePhi} > 0 && {anglePhi} < 2 Pi".ToExpr();

        // Define a rotor with angle theta in the plane orthogonal to v2 - v1
        var rotorSBlade = (v2 - v1).Gp(pseudoScalarInverse).GetBivectorPart();
        var rotorS = rotorSBlade.CreatePureRotor(angleTheta);

        // Create pure rotor that rotates v1 to v2 at theta = 0
        var rotor0 = v1.CreatePureRotor(v2, true);

        // The actual plane of rotation is made by rotating the plane of v1,v2
        // by angle theta in the plane orthogonal to v2 - v1
        var rotationBlade =
            rotorS
                .OmMap(v2.Op(v1))
                .FullSimplifyScalars(assumption);

        // Project v1, v2 into the actual plane of rotation
        var rotationBladeSubspace =
            rotationBlade.ToSubspace();

        var u1 = v1.ProjectOn(rotationBladeSubspace);
        var u2 = v2.ProjectOn(rotationBladeSubspace);

        // Define the actual rotor taking v1 into v2
        var rotationAngle =
            u1.GetEuclideanAngle(u2).ScalarValue.Simplify(assumption).RadiansToPolarAngle(ScalarProcessor);

        var rotor = rotationBlade.CreatePureRotor(rotationAngle);

        var rotationAxis =
            rotationBlade.Gp(pseudoScalarInverse).GetVectorPart().FullSimplifyScalars(assumption);

        // Make sure v1 rotates into v2 using the pure rotor at theta = 0
        var diff1 =
            (rotor0.OmMap(v1) - v2).FullSimplifyScalars();

        // Make sure u1 rotates into u2 using the actual rotor
        var diff2 =
            (rotor.OmMap(u1) - u2).FullSimplifyScalars();

        // Make sure v1 rotates into v2 using the actual rotor
        var diff3 =
            (rotor.OmMap(v1) - v2).FullSimplifyScalars();

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
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Op(e2).Op(e3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        // Define two unit vectors with angle phi between them
        var anglePhi = @"\[Phi]".ToExpr();
        var v1 = e1;
        var v2 = Mfs.Cos[anglePhi] * e1 + Mfs.Sin[anglePhi] * e2;
        var v1v2Dot = v1.ESp(v2);

        // The free angle parameter theta for rotor S
        var angleTheta = @"\[Theta]".RadiansToPolarAngle(ScalarProcessor);

        var assumption = $"Element[{angleTheta} | {anglePhi}, Reals] && {angleTheta} >= -Pi/2 && {angleTheta} <= Pi/2 && {anglePhi} >= 0 && {anglePhi} <= Pi".ToExpr();

        // Define parametric plane of rotation for rotor S
        var rotorSBlade = (v2 - v1).Gp(pseudoScalarInverse).GetBivectorPart();

        // Define parametric rotor S
        var rotorS = rotorSBlade.CreatePureRotor(angleTheta);

        // Create pure rotor that rotates v1 to v2 at theta = 0
        var rotor0 = v1.CreatePureRotor(v2, true);


        // Define parametric angle of rotation
        var rotorAngle =
            (1 + 2 * (v1v2Dot - 1) / (2 - Mfs.Power[Mfs.Sin[angleTheta], 2] * (v1v2Dot + 1))).ArcCos();

        // The actual plane of rotation is made by rotating the plane of v1,v2
        // by angle theta in the plane orthogonal to v2 - v1
        var rotorBlade =
            rotorS
                .OmMap(v2.Op(v1))
                .FullSimplifyScalars(assumption);

        var rotor = rotorBlade.CreatePureRotor(rotorAngle);

        var rotorAxis =
            rotorBlade.Gp(pseudoScalarInverse).GetVectorPart().FullSimplifyScalars(assumption);

        // Make sure v1 rotates into v2 using the pure rotor at theta = 0
        var diff1 =
            (rotor0.OmMap(v1) - v2).FullSimplifyScalars();

        // Make sure v1 rotates into v2 using the actual rotor
        var diff2 =
            (rotor.OmMap(v1) - v2).FullSimplifyScalars();

        var rotor0A =
            v1.CreateParametricPureRotor3D(v2, LinPolarAngle<Expr>.Angle0(ScalarProcessor)).Multivector;

        var rotorA =
            v1.CreateParametricPureRotor3D(v2, angleTheta).Multivector;

        // This should be 0
        var diff3 =
            (rotor.Multivector - rotorA).FullSimplifyScalars(assumption);

        // This should be 0
        var diff4 =
            (rotor0.Multivector - rotor0A).FullSimplifyScalars(assumption);

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
        Console.WriteLine($@"Diff3 = ${LaTeXComposer.GetMultivectorText(diff3)}$");
        Console.WriteLine($@"Diff4 = ${LaTeXComposer.GetMultivectorText(diff4)}$");
        Console.WriteLine();
    }

    /// <summary>
    /// Covariance of rotations on blades
    /// </summary>
    public static void Example6()
    {
        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);

        var a = CreateVector3D("a");
        var b = CreateVector3D("b");
        var c = CreateVector3D("c");

        var rotationBlade =
            ("Subscript[B,12]".ToExpr() * e1.Gp(e2) +
             "Subscript[B,23]".ToExpr() * e2.Gp(e3) +
             "Subscript[B,13]".ToExpr() * e1.Gp(e3)).GetBivectorPart();

        var rotationAngle = @"\[Theta]".RadiansToPolarAngle(ScalarProcessor);

        var rotor = rotationBlade.CreatePureRotor(rotationAngle);

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

    /// <summary>
    /// Create a scaled rotor
    /// </summary>
    public static void Example7()
    {
        var rotorA =
            "Subscript[a,0]".ToExpr() +
            GeometricProcessor.Bivector3D(
                "Subscript[a,12]".ToExpr(),
                "Subscript[a,13]".ToExpr(),
                "Subscript[a,23]".ToExpr()
            );

        var rotorB =
            "Subscript[b,0]".ToExpr() +
            GeometricProcessor.Bivector3D(
                "Subscript[b,12]".ToExpr(),
                "Subscript[b,13]".ToExpr(),
                "Subscript[b,23]".ToExpr()
            );

        var rotorAReverse = rotorA.Reverse();
        var rotorBReverse = rotorB.Reverse();

        var sA = rotorA.Gp(rotorAReverse);

        var u = GeometricProcessor.Vector(
            "Subscript[u,1]",
            "Subscript[u,2]",
            "Subscript[u,3]"
        );

        var v =
            rotorA.Gp(u).Gp(rotorBReverse) +
            rotorB.Gp(u).Gp(rotorAReverse);

        Console.WriteLine($@"$A = {LaTeXComposer.GetMultivectorText(rotorA)}$");
        Console.WriteLine($@"$B = {LaTeXComposer.GetMultivectorText(rotorB)}$");
        Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
        Console.WriteLine($@"$A u B^{{\sim}} + B u A^{{\sim}} = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine();
    }

    /// <summary>
    /// Create a pure scaled rotor based on two general vectors
    /// </summary>
    public static void Example8()
    {
        var u =
            GeometricProcessor.Vector(1, 0, 0);
        //GeometricProcessor.Vector("Subscript[u,1]", "Subscript[u,2]", "Subscript[u,3]");

        var v =
            GeometricProcessor.Vector("Subscript[v,1]", "Subscript[v,2]", "Subscript[v,3]");
        //GeometricProcessor.Vector(2, 1, -1);

        var scaledRotor = u.CreateScaledPureRotor(v);

        var scaledRotorInv =
            scaledRotor.GetPureScaledRotorInverse();

        var v1 =
            scaledRotor.OmMap(u).FullSimplifyScalars();

        var u1 =
            scaledRotorInv.OmMap(v).FullSimplifyScalars();

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
        var axis = LinSignedBasisVector.NegativeZ;
        var u =
            GeometricProcessor.Vector(
                "Subscript[u,1]",
                "Subscript[u,2]",
                "Subscript[u,3]"
            );

        var rotor1 = u.CreateScaledPureRotorFromAxis(axis, false);
        var rotor2 = u.CreateScaledPureRotorToAxis(axis, false);

        var v1 = rotor1.OmMap(axis).FullSimplifyScalars();
        var v2 = rotor2.OmMap(u).FullSimplifyScalars();

        Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
        Console.WriteLine($@"$R_1 = {LaTeXComposer.GetMultivectorText(rotor1)}$");
        Console.WriteLine($@"$R_2 = {LaTeXComposer.GetMultivectorText(rotor2)}$");
        Console.WriteLine($@"$R_1 e_1 R_1^{{\sim}} = {LaTeXComposer.GetMultivectorText(v1)}$");
        Console.WriteLine($@"$R_2 u R_2^{{\sim}} = {LaTeXComposer.GetMultivectorText(v2)}$");
        Console.WriteLine();
    }

    public static void Example10()
    {
        var e3 = GeometricProcessor.VectorTerm(2);

        var v =
            GeometricProcessor.Vector(
                "Subscript[v,1]",
                "Subscript[v,2]",
                "Subscript[v,3]",
                "Subscript[v,4]",
                "Subscript[v,5]"
            );

        var n = v.Op(e3);
        var nNormSquared = -n.Sp(n);
        var nUnit = n / nNormSquared.Sqrt();

        Console.WriteLine($@"$e_{{k}} = {LaTeXComposer.GetMultivectorText(e3)}$");
        Console.WriteLine($@"$v = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine($@"$N = v \wedge e_{{k}} = {LaTeXComposer.GetMultivectorText(n)}$");
        Console.WriteLine($@"$-N^2 = {LaTeXComposer.GetScalarText(nNormSquared)}$");
        Console.WriteLine();
    }

    /// <summary>
    /// Define rotors in 3D that rotate unit basis vectors to a given unit vector
    /// </summary>
    public static void Example11()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{e}";

        var assumeExpr =
            $@"And[Element[Subscript[v,1] | Subscript[v,2] | Subscript[v,3], Reals], Subscript[v,1]^2 + Subscript[v,2]^2 + Subscript[v,3]^2 == 1]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        ScalarProcessor.SimplificationFunc =
            expr => expr.FullSimplify();

        var vSpaceDimensions = 3;

        var e1 = GeometricProcessor.VectorTerm(0);
        var e2 = GeometricProcessor.VectorTerm(1);
        var e3 = GeometricProcessor.VectorTerm(2);
        var pseudoScalar = e1.Gp(e2).Gp(e3).GetKVectorPart(3);
        var pseudoScalarInverse = pseudoScalar.Inverse();

        var uVectors = new[]
        {
            e1, -e1, e2, -e2, e3, -e3
        };

        var v =
            "Subscript[v,1]".ToExpr() * e1 +
            "Subscript[v,2]".ToExpr() * e2 +
            "Subscript[v,3]".ToExpr() * e3;

        foreach (var u in uVectors)
        {
            // Compute the rotor from the angle and 2-blade of rotation
            var rotor = u.CreatePureRotor(v, true);

            var matrix =
                rotor.GetVectorMapPart(vSpaceDimensions).ToArray(vSpaceDimensions);

            // Create a general vector a and find its rotation b using rotor
            var a =
                "Subscript[a,1]".ToExpr() * e1 +
                "Subscript[a,2]".ToExpr() * e2 +
                "Subscript[a,3]".ToExpr() * e3;

            var b = rotor.OmMap(a);

            Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
            Console.WriteLine();

            Console.WriteLine($@"$v = {LaTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine();

            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(rotor)}$");
            Console.WriteLine();

            Console.WriteLine($@"$M = {LaTeXComposer.GetArrayText(matrix)}$");
            Console.WriteLine();

            Console.WriteLine($@"$a = {LaTeXComposer.GetMultivectorText(a)}$");
            Console.WriteLine();

            Console.WriteLine($@"$b = R a R^\dagger = {LaTeXComposer.GetMultivectorText(b)}$");
            Console.WriteLine();
        }
    }

}
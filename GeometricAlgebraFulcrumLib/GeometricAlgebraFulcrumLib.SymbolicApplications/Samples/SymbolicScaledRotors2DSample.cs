﻿using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples;

public static class SymbolicScaledRotors2DSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.DefaultProcessor;

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
    public static LaTeXComposerExpr LaTeXComposer { get; }
        = LaTeXComposerExpr.DefaultComposer;


    public static void Example1()
    {
        var e1p = GeometricProcessor.CreateTermVector(0);
        var e1n = -e1p;

        var e2p = GeometricProcessor.CreateTermVector(1);
        var e2n = -e2p;

        var a =
            "Subscript[a,0]".ToExpr() +
            GeometricProcessor.CreateBivector2D(
                "Subscript[a,12]".ToExpr()
            );

        var b =
            "Subscript[b,0]".ToExpr() +
            GeometricProcessor.CreateBivector2D(
                "Subscript[b,12]".ToExpr()
            );

        var u =
            GeometricProcessor.CreateVector(
                "Subscript[u,1]".ToExpr(),
                "Subscript[u,2]".ToExpr()
            );

        //var e1pRotated = a.Gp(e1p).Gp(a.Reverse());
        //var e2pRotated = a.Gp(e2p).Gp(a.Reverse());
        //var e3pRotated = a.Gp(e3p).Gp(a.Reverse());

        //var e1nRotated = a.Gp(e1n).Gp(a.Reverse());
        //var e2nRotated = a.Gp(e2n).Gp(a.Reverse());
        //var e3nRotated = a.Gp(e3n).Gp(a.Reverse());

        Console.WriteLine($@"$A = {LaTeXComposer.GetMultivectorText(a)}$");
        Console.WriteLine($@"$B = {LaTeXComposer.GetMultivectorText(b)}$");
        Console.WriteLine($@"$A A = {LaTeXComposer.GetMultivectorText(a.Gp(a))}$");
        Console.WriteLine($@"$A A^{{\sim}} = {LaTeXComposer.GetMultivectorText(a.Gp(a.Reverse()))}$");
        Console.WriteLine($@"$AB = {LaTeXComposer.GetMultivectorText(a.Gp(b))}$");
        Console.WriteLine($@"$BA = {LaTeXComposer.GetMultivectorText(b.Gp(a))}$");
        Console.WriteLine($@"$A u = {LaTeXComposer.GetMultivectorText(a.Gp(u))}$");
        Console.WriteLine($@"$u A^{{\sim}} = {LaTeXComposer.GetMultivectorText(u.Gp(a.Reverse()))}$");
        Console.WriteLine($@"$A u A^{{\sim}} = {LaTeXComposer.GetMultivectorText(a.Gp(u).Gp(a.Reverse()))}$");
        Console.WriteLine($@"$A A u = {LaTeXComposer.GetMultivectorText(a.Gp(a).Gp(u))}$");
        //Console.WriteLine($@"$A e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e1pRotated)}$");
        //Console.WriteLine($@"$A e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e2pRotated)}$");
        //Console.WriteLine($@"$A e_3 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e3pRotated)}$");

        Console.WriteLine();
    }

        
    public static void Example3()
    {
        var e1 = GeometricProcessor.CreateTermVector(0);
        var e2 = GeometricProcessor.CreateTermVector(1);

        var uVector = 
            GeometricProcessor.CreateVector("Subscript[u,1]", "Subscript[u,2]");
            
        var vVector = 
            GeometricProcessor.CreateVector("Subscript[v,1]", "Subscript[v,2]");

        var vuInv = vVector.Gp(uVector.Inverse());

        var vuWedge = vVector.Op(uVector);
        var vuWedgeSquared = vuWedge.Gp(vuWedge);
        var vuWedgeUnit = vuWedge / (-vuWedge.Sp(vuWedge)).Sqrt();

        var bBivector = 
            GeometricProcessor.CreateBivector2D("Subscript[b,12]");

        var xMultivector =
            "Subscript[x,0]".ToExpr() +
            GeometricProcessor.CreateVector("Subscript[x,1]", "Subscript[x,2]") +
            GeometricProcessor.CreateBivector2D("Subscript[x,12]");

        var a = GeometricProcessor.CreateScaledPureRotor2D(
            "Subscript[a, 0]",
            "Subscript[a, 12]"
        );

        var aInv = a.GetPureScaledRotorInverse();

        var b = GeometricProcessor.CreateScaledPureRotor2D(
            "Subscript[b, 0]",
            "Subscript[b, 12]"
        );

        var a2 =
            a.Multivector.Gp(a);

        var aa =
            a.Multivector.Gp(a.MultivectorReverse);

        var abba1 = 
            a.Multivector.Gp(b.MultivectorReverse) + 
            b.Multivector.Gp(a.MultivectorReverse);

        var abba2 = 
            a.Multivector.Gp(b.Multivector) + 
            b.Multivector.Gp(a.Multivector);
            
        var abba3 = 
            2 * a.Multivector.Gp(b.Multivector);

        var a2e1 =
            a2.Gp(e1);

        var ae1a = 
            a.Multivector.Gp(e1).Gp(a.MultivectorReverse);

        var a2e2 =
            a2.Gp(e2);

        var ae2a = 
            a.Multivector.Gp(e2).Gp(a.MultivectorReverse);

        var abe1 = 
            abba3.Gp(e1);

        var ae1b = 
            a.Multivector.Gp(e1).Gp(b.MultivectorReverse) + 
            b.Multivector.Gp(e1).Gp(a.MultivectorReverse);

        var abe2 = 
            abba3.Gp(e2);

        var ae2b = 
            a.Multivector.Gp(e2).Gp(b.MultivectorReverse) + 
            b.Multivector.Gp(e2).Gp(a.MultivectorReverse);


        Console.WriteLine($@"$A^2 = {LaTeXComposer.GetMultivectorText(a2)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A A^{{\sim}} = {LaTeXComposer.GetMultivectorText(aa)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A^{{-1}} = {LaTeXComposer.GetMultivectorText(aInv)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A B^{{\sim}} + B A^{{\sim}} = {LaTeXComposer.GetMultivectorText(abba1)}$");
        Console.WriteLine($@"$A B + B A = {LaTeXComposer.GetMultivectorText(abba2)}$");
        Console.WriteLine($@"$2 A B = {LaTeXComposer.GetMultivectorText(abba3)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae1a)}$");
        Console.WriteLine($@"$A^2 e_1 = {LaTeXComposer.GetMultivectorText(a2e1)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae2a)}$");
        Console.WriteLine($@"$A^2 e_2 = {LaTeXComposer.GetMultivectorText(a2e2)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A e_1 B^{{\sim}} + B e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae1b)}$");
        Console.WriteLine($@"$2 A B e_1 = {LaTeXComposer.GetMultivectorText(abe1)}$");
        Console.WriteLine();

        Console.WriteLine($@"$A e_2 B^{{\sim}} + B e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae2b)}$");
        Console.WriteLine($@"$2 A B e_2 = {LaTeXComposer.GetMultivectorText(abe2)}$");
        Console.WriteLine();


        Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(uVector)}$");
        Console.WriteLine($@"$v = {LaTeXComposer.GetMultivectorText(vVector)}$");
        Console.WriteLine($@"$C = {LaTeXComposer.GetMultivectorText(bBivector)}$");
        Console.WriteLine($@"$X = {LaTeXComposer.GetMultivectorText(xMultivector)}$");
        Console.WriteLine();

        Console.WriteLine($@"$v u^{{-1}} = {LaTeXComposer.GetMultivectorText(vuInv)}$");
        Console.WriteLine($@"$\left(\boldsymbol{{v}}\wedge\boldsymbol{{u}}\right)\left(\boldsymbol{{v}}\wedge\boldsymbol{{u}}\right) = {LaTeXComposer.GetMultivectorText(vuWedgeSquared)}$");
        Console.WriteLine($@"$\frac{{\boldsymbol{{v}}\wedge\boldsymbol{{u}}}}{{\sqrt{{-\left(\boldsymbol{{v}}\wedge\boldsymbol{{u}}\right)\left(\boldsymbol{{v}}\wedge\boldsymbol{{u}}\right)}}}} = {LaTeXComposer.GetMultivectorText(vuWedgeUnit)}$");

        Console.WriteLine($@"$A v A^{{\sim}} = {LaTeXComposer.GetMultivectorText(a.OmMap(uVector))}$");
        Console.WriteLine($@"$A C A^{{\sim}} = {LaTeXComposer.GetMultivectorText(a.OmMap(bBivector))}$");
        Console.WriteLine($@"$A X A^{{\sim}} = {LaTeXComposer.GetMultivectorText(a.OmMap(xMultivector))}$");
        Console.WriteLine();

    }
}
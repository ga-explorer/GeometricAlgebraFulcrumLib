using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class SymbolicScaledRotors3DSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;

        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; }
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Example1()
        {
            var e1p = GeometricProcessor.CreateVectorBasis(0);
            var e1n = -e1p;

            var e2p = GeometricProcessor.CreateVectorBasis(1);
            var e2n = -e2p;

            var e3p = GeometricProcessor.CreateVectorBasis(2);
            var e3n = -e3p;

            var a =
                "Subscript[a,0]".ToExpr() +
                GeometricProcessor.CreateBivector(
                    "Subscript[a,12]".ToExpr(),
                    "Subscript[a,13]".ToExpr(),
                    "Subscript[a,23]".ToExpr()
                );

            var e1pRotated = a.Gp(e1p).Gp(a.Reverse());
            var e2pRotated = a.Gp(e2p).Gp(a.Reverse());
            var e3pRotated = a.Gp(e3p).Gp(a.Reverse());

            var e1nRotated = a.Gp(e1n).Gp(a.Reverse());
            var e2nRotated = a.Gp(e2n).Gp(a.Reverse());
            var e3nRotated = a.Gp(e3n).Gp(a.Reverse());

            Console.WriteLine($@"$A e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e1pRotated)}$");
            Console.WriteLine($@"$A e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e2pRotated)}$");
            Console.WriteLine($@"$A e_3 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(e3pRotated)}$");

            Console.WriteLine();
        }

        public static void Example2()
        {
            var e1p = GeometricProcessor.CreateVectorBasis(0);
            var e1n = -e1p;

            var e2p = GeometricProcessor.CreateVectorBasis(1);
            var e2n = -e2p;

            var e3p = GeometricProcessor.CreateVectorBasis(2);
            var e3n = -e3p;

            var u = GeometricProcessor.CreateVector(
                "Subscript[u,1]".ToExpr(),
                "Subscript[u,2]".ToExpr(),
                "Subscript[u,3]".ToExpr()
            );

            var r1p = GeometricProcessor.CreateScaledPureRotor(u, e1p);
            var r1pArray = r1p.GetMultivectorsMappingArray(3, 3);

            var r2p = GeometricProcessor.CreateScaledPureRotor(u, e2p);
            var r2pArray = r2p.GetMultivectorsMappingArray(3, 3);

            var r3p = GeometricProcessor.CreateScaledPureRotor(u, e3p);
            var r3pArray = r3p.GetMultivectorsMappingArray(3, 3);

            Console.WriteLine($@"$R_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{1}}}} = {LaTeXComposer.GetMultivectorText(r1p)}$");
            Console.WriteLine($@"$M_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{1}}}} = {LaTeXComposer.GetArrayText(r1pArray)}$");
            Console.WriteLine();

            Console.WriteLine($@"$R_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{2}}}} = {LaTeXComposer.GetMultivectorText(r2p)}$");
            Console.WriteLine($@"$M_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{2}}}} = {LaTeXComposer.GetArrayText(r2pArray)}$");
            Console.WriteLine();

            Console.WriteLine($@"$R_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{3}}}} = {LaTeXComposer.GetMultivectorText(r3p)}$");
            Console.WriteLine($@"$M_{{\boldsymbol{{u}}\boldsymbol{{e}}_{{3}}}} = {LaTeXComposer.GetArrayText(r3pArray)}$");
            Console.WriteLine();
        }
        

        public static void Example3()
        {
            var e1 = GeometricProcessor.CreateVectorBasis(0);
            var e2 = GeometricProcessor.CreateVectorBasis(1);
            var e3 = GeometricProcessor.CreateVectorBasis(2);

            var a = GeometricProcessor.CreateScaledPureRotor3D(
                "Subscript[a, 0]".ToExpr(),
                "Subscript[a, 12]".ToExpr(),
                "Subscript[a, 13]".ToExpr(),
                "Subscript[a, 23]".ToExpr()
            );

            var b = GeometricProcessor.CreateScaledPureRotor3D(
                "Subscript[b, 0]".ToExpr(),
                "Subscript[b, 12]".ToExpr(),
                "Subscript[b, 13]".ToExpr(),
                "Subscript[b, 23]".ToExpr()
            );
            
            var a2 =
                a.Multivector.Gp(a);

            var aa =
                a.Multivector.Gp(a.MultivectorReverse);

            var ab = 
                a.Multivector.Gp(b.MultivectorReverse) + 
                b.Multivector.Gp(a.MultivectorReverse);

            var ae1a = 
                a.Multivector.Gp(e1).Gp(a.MultivectorReverse);

            var ae2a = 
                a.Multivector.Gp(e2).Gp(a.MultivectorReverse);

            var ae3a = 
                a.Multivector.Gp(e3).Gp(a.MultivectorReverse);

            var ae1b = 
                a.Multivector.Gp(e1).Gp(b.MultivectorReverse) + 
                b.Multivector.Gp(e1).Gp(a.MultivectorReverse);

            var ae2b = 
                a.Multivector.Gp(e2).Gp(b.MultivectorReverse) + 
                b.Multivector.Gp(e2).Gp(a.MultivectorReverse);
            
            var ae3b = 
                a.Multivector.Gp(e3).Gp(b.MultivectorReverse) + 
                b.Multivector.Gp(e3).Gp(a.MultivectorReverse);
            
            Console.WriteLine($@"$A^2 = {LaTeXComposer.GetMultivectorText(a2)}$");
            Console.WriteLine();

            Console.WriteLine($@"$A A^{{\sim}} = {LaTeXComposer.GetMultivectorText(aa)}$");
            Console.WriteLine();

            Console.WriteLine($@"$A B^{{\sim}} + B A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ab)}$");
            Console.WriteLine();

            Console.WriteLine($@"$A e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae1a)}$");
            Console.WriteLine($@"$A e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae2a)}$");
            Console.WriteLine($@"$A e_3 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae3a)}$");
            Console.WriteLine();

            Console.WriteLine($@"$A e_1 B^{{\sim}} + B e_1 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae1b)}$");
            Console.WriteLine($@"$A e_2 B^{{\sim}} + B e_2 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae2b)}$");
            Console.WriteLine($@"$A e_3 B^{{\sim}} + B e_3 A^{{\sim}} = {LaTeXComposer.GetMultivectorText(ae3b)}$");
            Console.WriteLine();
        }
    }
}
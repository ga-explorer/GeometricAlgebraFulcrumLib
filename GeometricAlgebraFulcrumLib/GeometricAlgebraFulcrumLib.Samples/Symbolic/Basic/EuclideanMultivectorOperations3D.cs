using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GAPoTNumLib.Text.Markdown.Tables;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text;
using TextComposerLib.Text.Markdown;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Basic
{
    public static class EuclideanMultivectorOperations3D
    {
        public static BasisBladeSet BasisSet { get; }
            = BasisBladeSet.CreateEuclidean(3);

        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
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

        public static MarkdownComposer Composer { get; }
            = new MarkdownComposer();


        private static Multivector<Expr> CreateScalar(string name)
        {
            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.CreateKVectorScalarStorage(name.ToExpr())
            );
        }

        private static Multivector<Expr> CreateVector(string name)
        {
            name = name.ToLower();

            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.CreateVectorStorage(
                    $"Subscript[{name},1]".ToExpr(), 
                    $"Subscript[{name},2]".ToExpr(), 
                    $"Subscript[{name},3]".ToExpr()
                )
            );
        }
        
        private static Multivector<Expr> CreateBivector(string name)
        {
            name = name.ToLower();

            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.CreateBivectorStorage(
                    $"Subscript[{name},12]".ToExpr(), 
                    $"Subscript[{name},13]".ToExpr(), 
                    $"Subscript[{name},23]".ToExpr()
                )
            );
        }

        private static Multivector<Expr> CreateTrivector(string name)
        {
            name = name.ToLower();

            return GeometricProcessor.CreateMultivector(
                GeometricProcessor.CreateKVectorStorage(
                    3, 
                    new[]{$"Subscript[{name},123]".ToExpr()}
                )
            );
        }

        private static Multivector<Expr> CreateEvenMultivector(string name)
        {
            name = name.ToLower();

            return CreateScalar($"Subscript[{name},0]") + CreateBivector(name);
        }
        
        private static Multivector<Expr> CreateOddMultivector(string name)
        {
            name = name.ToLower();

            return CreateVector(name) + CreateTrivector(name);
        }
        
        private static Multivector<Expr> CreateMultivector(string name)
        {
            name = name.ToLower();

            return CreateScalar($"Subscript[{name},0]") + 
                   CreateVector(name) + 
                   CreateBivector(name) +
                   CreateTrivector(name);
        }

        private static IReadOnlyList<BasisBlade> GetBasisBlades()
        {
            var basisBladesArray = new BasisBlade[8];

            basisBladesArray[0] = BasisBladeFactory.BasisScalar;

            basisBladesArray[1] = 0.CreateBasisVector();
            basisBladesArray[2] = 1.CreateBasisVector();
            basisBladesArray[3] = 2.CreateBasisVector();

            basisBladesArray[4] = 0.CreateBasisBivector();
            basisBladesArray[5] = 1.CreateBasisBivector();
            basisBladesArray[6] = 2.CreateBasisBivector();

            basisBladesArray[7] = BasisBladeFactory.CreateBasisBlade(7);

            return basisBladesArray;
        }

        private static MarkdownTable CreateEmptyProductTable()
        {
            var mdTable = new MarkdownTable();

            var firstColumn = mdTable.AddColumn("basis");

            firstColumn.Add("$1$");

            firstColumn.Add(@"$\boldsymbol{e}_{1}$");
            firstColumn.Add(@"$\boldsymbol{e}_{2}$");
            firstColumn.Add(@"$\boldsymbol{e}_{3}$");

            firstColumn.Add(@"$\boldsymbol{e}_{12}$");
            firstColumn.Add(@"$\boldsymbol{e}_{13}$");
            firstColumn.Add(@"$\boldsymbol{e}_{23}$");

            firstColumn.Add(@"$\boldsymbol{I}$");

            mdTable.AddColumn("1", "$1$");

            mdTable.AddColumn("e1", @"$\boldsymbol{e}_{1}$");
            mdTable.AddColumn("e2", @"$\boldsymbol{e}_{2}$");
            mdTable.AddColumn("e3", @"$\boldsymbol{e}_{3}$");

            mdTable.AddColumn("e12", @"$\boldsymbol{e}_{12}$");
            mdTable.AddColumn("e13", @"$\boldsymbol{e}_{13}$");
            mdTable.AddColumn("e23", @"$\boldsymbol{e}_{23}$");

            mdTable.AddColumn("e123", @"$\boldsymbol{I}$");

            return mdTable;
        }

        private static MarkdownTable CreateProductTable(Func<ulong, ulong, int> productSignatureFunc)
        {
            var basisBladesList = GetBasisBlades();
            var mdTable = CreateEmptyProductTable();

            for (var colIndex = 0; colIndex < basisBladesList.Count; colIndex++)
            {
                var mdTableColumn = mdTable[1 + colIndex];

                var basisBlade2 = basisBladesList[colIndex];
                for (var rowIndex = 0; rowIndex < basisBladesList.Count; rowIndex++)
                {
                    var basisBlade1 = basisBladesList[rowIndex];

                    var id = basisBlade1.Id ^ basisBlade2.Id;
                    var signature = productSignatureFunc(basisBlade1.Id, basisBlade2.Id);

                    if (id == 0)
                    {
                        mdTableColumn.Add(signature == 0 ? "" : $"${signature}$");
                    }
                    else if (id == 7)
                    {
                        var text = signature switch
                        {
                            0 => "",
                            1 => @"$\boldsymbol{I}$",
                            _ => @"$-\boldsymbol{I}$"
                        };

                        mdTableColumn.Add(text);
                    }
                    else
                    {
                        var idText =
                            id
                                .PatternToPositions()
                                .Select(n => (n + 1).ToString())
                                .Concatenate();

                        var text = signature switch
                        {
                            0 => "",
                            1 => $@"$\boldsymbol{{e}}_{{{idText}}}$",
                            _ => $@"$-\boldsymbol{{e}}_{{{idText}}}$"
                        };

                        mdTableColumn.Add(text);
                    }
                }
            }

            return mdTable;
        }
        

        public static void Execute()
        {
            var invI = 
                GeometricProcessor.PseudoScalarInverse.CreateKVector(GeometricProcessor);

            var mvList1 = new Dictionary<string, Multivector<Expr>>();
            var mvList2 = new Dictionary<string, Multivector<Expr>>();

            mvList1.Add("a", CreateScalar("a"));
            mvList2.Add("b", CreateScalar("b"));

            mvList1.Add("u", CreateVector("u"));
            mvList2.Add("v", CreateVector("v"));

            mvList1.Add("C", CreateBivector("C"));
            mvList2.Add("D", CreateBivector("D"));

            mvList1.Add("X", CreateTrivector("X"));
            mvList2.Add("Y", CreateTrivector("Y"));

            mvList1.Add("R", CreateEvenMultivector("R"));
            mvList2.Add("Q", CreateEvenMultivector("Q"));

            mvList1.Add("T", CreateOddMultivector("T"));
            mvList2.Add("S", CreateOddMultivector("S"));

            mvList1.Add("M", CreateMultivector("M"));
            mvList2.Add("N", CreateMultivector("N"));

            Composer.AppendHeader("Basic Operations on 3D Euclidean Multivectors", 1);
            Composer.AppendHeader("Product Tables", 2);

            Composer.AppendHeader("Geometric Product Table:", 3);
            Composer.AppendLineAtNewLine(CreateProductTable(BasisSet.GpSign).ToString());
            Composer.AppendLineAtNewLine();

            Composer.AppendHeader("Outer Product Table:", 3);
            Composer.AppendLineAtNewLine(CreateProductTable(BasisSet.OpSign).ToString());
            Composer.AppendLineAtNewLine();

            Composer.AppendHeader("Left Contraction Product Table:", 3);
            Composer.AppendLineAtNewLine(CreateProductTable(BasisSet.LcpSign).ToString());
            Composer.AppendLineAtNewLine();

            Composer.AppendHeader("Right Contraction Product Table:", 3);
            Composer.AppendLineAtNewLine(CreateProductTable(BasisSet.RcpSign).ToString());
            Composer.AppendLineAtNewLine();

            Composer.AppendHeader("Multivector Definitions", 2);

            foreach (var ((name1, mv1), (name2, mv2)) in mvList1.Zip(mvList2))
            {
                Composer.AppendAtNewLine($"${name1} = {LaTeXComposer.GetMultivectorText(mv1)}$");
                Composer.AppendAtNewLine($"${name2} = {LaTeXComposer.GetMultivectorText(mv2)}$");

                Composer.AppendLineAtNewLine();
            }

            Composer.AppendHeader("Unary Operations", 2);

            foreach (var (name, mv) in mvList1)
            {
                Composer.AppendAtNewLine($"$\\hat{{{name}}} = {LaTeXComposer.GetMultivectorText(mv.GradeInvolution())}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendAtNewLine($"${name}^{{\\sim}} = {LaTeXComposer.GetMultivectorText(mv.Reverse())}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendAtNewLine($"${name}^{{-1}} = {LaTeXComposer.GetMultivectorText(mv.Inverse())}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendAtNewLine($"${name}^{{2}} = {LaTeXComposer.GetMultivectorText(mv.Gp())}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendAtNewLine($@"${name} \rfloor I^{{-1}} = {LaTeXComposer.GetMultivectorText(mv.Gp(invI))}$");
                Composer.AppendLineAtNewLine();

                //Composer.AppendAtNewLine($"${name} {name} = {LaTeXComposer.GetMultivectorText(mv.Gp(mv))}$");
                //Composer.AppendLineAtNewLine();
                
                Composer.AppendAtNewLine($"${name} {name}^{{\\sim}} = {LaTeXComposer.GetMultivectorText(mv.GpReverse())}$");
                Composer.AppendLineAtNewLine();
                
                //Composer.AppendAtNewLine($"${name} {name}^{{\\sim}} = {LaTeXComposer.GetMultivectorText(mv.Gp(mv.Reverse()))}$");
                //Composer.AppendLineAtNewLine();
                
                Composer.AppendAtNewLine($@"$\left\Vert {name} \right\Vert ^{2} = {LaTeXComposer.GetScalarText(mv.NormSquared())}$");
                Composer.AppendLineAtNewLine();
                
                //Composer.AppendAtNewLine($@"${name} * {name}^{{\sim}} = {LaTeXComposer.GetScalarText(mv.Sp(mv.Reverse()))}$");
                //Composer.AppendLineAtNewLine();

                Composer.AppendLineAtNewLine();
            }

            Composer.AppendHeader("Binary Operations", 2);

            foreach (var (name1, mv1) in mvList1)
            {
                foreach (var (name2, mv2) in mvList2)
                {
                    Composer.AppendAtNewLine($@"${name1} {name2} = {LaTeXComposer.GetMultivectorText(mv1.Gp(mv2))}$");
                    Composer.AppendLineAtNewLine();

                    Composer.AppendAtNewLine($@"${name1} * {name2} = {LaTeXComposer.GetScalarText(mv1.Sp(mv2))}$");
                    Composer.AppendLineAtNewLine();

                    Composer.AppendAtNewLine($@"${name1} \wedge {name2} = {LaTeXComposer.GetMultivectorText(mv1.Op(mv2))}$");
                    Composer.AppendLineAtNewLine();

                    Composer.AppendAtNewLine($@"${name1} \rfloor {name2} = {LaTeXComposer.GetMultivectorText(mv1.Lcp(mv2))}$");
                    Composer.AppendLineAtNewLine();

                    Composer.AppendAtNewLine($@"${name1} \lfloor {name2} = {LaTeXComposer.GetMultivectorText(mv1.Rcp(mv2))}$");
                    Composer.AppendLineAtNewLine();

                    Composer.AppendLineAtNewLine();
                }
            }

            Composer.AppendHeader("Versor Product Operations", 2);

            var scaledRotor = mvList1["R"];
            var scaledReflectedRotor = mvList1["T"];
            foreach (var (name, mv) in mvList2)
            {
                Composer.AppendAtNewLine($@"$R {name} R^{{\sim}} = {LaTeXComposer.GetMultivectorText(scaledRotor.Gp(mv).Gp(scaledRotor.Reverse()))}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendAtNewLine($@"$T {name} T^{{\sim}} = {LaTeXComposer.GetMultivectorText(scaledReflectedRotor.Gp(mv).Gp(scaledReflectedRotor.Reverse()))}$");
                Composer.AppendLineAtNewLine();

                Composer.AppendLineAtNewLine();
            }

            Console.WriteLine(Composer.ToString());
        }
    }
}

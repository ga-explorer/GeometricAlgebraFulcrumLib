using GAPoTNumLib.Text.Markdown.Tables;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Markdown;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Samples.Basic;

public static class EuclideanMultivectorOperations3D
{
    public static int VSpaceDimensions { get; }
        = 3;

    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;
            
    // Create a 3-dimensional Euclidean geometric algebra processor based on the
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

    public static MarkdownComposer Composer { get; }
        = new MarkdownComposer();


    private static RGaMultivector<Expr> CreateScalar(string name)
    {
        return GeometricProcessor.Scalar(name);
    }

    private static RGaMultivector<Expr> Vector(string name)
    {
        name = name.ToLower();

        return GeometricProcessor.Vector(
            $"Subscript[{name},1]".ToExpr(), 
            $"Subscript[{name},2]".ToExpr(), 
            $"Subscript[{name},3]".ToExpr()
        );
    }
        
    private static RGaMultivector<Expr> Bivector(string name)
    {
        name = name.ToLower();

        return GeometricProcessor.Bivector3D(
            $"Subscript[{name},12]", 
            $"Subscript[{name},13]", 
            $"Subscript[{name},23]"
        );
    }

    private static RGaMultivector<Expr> CreateTrivector(string name)
    {
        name = name.ToLower();

        return GeometricProcessor.KVectorTerm(
            3, 
            $"Subscript[{name},123]".ToExpr()
        );
    }

    private static RGaMultivector<Expr> CreateEvenMultivector(string name)
    {
        name = name.ToLower();

        return CreateScalar($"Subscript[{name},0]") + Bivector(name);
    }
        
    private static RGaMultivector<Expr> CreateOddMultivector(string name)
    {
        name = name.ToLower();

        return Vector(name) + CreateTrivector(name);
    }
        
    private static RGaMultivector<Expr> Multivector(string name)
    {
        name = name.ToLower();

        return CreateScalar($"Subscript[{name},0]") + 
               Vector(name) + 
               Bivector(name) +
               CreateTrivector(name);
    }

    private static IReadOnlyList<RGaBasisBlade> GetBasisBlades()
    {
        var basisBladesArray = new RGaBasisBlade[8];

        basisBladesArray[0] = GeometricProcessor.BasisScalar;

        basisBladesArray[1] = GeometricProcessor.CreateBasisVector(0);
        basisBladesArray[2] = GeometricProcessor.CreateBasisVector(1);
        basisBladesArray[3] = GeometricProcessor.CreateBasisVector(2);

        basisBladesArray[4] = GeometricProcessor.CreateBasisBivector(0, 1);
        basisBladesArray[5] = GeometricProcessor.CreateBasisBivector(0, 2);
        basisBladesArray[6] = GeometricProcessor.CreateBasisBivector(1, 2);

        basisBladesArray[7] = GeometricProcessor.CreateBasisBlade(3, 0);

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

    private static MarkdownTable CreateProductTable(Func<ulong, ulong, IntegerSign> productSignatureFunc)
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
                    mdTableColumn.Add(signature.IsZero ? "" : $"${signature}$");
                }
                else if (id == 7)
                {
                    var text = signature.Value switch
                    {
                        0 => "",
                        > 0 => @"$\boldsymbol{I}$",
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

                    var text = signature.Value switch
                    {
                        0 => "",
                        > 0 => $@"$\boldsymbol{{e}}_{{{idText}}}$",
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
            GeometricProcessor.PseudoScalarInverse(VSpaceDimensions);

        var mvList1 = new Dictionary<string, RGaMultivector<Expr>>();
        var mvList2 = new Dictionary<string, RGaMultivector<Expr>>();

        mvList1.Add("a", CreateScalar("a"));
        mvList2.Add("b", CreateScalar("b"));

        mvList1.Add("u", Vector("u"));
        mvList2.Add("v", Vector("v"));

        mvList1.Add("C", Bivector("C"));
        mvList2.Add("D", Bivector("D"));

        mvList1.Add("X", CreateTrivector("X"));
        mvList2.Add("Y", CreateTrivector("Y"));

        mvList1.Add("R", CreateEvenMultivector("R"));
        mvList2.Add("Q", CreateEvenMultivector("Q"));

        mvList1.Add("T", CreateOddMultivector("T"));
        mvList2.Add("S", CreateOddMultivector("S"));

        mvList1.Add("M", Multivector("M"));
        mvList2.Add("N", Multivector("N"));

        Composer.AppendHeader("Basic Operations on 3D Euclidean Multivectors", 1);
        Composer.AppendHeader("Product Tables", 2);

        Composer.AppendHeader("Geometric Product Table:", 3);
        Composer.AppendLineAtNewLine(CreateProductTable(GeometricProcessor.GpSign).ToString());
        Composer.AppendLineAtNewLine();

        Composer.AppendHeader("Outer Product Table:", 3);
        Composer.AppendLineAtNewLine(CreateProductTable(GeometricProcessor.OpSign).ToString());
        Composer.AppendLineAtNewLine();

        Composer.AppendHeader("Left Contraction Product Table:", 3);
        Composer.AppendLineAtNewLine(CreateProductTable(GeometricProcessor.LcpSign).ToString());
        Composer.AppendLineAtNewLine();

        Composer.AppendHeader("Right Contraction Product Table:", 3);
        Composer.AppendLineAtNewLine(CreateProductTable(GeometricProcessor.RcpSign).ToString());
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

            Composer.AppendAtNewLine($"${name}^{{2}} = {LaTeXComposer.GetMultivectorText(mv.Gp(mv))}$");
            Composer.AppendLineAtNewLine();

            Composer.AppendAtNewLine($@"${name} \rfloor I^{{-1}} = {LaTeXComposer.GetMultivectorText(mv.Gp(invI))}$");
            Composer.AppendLineAtNewLine();

            //Composer.AppendAtNewLine($"${name} {name} = {LaTeXComposer.GetMultivectorText(mv.Gp(mv))}$");
            //Composer.AppendLineAtNewLine();
                
            Composer.AppendAtNewLine($"${name} {name}^{{\\sim}} = {LaTeXComposer.GetMultivectorText(mv.Gp(mv.Reverse()))}$");
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
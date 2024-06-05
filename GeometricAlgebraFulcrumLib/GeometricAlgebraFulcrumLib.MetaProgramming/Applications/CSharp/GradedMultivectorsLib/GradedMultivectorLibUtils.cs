using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

internal static class GradedMultivectorLibUtils
{
    public static string GetBasisBladeCode(int id)
    {
        return id
            .PatternToPositions()
            .Select(i => i + 1)
            .Concatenate();
    }

    public static string GetBasisBladeCode(int grade, int index)
    {
        var id = (int) BasisBladeUtils.BasisBladeGradeIndexToId(grade, (ulong)index);

        return GetBasisBladeCode(id);
    }

    public static string GetBasisBladeCode(RGaBasisBlade basisBlade)
    {
        var id = (int) basisBlade.Id;

        return GetBasisBladeCode(id);
    }

    public static string GetBasisBladeCode(int id, Func<string, string> codeFunc)
    {
        return codeFunc(GetBasisBladeCode(id));
    }

    public static string GetBasisBladeCode(int grade, int index, Func<string, string> codeFunc)
    {
        return codeFunc(GetBasisBladeCode(grade, index));
    }

    public static string GetBasisBladeCode(RGaBasisBlade basisBlade, Func<string, string> codeFunc)
    {
        return codeFunc(GetBasisBladeCode(basisBlade));
    }


    public static string GetBasisBladeScalarName(int id)
    {
        return GetBasisBladeCode(
            id,
            idText => $"Scalar{idText}"
        );
    }

    public static string GetBasisBladeScalarName(int grade, int index)
    {
        return GetBasisBladeCode(
            grade,
            index,
            idText => $"Scalar{idText}"
        );
    }

    public static string GetBasisBladeScalarName(RGaBasisBlade basisBlade)
    {
        return GetBasisBladeCode(
            basisBlade,
            idText => $"Scalar{idText}"
        );
    }


    public static string GetRhsCode(this RGaFloat64UnilinearCombinationTerm term, Func<string, string> argNameFunc)
    {
        Debug.Assert(!term.IsInputScalarZero);

        var idText = GetBasisBladeCode((int) term.InputBasisBladeId, argNameFunc);
        
        string scalarText;

        if (term.InputScalar.IsOne())
            scalarText = "+";

        else if (term.InputScalar.IsNegativeOne())
            scalarText = "-";
        
        else if (term.IsInputScalarPositive)
            scalarText = $"+ {term.InputScalar:G} *";

        else 
            scalarText = $"- {-term.InputScalar:G} *";

        return $"{scalarText} {idText}";
    }

    public static string GetRhsCode(this IEnumerable<RGaFloat64UnilinearCombinationTerm> termList, Func<string, string> argNameFunc)
    {
        var termCodeList = 
            termList
                .Select(term => term.GetRhsCode(argNameFunc))
                .ToArray();

        if (termCodeList.Length == 0) 
            return "0";

        var term0 = termCodeList[0];

        if (term0[0] == '+')
            termCodeList[0] = term0[2..];

        else if (term0[0] == '-')
            termCodeList[0] = "-" + term0[2..];

        return termCodeList.Concatenate(" ");
    }

    public static string GetRhsCode(this RGaFloat64BilinearCombinationTerm term, Func<string, string> arg1NameFunc, Func<string, string> arg2NameFunc)
    {
        Debug.Assert(!term.IsInputScalarZero);

        var id1Text = GetBasisBladeCode((int) term.Input1BasisBladeId, arg1NameFunc);
        var id2Text = GetBasisBladeCode((int) term.Input2BasisBladeId, arg2NameFunc);

        string scalarText;

        if (term.InputScalar.IsOne())
            scalarText = "+";

        else if (term.InputScalar.IsNegativeOne())
            scalarText = "-";
        
        else if (term.IsInputScalarPositive)
            scalarText = $"+ {term.InputScalar:G} *";

        else 
            scalarText = $"- {-term.InputScalar:G} *";

        return $"{scalarText} {id1Text} * {id2Text}";
    }

    public static string GetRhsCode(this IEnumerable<RGaFloat64BilinearCombinationTerm> termList, Func<string, string> arg1NameFunc, Func<string, string> arg2NameFunc)
    {
        var termCodeList =
            termList
                .Select(term => term.GetRhsCode(arg1NameFunc, arg2NameFunc))
                .ToArray();

        if (termCodeList.Length == 0)
            return "0";

        var term0 = termCodeList[0];

        if (term0[0] == '+')
            termCodeList[0] = term0[2..];

        else if (term0[0] == '-')
            termCodeList[0] = "-" + term0[2..];

        return termCodeList.Concatenate(" ");
    }
    
    public static string GetRhsCode(this RGaFloat64TrilinearCombinationTerm term, Func<string, string> arg1NameFunc, Func<string, string> arg2NameFunc, Func<string, string> arg3NameFunc)
    {
        Debug.Assert(!term.IsInputScalarZero);

        var id1Text = GetBasisBladeCode((int) term.Input1BasisBladeId, arg1NameFunc);
        var id2Text = GetBasisBladeCode((int) term.Input2BasisBladeId, arg2NameFunc);
        var id3Text = GetBasisBladeCode((int) term.Input3BasisBladeId, arg3NameFunc);

        string scalarText;

        if (term.InputScalar.IsOne())
            scalarText = "+";

        else if (term.InputScalar.IsNegativeOne())
            scalarText = "-";
        
        else if (term.IsInputScalarPositive)
            scalarText = $"+ {term.InputScalar:G} *";

        else 
            scalarText = $"- {-term.InputScalar:G} *";

        return $"{scalarText} {id1Text} * {id2Text} * {id3Text}";
    }
    
    public static string GetRhsCode(this IEnumerable<RGaFloat64TrilinearCombinationTerm> termList, Func<string, string> arg1NameFunc, Func<string, string> arg2NameFunc, Func<string, string> arg3NameFunc)
    {
        var termCodeList =
            termList
                .Select(term => term.GetRhsCode(arg1NameFunc, arg2NameFunc, arg3NameFunc))
                .ToArray();

        if (termCodeList.Length == 0)
            return "0";

        var term0 = termCodeList[0];

        if (term0[0] == '+')
            termCodeList[0] = term0[2..];

        else if (term0[0] == '-')
            termCodeList[0] = "-" + term0[2..];

        return termCodeList.Concatenate(" ");
    }

}
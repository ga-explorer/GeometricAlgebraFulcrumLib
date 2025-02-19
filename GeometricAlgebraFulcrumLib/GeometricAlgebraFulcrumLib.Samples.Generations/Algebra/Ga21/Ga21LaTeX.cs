using System.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga21;

public static class Ga21LaTeX
{
    private static string Concatenate(this IEnumerable<string> items, string itemSeparator)
    {
        var s = new StringBuilder();
    
        var flag = false;
        foreach (var item in items)
        {
            if (flag)
                s.Append(itemSeparator);
            else
                flag = true;
    
            s.Append(item);
        }
    
        return s.ToString();
    }
    
    public static string ToLaTeX(this Ga21KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga21KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(3);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga21KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(3);
    
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{2,3}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga21KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{1,2,3}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga21Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(8);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{2,3}}");
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{1,2,3}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga21KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga21KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(3);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga21KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(3);
    
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{0,1}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{0,2}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{1,2}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga21KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{0,1,2}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga21Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(8);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{0,1}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{0,2}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{0,1,2}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
}

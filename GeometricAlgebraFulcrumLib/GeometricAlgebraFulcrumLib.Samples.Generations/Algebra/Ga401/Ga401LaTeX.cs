using System.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public static class Ga401LaTeX
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
    
    public static string ToLaTeX(this Ga401KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(5);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(10);
    
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{2,3}}");
        if (mv.Scalar14 != 0d) termList.Add(@$"({mv.Scalar14:G}) \boldsymbol{{e}}_{{1,4}}");
        if (mv.Scalar24 != 0d) termList.Add(@$"({mv.Scalar24:G}) \boldsymbol{{e}}_{{2,4}}");
        if (mv.Scalar34 != 0d) termList.Add(@$"({mv.Scalar34:G}) \boldsymbol{{e}}_{{3,4}}");
        if (mv.Scalar15 != 0d) termList.Add(@$"({mv.Scalar15:G}) \boldsymbol{{e}}_{{1,5}}");
        if (mv.Scalar25 != 0d) termList.Add(@$"({mv.Scalar25:G}) \boldsymbol{{e}}_{{2,5}}");
        if (mv.Scalar35 != 0d) termList.Add(@$"({mv.Scalar35:G}) \boldsymbol{{e}}_{{3,5}}");
        if (mv.Scalar45 != 0d) termList.Add(@$"({mv.Scalar45:G}) \boldsymbol{{e}}_{{4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(10);
    
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (mv.Scalar124 != 0d) termList.Add(@$"({mv.Scalar124:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (mv.Scalar134 != 0d) termList.Add(@$"({mv.Scalar134:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (mv.Scalar234 != 0d) termList.Add(@$"({mv.Scalar234:G}) \boldsymbol{{e}}_{{2,3,4}}");
        if (mv.Scalar125 != 0d) termList.Add(@$"({mv.Scalar125:G}) \boldsymbol{{e}}_{{1,2,5}}");
        if (mv.Scalar135 != 0d) termList.Add(@$"({mv.Scalar135:G}) \boldsymbol{{e}}_{{1,3,5}}");
        if (mv.Scalar235 != 0d) termList.Add(@$"({mv.Scalar235:G}) \boldsymbol{{e}}_{{2,3,5}}");
        if (mv.Scalar145 != 0d) termList.Add(@$"({mv.Scalar145:G}) \boldsymbol{{e}}_{{1,4,5}}");
        if (mv.Scalar245 != 0d) termList.Add(@$"({mv.Scalar245:G}) \boldsymbol{{e}}_{{2,4,5}}");
        if (mv.Scalar345 != 0d) termList.Add(@$"({mv.Scalar345:G}) \boldsymbol{{e}}_{{3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(5);
    
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga401Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(32);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{5}}");
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{2,3}}");
        if (mv.Scalar14 != 0d) termList.Add(@$"({mv.Scalar14:G}) \boldsymbol{{e}}_{{1,4}}");
        if (mv.Scalar24 != 0d) termList.Add(@$"({mv.Scalar24:G}) \boldsymbol{{e}}_{{2,4}}");
        if (mv.Scalar34 != 0d) termList.Add(@$"({mv.Scalar34:G}) \boldsymbol{{e}}_{{3,4}}");
        if (mv.Scalar15 != 0d) termList.Add(@$"({mv.Scalar15:G}) \boldsymbol{{e}}_{{1,5}}");
        if (mv.Scalar25 != 0d) termList.Add(@$"({mv.Scalar25:G}) \boldsymbol{{e}}_{{2,5}}");
        if (mv.Scalar35 != 0d) termList.Add(@$"({mv.Scalar35:G}) \boldsymbol{{e}}_{{3,5}}");
        if (mv.Scalar45 != 0d) termList.Add(@$"({mv.Scalar45:G}) \boldsymbol{{e}}_{{4,5}}");
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (mv.Scalar124 != 0d) termList.Add(@$"({mv.Scalar124:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (mv.Scalar134 != 0d) termList.Add(@$"({mv.Scalar134:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (mv.Scalar234 != 0d) termList.Add(@$"({mv.Scalar234:G}) \boldsymbol{{e}}_{{2,3,4}}");
        if (mv.Scalar125 != 0d) termList.Add(@$"({mv.Scalar125:G}) \boldsymbol{{e}}_{{1,2,5}}");
        if (mv.Scalar135 != 0d) termList.Add(@$"({mv.Scalar135:G}) \boldsymbol{{e}}_{{1,3,5}}");
        if (mv.Scalar235 != 0d) termList.Add(@$"({mv.Scalar235:G}) \boldsymbol{{e}}_{{2,3,5}}");
        if (mv.Scalar145 != 0d) termList.Add(@$"({mv.Scalar145:G}) \boldsymbol{{e}}_{{1,4,5}}");
        if (mv.Scalar245 != 0d) termList.Add(@$"({mv.Scalar245:G}) \boldsymbol{{e}}_{{2,4,5}}");
        if (mv.Scalar345 != 0d) termList.Add(@$"({mv.Scalar345:G}) \boldsymbol{{e}}_{{3,4,5}}");
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(5);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(10);
    
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{0,1}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{0,2}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar14 != 0d) termList.Add(@$"({mv.Scalar14:G}) \boldsymbol{{e}}_{{0,3}}");
        if (mv.Scalar24 != 0d) termList.Add(@$"({mv.Scalar24:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar34 != 0d) termList.Add(@$"({mv.Scalar34:G}) \boldsymbol{{e}}_{{2,3}}");
        if (mv.Scalar15 != 0d) termList.Add(@$"({mv.Scalar15:G}) \boldsymbol{{e}}_{{0,4}}");
        if (mv.Scalar25 != 0d) termList.Add(@$"({mv.Scalar25:G}) \boldsymbol{{e}}_{{1,4}}");
        if (mv.Scalar35 != 0d) termList.Add(@$"({mv.Scalar35:G}) \boldsymbol{{e}}_{{2,4}}");
        if (mv.Scalar45 != 0d) termList.Add(@$"({mv.Scalar45:G}) \boldsymbol{{e}}_{{3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(10);
    
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{0,1,2}}");
        if (mv.Scalar124 != 0d) termList.Add(@$"({mv.Scalar124:G}) \boldsymbol{{e}}_{{0,1,3}}");
        if (mv.Scalar134 != 0d) termList.Add(@$"({mv.Scalar134:G}) \boldsymbol{{e}}_{{0,2,3}}");
        if (mv.Scalar234 != 0d) termList.Add(@$"({mv.Scalar234:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (mv.Scalar125 != 0d) termList.Add(@$"({mv.Scalar125:G}) \boldsymbol{{e}}_{{0,1,4}}");
        if (mv.Scalar135 != 0d) termList.Add(@$"({mv.Scalar135:G}) \boldsymbol{{e}}_{{0,2,4}}");
        if (mv.Scalar235 != 0d) termList.Add(@$"({mv.Scalar235:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (mv.Scalar145 != 0d) termList.Add(@$"({mv.Scalar145:G}) \boldsymbol{{e}}_{{0,3,4}}");
        if (mv.Scalar245 != 0d) termList.Add(@$"({mv.Scalar245:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (mv.Scalar345 != 0d) termList.Add(@$"({mv.Scalar345:G}) \boldsymbol{{e}}_{{2,3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(5);
    
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{0,1,2,3}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{0,1,2,4}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{0,1,3,4}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{0,2,3,4}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{0,1,2,3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga401Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(32);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar12 != 0d) termList.Add(@$"({mv.Scalar12:G}) \boldsymbol{{e}}_{{0,1}}");
        if (mv.Scalar13 != 0d) termList.Add(@$"({mv.Scalar13:G}) \boldsymbol{{e}}_{{0,2}}");
        if (mv.Scalar23 != 0d) termList.Add(@$"({mv.Scalar23:G}) \boldsymbol{{e}}_{{1,2}}");
        if (mv.Scalar14 != 0d) termList.Add(@$"({mv.Scalar14:G}) \boldsymbol{{e}}_{{0,3}}");
        if (mv.Scalar24 != 0d) termList.Add(@$"({mv.Scalar24:G}) \boldsymbol{{e}}_{{1,3}}");
        if (mv.Scalar34 != 0d) termList.Add(@$"({mv.Scalar34:G}) \boldsymbol{{e}}_{{2,3}}");
        if (mv.Scalar15 != 0d) termList.Add(@$"({mv.Scalar15:G}) \boldsymbol{{e}}_{{0,4}}");
        if (mv.Scalar25 != 0d) termList.Add(@$"({mv.Scalar25:G}) \boldsymbol{{e}}_{{1,4}}");
        if (mv.Scalar35 != 0d) termList.Add(@$"({mv.Scalar35:G}) \boldsymbol{{e}}_{{2,4}}");
        if (mv.Scalar45 != 0d) termList.Add(@$"({mv.Scalar45:G}) \boldsymbol{{e}}_{{3,4}}");
        if (mv.Scalar123 != 0d) termList.Add(@$"({mv.Scalar123:G}) \boldsymbol{{e}}_{{0,1,2}}");
        if (mv.Scalar124 != 0d) termList.Add(@$"({mv.Scalar124:G}) \boldsymbol{{e}}_{{0,1,3}}");
        if (mv.Scalar134 != 0d) termList.Add(@$"({mv.Scalar134:G}) \boldsymbol{{e}}_{{0,2,3}}");
        if (mv.Scalar234 != 0d) termList.Add(@$"({mv.Scalar234:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (mv.Scalar125 != 0d) termList.Add(@$"({mv.Scalar125:G}) \boldsymbol{{e}}_{{0,1,4}}");
        if (mv.Scalar135 != 0d) termList.Add(@$"({mv.Scalar135:G}) \boldsymbol{{e}}_{{0,2,4}}");
        if (mv.Scalar235 != 0d) termList.Add(@$"({mv.Scalar235:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (mv.Scalar145 != 0d) termList.Add(@$"({mv.Scalar145:G}) \boldsymbol{{e}}_{{0,3,4}}");
        if (mv.Scalar245 != 0d) termList.Add(@$"({mv.Scalar245:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (mv.Scalar345 != 0d) termList.Add(@$"({mv.Scalar345:G}) \boldsymbol{{e}}_{{2,3,4}}");
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{0,1,2,3}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{0,1,2,4}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{0,1,3,4}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{0,2,3,4}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{0,1,2,3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
}

using System.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;

public static class Ga51LaTeX
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
    
    public static string ToLaTeX(this Ga51KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(6);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{5}}");
        if (mv.Scalar6 != 0d) termList.Add(@$"({mv.Scalar6:G}) \boldsymbol{{e}}_{{6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(15);
    
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
        if (mv.Scalar16 != 0d) termList.Add(@$"({mv.Scalar16:G}) \boldsymbol{{e}}_{{1,6}}");
        if (mv.Scalar26 != 0d) termList.Add(@$"({mv.Scalar26:G}) \boldsymbol{{e}}_{{2,6}}");
        if (mv.Scalar36 != 0d) termList.Add(@$"({mv.Scalar36:G}) \boldsymbol{{e}}_{{3,6}}");
        if (mv.Scalar46 != 0d) termList.Add(@$"({mv.Scalar46:G}) \boldsymbol{{e}}_{{4,6}}");
        if (mv.Scalar56 != 0d) termList.Add(@$"({mv.Scalar56:G}) \boldsymbol{{e}}_{{5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(20);
    
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
        if (mv.Scalar126 != 0d) termList.Add(@$"({mv.Scalar126:G}) \boldsymbol{{e}}_{{1,2,6}}");
        if (mv.Scalar136 != 0d) termList.Add(@$"({mv.Scalar136:G}) \boldsymbol{{e}}_{{1,3,6}}");
        if (mv.Scalar236 != 0d) termList.Add(@$"({mv.Scalar236:G}) \boldsymbol{{e}}_{{2,3,6}}");
        if (mv.Scalar146 != 0d) termList.Add(@$"({mv.Scalar146:G}) \boldsymbol{{e}}_{{1,4,6}}");
        if (mv.Scalar246 != 0d) termList.Add(@$"({mv.Scalar246:G}) \boldsymbol{{e}}_{{2,4,6}}");
        if (mv.Scalar346 != 0d) termList.Add(@$"({mv.Scalar346:G}) \boldsymbol{{e}}_{{3,4,6}}");
        if (mv.Scalar156 != 0d) termList.Add(@$"({mv.Scalar156:G}) \boldsymbol{{e}}_{{1,5,6}}");
        if (mv.Scalar256 != 0d) termList.Add(@$"({mv.Scalar256:G}) \boldsymbol{{e}}_{{2,5,6}}");
        if (mv.Scalar356 != 0d) termList.Add(@$"({mv.Scalar356:G}) \boldsymbol{{e}}_{{3,5,6}}");
        if (mv.Scalar456 != 0d) termList.Add(@$"({mv.Scalar456:G}) \boldsymbol{{e}}_{{4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(15);
    
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
        if (mv.Scalar1236 != 0d) termList.Add(@$"({mv.Scalar1236:G}) \boldsymbol{{e}}_{{1,2,3,6}}");
        if (mv.Scalar1246 != 0d) termList.Add(@$"({mv.Scalar1246:G}) \boldsymbol{{e}}_{{1,2,4,6}}");
        if (mv.Scalar1346 != 0d) termList.Add(@$"({mv.Scalar1346:G}) \boldsymbol{{e}}_{{1,3,4,6}}");
        if (mv.Scalar2346 != 0d) termList.Add(@$"({mv.Scalar2346:G}) \boldsymbol{{e}}_{{2,3,4,6}}");
        if (mv.Scalar1256 != 0d) termList.Add(@$"({mv.Scalar1256:G}) \boldsymbol{{e}}_{{1,2,5,6}}");
        if (mv.Scalar1356 != 0d) termList.Add(@$"({mv.Scalar1356:G}) \boldsymbol{{e}}_{{1,3,5,6}}");
        if (mv.Scalar2356 != 0d) termList.Add(@$"({mv.Scalar2356:G}) \boldsymbol{{e}}_{{2,3,5,6}}");
        if (mv.Scalar1456 != 0d) termList.Add(@$"({mv.Scalar1456:G}) \boldsymbol{{e}}_{{1,4,5,6}}");
        if (mv.Scalar2456 != 0d) termList.Add(@$"({mv.Scalar2456:G}) \boldsymbol{{e}}_{{2,4,5,6}}");
        if (mv.Scalar3456 != 0d) termList.Add(@$"({mv.Scalar3456:G}) \boldsymbol{{e}}_{{3,4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(6);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
        if (mv.Scalar12346 != 0d) termList.Add(@$"({mv.Scalar12346:G}) \boldsymbol{{e}}_{{1,2,3,4,6}}");
        if (mv.Scalar12356 != 0d) termList.Add(@$"({mv.Scalar12356:G}) \boldsymbol{{e}}_{{1,2,3,5,6}}");
        if (mv.Scalar12456 != 0d) termList.Add(@$"({mv.Scalar12456:G}) \boldsymbol{{e}}_{{1,2,4,5,6}}");
        if (mv.Scalar13456 != 0d) termList.Add(@$"({mv.Scalar13456:G}) \boldsymbol{{e}}_{{1,3,4,5,6}}");
        if (mv.Scalar23456 != 0d) termList.Add(@$"({mv.Scalar23456:G}) \boldsymbol{{e}}_{{2,3,4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51KVector6 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{1,2,3,4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga51Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(64);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{5}}");
        if (mv.Scalar6 != 0d) termList.Add(@$"({mv.Scalar6:G}) \boldsymbol{{e}}_{{6}}");
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
        if (mv.Scalar16 != 0d) termList.Add(@$"({mv.Scalar16:G}) \boldsymbol{{e}}_{{1,6}}");
        if (mv.Scalar26 != 0d) termList.Add(@$"({mv.Scalar26:G}) \boldsymbol{{e}}_{{2,6}}");
        if (mv.Scalar36 != 0d) termList.Add(@$"({mv.Scalar36:G}) \boldsymbol{{e}}_{{3,6}}");
        if (mv.Scalar46 != 0d) termList.Add(@$"({mv.Scalar46:G}) \boldsymbol{{e}}_{{4,6}}");
        if (mv.Scalar56 != 0d) termList.Add(@$"({mv.Scalar56:G}) \boldsymbol{{e}}_{{5,6}}");
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
        if (mv.Scalar126 != 0d) termList.Add(@$"({mv.Scalar126:G}) \boldsymbol{{e}}_{{1,2,6}}");
        if (mv.Scalar136 != 0d) termList.Add(@$"({mv.Scalar136:G}) \boldsymbol{{e}}_{{1,3,6}}");
        if (mv.Scalar236 != 0d) termList.Add(@$"({mv.Scalar236:G}) \boldsymbol{{e}}_{{2,3,6}}");
        if (mv.Scalar146 != 0d) termList.Add(@$"({mv.Scalar146:G}) \boldsymbol{{e}}_{{1,4,6}}");
        if (mv.Scalar246 != 0d) termList.Add(@$"({mv.Scalar246:G}) \boldsymbol{{e}}_{{2,4,6}}");
        if (mv.Scalar346 != 0d) termList.Add(@$"({mv.Scalar346:G}) \boldsymbol{{e}}_{{3,4,6}}");
        if (mv.Scalar156 != 0d) termList.Add(@$"({mv.Scalar156:G}) \boldsymbol{{e}}_{{1,5,6}}");
        if (mv.Scalar256 != 0d) termList.Add(@$"({mv.Scalar256:G}) \boldsymbol{{e}}_{{2,5,6}}");
        if (mv.Scalar356 != 0d) termList.Add(@$"({mv.Scalar356:G}) \boldsymbol{{e}}_{{3,5,6}}");
        if (mv.Scalar456 != 0d) termList.Add(@$"({mv.Scalar456:G}) \boldsymbol{{e}}_{{4,5,6}}");
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
        if (mv.Scalar1236 != 0d) termList.Add(@$"({mv.Scalar1236:G}) \boldsymbol{{e}}_{{1,2,3,6}}");
        if (mv.Scalar1246 != 0d) termList.Add(@$"({mv.Scalar1246:G}) \boldsymbol{{e}}_{{1,2,4,6}}");
        if (mv.Scalar1346 != 0d) termList.Add(@$"({mv.Scalar1346:G}) \boldsymbol{{e}}_{{1,3,4,6}}");
        if (mv.Scalar2346 != 0d) termList.Add(@$"({mv.Scalar2346:G}) \boldsymbol{{e}}_{{2,3,4,6}}");
        if (mv.Scalar1256 != 0d) termList.Add(@$"({mv.Scalar1256:G}) \boldsymbol{{e}}_{{1,2,5,6}}");
        if (mv.Scalar1356 != 0d) termList.Add(@$"({mv.Scalar1356:G}) \boldsymbol{{e}}_{{1,3,5,6}}");
        if (mv.Scalar2356 != 0d) termList.Add(@$"({mv.Scalar2356:G}) \boldsymbol{{e}}_{{2,3,5,6}}");
        if (mv.Scalar1456 != 0d) termList.Add(@$"({mv.Scalar1456:G}) \boldsymbol{{e}}_{{1,4,5,6}}");
        if (mv.Scalar2456 != 0d) termList.Add(@$"({mv.Scalar2456:G}) \boldsymbol{{e}}_{{2,4,5,6}}");
        if (mv.Scalar3456 != 0d) termList.Add(@$"({mv.Scalar3456:G}) \boldsymbol{{e}}_{{3,4,5,6}}");
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
        if (mv.Scalar12346 != 0d) termList.Add(@$"({mv.Scalar12346:G}) \boldsymbol{{e}}_{{1,2,3,4,6}}");
        if (mv.Scalar12356 != 0d) termList.Add(@$"({mv.Scalar12356:G}) \boldsymbol{{e}}_{{1,2,3,5,6}}");
        if (mv.Scalar12456 != 0d) termList.Add(@$"({mv.Scalar12456:G}) \boldsymbol{{e}}_{{1,2,4,5,6}}");
        if (mv.Scalar13456 != 0d) termList.Add(@$"({mv.Scalar13456:G}) \boldsymbol{{e}}_{{1,3,4,5,6}}");
        if (mv.Scalar23456 != 0d) termList.Add(@$"({mv.Scalar23456:G}) \boldsymbol{{e}}_{{2,3,4,5,6}}");
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{1,2,3,4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(6);
    
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar6 != 0d) termList.Add(@$"({mv.Scalar6:G}) \boldsymbol{{e}}_{{5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(15);
    
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
        if (mv.Scalar16 != 0d) termList.Add(@$"({mv.Scalar16:G}) \boldsymbol{{e}}_{{0,5}}");
        if (mv.Scalar26 != 0d) termList.Add(@$"({mv.Scalar26:G}) \boldsymbol{{e}}_{{1,5}}");
        if (mv.Scalar36 != 0d) termList.Add(@$"({mv.Scalar36:G}) \boldsymbol{{e}}_{{2,5}}");
        if (mv.Scalar46 != 0d) termList.Add(@$"({mv.Scalar46:G}) \boldsymbol{{e}}_{{3,5}}");
        if (mv.Scalar56 != 0d) termList.Add(@$"({mv.Scalar56:G}) \boldsymbol{{e}}_{{4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(20);
    
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
        if (mv.Scalar126 != 0d) termList.Add(@$"({mv.Scalar126:G}) \boldsymbol{{e}}_{{0,1,5}}");
        if (mv.Scalar136 != 0d) termList.Add(@$"({mv.Scalar136:G}) \boldsymbol{{e}}_{{0,2,5}}");
        if (mv.Scalar236 != 0d) termList.Add(@$"({mv.Scalar236:G}) \boldsymbol{{e}}_{{1,2,5}}");
        if (mv.Scalar146 != 0d) termList.Add(@$"({mv.Scalar146:G}) \boldsymbol{{e}}_{{0,3,5}}");
        if (mv.Scalar246 != 0d) termList.Add(@$"({mv.Scalar246:G}) \boldsymbol{{e}}_{{1,3,5}}");
        if (mv.Scalar346 != 0d) termList.Add(@$"({mv.Scalar346:G}) \boldsymbol{{e}}_{{2,3,5}}");
        if (mv.Scalar156 != 0d) termList.Add(@$"({mv.Scalar156:G}) \boldsymbol{{e}}_{{0,4,5}}");
        if (mv.Scalar256 != 0d) termList.Add(@$"({mv.Scalar256:G}) \boldsymbol{{e}}_{{1,4,5}}");
        if (mv.Scalar356 != 0d) termList.Add(@$"({mv.Scalar356:G}) \boldsymbol{{e}}_{{2,4,5}}");
        if (mv.Scalar456 != 0d) termList.Add(@$"({mv.Scalar456:G}) \boldsymbol{{e}}_{{3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(15);
    
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{0,1,2,3}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{0,1,2,4}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{0,1,3,4}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{0,2,3,4}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1236 != 0d) termList.Add(@$"({mv.Scalar1236:G}) \boldsymbol{{e}}_{{0,1,2,5}}");
        if (mv.Scalar1246 != 0d) termList.Add(@$"({mv.Scalar1246:G}) \boldsymbol{{e}}_{{0,1,3,5}}");
        if (mv.Scalar1346 != 0d) termList.Add(@$"({mv.Scalar1346:G}) \boldsymbol{{e}}_{{0,2,3,5}}");
        if (mv.Scalar2346 != 0d) termList.Add(@$"({mv.Scalar2346:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1256 != 0d) termList.Add(@$"({mv.Scalar1256:G}) \boldsymbol{{e}}_{{0,1,4,5}}");
        if (mv.Scalar1356 != 0d) termList.Add(@$"({mv.Scalar1356:G}) \boldsymbol{{e}}_{{0,2,4,5}}");
        if (mv.Scalar2356 != 0d) termList.Add(@$"({mv.Scalar2356:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1456 != 0d) termList.Add(@$"({mv.Scalar1456:G}) \boldsymbol{{e}}_{{0,3,4,5}}");
        if (mv.Scalar2456 != 0d) termList.Add(@$"({mv.Scalar2456:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar3456 != 0d) termList.Add(@$"({mv.Scalar3456:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(6);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{0,1,2,3,4}}");
        if (mv.Scalar12346 != 0d) termList.Add(@$"({mv.Scalar12346:G}) \boldsymbol{{e}}_{{0,1,2,3,5}}");
        if (mv.Scalar12356 != 0d) termList.Add(@$"({mv.Scalar12356:G}) \boldsymbol{{e}}_{{0,1,2,4,5}}");
        if (mv.Scalar12456 != 0d) termList.Add(@$"({mv.Scalar12456:G}) \boldsymbol{{e}}_{{0,1,3,4,5}}");
        if (mv.Scalar13456 != 0d) termList.Add(@$"({mv.Scalar13456:G}) \boldsymbol{{e}}_{{0,2,3,4,5}}");
        if (mv.Scalar23456 != 0d) termList.Add(@$"({mv.Scalar23456:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51KVector6 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{0,1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga51Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(64);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
        if (mv.Scalar1 != 0d) termList.Add(@$"({mv.Scalar1:G}) \boldsymbol{{e}}_{{0}}");
        if (mv.Scalar2 != 0d) termList.Add(@$"({mv.Scalar2:G}) \boldsymbol{{e}}_{{1}}");
        if (mv.Scalar3 != 0d) termList.Add(@$"({mv.Scalar3:G}) \boldsymbol{{e}}_{{2}}");
        if (mv.Scalar4 != 0d) termList.Add(@$"({mv.Scalar4:G}) \boldsymbol{{e}}_{{3}}");
        if (mv.Scalar5 != 0d) termList.Add(@$"({mv.Scalar5:G}) \boldsymbol{{e}}_{{4}}");
        if (mv.Scalar6 != 0d) termList.Add(@$"({mv.Scalar6:G}) \boldsymbol{{e}}_{{5}}");
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
        if (mv.Scalar16 != 0d) termList.Add(@$"({mv.Scalar16:G}) \boldsymbol{{e}}_{{0,5}}");
        if (mv.Scalar26 != 0d) termList.Add(@$"({mv.Scalar26:G}) \boldsymbol{{e}}_{{1,5}}");
        if (mv.Scalar36 != 0d) termList.Add(@$"({mv.Scalar36:G}) \boldsymbol{{e}}_{{2,5}}");
        if (mv.Scalar46 != 0d) termList.Add(@$"({mv.Scalar46:G}) \boldsymbol{{e}}_{{3,5}}");
        if (mv.Scalar56 != 0d) termList.Add(@$"({mv.Scalar56:G}) \boldsymbol{{e}}_{{4,5}}");
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
        if (mv.Scalar126 != 0d) termList.Add(@$"({mv.Scalar126:G}) \boldsymbol{{e}}_{{0,1,5}}");
        if (mv.Scalar136 != 0d) termList.Add(@$"({mv.Scalar136:G}) \boldsymbol{{e}}_{{0,2,5}}");
        if (mv.Scalar236 != 0d) termList.Add(@$"({mv.Scalar236:G}) \boldsymbol{{e}}_{{1,2,5}}");
        if (mv.Scalar146 != 0d) termList.Add(@$"({mv.Scalar146:G}) \boldsymbol{{e}}_{{0,3,5}}");
        if (mv.Scalar246 != 0d) termList.Add(@$"({mv.Scalar246:G}) \boldsymbol{{e}}_{{1,3,5}}");
        if (mv.Scalar346 != 0d) termList.Add(@$"({mv.Scalar346:G}) \boldsymbol{{e}}_{{2,3,5}}");
        if (mv.Scalar156 != 0d) termList.Add(@$"({mv.Scalar156:G}) \boldsymbol{{e}}_{{0,4,5}}");
        if (mv.Scalar256 != 0d) termList.Add(@$"({mv.Scalar256:G}) \boldsymbol{{e}}_{{1,4,5}}");
        if (mv.Scalar356 != 0d) termList.Add(@$"({mv.Scalar356:G}) \boldsymbol{{e}}_{{2,4,5}}");
        if (mv.Scalar456 != 0d) termList.Add(@$"({mv.Scalar456:G}) \boldsymbol{{e}}_{{3,4,5}}");
        if (mv.Scalar1234 != 0d) termList.Add(@$"({mv.Scalar1234:G}) \boldsymbol{{e}}_{{0,1,2,3}}");
        if (mv.Scalar1235 != 0d) termList.Add(@$"({mv.Scalar1235:G}) \boldsymbol{{e}}_{{0,1,2,4}}");
        if (mv.Scalar1245 != 0d) termList.Add(@$"({mv.Scalar1245:G}) \boldsymbol{{e}}_{{0,1,3,4}}");
        if (mv.Scalar1345 != 0d) termList.Add(@$"({mv.Scalar1345:G}) \boldsymbol{{e}}_{{0,2,3,4}}");
        if (mv.Scalar2345 != 0d) termList.Add(@$"({mv.Scalar2345:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (mv.Scalar1236 != 0d) termList.Add(@$"({mv.Scalar1236:G}) \boldsymbol{{e}}_{{0,1,2,5}}");
        if (mv.Scalar1246 != 0d) termList.Add(@$"({mv.Scalar1246:G}) \boldsymbol{{e}}_{{0,1,3,5}}");
        if (mv.Scalar1346 != 0d) termList.Add(@$"({mv.Scalar1346:G}) \boldsymbol{{e}}_{{0,2,3,5}}");
        if (mv.Scalar2346 != 0d) termList.Add(@$"({mv.Scalar2346:G}) \boldsymbol{{e}}_{{1,2,3,5}}");
        if (mv.Scalar1256 != 0d) termList.Add(@$"({mv.Scalar1256:G}) \boldsymbol{{e}}_{{0,1,4,5}}");
        if (mv.Scalar1356 != 0d) termList.Add(@$"({mv.Scalar1356:G}) \boldsymbol{{e}}_{{0,2,4,5}}");
        if (mv.Scalar2356 != 0d) termList.Add(@$"({mv.Scalar2356:G}) \boldsymbol{{e}}_{{1,2,4,5}}");
        if (mv.Scalar1456 != 0d) termList.Add(@$"({mv.Scalar1456:G}) \boldsymbol{{e}}_{{0,3,4,5}}");
        if (mv.Scalar2456 != 0d) termList.Add(@$"({mv.Scalar2456:G}) \boldsymbol{{e}}_{{1,3,4,5}}");
        if (mv.Scalar3456 != 0d) termList.Add(@$"({mv.Scalar3456:G}) \boldsymbol{{e}}_{{2,3,4,5}}");
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{0,1,2,3,4}}");
        if (mv.Scalar12346 != 0d) termList.Add(@$"({mv.Scalar12346:G}) \boldsymbol{{e}}_{{0,1,2,3,5}}");
        if (mv.Scalar12356 != 0d) termList.Add(@$"({mv.Scalar12356:G}) \boldsymbol{{e}}_{{0,1,2,4,5}}");
        if (mv.Scalar12456 != 0d) termList.Add(@$"({mv.Scalar12456:G}) \boldsymbol{{e}}_{{0,1,3,4,5}}");
        if (mv.Scalar13456 != 0d) termList.Add(@$"({mv.Scalar13456:G}) \boldsymbol{{e}}_{{0,2,3,4,5}}");
        if (mv.Scalar23456 != 0d) termList.Add(@$"({mv.Scalar23456:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{0,1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector0 mv)
    {
        var scalarArray = new double[1];
    
        scalarArray[0] = mv.Scalar;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(1);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector1 mv)
    {
        var scalarArray = new double[6];
    
        scalarArray[0] = mv.Scalar3;
        scalarArray[1] = mv.Scalar4;
        scalarArray[2] = mv.Scalar5;
        scalarArray[3] = mv.Scalar6;
        scalarArray[4] = mv.Scalar1 + mv.Scalar2;
        scalarArray[5] = 0.5 * mv.Scalar1 - 0.5 * mv.Scalar2;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(6);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{2}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{3}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{4}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{o}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector2 mv)
    {
        var scalarArray = new double[15];
    
        scalarArray[0] = mv.Scalar34;
        scalarArray[1] = mv.Scalar35;
        scalarArray[2] = mv.Scalar45;
        scalarArray[3] = mv.Scalar36;
        scalarArray[4] = mv.Scalar46;
        scalarArray[5] = mv.Scalar56;
        scalarArray[6] = -mv.Scalar13 - mv.Scalar23;
        scalarArray[7] = -mv.Scalar14 - mv.Scalar24;
        scalarArray[8] = -mv.Scalar15 - mv.Scalar25;
        scalarArray[9] = -mv.Scalar16 - mv.Scalar26;
        scalarArray[10] = -0.5 * mv.Scalar13 + 0.5 * mv.Scalar23;
        scalarArray[11] = -0.5 * mv.Scalar14 + 0.5 * mv.Scalar24;
        scalarArray[12] = -0.5 * mv.Scalar15 + 0.5 * mv.Scalar25;
        scalarArray[13] = -0.5 * mv.Scalar16 + 0.5 * mv.Scalar26;
        scalarArray[14] = -mv.Scalar12;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(15);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,3}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{2,3}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,4}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{2,4}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{3,4}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{1,o}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{2,o}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{3,o}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{4,o}}");
        if (scalarArray[10] != 0d) termList.Add(@$"({scalarArray[10]:G}) \boldsymbol{{e}}_{{1,\infty}}");
        if (scalarArray[11] != 0d) termList.Add(@$"({scalarArray[11]:G}) \boldsymbol{{e}}_{{2,\infty}}");
        if (scalarArray[12] != 0d) termList.Add(@$"({scalarArray[12]:G}) \boldsymbol{{e}}_{{3,\infty}}");
        if (scalarArray[13] != 0d) termList.Add(@$"({scalarArray[13]:G}) \boldsymbol{{e}}_{{4,\infty}}");
        if (scalarArray[14] != 0d) termList.Add(@$"({scalarArray[14]:G}) \boldsymbol{{e}}_{{o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector3 mv)
    {
        var scalarArray = new double[20];
    
        scalarArray[0] = mv.Scalar345;
        scalarArray[1] = mv.Scalar346;
        scalarArray[2] = mv.Scalar356;
        scalarArray[3] = mv.Scalar456;
        scalarArray[4] = mv.Scalar134 + mv.Scalar234;
        scalarArray[5] = mv.Scalar135 + mv.Scalar235;
        scalarArray[6] = mv.Scalar145 + mv.Scalar245;
        scalarArray[7] = mv.Scalar136 + mv.Scalar236;
        scalarArray[8] = mv.Scalar146 + mv.Scalar246;
        scalarArray[9] = mv.Scalar156 + mv.Scalar256;
        scalarArray[10] = 0.5 * mv.Scalar134 - 0.5 * mv.Scalar234;
        scalarArray[11] = 0.5 * mv.Scalar135 - 0.5 * mv.Scalar235;
        scalarArray[12] = 0.5 * mv.Scalar145 - 0.5 * mv.Scalar245;
        scalarArray[13] = 0.5 * mv.Scalar136 - 0.5 * mv.Scalar236;
        scalarArray[14] = 0.5 * mv.Scalar146 - 0.5 * mv.Scalar246;
        scalarArray[15] = 0.5 * mv.Scalar156 - 0.5 * mv.Scalar256;
        scalarArray[16] = -mv.Scalar123;
        scalarArray[17] = -mv.Scalar124;
        scalarArray[18] = -mv.Scalar125;
        scalarArray[19] = -mv.Scalar126;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(20);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{2,3,4}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{1,2,o}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{1,3,o}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{2,3,o}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{1,4,o}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{2,4,o}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{3,4,o}}");
        if (scalarArray[10] != 0d) termList.Add(@$"({scalarArray[10]:G}) \boldsymbol{{e}}_{{1,2,\infty}}");
        if (scalarArray[11] != 0d) termList.Add(@$"({scalarArray[11]:G}) \boldsymbol{{e}}_{{1,3,\infty}}");
        if (scalarArray[12] != 0d) termList.Add(@$"({scalarArray[12]:G}) \boldsymbol{{e}}_{{2,3,\infty}}");
        if (scalarArray[13] != 0d) termList.Add(@$"({scalarArray[13]:G}) \boldsymbol{{e}}_{{1,4,\infty}}");
        if (scalarArray[14] != 0d) termList.Add(@$"({scalarArray[14]:G}) \boldsymbol{{e}}_{{2,4,\infty}}");
        if (scalarArray[15] != 0d) termList.Add(@$"({scalarArray[15]:G}) \boldsymbol{{e}}_{{3,4,\infty}}");
        if (scalarArray[16] != 0d) termList.Add(@$"({scalarArray[16]:G}) \boldsymbol{{e}}_{{1,o,\infty}}");
        if (scalarArray[17] != 0d) termList.Add(@$"({scalarArray[17]:G}) \boldsymbol{{e}}_{{2,o,\infty}}");
        if (scalarArray[18] != 0d) termList.Add(@$"({scalarArray[18]:G}) \boldsymbol{{e}}_{{3,o,\infty}}");
        if (scalarArray[19] != 0d) termList.Add(@$"({scalarArray[19]:G}) \boldsymbol{{e}}_{{4,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector4 mv)
    {
        var scalarArray = new double[15];
    
        scalarArray[0] = mv.Scalar3456;
        scalarArray[1] = -mv.Scalar1345 - mv.Scalar2345;
        scalarArray[2] = -mv.Scalar1346 - mv.Scalar2346;
        scalarArray[3] = -mv.Scalar1356 - mv.Scalar2356;
        scalarArray[4] = -mv.Scalar1456 - mv.Scalar2456;
        scalarArray[5] = -0.5 * mv.Scalar1345 + 0.5 * mv.Scalar2345;
        scalarArray[6] = -0.5 * mv.Scalar1346 + 0.5 * mv.Scalar2346;
        scalarArray[7] = -0.5 * mv.Scalar1356 + 0.5 * mv.Scalar2356;
        scalarArray[8] = -0.5 * mv.Scalar1456 + 0.5 * mv.Scalar2456;
        scalarArray[9] = -mv.Scalar1234;
        scalarArray[10] = -mv.Scalar1235;
        scalarArray[11] = -mv.Scalar1245;
        scalarArray[12] = -mv.Scalar1236;
        scalarArray[13] = -mv.Scalar1246;
        scalarArray[14] = -mv.Scalar1256;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(15);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,2,3,o}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{1,2,4,o}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,3,4,o}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{2,3,4,o}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{1,2,3,\infty}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{1,2,4,\infty}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{1,3,4,\infty}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{2,3,4,\infty}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{1,2,o,\infty}}");
        if (scalarArray[10] != 0d) termList.Add(@$"({scalarArray[10]:G}) \boldsymbol{{e}}_{{1,3,o,\infty}}");
        if (scalarArray[11] != 0d) termList.Add(@$"({scalarArray[11]:G}) \boldsymbol{{e}}_{{2,3,o,\infty}}");
        if (scalarArray[12] != 0d) termList.Add(@$"({scalarArray[12]:G}) \boldsymbol{{e}}_{{1,4,o,\infty}}");
        if (scalarArray[13] != 0d) termList.Add(@$"({scalarArray[13]:G}) \boldsymbol{{e}}_{{2,4,o,\infty}}");
        if (scalarArray[14] != 0d) termList.Add(@$"({scalarArray[14]:G}) \boldsymbol{{e}}_{{3,4,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector5 mv)
    {
        var scalarArray = new double[6];
    
        scalarArray[0] = mv.Scalar13456 + mv.Scalar23456;
        scalarArray[1] = 0.5 * mv.Scalar13456 - 0.5 * mv.Scalar23456;
        scalarArray[2] = -mv.Scalar12345;
        scalarArray[3] = -mv.Scalar12346;
        scalarArray[4] = -mv.Scalar12356;
        scalarArray[5] = -mv.Scalar12456;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(6);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3,4,o}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,2,3,4,\infty}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{1,2,3,o,\infty}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,2,4,o,\infty}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{1,3,4,o,\infty}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{2,3,4,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51KVector6 mv)
    {
        var scalarArray = new double[1];
    
        scalarArray[0] = -mv.Scalar123456;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51KVector6 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(1);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3,4,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga51Multivector mv)
    {
        var scalarArray = new double[64];
    
        scalarArray[0] = mv.Scalar;
        scalarArray[1] = mv.Scalar3;
        scalarArray[2] = mv.Scalar4;
        scalarArray[3] = mv.Scalar34;
        scalarArray[4] = mv.Scalar5;
        scalarArray[5] = mv.Scalar35;
        scalarArray[6] = mv.Scalar45;
        scalarArray[7] = mv.Scalar345;
        scalarArray[8] = mv.Scalar6;
        scalarArray[9] = mv.Scalar36;
        scalarArray[10] = mv.Scalar46;
        scalarArray[11] = mv.Scalar346;
        scalarArray[12] = mv.Scalar56;
        scalarArray[13] = mv.Scalar356;
        scalarArray[14] = mv.Scalar456;
        scalarArray[15] = mv.Scalar3456;
        scalarArray[16] = mv.Scalar1 + mv.Scalar2;
        scalarArray[17] = -mv.Scalar13 - mv.Scalar23;
        scalarArray[18] = -mv.Scalar14 - mv.Scalar24;
        scalarArray[19] = mv.Scalar134 + mv.Scalar234;
        scalarArray[20] = -mv.Scalar15 - mv.Scalar25;
        scalarArray[21] = mv.Scalar135 + mv.Scalar235;
        scalarArray[22] = mv.Scalar145 + mv.Scalar245;
        scalarArray[23] = -mv.Scalar1345 - mv.Scalar2345;
        scalarArray[24] = -mv.Scalar16 - mv.Scalar26;
        scalarArray[25] = mv.Scalar136 + mv.Scalar236;
        scalarArray[26] = mv.Scalar146 + mv.Scalar246;
        scalarArray[27] = -mv.Scalar1346 - mv.Scalar2346;
        scalarArray[28] = mv.Scalar156 + mv.Scalar256;
        scalarArray[29] = -mv.Scalar1356 - mv.Scalar2356;
        scalarArray[30] = -mv.Scalar1456 - mv.Scalar2456;
        scalarArray[31] = mv.Scalar13456 + mv.Scalar23456;
        scalarArray[32] = 0.5 * mv.Scalar1 - 0.5 * mv.Scalar2;
        scalarArray[33] = -0.5 * mv.Scalar13 + 0.5 * mv.Scalar23;
        scalarArray[34] = -0.5 * mv.Scalar14 + 0.5 * mv.Scalar24;
        scalarArray[35] = 0.5 * mv.Scalar134 - 0.5 * mv.Scalar234;
        scalarArray[36] = -0.5 * mv.Scalar15 + 0.5 * mv.Scalar25;
        scalarArray[37] = 0.5 * mv.Scalar135 - 0.5 * mv.Scalar235;
        scalarArray[38] = 0.5 * mv.Scalar145 - 0.5 * mv.Scalar245;
        scalarArray[39] = -0.5 * mv.Scalar1345 + 0.5 * mv.Scalar2345;
        scalarArray[40] = -0.5 * mv.Scalar16 + 0.5 * mv.Scalar26;
        scalarArray[41] = 0.5 * mv.Scalar136 - 0.5 * mv.Scalar236;
        scalarArray[42] = 0.5 * mv.Scalar146 - 0.5 * mv.Scalar246;
        scalarArray[43] = -0.5 * mv.Scalar1346 + 0.5 * mv.Scalar2346;
        scalarArray[44] = 0.5 * mv.Scalar156 - 0.5 * mv.Scalar256;
        scalarArray[45] = -0.5 * mv.Scalar1356 + 0.5 * mv.Scalar2356;
        scalarArray[46] = -0.5 * mv.Scalar1456 + 0.5 * mv.Scalar2456;
        scalarArray[47] = 0.5 * mv.Scalar13456 - 0.5 * mv.Scalar23456;
        scalarArray[48] = -mv.Scalar12;
        scalarArray[49] = -mv.Scalar123;
        scalarArray[50] = -mv.Scalar124;
        scalarArray[51] = -mv.Scalar1234;
        scalarArray[52] = -mv.Scalar125;
        scalarArray[53] = -mv.Scalar1235;
        scalarArray[54] = -mv.Scalar1245;
        scalarArray[55] = -mv.Scalar12345;
        scalarArray[56] = -mv.Scalar126;
        scalarArray[57] = -mv.Scalar1236;
        scalarArray[58] = -mv.Scalar1246;
        scalarArray[59] = -mv.Scalar12346;
        scalarArray[60] = -mv.Scalar1256;
        scalarArray[61] = -mv.Scalar12356;
        scalarArray[62] = -mv.Scalar12456;
        scalarArray[63] = -mv.Scalar123456;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga51Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(64);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{2}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{3}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{4}}");
        if (scalarArray[16] != 0d) termList.Add(@$"({scalarArray[16]:G}) \boldsymbol{{e}}_{{o}}");
        if (scalarArray[32] != 0d) termList.Add(@$"({scalarArray[32]:G}) \boldsymbol{{e}}_{{\infty}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,2}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{1,3}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{2,3}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{1,4}}");
        if (scalarArray[10] != 0d) termList.Add(@$"({scalarArray[10]:G}) \boldsymbol{{e}}_{{2,4}}");
        if (scalarArray[12] != 0d) termList.Add(@$"({scalarArray[12]:G}) \boldsymbol{{e}}_{{3,4}}");
        if (scalarArray[17] != 0d) termList.Add(@$"({scalarArray[17]:G}) \boldsymbol{{e}}_{{1,o}}");
        if (scalarArray[18] != 0d) termList.Add(@$"({scalarArray[18]:G}) \boldsymbol{{e}}_{{2,o}}");
        if (scalarArray[20] != 0d) termList.Add(@$"({scalarArray[20]:G}) \boldsymbol{{e}}_{{3,o}}");
        if (scalarArray[24] != 0d) termList.Add(@$"({scalarArray[24]:G}) \boldsymbol{{e}}_{{4,o}}");
        if (scalarArray[33] != 0d) termList.Add(@$"({scalarArray[33]:G}) \boldsymbol{{e}}_{{1,\infty}}");
        if (scalarArray[34] != 0d) termList.Add(@$"({scalarArray[34]:G}) \boldsymbol{{e}}_{{2,\infty}}");
        if (scalarArray[36] != 0d) termList.Add(@$"({scalarArray[36]:G}) \boldsymbol{{e}}_{{3,\infty}}");
        if (scalarArray[40] != 0d) termList.Add(@$"({scalarArray[40]:G}) \boldsymbol{{e}}_{{4,\infty}}");
        if (scalarArray[48] != 0d) termList.Add(@$"({scalarArray[48]:G}) \boldsymbol{{e}}_{{o,\infty}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (scalarArray[11] != 0d) termList.Add(@$"({scalarArray[11]:G}) \boldsymbol{{e}}_{{1,2,4}}");
        if (scalarArray[13] != 0d) termList.Add(@$"({scalarArray[13]:G}) \boldsymbol{{e}}_{{1,3,4}}");
        if (scalarArray[14] != 0d) termList.Add(@$"({scalarArray[14]:G}) \boldsymbol{{e}}_{{2,3,4}}");
        if (scalarArray[19] != 0d) termList.Add(@$"({scalarArray[19]:G}) \boldsymbol{{e}}_{{1,2,o}}");
        if (scalarArray[21] != 0d) termList.Add(@$"({scalarArray[21]:G}) \boldsymbol{{e}}_{{1,3,o}}");
        if (scalarArray[22] != 0d) termList.Add(@$"({scalarArray[22]:G}) \boldsymbol{{e}}_{{2,3,o}}");
        if (scalarArray[25] != 0d) termList.Add(@$"({scalarArray[25]:G}) \boldsymbol{{e}}_{{1,4,o}}");
        if (scalarArray[26] != 0d) termList.Add(@$"({scalarArray[26]:G}) \boldsymbol{{e}}_{{2,4,o}}");
        if (scalarArray[28] != 0d) termList.Add(@$"({scalarArray[28]:G}) \boldsymbol{{e}}_{{3,4,o}}");
        if (scalarArray[35] != 0d) termList.Add(@$"({scalarArray[35]:G}) \boldsymbol{{e}}_{{1,2,\infty}}");
        if (scalarArray[37] != 0d) termList.Add(@$"({scalarArray[37]:G}) \boldsymbol{{e}}_{{1,3,\infty}}");
        if (scalarArray[38] != 0d) termList.Add(@$"({scalarArray[38]:G}) \boldsymbol{{e}}_{{2,3,\infty}}");
        if (scalarArray[41] != 0d) termList.Add(@$"({scalarArray[41]:G}) \boldsymbol{{e}}_{{1,4,\infty}}");
        if (scalarArray[42] != 0d) termList.Add(@$"({scalarArray[42]:G}) \boldsymbol{{e}}_{{2,4,\infty}}");
        if (scalarArray[44] != 0d) termList.Add(@$"({scalarArray[44]:G}) \boldsymbol{{e}}_{{3,4,\infty}}");
        if (scalarArray[49] != 0d) termList.Add(@$"({scalarArray[49]:G}) \boldsymbol{{e}}_{{1,o,\infty}}");
        if (scalarArray[50] != 0d) termList.Add(@$"({scalarArray[50]:G}) \boldsymbol{{e}}_{{2,o,\infty}}");
        if (scalarArray[52] != 0d) termList.Add(@$"({scalarArray[52]:G}) \boldsymbol{{e}}_{{3,o,\infty}}");
        if (scalarArray[56] != 0d) termList.Add(@$"({scalarArray[56]:G}) \boldsymbol{{e}}_{{4,o,\infty}}");
        if (scalarArray[15] != 0d) termList.Add(@$"({scalarArray[15]:G}) \boldsymbol{{e}}_{{1,2,3,4}}");
        if (scalarArray[23] != 0d) termList.Add(@$"({scalarArray[23]:G}) \boldsymbol{{e}}_{{1,2,3,o}}");
        if (scalarArray[27] != 0d) termList.Add(@$"({scalarArray[27]:G}) \boldsymbol{{e}}_{{1,2,4,o}}");
        if (scalarArray[29] != 0d) termList.Add(@$"({scalarArray[29]:G}) \boldsymbol{{e}}_{{1,3,4,o}}");
        if (scalarArray[30] != 0d) termList.Add(@$"({scalarArray[30]:G}) \boldsymbol{{e}}_{{2,3,4,o}}");
        if (scalarArray[39] != 0d) termList.Add(@$"({scalarArray[39]:G}) \boldsymbol{{e}}_{{1,2,3,\infty}}");
        if (scalarArray[43] != 0d) termList.Add(@$"({scalarArray[43]:G}) \boldsymbol{{e}}_{{1,2,4,\infty}}");
        if (scalarArray[45] != 0d) termList.Add(@$"({scalarArray[45]:G}) \boldsymbol{{e}}_{{1,3,4,\infty}}");
        if (scalarArray[46] != 0d) termList.Add(@$"({scalarArray[46]:G}) \boldsymbol{{e}}_{{2,3,4,\infty}}");
        if (scalarArray[51] != 0d) termList.Add(@$"({scalarArray[51]:G}) \boldsymbol{{e}}_{{1,2,o,\infty}}");
        if (scalarArray[53] != 0d) termList.Add(@$"({scalarArray[53]:G}) \boldsymbol{{e}}_{{1,3,o,\infty}}");
        if (scalarArray[54] != 0d) termList.Add(@$"({scalarArray[54]:G}) \boldsymbol{{e}}_{{2,3,o,\infty}}");
        if (scalarArray[57] != 0d) termList.Add(@$"({scalarArray[57]:G}) \boldsymbol{{e}}_{{1,4,o,\infty}}");
        if (scalarArray[58] != 0d) termList.Add(@$"({scalarArray[58]:G}) \boldsymbol{{e}}_{{2,4,o,\infty}}");
        if (scalarArray[60] != 0d) termList.Add(@$"({scalarArray[60]:G}) \boldsymbol{{e}}_{{3,4,o,\infty}}");
        if (scalarArray[31] != 0d) termList.Add(@$"({scalarArray[31]:G}) \boldsymbol{{e}}_{{1,2,3,4,o}}");
        if (scalarArray[47] != 0d) termList.Add(@$"({scalarArray[47]:G}) \boldsymbol{{e}}_{{1,2,3,4,\infty}}");
        if (scalarArray[55] != 0d) termList.Add(@$"({scalarArray[55]:G}) \boldsymbol{{e}}_{{1,2,3,o,\infty}}");
        if (scalarArray[59] != 0d) termList.Add(@$"({scalarArray[59]:G}) \boldsymbol{{e}}_{{1,2,4,o,\infty}}");
        if (scalarArray[61] != 0d) termList.Add(@$"({scalarArray[61]:G}) \boldsymbol{{e}}_{{1,3,4,o,\infty}}");
        if (scalarArray[62] != 0d) termList.Add(@$"({scalarArray[62]:G}) \boldsymbol{{e}}_{{2,3,4,o,\infty}}");
        if (scalarArray[63] != 0d) termList.Add(@$"({scalarArray[63]:G}) \boldsymbol{{e}}_{{1,2,3,4,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
}

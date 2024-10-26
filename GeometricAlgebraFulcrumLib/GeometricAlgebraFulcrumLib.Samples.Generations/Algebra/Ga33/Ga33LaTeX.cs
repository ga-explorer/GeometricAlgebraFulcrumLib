using System.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga33;

public static class Ga33LaTeX
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
    
    public static string ToLaTeX(this Ga33KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga33KVector1 mv)
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
    
    public static string ToLaTeX(this Ga33KVector2 mv)
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
    
    public static string ToLaTeX(this Ga33KVector3 mv)
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
    
    public static string ToLaTeX(this Ga33KVector4 mv)
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
    
    public static string ToLaTeX(this Ga33KVector5 mv)
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
    
    public static string ToLaTeX(this Ga33KVector6 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{1,2,3,4,5,6}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga33Multivector mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga33KVector1 mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector2 mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector3 mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector4 mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector5 mv)
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
    
    public static string ToPGaLaTeX(this Ga33KVector6 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar123456 != 0d) termList.Add(@$"({mv.Scalar123456:G}) \boldsymbol{{e}}_{{0,1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga33Multivector mv)
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
    
}

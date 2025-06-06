using System.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public static class Ga41LaTeX
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
    
    public static string ToLaTeX(this Ga41KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga41KVector1 mv)
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
    
    public static string ToLaTeX(this Ga41KVector2 mv)
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
    
    public static string ToLaTeX(this Ga41KVector3 mv)
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
    
    public static string ToLaTeX(this Ga41KVector4 mv)
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
    
    public static string ToLaTeX(this Ga41KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{1,2,3,4,5}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToLaTeX(this Ga41Multivector mv)
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
    
    public static string ToPGaLaTeX(this Ga41KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar != 0d) termList.Add(@$"({mv.Scalar:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga41KVector1 mv)
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
    
    public static string ToPGaLaTeX(this Ga41KVector2 mv)
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
    
    public static string ToPGaLaTeX(this Ga41KVector3 mv)
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
    
    public static string ToPGaLaTeX(this Ga41KVector4 mv)
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
    
    public static string ToPGaLaTeX(this Ga41KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var termList = new List<string>(1);
    
        if (mv.Scalar12345 != 0d) termList.Add(@$"({mv.Scalar12345:G}) \boldsymbol{{e}}_{{0,1,2,3,4}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    public static string ToPGaLaTeX(this Ga41Multivector mv)
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
    
    private static double[] ToCGaBasis(this Ga41KVector0 mv)
    {
        var scalarArray = new double[1];
    
        scalarArray[0] = mv.Scalar;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector0 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(1);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41KVector1 mv)
    {
        var scalarArray = new double[5];
    
        scalarArray[0] = mv.Scalar3;
        scalarArray[1] = mv.Scalar4;
        scalarArray[2] = mv.Scalar5;
        scalarArray[3] = mv.Scalar1 + mv.Scalar2;
        scalarArray[4] = 0.5 * mv.Scalar1 - 0.5 * mv.Scalar2;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector1 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(5);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{2}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{3}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{o}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41KVector2 mv)
    {
        var scalarArray = new double[10];
    
        scalarArray[0] = mv.Scalar34;
        scalarArray[1] = mv.Scalar35;
        scalarArray[2] = mv.Scalar45;
        scalarArray[3] = -mv.Scalar13 - mv.Scalar23;
        scalarArray[4] = -mv.Scalar14 - mv.Scalar24;
        scalarArray[5] = -mv.Scalar15 - mv.Scalar25;
        scalarArray[6] = -0.5 * mv.Scalar13 + 0.5 * mv.Scalar23;
        scalarArray[7] = -0.5 * mv.Scalar14 + 0.5 * mv.Scalar24;
        scalarArray[8] = -0.5 * mv.Scalar15 + 0.5 * mv.Scalar25;
        scalarArray[9] = -mv.Scalar12;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector2 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(10);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,3}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{2,3}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,o}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{2,o}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{3,o}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{1,\infty}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{2,\infty}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{3,\infty}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41KVector3 mv)
    {
        var scalarArray = new double[10];
    
        scalarArray[0] = mv.Scalar345;
        scalarArray[1] = mv.Scalar134 + mv.Scalar234;
        scalarArray[2] = mv.Scalar135 + mv.Scalar235;
        scalarArray[3] = mv.Scalar145 + mv.Scalar245;
        scalarArray[4] = 0.5 * mv.Scalar134 - 0.5 * mv.Scalar234;
        scalarArray[5] = 0.5 * mv.Scalar135 - 0.5 * mv.Scalar235;
        scalarArray[6] = 0.5 * mv.Scalar145 - 0.5 * mv.Scalar245;
        scalarArray[7] = -mv.Scalar123;
        scalarArray[8] = -mv.Scalar124;
        scalarArray[9] = -mv.Scalar125;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector3 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(10);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,2,o}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{1,3,o}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{2,3,o}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{1,2,\infty}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{1,3,\infty}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{2,3,\infty}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{1,o,\infty}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{2,o,\infty}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{3,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41KVector4 mv)
    {
        var scalarArray = new double[5];
    
        scalarArray[0] = -mv.Scalar1345 - mv.Scalar2345;
        scalarArray[1] = -0.5 * mv.Scalar1345 + 0.5 * mv.Scalar2345;
        scalarArray[2] = -mv.Scalar1234;
        scalarArray[3] = -mv.Scalar1235;
        scalarArray[4] = -mv.Scalar1245;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector4 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(5);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3,o}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1,2,3,\infty}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{1,2,o,\infty}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,3,o,\infty}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{2,3,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41KVector5 mv)
    {
        var scalarArray = new double[1];
    
        scalarArray[0] = -mv.Scalar12345;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41KVector5 mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(1);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{1,2,3,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
    private static double[] ToCGaBasis(this Ga41Multivector mv)
    {
        var scalarArray = new double[32];
    
        scalarArray[0] = mv.Scalar;
        scalarArray[1] = mv.Scalar3;
        scalarArray[2] = mv.Scalar4;
        scalarArray[3] = mv.Scalar34;
        scalarArray[4] = mv.Scalar5;
        scalarArray[5] = mv.Scalar35;
        scalarArray[6] = mv.Scalar45;
        scalarArray[7] = mv.Scalar345;
        scalarArray[8] = mv.Scalar1 + mv.Scalar2;
        scalarArray[9] = -mv.Scalar13 - mv.Scalar23;
        scalarArray[10] = -mv.Scalar14 - mv.Scalar24;
        scalarArray[11] = mv.Scalar134 + mv.Scalar234;
        scalarArray[12] = -mv.Scalar15 - mv.Scalar25;
        scalarArray[13] = mv.Scalar135 + mv.Scalar235;
        scalarArray[14] = mv.Scalar145 + mv.Scalar245;
        scalarArray[15] = -mv.Scalar1345 - mv.Scalar2345;
        scalarArray[16] = 0.5 * mv.Scalar1 - 0.5 * mv.Scalar2;
        scalarArray[17] = -0.5 * mv.Scalar13 + 0.5 * mv.Scalar23;
        scalarArray[18] = -0.5 * mv.Scalar14 + 0.5 * mv.Scalar24;
        scalarArray[19] = 0.5 * mv.Scalar134 - 0.5 * mv.Scalar234;
        scalarArray[20] = -0.5 * mv.Scalar15 + 0.5 * mv.Scalar25;
        scalarArray[21] = 0.5 * mv.Scalar135 - 0.5 * mv.Scalar235;
        scalarArray[22] = 0.5 * mv.Scalar145 - 0.5 * mv.Scalar245;
        scalarArray[23] = -0.5 * mv.Scalar1345 + 0.5 * mv.Scalar2345;
        scalarArray[24] = -mv.Scalar12;
        scalarArray[25] = -mv.Scalar123;
        scalarArray[26] = -mv.Scalar124;
        scalarArray[27] = -mv.Scalar1234;
        scalarArray[28] = -mv.Scalar125;
        scalarArray[29] = -mv.Scalar1235;
        scalarArray[30] = -mv.Scalar1245;
        scalarArray[31] = -mv.Scalar12345;
    
        return scalarArray;
    }
    
    public static string ToCGaLaTeX(this Ga41Multivector mv)
    {
        if (mv.IsZero()) return "0";
        
        var scalarArray = mv.ToCGaBasis();
        var termList = new List<string>(32);
    
        if (scalarArray[0] != 0d) termList.Add(@$"({scalarArray[0]:G}) \boldsymbol{{e}}_{{}}");
        if (scalarArray[1] != 0d) termList.Add(@$"({scalarArray[1]:G}) \boldsymbol{{e}}_{{1}}");
        if (scalarArray[2] != 0d) termList.Add(@$"({scalarArray[2]:G}) \boldsymbol{{e}}_{{2}}");
        if (scalarArray[4] != 0d) termList.Add(@$"({scalarArray[4]:G}) \boldsymbol{{e}}_{{3}}");
        if (scalarArray[8] != 0d) termList.Add(@$"({scalarArray[8]:G}) \boldsymbol{{e}}_{{o}}");
        if (scalarArray[16] != 0d) termList.Add(@$"({scalarArray[16]:G}) \boldsymbol{{e}}_{{\infty}}");
        if (scalarArray[3] != 0d) termList.Add(@$"({scalarArray[3]:G}) \boldsymbol{{e}}_{{1,2}}");
        if (scalarArray[5] != 0d) termList.Add(@$"({scalarArray[5]:G}) \boldsymbol{{e}}_{{1,3}}");
        if (scalarArray[6] != 0d) termList.Add(@$"({scalarArray[6]:G}) \boldsymbol{{e}}_{{2,3}}");
        if (scalarArray[9] != 0d) termList.Add(@$"({scalarArray[9]:G}) \boldsymbol{{e}}_{{1,o}}");
        if (scalarArray[10] != 0d) termList.Add(@$"({scalarArray[10]:G}) \boldsymbol{{e}}_{{2,o}}");
        if (scalarArray[12] != 0d) termList.Add(@$"({scalarArray[12]:G}) \boldsymbol{{e}}_{{3,o}}");
        if (scalarArray[17] != 0d) termList.Add(@$"({scalarArray[17]:G}) \boldsymbol{{e}}_{{1,\infty}}");
        if (scalarArray[18] != 0d) termList.Add(@$"({scalarArray[18]:G}) \boldsymbol{{e}}_{{2,\infty}}");
        if (scalarArray[20] != 0d) termList.Add(@$"({scalarArray[20]:G}) \boldsymbol{{e}}_{{3,\infty}}");
        if (scalarArray[24] != 0d) termList.Add(@$"({scalarArray[24]:G}) \boldsymbol{{e}}_{{o,\infty}}");
        if (scalarArray[7] != 0d) termList.Add(@$"({scalarArray[7]:G}) \boldsymbol{{e}}_{{1,2,3}}");
        if (scalarArray[11] != 0d) termList.Add(@$"({scalarArray[11]:G}) \boldsymbol{{e}}_{{1,2,o}}");
        if (scalarArray[13] != 0d) termList.Add(@$"({scalarArray[13]:G}) \boldsymbol{{e}}_{{1,3,o}}");
        if (scalarArray[14] != 0d) termList.Add(@$"({scalarArray[14]:G}) \boldsymbol{{e}}_{{2,3,o}}");
        if (scalarArray[19] != 0d) termList.Add(@$"({scalarArray[19]:G}) \boldsymbol{{e}}_{{1,2,\infty}}");
        if (scalarArray[21] != 0d) termList.Add(@$"({scalarArray[21]:G}) \boldsymbol{{e}}_{{1,3,\infty}}");
        if (scalarArray[22] != 0d) termList.Add(@$"({scalarArray[22]:G}) \boldsymbol{{e}}_{{2,3,\infty}}");
        if (scalarArray[25] != 0d) termList.Add(@$"({scalarArray[25]:G}) \boldsymbol{{e}}_{{1,o,\infty}}");
        if (scalarArray[26] != 0d) termList.Add(@$"({scalarArray[26]:G}) \boldsymbol{{e}}_{{2,o,\infty}}");
        if (scalarArray[28] != 0d) termList.Add(@$"({scalarArray[28]:G}) \boldsymbol{{e}}_{{3,o,\infty}}");
        if (scalarArray[15] != 0d) termList.Add(@$"({scalarArray[15]:G}) \boldsymbol{{e}}_{{1,2,3,o}}");
        if (scalarArray[23] != 0d) termList.Add(@$"({scalarArray[23]:G}) \boldsymbol{{e}}_{{1,2,3,\infty}}");
        if (scalarArray[27] != 0d) termList.Add(@$"({scalarArray[27]:G}) \boldsymbol{{e}}_{{1,2,o,\infty}}");
        if (scalarArray[29] != 0d) termList.Add(@$"({scalarArray[29]:G}) \boldsymbol{{e}}_{{1,3,o,\infty}}");
        if (scalarArray[30] != 0d) termList.Add(@$"({scalarArray[30]:G}) \boldsymbol{{e}}_{{2,3,o,\infty}}");
        if (scalarArray[31] != 0d) termList.Add(@$"({scalarArray[31]:G}) \boldsymbol{{e}}_{{1,2,3,o,\infty}}");
    
        return termList.Count == 1
            ? termList[0]
            : termList.Concatenate(" + ");
    }
    
}

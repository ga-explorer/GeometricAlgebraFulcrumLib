using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Transforms;

public sealed class SvgTransformTranslate : SvgTransform
{
    public static SvgTransformTranslate Create(double tx, double ty = 0.0d)
    {
        return new SvgTransformTranslate { TxValue = tx, TyValue = ty };
    }


    public double TxValue { get; set; }

    public double TyValue { get; set; }


    public override string ValueText 
        => new StringBuilder()
            .Append("translate(")
            .Append(TxValue)
            .Append(", ")
            .Append(TyValue)
            .Append(")")
            .ToString();


    private SvgTransformTranslate()
    {
    }
}
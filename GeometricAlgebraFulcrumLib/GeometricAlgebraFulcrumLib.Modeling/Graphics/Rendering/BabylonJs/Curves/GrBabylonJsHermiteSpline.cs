using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;

public sealed class GrBabylonJsHermiteSpline :
    GrBabylonJsCurve3Base
{
    protected override string ConstructorName 
        => "BABYLON.Curve3.CreateHermiteSpline";

    public GrBabylonJsVector3Value Point1 { get; set; }

    public GrBabylonJsVector3Value Tangent1 { get; set; }

    public GrBabylonJsVector3Value Point2 { get; set; }

    public GrBabylonJsVector3Value Tangent2 { get; set; }

    public GrBabylonJsVector3Value PointNumber { get; set; }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Point1.GetAttributeValueCode();
        yield return Tangent1.GetAttributeValueCode();
        yield return Point2.GetAttributeValueCode();
        yield return Tangent2.GetAttributeValueCode();
        yield return PointNumber.GetAttributeValueCode();
    }


    public GrBabylonJsHermiteSpline(string constName) 
        : base(constName)
    {
    }
}
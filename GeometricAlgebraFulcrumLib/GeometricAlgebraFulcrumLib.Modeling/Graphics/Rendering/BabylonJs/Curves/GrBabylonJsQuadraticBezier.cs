using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;

public sealed class GrBabylonJsQuadraticBezier :
    GrBabylonJsCurve3Base
{
    protected override string ConstructorName 
        => "BABYLON.Curve3.CreateQuadraticBezier";

    public GrBabylonJsVector3Value Point1 { get; set; }

    public GrBabylonJsVector3Value Point2 { get; set; }

    public GrBabylonJsVector3Value Point3 { get; set; }

    public GrBabylonJsVector3Value PointNumber { get; set; }
    

    public GrBabylonJsQuadraticBezier(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Point1.GetAttributeValueCode();
        yield return Point2.GetAttributeValueCode();
        yield return Point3.GetAttributeValueCode();
        yield return PointNumber.GetAttributeValueCode();
    }
}
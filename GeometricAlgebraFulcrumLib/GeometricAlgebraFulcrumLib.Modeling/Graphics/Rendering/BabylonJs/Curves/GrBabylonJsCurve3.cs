using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;

public sealed class GrBabylonJsCurve3 :
    GrBabylonJsCurve3Base
{
    protected override string ConstructorName 
        => "new BABYLON.Curve3";

    public GrBabylonJsVector3ArrayValue Points { get; set; }
    

    public GrBabylonJsCurve3(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Points.GetAttributeValueCode();
    }
}
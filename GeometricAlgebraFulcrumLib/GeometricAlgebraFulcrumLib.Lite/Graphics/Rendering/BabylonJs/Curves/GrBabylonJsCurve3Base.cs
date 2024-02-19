namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Curves;

public abstract class GrBabylonJsCurve3Base :
    GrBabylonJsObject
{
    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => null;


    protected GrBabylonJsCurve3Base(string constName) 
        : base(constName)
    {
    }
}
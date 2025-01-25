namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class SdlPigmentListPigment : 
    GrPovRayTransformablePigment
{
    public GrPovRayColorListPigmentPattern Pattern { get; set; }

    public IGrPovRayPigment Pigment1 { get; set; }

    public IGrPovRayPigment Pigment2 { get; set; }

    public IGrPovRayPigment Pigment3 { get; set; }


    public SdlPigmentListPigment(string identifier) : 
        base(identifier)
    {
    }

    
    public override bool IsEmptyCodeElement()
    {
        throw new NotImplementedException();
    }

    public override string GetPovRayCode()
    {
        throw new NotImplementedException();
    }

}
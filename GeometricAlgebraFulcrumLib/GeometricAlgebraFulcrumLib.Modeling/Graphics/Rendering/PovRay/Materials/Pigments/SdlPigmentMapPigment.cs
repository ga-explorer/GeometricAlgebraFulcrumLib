using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public sealed class SdlPigmentMapPigment : 
    GrPovRayTransformablePigment
{
    public IGrPovRayPattern Pattern { get; set; }

    public List<SdlPigmentMapItem> PigmentItems { get; private set; }


    public SdlPigmentMapPigment()
        : base(string.Empty)
    {
        PigmentItems = new List<SdlPigmentMapItem>();
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
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public abstract class GrPovRayPigment :
    IGrPovRayPigment
{
    public string BasePigmentName { get; }

    public GrPovRayColorValue? QuickColor { get; set; }


    protected GrPovRayPigment(string basePigmentName)
    {
        BasePigmentName = basePigmentName;
    }


    public virtual bool IsEmptyCodeElement()
    {
        return false;
    }

    public abstract string GetPovRayCode();

    public override string ToString()
    {
        return GetPovRayCode();
    }
}
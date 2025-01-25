namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;

public abstract class GrPovRayDirective :
    IGrPovRayDirective
{
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
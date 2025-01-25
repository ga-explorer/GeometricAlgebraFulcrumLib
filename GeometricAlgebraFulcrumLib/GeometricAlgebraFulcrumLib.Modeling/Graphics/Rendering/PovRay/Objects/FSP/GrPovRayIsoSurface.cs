using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayIsoSurface : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject
{
    public GrPovRayFreeCode Function { get; }

    public new GrPovRayIsoSurfaceProperties Properties { get; private set; }
        = new GrPovRayIsoSurfaceProperties();


    internal GrPovRayIsoSurface(GrPovRayFreeCode function)
    {
        Function = function;
    }

    
    public GrPovRayIsoSurface SetProperties(GrPovRayIsoSurfaceProperties properties)
    {
        Properties = new GrPovRayIsoSurfaceProperties(properties);

        return this;
    }
    
    protected override string GetModifiersCode()
    {
        var composer = new LinearTextComposer();

        composer.AppendAtNewLine(Properties.GetPovRayCode());

        if (Material is not null && !Material.IsEmptyCodeElement())
            composer.AppendAtNewLine(Material.GetPovRayCode());

        if (!Transform.IsNearIdentity()) 
            composer.AppendAtNewLine(Transform.GetPovRayMatrixCode());
        
        return composer.ToString();
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("isosurface {")
            .IncreaseIndentation()
            .AppendAtNewLine(Function.GetPovRayCode())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}
namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public abstract class GrPovRayPolynomialObject : 
    GrPovRayObject, 
    IGrPovRayPolynomialObject
{
    public bool? SturmianRootSolver { get; set; }


    protected override string GetModifiersCode()
    {
        if (SturmianRootSolver is null) 
            return base.GetModifiersCode();

        return base.GetModifiersCode() + 
               Environment.NewLine + 
               "sturm " + (SturmianRootSolver == true ? "on" : "off");
    }
}
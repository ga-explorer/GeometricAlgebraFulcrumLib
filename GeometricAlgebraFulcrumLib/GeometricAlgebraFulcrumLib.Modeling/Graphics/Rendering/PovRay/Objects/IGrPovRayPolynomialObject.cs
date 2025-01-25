namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

public interface IGrPovRayPolynomialObject : 
    IGrPovRayObject
{
    bool? SturmianRootSolver { get; set; }
}


using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay;

public interface IGrPovRayDynamicVisitor : 
    IDynamicTreeVisitor<IGrPovRayCodeElement>
{
}

public interface IGrPovRayDynamicVisitor<out TReturnValue> : 
    IDynamicTreeVisitor<IGrPovRayCodeElement, TReturnValue>
{
}
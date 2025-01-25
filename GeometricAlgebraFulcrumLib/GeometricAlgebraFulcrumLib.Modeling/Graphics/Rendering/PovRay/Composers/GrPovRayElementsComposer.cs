namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public abstract class GrPovRayElementsComposer
{
    public GrPovRayScene SceneObject { get; }


    public GrPovRayElementsComposer(GrPovRayScene sceneObject)
    {
        SceneObject = sceneObject;
    }



}
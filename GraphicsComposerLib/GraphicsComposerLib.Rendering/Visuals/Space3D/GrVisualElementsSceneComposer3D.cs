using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D;

public abstract class GrVisualElementsSceneComposer3D<T>
{
    public abstract T SceneObject { get; }

    
    public void AddMaterials(IEnumerable<IGrVisualElementMaterial3D> materialList)
    {
        foreach (var material in materialList)
            AddMaterial(material);
    }

    public void AddMaterials(params IGrVisualElementMaterial3D[] materialList)
    {
        foreach (var material in materialList)
            AddMaterial(material);
    }

    public abstract void AddMaterial(IGrVisualElementMaterial3D material);


    public void AddElement(IGrVisualElement3D visualElement)
    {
        switch (visualElement)
        {
            case GrVisualLaTeXText3D latexText:
                AddLaTeXText(latexText);
                break;

            case GrVisualImage3D image:
                AddImage(image);
                break;

            case GrVisualPoint3D point:
                AddPoint(point);
                break;

            case GrVisualVector3D lineVector:
                AddVector(lineVector);
                break;

            case GrVisualLineSegment3D lineSegment:
                AddLineSegment(lineSegment);
                break;
                
            case GrVisualLineCurve3D lineCurve:
                AddLineCurve(lineCurve);
                break;

            case GrVisualRectangleSurface3D rectangleSurface:
                AddRectangleSurface(rectangleSurface);
                break;

            case GrVisualCircleCurve3D circleCurve:
                AddCircleCurve(circleCurve);
                break;

            case GrVisualCircleCurveArc3D circleCurveArc:
                AddCircleCurveArc(circleCurveArc);
                break;

            case GrVisualCircleSurface3D circleSurface:
                AddCircleSurface(circleSurface);
                break;

            case GrVisualCircleSurfaceArc3D circleSurfaceArc:
                AddCircleSurfaceArc(circleSurfaceArc);
                break;
                
            case GrVisualRingSurface3D ringSurface:
                AddRingSurface(ringSurface);
                break;

            case GrVisualRightAngle3D rightAngle:
                AddRightAngle(rightAngle);
                break;

            case IGrVisualElementList3D elementList:
                AddElements(elementList);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void AddElements(IEnumerable<IGrVisualElement3D> visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);
    }
    
    public void AddElements(params IGrVisualElement3D[] visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);
    }
    

    public abstract void AddLaTeXText(GrVisualLaTeXText3D visualElement);

    public abstract void AddXzSquareGrid(GrVisualXzSquareGrid3D visualElement);

    public abstract void AddImage(GrVisualImage3D visualElement);

    public abstract void AddPoint(GrVisualPoint3D visualElement);

    public abstract void AddVector(GrVisualVector3D visualElement);

    public abstract void AddLineSegment(GrVisualLineSegment3D visualElement);
    
    public abstract void AddLineCurve(GrVisualLineCurve3D visualElement);

    public abstract void AddCircleCurve(GrVisualCircleCurve3D visualElement);

    public abstract void AddCircleCurveArc(GrVisualCircleCurveArc3D visualElement);

    public abstract void AddRectangleSurface(GrVisualRectangleSurface3D visualElement);

    public abstract void AddCircleSurface(GrVisualCircleSurface3D visualElement);

    public abstract void AddCircleSurfaceArc(GrVisualCircleSurfaceArc3D visualElement);
    
    public abstract void AddRingSurface(GrVisualRingSurface3D visualElement);
    
    public abstract void AddRightAngle(GrVisualRightAngle3D visualElement);
}
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Grids;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D.Images;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space2D;

public abstract class GrVisualElementsSceneComposer2D<T>
{
    public abstract T SceneObject { get; }


    public abstract GrVisualLaTeXText2D AddLaTeXText(GrVisualLaTeXText2D visualElement);

    public abstract GrVisualSquareGrid2D AddSquareGrid(GrVisualSquareGrid2D visualElement);

    public abstract IGrVisualImage2D AddImage(IGrVisualImage2D visualElement);

    public abstract GrVisualPoint2D AddPoint(GrVisualPoint2D visualElement);

    //public abstract GrVisualArrowHead2D AddArrowHead(GrVisualArrowHead2D visualElement);


    //public abstract GrVisualParametricCurve2D AddParametricCurve(GrVisualParametricCurve2D visualElement);


    public abstract GrVisualCurve2D AddCurve(GrVisualCurve2D visualElement);

    public GrVisualLineSegment2D AddLineSegment(GrVisualLineSegment2D visualElement)
    {
        AddCurve(visualElement);

        return visualElement;
    }

    //public GrVisualPointPathCurve2D AddLinePath(GrVisualPointPathCurve2D visualElement)
    //{
    //    AddCurve(visualElement);

    //    return visualElement;
    //}

    //public GrVisualCircleCurve2D AddCircle(GrVisualCircleCurve2D visualElement)
    //{
    //    AddCurve(visualElement);

    //    return visualElement;
    //}

    //public GrVisualCircleArcCurve2D AddCircleArc(GrVisualCircleArcCurve2D visualElement)
    //{
    //    AddCurve(visualElement);

    //    return visualElement;
    //}

    //public abstract GrVisualRightAngle2D AddRightAngle(GrVisualRightAngle2D visualElement);

}
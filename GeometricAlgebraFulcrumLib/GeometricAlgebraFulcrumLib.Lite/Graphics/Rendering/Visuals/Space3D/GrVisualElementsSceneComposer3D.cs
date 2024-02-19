using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using WebComposerLib.Html.Media;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D;

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

    public abstract IGrVisualElementMaterial3D AddOrGetColorMaterial(Color color);

    
    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, params IFloat64Vector3D[] positionList)
    {
        for (var i = 0; i < positionList.Length; i++)
            AddPoint(
                nameFunc(i),
                positionList[i],
                material, 
                thickness
            );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, Color color, double thickness, params IFloat64Vector3D[] positionList)
    {
        for (var i = 0; i < positionList.Length; i++)
            AddPoint(
                nameFunc(i),
                positionList[i],
                color, 
                thickness
            );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, IEnumerable<IFloat64Vector3D> positionList)
    {
        var i = 0;
        foreach (var position in positionList)
        {
            AddPoint(
                nameFunc(i),
                position,
                material,
                thickness
            );

            i++;
        }

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, Color color, double thickness, IEnumerable<IFloat64Vector3D> positionList)
    {
        var i = 0;
        foreach (var position in positionList)
        {
            AddPoint(
                nameFunc(i),
                position,
                color,
                thickness
            );

            i++;
        }

        return this;
    }


    public GrVisualElementsSceneComposer3D<T> AddPoint(string name, IFloat64Vector3D position, IGrVisualElementMaterial3D material, double thickness)
    {
        AddPoint(
            GrVisualPoint3D.CreateStatic(
                name, 
                material.CreateThickSurfaceStyle(thickness),
                position
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddPoint(string name, IFloat64Vector3D position, Color color, double thickness)
    {
        AddPoint(
            GrVisualPoint3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                position
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Vector3D direction, IGrVisualElementMaterial3D material, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                Float64Vector3D.Zero,
                direction
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Vector3D direction, Color color, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                Float64Vector3D.Zero, 
                direction
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Vector3D origin, IFloat64Vector3D direction, IGrVisualElementMaterial3D material, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                origin, 
                direction
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Vector3D origin, IFloat64Vector3D direction, Color color, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                origin, 
                direction
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Vector3D point1, IFloat64Vector3D point2, IGrVisualElementMaterial3D material, double thickness)
    {
        AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                name,
                material.CreateTubeCurveStyle(thickness), 
                point1, 
                point2
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Vector3D point1, IFloat64Vector3D point2, Color color, double thickness)
    {
        AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                point1, 
                point2
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Vector3D point1, IFloat64Vector3D point2, Color color)
    {
        AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                name, 
                color.CreateSolidLineCurveStyle(),
                point1, 
                point2
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Vector3D point1, IFloat64Vector3D point2, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddLineSegment(
            GrVisualLineSegment3D.CreateStatic(
                name, 
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs), 
                point1, 
                point2
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Vector3D> pointList, IGrVisualElementMaterial3D material, double thickness)
    {
        AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness), 
                pointList
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Vector3D> pointList, Color color, double thickness)
    {
        AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness), 
                pointList
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Vector3D> pointList, Color color)
    {
        AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                name, 
                color.CreateSolidLineCurveStyle(), 
                pointList
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Vector3D> pointList, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddLinePath(
            GrVisualPointPathCurve3D.CreateStatic(
                name, 
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs), 
                pointList
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleOriginYz(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            Float64Vector3D.Zero,
            Float64Vector3D.E1,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleOriginZx(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            Float64Vector3D.Zero,
            Float64Vector3D.E2,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleOriginXy(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            Float64Vector3D.Zero,
            Float64Vector3D.E3,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleCurve(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                material.CreateTubeCurveStyle(thickness),
                center,
                normal.ToUnitVector(),
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleCurve(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color, double thickness)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                center,
                normal.ToUnitVector(),
                radius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleCurve(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                color.CreateSolidLineCurveStyle(),
                center,
                normal.ToUnitVector(),
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleCurve(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                center,
                normal.ToUnitVector(),
                radius
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                material.CreateTubeCurveStyle(thickness),
                center,
                direction1.ToUnitVector(),
                direction2.ToUnitVector(),
                radius,
                innerArc
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                center,
                direction1.ToUnitVector(),
                direction2.ToUnitVector(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                color.CreateSolidLineCurveStyle(),
                center,
                direction1.ToUnitVector(),
                direction2.ToUnitVector(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                center,
                direction1.ToUnitVector(),
                direction2.ToUnitVector(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, IGrVisualElementMaterial3D material, double thickness)
    {
        AddParametricCurve(
            GrVisualParametricCurve3D.Create(
                name, 
                material.CreateTubeCurveStyle(thickness), 
                curve, 
                parameterValues, 
                frameParameterValues
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, double thickness)
    {
        AddParametricCurve(
            GrVisualParametricCurve3D.Create(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                curve, 
                parameterValues, 
                frameParameterValues
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color)
    {
        AddParametricCurve(
            GrVisualParametricCurve3D.Create(
                name, 
                color.CreateSolidLineCurveStyle(),
                curve, 
                parameterValues, 
                frameParameterValues
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddParametricCurve(
            GrVisualParametricCurve3D.Create(
                name,
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                curve, 
                parameterValues, 
                frameParameterValues
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, double thickness)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                color.CreateSolidLineCurveStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, Color innerColor)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                AddOrGetColorMaterial(innerColor).CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, double thickness, Color innerColor)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                AddOrGetColorMaterial(innerColor).CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, Color innerColor)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                color.CreateSolidLineCurveStyle(),
                AddOrGetColorMaterial(innerColor).CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, Color innerColor)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                AddOrGetColorMaterial(innerColor).CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                innerMaterial.CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, double thickness, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                innerMaterial.CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                color.CreateSolidLineCurveStyle(),
                innerMaterial.CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            GrVisualRightAngle3D.CreateStatic(
                name, 
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                innerMaterial.CreateThinSurfaceStyle(),
                center, 
                direction1, 
                direction2, 
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphereSurface(string name, IFloat64Vector3D center, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(thickness),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphereSurface(string name, IFloat64Vector3D center, double radius, Color color, double thickness)
    {
        AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphereSurface(string name, IFloat64Vector3D center, double radius, IGrVisualElementMaterial3D material)
    {
        AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                material.CreateThinSurfaceStyle(),
                center,
                radius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddSphereSurface(string name, IFloat64Vector3D center, double radius, Color color)
    {
        AddSphereSurface(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddTorus(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double minRadius, double maxRadius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleRingSurface(
            GrVisualCircleRingSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(thickness),
                center,
                normal,
                minRadius,
                maxRadius
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddTorus(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double minRadius, double maxRadius, Color color, double thickness)
    {
        AddCircleRingSurface(
            GrVisualCircleRingSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                center,
                normal,
                minRadius,
                maxRadius
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(thickness),
                center,
                normal,
                radius,
                false
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color, double thickness)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                center,
                normal,
                radius,
                false
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                material.CreateThinSurfaceStyle(),
                center,
                normal,
                radius,
                false
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                center,
                normal,
                radius,
                false
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(AddOrGetColorMaterial(edgeColor), thickness),
                center,
                normal,
                radius,
                true
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, Color color, double thickness, Color edgeColor)
    {
        AddCircleSurface(
            GrVisualCircleSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(AddOrGetColorMaterial(edgeColor), thickness),
                center,
                normal,
                radius,
                true
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                false
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                false
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                false
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(edgeMaterial, thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                true
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(edgeMaterial, thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                true
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(AddOrGetColorMaterial(edgeColor), thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                true
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Vector3D center, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness, Color edgeColor)
    {
        AddCircleArcSurface(
            GrVisualCircleArcSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(AddOrGetColorMaterial(edgeColor), thickness),
                center,
                direction1,
                direction2,
                radius,
                innerArc,
                true
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddTriangle(string name, IFloat64Vector3D position1, IFloat64Vector3D position2, IFloat64Vector3D position3, IGrVisualElementMaterial3D material, double thickness)
    {
        AddTriangleSurface(
            GrVisualTriangleSurface3D.CreateStatic(
                name, 
                material.CreateThickSurfaceStyle(thickness),
                position1, 
                position2, 
                position3
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddTriangle(string name, IFloat64Vector3D position1, IFloat64Vector3D position2, IFloat64Vector3D position3, Color color, double thickness)
    {
        AddTriangleSurface(
            GrVisualTriangleSurface3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                position1, 
                position2, 
                position3
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddTriangle(string name, IFloat64Vector3D position1, IFloat64Vector3D position2, IFloat64Vector3D position3, IGrVisualElementMaterial3D material)
    {
        AddTriangleSurface(
            GrVisualTriangleSurface3D.CreateStatic(
                name, 
                material.CreateThinSurfaceStyle(),
                position1, 
                position2, 
                position3
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddTriangle(string name, IFloat64Vector3D position1, IFloat64Vector3D position2, IFloat64Vector3D position3, Color color)
    {
        AddTriangleSurface(
            GrVisualTriangleSurface3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                position1, 
                position2, 
                position3
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddParallelogram(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, IGrVisualElementMaterial3D material, double thickness)
    {
        AddParallelogramSurface(
            GrVisualParallelogramSurface3D.CreateStatic(
                name, 
                material.CreateThickSurfaceStyle(thickness),
                position, 
                direction1, 
                direction2
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParallelogram(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, Color color, double thickness)
    {
        AddParallelogramSurface(
            GrVisualParallelogramSurface3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                position, 
                direction1, 
                direction2
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParallelogram(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, IGrVisualElementMaterial3D material)
    {
        AddParallelogramSurface(
            GrVisualParallelogramSurface3D.CreateStatic(
                name, 
                material.CreateThinSurfaceStyle(),
                position, 
                direction1, 
                direction2
            )
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParallelogram(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, Color color)
    {
        AddParallelogramSurface(
            GrVisualParallelogramSurface3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                position, 
                direction1, 
                direction2
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddParallelogram(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, GrVisualSurfaceThinStyle3D style)
    {
        AddParallelogramSurface(
            GrVisualParallelogramSurface3D.CreateStatic(
                name, 
                style,
                position, 
                direction1, 
                direction2
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddParallelogramSystem(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, IFloat64Vector3D direction3, GrVisualSurfaceThinStyle3D faceStyle)
    {
        AddParallelogram(
            name, 
            position, 
            direction1, 
            direction2, 
            faceStyle
        );

        AddParallelogram(
            name, 
            position, 
            direction2, 
            direction3, 
            faceStyle
        );

        AddParallelogram(
            name, 
            position, 
            direction3, 
            direction1, 
            faceStyle
        );

        var position123 = 
            position.Add(direction1).Add(direction2).Add(direction3);

        AddParallelogram(
            name, 
            position123, 
            direction1.Negative(), 
            direction2.Negative(), 
            faceStyle
        );
            
        AddParallelogram(
            name, 
            position123, 
            direction2.Negative(), 
            direction3.Negative(), 
            faceStyle
        );

        AddParallelogram(
            name, 
            position123, 
            direction3.Negative(), 
            direction1.Negative(), 
            faceStyle
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddParallelogramSystem(string name, IFloat64Vector3D position, IFloat64Vector3D direction1, IFloat64Vector3D direction2, IFloat64Vector3D direction3, GrVisualSurfaceThinStyle3D faceStyle12, GrVisualSurfaceThinStyle3D faceStyle23, GrVisualSurfaceThinStyle3D faceStyle31)
    {
        AddParallelogram(
            name, 
            position, 
            direction1, 
            direction2, 
            faceStyle12
        );

        AddParallelogram(
            name, 
            position, 
            direction2, 
            direction3, 
            faceStyle23
        );

        AddParallelogram(
            name, 
            position, 
            direction3, 
            direction1, 
            faceStyle31
        );

        var position123 = 
            position.Add(direction1).Add(direction2).Add(direction3);

        AddParallelogram(
            name, 
            position123, 
            direction1.Negative(), 
            direction2.Negative(), 
            faceStyle12
        );
            
        AddParallelogram(
            name, 
            position123, 
            direction2.Negative(), 
            direction3.Negative(), 
            faceStyle23
        );

        AddParallelogram(
            name, 
            position123, 
            direction3.Negative(), 
            direction1.Negative(), 
            faceStyle31
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, WclHtmlImageDataUrlCache pngCache, IFloat64Vector3D origin, double scalingFactor)
    {
        AddLaTeXText(
            GrVisualLaTeXText3D.CreateStatic(
                name, 
                pngCache, 
                name, 
                origin, 
                scalingFactor
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, WclHtmlImageDataUrlCache pngCache, GrVisualAnimatedVector3D origin, double scalingFactor)
    {
        AddLaTeXText(
            GrVisualLaTeXText3D.CreateAnimated(
                name, 
                pngCache, 
                name, 
                origin, 
                scalingFactor
            )
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, WclHtmlImageDataUrlCache pngCache, string key, IFloat64Vector3D origin, double scalingFactor)
    {
        AddLaTeXText(
            GrVisualLaTeXText3D.CreateStatic(
                name, 
                pngCache, 
                key, 
                origin, 
                scalingFactor
            )
        );

        return this;
    }
        
    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, WclHtmlImageDataUrlCache pngCache, string key, GrVisualAnimatedVector3D origin, double scalingFactor)
    {
        AddLaTeXText(
            GrVisualLaTeXText3D.CreateAnimated(
                name, 
                pngCache, 
                key, 
                origin, 
                scalingFactor
            )
        );

        return this;
    }


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

            case GrVisualArrowHead3D arrowHead:
                AddArrowHead(arrowHead);
                break;

            case GrVisualLineSegment3D lineSegment:
                AddLineSegment(lineSegment);
                break;
                
            case GrVisualPointPathCurve3D lineCurve:
                AddLinePath(lineCurve);
                break;
                    
            case GrVisualTriangleSurface3D triangleSurface:
                AddTriangleSurface(triangleSurface);
                break;

            case GrVisualParallelogramSurface3D parallelogramSurface:
                AddParallelogramSurface(parallelogramSurface);
                break;

            case GrVisualCircleCurve3D circleCurve:
                AddCircleCurve(circleCurve);
                break;

            case GrVisualCircleArcCurve3D circleCurveArc:
                AddCircleArc(circleCurveArc);
                break;

            case GrVisualParametricCurve3D parametricCurve:
                AddParametricCurve(parametricCurve);
                break;

            case GrVisualCircleSurface3D circleSurface:
                AddCircleSurface(circleSurface);
                break;

            case GrVisualCircleArcSurface3D circleSurfaceArc:
                AddCircleArcSurface(circleSurfaceArc);
                break;
                
            case GrVisualCircleRingSurface3D ringSurface:
                AddCircleRingSurface(ringSurface);
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
        
    public void AddVectors(params GrVisualVector3D[] visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);
    }

    public GrVisualVector3D AddVector(GrVisualVector3D visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);

        return visualElement;
    }
        
    public GrVisualBivector3D AddBivector(GrVisualBivector3D visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);

        return visualElement;
    }

    public GrVisualFrame3D AddFrame(GrVisualFrame3D visualElement)
    {
        foreach (var childElement in visualElement)
            AddElement(childElement);

        return visualElement;
    }


    public abstract GrVisualLaTeXText3D AddLaTeXText(GrVisualLaTeXText3D visualElement);
        
    public abstract GrVisualSquareGrid3D AddSquareGrid(GrVisualSquareGrid3D visualElement);

    public abstract IGrVisualImage3D AddImage(IGrVisualImage3D visualElement);

    public abstract GrVisualPoint3D AddPoint(GrVisualPoint3D visualElement);

    public abstract GrVisualArrowHead3D AddArrowHead(GrVisualArrowHead3D visualElement);

    public abstract GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement);

    public abstract GrVisualParametricCurve3D AddParametricCurve(GrVisualParametricCurve3D visualElement);
        
    public abstract GrVisualCurveWithAnimation3D AddCurve(GrVisualCurveWithAnimation3D visualElement);

    public GrVisualLineSegment3D AddLineSegment(GrVisualLineSegment3D visualElement)
    {
        AddCurve(visualElement);

        return visualElement;
    }
    
    public GrVisualPointPathCurve3D AddLinePath(GrVisualPointPathCurve3D visualElement)
    {
        AddCurve(visualElement);

        return visualElement;
    }

    //public GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement)
    //{
    //    AddCurve(visualElement);

    //    return visualElement;
    //}

    public GrVisualCircleArcCurve3D AddCircleArc(GrVisualCircleArcCurve3D visualElement)
    {
        AddCurve(visualElement);

        return visualElement;
    }
        
    public abstract GrVisualRightAngle3D AddRightAngle(GrVisualRightAngle3D visualElement);
        

    public abstract GrVisualTriangleSurface3D AddTriangleSurface(GrVisualTriangleSurface3D visualElement);

    public abstract GrVisualParallelogramSurface3D AddParallelogramSurface(GrVisualParallelogramSurface3D visualElement);

    public abstract GrVisualParallelepipedSurface3D AddParallelepipedSurface(GrVisualParallelepipedSurface3D visualElement);

    public abstract GrVisualSphereSurface3D AddSphereSurface(GrVisualSphereSurface3D visualElement);

    public abstract GrVisualCircleSurface3D AddCircleSurface(GrVisualCircleSurface3D visualElement);

    public abstract GrVisualCircleArcSurface3D AddCircleArcSurface(GrVisualCircleArcSurface3D visualElement);
    
    public abstract GrVisualCircleRingSurface3D AddCircleRingSurface(GrVisualCircleRingSurface3D visualElement);
    
        
}
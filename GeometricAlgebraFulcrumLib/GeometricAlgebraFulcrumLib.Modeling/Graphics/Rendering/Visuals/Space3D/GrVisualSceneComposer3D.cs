using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D;

public abstract class GrVisualSceneComposer3D
{
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

    
    public GrVisualSceneComposer3D AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, params ILinFloat64Vector3D[] positionList)
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

    public GrVisualSceneComposer3D AddPoints(Func<int, string> nameFunc, Color color, double thickness, params ILinFloat64Vector3D[] positionList)
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
    
    public GrVisualSceneComposer3D AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, IEnumerable<ILinFloat64Vector3D> positionList)
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

    public GrVisualSceneComposer3D AddPoints(Func<int, string> nameFunc, Color color, double thickness, IEnumerable<ILinFloat64Vector3D> positionList)
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


    public GrVisualSceneComposer3D AddPoint(string name, ILinFloat64Vector3D position, IGrVisualElementMaterial3D material, double thickness)
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
    
    public GrVisualSceneComposer3D AddPoint(string name, ILinFloat64Vector3D position, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddVector(string name, ILinFloat64Vector3D direction, IGrVisualElementMaterial3D material, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                material.CreateTubeCurveStyle(thickness),
                LinFloat64Vector3D.Zero,
                direction
            )
        );

        return this;
    }

    public GrVisualSceneComposer3D AddVector(string name, ILinFloat64Vector3D direction, Color color, double thickness)
    {
        AddVector(
            GrVisualVector3D.CreateStatic(
                name, 
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                LinFloat64Vector3D.Zero, 
                direction
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddVector(string name, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, IGrVisualElementMaterial3D material, double thickness)
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

    public GrVisualSceneComposer3D AddVector(string name, ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, Color color, double thickness)
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
        
    public GrVisualSceneComposer3D AddLineSegment(string name, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, IGrVisualElementMaterial3D material, double thickness)
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

    public GrVisualSceneComposer3D AddLineSegment(string name, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddLineSegment(string name, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, Color color)
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
        
    public GrVisualSceneComposer3D AddLineSegment(string name, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, Color color, GrVisualDashedLineSpecs dashSpecs)
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
    
    public GrVisualSceneComposer3D AddLinePath(string name, IReadOnlyList<ILinFloat64Vector3D> pointList, IGrVisualElementMaterial3D material, double thickness)
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

    public GrVisualSceneComposer3D AddLinePath(string name, IReadOnlyList<ILinFloat64Vector3D> pointList, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddLinePath(string name, IReadOnlyList<ILinFloat64Vector3D> pointList, Color color)
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

    public GrVisualSceneComposer3D AddLinePath(string name, IReadOnlyList<ILinFloat64Vector3D> pointList, Color color, GrVisualDashedLineSpecs dashSpecs)
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
    
    public GrVisualSceneComposer3D AddCircleOriginYz(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E1,
            radius,
            material,
            thickness
        );
    }

    public GrVisualSceneComposer3D AddCircleOriginZx(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E2,
            radius,
            material,
            thickness
        );
    }

    public GrVisualSceneComposer3D AddCircleOriginXy(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircleCurve(
            name,
            LinFloat64Vector3D.Zero,
            LinFloat64Vector3D.E3,
            radius,
            material,
            thickness
        );
    }

    public GrVisualSceneComposer3D AddCircleCurve(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                material.CreateTubeCurveStyle(thickness),
                center,
                normal.ToUnitLinVector3D(),
                radius
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddCircleCurve(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color, double thickness)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                center,
                normal.ToUnitLinVector3D(),
                radius
            )
        );

        return this;
    }

    public GrVisualSceneComposer3D AddCircleCurve(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                color.CreateSolidLineCurveStyle(),
                center,
                normal.ToUnitLinVector3D(),
                radius
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddCircleCurve(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircleCurve(
            GrVisualCircleCurve3D.CreateStatic(
                name,
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                center,
                normal.ToUnitLinVector3D(),
                radius
            )
        );

        return this;
    }
        
    public GrVisualSceneComposer3D AddCircleArc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                material.CreateTubeCurveStyle(thickness),
                center,
                direction1.ToUnitLinVector3D(),
                direction2.ToUnitLinVector3D(),
                radius,
                innerArc
            )
        );

        return this;
    }

    public GrVisualSceneComposer3D AddCircleArc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateTubeCurveStyle(thickness),
                center,
                direction1.ToUnitLinVector3D(),
                direction2.ToUnitLinVector3D(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddCircleArc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                color.CreateSolidLineCurveStyle(),
                center,
                direction1.ToUnitLinVector3D(),
                direction2.ToUnitLinVector3D(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddCircleArc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircleArc(
            GrVisualCircleArcCurve3D.CreateStatic(
                name,
                new GrVisualCurveDashedLineStyle3D(color, dashSpecs),
                center,
                direction1.ToUnitLinVector3D(),
                direction2.ToUnitLinVector3D(),
                radius,
                innerArc
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddParametricCurve(string name, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, IGrVisualElementMaterial3D material, double thickness)
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

    public GrVisualSceneComposer3D AddParametricCurve(string name, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddParametricCurve(string name, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color)
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

    public GrVisualSceneComposer3D AddParametricCurve(string name, Float64Path3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, GrVisualDashedLineSpecs dashSpecs)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness)
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

    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, Color innerColor)
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

    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, double thickness, Color innerColor)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, Color innerColor)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, Color innerColor)
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
        
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D innerMaterial)
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

    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, double thickness, IGrVisualElementMaterial3D innerMaterial)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, IGrVisualElementMaterial3D innerMaterial)
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
    
    public GrVisualSceneComposer3D AddRightAngle(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, IGrVisualElementMaterial3D innerMaterial)
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
    
    public GrVisualSceneComposer3D AddSphereSurface(string name, ILinFloat64Vector3D center, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                material.CreateThickSurfaceStyle(thickness),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddSphereSurface(string name, ILinFloat64Vector3D center, double radius, Color color, double thickness)
    {
        AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThickSurfaceStyle(thickness),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddSphereSurface(string name, ILinFloat64Vector3D center, double radius, IGrVisualElementMaterial3D material)
    {
        AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                material.CreateThinSurfaceStyle(),
                center,
                radius
            )
        );

        return this;
    }

    public GrVisualSceneComposer3D AddSphereSurface(string name, ILinFloat64Vector3D center, double radius, Color color)
    {
        AddSphere(
            GrVisualSphereSurface3D.CreateStatic(
                name,
                AddOrGetColorMaterial(color).CreateThinSurfaceStyle(),
                center,
                radius
            )
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddTorus(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double minRadius, double maxRadius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddRing(
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

    public GrVisualSceneComposer3D AddTorus(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double minRadius, double maxRadius, Color color, double thickness)
    {
        AddRing(
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
    
    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddDisc(
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

    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color, double thickness)
    {
        AddDisc(
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
    
    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material)
    {
        AddDisc(
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

    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color)
    {
        AddDisc(
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
    
    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddDisc(
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
    
    public GrVisualSceneComposer3D AddDisc(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D normal, double radius, Color color, double thickness, Color edgeColor)
    {
        AddDisc(
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

    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddDiscSector(
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
    
    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddDiscSector(
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
    
    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color)
    {
        AddDiscSector(
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
    
    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddDiscSector(
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
    
    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddDiscSector(
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

    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddDiscSector(
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
    
    public GrVisualSceneComposer3D AddDiscSector(string name, ILinFloat64Vector3D center, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, double radius, bool innerArc, Color color, double thickness, Color edgeColor)
    {
        AddDiscSector(
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
        
    public GrVisualSceneComposer3D AddTriangle(string name, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, IGrVisualElementMaterial3D material, double thickness)
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
    
    public GrVisualSceneComposer3D AddTriangle(string name, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddTriangle(string name, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, IGrVisualElementMaterial3D material)
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
    
    public GrVisualSceneComposer3D AddTriangle(string name, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, Color color)
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
        
    public GrVisualSceneComposer3D AddParallelogram(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, IGrVisualElementMaterial3D material, double thickness)
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
    
    public GrVisualSceneComposer3D AddParallelogram(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, Color color, double thickness)
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
    
    public GrVisualSceneComposer3D AddParallelogram(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, IGrVisualElementMaterial3D material)
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
    
    public GrVisualSceneComposer3D AddParallelogram(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, Color color)
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
    
    public GrVisualSceneComposer3D AddParallelogram(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, GrVisualSurfaceThinStyle3D style)
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

    public GrVisualSceneComposer3D AddParallelogramSystem(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualSurfaceThinStyle3D faceStyle)
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
            position.VectorAdd(direction1).VectorAdd(direction2).VectorAdd(direction3);

        AddParallelogram(
            name, 
            position123, 
            direction1.VectorNegative(), 
            direction2.VectorNegative(), 
            faceStyle
        );
            
        AddParallelogram(
            name, 
            position123, 
            direction2.VectorNegative(), 
            direction3.VectorNegative(), 
            faceStyle
        );

        AddParallelogram(
            name, 
            position123, 
            direction3.VectorNegative(), 
            direction1.VectorNegative(), 
            faceStyle
        );

        return this;
    }
        
    public GrVisualSceneComposer3D AddParallelogramSystem(string name, ILinFloat64Vector3D position, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2, ILinFloat64Vector3D direction3, GrVisualSurfaceThinStyle3D faceStyle12, GrVisualSurfaceThinStyle3D faceStyle23, GrVisualSurfaceThinStyle3D faceStyle31)
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
            position.VectorAdd(direction1).VectorAdd(direction2).VectorAdd(direction3);

        AddParallelogram(
            name, 
            position123, 
            direction1.VectorNegative(), 
            direction2.VectorNegative(), 
            faceStyle12
        );
            
        AddParallelogram(
            name, 
            position123, 
            direction2.VectorNegative(), 
            direction3.VectorNegative(), 
            faceStyle23
        );

        AddParallelogram(
            name, 
            position123, 
            direction3.VectorNegative(), 
            direction1.VectorNegative(), 
            faceStyle31
        );

        return this;
    }
    
    public GrVisualSceneComposer3D AddLaTeXText(string name, IGrVisualImageSource texture, ILinFloat64Vector3D origin, double scalingFactor)
    {
        AddImage(
            GrVisualImage3D.CreateStatic(
                name, 
                texture, 
                origin, 
                scalingFactor
            )
        );

        return this;
    }

        
    public GrVisualSceneComposer3D AddLaTeXText(string name, IGrVisualImageSource texture, GrVisualAnimatedVector3D origin, double scalingFactor)
    {
        AddImage(
            GrVisualImage3D.CreateAnimated(
                name, 
                texture, 
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
            case GrVisualImage3D latexText:
                AddImage(latexText);
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
                AddDisc(circleSurface);
                break;

            case GrVisualCircleArcSurface3D circleSurfaceArc:
                AddDiscSector(circleSurfaceArc);
                break;
                
            case GrVisualCircleRingSurface3D ringSurface:
                AddRing(ringSurface);
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
    

    public abstract GrVisualImage3D AddImage(GrVisualImage3D visualElement);
    
    public abstract GrVisualSquareGrid3D AddSquareGrid(GrVisualSquareGrid3D visualElement);
    
    public abstract void AddGrid(string name, ITriplet<Float64Scalar> center, LinFloat64Quaternion orientation, int unitCount, double unitSize = 1, double opacity = 1);

    public abstract void AddAxes(string name, ITriplet<Float64Scalar> origin, LinFloat64Quaternion orientation, double scalingFactor = 1, double opacity = 1);

    public abstract IGrVisualImage3D AddImage(IGrVisualImage3D visualElement);

    public abstract GrVisualPoint3D AddPoint(GrVisualPoint3D visualElement);

    public abstract GrVisualArrowHead3D AddArrowHead(GrVisualArrowHead3D visualElement);

    public abstract GrVisualCircleCurve3D AddCircleCurve(GrVisualCircleCurve3D visualElement);

    public abstract GrVisualParametricCurve3D AddParametricCurve(GrVisualParametricCurve3D visualElement);
        
    public abstract GrVisualCurveWithAnimation3D AddCurve(GrVisualCurveWithAnimation3D visualElement);

    public virtual GrVisualLineSegment3D AddLineSegment(GrVisualLineSegment3D visualElement)
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

    public abstract GrVisualSphereSurface3D AddSphere(GrVisualSphereSurface3D visualElement);

    public abstract GrVisualCircleSurface3D AddDisc(GrVisualCircleSurface3D visualElement);

    public abstract GrVisualCircleArcSurface3D AddDiscSector(GrVisualCircleArcSurface3D visualElement);
    
    public abstract GrVisualCircleRingSurface3D AddRing(GrVisualCircleRingSurface3D visualElement);
    
}

public abstract class GrVisualSceneComposer3D<T> :
    GrVisualSceneComposer3D
{
    public abstract T SceneObject { get; }
}
using GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Grids;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples;
using SixLabors.ImageSharp;
using GraphicsComposerLib.Geometry.ParametricShapes.Curves;
using GraphicsComposerLib.Rendering.Images;

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

    public abstract IGrVisualElementMaterial3D AddOrGetColorMaterial(Color color);

    
    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, params IFloat64Tuple3D[] positionList)
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

    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, Color color, double thickness, params IFloat64Tuple3D[] positionList)
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
    
    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, IGrVisualElementMaterial3D material, double thickness, IEnumerable<IFloat64Tuple3D> positionList)
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

    public GrVisualElementsSceneComposer3D<T> AddPoints(Func<int, string> nameFunc, Color color, double thickness, IEnumerable<IFloat64Tuple3D> positionList)
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


    public GrVisualElementsSceneComposer3D<T> AddPoint(string name, IFloat64Tuple3D position, IGrVisualElementMaterial3D material, double thickness)
    {
        AddPoint(
            new GrVisualPoint3D(name, position)
            {
                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddPoint(string name, IFloat64Tuple3D position, Color color, double thickness)
    {
        AddPoint(
            new GrVisualPoint3D(name, position)
            {
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Tuple3D direction, IGrVisualElementMaterial3D material, double thickness)
    {
        AddVector(
            new GrVisualVector3D(name, Float64Tuple3D.Zero, direction)
            {
                Style = new GrVisualVectorStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Tuple3D direction, Color color, double thickness)
    {
        AddVector(
            new GrVisualVector3D(name, Float64Tuple3D.Zero, direction)
            {
                Style = new GrVisualVectorStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Tuple3D origin, IFloat64Tuple3D direction, IGrVisualElementMaterial3D material, double thickness)
    {
        AddVector(
            new GrVisualVector3D(name, origin, direction)
            {
                Style = new GrVisualVectorStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddVector(string name, IFloat64Tuple3D origin, IFloat64Tuple3D direction, Color color, double thickness)
    {
        AddVector(
            new GrVisualVector3D(name, origin, direction)
            {
                Style = new GrVisualVectorStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Tuple3D point1, IFloat64Tuple3D point2, IGrVisualElementMaterial3D material, double thickness)
    {
        AddLineSegment(
            new GrVisualLineSegment3D(name)
            {
                Position1 = point1,
                Position2 = point2,
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Tuple3D point1, IFloat64Tuple3D point2, Color color, double thickness)
    {
        AddLineSegment(
            new GrVisualLineSegment3D(name)
            {
                Position1 = point1,
                Position2 = point2,
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Tuple3D point1, IFloat64Tuple3D point2, Color color)
    {
        AddLineSegment(
            new GrVisualLineSegment3D(name)
            {
                Position1 = point1,
                Position2 = point2,
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLineSegment(string name, IFloat64Tuple3D point1, IFloat64Tuple3D point2, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddLineSegment(
            new GrVisualLineSegment3D(name)
            {
                Position1 = point1,
                Position2 = point2,
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Tuple3D> pointList, IGrVisualElementMaterial3D material, double thickness)
    {
        AddLinePath(
            new GrVisualLinePathCurve3D(name, pointList)
            {
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Tuple3D> pointList, Color color, double thickness)
    {
        AddLinePath(
            new GrVisualLinePathCurve3D(name, pointList)
            {
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Tuple3D> pointList, Color color)
    {
        AddLinePath(
            new GrVisualLinePathCurve3D(name, pointList)
            {
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLinePath(string name, IReadOnlyList<IFloat64Tuple3D> pointList, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddLinePath(
            new GrVisualLinePathCurve3D(name, pointList)
            {
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleOriginYz(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircle(
            name,
            Float64Tuple3D.Zero,
            Float64Tuple3D.E1,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleOriginZx(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircle(
            name,
            Float64Tuple3D.Zero,
            Float64Tuple3D.E2,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleOriginXy(string name, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        return AddCircle(
            name,
            Float64Tuple3D.Zero,
            Float64Tuple3D.E3,
            radius,
            material,
            thickness
        );
    }

    public GrVisualElementsSceneComposer3D<T> AddCircle(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircle(
            new GrVisualCircleCurve3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircle(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color, double thickness)
    {
        AddCircle(
            new GrVisualCircleCurve3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddCircle(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color)
    {
        AddCircle(
            new GrVisualCircleCurve3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircle(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircle(
            new GrVisualCircleCurve3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddCircleArc(
            new GrVisualCircleArcCurve3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddCircleArc(
            new GrVisualCircleArcCurve3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color)
    {
        AddCircleArc(
            new GrVisualCircleArcCurve3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddCircleArc(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddCircleArc(
            new GrVisualCircleArcCurve3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IGraphicsC1ParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, IGrVisualElementMaterial3D material, double thickness)
    {
        AddParametricCurve(
            new GrVisualParametricCurve3D(name, curve, parameterValues, frameParameterValues)
            {
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IGraphicsC1ParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, double thickness)
    {
        AddParametricCurve(
            new GrVisualParametricCurve3D(name, curve, parameterValues, frameParameterValues)
            {
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IGraphicsC1ParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color)
    {
        AddParametricCurve(
            new GrVisualParametricCurve3D(name, curve, parameterValues, frameParameterValues)
            {
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddParametricCurve(string name, IGraphicsC1ParametricCurve3D curve, IReadOnlyList<double> parameterValues, IReadOnlyList<double> frameParameterValues, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddParametricCurve(
            new GrVisualParametricCurve3D(name, curve, parameterValues, frameParameterValues)
            {
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, double thickness)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveSolidLineStyle3D(color)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, Color innerColor)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(material, thickness),

                InnerStyle = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(innerColor)
                )
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, double thickness, Color innerColor)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                ),

                InnerStyle = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(innerColor)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, Color innerColor)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveSolidLineStyle3D(color),

                InnerStyle = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(innerColor)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, Color innerColor)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs),

                InnerStyle = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(innerColor)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(material, thickness),

                InnerStyle = new GrVisualSurfaceThinStyle3D(innerMaterial)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, double thickness, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveTubeStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                ),

                InnerStyle = new GrVisualSurfaceThinStyle3D(innerMaterial)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveSolidLineStyle3D(color),

                InnerStyle = new GrVisualSurfaceThinStyle3D(innerMaterial)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRightAngle(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, Color color, GrVisualDashedLineSpecs dashSpecs, IGrVisualElementMaterial3D innerMaterial)
    {
        AddRightAngle(
            new GrVisualRightAngle3D(name, center, direction1, direction2, radius)
            {
                Style = new GrVisualCurveDashedLineStyle3D(color, dashSpecs),

                InnerStyle = new GrVisualSurfaceThinStyle3D(innerMaterial)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphere(string name, IFloat64Tuple3D center, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddSphere(
            new GrVisualSphere3D(name)
            {
                Center = center,
                Radius = radius,
                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphere(string name, IFloat64Tuple3D center, double radius, Color color, double thickness)
    {
        AddSphere(
            new GrVisualSphere3D(name)
            {
                Center = center,
                Radius = radius,
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddSphere(string name, IFloat64Tuple3D center, double radius, IGrVisualElementMaterial3D material)
    {
        AddSphere(
            new GrVisualSphere3D(name)
            {
                Center = center,
                Radius = radius,
                Style = new GrVisualSurfaceThinStyle3D(material)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddSphere(string name, IFloat64Tuple3D center, double radius, Color color)
    {
        AddSphere(
            new GrVisualSphere3D(name)
            {
                Center = center,
                Radius = radius,
                Style = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(color)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddTorus(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double minRadius, double maxRadius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddRingSurface(
            new GrVisualRingSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                MinRadius = minRadius,
                MaxRadius = maxRadius,

                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddTorus(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double minRadius, double maxRadius, Color color, double thickness)
    {
        AddRingSurface(
            new GrVisualRingSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                MinRadius = minRadius,
                MaxRadius = maxRadius,

                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, IGrVisualElementMaterial3D material, double thickness)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,

                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color, double thickness)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,

                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, IGrVisualElementMaterial3D material)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,

                Style = new GrVisualSurfaceThinStyle3D(material)
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,

                Style = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(color)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                DrawEdge = true,

                Style = new GrVisualSurfaceThickStyle3D(
                    material, 
                    AddOrGetColorMaterial(edgeColor), 
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDisc(string name, IFloat64Tuple3D center, IFloat64Tuple3D normal, double radius, Color color, double thickness, Color edgeColor)
    {
        AddDisc(
            new GrVisualCircleSurface3D(name)
            {
                Center = center,
                Normal = normal.ToUnitVector(),
                Radius = radius,
                DrawEdge = true,

                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color), 
                    AddOrGetColorMaterial(edgeColor), 
                    thickness
                )
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color, double thickness)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                Style = new GrVisualSurfaceThinStyle3D(AddOrGetColorMaterial(color))
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                DrawEdge = true,
                Style = new GrVisualSurfaceThickStyle3D(material, edgeMaterial, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color, double thickness, IGrVisualElementMaterial3D edgeMaterial)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                DrawEdge = true,
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color), 
                    edgeMaterial, 
                    thickness
                )
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, IGrVisualElementMaterial3D material, double thickness, Color edgeColor)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                DrawEdge = true,
                Style = new GrVisualSurfaceThickStyle3D(
                    material, 
                    AddOrGetColorMaterial(edgeColor), 
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddDiscSector(string name, IFloat64Tuple3D center, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, double radius, bool innerArc, Color color, double thickness, Color edgeColor)
    {
        AddDiscSector(
            new GrVisualCircleSurfaceArc3D(name)
            {
                Center = center,
                Direction1 = direction1.ToUnitVector(),
                Direction2 = direction2.ToUnitVector(),
                Radius = radius,
                InnerArc = innerArc,
                DrawEdge = true,
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color), 
                    AddOrGetColorMaterial(edgeColor), 
                    thickness
                )
            }
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddRectangle(string name, IFloat64Tuple3D bottomLeftCorner, IFloat64Tuple3D widthDirection, IFloat64Tuple3D heightDirection, IGrVisualElementMaterial3D material, double thickness)
    {
        AddRectangle(
            new GrVisualRectangleSurface3D(name, bottomLeftCorner, widthDirection, heightDirection)
            {
                Style = new GrVisualSurfaceThickStyle3D(material, thickness)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRectangle(string name, IFloat64Tuple3D bottomLeftCorner, IFloat64Tuple3D widthDirection, IFloat64Tuple3D heightDirection, Color color, double thickness)
    {
        AddRectangle(
            new GrVisualRectangleSurface3D(name, bottomLeftCorner, widthDirection, heightDirection)
            {
                Style = new GrVisualSurfaceThickStyle3D(
                    AddOrGetColorMaterial(color),
                    thickness
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRectangle(string name, IFloat64Tuple3D bottomLeftCorner, IFloat64Tuple3D widthDirection, IFloat64Tuple3D heightDirection, IGrVisualElementMaterial3D material)
    {
        AddRectangle(
            new GrVisualRectangleSurface3D(name, bottomLeftCorner, widthDirection, heightDirection)
            {
                Style = new GrVisualSurfaceThinStyle3D(material)
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddRectangle(string name, IFloat64Tuple3D bottomLeftCorner, IFloat64Tuple3D widthDirection, IFloat64Tuple3D heightDirection, Color color)
    {
        AddRectangle(
            new GrVisualRectangleSurface3D(name, bottomLeftCorner, widthDirection, heightDirection)
            {
                Style = new GrVisualSurfaceThinStyle3D(
                    AddOrGetColorMaterial(color)
                )
            }
        );

        return this;
    }
    
    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, GrImageBase64StringCache pngCache, IFloat64Tuple3D origin, double scalingFactor)
    {
        AddLaTeXText(
            new GrVisualLaTeXText3D(name, pngCache, name, origin, scalingFactor)
        );

        return this;
    }

    public GrVisualElementsSceneComposer3D<T> AddLaTeXText(string name, GrImageBase64StringCache pngCache, string key, IFloat64Tuple3D origin, double scalingFactor)
    {
        AddLaTeXText(
            new GrVisualLaTeXText3D(name, pngCache, key, origin, scalingFactor)
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

            case GrVisualVector3D vector:
                AddVector(vector);
                break;

            case GrVisualLineSegment3D lineSegment:
                AddLineSegment(lineSegment);
                break;
                
            case GrVisualLinePathCurve3D lineCurve:
                AddLinePath(lineCurve);
                break;

            case GrVisualRectangleSurface3D rectangleSurface:
                AddRectangle(rectangleSurface);
                break;

            case GrVisualCircleCurve3D circleCurve:
                AddCircle(circleCurve);
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

            case GrVisualCircleSurfaceArc3D circleSurfaceArc:
                AddDiscSector(circleSurfaceArc);
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
    
    public abstract void AddLinePath(GrVisualLinePathCurve3D visualElement);

    public abstract void AddCircle(GrVisualCircleCurve3D visualElement);

    public abstract void AddCircleArc(GrVisualCircleArcCurve3D visualElement);

    public abstract void AddParametricCurve(GrVisualParametricCurve3D visualElement);

    public abstract void AddRectangle(GrVisualRectangleSurface3D visualElement);

    public abstract void AddSphere(GrVisualSphere3D visualElement);

    public abstract void AddDisc(GrVisualCircleSurface3D visualElement);

    public abstract void AddDiscSector(GrVisualCircleSurfaceArc3D visualElement);
    
    public abstract void AddRingSurface(GrVisualRingSurface3D visualElement);
    
    public abstract void AddRightAngle(GrVisualRightAngle3D visualElement);
}
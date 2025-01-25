using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FPP;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public class GrPovRayGridElementsComposer :
    GrPovRayElementsComposer
{
    public GrVisualSquareGrid3D VisualSquareGrid { get; }


    public GrPovRayGridElementsComposer(GrPovRayScene sceneObject, GrVisualSquareGrid3D visualSquareGrid) 
        : base(sceneObject)
    {
        VisualSquareGrid = visualSquareGrid;
    }


    public GrPovRayColorMapPigment RasterLinesPigment(double lineWidth, double lineIntensity)
    {
        var pigment = new GrPovRayColorMapPigment(GrPovRayPattern.GradientX());

        pigment.ColorMap
            .AddOrSetRgbt(0, lineIntensity, 0)
            .AddOrSetRgbt(0 + lineWidth, lineIntensity, 0, 1, 1)
            .AddOrSetRgbt(1 - lineWidth, 1, 1, lineIntensity, 1)
            .AddOrSetRgbt(1, lineIntensity, 0);

        pigment.AffineMap.Scale(VisualSquareGrid.UnitSize);

        return pigment;
    }

    public GrPovRayPolygon GridRectangle()
    {
        var rectangle = GrPovRayPolygon.CreateRectangleXy(
            VisualSquareGrid.Size1, 
            VisualSquareGrid.Size2
        );
        
        return rectangle;
    }
}
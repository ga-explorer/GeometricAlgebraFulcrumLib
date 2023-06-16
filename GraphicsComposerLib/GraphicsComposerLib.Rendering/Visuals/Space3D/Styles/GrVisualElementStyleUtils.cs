namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

public static class GrVisualElementStyleUtils
{
    //public static GrVisualArrowHeadThickStyle3D CreateThickArrowHeadStyle(this IGrVisualElementMaterial3D material, double thickness)
    //{
    //    return new GrVisualArrowHeadThickStyle3D(material, thickness);
    //}


    public static GrVisualCurveDashedLineStyle3D CreateDashedLineCurveStyle(this Color color, int dashOn, int dashOff, int dashPerLine)
    {
        return new GrVisualCurveDashedLineStyle3D(
            color,
            new GrVisualDashedLineSpecs(dashOn, dashOff, dashPerLine)
        );
    }

    public static GrVisualCurveDashedLineStyle3D CreateDashedLineCurveStyle(this Color color, GrVisualDashedLineSpecs dashSpecs)
    {
        return new GrVisualCurveDashedLineStyle3D(
            color,
            dashSpecs
        );
    }

    public static GrVisualCurveSolidLineStyle3D CreateSolidLineCurveStyle(this Color color)
    {
        return new GrVisualCurveSolidLineStyle3D(color);
    }

    public static GrVisualCurveTubeStyle3D CreateTubeCurveStyle(this IGrVisualElementMaterial3D material, double thickness)
    {
        return new GrVisualCurveTubeStyle3D(material, thickness);
    }

    public static GrVisualSurfaceThinStyle3D CreateThinSurfaceStyle(this IGrVisualElementMaterial3D material)
    {
        return new GrVisualSurfaceThinStyle3D(material);
    }

    public static GrVisualSurfaceThinStyle3D CreateThinSurfaceStyle(this IGrVisualElementMaterial3D material, Color edgeColor)
    {
        return new GrVisualSurfaceThinStyle3D(material, edgeColor);
    }

    public static GrVisualSurfaceThickStyle3D CreateThickSurfaceStyle(this IGrVisualElementMaterial3D material, double thickness)
    {
        return new GrVisualSurfaceThickStyle3D(material, thickness);
    }

    public static GrVisualSurfaceThickStyle3D CreateThickSurfaceStyle(this IGrVisualElementMaterial3D material, IGrVisualElementMaterial3D edgeMaterial, double thickness)
    {
        return new GrVisualSurfaceThickStyle3D(material, edgeMaterial, thickness);
    }
}


namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

public static class XeoglConstantsUtils
{
    private static readonly string[] GraphicsPrimitiveTypeNames =
    {
        "points",
        "lines",
        "line-loop",
        "line-strip",
        "triangles",
        "triangle-strip",
        "triangle-fan"
    };

    private static readonly string[] BillboardBehaviourNames =
    {
        "none",
        "spherical",
        "cylindrical"
    };

    private static readonly string[] MaterialTypeNames =
    {
        "CodeMaterial",
        "MetallicMaterial",
        "SpecularMaterial",
        "PhongMaterial",
        "LambertMaterial",
        "EmphasisMaterial",
        "EdgeMaterial",
        "OutlineMaterial"
    };

    private static readonly string[] AlphaModeNames =
    {
        "opaque", "blend", "mask"
    };

    private static readonly string[] WindingDirectionNames =
    {
        "ccw", "cw"
    };

    public static readonly string[] SpaceNames =
    {
        "view", "space"
    };

    public static readonly string[] CameraProjectionTypeNames =
    {
        "perspective", "ortho", "frustum", "customProjection"
    };

    public static readonly string[] FieldOfViewAxisNames =
    {
        "x", "y", "min"
    };

        
    public static string GetName(this XeoglBillboardBehaviour billboardBehaviour)
    {
        return BillboardBehaviourNames[(int) billboardBehaviour];
    }

    public static string GetName(this XeoglMaterialType materialType)
    {
        return MaterialTypeNames[(int) materialType];
    }

    public static string GetName(this XeoglAlphaMode alphaMode)
    {
        return AlphaModeNames[(int) alphaMode];
    }

    public static string GetName(this XeoglWindingDirection windingDirection)
    {
        return WindingDirectionNames[(int) windingDirection];
    }

    public static string GetName(this XeoglSpace space)
    {
        return SpaceNames[(int) space];
    }

    public static string GetName(this XeoglCameraProjectionType projectionType)
    {
        return CameraProjectionTypeNames[(int) projectionType];
    }

    public static string GetName(this XeoglPerspectiveFieldOfViewAxis fieldOfViewAxis)
    {
        return FieldOfViewAxisNames[(int) fieldOfViewAxis];
    }
}
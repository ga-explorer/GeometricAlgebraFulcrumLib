using GraphicsComposerLib.Geometry.Primitives;

namespace GraphicsComposerLib.WebGl.Xeogl.Constants
{
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

        
        public static string GetName(this GraphicsPrimitiveType3D primitiveType)
            => GraphicsPrimitiveTypeNames[(int)primitiveType];

        public static string GetName(this XeoglBillboardBehaviour billboardBehaviour)
            => BillboardBehaviourNames[(int)billboardBehaviour];

        public static string GetName(this XeoglMaterialType materialType)
            => MaterialTypeNames[(int)materialType];

        public static string GetName(this XeoglAlphaMode alphaMode)
            => AlphaModeNames[(int)alphaMode];

        public static string GetName(this XeoglWindingDirection windingDirection)
            => WindingDirectionNames[(int)windingDirection];

        public static string GetName(this XeoglSpace space)
            => SpaceNames[(int)space];

        public static string GetName(this XeoglCameraProjectionType projectionType)
            => CameraProjectionTypeNames[(int)projectionType];

        public static string GetName(this XeoglPerspectiveFieldOfViewAxis fieldOfViewAxis)
            => FieldOfViewAxisNames[(int)fieldOfViewAxis];

    }
}

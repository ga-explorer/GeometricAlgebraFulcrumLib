using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;

public static class GrPovRayPigmentUtils
{
    public static GrPovRayColorListPigment ToColorListPigment(this IGrPovRayColorListPigmentPattern pattern)
    {
        return new GrPovRayColorListPigment(pattern);
    }

    public static GrPovRayColorListPigment ToColorListPigment(this IGrPovRayColorListPigmentPattern pattern, string basePigmentName)
    {
        return new GrPovRayColorListPigment(basePigmentName, pattern);
    }
}
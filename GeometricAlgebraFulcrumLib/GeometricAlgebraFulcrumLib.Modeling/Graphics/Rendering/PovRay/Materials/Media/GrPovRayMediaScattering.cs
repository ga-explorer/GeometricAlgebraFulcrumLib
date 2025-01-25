using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media;

public sealed record GrPovRayMediaScattering
    : IGrPovRayCodeElement
{
    public int ScatteringType { get; }

    public GrPovRayColorValue Color { get; }

    public double Eccentricity { get; set; } = 0;

    public double Extinction { get; set; } = 1;


    public GrPovRayMediaScattering(int scatteringType, GrPovRayColorValue color, double eccentricity = 0, double extinction = 1)
    {
        ScatteringType = scatteringType;
        Color = color;
        Eccentricity = eccentricity;
        Extinction = extinction;
    }


    public bool IsEmptyCodeElement()
    {
        return false;
    }

    public string GetPovRayCode()
    {
        return $"scattering {{{ScatteringType}, {Color.GetPovRayCode()} eccentricity {Eccentricity} extinction {Extinction}}}";
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}
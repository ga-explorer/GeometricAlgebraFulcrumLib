using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRaySphereSweep : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject,
    IReadOnlyList<GrPovRaySphereListItem>
{
    private static readonly string[] SweepTypeNames = new[]
    {
        "linear_spline", 
        "b_spline", 
        "cubic_spline"
    };

    
    public GrPovRaySphereList Spheres { get; }
        = new GrPovRaySphereList();

    public int Count 
        => Spheres.Count;

    public GrPovRaySphereListItem this[int index]
    {
        get => Spheres[index];
        set => Spheres[index] = value;
    }


    public GrPovRayFloat32Value? Tolerance { get; set; }

    public GrPovRaySphereSweepType SweepType { get; }

    public string SweepTypeName 
        => SweepTypeNames[(int)SweepType];
    

    internal GrPovRaySphereSweep(GrPovRaySphereSweepType interpolationKind)
    {
        SweepType = interpolationKind;
    }

    
    public GrPovRaySphereSweep SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("sphere_sweep {")
            .IncreaseIndentation()
            .AppendAtNewLine(SweepTypeName)
            .AppendAtNewLine(Spheres.Count + ", ")
            .AppendAtNewLine(Spheres.GetPovRayCode());

        if (Tolerance is not null)
            composer.AppendAtNewLine("tolerance " + Tolerance.GetPovRayCode());

        composer
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
    
    public IEnumerator<GrPovRaySphereListItem> GetEnumerator()
    {
        return Spheres.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
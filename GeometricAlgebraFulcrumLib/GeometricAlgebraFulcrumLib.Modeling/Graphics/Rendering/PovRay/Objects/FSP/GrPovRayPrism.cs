using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayPrism : 
    GrPovRayPolynomialObject, 
    IGrPovRayFiniteSolidObject,
    IReadOnlyList<GrPovRayVector2Value>
{
    private static readonly string[] SplineTypeNames = new[]
    {
        "linear_spline", 
        "quadratic_spline",
        "cubic_spline",
        "bezier_spline"
    };

    private static readonly string[] SweepTypeNames = new[]
    {
        "linear_sweep", 
        "conic_sweep"
    };


    
    public GrPovRayVector2List Points { get; } 
        = new GrPovRayVector2List();

    public int Count 
        => Points.Count;

    public GrPovRayVector2Value this[int index]
    {
        get => Points[index];
        set => Points[index] = value;
    }
    
    public GrPovRayFloat32Value Height1 { get; }

    public GrPovRayFloat32Value Height2 { get; }

    public bool Open { get; set; }

    public GrPovRayPrismSplineType SplineType { get; }
    
    public GrPovRayPrismSweepType SweepType { get; }

    public string SplineTypeName 
        => SplineTypeNames[(int)SplineType];
    
    public string SweepTypeName 
        => SweepTypeNames[(int)SweepType];
    

    internal GrPovRayPrism(GrPovRayFloat32Value height1, GrPovRayFloat32Value height2, GrPovRayPrismSplineType splineType, GrPovRayPrismSweepType sweepType, bool open)
    {
        Height1 = height1;
        Height2 = height2;
        SplineType = splineType;
        SweepType = sweepType;
        Open = open;
    }


    public GrPovRayPrism SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("prism {")
            .IncreaseIndentation()
            .Append(SplineTypeName + ", ")
            .Append(SweepTypeName + ", ")
            .AppendAtNewLine(Height1.GetPovRayCode() + ", ")
            .Append(Height2.GetPovRayCode() + ", ")
            .AppendAtNewLine(Points.Count + ", ")
            .AppendAtNewLine(Points.GetPovRayCode());

        if (Open)
            composer.AppendAtNewLine("open");

        composer
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
    
    public IEnumerator<GrPovRayVector2Value> GetEnumerator()
    {
        return Points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
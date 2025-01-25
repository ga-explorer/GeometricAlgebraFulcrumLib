using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayLathe : 
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

    
    public GrPovRayVector2List Points { get; } 
        = new GrPovRayVector2List();

    public int Count 
        => Points.Count;

    public GrPovRayVector2Value this[int index]
    {
        get => Points[index];
        set => Points[index] = value;
    }

    public GrPovRayLatheSplineType SplineType { get; }
    
    public string SplineTypeName 
        => SplineTypeNames[(int)SplineType];
    

    internal GrPovRayLathe(GrPovRayLatheSplineType splineType)
    {
        SplineType = splineType;
    }


    public GrPovRayLathe SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("lathe {")
            .IncreaseIndentation()
            .AppendAtNewLine(SplineTypeName)
            .AppendAtNewLine(Points.Count + ", ")
            .AppendAtNewLine(Points.GetPovRayCode())
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
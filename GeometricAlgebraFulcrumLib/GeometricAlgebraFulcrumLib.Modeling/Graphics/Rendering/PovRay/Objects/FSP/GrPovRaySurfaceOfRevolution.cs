using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRaySurfaceOfRevolution : 
    GrPovRayPolynomialObject, 
    IGrPovRayFiniteSolidObject,
    IReadOnlyList<GrPovRayVector2Value>
{
    public GrPovRayVector2List Points { get; } 
        = new GrPovRayVector2List();

    public int Count 
        => Points.Count;

    public GrPovRayVector2Value this[int index]
    {
        get => Points[index];
        set => Points[index] = value;
    }

    public bool Open { get; set; }
    

    internal GrPovRaySurfaceOfRevolution(bool open)
    {
        Open = open;
    }

    
    public GrPovRaySurfaceOfRevolution SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("sor {")
            .IncreaseIndentation()
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
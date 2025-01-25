using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;

public sealed class GrPovRayUnion :
    GrPovRayCsgObject,
    IReadOnlyList<IGrPovRayObject>
{
    public override GrPovRayCsgOperation CsgOperation 
        => GrPovRayCsgOperation.Union;

    public bool? SplitUnion { get; set; }

    public GrPovRayObjectList Objects { get; }
        = new GrPovRayObjectList();

    public int Count 
        => Objects.Count;

    public IGrPovRayObject this[int index]
    {
        get => Objects[index];
        set => Objects[index] = value;
    }

    internal GrPovRayUnion()
    {
    }

    
    public GrPovRayUnion SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }
    
    public override bool IsEmptyCodeElement()
    {
        return Objects.IsNullOrEmpty() &&
               Material.IsNullOrEmpty() &&
               Transform.IsNearIdentity();
    }

    protected override string GetObjectsCode()
    {
        return Objects.GetPovRayCode();
    }
    
    public override string GetPovRayCode()
    {
        var composer = new LinearTextComposer()
            .AppendLine(CsgOperationName + " {")
            .IncreaseIndentation()
            .AppendAtNewLine(GetObjectsCode());

        if (SplitUnion is not null)
            composer.AppendAtNewLine(
                "split_union " + (SplitUnion == true ? "on" : "off")
            );

        composer
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }

    public IEnumerator<IGrPovRayObject> GetEnumerator()
    {
        return Objects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
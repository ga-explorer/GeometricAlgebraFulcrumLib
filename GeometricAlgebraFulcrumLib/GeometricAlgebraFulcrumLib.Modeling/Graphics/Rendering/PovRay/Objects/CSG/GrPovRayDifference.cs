using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.CSG;

public sealed class GrPovRayDifference :
    GrPovRayCsgObject,
    IReadOnlyList<IGrPovRaySolidObject>
{
    public override GrPovRayCsgOperation CsgOperation 
        => GrPovRayCsgOperation.Difference;

    public GrPovRaySolidObjectList Objects { get; }
        = new GrPovRaySolidObjectList();

    public int Count 
        => Objects.Count;

    public IGrPovRaySolidObject this[int index]
    {
        get => Objects[index];
        set => Objects[index] = value;
    }

    internal GrPovRayDifference()
    {
    }

    
    public GrPovRayDifference SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }
    
    public override bool IsEmptyCodeElement()
    {
        return Objects.IsNullOrEmpty();
    }

    protected override string GetObjectsCode()
    {
        return Objects.GetPovRayCode();
    }
    
    public IEnumerator<IGrPovRaySolidObject> GetEnumerator()
    {
        return Objects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
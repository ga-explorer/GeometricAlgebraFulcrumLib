using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRayObjectList :
    GrPovRayCodeElementList<IGrPovRayObject>
{
    public override string Separator 
        => Environment.NewLine;


    public GrPovRayObjectList Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayObjectList Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    public GrPovRayObjectList Add(IGrPovRayObject st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayObjectList Append(IGrPovRayObject st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayObjectList Prepend(IGrPovRayObject st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }

    public GrPovRayObjectList Insert(IGrPovRayObject st, int index)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRaySolidObjectList :
    GrPovRayCodeElementList<IGrPovRaySolidObject>
{
    public override string Separator 
        => Environment.NewLine;


    public GrPovRaySolidObjectList Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRaySolidObjectList Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    public GrPovRaySolidObjectList Add(IGrPovRaySolidObject st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRaySolidObjectList Append(IGrPovRaySolidObject st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRaySolidObjectList Prepend(IGrPovRaySolidObject st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }

    public GrPovRaySolidObjectList Insert(IGrPovRaySolidObject st, int index)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
}
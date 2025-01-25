using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRayMediaList :
    GrPovRayCodeElementList<IGrPovRayMedia>
{
    public override string Separator 
        => Environment.NewLine;


    public GrPovRayMediaList Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayMediaList Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    public GrPovRayMediaList Add(IGrPovRayMedia st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayMediaList Append(IGrPovRayMedia st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayMediaList Prepend(IGrPovRayMedia st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }

    public GrPovRayMediaList Insert(IGrPovRayMedia st, int index)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
}
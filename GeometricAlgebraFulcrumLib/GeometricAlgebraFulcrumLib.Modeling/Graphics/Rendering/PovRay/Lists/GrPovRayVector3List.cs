using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRayVector3List :
    GrPovRayCodeElementList<GrPovRayVector3Value>
{
    public override string Separator 
        => ", ";
    

    public GrPovRayVector3List Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayVector3List Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    public GrPovRayVector3List Add(GrPovRayVector3Value st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayVector3List Append(GrPovRayVector3Value st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayVector3List Prepend(GrPovRayVector3Value st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }

    public GrPovRayVector3List Insert(GrPovRayVector3Value st, int index)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
}
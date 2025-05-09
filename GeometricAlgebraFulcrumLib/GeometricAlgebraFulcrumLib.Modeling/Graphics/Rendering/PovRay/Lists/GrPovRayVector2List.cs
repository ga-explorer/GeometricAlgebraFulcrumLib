using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRayVector2List :
    GrPovRayCodeElementList<GrPovRayVector2Value>
{
    public override string Separator 
        => ", ";


    public GrPovRayVector2List Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayVector2List Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    
    public GrPovRayVector2List Add(IPair<Float64Scalar> st)
    {
        CodeElementList.Add(GrPovRayVector2Value.Create(st));

        return this;
    }

    public GrPovRayVector2List Add(GrPovRayVector2Value st)
    {
        CodeElementList.Add(st);

        return this;
    }
    
    public GrPovRayVector2List Add(GrPovRayFloat32Value x, GrPovRayFloat32Value y)
    {
        return Add(GrPovRayVector2Value.Create(x, y));
    }
    

    public GrPovRayVector2List Append(IPair<Float64Scalar> st)
    {
        CodeElementList.Add(GrPovRayVector2Value.Create(st));

        return this;
    }

    public GrPovRayVector2List Append(GrPovRayVector2Value st)
    {
        CodeElementList.Add(st);

        return this;
    }
    
    public GrPovRayVector2List Append(GrPovRayFloat32Value x, GrPovRayFloat32Value y)
    {
        return Append(GrPovRayVector2Value.Create(x, y));
    }
    
    
    public GrPovRayVector2List Prepend(IPair<Float64Scalar> st)
    {
        CodeElementList.Insert(0, GrPovRayVector2Value.Create(st));

        return this;
    }

    public GrPovRayVector2List Prepend(GrPovRayVector2Value st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }
    
    public GrPovRayVector2List Prepend(GrPovRayFloat32Value x, GrPovRayFloat32Value y)
    {
        return Prepend(GrPovRayVector2Value.Create(x, y));
    }
    
    
    public GrPovRayVector2List Insert(int index, IPair<Float64Scalar> st)
    {
        CodeElementList.Insert(index, GrPovRayVector2Value.Create(st));

        return this;
    }

    public GrPovRayVector2List Insert(int index, GrPovRayVector2Value st)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
    
    public GrPovRayVector2List Insert(int index, GrPovRayFloat32Value x, GrPovRayFloat32Value y)
    {
        return Insert(index, GrPovRayVector2Value.Create(x, y));
    }

}
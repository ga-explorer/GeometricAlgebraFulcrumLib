using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public abstract class GrPovRayCodeElementList<T> :
    IReadOnlyList<T>,
    IGrPovRayCodeElement
    where T : IGrPovRayCodeElement
{
    protected readonly List<T> CodeElementList
        = new List<T>();
 
    public abstract string Separator { get; }

    public int Count
        => CodeElementList.Count;

    public T this[int index]
    {
        get => CodeElementList[index];
        set => CodeElementList[index] = value;
    }


    public bool Contains(T element)
    {
        return CodeElementList.Contains(element);
    }
    
    public bool IsEmptyCodeElement()
    {
        return CodeElementList.Count == 0;
    }

    public virtual string GetPovRayCode()
    {
        return CodeElementList.Select(
            v => v.GetPovRayCode()
        ).Concatenate(Environment.NewLine);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return CodeElementList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return GetPovRayCode();
    }
}
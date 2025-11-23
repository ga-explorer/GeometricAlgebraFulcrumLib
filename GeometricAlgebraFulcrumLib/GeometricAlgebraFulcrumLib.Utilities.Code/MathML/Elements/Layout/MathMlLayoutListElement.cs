using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout;

public abstract class MathMlLayoutListElement<T>
    : MathMlLayoutElement, IMathMlLayoutElement<T> where T : IMathMlElement
{
    protected readonly List<T> ContentsList 
        = new List<T>();


    public override IEnumerable<IMathMlElement> Contents
        => ContentsList.Cast<IMathMlElement>();

    public override string ContentsText
        => ContentsList
            .Select(e => e.ToString())
            .Concatenate(Environment.NewLine);

    public int Count 
        => ContentsList.Count;

    public T this[int index]
    {
        get => ContentsList[index];
        set => ContentsList[index] = value;
    }


    public MathMlLayoutListElement<T> Clear()
    {
        ContentsList.Clear();

        return this;
    }

    public MathMlLayoutListElement<T> Append(T element)
    {
        ContentsList.Add(element);

        return this;
    }

    public MathMlLayoutListElement<T> AppendElements(IEnumerable<T> elementsList)
    {
        ContentsList.AddRange(elementsList);

        return this;
    }

    public MathMlLayoutListElement<T> AppendElements(params T[] elementsList)
    {
        ContentsList.AddRange(elementsList);

        return this;
    }

    public MathMlLayoutListElement<T> Prepend(T element)
    {
        ContentsList.Insert(0, element);

        return this;
    }

    public MathMlLayoutListElement<T> PrependElements(IEnumerable<T> elementsList)
    {
        ContentsList.InsertRange(0, elementsList);

        return this;
    }

    public MathMlLayoutListElement<T> PrependElements(params T[] elementsList)
    {
        ContentsList.InsertRange(0, elementsList);

        return this;
    }

    public MathMlLayoutListElement<T> Insert(int index, T element)
    {
        ContentsList.Insert(index, element);

        return this;
    }

    public MathMlLayoutListElement<T> InsertElements(int index, IEnumerable<T> elementsList)
    {
        ContentsList.InsertRange(index, elementsList);

        return this;
    }

    public MathMlLayoutListElement<T> InsertElements(int index, params T[] elementsList)
    {
        ContentsList.InsertRange(index, elementsList);

        return this;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return ContentsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ContentsList.GetEnumerator();
    }
}
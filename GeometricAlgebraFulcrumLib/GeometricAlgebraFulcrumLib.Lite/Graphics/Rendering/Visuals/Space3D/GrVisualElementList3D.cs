using System.Collections;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D;

public sealed class GrVisualElementList3D :
    GrVisualElement3D,
    IGrVisualElementList3D
{
    private readonly Dictionary<string, IGrVisualElement3D> _visualElementList
        = new Dictionary<string, IGrVisualElement3D>();


    public int Count
        => _visualElementList.Count;

    public IGrVisualElement3D this[int index]
    {
        get
        {
            if (index < 0 || index >= _visualElementList.Count)
                throw new IndexOutOfRangeException();

            foreach (var item in _visualElementList.Values)
            {
                if (index == 0)
                    return item;

                index--;
            }

            throw new IndexOutOfRangeException();
        }
    }


    public GrVisualElementList3D(string name)
        : base(name)
    {

    }


    public override bool IsValid()
    {
        return _visualElementList.All(v =>
            v.Value.IsValid()
        );
    }

    public GrVisualElementList3D Clear()
    {
        _visualElementList.Clear();

        return this;
    }

    public GrVisualElementList3D Add(IGrVisualElement3D visualElement)
    {
        _visualElementList.Add(visualElement.Name, visualElement);

        return this;
    }

    public IEnumerator<IGrVisualElement3D> GetEnumerator()
    {
        return _visualElementList.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
using System.Collections;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry;

public sealed class XeoglGeometryBuilder : IReadOnlyList<XeoglGeometry>
{
    private readonly List<XeoglGeometry> _geometryItems = 
        new List<XeoglGeometry>();


    public int Count 
        => _geometryItems.Count;

    public XeoglGeometry this[int index]
    {
        get => _geometryItems[index];
        set => _geometryItems[index] = value ?? throw new ArgumentNullException(nameof(value));
    }


    public XeoglGeometryBuilder Add(XeoglGeometry geometryItem)
    {
        return this;
    }


    public IEnumerator<XeoglGeometry> GetEnumerator()
    {
        return _geometryItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _geometryItems.GetEnumerator();
    }
}
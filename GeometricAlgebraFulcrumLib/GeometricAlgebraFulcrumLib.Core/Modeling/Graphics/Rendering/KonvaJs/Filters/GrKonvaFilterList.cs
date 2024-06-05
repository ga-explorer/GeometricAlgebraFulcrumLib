using System.Collections;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Styles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Filters;

public class GrKonvaFilterList :
    GrKonvaShapeSubStyle,
    IReadOnlyList<GrKonvaFilter>
{
    private readonly List<GrKonvaFilter> _filterList 
        = new List<GrKonvaFilter>();


    public int Count 
        => _filterList.Count;

    public GrKonvaFilter this[int index] 
        => _filterList[index];
    

    public GrKonvaFilterList(GrKonvaShapeStyle parentStyle) 
        : base(parentStyle)
    {
    }


    public GrKonvaFilterList Clear()
    {
        _filterList.Clear();

        return this;
    }

    public GrKonvaFilterList Add(GrKonvaFilter filter)
    {
        var index = _filterList.FindIndex(
            f => f.FilterName == filter.FilterName
        );

        if (index >= 0)
            _filterList.RemoveAt(index);

        _filterList.Add(filter);

        return this;
    }

    public GrKonvaFilterList AddBlur(GrKonvaJsFloat32Value radius)
    {
        return Add(
            new GrKonvaFilterBlur(ParentStyle)
            {
                BlurRadius = radius
            }
        );
    }
    
    public GrKonvaFilterList AddBrighten(GrKonvaJsFloat32Value brightness)
    {
        return Add(
            new GrKonvaFilterBrighten(ParentStyle)
            {
                Brightness = brightness
            }
        );
    }
    
    public GrKonvaFilterList AddContrast(GrKonvaJsFloat32Value contrast)
    {
        return Add(
            new GrKonvaFilterContrast(ParentStyle)
            {
                Contrast = contrast
            }
        );
    }


    public IEnumerator<GrKonvaFilter> GetEnumerator()
    {
        return _filterList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
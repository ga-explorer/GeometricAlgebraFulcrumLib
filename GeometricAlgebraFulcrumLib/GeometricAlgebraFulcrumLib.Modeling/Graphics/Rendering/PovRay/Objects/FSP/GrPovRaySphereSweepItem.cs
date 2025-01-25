using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRaySphereList : 
    GrPovRayObject, 
    IGrPovRayFiniteSolidObject,
    IReadOnlyList<GrPovRaySphereListItem>
{
    private readonly List<GrPovRaySphereListItem> _itemList 
        = new List<GrPovRaySphereListItem>();

    public int Count 
        => _itemList.Count;

    public GrPovRaySphereListItem this[int index]
    {
        get => _itemList[index];
        set => _itemList[index] = value;
    }

    
    
    public GrPovRaySphereList Clear()
    {
        _itemList.Clear();

        return this;
    }

    public GrPovRaySphereList Remove(int index)
    {
        _itemList.RemoveAt(index);

        return this;
    }
    
    public GrPovRaySphereList Add(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        _itemList.Add(
            new GrPovRaySphereListItem(center, radius)
        );

        return this;
    }

    public GrPovRaySphereList Append(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        _itemList.Add(
            new GrPovRaySphereListItem(center, radius)
        );

        return this;
    }
    
    public GrPovRaySphereList Prepend(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        _itemList.Insert(
            0,
            new GrPovRaySphereListItem(center, radius)
        );

        return this;
    }
    
    public GrPovRaySphereList Insert(int index, GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        _itemList.Insert(
            index,
            new GrPovRaySphereListItem(center, radius)
        );

        return this;
    }
    
    public override string GetPovRayCode()
    {
        return _itemList.Select(
            item => $"{item.Center.GetPovRayCode()}, {item.Radius.GetPovRayCode()}"
        ).Concatenate("," + Environment.NewLine);
    }
    
    public IEnumerator<GrPovRaySphereListItem> GetEnumerator()
    {
        return _itemList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}

public sealed record GrPovRaySphereListItem
{
    public GrPovRayVector3Value Center { get; }

    public GrPovRayFloat32Value Radius { get; }


    internal GrPovRaySphereListItem(GrPovRayVector3Value center, GrPovRayFloat32Value radius)
    {
        Center = center;
        Radius = radius;
    }
}
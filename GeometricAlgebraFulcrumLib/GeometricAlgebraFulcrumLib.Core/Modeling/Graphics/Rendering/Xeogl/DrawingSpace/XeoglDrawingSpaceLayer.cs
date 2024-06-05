using System.Collections;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry.Primitives;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Materials;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Objects;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Transforms;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.DrawingSpace;

/// <summary>
/// This class represents a single layer in a 3D xeogl drawing space.
/// Internally, a layer is either just a single xeogl mesh, or a xeogl mesh group.
/// </summary>
public sealed class XeoglDrawingSpaceLayer
    : IReadOnlyCollection<XeoglMesh>
{
    private readonly List<XeoglMesh> _contentsList
        = new List<XeoglMesh>();


    public XeoglDrawingSpace ParentDrawingSpace { get; }

    public int Count 
        => _contentsList.Count;

    public XeoglMaterial DefaultMaterial { get; private set; }

    public IXeoglTransform DefaultTransform { get; private set; }

    public XeoglGeometry DefaultGeometry { get; private set; }

    public string LayerName { get; set; }

    public string LayerDescription { get; set; }

    public bool IsVisible { get; set; } 
        = true;

    public bool IsEmpty 
        => _contentsList.Count == 0;


    internal XeoglDrawingSpaceLayer(XeoglDrawingSpace parentDrawingSpace, string layerName)
    {
        ParentDrawingSpace = parentDrawingSpace;

        LayerName = layerName;
    }


    public XeoglDrawingSpaceLayer Clear()
    {
        _contentsList.Clear();

        return this;
    }

    public XeoglDrawingSpaceLayer Show()
    {
        IsVisible = true;

        return this;
    }

    public XeoglDrawingSpaceLayer Hide()
    {
        IsVisible = true;

        return this;
    }

    public XeoglObject GetXeoglObject()
    {
        if (_contentsList.Count == 0)
            return null;

        if (_contentsList.Count == 1)
            return _contentsList[0];

        var xeoglGroup = new XeoglObjectsGroup();

        xeoglGroup.AddChildren(_contentsList);

        return xeoglGroup;
    }


    public XeoglDrawingSpaceLayer SetGeometry(XeoglGeometry geometry)
    {
        DefaultGeometry = geometry;

        return this;
    }

    public XeoglDrawingSpaceLayer SetCodeGeometry(string geometryCode)
    {
        if (!string.IsNullOrEmpty(geometryCode))
            DefaultGeometry = XeoglCodeGeometry.Create(geometryCode);

        return this;
    }

    public XeoglDrawingSpaceLayer SetStoredGeometry(string geometryVariableName)
    {
        if (ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            DefaultGeometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        return this;
    }


    public XeoglDrawingSpaceLayer SetTransform(IXeoglTransform transform)
    {
        DefaultTransform = transform;

        return this;
    }

    public XeoglDrawingSpaceLayer SetTransform(SquareMatrix4 transformMatrix)
    {
        DefaultTransform = transformMatrix.ToXeoglTransform();

        return this;
    }

    public XeoglDrawingSpaceLayer SetTransform(IAffineMap3D affineMap)
    {
        DefaultTransform = affineMap.GetSquareMatrix4().ToXeoglTransform();

        return this;
    }

    public XeoglDrawingSpaceLayer SetTransform(RotateScaleTranslateMap3D affineMap)
    {
        DefaultTransform = affineMap.ToXeoglTransform();

        return this;
    }


    public XeoglDrawingSpaceLayer SetMaterial(XeoglMaterial material)
    {
        DefaultMaterial = material;

        return this;
    }

    public XeoglDrawingSpaceLayer SetCodeMaterial(string codeMaterial)
    {
        if (!string.IsNullOrEmpty(codeMaterial))
            DefaultMaterial = new XeoglCodeMaterial(codeMaterial);

        return this;
    }

    public XeoglDrawingSpaceLayer SetStoredMaterial(string materialVariableName)
    {
        if (ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            DefaultMaterial = new XeoglCodeMaterial(materialVariableName + "Material");

        return this;
    }

    public XeoglDrawingSpaceLayer SetEmissiveMaterial(Color color)
    {
        return SetMaterial(
            new XeoglPhongMaterial()
            {
                EmissiveColor = color
            }
        );
    }


    public XeoglDrawingSpaceLayer DrawMesh(XeoglMesh xeoglMesh)
    {
        if (!ReferenceEquals(xeoglMesh, null))
            _contentsList.Add(xeoglMesh);

        return this;
    }


    public XeoglDrawingSpaceLayer DrawDefaultGeometry()
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(DefaultTransform, DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawDefaultGeometry(IXeoglTransform transform)
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(transform, DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawDefaultGeometry(string materialVariableName)
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(DefaultTransform, material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawDefaultGeometry(XeoglMaterial material)
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(DefaultTransform, material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawDefaultGeometry(IXeoglTransform transform, string materialVariableName)
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(transform, material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawDefaultGeometry(IXeoglTransform transform, XeoglMaterial material)
    {
        if (ReferenceEquals(DefaultGeometry, null))
            return this;

        _contentsList.Add(
            DefaultGeometry.ToXeoglMesh(transform, material)
        );

        return this;
    }


    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        _contentsList.Add(
            geometry.ToXeoglMesh(DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(params XeoglGeometry[] geometryList)
    {
        foreach (var geometry in geometryList)
        {
            if (ReferenceEquals(geometry, null))
                continue;

            _contentsList.Add(
                geometry.ToXeoglMesh(DefaultTransform, DefaultMaterial)
            );
        }

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(IEnumerable<XeoglGeometry> geometryList)
    {
        foreach (var geometry in geometryList)
        {
            if (ReferenceEquals(geometry, null))
                continue;

            _contentsList.Add(
                geometry.ToXeoglMesh(DefaultTransform, DefaultMaterial)
            );
        }

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry, IXeoglTransform transform)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry, string materialVariableName)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            geometry.ToXeoglMesh(material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry, XeoglMaterial material)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        if (ReferenceEquals(material, null))
            return this;

        _contentsList.Add(
            geometry.ToXeoglMesh(material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry, IXeoglTransform transform, string materialVariableName)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometry(XeoglGeometry geometry, IXeoglTransform transform, XeoglMaterial material)
    {
        if (ReferenceEquals(geometry, null))
            return this;

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, material)
        );

        return this;
    }


    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        _contentsList.Add(
            geometry.ToXeoglMesh(DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, IXeoglTransform transform)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, DefaultMaterial)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, string materialVariableName)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            geometry.ToXeoglMesh(material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, XeoglMaterial material)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        if (ReferenceEquals(material, null))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        _contentsList.Add(
            geometry.ToXeoglMesh(material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, IXeoglTransform transform, string materialVariableName)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        var material = new XeoglCodeMaterial(materialVariableName + "Material");

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, material)
        );

        return this;
    }

    public XeoglDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, IXeoglTransform transform, XeoglMaterial material)
    {
        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
            return this;

        if (ReferenceEquals(material, null))
            return this;

        var geometry = new XeoglCodeGeometry(geometryVariableName + "Geometry");

        _contentsList.Add(
            geometry.ToXeoglMesh(transform, material)
        );

        return this;
    }


    public XeoglDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength)
    {
        var geometryArray = _contentsList
            .Select(m => m.Geometry as XeoglTrianglesGeometry)
            .Where(g => !ReferenceEquals(g, null))
            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
            .ToArray();

        foreach (var geometry in geometryArray)
            DrawGeometry(geometry);

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength, string materialVariableName)
    {
        var geometryArray = _contentsList
            .Select(m => m.Geometry as XeoglTrianglesGeometry)
            .Where(g => !ReferenceEquals(g, null))
            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
            .ToArray();

        foreach (var geometry in geometryArray)
            DrawGeometry(geometry, materialVariableName);

        return this;
    }

    public XeoglDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength, XeoglMaterial material)
    {
        var geometryArray = _contentsList
            .Select(m => m.Geometry as XeoglTrianglesGeometry)
            .Where(g => !ReferenceEquals(g, null))
            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
            .ToArray();

        foreach (var geometry in geometryArray)
            DrawGeometry(geometry, material);

        return this;
    }


    public IEnumerator<XeoglMesh> GetEnumerator()
    {
        return _contentsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _contentsList.GetEnumerator();
    }
}
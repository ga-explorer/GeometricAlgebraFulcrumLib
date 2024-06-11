namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.DrawingSpace;

///// <summary>
///// This class represents a single layer in a 3D xeogl drawing space.
///// Internally, a layer is either just a single xeogl mesh, or a xeogl mesh group.
///// </summary>
//public sealed class TjDrawingSpaceLayer
//    : IReadOnlyCollection<TjMesh>
//{
//    private readonly List<TjMesh> _contentsList
//        = new List<TjMesh>();


//    public TjDrawingSpace ParentDrawingSpace { get; }

//    public int Count 
//        => _contentsList.Count;

//    public TjMaterial DefaultMaterial { get; private set; }

//    public ITjTransform DefaultTransform { get; private set; }

//    public TjGeometry DefaultGeometry { get; private set; }

//    public string LayerName { get; set; }

//    public string LayerDescription { get; set; }

//    public bool IsVisible { get; set; } 
//        = true;

//    public bool IsEmpty 
//        => _contentsList.Count == 0;


//    internal TjDrawingSpaceLayer(TjDrawingSpace parentDrawingSpace, string layerName)
//    {
//        ParentDrawingSpace = parentDrawingSpace;

//        LayerName = layerName;
//    }


//    public TjDrawingSpaceLayer Clear()
//    {
//        _contentsList.Clear();

//        return this;
//    }

//    public TjDrawingSpaceLayer Show()
//    {
//        IsVisible = true;

//        return this;
//    }

//    public TjDrawingSpaceLayer Hide()
//    {
//        IsVisible = true;

//        return this;
//    }

//    public TjObject3D GetTjObject()
//    {
//        if (_contentsList.Count == 0)
//            return null;

//        if (_contentsList.Count == 1)
//            return _contentsList[0];

//        var xeoglGroup = new TjObjectsGroup();

//        xeoglGroup.AddChildren(_contentsList);

//        return xeoglGroup;
//    }


//    public TjDrawingSpaceLayer SetGeometry(TjGeometry geometry)
//    {
//        DefaultGeometry = geometry;

//        return this;
//    }

//    public TjDrawingSpaceLayer SetCodeGeometry(string geometryCode)
//    {
//        if (!string.IsNullOrEmpty(geometryCode))
//            DefaultGeometry = TjCodeGeometry.Create(geometryCode);

//        return this;
//    }

//    public TjDrawingSpaceLayer SetStoredGeometry(string geometryVariableName)
//    {
//        if (ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            DefaultGeometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        return this;
//    }


//    public TjDrawingSpaceLayer SetTransform(ITjTransform transform)
//    {
//        DefaultTransform = transform;

//        return this;
//    }

//    public TjDrawingSpaceLayer SetTransform(Matrix4X4 transformMatrix)
//    {
//        DefaultTransform = transformMatrix.ToTjTransform();

//        return this;
//    }

//    public TjDrawingSpaceLayer SetTransform(IAffineMap3D affineMap)
//    {
//        DefaultTransform = affineMap.ToMatrix().ToTjTransform();

//        return this;
//    }

//    public TjDrawingSpaceLayer SetTransform(RotateScaleTranslateMap3D affineMap)
//    {
//        DefaultTransform = affineMap.ToTjTransform();

//        return this;
//    }


//    public TjDrawingSpaceLayer SetMaterial(TjMaterial material)
//    {
//        DefaultMaterial = material;

//        return this;
//    }

//    public TjDrawingSpaceLayer SetCodeMaterial(string codeMaterial)
//    {
//        if (!string.IsNullOrEmpty(codeMaterial))
//            DefaultMaterial = new TjCodeMaterial(codeMaterial);

//        return this;
//    }

//    public TjDrawingSpaceLayer SetStoredMaterial(string materialVariableName)
//    {
//        if (ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            DefaultMaterial = new TjCodeMaterial(materialVariableName + "Material");

//        return this;
//    }

//    public TjDrawingSpaceLayer SetEmissiveMaterial(Color color)
//    {
//        return SetMaterial(
//            new TjPhongMaterial()
//            {
//                EmissiveColor = color
//            }
//        );
//    }


//    public TjDrawingSpaceLayer DrawMesh(TjMesh xeoglMesh)
//    {
//        if (!ReferenceEquals(xeoglMesh, null))
//            _contentsList.Add(xeoglMesh);

//        return this;
//    }


//    public TjDrawingSpaceLayer DrawDefaultGeometry()
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(DefaultTransform, DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawDefaultGeometry(ITjTransform transform)
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(transform, DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawDefaultGeometry(string materialVariableName)
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(DefaultTransform, material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawDefaultGeometry(TjMaterial material)
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(DefaultTransform, material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawDefaultGeometry(ITjTransform transform, string materialVariableName)
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawDefaultGeometry(ITjTransform transform, TjMaterial material)
//    {
//        if (ReferenceEquals(DefaultGeometry, null))
//            return this;

//        _contentsList.Add(
//            DefaultGeometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }


//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        _contentsList.Add(
//            geometry.ToTjMesh(DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(params TjGeometry[] geometryList)
//    {
//        foreach (var geometry in geometryList)
//        {
//            if (ReferenceEquals(geometry, null))
//                continue;

//            _contentsList.Add(
//                geometry.ToTjMesh(DefaultTransform, DefaultMaterial)
//            );
//        }

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(IEnumerable<TjGeometry> geometryList)
//    {
//        foreach (var geometry in geometryList)
//        {
//            if (ReferenceEquals(geometry, null))
//                continue;

//            _contentsList.Add(
//                geometry.ToTjMesh(DefaultTransform, DefaultMaterial)
//            );
//        }

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry, ITjTransform transform)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry, string materialVariableName)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            geometry.ToTjMesh(material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry, TjMaterial material)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        if (ReferenceEquals(material, null))
//            return this;

//        _contentsList.Add(
//            geometry.ToTjMesh(material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry, ITjTransform transform, string materialVariableName)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometry(TjGeometry geometry, ITjTransform transform, TjMaterial material)
//    {
//        if (ReferenceEquals(geometry, null))
//            return this;

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }


//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        _contentsList.Add(
//            geometry.ToTjMesh(DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, ITjTransform transform)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, DefaultMaterial)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, string materialVariableName)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            geometry.ToTjMesh(material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, TjMaterial material)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        if (ReferenceEquals(material, null))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        _contentsList.Add(
//            geometry.ToTjMesh(material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, ITjTransform transform, string materialVariableName)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        if (!ParentDrawingSpace.ContainsStoredMaterial(materialVariableName))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        var material = new TjCodeMaterial(materialVariableName + "Material");

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawStoredGeometry(string geometryVariableName, ITjTransform transform, TjMaterial material)
//    {
//        if (!ParentDrawingSpace.ContainsStoredGeometry(geometryVariableName))
//            return this;

//        if (ReferenceEquals(material, null))
//            return this;

//        var geometry = new TjCodeGeometry(geometryVariableName + "Geometry");

//        _contentsList.Add(
//            geometry.ToTjMesh(transform, material)
//        );

//        return this;
//    }


//    public TjDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength)
//    {
//        var geometryArray = _contentsList
//            .Select(m => m.Geometry as TjTrianglesGeometry)
//            .Where(g => !ReferenceEquals(g, null))
//            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
//            .ToArray();

//        foreach (var geometry in geometryArray)
//            DrawGeometry(geometry);

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength, string materialVariableName)
//    {
//        var geometryArray = _contentsList
//            .Select(m => m.Geometry as TjTrianglesGeometry)
//            .Where(g => !ReferenceEquals(g, null))
//            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
//            .ToArray();

//        foreach (var geometry in geometryArray)
//            DrawGeometry(geometry, materialVariableName);

//        return this;
//    }

//    public TjDrawingSpaceLayer DrawGeometryNormals(double normalSegmentsLength, TjMaterial material)
//    {
//        var geometryArray = _contentsList
//            .Select(m => m.Geometry as TjTrianglesGeometry)
//            .Where(g => !ReferenceEquals(g, null))
//            .Select(g => g.GetNormalLinesGeometry(normalSegmentsLength))
//            .ToArray();

//        foreach (var geometry in geometryArray)
//            DrawGeometry(geometry, material);

//        return this;
//    }


//    public IEnumerator<TjMesh> GetEnumerator()
//    {
//        return _contentsList.GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return _contentsList.GetEnumerator();
//    }
//}
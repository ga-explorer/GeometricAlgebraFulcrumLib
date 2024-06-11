using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Fog;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Math;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Objects;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Scenes;

/// <summary>
/// Scenes allow you to set up what and where is to be rendered by three.js.
/// This is where you place objects, lights and cameras.
/// https://threejs.org/docs/#api/en/scenes/Scene
/// </summary>
public class TjScene :
    TjComponentWithAttributes,
    IReadOnlyCollection<TjObject3D>
{
    private readonly List<TjObject3D> _objectsList
        = new List<TjObject3D>();


    public override string JavaScriptClassName 
        => "Scene";

    public int Count 
        => _objectsList.Count;

    public bool AutoUpdate { get; set; }

    public ITjSceneBackgroundObject Background { get; set; }

    public TjFog Fog { get; private set; }

    public TjMaterialBase OverrideMaterial { get; set; }


    public TjScene Reset()
    {
        _objectsList.Clear();

        return this;
    }

    public TjScene SetLinearFog(TjColor color, double nearDistance, double farDistance)
    {
        Fog = new TjLinearFog()
        {
            Color = color,
            NearDistance = nearDistance,
            FarDistance = farDistance,
            Description = "Linear Fog"
        };

        return this;
    }

    public TjScene SetExponentialSquaredFog(TjColor color, double density)
    {
        Fog = new TjExponentialSquaredFog()
        {
            Color = color,
            Density = density,
            Description = "Exponential Squared Fog"
        };

        return this;
    }

    public TjScene RemoveFog()
    {
        Fog = null;

        return this;
    }

    public TjScene Add(TjObject3D sceneObject)
    {
        sceneObject.ParentScene = this;
        _objectsList.Add(sceneObject);

        return this;
    }

    public TjScene Add(params TjObject3D[] sceneObjectList)
    {
        foreach (var sceneObject in sceneObjectList)
        {
            sceneObject.ParentScene = this;
            _objectsList.Add(sceneObject);
        }

        return this;
    }

    public TjScene Add(IEnumerable<TjObject3D> sceneObjectList)
    {
        foreach (var sceneObject in sceneObjectList)
        {
            sceneObject.ParentScene = this;
            _objectsList.Add(sceneObject);
        }

        return this;
    }

    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("autoUpdate", AutoUpdate, true)
            .SetTextValue("background", Background.ToString(), string.Empty)
            .SetTextValue("overrideMaterial", OverrideMaterial.ToString(), string.Empty)
            ;
    }
        

    public IEnumerator<TjObject3D> GetEnumerator()
    {
        return _objectsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
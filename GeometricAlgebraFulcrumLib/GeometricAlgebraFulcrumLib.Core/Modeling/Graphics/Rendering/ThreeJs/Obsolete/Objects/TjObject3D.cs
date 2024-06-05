using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Scenes;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Objects;

public abstract class TjObject3D :
    TjComponentWithAttributes
{
    public TjScene ParentScene { get; internal set; }

    public string Name { get; set; }

    public bool Visible { get; set; }
        = true;

    public bool CastShadow { get; set; }

    public bool ReceiveShadow { get; set; }

    public bool FrustumCulled { get; set; }
        = true;

    public int RenderOrder { get; set; }

    public ILinFloat64Vector3D Position { get; set; }
        = LinFloat64Vector3D.Zero;

    public ILinFloat64Vector3D UpDirection { get; set; }
        = LinFloat64Vector3D.E2;

    public int LayerMask { get; set; } 
        = 1;



    protected TjObject3D()
    {

    }

    protected TjObject3D(TjScene parentScene)
    {
        ParentScene = parentScene;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("visible", Visible, true)
            .SetValue("castShadow", CastShadow, false)
            .SetValue("receiveShadow", ReceiveShadow, false)
            .SetValue("frustumCulled", FrustumCulled, true)
            .SetValue("renderOrder", RenderOrder, 0)
            .SetValue("layers.mask", LayerMask, 1)
            .SetThreeJsVector3Value("position", Position, LinFloat64Vector3D.Zero)
            .SetThreeJsVector3Value("up", UpDirection, LinFloat64Vector3D.E2)
            .SetTextValue("name", Name, string.Empty);
    }
}
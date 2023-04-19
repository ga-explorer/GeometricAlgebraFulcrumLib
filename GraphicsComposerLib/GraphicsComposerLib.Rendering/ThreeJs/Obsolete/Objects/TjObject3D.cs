using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Scenes;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Objects
{
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

        public IFloat64Tuple3D Position { get; set; }
            = Float64Tuple3D.Zero;

        public IFloat64Tuple3D UpDirection { get; set; }
            = Float64Tuple3D.E2;

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
                .SetThreeJsVector3Value("position", Position, Float64Tuple3D.Zero)
                .SetThreeJsVector3Value("up", UpDirection, Float64Tuple3D.E2)
                .SetTextValue("name", Name, string.Empty);
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Scenes;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
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

        public ITuple3D Position { get; set; }
            = Tuple3D.Zero;

        public ITuple3D UpDirection { get; set; }
            = Tuple3D.E2;

        public int LayerMask { get; set; } 
            = 1;



        protected TjObject3D()
        {

        }

        protected TjObject3D([NotNull] TjScene parentScene)
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
                .SetThreeJsVector3Value("position", Position, Tuple3D.Zero)
                .SetThreeJsVector3Value("up", UpDirection, Tuple3D.E2)
                .SetTextValue("name", Name, string.Empty);
        }
    }
}

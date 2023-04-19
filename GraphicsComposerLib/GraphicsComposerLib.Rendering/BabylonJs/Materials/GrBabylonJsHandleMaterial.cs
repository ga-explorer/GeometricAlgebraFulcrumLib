using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/classes/BABYLON.HandleMaterial
    /// </summary>
    public sealed class GrBabylonJsHandleMaterial :
        GrBabylonJsMaterial
    {
        public sealed class HandleMaterialProperties :
            MaterialProperties
        {
            public GrBabylonJsFloat32Value? AnimationLength { get; set; }

            public GrBabylonJsColor3Value? BaseColor { get; set; }
        
            public GrBabylonJsFloat32Value? BaseScale { get; set; }
        
            public GrBabylonJsFloat32Value? DragScale { get; set; }

            public GrBabylonJsColor3Value? HoverColor { get; set; }
        
            public GrBabylonJsFloat32Value? HoverScale { get; set; }

            public GrBabylonJsFloat32Value? Hover { get; set; }

            public GrBabylonJsFloat32Value? Drag { get; set; }
        

            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return AnimationLength.GetNameValueCodePair("animationLength");
                yield return BaseColor.GetNameValueCodePair("baseColor");
                yield return BaseScale.GetNameValueCodePair("baseScale");
                yield return DragScale.GetNameValueCodePair("dragScale");
                yield return HoverColor.GetNameValueCodePair("hoverColor");
                yield return HoverScale.GetNameValueCodePair("hoverScale");
                yield return Hover.GetNameValueCodePair("hover");
                yield return Drag.GetNameValueCodePair("drag");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.HandleMaterial";

        public HandleMaterialProperties? Properties { get; private set; }
            = new HandleMaterialProperties();
    
        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsHandleMaterial(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsHandleMaterial(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsHandleMaterial SetProperties(HandleMaterialProperties properties)
        {
            Properties = properties;

            return this;
        }
    }
}
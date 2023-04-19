using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
    public abstract class GrBabylonJsLinesMesh :
        GrBabylonJsObject
    {
        public sealed class LinesMeshProperties :
            GrBabylonJsMesh.MeshProperties
        {
            public GrBabylonJsColor3Value? Color { get; set; }

            public GrBabylonJsFloat32Value? Alpha { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return Alpha.GetNameValueCodePair("alpha");
                yield return Color.GetNameValueCodePair("color");
            }
        }


        public GrBabylonJsSceneValue ParentScene { get; set; }

        public string SceneVariableName 
            => ParentScene.Value.ConstName;

        public LinesMeshProperties? Properties { get; protected set; }
            = new LinesMeshProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;
    

        protected GrBabylonJsLinesMesh(string constName) 
            : base(constName)
        {
        }
    
        protected GrBabylonJsLinesMesh(string constName, GrBabylonJsSceneValue scene) 
            : base(constName)
        {
            ParentScene = scene;
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();

            var optionsCode = 
                ObjectOptions is null 
                    ? "{}" 
                    : ObjectOptions.GetCode();

            yield return optionsCode;

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return ParentScene.Value.ConstName;
        }
    }
}
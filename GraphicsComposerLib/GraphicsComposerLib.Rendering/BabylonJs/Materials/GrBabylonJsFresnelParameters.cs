using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials
{
    public sealed class GrBabylonJsFresnelParameters :
        GrBabylonJsObject
    {
        public sealed class FresnelParametersOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsFloat32Value? Bias { get; set; }

            public GrBabylonJsBooleanValue? IsEnabled { get; set; }

            public GrBabylonJsColor3Value? LeftColor { get; set; }

            public GrBabylonJsColor3Value? RightColor { get; set; }

            public GrBabylonJsFloat32Value? Power { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Bias.GetNameValueCodePair("bias");
                yield return IsEnabled.GetNameValueCodePair("isEnabled");
                yield return LeftColor.GetNameValueCodePair("leftColor");
                yield return RightColor.GetNameValueCodePair("rightColor");
                yield return Power.GetNameValueCodePair("power");
            }
        }

        public class FresnelParametersProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsFloat32Value? Bias { get; set; }

            public GrBabylonJsBooleanValue? IsEnabled { get; set; }

            public GrBabylonJsColor3Value? LeftColor { get; set; }

            public GrBabylonJsColor3Value? RightColor { get; set; }

            public GrBabylonJsFloat32Value? Power { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Bias.GetNameValueCodePair("bias");
                yield return IsEnabled.GetNameValueCodePair("isEnabled");
                yield return LeftColor.GetNameValueCodePair("leftColor");
                yield return RightColor.GetNameValueCodePair("rightColor");
                yield return Power.GetNameValueCodePair("power");
            }
        }

        protected override string ConstructorName
            => "new BABYLON.FresnelParameters";

        public FresnelParametersOptions? Options { get; private set; }
            = new FresnelParametersOptions();

        public FresnelParametersProperties? Properties { get; private set; }
            = new FresnelParametersProperties();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;
    
    
        public GrBabylonJsFresnelParameters(string constName) 
            : base(constName)
        {
        }


        public GrBabylonJsFresnelParameters SetOptions(FresnelParametersOptions options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsFresnelParameters SetProperties(FresnelParametersProperties properties)
        {
            Properties = properties;

            return this;
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            var optionsCode = 
                ObjectOptions is null 
                    ? "{}" 
                    : ObjectOptions.GetCode();

            yield return optionsCode;
        }
    }
}
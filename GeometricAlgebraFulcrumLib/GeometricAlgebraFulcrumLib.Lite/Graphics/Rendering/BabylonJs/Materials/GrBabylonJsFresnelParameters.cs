using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Materials
{
    public sealed class GrBabylonJsFresnelParameters :
        GrBabylonJsObject
    {
        public sealed class FresnelParametersOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsFloat32Value? Bias
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bias");
                set => SetAttributeValue("bias", value);
            }

            public GrBabylonJsBooleanValue? IsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
                set => SetAttributeValue("isEnabled", value);
            }

            public GrBabylonJsColor3Value? LeftColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("leftColor");
                set => SetAttributeValue("leftColor", value);
            }

            public GrBabylonJsColor3Value? RightColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("rightColor");
                set => SetAttributeValue("rightColor", value);
            }

            public GrBabylonJsFloat32Value? Power
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("power");
                set => SetAttributeValue("power", value);
            }
            

            public FresnelParametersOptions()
            {
            }

            public FresnelParametersOptions(FresnelParametersOptions options)
            {
                SetAttributeValues(options);
            }
        }

        public class FresnelParametersProperties :
            GrBabylonJsObjectProperties
        {
            public GrBabylonJsFloat32Value? Bias
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("bias");
                set => SetAttributeValue("bias", value);
            }

            public GrBabylonJsBooleanValue? IsEnabled
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isEnabled");
                set => SetAttributeValue("isEnabled", value);
            }

            public GrBabylonJsColor3Value? LeftColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("leftColor");
                set => SetAttributeValue("leftColor", value);
            }

            public GrBabylonJsColor3Value? RightColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("rightColor");
                set => SetAttributeValue("rightColor", value);
            }

            public GrBabylonJsFloat32Value? Power
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("power");
                set => SetAttributeValue("power", value);
            }
            

            public FresnelParametersProperties()
            {
            }

            public FresnelParametersProperties(FresnelParametersProperties options)
            {
                SetAttributeValues(options);
            }
        }

        protected override string ConstructorName
            => "new BABYLON.FresnelParameters";

        public FresnelParametersOptions Options { get; private set; }
            = new FresnelParametersOptions();

        public FresnelParametersProperties Properties { get; private set; }
            = new FresnelParametersProperties();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;
    
    
        public GrBabylonJsFresnelParameters(string constName) 
            : base(constName)
        {
        }


        public GrBabylonJsFresnelParameters SetOptions(FresnelParametersOptions options)
        {
            Options = new FresnelParametersOptions(options);

            return this;
        }

        public GrBabylonJsFresnelParameters SetProperties(FresnelParametersProperties properties)
        {
            Properties = new FresnelParametersProperties(properties);

            return this;
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            var optionsCode = 
                ObjectOptions.Count == 0
                    ? "{}" 
                    : ObjectOptions.GetCode();

            yield return optionsCode;
        }
    }
}
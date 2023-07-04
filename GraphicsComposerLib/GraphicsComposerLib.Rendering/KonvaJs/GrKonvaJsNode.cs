using GraphicsComposerLib.Rendering.KonvaJs.Values;

namespace GraphicsComposerLib.Rendering.KonvaJs;

/// <summary>
/// https://konvajs.org/api/Konva.Node.html
/// </summary>
public abstract class GrKonvaJsNode :
    GrKonvaJsObject
{
    private static ulong _nodeIdCounter;

    private static string CreateNodeId()
    {
        var nodeId = $"konvaNode{_nodeIdCounter:x4}";

        _nodeIdCounter++;

        return nodeId;
    }


    public abstract class GrKonvaJsNodeProperties :
        GrKonvaJsObjectProperties
    {
        public GrKonvaJsStringValue? Id
        {
            get => GetAttributeValueOrNull<GrKonvaJsStringValue>("id");
            set => SetAttributeValue("id", value);
        }

        public GrKonvaJsStringValue? Name
        {
            get => GetAttributeValueOrNull<GrKonvaJsStringValue>("name");
            set => SetAttributeValue("name", value);
        }

        public GrKonvaJsVector2Value? AbsolutePosition
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("absolutePosition");
            set => SetAttributeValue("absolutePosition", value);
        }

        public GrKonvaJsFloat32Value? Alpha
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("alpha");
            set => SetAttributeValue("alpha", value);
        }
        
        public GrKonvaJsFloat32Value? Red
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("red");
            set => SetAttributeValue("red", value);
        }

        public GrKonvaJsFloat32Value? Green
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("green");
            set => SetAttributeValue("green", value);
        }

        public GrKonvaJsFloat32Value? Blue
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("blue");
            set => SetAttributeValue("blue", value);
        }

        public GrKonvaJsFloat32Value? Hue
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("hue");
            set => SetAttributeValue("hue", value);
        }

        public GrKonvaJsFloat32Value? Luminance
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("luminance");
            set => SetAttributeValue("luminance", value);
        }
        
        public GrKonvaJsFloat32Value? BlurRadius
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("BlurRadius");
            set => SetAttributeValue("BlurRadius", value);
        }

        public GrKonvaJsFloat32Value? Brightness
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Brightness");
            set => SetAttributeValue("Brightness", value);
        }
        
        public GrKonvaJsFloat32Value? Contrast
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Contrast");
            set => SetAttributeValue("Contrast", value);
        }
        
        public GrKonvaJsBooleanValue? Draggable
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Draggable");
            set => SetAttributeValue("Draggable", value);
        }

        public GrKonvaJsFloat32Value? DragDistance
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("DragDistance");
            set => SetAttributeValue("DragDistance", value);
        }

        public GrKonvaJsCodeValue? DragBoundFunc
        {
            get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("DragBoundFunc");
            set => SetAttributeValue("DragBoundFunc", value);
        }
        
        public GrKonvaJsBooleanValue? EmbossBlend
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("EmbossBlend");
            set => SetAttributeValue("EmbossBlend", value);
        }

        public GrKonvaJsFloat32Value? EmbossStrength
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("EmbossStrength");
            set => SetAttributeValue("EmbossStrength", value);
        }
        
        public GrKonvaJsFloat32Value? EmbossWhiteLevel
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("EmbossWhiteLevel");
            set => SetAttributeValue("EmbossWhiteLevel", value);
        }
        
        public GrKonvaJsEmbossDirectionValue? EmbossDirection
        {
            get => GetAttributeValueOrNull<GrKonvaJsEmbossDirectionValue>("EmbossDirection");
            set => SetAttributeValue("EmbossDirection", value);
        }
        
        public GrKonvaJsFloat32Value? Enhance
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Enhance");
            set => SetAttributeValue("Enhance", value);
        }
        
        public GrKonvaJsFloat32Value? OffsetX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetX");
            set => SetAttributeValue("OffsetX", value);
        }
        
        public GrKonvaJsFloat32Value? OffsetY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("OffsetY");
            set => SetAttributeValue("OffsetY", value);
        }

        public GrKonvaJsFloat32Value? Width
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Width");
            set => SetAttributeValue("Width", value);
        }

        public GrKonvaJsFloat32Value? Height
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Height");
            set => SetAttributeValue("Height", value);
        }

        public GrKonvaJsInt32Value? KaleidoscopeAngle
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("KaleidoscopeAngle");
            set => SetAttributeValue("KaleidoscopeAngle", value);
        }
        
        public GrKonvaJsInt32Value? KaleidoscopePower
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("KaleidoscopePower");
            set => SetAttributeValue("KaleidoscopePower", value);
        }
        
        public GrKonvaJsFloat32Value? Levels
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Levels");
            set => SetAttributeValue("Levels", value);
        }

        public GrKonvaJsBooleanValue? Listening
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Listening");
            set => SetAttributeValue("Listening", value);
        }
        
        public GrKonvaJsFloat32Value? Noise
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Noise");
            set => SetAttributeValue("Noise", value);
        }

        public GrKonvaJsFloat32Value? Opacity
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Opacity");
            set => SetAttributeValue("Opacity", value);
        }

        public GrKonvaJsInt32Value? PixelSize
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("PixelSize");
            set => SetAttributeValue("PixelSize", value);
        }

        public GrKonvaJsVector2Value? Position
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Position");
            set => SetAttributeValue("Position", value);
        }
        
        public GrKonvaJsBooleanValue? PreventDefault
        {
            get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("PreventDefault");
            set => SetAttributeValue("PreventDefault", value);
        }

        public GrKonvaJsVector2Value? Scale
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Scale");
            set => SetAttributeValue("Scale", value);
        }
        
        public GrKonvaJsVector2Value? Skew
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Skew");
            set => SetAttributeValue("Skew", value);
        }
        
        public GrKonvaJsVector2Value? Size
        {
            get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Size");
            set => SetAttributeValue("Size", value);
        }

        public GrKonvaJsFloat32Value? Rotation
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Rotation");
            set => SetAttributeValue("Rotation", value);
        }

        public GrKonvaJsFloat32Value? Saturation
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Saturation");
            set => SetAttributeValue("Saturation", value);
        }
        
        public GrKonvaJsFloat32Value? Value
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Value");
            set => SetAttributeValue("Value", value);
        }

        public GrKonvaJsFloat32Value? Visible
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Visible");
            set => SetAttributeValue("Visible", value);
        }
        
        public GrKonvaJsFloat32Value? X
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("X");
            set => SetAttributeValue("X", value);
        }

        public GrKonvaJsFloat32Value? Y
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Y");
            set => SetAttributeValue("Y", value);
        }

        public GrKonvaJsFloat32Value? ScaleX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleX");
            set => SetAttributeValue("ScaleX", value);
        }

        public GrKonvaJsFloat32Value? ScaleY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ScaleY");
            set => SetAttributeValue("ScaleY", value);
        }
        
        public GrKonvaJsFloat32Value? SkewX
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("SkewX");
            set => SetAttributeValue("SkewX", value);
        }

        public GrKonvaJsFloat32Value? SkewY
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("skewY");
            set => SetAttributeValue("skewY", value);
        }
        
        public GrKonvaJsFloat32Value? Threshold
        {
            get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("threshold");
            set => SetAttributeValue("threshold", value);
        }
        
        public GrKonvaJsInt32Value? ZIndex
        {
            get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("zIndex");
            set => SetAttributeValue("zIndex", value);
        }

        public GrKonvaJsTransformsEnabledValue? TransformsEnabled
        {
            get => GetAttributeValueOrNull<GrKonvaJsTransformsEnabledValue>("transformsEnabled");
            set => SetAttributeValue("transformsEnabled", value);
        }
        
        public GrKonvaJsCacheConfigValue? Cache
        {
            get => GetAttributeValueOrNull<GrKonvaJsCacheConfigValue>("cache");
            set => SetAttributeValue("cache", value);
        }
        
        //filters

        public GrKonvaJsGlobalCompositeOperationValue? GlobalCompositeOperation
        {
            get => GetAttributeValueOrNull<GrKonvaJsGlobalCompositeOperationValue>("globalCompositeOperation");
            set => SetAttributeValue("globalCompositeOperation", value);
        }
        

        //protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        //{
        //    yield return Id.GetNameValueCodePair("id");
        //    yield return Name.GetNameValueCodePair("name");
        //    yield return AbsolutePosition.GetNameValueCodePair("absolutePosition");
        //    yield return Alpha.GetNameValueCodePair("alpha");
        //    yield return Red.GetNameValueCodePair("red");
        //    yield return Green.GetNameValueCodePair("green");
        //    yield return Blue.GetNameValueCodePair("blue");
        //    yield return Hue.GetNameValueCodePair("hue");
        //    yield return Luminance.GetNameValueCodePair("luminance");
        //    yield return BlurRadius.GetNameValueCodePair("blurRadius");
        //    yield return Brightness.GetNameValueCodePair("brightness");
        //    yield return Contrast.GetNameValueCodePair("contrast");
        //    yield return Draggable.GetNameValueCodePair("draggable");
        //    yield return DragDistance.GetNameValueCodePair("dragDistance");
        //    yield return DragBoundFunc.GetNameValueCodePair("dragBoundFunc");
        //    yield return EmbossBlend.GetNameValueCodePair("embossBlend");
        //    yield return EmbossStrength.GetNameValueCodePair("embossStrength");
        //    yield return EmbossWhiteLevel.GetNameValueCodePair("embossWhiteLevel");
        //    yield return EmbossDirection.GetNameValueCodePair("embossDirection");
        //    yield return Enhance.GetNameValueCodePair("enhance");
        //    yield return OffsetX.GetNameValueCodePair("offsetX");
        //    yield return OffsetY.GetNameValueCodePair("offsetY");
        //    yield return Width.GetNameValueCodePair("width");
        //    yield return Height.GetNameValueCodePair("height");
        //    yield return KaleidoscopeAngle.GetNameValueCodePair("kaleidoscopeAngle");
        //    yield return KaleidoscopePower.GetNameValueCodePair("kaleidoscopePower");
        //    yield return Levels.GetNameValueCodePair("levels");
        //    yield return Listening.GetNameValueCodePair("listening");
        //    yield return Noise.GetNameValueCodePair("noise");
        //    yield return Opacity.GetNameValueCodePair("opacity");
        //    yield return PixelSize.GetNameValueCodePair("pixelSize");
        //    yield return PreventDefault.GetNameValueCodePair("preventDefault");
        //    yield return Position.GetNameValueCodePair("position");
        //    yield return Scale.GetNameValueCodePair("scale");
        //    yield return Skew.GetNameValueCodePair("skew");
        //    yield return Size.GetNameValueCodePair("size");
        //    yield return Rotation.GetNameValueCodePair("rotation");
        //    yield return Saturation.GetNameValueCodePair("saturation");
        //    yield return Value.GetNameValueCodePair("value");
        //    yield return Visible.GetNameValueCodePair("visible");
        //    yield return X.GetNameValueCodePair("x");
        //    yield return Y.GetNameValueCodePair("y");
        //    yield return ScaleX.GetNameValueCodePair("scaleX");
        //    yield return ScaleY.GetNameValueCodePair("scaleY");
        //    yield return SkewX.GetNameValueCodePair("skewX");
        //    yield return SkewY.GetNameValueCodePair("skewY");
        //    yield return Threshold.GetNameValueCodePair("threshold");
        //    yield return ZIndex.GetNameValueCodePair("zIndex");
        //    yield return Cache.GetNameValueCodePair("cache");
        //    yield return GlobalCompositeOperation.GetNameValueCodePair("globalCompositeOperation");
        //}
    }
    
    
    public abstract GrKonvaJsNodeProperties? NodeProperties { get; }
    
    public override GrKonvaJsObjectProperties? ObjectProperties 
        => NodeProperties;

    public string NodeId { get; }

    public bool DrawAfterCreation { get; set; } = false;


    protected GrKonvaJsNode(string constName) 
        : base(constName)
    {
        NodeId = CreateNodeId();
    }
}
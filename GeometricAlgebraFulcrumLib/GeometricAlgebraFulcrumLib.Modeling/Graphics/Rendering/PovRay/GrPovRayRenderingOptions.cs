using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay
{
    /// <summary>
    /// https://www.povray.org/documentation/3.7.0/t2_2.html#t2_2_8
    /// https://wiki.povray.org/content/Category:Command-Line_and_INI-File_Options
    /// </summary>
    public sealed class GrPovRayRenderingOptions
        : GrPovRayAttributeSet
    {
        public GrPovRayInt32Value? Width
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Width");
            set => SetAttributeValue("Width", value);
        }

        public GrPovRayInt32Value? Height
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Height");
            set => SetAttributeValue("Height", value);
        }

        public GrPovRayInt32Value? MaxImageBufferMemory
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Max_Image_Buffer_Memory");
            set => SetAttributeValue("Max_Image_Buffer_Memory", value);
        }

        public GrPovRayOutputFileTypeValue? OutputFileType
        {
            get => GetAttributeValueOrNull<GrPovRayOutputFileTypeValue>("Output_File_Type");
            set => SetAttributeValue("Output_File_Type", value);
        }
        
        public GrPovRayBooleanValue? Display
        {
            get => GetAttributeValueOrNull<GrPovRayBooleanValue>("Display");
            set => SetAttributeValue("Display", value);
        }
        
        public GrPovRayBooleanValue? OutputAlpha
        {
            get => GetAttributeValueOrNull<GrPovRayBooleanValue>("Output_Alpha");
            set => SetAttributeValue("Output_Alpha", value);
        }

        public GrPovRayInt32Value? WorkThreads
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Work_Threads");
            set => SetAttributeValue("Work_Threads", value);
        }
        
        public GrPovRayInt32Value? Quality
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Quality");
            set => SetAttributeValue("Quality", value);
        }
        
        public GrPovRaySamplingMethodValue? SamplingMethod
        {
            get => GetAttributeValueOrNull<GrPovRaySamplingMethodValue>("Sampling_Method");
            set => SetAttributeValue("Sampling_Method", value);
        } 

        public GrPovRayBooleanValue? AntiAlias
        {
            get => GetAttributeValueOrNull<GrPovRayBooleanValue>("Antialias");
            set => SetAttributeValue("Antialias", value);
        }
        
        public GrPovRayInt32Value? AntiAliasDepth
        {
            get => GetAttributeValueOrNull<GrPovRayInt32Value>("Antialias_Depth");
            set => SetAttributeValue("Antialias_Depth", value);
        } 
        
        public GrPovRayFloat32Value? AntiAliasConfidence
        {
            get => GetAttributeValueOrNull<GrPovRayFloat32Value>("Antialias_Confidence");
            set => SetAttributeValue("Antialias_Confidence", value);
        } 
        
        public GrPovRayFloat32Value? AntiAliasThreshold
        {
            get => GetAttributeValueOrNull<GrPovRayFloat32Value>("Antialias_Threshold");
            set => SetAttributeValue("Antialias_Threshold", value);
        } 
        
        public GrPovRayFloat32Value? AntiAliasGamma
        {
            get => GetAttributeValueOrNull<GrPovRayFloat32Value>("Antialias_Gamma");
            set => SetAttributeValue("Antialias_Gamma", value);
        } 

        public GrPovRayBooleanValue? Jitter
        {
            get => GetAttributeValueOrNull<GrPovRayBooleanValue>("Jitter");
            set => SetAttributeValue("Jitter", value);
        }
        
        public GrPovRayFloat32Value? JitterAmount
        {
            get => GetAttributeValueOrNull<GrPovRayFloat32Value>("Jitter_Amount");
            set => SetAttributeValue("Jitter_Amount", value);
        }

        public double AspectRatio 
            => Width is null || Width.IsEmpty || 
               Height is null || Height.IsEmpty
                ? throw new InvalidOperationException() 
                : Width.Value / (double)Height.Value;

        
        public GrPovRayRenderingOptions()
        {
            Width = 1024;
            Height = 768;
            MaxImageBufferMemory = 1024;
            OutputFileType = GrPovRayOutputFileTypeValue.Png;
            Display = true;
            OutputAlpha = true;
            Quality = 9;
            AntiAlias = true;
            SamplingMethod = GrPovRaySamplingMethod.Method2;
            AntiAliasDepth = 3;
            Jitter = false;
        }
        
        public GrPovRayRenderingOptions(GrPovRayRenderingOptions options)
        {
            Width = options.Width;
            Height = options.Height;
            MaxImageBufferMemory = options.MaxImageBufferMemory;
            OutputFileType = options.OutputFileType;
            Display = options.Display;
            OutputAlpha = options.OutputAlpha;
            WorkThreads = options.WorkThreads;
            Quality = options.Quality;
            AntiAlias = options.AntiAlias;
            SamplingMethod = options.SamplingMethod;
            AntiAliasDepth = options.AntiAliasDepth;
            AntiAliasConfidence = options.AntiAliasConfidence;
            AntiAliasThreshold = options.AntiAliasThreshold;
            AntiAliasGamma = options.AntiAliasGamma;
            Jitter = options.Jitter;
            JitterAmount = options.JitterAmount;
        }


        public override string GetPovRayCode()
        {
            return GetAttributeValueCode(
                (key, value) => $"{key}={value}"
            ).Concatenate(Environment.NewLine);
        }
    }
}

using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Interiors
{
    public class GrPovRayInterior :
        IGrPovRayInterior
    {
        public string BaseElementIdentifier { get; }

        public bool IsEmptyCodeElement() => Media.IsNullOrEmpty() && Properties.IsNullOrEmpty();

        public GrPovRayMediaList Media { get; } 
            = new GrPovRayMediaList();

        public GrPovRayInteriorProperties Properties { get; private set; }
            = new GrPovRayInteriorProperties();


        public GrPovRayInterior()
        {
            BaseElementIdentifier = string.Empty;
        }

        public GrPovRayInterior(string identifier)
        {
            BaseElementIdentifier = identifier;
        }


        public GrPovRayInterior SetProperties(GrPovRayInteriorProperties properties)
        {
            Properties = new GrPovRayInteriorProperties(properties);

            return this;
        }

        public string GetPovRayCode()
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine("interior {")
                .IncreaseIndentation();

            if (!string.IsNullOrEmpty(BaseElementIdentifier))
                composer.AppendAtNewLine(BaseElementIdentifier);

            composer.AppendAtNewLine(Properties.GetPovRayCode());

            if (!Media.IsEmptyCodeElement())
                composer.AppendAtNewLine(Media.GetPovRayCode());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }
    }
}

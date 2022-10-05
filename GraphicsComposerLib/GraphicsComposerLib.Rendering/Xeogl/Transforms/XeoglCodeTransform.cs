using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.Xeogl.Transforms
{
    public sealed class XeoglCodeTransform : IXeoglTransform
    {
        public static XeoglCodeTransform CreateMatrix(string code)
            => new XeoglCodeTransform()
            {
                UseMatrix = true,
                MatrixCode = code
            };

        public static XeoglCodeTransform CreateQuaternion(string quaternionCode)
            => new XeoglCodeTransform()
            {
                UseQuaternion = true,
                QuaternionCode = quaternionCode
            };

        public static XeoglCodeTransform CreateRotate(string rotateCode)
            => new XeoglCodeTransform()
            {
                RotateCode = rotateCode
            };

        public static XeoglCodeTransform CreateScale(string scaleCode)
            => new XeoglCodeTransform()
            {
                ScaleCode = scaleCode
            };

        public static XeoglCodeTransform CreateTranslate(string translateCode)
            => new XeoglCodeTransform()
            {
                TranslateCode = translateCode
            };

        public static XeoglCodeTransform CreateQuaternionScaleTranslate(string quaternionCode, string scaleCode, string translateCode)
            => new XeoglCodeTransform()
            {
                UseQuaternion = true,
                QuaternionCode = quaternionCode,
                ScaleCode = scaleCode,
                TranslateCode = translateCode
            };

        public static XeoglCodeTransform CreateQuaternionScale(string quaternionCode, string scaleCode)
            => new XeoglCodeTransform()
            {
                UseQuaternion = true,
                QuaternionCode = quaternionCode,
                ScaleCode = scaleCode
            };

        public static XeoglCodeTransform CreateQuaternionTranslate(string quaternionCode, string translateCode)
            => new XeoglCodeTransform()
            {
                UseQuaternion = true,
                QuaternionCode = quaternionCode,
                TranslateCode = translateCode
            };

        public static XeoglCodeTransform CreateRotateScaleTranslate(string rotateCode, string scaleCode, string translateCode)
            => new XeoglCodeTransform()
            {
                RotateCode = rotateCode,
                ScaleCode = scaleCode,
                TranslateCode = translateCode
            };

        public static XeoglCodeTransform CreateRotateScale(string rotateCode, string scaleCode)
            => new XeoglCodeTransform()
            {
                RotateCode = rotateCode,
                ScaleCode = scaleCode
            };

        public static XeoglCodeTransform CreateRotateTranslate(string rotateCode, string translateCode)
            => new XeoglCodeTransform()
            {
                RotateCode = rotateCode,
                TranslateCode = translateCode
            };

        public static XeoglCodeTransform CreateScaleTranslate(string scaleCode, string translateCode)
            => new XeoglCodeTransform()
            {
                ScaleCode = scaleCode,
                TranslateCode = translateCode
            };


        public bool UseMatrix { get; set; }

        public bool UseQuaternion { get; set; }

        public string MatrixCode { get; set; }

        public string QuaternionCode { get; set; }

        public string RotateCode { get; set; }

        public string ScaleCode { get; set; }

        public string TranslateCode { get; set; }


        public bool ContainsMatrix
            => !string.IsNullOrEmpty(MatrixCode);

        public bool ContainsQuaternion 
            => !string.IsNullOrEmpty(QuaternionCode);

        public bool ContainsRotate 
            => !string.IsNullOrEmpty(RotateCode);

        public bool ContainsScale
            => !string.IsNullOrEmpty(ScaleCode);

        public bool ContainsTranslate
            => !string.IsNullOrEmpty(TranslateCode);


        public string GetMatrixText()
            => MatrixCode;

        public string GetQuaternionText()
            => QuaternionCode;

        public string GetRotateText()
            => RotateCode;

        public string GetScaleText()
            => ScaleCode;

        public string GetTranslateText()
            => TranslateCode;


        public override string ToString()
        {
            var composer = new LinearTextComposer();

            if (UseMatrix)
            {
                if (string.IsNullOrEmpty(MatrixCode))
                    return string.Empty;

                composer
                    .Append("matrix: ")
                    .Append(MatrixCode);

                return composer.ToString();
            }

            var commaFlag = false;

            if (UseQuaternion)
            {
                if (ContainsQuaternion)
                {
                    composer
                        .Append("quaternion: ")
                        .Append(QuaternionCode);

                    commaFlag = true;
                }
            }
            else if (ContainsRotate)
            {
                composer
                    .Append("rotation: ")
                    .Append(RotateCode);

                commaFlag = true;
            }

            if (ContainsScale)
            {
                if (commaFlag)
                    composer.AppendLine(",");

                composer
                    .AppendAtNewLine("scale: ")
                    .Append(ScaleCode);

                commaFlag = true;
            }

            if (ContainsTranslate)
            {
                if (commaFlag)
                    composer.AppendLine(",");

                composer
                    .AppendAtNewLine("position: ")
                    .Append(TranslateCode);
            }

            return composer.ToString();
        }
    }
}
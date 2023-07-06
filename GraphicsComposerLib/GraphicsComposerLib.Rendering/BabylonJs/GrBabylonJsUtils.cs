using System.Collections.Immutable;
using System.Numerics;
using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GraphicsComposerLib.Rendering.BabylonJs.Animations;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;
using GraphicsComposerLib.Rendering.BabylonJs.Materials;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using Newtonsoft.Json.Linq;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using WebComposerLib.Colors;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    public static class GrBabylonJsUtils
    {
        internal static string? GetValueCode<T>(this SparseCodeAttributeValue<T>? value)
        {
            return value is null || value.IsEmpty
                ? null 
                : value.GetCode();
        }

        internal static Pair<string>? GetNameValueCodePair(this GrBabylonJsCodeValue? value, string name)
        {
            return value is null || value.IsEmpty
                ? null 
                : new Pair<string>(name, value.GetCode());
        }

        internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name)
        {
            return value is null || value.IsEmpty
                ? null 
                : new Pair<string>(name, value.GetCode());
        }
        
        internal static Pair<string>? GetNameValueCodePair<T>(this SparseCodeAttributeValue<T>? value, string name, SparseCodeAttributeValue<T>? defaultValue)
        {
            return value is null || value.IsEmpty
                ? defaultValue.GetNameValueCodePair(name) 
                : new Pair<string>(name, value.GetCode());
        }

        public static GrBabylonJsVector3Value ToBabylonJsVector3Value(this IFloat64Vector3D value)
        {
            return GrBabylonJsVector3Value.Create(value);
        }

        public static bool HasValue(this SparseCodeAttributeValue? value)
        {
            return value is not null && !value.IsEmpty;
        }
        
        public static bool IsNullOrEmpty(this SparseCodeAttributeValue? value)
        {
            return value is null || value.IsEmpty;
        }

        public static GrBabylonJsSimpleMaterial ToBabylonJsSimpleMaterial(this System.Drawing.Color color, string materialName)
        {
            return color.ToImageSharpColor().ToBabylonJsSimpleMaterial(materialName);
        }

        public static GrBabylonJsSimpleMaterial ToBabylonJsSimpleMaterial(this Color color, string materialName)
        {
            return new GrBabylonJsSimpleMaterial(materialName)
                .SetProperties(
                    new GrBabylonJsSimpleMaterial.SimpleMaterialProperties
                    {
                        DiffuseColor = color
                    }
                );
        }

        public static GrBabylonJsStandardMaterial ToBabylonJsStandardMaterial(this System.Drawing.Color color, string materialName)
        {
            return color.ToImageSharpColor().ToBabylonJsStandardMaterial(materialName);
        }

        public static GrBabylonJsStandardMaterial ToBabylonJsStandardMaterial(this Color color, string materialName)
        {
            return new GrBabylonJsStandardMaterial(materialName)
                .SetProperties(
                    new GrBabylonJsStandardMaterial.StandardMaterialProperties(color)
                );
        }

        
        public static JToken GetBabylonJsJson(this bool value)
        {
            return JToken.FromObject(value);
        }
        
        public static JToken GetBabylonJsJson(this int value)
        {
            return JToken.FromObject(value);
        }
        
        public static JToken GetBabylonJsJson(this float value)
        {
            return JToken.FromObject(value);
        }
        
        public static JToken GetBabylonJsJson(this double value)
        {
            return JToken.FromObject((float) value);
        }
        
        public static JToken GetBabylonJsJson(this SizeF value)
        {
            return new JArray(
                value.Width,
                value.Height
            );
        }

        public static JToken GetBabylonJsJson(this IPair<double> value)
        {
            return new JArray(
                (float) value.Item1,
                (float) value.Item2
            );
        }

        public static JToken GetBabylonJsJson(this IFloat64Vector2D value)
        {
            return new JArray(
                (float) value.X,
                (float) value.Y
            );
        }
        
        public static JToken GetBabylonJsJson(this ITriplet<double> value)
        {
            return new JArray(
                (float) value.Item1,
                (float) value.Item2,
                (float) value.Item3
            );
        }

        public static JToken GetBabylonJsJson(this IFloat64Vector3D value)
        {
            return new JArray(
                (float) value.X,
                (float) value.Y,
                (float) value.Z
            );
        }
        
        public static JToken GetBabylonJsJson(this IQuad<double> value)
        {
            return new JArray(
                (float) value.Item1,
                (float) value.Item2,
                (float) value.Item3,
                (float) value.Item4
            );
        }

        public static JToken GetBabylonJsJson(this IFloat64Vector4D value)
        {
            return new JArray(
                (float) value.X,
                (float) value.Y,
                (float) value.Z,
                (float) value.W
            );
        }
        
        public static JToken GetBabylonJsJson(this Float64Quaternion value)
        {
            return new JArray(
                (float) -value.ScalarI,
                (float) -value.ScalarJ,
                (float) -value.ScalarK,
                (float) value.Scalar
            );
        }
        
        public static JToken GetBabylonJsJson(this Quaternion value)
        {
            return new JArray(
                value.X,
                value.Y,
                value.Z,
                value.W
            );
        }

        public static JToken GetBabylonJsJson(this System.Drawing.Color color, bool useAlpha = false)
        {
            if (useAlpha)
                return new JArray(
                    (color.R / 255f).GetBabylonJsJson(),
                    (color.G / 255f).GetBabylonJsJson(),
                    (color.B / 255f).GetBabylonJsJson(),
                    (color.A / 255f).GetBabylonJsJson()
                );

            return new JArray(
                (color.R / 255f).GetBabylonJsJson(),
                (color.G / 255f).GetBabylonJsJson(),
                (color.B / 255f).GetBabylonJsJson()
            );
        }

        public static JToken GetBabylonJsJson(this Color color, bool useAlpha = false)
        {
            if (useAlpha)
            {
                var c = color.ToPixel<Rgba32>();

                return new JArray(
                    (c.R / 255f).GetBabylonJsJson(),
                    (c.G / 255f).GetBabylonJsJson(),
                    (c.B / 255f).GetBabylonJsJson(),
                    (c.A / 255f).GetBabylonJsJson()
                );
            }
            else
            {
                var c = color.ToPixel<Rgb24>();

                return new JArray(
                    (c.R / 255f).GetBabylonJsJson(),
                    (c.G / 255f).GetBabylonJsJson(),
                    (c.B / 255f).GetBabylonJsJson()
                );
            }
        }


        public static string GetBabylonJsCode(this bool value)
        {
            return value ? "true" : "false";
        }

        public static string GetBabylonJsCode(this int value)
        {
            return value.ToString();
        }

        public static string GetBabylonJsCode(this float value)
        {
            return value.ToString("G");
        }

        public static string GetBabylonJsCode(this double value)
        {
            return ((float) value).ToString("G");
        }

        public static string GetBabylonJsCode(this SizeF value)
        {
            return value.Width == value.Height
                ? value.Width.GetBabylonJsCode()
                : $"{{ width: {value.Width.GetBabylonJsCode()}, height: {value.Height.GetBabylonJsCode()} }}";
        }

        public static string GetBabylonJsCode(this System.Drawing.Color color, bool useAlpha = false)
        {
            return useAlpha 
                ? $"BABYLON.Color4.FromInts({color.R}, {color.G}, {color.B}, {color.A})" 
                : $"BABYLON.Color3.FromInts({color.R}, {color.G}, {color.B})";
        }

        public static string GetBabylonJsCode(this Color color, bool useAlpha = false)
        {
            if (useAlpha)
            {
                var c = color.ToPixel<Rgba32>();

                return $"BABYLON.Color4.FromInts({c.R}, {c.G}, {c.B}, {c.A})";
            }
            else
            {
                var c = color.ToPixel<Rgb24>();

                return $"BABYLON.Color3.FromInts({c.R}, {c.G}, {c.B})";
            }
        }

        public static string GetBabylonJsCode(this LinUnitBasisVector3D axis)
        {
            return axis switch
            {
                LinUnitBasisVector3D.PositiveX => "BABYLON.Axis.X",
                LinUnitBasisVector3D.NegativeX => "(-BABYLON.Axis.X)",
                LinUnitBasisVector3D.PositiveY => "BABYLON.Axis.Y",
                LinUnitBasisVector3D.NegativeY => "(-BABYLON.Axis.Y)",
                LinUnitBasisVector3D.PositiveZ => "BABYLON.Axis.Z",
                _ => "(-BABYLON.Axis.Z)"
            };
        }

        public static string GetBabylonJsCode(this IPair<double> vector)
        {
            return $"new BABYLON.Vector2({(float) vector.Item1:G}, {(float) vector.Item2:G})";
        }

        public static string GetBabylonJsCode(this ITriplet<double> vector)
        {
            return $"new BABYLON.Vector3({(float) vector.Item1:G}, {(float) vector.Item2:G}, {(float) vector.Item3:G})";
        }
        
        public static string GetBabylonJsCode(this IQuad<double> vector)
        {
            return $"new BABYLON.Vector4({(float) vector.Item1:G}, {(float) vector.Item2:G}, {(float) vector.Item3:G}, {(float) vector.Item4:G})";
        }

        public static string GetBabylonJsCode(this Float64Quaternion quaternion)
        {
            var q1 = (float) quaternion.Scalar;
            var qi = (float) -quaternion.ScalarI;
            var qj = (float) -quaternion.ScalarJ;
            var qk = (float) -quaternion.ScalarK;

            return $"new BABYLON.Quaternion({qi:G}, {qj:G}, {qk:G}, {q1:G})";
        }

        public static string GetBabylonJsCode(this Quaternion quaternion)
        {
            return $"new BABYLON.Quaternion({quaternion.X:G}, {quaternion.Y:G}, {quaternion.Z:G}, {quaternion.W:G})";
        }
        
        public static string GetBabylonJsCode(this IEnumerable<int> vectorList)
        {
            return vectorList.Select(
                vector => vector.GetBabylonJsCode()
            ).Concatenate(", ", "[", "]");
        }
        
        public static string GetBabylonJsArrayCode(this IEnumerable<string> itemsList)
        {
            var itemsText = 
                itemsList.Concatenate($",{Environment.NewLine}");

            return new LinearTextComposer()
                .AppendLine("[")
                .IncreaseIndentation()
                .Append(itemsText)
                .DecreaseIndentation()
                .AppendAtNewLine("]")
                .ToString();
        }
        
        public static string GetBabylonJsCode(this IEnumerable<Color> colorList, bool useAlpha = false)
        {
            return colorList.Select(
                vector => vector.GetBabylonJsCode(useAlpha)
            ).GetBabylonJsArrayCode();
        }
        
        public static string GetBabylonJsCode(this IEnumerable<IReadOnlyList<Color>> colorArrayList, bool useAlpha = false)
        {
            return colorArrayList.Select(
                vector => vector.GetBabylonJsCode(useAlpha)
            ).GetBabylonJsArrayCode();
        }

        public static string GetBabylonJsCode(this IEnumerable<IPair<double>> vectorList)
        {
            return vectorList.Select(
                vector => vector.GetBabylonJsCode()
            ).GetBabylonJsArrayCode();
        }

        public static string GetBabylonJsCode(this IEnumerable<ITriplet<double>> vectorList)
        {
            return vectorList.Select(
                vector => vector.GetBabylonJsCode()
            ).GetBabylonJsArrayCode();
        }
        
        public static string GetBabylonJsCode(this IEnumerable<IReadOnlyList<ITriplet<double>>> vectorList)
        {
            return vectorList.Select(
                vector => vector.GetBabylonJsCode()
            ).GetBabylonJsArrayCode();
        }

        public static string GetBabylonJsCode(this IEnumerable<IQuad<double>> vectorList)
        {
            return vectorList.Select(
                vector => vector.GetBabylonJsCode()
            ).GetBabylonJsArrayCode();
        }
        
        public static string GetAffineMapBabylonJsArrayCode(this double[,] affineMapArray, bool isTransposed = false)
        {
            return affineMapArray
                .GetItems(isTransposed)
                .Select(
                    s => s.GetBabylonJsCode()
                ).Concatenate(
                    ",", 
                    "[", 
                    "]"
                );
        }
        
        public static string GetAffineMapBabylonJsMatrixCode(this double[,] affineMapArray, bool isTransposed = false)
        {
            var affineMapArrayCode = 
                affineMapArray.GetAffineMapBabylonJsArrayCode(isTransposed);

            return $"BABYLON.Matrix.FromArray({affineMapArrayCode}, 0)";
        }

        public static string GetBabylonJsArrayCode(this IAffineMap3D affineMap, bool isTransposed = false)
        {
            return affineMap
                .GetArray2D()
                .GetAffineMapBabylonJsArrayCode(isTransposed);
        }

        public static string GetBabylonJsMatrixCode(this IAffineMap3D affineMap, bool isTransposed = false)
        {
            return affineMap
                .GetArray2D()
                .GetAffineMapBabylonJsMatrixCode(isTransposed);
        }

        public static string GetBabylonJsCode(this IEnumerable<SparseCodeAttributeValue> valueList)
        {
            return valueList.Select(
                value => value.GetCode()
            ).GetBabylonJsArrayCode();
        }

        public static string GetBabylonJsCode(this IPair<ITriplet<double>> vectorList)
        {
            var p1 = vectorList.Item1.GetBabylonJsCode();
            var p2 = vectorList.Item2.GetBabylonJsCode();

            return $"[{p1}, {p2}]";
        }
        
        public static string GetBabylonJsCode(this ITriplet<ITriplet<double>> vectorList)
        {
            var p1 = vectorList.Item1.GetBabylonJsCode();
            var p2 = vectorList.Item2.GetBabylonJsCode();
            var p3 = vectorList.Item3.GetBabylonJsCode();

            return $"[{p1}, {p2}, {p3}]";
        }
        
        public static string GetBabylonJsCode(this IQuad<ITriplet<double>> vectorList)
        {
            var p1 = vectorList.Item1.GetBabylonJsCode();
            var p2 = vectorList.Item2.GetBabylonJsCode();
            var p3 = vectorList.Item3.GetBabylonJsCode();
            var p4 = vectorList.Item4.GetBabylonJsCode();

            return $"[{p1}, {p2}, {p3}, {p4}]";
        }


        public static string GetBabylonJsCode(this GrBabylonJsNamedColor3 namedColor)
        {
            return namedColor switch
            {
                GrBabylonJsNamedColor3.Red => "BABYLON.Color3.Red()",
                GrBabylonJsNamedColor3.Green => "BABYLON.Color3.Green()",
                GrBabylonJsNamedColor3.Blue => "BABYLON.Color3.Blue()",
                GrBabylonJsNamedColor3.Black => "BABYLON.Color3.Black()",
                GrBabylonJsNamedColor3.White => "BABYLON.Color3.White()",
                GrBabylonJsNamedColor3.Gray => "BABYLON.Color3.Gray()",
                GrBabylonJsNamedColor3.Yellow => "BABYLON.Color3.Yellow()",
                GrBabylonJsNamedColor3.Magenta => "BABYLON.Color3.Magenta()",
                GrBabylonJsNamedColor3.Purple => "BABYLON.Color3.Purple()",
                GrBabylonJsNamedColor3.Teal => "BABYLON.Color3.Teal()",
                _ => throw new ArgumentOutOfRangeException(nameof(namedColor), namedColor, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsAnimationLoopMode animationLoopMode)
        {
            return animationLoopMode switch
            {
                GrBabylonJsAnimationLoopMode.Relative => "BABYLON.Animation.ANIMATIONLOOPMODE_RELATIVE",
                GrBabylonJsAnimationLoopMode.Cycle => "BABYLON.Animation.ANIMATIONLOOPMODE_CYCLE",
                GrBabylonJsAnimationLoopMode.Constant => "BABYLON.Animation.ANIMATIONLOOPMODE_CONSTANT",
                _ => throw new ArgumentOutOfRangeException(nameof(animationLoopMode), animationLoopMode, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsAnimationType animationType)
        {
            return animationType switch
            {
                GrBabylonJsAnimationType.Float => "BABYLON.Animation.ANIMATIONTYPE_FLOAT",
                GrBabylonJsAnimationType.Vector2 => "BABYLON.Animation.ANIMATIONTYPE_VECTOR2",
                GrBabylonJsAnimationType.Vector3 => "BABYLON.Animation.ANIMATIONTYPE_VECTOR3",
                GrBabylonJsAnimationType.Quaternion => "BABYLON.Animation.ANIMATIONTYPE_QUATERNION",
                GrBabylonJsAnimationType.Matrix => "BABYLON.Animation.ANIMATIONTYPE_MATRIX",
                GrBabylonJsAnimationType.Size => "BABYLON.Animation.ANIMATIONTYPE_SIZE",
                GrBabylonJsAnimationType.Color3 => "BABYLON.Animation.ANIMATIONTYPE_COLOR3",
                GrBabylonJsAnimationType.Color4 => "BABYLON.Animation.ANIMATIONTYPE_COLOR4",
                _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsCameraMode cameraMode)
        {
            return cameraMode switch
            {
                GrBabylonJsCameraMode.PerspectiveCamera => "BABYLON.Camera.PERSPECTIVE_CAMERA",
                GrBabylonJsCameraMode.OrthographicCamera => "BABYLON.Camera.ORTHOGRAPHIC_CAMERA",
                _ => throw new ArgumentOutOfRangeException(nameof(cameraMode), cameraMode, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsCameraFovMode cameraFovMode)
        {
            return cameraFovMode switch
            {
                GrBabylonJsCameraFovMode.VerticalFixed => "BABYLON.Camera.FOVMODE_VERTICAL_FIXED",
                GrBabylonJsCameraFovMode.HorizontalFixed => "BABYLON.Camera.FOVMODE_HORIZONTAL_FIXED",
                _ => throw new ArgumentOutOfRangeException(nameof(cameraFovMode), cameraFovMode, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsMeshOrientation meshOrientation)
        {
            return meshOrientation switch
            {
                GrBabylonJsMeshOrientation.Front => "BABYLON.Mesh.FRONTSIDE",
                GrBabylonJsMeshOrientation.Back => "BABYLON.Mesh.BACKSIDE",
                GrBabylonJsMeshOrientation.FrontAndBack => "BABYLON.Mesh.DOUBLESIDE",
                _ => throw new ArgumentOutOfRangeException(nameof(meshOrientation), meshOrientation, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsTextureWrapMode textureWrapMode)
        {
            return textureWrapMode switch
            {
                GrBabylonJsTextureWrapMode.Clamp => "BABYLON.Texture.CLAMP_ADDRESSMODE",
                GrBabylonJsTextureWrapMode.Wrap => "BABYLON.Texture.WRAP_ADDRESSMODE",
                GrBabylonJsTextureWrapMode.Mirror => "BABYLON.Texture.MIRROR_ADDRESSMODE",
                _ => throw new ArgumentOutOfRangeException(nameof(textureWrapMode), textureWrapMode, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsTextureFormat textureFormat)
        {
            return textureFormat switch
            {
                GrBabylonJsTextureFormat.Alpha => "BABYLON.Engine.TEXTUREFORMAT_ALPHA",
                GrBabylonJsTextureFormat.Luminance => "BABYLON.Engine.TEXTUREFORMAT_LUMINANCE",
                GrBabylonJsTextureFormat.LuminanceAlpha => "BABYLON.Engine.TEXTUREFORMAT_LUMINANCE_ALPHA",
                GrBabylonJsTextureFormat.Rgb => "BABYLON.Engine.TEXTUREFORMAT_RGB",
                GrBabylonJsTextureFormat.Rgba => "BABYLON.Engine.TEXTUREFORMAT_RGBA",
                GrBabylonJsTextureFormat.R => "BABYLON.Engine.TEXTUREFORMAT_R",
                GrBabylonJsTextureFormat.Rg => "BABYLON.Engine.TEXTUREFORMAT_RG",
                GrBabylonJsTextureFormat.IntegerR => "BABYLON.Engine.TEXTUREFORMAT_R_INTEGER",
                GrBabylonJsTextureFormat.IntegerRg => "BABYLON.Engine.TEXTUREFORMAT_RG_INTEGER",
                GrBabylonJsTextureFormat.IntegerRgb => "BABYLON.Engine.TEXTUREFORMAT_RGB_INTEGER",
                GrBabylonJsTextureFormat.IntegerRgba => "BABYLON.Engine.TEXTUREFORMAT_RGBA_INTEGER",
                _ => throw new ArgumentOutOfRangeException(nameof(textureFormat), textureFormat, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsTextureSamplingMode textureSamplingMode)
        {
            return textureSamplingMode switch
            {
                GrBabylonJsTextureSamplingMode.Nearest => "BABYLON.Texture.NEAREST_SAMPLINGMODE",
                GrBabylonJsTextureSamplingMode.Bilinear => "BABYLON.Texture.BILINEAR_SAMPLINGMODE",
                GrBabylonJsTextureSamplingMode.Trilinear => "BABYLON.Texture.TRILINEAR_SAMPLINGMODE",
                GrBabylonJsTextureSamplingMode.NearestNearestMipNearest => "BABYLON.Texture.NEAREST_NEAREST_MIPNEAREST",
                GrBabylonJsTextureSamplingMode.NearestLinearMipNearest => "BABYLON.Texture.NEAREST_LINEAR_MIPNEAREST",
                GrBabylonJsTextureSamplingMode.NearestLinearMipLinear => "BABYLON.Texture.NEAREST_LINEAR_MIPLINEAR",
                GrBabylonJsTextureSamplingMode.NearestLinear => "BABYLON.Texture.NEAREST_LINEAR",
                GrBabylonJsTextureSamplingMode.NearestNearest => "BABYLON.Texture.NEAREST_NEAREST",
                GrBabylonJsTextureSamplingMode.LinearNearestMipNearest => "BABYLON.Texture.LINEAR_NEAREST_MIPNEAREST",
                GrBabylonJsTextureSamplingMode.LinearNearestMipLinear => "BABYLON.Texture.LINEAR_NEAREST_MIPLINEAR",
                GrBabylonJsTextureSamplingMode.LinearLinear => "BABYLON.Texture.LINEAR_LINEAR",
                GrBabylonJsTextureSamplingMode.LinearNearest => "BABYLON.Texture.LINEAR_NEAREST",
                _ => throw new ArgumentOutOfRangeException(nameof(textureSamplingMode), textureSamplingMode, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsTextureCoordinatesMode textureCoordinatesMode)
        {
            return textureCoordinatesMode switch
            {
                GrBabylonJsTextureCoordinatesMode.Explicit => "BABYLON.Texture.EXPLICIT_MODE",
                GrBabylonJsTextureCoordinatesMode.Spherical => "BABYLON.Texture.SPHERICAL_MODE",
                GrBabylonJsTextureCoordinatesMode.Planar => "BABYLON.Texture.PLANAR_MODE",
                GrBabylonJsTextureCoordinatesMode.Cubic => "BABYLON.Texture.CUBIC_MODE",
                GrBabylonJsTextureCoordinatesMode.Projection => "BABYLON.Texture.PROJECTION_MODE",
                GrBabylonJsTextureCoordinatesMode.SkyBox => "BABYLON.Texture.SKYBOX_MODE",
                GrBabylonJsTextureCoordinatesMode.InverseCubic => "BABYLON.Texture.INVCUBIC_MODE",
                GrBabylonJsTextureCoordinatesMode.EquiRectangular => "BABYLON.Texture.EQUIRECTANGULAR_MODE",
                GrBabylonJsTextureCoordinatesMode.FixedEquiRectangular => "BABYLON.Texture.FIXED_EQUIRECTANGULAR_MODE",
                GrBabylonJsTextureCoordinatesMode.FixedEquiRectangularMirrored => "BABYLON.Texture.FIXED_EQUIRECTANGULAR_MIRRORED_MODE",
                _ => throw new ArgumentOutOfRangeException(nameof(textureCoordinatesMode), textureCoordinatesMode, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsMaterialTransparencyMode transparencyMode)
        {
            return transparencyMode switch
            {
                GrBabylonJsMaterialTransparencyMode.Opaque => "BABYLON.Material.MATERIAL_OPAQUE",
                GrBabylonJsMaterialTransparencyMode.AlphaTest => "BABYLON.Material.MATERIAL_ALPHATEST",
                GrBabylonJsMaterialTransparencyMode.AlphaBlend => "BABYLON.Material.MATERIAL_ALPHABLEND",
                GrBabylonJsMaterialTransparencyMode.AlphaTestAndBlend => "BABYLON.Material.MATERIAL_ALPHATESTANDBLEND",
                _ => throw new ArgumentOutOfRangeException(nameof(transparencyMode), transparencyMode, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsMeshCap cylinderCap)
        {
            return cylinderCap switch
            {
                GrBabylonJsMeshCap.None => "BABYLON.Mesh.NO_CAP",
                GrBabylonJsMeshCap.Start => "BABYLON.Mesh.CAP_START",
                GrBabylonJsMeshCap.End => "BABYLON.Mesh.CAP_END",
                GrBabylonJsMeshCap.StartAndEnd => "BABYLON.Mesh.CAP_ALL",
                _ => throw new ArgumentOutOfRangeException(nameof(cylinderCap), cylinderCap, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsHorizontalAlignment alignment)
        {
            return alignment switch
            {
                GrBabylonJsHorizontalAlignment.Left => "BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_LEFT",
                GrBabylonJsHorizontalAlignment.Center => "BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_CENTER",
                GrBabylonJsHorizontalAlignment.Right => "BABYLON.GUI.Control.HORIZONTAL_ALIGNMENT_RIGHT",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsVerticalAlignment alignment)
        {
            return alignment switch
            {
                GrBabylonJsVerticalAlignment.Bottom => "BABYLON.GUI.Control.VERTICAL_ALIGNMENT_BOTTOM",
                GrBabylonJsVerticalAlignment.Center => "BABYLON.GUI.Control.VERTICAL_ALIGNMENT_CENTER",
                GrBabylonJsVerticalAlignment.Top => "BABYLON.GUI.Control.VERTICAL_ALIGNMENT_TOP",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsFogMode fogMode)
        {
            return fogMode switch
            {
                GrBabylonJsFogMode.None => "BABYLON.Scene.FOGMODE_NONE",
                GrBabylonJsFogMode.Exp => "BABYLON.Scene.FOGMODE_EXP",
                GrBabylonJsFogMode.Exp2 => "BABYLON.Scene.FOGMODE_EXP2",
                GrBabylonJsFogMode.Linear => "BABYLON.Scene.FOGMODE_LINEAR",
                _ => throw new ArgumentOutOfRangeException(nameof(fogMode), fogMode, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsAlphaMode alphaMode)
        {
            return alphaMode switch
            {
                GrBabylonJsAlphaMode.Disable => "BABYLON.Engine.ALPHA_DISABLE",
                GrBabylonJsAlphaMode.Add => "BABYLON.Engine.ALPHA_ADD",
                GrBabylonJsAlphaMode.Combine => "BABYLON.Engine.ALPHA_COMBINE",
                GrBabylonJsAlphaMode.Subtract => "BABYLON.Engine.ALPHA_SUBTRACT",
                GrBabylonJsAlphaMode.Multiply => "BABYLON.Engine.ALPHA_MULTIPLY",
                GrBabylonJsAlphaMode.Maximized => "BABYLON.Engine.ALPHA_MAXIMIZED",
                GrBabylonJsAlphaMode.OneOne => "BABYLON.Engine.ALPHA_ONEONE",
                GrBabylonJsAlphaMode.PreMultiplied => "BABYLON.Engine.ALPHA_PREMULTIPLIED",
                GrBabylonJsAlphaMode.PreMultipliedPorterDuff => "BABYLON.Engine.ALPHA_PREMULTIPLIED_PORTERDUFF",
                GrBabylonJsAlphaMode.Interpolate => "BABYLON.Engine.ALPHA_INTERPOLATE",
                GrBabylonJsAlphaMode.ScreenMode => "BABYLON.Engine.ALPHA_SCREENMODE",
                _ => throw new ArgumentOutOfRangeException(nameof(alphaMode), alphaMode, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsImageStretch imageStretch)
        {
            return imageStretch switch
            {
                GrBabylonJsImageStretch.None => "BABYLON.GUI.Image.STRETCH_NONE",
                GrBabylonJsImageStretch.Fill => "BABYLON.GUI.Image.STRETCH_FILL",
                GrBabylonJsImageStretch.Uniform => "BABYLON.GUI.Image.STRETCH_UNIFORM",
                GrBabylonJsImageStretch.Extend => "BABYLON.GUI.Image.STRETCH_EXTEND",
                GrBabylonJsImageStretch.NinePatch => "BABYLON.GUI.Image.STRETCH_NINE_PATCH",
                _ => throw new ArgumentOutOfRangeException(nameof(imageStretch), imageStretch, null)
            };
        }

        public static string GetBabylonJsCode(this GrBabylonJsAlphaBlendingMode alphaBlendingMode)
        {
            return alphaBlendingMode switch
            {
                GrBabylonJsAlphaBlendingMode.Add => "BABYLON.Constants.ALPHA_ADD",
                GrBabylonJsAlphaBlendingMode.AlphaToColor => "BABYLON.Constants.ALPHA_ALPHATOCOLOR",
                GrBabylonJsAlphaBlendingMode.Combine => "BABYLON.Constants.ALPHA_COMBINE",
                GrBabylonJsAlphaBlendingMode.Disable => "BABYLON.Constants.ALPHA_DISABLE",
                GrBabylonJsAlphaBlendingMode.EquationAdd => "BABYLON.Constants.ALPHA_EQUATION_ADD",
                GrBabylonJsAlphaBlendingMode.EquationDarken => "BABYLON.Constants.ALPHA_EQUATION_DARKEN",
                GrBabylonJsAlphaBlendingMode.EquationMax => "BABYLON.Constants.ALPHA_EQUATION_MAX",
                GrBabylonJsAlphaBlendingMode.EquationMin => "BABYLON.Constants.ALPHA_EQUATION_MIN",
                GrBabylonJsAlphaBlendingMode.EquationReverseSubtract => "BABYLON.Constants.ALPHA_EQUATION_REVERSE_SUBTRACT",
                GrBabylonJsAlphaBlendingMode.EquationSubtract => "BABYLON.Constants.ALPHA_EQUATION_SUBSTRACT",
                GrBabylonJsAlphaBlendingMode.Exclusion => "BABYLON.Constants.ALPHA_EXCLUSION",
                GrBabylonJsAlphaBlendingMode.Interpolate => "BABYLON.Constants.ALPHA_INTERPOLATE",
                GrBabylonJsAlphaBlendingMode.LayerAccumulate => "BABYLON.Constants.ALPHA_LAYER_ACCUMULATE",
                GrBabylonJsAlphaBlendingMode.Maximized => "BABYLON.Constants.ALPHA_MAXIMIZED",
                GrBabylonJsAlphaBlendingMode.Multiply => "BABYLON.Constants.ALPHA_MULTIPLY",
                GrBabylonJsAlphaBlendingMode.OneOne => "BABYLON.Constants.ALPHA_ONEONE",
                GrBabylonJsAlphaBlendingMode.OneOneOneOne => "BABYLON.Constants.ALPHA_ONEONE_ONEONE",
                GrBabylonJsAlphaBlendingMode.OneOneOneZero => "BABYLON.Constants.ALPHA_ONEONE_ONEZERO",
                GrBabylonJsAlphaBlendingMode.PreMultiplied => "BABYLON.Constants.ALPHA_PREMULTIPLIED",
                GrBabylonJsAlphaBlendingMode.PreMultipliedPorterDuff => "BABYLON.Constants.ALPHA_PREMULTIPLIED_PORTERDUFF",
                GrBabylonJsAlphaBlendingMode.ReverseOneMinus => "BABYLON.Constants.ALPHA_REVERSEONEMINUS",
                GrBabylonJsAlphaBlendingMode.ScreenMode => "BABYLON.Constants.ALPHA_SCREENMODE",
                GrBabylonJsAlphaBlendingMode.SrcDstOneMinusSrcAlpha => "BABYLON.Constants.ALPHA_SRC_DSTONEMINUSSRCALPHA",
                GrBabylonJsAlphaBlendingMode.Subtract => "BABYLON.Constants.ALPHA_SUBTRACT",
                _ => throw new ArgumentOutOfRangeException(nameof(alphaBlendingMode), alphaBlendingMode, null)
            };
        }
        
        public static string GetBabylonJsCode(this GrBabylonJsBillboardMode billboardMode)
        {
            return billboardMode switch
            {
                GrBabylonJsBillboardMode.None => "BABYLON.AbstractMesh.BILLBOARDMODE_NONE",
                GrBabylonJsBillboardMode.X => "BABYLON.AbstractMesh.BILLBOARDMODE_X",
                GrBabylonJsBillboardMode.Y => "BABYLON.AbstractMesh.BILLBOARDMODE_Y",
                GrBabylonJsBillboardMode.Z => "BABYLON.AbstractMesh.BILLBOARDMODE_Z",
                GrBabylonJsBillboardMode.All => "BABYLON.AbstractMesh.BILLBOARDMODE_ALL",
                _ => throw new ArgumentOutOfRangeException(nameof(billboardMode), billboardMode, null)
            };
        }
        

        public static string GetBabylonJsCode(this GrBabylonJsKeyFrameDictionary<double> keyFrameList)
        {
            var keyFrameArray = keyFrameList.ToImmutableArray();

            if (keyFrameArray.Length == 0)
                return "[]";

            if (keyFrameArray.Length <= 3)
                return keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($", ", "[", "]");

            var itemsText =
                keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($",{Environment.NewLine}");

            return new LinearTextComposer()
                .AppendLine("[")
                .IncreaseIndentation()
                .Append(itemsText)
                .DecreaseIndentation()
                .AppendAtNewLine("]")
                .ToString();
        }
        
        public static string GetBabylonCode(this GrBabylonJsKeyFrameDictionary<Float64Vector2D> keyFrameList)
        {
            var keyFrameArray = keyFrameList.ToImmutableArray();

            if (keyFrameArray.Length == 0)
                return "[]";

            if (keyFrameArray.Length == 1)
                return keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($", ", "[", "]");

            var itemsText =
                keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($",{Environment.NewLine}");

            return new LinearTextComposer()
                .AppendLine("[")
                .IncreaseIndentation()
                .Append(itemsText)
                .DecreaseIndentation()
                .AppendAtNewLine("]")
                .ToString();
        }
        
        public static string GetBabylonCode(this GrBabylonJsKeyFrameDictionary<Float64Vector3D> keyFrameList)
        {
            var keyFrameArray = keyFrameList.ToImmutableArray();

            if (keyFrameArray.Length == 0)
                return "[]";

            if (keyFrameArray.Length == 1)
                return keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($", ", "[", "]");

            var itemsText =
                keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($",{Environment.NewLine}");

            return new LinearTextComposer()
                .AppendLine("[")
                .IncreaseIndentation()
                .Append(itemsText)
                .DecreaseIndentation()
                .AppendAtNewLine("]")
                .ToString();
        }

        public static string GetBabylonJsCode(this GrBabylonJsKeyFrameDictionary<Float64Quaternion> keyFrameList)
        {
            var keyFrameArray = keyFrameList.ToImmutableArray();

            if (keyFrameArray.Length == 0)
                return "[]";

            if (keyFrameArray.Length == 1)
                return keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($", ", "[", "]");

            var itemsText =
                keyFrameArray
                    .Select(indexValuePair =>
                        $"{{frame: {indexValuePair.Key.GetBabylonJsCode()}, value: {indexValuePair.Value.GetBabylonJsCode()}}}"
                    ).Concatenate($",{Environment.NewLine}");

            return new LinearTextComposer()
                .AppendLine("[")
                .IncreaseIndentation()
                .Append(itemsText)
                .DecreaseIndentation()
                .AppendAtNewLine("]")
                .ToString();
        }


        public static void AddToScene(this GrBabylonJsObject babylonJsObject, GrBabylonJsScene scene)
        {
            scene.ObjectList.Add(babylonJsObject);
        }
    }
}

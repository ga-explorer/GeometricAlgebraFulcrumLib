using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Meshes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.Scene
/// </summary>
public sealed class GrBabylonJsScene :
    GrBabylonJsObject
{
    protected override string ConstructorName 
        => "new BABYLON.Scene";

    public GrBabylonJsSceneOptions Options { get; private set; }
        = new GrBabylonJsSceneOptions();

    public GrBabylonJsSceneProperties Properties { get; private set; }
        = new GrBabylonJsSceneProperties();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;

    public GrBabylonJsCodeValue Engine { get; set; } 
        = "engine";

    public GrBabylonJsObjectList ObjectList { get; } 
        = new GrBabylonJsObjectList();

    public IEnumerable<GrBabylonJsMaterial> Materials
        => ObjectList
            .Where(s => s is GrBabylonJsMaterial)
            .Cast<GrBabylonJsMaterial>();
        
    public IEnumerable<GrBabylonJsCurve3> Curves
        => ObjectList
            .Where(s => s is GrBabylonJsCurve3)
            .Cast<GrBabylonJsCurve3>();
        
    public IEnumerable<GrBabylonJsMesh> Meshes
        => ObjectList
            .Where(s => s is GrBabylonJsMesh)
            .Cast<GrBabylonJsMesh>();
        
    public IEnumerable<GrBabylonJsAnimation> Animations
        => ObjectList
            .Where(s => s is GrBabylonJsAnimation)
            .Cast<GrBabylonJsAnimation>();

    public GrBabylonJsKeyFrameDictionaryCache KeyFramesCache { get; }
        = new GrBabylonJsKeyFrameDictionaryCache();

    public List<GrBabylonJsCodeValue> BeforeSceneRenderCode { get; }
        = new List<GrBabylonJsCodeValue>();


    public GrBabylonJsScene(string constName) 
        : base(constName)
    {
    }


    public GrBabylonJsScene SetOptions(GrBabylonJsSceneOptions options)
    {
        Options = new GrBabylonJsSceneOptions(options);

        return this;
    }

    public GrBabylonJsScene SetProperties(GrBabylonJsSceneProperties properties)
    {
        Properties = new GrBabylonJsSceneProperties(properties);

        return this;
    }


    public GrBabylonJsMaterial GetMaterial(string name)
    {
        if (!ObjectList.TryGetObject(name, out var sceneObject))
            throw new KeyNotFoundException();

        return sceneObject as GrBabylonJsMaterial 
               ?? throw new KeyNotFoundException();
    }
        
    public GrBabylonJsGuiFullScreenUi GetGuiFullScreenUi(string name)
    {
        if (!ObjectList.TryGetObject(name, out var sceneObject))
            throw new KeyNotFoundException();

        return sceneObject as GrBabylonJsGuiFullScreenUi 
               ?? throw new KeyNotFoundException();
    }

    public GrBabylonJsFreeCode AddFreeCode(string code)
    {
        var sceneObject = 
            new GrBabylonJsFreeCode(code);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsFreeCode AddFreeCode(params string[] codeLines)
    {
        return AddFreeCode(codeLines.Concatenate(Environment.NewLine));
    }
        
    public GrBabylonJsFreeCode AddFreeCode(IEnumerable<string> codeLines)
    {
        return AddFreeCode(codeLines.Concatenate(Environment.NewLine));
    }

    public GrBabylonJsArcRotateCamera AddArcRotateCamera(string name, GrBabylonJsAngleValue alpha, GrBabylonJsAngleValue beta, GrBabylonJsFloat32Value radius, GrBabylonJsVector3Value target)
    {
        var sceneObject = 
            new GrBabylonJsArcRotateCamera(name, this)
            {
                Alpha = alpha,
                Beta = beta,
                Radius = radius,
                Target = target,
                SetActiveOnSceneIfNoneActive = true
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsArcRotateCamera AddArcRotateCamera(string name, GrBabylonJsAngleValue alpha, GrBabylonJsAngleValue beta, GrBabylonJsFloat32Value radius, GrBabylonJsVector3Value target, GrBabylonJsArcRotateCameraProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsArcRotateCamera(name, this)
            {
                Alpha = alpha,
                Beta = beta,
                Radius = radius,
                Target = target,
                SetActiveOnSceneIfNoneActive = true
            }.SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsUniversalCamera AddUniversalCamera(string name, GrBabylonJsVector3Value position)
    {
        var sceneObject = 
            new GrBabylonJsUniversalCamera(name, this)
            {
                Position = position
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsUniversalCamera AddUniversalCamera(string name, GrBabylonJsVector3Value position, GrBabylonJsUniversalCameraProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsUniversalCamera(name, this)
            {
                Position = position
            }.SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name)
    {
        var sceneObject = 
            new GrBabylonJsEnvironmentHelper(name, this);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelperOptions options)
    {
        var sceneObject = 
            new GrBabylonJsEnvironmentHelper(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelperProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsEnvironmentHelper(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsEnvironmentHelper AddEnvironmentHelper(string name, GrBabylonJsEnvironmentHelperOptions options, GrBabylonJsEnvironmentHelperProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsEnvironmentHelper(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }


    public GrBabylonJsTexture AddTexture(string name, string url)
    {
        var sceneObject = 
            new GrBabylonJsTexture(name, this)
            {
                Url = url
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTextureOptions options)
    {
        var sceneObject = 
            new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTextureProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsTexture AddTexture(string name, string url, GrBabylonJsTextureOptions options, GrBabylonJsTextureProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsTexture(name, this)
                {
                    Url = url
                }
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

        
    public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element)
    {
        var sceneObject = 
            new GrBabylonJsHtmlElementTexture(name, this, element);
            
        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element, GrBabylonJsHtmlElementTextureOptions options)
    {
        var sceneObject = 
            new GrBabylonJsHtmlElementTexture(name, this, element)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsHtmlElementTexture AddHtmlElementTexture(string name, GrBabylonJsCodeValue element, GrBabylonJsHtmlElementTextureOptions options, GrBabylonJsHtmlElementTextureProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsHtmlElementTexture(name, this, element)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }


    public GrBabylonJsSimpleMaterial AddSimpleMaterial(string name, GrBabylonJsSimpleMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsSimpleMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsSimpleMaterial AddSimpleMaterial(string name, GrBabylonJsColor3Value color)
    {
        var alpha = color.Value.ToPixel<Rgba32>().A;

        if (alpha == 255)
            return AddSimpleMaterial(
                name, 
                new GrBabylonJsSimpleMaterialProperties
                {
                    DiffuseColor = color
                }
            );

        return AddSimpleMaterial(
            name, 
            new GrBabylonJsSimpleMaterialProperties
            {
                DiffuseColor = color,
                Alpha = alpha / 255f,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                BackFaceCulling = true
            }
        );
    }
        
    public GrBabylonJsStandardMaterial AddStandardMaterial(string name, GrBabylonJsStandardMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsStandardMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsStandardMaterial AddStandardMaterial(string name, GrBabylonJsColor3Value color)
    {
        var alpha = color.Value.ToPixel<Rgba32>().A;

        if (alpha == 255)
            return AddStandardMaterial(
                name, 
                new GrBabylonJsStandardMaterialProperties(color)
            );

        return AddStandardMaterial(
            name, 
            new GrBabylonJsStandardMaterialProperties(color)
            {
                Alpha = alpha / 255f,
                TransparencyMode = GrBabylonJsMaterialTransparencyMode.AlphaBlend,
                BackFaceCulling = true
            }
        );
    }
        
    public GrBabylonJsShadowOnlyMaterial AddShadowOnlyMaterial(string name, GrBabylonJsShadowOnlyMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsShadowOnlyMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsOcclusionMaterial AddOcclusionMaterial(string name, GrBabylonJsOcclusionMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsOcclusionMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsNormalMaterial AddNormalMaterial(string name, GrBabylonJsNormalMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsNormalMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsHandleMaterial AddHandleMaterial(string name, GrBabylonJsHandleMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsHandleMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsGridMaterial AddGridMaterial(string name, GrBabylonJsGridMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsGridMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsGradientMaterial AddGradientMaterial(string name, GrBabylonJsGradientMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsGradientMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsBackgroundMaterial AddBackgroundMaterial(string name, GrBabylonJsBackgroundMaterialProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsBackgroundMaterial(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsCurve3 AddCurve3(string name, GrBabylonJsVector3ArrayValue points)
    {
        var sceneObject = 
            new GrBabylonJsCurve3(name)
            {
                Points = points
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsArcThru3Points AddArcThru3Points(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsInt32Value steps, GrBabylonJsBooleanValue closed, GrBabylonJsBooleanValue fullCircle)
    {
        var sceneObject = 
            new GrBabylonJsArcThru3Points(name)
            {
                Point1 = point1,
                Point2 = point2,
                Point3 = point3,
                Steps = steps,
                Closed = closed,
                FullCircle = fullCircle
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsCatmullRomSpline AddCatmullRomSpline(string name, GrBabylonJsVector3ArrayValue points, GrBabylonJsVector3Value pointNumber, GrBabylonJsBooleanValue closed)
    {
        var sceneObject = 
            new GrBabylonJsCatmullRomSpline(name)
            {
                Points = points,
                PointNumber = pointNumber,
                Closed = closed
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsCubicBezier AddQuadraticBezier(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsVector3Value pointNumber)
    {
        var sceneObject = 
            new GrBabylonJsCubicBezier(name)
            {
                Point1 = point1,
                Point2 = point2,
                Point3 = point3,
                PointNumber = pointNumber
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsCubicBezier AddCubicBezier(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value point3, GrBabylonJsVector3Value point4, GrBabylonJsVector3Value pointNumber)
    {
        var sceneObject = 
            new GrBabylonJsCubicBezier(name)
            {
                Point1 = point1,
                Point2 = point2,
                Point3 = point3,
                Point4 = point4,
                PointNumber = pointNumber
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsHermiteSpline AddHermiteSpline(string name, GrBabylonJsVector3Value point1, GrBabylonJsVector3Value point2, GrBabylonJsVector3Value tangent1, GrBabylonJsVector3Value tangent2, GrBabylonJsVector3Value pointNumber)
    {
        var sceneObject = 
            new GrBabylonJsHermiteSpline(name)
            {
                Point1 = point1,
                Point2 = point2,
                Tangent1 = tangent1,
                Tangent2 = tangent2,
                PointNumber = pointNumber
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsMeshClone AddClone(string name, string parentMeshConstName, GrBabylonJsMeshProperties properties)
    {
        var sceneObject =
            new GrBabylonJsMeshClone(name, parentMeshConstName)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsMeshClone AddClone(string name, GrBabylonJsMesh parentMesh, GrBabylonJsMeshProperties properties)
    {
        var sceneObject =
            new GrBabylonJsMeshClone(name, parentMesh.ConstName)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsuleOptions options)
    {
        var sceneObject = 
            new GrBabylonJsCapsule(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsCapsule AddCapsule(string name, GrBabylonJsCapsuleOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsCapsule(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinderOptions options)
    {
        var sceneObject = 
            new GrBabylonJsCylinder(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsCylinder AddCylinder(string name, GrBabylonJsCylinderOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsCylinder(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDiscOptions options)
    {
        var sceneObject = 
            new GrBabylonJsDisc(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsDisc AddDisc(string name, GrBabylonJsDiscOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsDisc(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsGround AddGround(string name, GrBabylonJsGroundOptions options)
    {
        var sceneObject = 
            new GrBabylonJsGround(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsGround AddGround(string name, GrBabylonJsGroundOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsGround(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlaneOptions options)
    {
        var sceneObject = 
            new GrBabylonJsPlane(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsPlane AddPlane(string name, GrBabylonJsPlaneOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsPlane(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsBox AddBox(string name, GrBabylonJsBoxOptions options)
    {
        var sceneObject =
            new GrBabylonJsBox(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsBox AddBox(string name, GrBabylonJsBoxOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject =
            new GrBabylonJsBox(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphereOptions options)
    {
        var sceneObject = 
            new GrBabylonJsSphere(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsSphere AddSphere(string name, GrBabylonJsSphereOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsSphere(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsIcoSphere AddIcoSphere(string name, GrBabylonJsIcoSphereOptions options)
    {
        var sceneObject = 
            new GrBabylonJsIcoSphere(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsIcoSphere AddIcoSphere(string name, GrBabylonJsIcoSphereOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsIcoSphere(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorusOptions options)
    {
        var sceneObject = 
            new GrBabylonJsTorus(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsTorus AddTorus(string name, GrBabylonJsTorusOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsTorus(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsTube AddTube(string name, GrBabylonJsTubeOptions options)
    {
        var sceneObject = 
            new GrBabylonJsTube(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsTube AddTube(string name, GrBabylonJsTubeOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsTube(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsLines AddLines(string name, GrBabylonJsLinesOptions options)
    {
        var sceneObject = 
            new GrBabylonJsLines(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsLines AddLines(string name, GrBabylonJsLinesOptions options, GrBabylonJsLinesMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsLines(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLinesOptions options)
    {
        var sceneObject = 
            new GrBabylonJsDashedLines(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsDashedLines AddDashedLines(string name, GrBabylonJsDashedLinesOptions options, GrBabylonJsLinesMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsDashedLines(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsLineSystem AddLineSystem(string name, GrBabylonJsLinesSystemOptions options)
    {
        var sceneObject = 
            new GrBabylonJsLineSystem(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsLineSystem AddLineSystem(string name, GrBabylonJsLinesSystemOptions options, GrBabylonJsLinesMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsLineSystem(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsLathe AddLathe(string name, GrBabylonJsLatheOptions options)
    {
        var sceneObject = 
            new GrBabylonJsLathe(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsLathe AddLathe(string name, GrBabylonJsLatheOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsLathe(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsRibbon AddRibbon(string name, GrBabylonJsRibbonOptions options)
    {
        var sceneObject = 
            new GrBabylonJsRibbon(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsRibbon AddRibbon(string name, GrBabylonJsRibbonOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsRibbon(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsExtrudeShape AddExtrudeShape(string name, GrBabylonJsExtrudeShapeOptions options)
    {
        var sceneObject = 
            new GrBabylonJsExtrudeShape(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsExtrudeShape AddExtrudeShape(string name, GrBabylonJsExtrudeShapeOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsExtrudeShape(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsCustomExtrudeShape AddExtrudeShapeCustom(string name, GrBabylonJsCustomExtrudeShapeOptions options)
    {
        var sceneObject = 
            new GrBabylonJsCustomExtrudeShape(name, this)
                .SetOptions(options);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsCustomExtrudeShape AddExtrudeShapeCustom(string name, GrBabylonJsCustomExtrudeShapeOptions options, GrBabylonJsMeshProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsCustomExtrudeShape(name, this)
                .SetOptions(options)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

        
    public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue? isForeground = null, GrBabylonJsTextureSamplingModeValue? samplingMode = null, GrBabylonJsBooleanValue? adaptiveScaling = null)
    {
        var sceneObject = 
            new GrBabylonJsGuiFullScreenUi(name, this)
            {
                IsForeground = isForeground,
                SamplingMode = samplingMode,
                AdaptiveScaling = adaptiveScaling
            };

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsGuiFullScreenUi AddGuiFullScreenUi(string name, GrBabylonJsBooleanValue isForeground, GrBabylonJsTextureSamplingMode samplingMode, GrBabylonJsBooleanValue adaptiveScaling, GrBabylonJsAdvancedDynamicTextureProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiFullScreenUi(name, this)
            {
                IsForeground = isForeground,
                SamplingMode = samplingMode,
                AdaptiveScaling = adaptiveScaling
            };
            
        sceneObject.SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        

    public GrBabylonJsAnimationOfFloat AddFloat32Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<double> keyFrames)
    {
        var (keyFramesIndex, _) = 
            KeyFramesCache.AddOrGetFloatKeyFrames(keyFrames);

        var sceneObject = 
            new GrBabylonJsAnimationOfFloat(
                name, 
                targetPropertyName, 
                samplingSpecs, 
                KeyFramesCache,
                keyFramesIndex
            );

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsAnimationOfVector2 AddVector2Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<LinFloat64Vector2D> keyFrames)
    {
        var (keyFramesIndex, _) = 
            KeyFramesCache.AddOrGetVector2KeyFrames(keyFrames);

        var sceneObject = 
            new GrBabylonJsAnimationOfVector2(
                name, 
                targetPropertyName, 
                samplingSpecs, 
                KeyFramesCache,
                keyFramesIndex
            );

        ObjectList.Add(sceneObject);

        return sceneObject;
    }
        
    public GrBabylonJsAnimationOfVector3 AddVector3Animation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D> keyFrames)
    {
        var (keyFramesIndex, _) = 
            KeyFramesCache.AddOrGetVector3KeyFrames(keyFrames);

        var sceneObject = 
            new GrBabylonJsAnimationOfVector3(
                name, 
                targetPropertyName, 
                samplingSpecs, 
                KeyFramesCache,
                keyFramesIndex
            );

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsAnimationOfQuaternion AddQuaternionAnimation(string name, string targetPropertyName, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<LinFloat64Quaternion> keyFrames)
    {
        var (keyFramesIndex, _) = 
            KeyFramesCache.AddOrGetQuaternionKeyFrames(keyFrames);

        var sceneObject = 
            new GrBabylonJsAnimationOfQuaternion(
                name, 
                targetPropertyName, 
                samplingSpecs, 
                KeyFramesCache,
                keyFramesIndex
            );

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public Pair<GrBabylonJsAnimationOfFloat> AddVector2ComponentAnimations(string name, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<LinFloat64Vector2D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
    {
        var (keyXValues, keyYValues) = 
            keyPositions.SeparateComponents(optimizationKind);

        var xAnimation = AddFloat32Animation(
            name + "XAnimation",
            "x",
            samplingSpecs,
            keyXValues
        );
            
        var yAnimation = AddFloat32Animation(
            name + "YAnimation",
            "y",
            samplingSpecs,
            keyYValues
        );
            
        return new Pair<GrBabylonJsAnimationOfFloat>(
            xAnimation,
            yAnimation
        );
    }

    public Triplet<GrBabylonJsAnimationOfFloat> AddVector3ComponentAnimations(string name, GrBabylonJsAnimationSpecs samplingSpecs, IEnumerable<KeyValuePair<int, LinFloat64Vector3D>> frameIndexPositionPairs, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
    {
        var keyPositions = new GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D>();

        foreach (var (frameIndex, position) in frameIndexPositionPairs)
            keyPositions.SetKeyFrameValue(frameIndex, position);

        return AddVector3ComponentAnimations(
            name,
            samplingSpecs,
            keyPositions,
            optimizationKind
        );
    }

    public Triplet<GrBabylonJsAnimationOfFloat> AddVector3ComponentAnimations(string name, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<LinFloat64Vector3D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
    {
        var (keyXValues, keyYValues, keyZValues) = 
            keyPositions.SeparateComponents(optimizationKind);

        var xAnimation = AddFloat32Animation(
            name + "XAnimation",
            "x",
            samplingSpecs,
            keyXValues
        );
            
        var yAnimation = AddFloat32Animation(
            name + "YAnimation",
            "y",
            samplingSpecs,
            keyYValues
        );
            
        var zAnimation = AddFloat32Animation(
            name + "ZAnimation",
            "z",
            samplingSpecs,
            keyZValues
        );

        return new Triplet<GrBabylonJsAnimationOfFloat>(
            xAnimation,
            yAnimation,
            zAnimation
        );
    }
        
    //public Quad<GrBabylonJsFloat32Animation> AddComponentAnimations(string name, GrBabylonJsAnimationSpecs samplingSpecs, GrBabylonJsKeyFrameDictionary<Float64Tuple4D> keyPositions, GrBabylonJsKeyFrameOptimizationKind optimizationKind = GrBabylonJsKeyFrameOptimizationKind.Separate)
    //{
    //    var (keyXValues, keyYValues, keyZValues, keyWValues) = 
    //        keyPositions.SeparateComponents(optimizationKind);

    //    var xAnimation = AddFloat32Animation(
    //        name + "XAnimation",
    //        "x",
    //        samplingSpecs,
    //        keyXValues
    //    );
            
    //    var yAnimation = AddFloat32Animation(
    //        name + "YAnimation",
    //        "y",
    //        samplingSpecs,
    //        keyYValues
    //    );
            
    //    var zAnimation = AddFloat32Animation(
    //        name + "ZAnimation",
    //        "z",
    //        samplingSpecs,
    //        keyZValues
    //    );
            
    //    var wAnimation = AddFloat32Animation(
    //        name + "WAnimation",
    //        "w",
    //        samplingSpecs,
    //        keyWValues
    //    );

    //    return new Quad<GrBabylonJsFloat32Animation>(
    //        xAnimation,
    //        yAnimation,
    //        zAnimation,
    //        wAnimation
    //    );
    //}
        
    public GrBabylonJsAnimationGroup AddAnimationGroup(string name)
    {
        var sceneObject = 
            new GrBabylonJsAnimationGroup(name, this);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    public GrBabylonJsAnimationGroup AddAnimationGroup(string name, GrBabylonJsAnimationGroupProperties properties)
    {
        var sceneObject = 
            new GrBabylonJsAnimationGroup(name, this)
                .SetProperties(properties);

        ObjectList.Add(sceneObject);

        return sceneObject;
    }

    //public JArray GetAnimationKeyFramesJson()
    //{
    //    var array = new JArray();

    //    foreach (var animation in Animations)
    //    {
    //        var name = animation.ConstName + "KeyFrames";
    //        var value = animation.GetKeyFramesJson();

    //        array.Add(
    //            JObject.FromObject(new {animation = name}) (name, value)
    //        );
    //    }

    //    return array;
    //}

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Engine.GetAttributeValueCode();

        var optionsCode = 
            ObjectOptions.GetAttributeSetCode();

        yield return optionsCode;

    }
}
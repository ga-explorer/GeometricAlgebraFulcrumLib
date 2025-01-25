using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Atmospheric;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Directives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Pigments;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public class GrPovRayStatementList :
    GrPovRayCodeElementList<IGrPovRayStatement>
{
    public override string Separator 
        => Environment.NewLine + Environment.NewLine;


    public GrPovRayStatementList Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayStatementList Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    public GrPovRayStatementList Add(IGrPovRayStatement st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayStatementList Append(IGrPovRayStatement st)
    {
        CodeElementList.Add(st);

        return this;
    }

    public GrPovRayStatementList Prepend(IGrPovRayStatement st)
    {
        CodeElementList.Insert(0, st);

        return this;
    }

    public GrPovRayStatementList Insert(int index, IGrPovRayStatement st)
    {
        CodeElementList.Insert(index, st);

        return this;
    }
    
    
    public GrPovRayStatementList FreeCode(string code)
    {
        return Append(new GrPovRayFreeCode(code));
    }


    public GrPovRayStatementList Include(string fileName)
    {
        CodeElementList.Add(
            new GrPovRayIncludeDirective(fileName)
        );

        return this;
    }

    public GrPovRayStatementList Include(params string[] fileNameList)
    {
        CodeElementList.AddRange(
            fileNameList.Select(fileName => new GrPovRayIncludeDirective(fileName))
        );

        return this;
    }


    public GrPovRayStatementList Default(IGrPovRayPigment pigment)
    {
        var directive = new GrPovRayDefaultPigmentDirective(pigment);

        CodeElementList.Add(directive);

        return this;
    }
    
    public GrPovRayStatementList Default(IGrPovRayFinish finish)
    {
        var directive = new GrPovRayDefaultFinishDirective(finish);

        CodeElementList.Add(directive);

        return this;
    }
    
    public GrPovRayStatementList Default(IGrPovRayTexture texture)
    {
        var directive = new GrPovRayDefaultTextureDirective(texture);

        CodeElementList.Add(directive);

        return this;
    }


    public GrPovRayStatementList Declare<T>(string identifier, T element) where T : IGrPovRayRValue
    {
        var directive = new GrPovRayDeclareDirective<T>(identifier, element, false);

        CodeElementList.Add(directive);

        return this;
    }
    
    public GrPovRayStatementList Local<T>(string identifier, T element) where T : IGrPovRayRValue
    {
        var directive = new GrPovRayDeclareDirective<T>(identifier, element, true);

        CodeElementList.Add(directive);

        return this;
    }

    
    public GrPovRayStatementList DeclareCamera(string identifier, GrPovRayCamera camera)
    {
        return Declare<GrPovRayCameraValue>(identifier, camera);
    }
    
    public GrPovRayStatementList DeclareOrthographicCamera(string identifier, GrPovRayFullCameraProperties? properties = null)
    {
        var camera = GrPovRayCamera.Orthographic();
        
        if (properties is not null)
            camera.SetProperties(properties);

        return Declare<GrPovRayCameraValue>(identifier, camera);
    }
    
    public GrPovRayStatementList DeclareOrthographicCamera(string identifier, string baseCameraName, GrPovRayFullCameraProperties? properties = null)
    {
        var camera = GrPovRayCamera.Orthographic(baseCameraName);
        
        if (properties is not null)
            camera.SetProperties(properties);

        return Declare<GrPovRayCameraValue>(identifier, camera);
    }

    public GrPovRayStatementList DeclarePerspectiveCamera(string identifier, GrPovRayFullCameraProperties? properties = null)
    {
        var camera = GrPovRayCamera.Perspective();
        
        if (properties is not null)
            camera.UpdateProperties(properties);

        return Declare<GrPovRayCameraValue>(identifier, camera);
    }
    
    public GrPovRayStatementList DeclarePerspectiveCamera(string identifier, string baseCameraName, GrPovRayFullCameraProperties? properties = null)
    {
        var camera = GrPovRayCamera.Perspective(baseCameraName);
        
        if (properties is not null)
            camera.UpdateProperties(properties);

        return Declare<GrPovRayCameraValue>(identifier, camera);
    }


    public GrPovRayStatementList DeclareObject(string identifier, GrPovRayObject obj)
    {
        return Declare<GrPovRayObjectValue>(identifier, obj);
    }

    
    public GrPovRayStatementList DeclareFinish(string identifier, GrPovRayFinishProperties? properties = null)
    {
        var finish = new GrPovRayFinish();
        
        if (properties is not null)
            finish.SetProperties(properties);

        return Declare<GrPovRayFinishValue>(identifier, finish);
    }
    
    public GrPovRayStatementList DeclareFinish(string identifier, string baseFinishName, GrPovRayFinishProperties? properties = null)
    {
        var finish = new GrPovRayFinish(baseFinishName);
        
        if (properties is not null)
            finish.SetProperties(properties);

        return Declare<GrPovRayFinishValue>(identifier, finish);
    }


    public GrPovRayMaterial DeclareMaterial(string identifier, GrPovRayMaterial material)
    {
        Declare<GrPovRayMaterialValue>(identifier, material);

        return GrPovRayMaterial.Create(identifier);
    }

    public GrPovRayMaterial DeclareMaterial(string identifier, GrPovRayColorValue color)
    {
        var material = GrPovRayMaterial.Create(color);

        return DeclareMaterial(identifier, material);
    }
    
    public GrPovRayMaterial DeclareMaterial(string identifier, GrPovRayColorValue color, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(color, finishProperties);

        return DeclareMaterial(identifier, material);
    }
    
    public GrPovRayMaterial DeclareMaterial(string identifier, GrPovRayColorValue color, IGrPovRayFinish finishProperties)
    {
        var material = GrPovRayMaterial.Create(color, finishProperties);

        return DeclareMaterial(identifier, material);
    }

    public GrPovRayMaterial DeclareMaterial(string identifier, GrPovRayColorValue color, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(color, baseFinishName, finishProperties);

        return DeclareMaterial(identifier, material);
    }
    
    public GrPovRayMaterial DeclareMaterial(string identifier, IGrPovRayPigment pigment)
    {
        var material = GrPovRayMaterial.Create(pigment);

        return DeclareMaterial(identifier, material);
    }
    
    public GrPovRayMaterial DeclareMaterial(string identifier, IGrPovRayPigment pigment, GrPovRayFinishProperties finishProperties)
    {
        var material = GrPovRayMaterial.Create(pigment, finishProperties);

        return DeclareMaterial(identifier, material);
    }
    
    public GrPovRayMaterial DeclareMaterial(string identifier, IGrPovRayPigment pigment, IGrPovRayFinish finishProperties)
    {
        var material = GrPovRayMaterial.Create(pigment, finishProperties);

        return DeclareMaterial(identifier, material);
    }

    public GrPovRayMaterial DeclareMaterial(string identifier, IGrPovRayPigment pigment, string baseFinishName, GrPovRayFinishProperties? finishProperties = null)
    {
        var material = GrPovRayMaterial.Create(pigment, baseFinishName, finishProperties);

        return DeclareMaterial(identifier, material);
    }

    
    public GrPovRayStatementList UnDefine(string identifier)
    {
        CodeElementList.Add(
            new GrPovRayUnDefineDirective(identifier)
        );

        return this;
    }

    public GrPovRayForDirective For(string loopVariable, GrPovRayFloat32Value startValue, GrPovRayFloat32Value endValue)
    {
        var st = new GrPovRayForDirective(loopVariable, startValue, endValue, 1);

        CodeElementList.Add(st);

        return st;
    }

    public GrPovRayForDirective For(string loopVariable, GrPovRayFloat32Value startValue, GrPovRayFloat32Value endValue, GrPovRayFloat32Value stepValue)
    {
        var st = new GrPovRayForDirective(loopVariable, startValue, endValue, stepValue);

        CodeElementList.Add(st);

        return st;
    }


    public GrPovRayStatementList AtmosphericBackground(GrPovRayColorValue color)
    {
        CodeElementList.Add(
            GrPovRayAtmosphericBackground.Create(color)
        );

        return this;
    }
    
    public GrPovRayAtmosphericSkySphere AtmosphericSkySphere(IGrPovRayPigment pigment, GrPovRayColorValue? emissionColor = null)
    {
        var element = GrPovRayAtmosphericSkySphere.Create(pigment, emissionColor);

        CodeElementList.Add(element);

        return element;
    }

    
    public override string GetPovRayCode()
    {
        return CodeElementList.Select(
            v => v.GetPovRayCode() + Environment.NewLine
        ).Concatenate(Environment.NewLine);
    }
}
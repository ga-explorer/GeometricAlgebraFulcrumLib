using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public static class BabylonJsGuiUtils
{
    
    public static GrBabylonJsGuiTextBlock AddGuiTextBlock(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsStringValue text, GrBabylonJsGuiTextBlockProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiTextBlock(name, container)
            {
                Text = text
            }.SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiImage AddGuiImage(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsStringValue url, GrBabylonJsGuiImageProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiImage(name, container)
            {
                Url = url
            }.SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiLine AddGuiLine(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiLineProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiLine(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiMultiLine AddGuiMultiLine(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiMultiLineProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiMultiLine(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }
    
    public static GrBabylonJsGuiStackPanel AddGuiStackPanel(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiStackPanelProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiStackPanel(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

}
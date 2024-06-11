using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public static class BabylonJsGuiUtils
{
    
    public static GrBabylonJsGuiTextBlock AddGuiTextBlock(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsStringValue text, GrBabylonJsGuiTextBlock.GuiTextBlockProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiTextBlock(name, container)
            {
                Text = text
            }.SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiImage AddGuiImage(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsStringValue url, GrBabylonJsGuiImage.GuiImageProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiImage(name, container)
            {
                Url = url
            }.SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiLine AddGuiLine(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiLine.GuiLineProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiLine(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

    public static GrBabylonJsGuiMultiLine AddGuiMultiLine(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiMultiLine.GuiMultiLineProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiMultiLine(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }
    
    public static GrBabylonJsGuiStackPanel AddGuiStackPanel(this IGrBabylonJsGuiControlContainer container, string name, GrBabylonJsGuiStackPanel.GuiStackPanelProperties? properties)
    {
        var sceneObject = 
            new GrBabylonJsGuiStackPanel(name, container)
                .SetProperties(properties);

        container.ControlList.Add(sceneObject);

        return sceneObject;
    }

}
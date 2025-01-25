namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs;

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
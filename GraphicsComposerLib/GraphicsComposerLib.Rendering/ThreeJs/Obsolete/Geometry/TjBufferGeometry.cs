namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// A representation of mesh, line, or point geometry. Includes vertex
    /// positions, face indices, normals, colors, UVs, and custom attributes
    /// within buffers, reducing the cost of passing all this data to the GPU.
    /// https://threejs.org/docs/#api/en/core/BufferGeometry
    /// </summary>
    public class TjBufferGeometry :
        TjBufferGeometryBase
    {
        public override string JavaScriptClassName 
            => "BufferGeometry";
    }
}
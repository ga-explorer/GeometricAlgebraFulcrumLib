namespace GraphicsComposerLib.Xeogl.Geometry
{
    /// <summary>
    /// A Geometry defines a mesh for attached Meshes.
    /// </summary>
    /// <remarks>
    /// Geometries may be automatically quantized to reduce memory and GPU bus
    /// usage. Usually, geometry attributes such as positions and normals are
    /// stored as 32-bit floating-point numbers. Quantization compresses those
    /// attributes to 16-bit integers represented on a scale between the minimum
    /// and maximum values. Decompression is then done on the GPU, via a simple
    /// matrix multiplication in the vertex shader.
    /// Since each normal vector is oct-encoded into two 8-bit unsigned integers,
    /// this can cause them to lose precision, which may affect the accuracy of
    /// any operations that rely on them being perfectly perpendicular to their
    /// surfaces. In such cases, you may need to disable compression for your
    /// geometries and models.
    /// Geometries are automatically combined into the same vertex buffer objects
    /// (VBOs) so that we reduce the number of VBO binds done by WebGL on each
    /// frame. VBO binds are expensive, so this really makes a difference when
    /// we have large numbers of Meshes that share similar Materials (as is often
    /// the case in CAD rendering).
    /// Since combined VBOs need to be rebuilt whenever we destroy a Geometry,
    /// we can disable this optimization for individual Models and Geometries
    /// when we know that we'll be continually creating and destroying them.
    /// See Also: http://xeogl.org/docs/classes/Geometry.html
    /// </remarks>
    public abstract class XeoglGeometry 
        : XeoglComponent, IXeoglGeometry
    {
        /// <summary>
        /// Stores positions, colors, normals and UVs in quantized and oct-encoded
        /// formats for reduced memory footprint and GPU bus usage.
        /// </summary>
        public bool Quantized { get; set; }

        /// <summary>
        /// Combines positions, colors, normals and UVs into the same WebGL
        /// vertex buffers with other Geometries, in order to reduce the number
        /// of buffer binds performed per frame.
        /// </summary>
        public bool Combined { get; set; }

        /// <summary>
        /// When a Mesh renders this Geometry as wire frame, this indicates the
        /// threshold angle (in degrees) between the face normals of adjacent
        /// triangles below which the edge is discarded.
        /// </summary>
        public double EdgeThreshold { get; set; } 
            = 2;


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("quantized", Quantized, false)
                .SetAttributeValue("combined", Combined, false)
                .SetAttributeValue("edgeThreshold", EdgeThreshold, 2);
        }
    }
}
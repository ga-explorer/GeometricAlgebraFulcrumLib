using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D.Immutable;

namespace NumericalGeometryLib.Accelerators.Grids.Space3D
{
    /// <summary>
    /// This class can be used for collecting information about a Grid Cell
    /// and its child geometric objects for debugging and statistics
    /// </summary>
    public sealed class AccGridCellInfo3D
    {
        public int IndexX { get; }

        public int IndexY { get; }

        public int IndexZ { get; }

        public bool IsEmpty { get; }

        public IEnumerable<IFiniteGeometricShape3D> GeometricObjects { get; }

        public BoundingBox3D BoundingBox { get; }


        internal AccGridCellInfo3D(int indexX, int indexY, int indexZ, BoundingBox3D boundingBox)
        {
            IndexX = indexX;
            IndexY = indexY;
            IndexZ = indexZ;
            BoundingBox = boundingBox;
            IsEmpty = true;
            GeometricObjects = Enumerable.Empty<IFiniteGeometricShape3D>();
        }

        internal AccGridCellInfo3D(int indexX, int indexY, int indexZ, BoundingBox3D boundingBox, IEnumerable<IFiniteGeometricShape3D> geometricObjects)
        {
            IndexX = indexX;
            IndexY = indexY;
            IndexZ = indexZ;
            BoundingBox = boundingBox;
            IsEmpty = false;
            GeometricObjects = geometricObjects;
        }
    }
}
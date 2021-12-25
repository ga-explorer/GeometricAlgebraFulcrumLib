using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space2D.Immutable;

namespace NumericalGeometryLib.Accelerators.Grids.Space2D
{
    /// <summary>
    /// This class can be used for collecting information about a Grid Cell
    /// and its child geometric objects for debugging and statistics
    /// </summary>
    public sealed class AccGridCellInfo2D
    {
        public int IndexX { get; }

        public int IndexY { get; }

        public bool IsEmpty { get; }

        public IEnumerable<IFiniteGeometricShape2D> GeometricObjects { get; }

        public BoundingBox2D BoundingBox { get; }


        internal AccGridCellInfo2D(int indexX, int indexY, BoundingBox2D boundingBox)
        {
            IndexX = indexX;
            IndexY = indexY;
            BoundingBox = boundingBox;
            IsEmpty = true;
            GeometricObjects = Enumerable.Empty<IFiniteGeometricShape2D>();
        }

        internal AccGridCellInfo2D(int indexX, int indexY, BoundingBox2D boundingBox, IEnumerable<IFiniteGeometricShape2D> geometricObjects)
        {
            IndexX = indexX;
            IndexY = indexY;
            BoundingBox = boundingBox;
            IsEmpty = false;
            GeometricObjects = geometricObjects;
        }
    }
}
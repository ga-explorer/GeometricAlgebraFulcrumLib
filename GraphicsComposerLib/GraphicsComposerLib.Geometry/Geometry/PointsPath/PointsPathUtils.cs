using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space2D;
using GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space3D;
using GraphicsComposerLib.Geometry.Geometry.PointsMesh;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space3D;

namespace GraphicsComposerLib.Geometry.Geometry.PointsPath
{
    public static class PointsPathUtils
    {
        public static Pair<ITuple2D> GetPointsPair(this IPointsPath2D path, int index1, int index2)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            return new Pair<ITuple2D>(path[index1], path[index2]);
        }

        public static Pair<ITuple2D> GetPointsPair(this IPointsPath2D path, Pair<int> pointIndexPair)
        {
            return new Pair<ITuple2D>(
                path[pointIndexPair.Item1], 
                path[pointIndexPair.Item2]
            );
        }


        public static Pair<ITuple3D> GetPointsPair(this IPointsPath3D path, int index1, int index2)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            return new Pair<ITuple3D>(path[index1], path[index2]);
        }

        public static Pair<ITuple3D> GetPointsPair(this IPointsPath3D path, Pair<int> pointIndexPair)
        {
            return new Pair<ITuple3D>(
                path[pointIndexPair.Item1], 
                path[pointIndexPair.Item2]
            );
        }


        public static ArrayPointsPath2D ToArrayPointsPath2D(this ITuple2D[] pathPoints)
        {
            return new ArrayPointsPath2D(pathPoints);
        }

        public static ArrayPointsPath2D ToArrayPointsPath2D(this IEnumerable<ITuple2D> pathPoints)
        {
            return new ArrayPointsPath2D(pathPoints);
        }

        public static CircularPointsPath2D ToCircularPointsPath2D(this IPeriodicSequence1D<double> parameterSequence, double radius)
        {
            return new CircularPointsPath2D(new Tuple2D(0, 0), radius, parameterSequence);
        }

        public static CircularPointsPath2D ToCircularPointsPath2D(this IPeriodicSequence1D<double> parameterSequence, ITuple2D center, double radius)
        {
            return new CircularPointsPath2D(center, radius, parameterSequence);
        }

        public static ConstantPointsPath2D ToConstantPointsPath2D(this ITuple2D point, int count)
        {
            return new ConstantPointsPath2D(count, point);
        }

        public static LinearPointsPath2D ToLinearPointsPath2D(this IPeriodicSequence1D<double> parameterSequence, ITuple2D point1, ITuple2D point2)
        {
            return new LinearPointsPath2D(point1, point2, parameterSequence);
        }

        public static ListPointsPath2D ToListPointsPath2D(this IReadOnlyList<ITuple2D> pathPoints)
        {
            return new ListPointsPath2D(pathPoints);
        }

        public static ListPointsPath2D ToListPointsPath2D(this IEnumerable<ITuple2D> pathPoints)
        {
            return new ListPointsPath2D(pathPoints);
        }

        public static MultiplexedPointsPath2D ToMultiplexedPointsPath2D(this IEnumerable<IPointsPath2D> sequencesList, IEnumerable<int> sequenceSelectionList)
        {
            return new MultiplexedPointsPath2D(sequencesList.ToArray(), sequenceSelectionList);
        }

        public static MultiplexedPointsPath2D ToMultiplexedPointsPath2D(this IPointsPath2D[] sequencesList, IEnumerable<int> sequenceSelectionList)
        {
            return new MultiplexedPointsPath2D(sequencesList, sequenceSelectionList);
        }

        public static ParametricPointsPath2D ToParametricPointsPath2D(this IPeriodicSequence1D<double> parameterSequence, Func<double, ITuple2D> mappingFunc)
        {
            return new ParametricPointsPath2D(
                parameterSequence,
                mappingFunc
            );
        }


        public static ReversedPointsPath2D ToReversedPath(this IPointsPath2D basePath)
        {
            return new ReversedPointsPath2D(basePath);
        }

        public static PartialPointsPath2D GetPartialPath(this IPointsPath2D basePath, IndexMapRange1D baseIndexRange)
        {
            return new PartialPointsPath2D(basePath, baseIndexRange);
        }

        public static PartialPointsPath2D GetPartialPath(this IPointsPath2D basePath, int firstIndex)
        {
            return new PartialPointsPath2D(basePath, firstIndex);
        }

        public static PartialPointsPath2D GetPartialPath(this IPointsPath2D basePath, int firstIndex, int count)
        {
            return new PartialPointsPath2D(basePath, firstIndex, count);
        }

        public static PartialPointsPath2D GetPartialPath(this IPointsPath2D basePath, int firstIndex, int count, bool moveForward)
        {
            return new PartialPointsPath2D(basePath, firstIndex, count, moveForward);
        }

        public static LerpPointsPath2D GetLerpPath(this LerpPathsMesh2D baseMesh, double paramValue)
        {
            return new LerpPointsPath2D(baseMesh, paramValue);
        }

        public static MappedPointsPath2D MapToPath2D(this IPointsPath2D basePath, Func<ITuple2D, ITuple2D> mapping)
        {
            return new MappedPointsPath2D(basePath, mapping);
        }

        public static Mapped3DPointsPath2D MapToPath2D(this IPointsPath3D basePath, Func<ITuple3D, ITuple2D> mapping)
        {
            return new Mapped3DPointsPath2D(basePath, mapping);
        }

        public static PointsMeshSlicePointsPath2D GetSlicePath(this IPointsMesh2D baseMesh, int slicedDimension, int sliceIndex)
        {
            return new PointsMeshSlicePointsPath2D(baseMesh, slicedDimension, sliceIndex);
        }

        public static PointsMeshSubsetPointsPath2D GetPointsSubsetPath(this IPointsMesh2D baseMesh, IIndexMap1D2D indexMapping)
        {
            return new PointsMeshSubsetPointsPath2D(baseMesh, indexMapping);
        }

        public static PointsMeshSubsetPointsPath2D GetPointsSubsetPath(this IPointsMesh2D baseMesh, int pointsCount, Func<int, Pair<int>> indexMapping)
        {
            return new PointsMeshSubsetPointsPath2D(
                baseMesh, 
                new ComputedIndexMap1DTo2D(pointsCount, indexMapping)
            );
        }


        public static ConstantPointsPath3D ToConstantPointsPath3D(this ITuple3D point, int count)
        {
            return new ConstantPointsPath3D(count, point);
        }

        public static ArrayPointsPath3D ArrayPointsPath3D(this ITuple3D[] pathPoints)
        {
            return new ArrayPointsPath3D(pathPoints);
        }

        public static ArrayPointsPath3D ToArrayPointsPath3D(this IEnumerable<ITuple3D> pathPoints)
        {
            return new ArrayPointsPath3D(pathPoints);
        }

        public static LinearPointsPath3D ToLinearPointsPath3D(this IPeriodicSequence1D<double> parameterSequence, ITuple3D point1, ITuple3D point2)
        {
            return new LinearPointsPath3D(point1, point2, parameterSequence);
        }

        public static ParametricPointsPath3D ToParametricPointsPath3D(this IPeriodicSequence1D<double> parameterSequence, Func<double, ITuple3D> mappingFunc)
        {
            return new ParametricPointsPath3D(
                parameterSequence,
                mappingFunc
            );
        }

        public static PlanarXyPointsPath3D ToXyPath3D(this IPointsPath2D basePath)
        {
            return new PlanarXyPointsPath3D(basePath, 0);
        }

        public static PlanarXyPointsPath3D ToXyPath3D(this IPointsPath2D basePath, double valueZ)
        {
            return new PlanarXyPointsPath3D(basePath, valueZ);
        }

        public static PlanarYzPointsPath3D ToYzPath3D(this IPointsPath2D basePath)
        {
            return new PlanarYzPointsPath3D(basePath, 0);
        }

        public static PlanarYzPointsPath3D ToYzPath3D(this IPointsPath2D basePath, double valueX)
        {
            return new PlanarYzPointsPath3D(basePath, valueX);
        }

        public static PlanarZxPointsPath3D ToZxPath3D(this IPointsPath2D basePath)
        {
            return new PlanarZxPointsPath3D(basePath, 0);
        }

        public static PlanarZxPointsPath3D ToZxPath3D(this IPointsPath2D basePath, double valueY)
        {
            return new PlanarZxPointsPath3D(basePath, valueY);
        }


        public static ReversedPointsPath3D ToReversedPath(this IPointsPath3D basePath)
        {
            return new ReversedPointsPath3D(basePath);
        }

        public static PartialPointsPath3D GetPartialPath(this IPointsPath3D basePath, IndexMapRange1D baseIndexRange)
        {
            return new PartialPointsPath3D(basePath, baseIndexRange);
        }

        public static PartialPointsPath3D GetPartialPath(this IPointsPath3D basePath, int firstIndex)
        {
            return new PartialPointsPath3D(basePath, firstIndex);
        }

        public static PartialPointsPath3D GetPartialPath(this IPointsPath3D basePath, int firstIndex, int count)
        {
            return new PartialPointsPath3D(basePath, firstIndex, count);
        }

        public static PartialPointsPath3D GetPartialPath(this IPointsPath3D basePath, int firstIndex, int count, bool moveForward)
        {
            return new PartialPointsPath3D(basePath, firstIndex, count, moveForward);
        }

        public static LerpPointsPath3D GetLerpPath(this LerpPathsMesh3D baseMesh, double paramValue)
        {
            return new LerpPointsPath3D(baseMesh, paramValue);
        }

        public static MappedPointsPath3D MapToPath3D(this IPointsPath3D basePath, Func<ITuple3D, ITuple3D> mapping)
        {
            return new MappedPointsPath3D(basePath, mapping);
        }

        public static Mapped2DPointsPath3D MapToPath3D(this IPointsPath2D basePath, Func<ITuple2D, ITuple3D> mapping)
        {
            return new Mapped2DPointsPath3D(basePath, mapping);
        }

        public static PointsMeshSlicePointsPath3D GetSlicePath(this IPointsMesh3D baseMesh, int slicedDimension, int sliceIndex)
        {
            return new PointsMeshSlicePointsPath3D(baseMesh, slicedDimension, sliceIndex);
        }

        public static PointsMeshSubsetPointsPath3D GetPointsSubsetPath(this IPointsMesh3D baseMesh, IIndexMap1DTo2D indexMapping)
        {
            return new PointsMeshSubsetPointsPath3D(baseMesh, indexMapping);
        }

        public static PointsMeshSubsetPointsPath3D GetPointsSubsetPath(this IPointsMesh3D baseMesh, int pointsCount, Func<int, Pair<int>> indexMapping)
        {
            return new PointsMeshSubsetPointsPath3D(
                baseMesh, 
                new ComputedIndexMap1DTo2D(pointsCount, indexMapping)
            );
        }
    }
}

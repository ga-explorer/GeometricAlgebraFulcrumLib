using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Geometry.Geometry.PointsPath;
using GraphicsComposerLib.Geometry.Geometry.PointsPath.Space2D;

namespace GraphicsComposerLib.Geometry.Geometry.PathsMesh.Space2D
{
    /// <summary>
    /// This class represents a mesh from an interpolation of two 2D point paths
    /// </summary>
    public sealed class LerpPathsMesh2D
        : IPathsMesh2D
    {
        public IPointsPath2D Path1 { get; }

        public IPointsPath2D Path2 { get; }

        public int Count { get; }

        public IPointsPath2D this[int index]
        {
            get
            {
                var paramValue = index.Mod(Count) / (double)(Count - 1);

                return new LerpPointsPath2D(this, paramValue);
            }
        }

        public bool IsBasic
            => true;

        public bool IsOperator
            => false;

        public int PathPointsCount
            => Path1.Count;

        public int MeshPointsCount
            => PathPointsCount * Count;


        public LerpPathsMesh2D(IPointsPath2D firstPath, IPointsPath2D lastPath, int pathsCount)
        {
            if (ReferenceEquals(firstPath, null))
                throw new ArgumentNullException(nameof(firstPath));

            if (ReferenceEquals(lastPath, null))
                throw new ArgumentNullException(nameof(lastPath));

            if (firstPath.Count != lastPath.Count)
                throw new ArgumentException("Paths points count don't match");

            Path1 = firstPath;
            Path2 = lastPath;
            Count = pathsCount;
        }


        public IEnumerator<IPointsPath2D> GetEnumerator()
        {
            var delta = 1.0d / (Count - 1);
            for (var index = 0; index < Count; index++)
                yield return new LerpPointsPath2D(
                    this,
                    index * delta
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
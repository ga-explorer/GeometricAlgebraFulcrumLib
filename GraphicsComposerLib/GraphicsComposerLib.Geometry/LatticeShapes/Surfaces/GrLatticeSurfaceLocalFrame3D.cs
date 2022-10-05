using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GraphicsComposerLib.Geometry.LatticeShapes.Surfaces
{
    public sealed class GrLatticeSurfaceLocalFrame3D : 
        IGraphicsVertex3D
    {
        public GrLatticeSurface3D ParentSurface { get; }

        public GrLatticeSurfaceList3D ParentSurfaceList 
            => ParentSurface.ParentList;
        
        public int Index { get; internal set; } = -1;

        public HashSet<Pair<int>> LatticeIndexSet { get; }
            = new HashSet<Pair<int>>();

        private Tuple3D _point;
        public Tuple3D Point
        {
            get => _point;
            internal set
            {
                if (ParentSurface.IsReady)
                    throw new InvalidOperationException();

                _point = value;
            }
        }

        public double Item1 => _point.X;
        
        public double Item2 => _point.Y;

        public double Item3 => _point.Z;
        
        public double X => _point.X;
        
        public double Y => _point.Y;
        
        public double Z => _point.Z;

        public Triplet<double> PointTriplet 
            => new Triplet<double>(Point.X, Point.Y, Point.Z);

        public GrNormal3D Normal { get; }
            = new GrNormal3D();
        
        public Pair<double> ParameterValue { get; set; }

        public Color Color { get; set; }

        public bool HasParameterValue 
            => true;

        public bool HasNormal 
            => true;

        public bool HasColor 
            => true;
        
        public GraphicsVertexDataKind3D DataKind 
            => GraphicsVertexDataKind3D.NormalTextureColorData;


        internal GrLatticeSurfaceLocalFrame3D([NotNull] GrLatticeSurface3D parentSurface, [NotNull] Pair<int> uvIndex, [NotNull] ITriplet<double> pointTriplet)
        {
            ParentSurface = parentSurface;

            _point = new Tuple3D(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

            LatticeIndexSet.Add(uvIndex);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return _point.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasPoint(double x, double y, double z)
        {
            return Point.X == x && 
                   Point.Y == y && 
                   Point.Z == z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasPoint(ITriplet<double> pointTriplet)
        {
            return Point.X == pointTriplet.Item1 && 
                   Point.Y == pointTriplet.Item2 && 
                   Point.Z == pointTriplet.Item3;
        }
        
        internal Tuple2D ComputeTextureUv()
        {
            var textureUv = Tuple2D.Zero;

            foreach (var (indexU, indexV) in LatticeIndexSet)
            {
                textureUv += ParentSurface.GetLatticeTextureUv(indexU, indexV);
            }
            
            textureUv /= LatticeIndexSet.Count;

            ParameterValue = new Pair<double>(textureUv.Item1, textureUv.Item2);

            return textureUv;
        }

        //internal GrSurfaceLocalFrame3D ComputeLocalFrame()
        //{
        //    // TODO: this method is not correct around singular points

        //    var tangentU = Tuple3D.Zero;
        //    var tangentV = Tuple3D.Zero;

        //    foreach (var (indexU, indexV) in LatticeIndexSet)
        //    {
        //        tangentU += ParentSurface.GetLatticeTangentU(indexU, indexV);
        //        tangentV += ParentSurface.GetLatticeTangentV(indexU, indexV);
        //    }

        //    var normal = tangentU.VectorCross(tangentV).ToUnitVector();
        //    tangentU = tangentU.ToUnitVector();
        //    tangentV = tangentV.ToUnitVector();

        //    LocalFrame = new GrSurfaceLocalFrame3D(tangentU, tangentV, normal);

        //    return LocalFrame;
        //}

    }
}

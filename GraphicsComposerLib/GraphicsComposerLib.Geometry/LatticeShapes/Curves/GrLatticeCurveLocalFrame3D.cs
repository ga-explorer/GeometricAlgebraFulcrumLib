using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GraphicsComposerLib.Geometry.LatticeShapes.Curves
{
    public sealed class GrLatticeCurveLocalFrame3D : 
        IGraphicsCurveLocalFrame3D
    {
        public GrLatticeCurve3D ParentCurve { get; }

        public GrLatticeCurveList3D ParentCurveList 
            => ParentCurve.ParentList;
        
        public int Index { get; internal set; } = -1;

        public HashSet<int> LatticeIndexSet { get; }
            = new HashSet<int>();

        private Float64Tuple3D _point;
        public Float64Tuple3D Point
        {
            get => _point;
            internal set
            {
                if (ParentCurve.IsReady)
                    throw new InvalidOperationException();

                _point = value;
            }
        }

        public double Item1 
            => _point.X;
        
        public double Item2 
            => _point.Y;

        public double Item3 
            => _point.Z;
        
        public double X 
            => _point.X;
        
        public double Y 
            => _point.Y;
        
        public double Z 
            => _point.Z;

        public bool IsValid()
        {
            return _point.IsValid();
        }

        public Triplet<double> PointTriplet 
            => new Triplet<double>(Point.X, Point.Y, Point.Z);

        public GrNormal3D Normal1 { get; }
            = new GrNormal3D();
        
        public GrNormal3D Normal2 { get; }
            = new GrNormal3D();

        public Float64Tuple3D Tangent { get; }

        public double ParameterValue { get; private set; }

        public Color Color { get; set; }


        public GrLatticeCurveLocalFrame3D([NotNull] GrLatticeCurve3D parentCurve, int uIndex, [NotNull] ITriplet<double> pointTriplet)
        {
            ParentCurve = parentCurve;

            _point = new Float64Tuple3D(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

            LatticeIndexSet.Add(uIndex);
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
        
        internal double ComputeTextureU()
        {
            return LatticeIndexSet.Average(indexU => ParentCurve.GetLatticeTextureU(indexU));
        }

        //internal GrLatticeSurfaceLocalFrame3D ComputeLocalFrame()
        //{
        //    // TODO: this method is not correct around singular points

        //    var tangentU = Tuple3D.Zero;
        //    var tangentV = Tuple3D.Zero;

        //    foreach (var (indexU, indexV) in LatticeIndexSet)
        //    {
        //        tangentU += ParentCurve.GetTangentU(indexU, indexV);
        //        tangentV += ParentCurve.GetTangentV(indexU, indexV);
        //    }

        //    var normal = tangentU.VectorCross(tangentV).ToUnitVector();
        //    tangentU = tangentU.ToUnitVector();
        //    tangentV = tangentV.ToUnitVector();

        //    LocalFrame = new GrLatticeSurfaceLocalFrame3D(tangentU, tangentV, normal);

        //    return LocalFrame;
        //}

    }
}

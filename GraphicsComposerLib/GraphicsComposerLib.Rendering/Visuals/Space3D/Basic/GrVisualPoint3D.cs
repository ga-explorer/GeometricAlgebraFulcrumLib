﻿using GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public sealed class GrVisualPoint3D :
        GrVisualElement3D,
        ITuple3D
    {
        public double Item1 => Position.X;
        
        public double Item2 => Position.Y;
        
        public double Item3 => Position.Z;
        
        public double X => Position.X;
        
        public double Y => Position.Y;
        
        public double Z => Position.Z;

        public GrVisualSurfaceThickStyle3D Style { get; set; } 

        public ITuple3D Position { get; } 


        public GrVisualPoint3D(string name, ITuple3D position) 
            : base(name)
        {
            Position = position;
        }


        public bool IsValid()
        {
            return Position.IsValid();
        }
    }
}
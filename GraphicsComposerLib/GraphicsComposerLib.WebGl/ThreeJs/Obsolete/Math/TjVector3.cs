using System.Diagnostics;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.WebGl.ThreeJs.Obsolete.Math
{
    /// <summary>
    /// Class representing a 3D vector. A 3D vector is an ordered triplet
    /// of numbers (labeled x, y, and z), which can be used to represent
    /// a number of things, such as:
    /// - A point in 3D space.
    /// - A direction and length in 3D space. In three.js the length will always be the Euclidean distance (straight-line distance) from (0, 0, 0) to (x, y, z) and the direction is also measured from (0, 0, 0) towards (x, y, z).
    /// - Any arbitrary ordered triplet of numbers.
    /// There are other things a 3D vector can be used to represent, such as momentum vectors and so on, however these are the most common uses in three.js.
    /// https://threejs.org/docs/#api/en/math/Vector3
    /// </summary>
    public sealed class TjVector3 :
        TjComponentSimple,
        ITuple3D
    {
        public override string JavaScriptClassName 
            => "Vector3";

        public double X { get; }
        
        public double Y { get; }
        
        public double Z { get; }

        public double Item1 
            => X;

        public double Item2 
            => Y;

        public double Item3 
            => Z;

        public bool IsValid
            => !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               !double.IsNaN(Z);

        public bool IsInvalid 
            => !IsValid;


        public TjVector3()
        {
            X = 0d;
            Y = 0d;
            Z = 0D;
        }

        public TjVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid);
        }

        public TjVector3(ITuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;

            Debug.Assert(IsValid);
        }


        protected override string GetConstructorArgumentsText()
        {
            return $"{X:G}, {Y:G}, {Z:G}";
        }

        protected override string GetSetMethodArgumentsText()
        {
            return $"{X:G}, {Y:G}, {Z:G}";
        }
    }
}

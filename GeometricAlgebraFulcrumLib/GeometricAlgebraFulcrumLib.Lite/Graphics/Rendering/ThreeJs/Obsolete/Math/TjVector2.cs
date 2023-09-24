using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Math
{
    /// <summary>
    /// Class representing a 2D vector. A 2D vector is an ordered pair of numbers
    /// (labeled x and y), which can be used to represent a number of things, such as:
    /// - A point in 2D space (i.e. a position on a plane).
    /// - A direction and length across a plane. In three.js the length will always
    ///   be the Euclidean distance (straight-line distance) from (0, 0) to (x, y) and
    ///   the direction is also measured from (0, 0) towards (x, y).
    /// - Any arbitrary ordered pair of numbers.
    /// There are other things a 2D vector can be used to represent, such as
    /// momentum vectors, complex numbers and so on, however these are the most common
    /// uses in three.js.
    /// https://threejs.org/docs/#api/en/math/Vector2
    /// </summary>
    public sealed class TjVector2 :
        TjComponentSimple,
        IFloat64Vector2D
    {
        public override string JavaScriptClassName 
            => "Vector2";
        
        public int VSpaceDimensions 
            => 2;

        public Float64Scalar X { get; }
        
        public Float64Scalar Y { get; }

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;

        public bool IsValid() =>
            !double.IsNaN(X) &&
            !double.IsNaN(Y);


        public TjVector2()
        {
            X = 0d;
            Y = 0d;
        }

        public TjVector2(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        public TjVector2(IFloat64Vector2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;

            Debug.Assert(IsValid());
        }


        protected override string GetConstructorArgumentsText()
        {
            return $"{X:G}, {Y:G}";
        }

        protected override string GetSetMethodArgumentsText()
        {
            return $"{X:G}, {Y:G}";
        }
    }
}
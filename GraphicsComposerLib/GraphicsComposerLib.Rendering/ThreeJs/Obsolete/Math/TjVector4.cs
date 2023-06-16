using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Math
{
    /// <summary>
    /// Class representing a 4D vector. A 4D vector is an ordered quadruplet of
    /// numbers (labeled x, y, z, and w), which can be used to represent a number
    /// of things, such as:
    /// - A point in 4D space.
    /// - A direction and length in 4D space. In three.js the length will always
    ///   be the Euclidean distance (straight-line distance) from (0, 0, 0, 0) to
    ///   (x, y, z, w) and the direction is also measured from (0, 0, 0, 0) towards
    ///   (x, y, z, w).
    /// - Any arbitrary ordered quadruplet of numbers.
    /// There are other things a 4D vector can be used to represent, however these
    /// are the most common uses in three.js.
    /// https://threejs.org/docs/#api/en/math/Vector4
    /// </summary>
    public sealed class TjVector4 :
        TjComponentSimple,
        IFloat64Tuple4D
    {
        public override string JavaScriptClassName 
            => "Vector4";

        public Float64Scalar X { get; }
        
        public Float64Scalar Y { get; }
        
        public Float64Scalar Z { get; }

        public Float64Scalar W { get; }
        
        public int VSpaceDimensions 
            => 4;

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;

        public double Item3 
            => Z.Value;

        public double Item4 
            => W.Value;

        public bool IsValid() =>
            !double.IsNaN(X) &&
            !double.IsNaN(Y) &&
            !double.IsNaN(Z) &&
            !double.IsNaN(W);


        public TjVector4()
        {
            X = 0d;
            Y = 0d;
            Z = 0d;
            W = 0d;
        }

        public TjVector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;

            Debug.Assert(IsValid());
        }

        public TjVector4(IFloat64Tuple4D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
            W = tuple.W;

            Debug.Assert(IsValid());
        }


        protected override string GetConstructorArgumentsText()
        {
            return $"{X:G}, {Y:G}, {Z:G}, {W:G}";
        }

        protected override string GetSetMethodArgumentsText()
        {
            return $"{X:G}, {Y:G}, {Z:G}, {W:G}";
        }
    }
}
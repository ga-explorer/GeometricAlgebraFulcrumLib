using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Transforms
{
    //TODO: Create simplified versions of this transform, for example no scaling
    /// <summary>
    /// A general Euclidean similarity transform in 3D space. It includes a translation map followed by
    /// a rotation about the origin then a uniform scaling about the origin then a translation
    /// </summary>
    public sealed class TrstMap3D : ITransform3D
    {
        /// <summary>
        /// The components of a rotor multivector used for the rotation transform
        /// </summary>
        private double _rotor0, _rotor12, _rotor13, _rotor23;

        /// <summary>
        /// The components of the initial translation vector
        /// </summary>
        private double _translate1X, _translate1Y, _translate1Z;

        /// <summary>
        /// The components of the final translation vector
        /// </summary>
        private double _translate2X, _translate2Y, _translate2Z;

        /// <summary>
        /// The scaling factor
        /// </summary>
        private double _scaleFactor;


        /// This construction of the transform assumes:
        /// 1- We have a line segment on the x-axis with given x-coordinates (section1)
        /// 2- We have an arbitrary line segment in 3D space (section2)
        /// 3- We use the two line segments to define a "Translate then Rotate then Scale then Translate" map that maps section1 to section2
        /// 4- We use the given parameters of section1 and section2 to map points to points using the map we constructed in step 3
        public static TrstMap3D CreateFromXSectionToLineSectionMap(double section1StartX, double section1EndX, Tuple3D section2Start, Tuple3D section2End)
        {
            var section1VectorX = section1EndX - section1StartX;
            var section1LengthInv = 1.0d / Math.Abs(section1VectorX);
            var section1UnitVectorX = section1VectorX * section1LengthInv;

            var section2VectorX = section2End.X - section2Start.X;
            var section2VectorY = section2End.Y - section2Start.Y;
            var section2VectorZ = section2End.Z - section2Start.Z;
            var section2Length = Math.Sqrt(
                section2VectorX * section2VectorX +
                section2VectorY * section2VectorY +
                section2VectorZ * section2VectorZ
                );
            var section2LengthInv = 1.0d / section2Length;
            var section2UnitVectorX = section2VectorX * section2LengthInv;
            var section2UnitVectorY = section2VectorY * section2LengthInv;
            var section2UnitVectorZ = section2VectorZ * section2LengthInv;

            //Begin GMac Macro Code Generation, 2018-01-15T14:29:54.0907869+02:00
            //Macro: Geometry3D.Cga5d.AlignUnitVectorsRotor
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 15 sub-expressions, 0 generated temps, 15 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 4 total.
            //Computations: 0.736842105263158 average, 14 total.
            //Memory Reads: 1.36842105263158 average, 26 total.
            //Memory Writes: 19 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> var rotor0
            //   result.#e1^e2# <=> <Variable> var rotor12
            //   result.#e1^e3# <=> <Variable> var rotor13
            //   result.#e2^e3# <=> <Variable> var rotor23
            //   unitVector1.#e1# <=> <Variable> section1UnitVectorX
            //   unitVector2.#e1# <=> <Variable> section2UnitVectorX
            //   unitVector2.#e2# <=> <Variable> section2UnitVectorY
            //   unitVector2.#e3# <=> <Variable> section2UnitVectorZ

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            var rotor23 = 0;

            tmp0 = section1UnitVectorX * section2UnitVectorX;
            tmp0 = Math.Acos(tmp0);
            tmp0 = 0.5 * tmp0;
            var rotor0 = Math.Cos(tmp0);

            tmp1 = section1UnitVectorX * section2UnitVectorY;
            tmp2 = Math.Pow(tmp1, 2);
            tmp3 = section1UnitVectorX * section2UnitVectorZ;
            tmp4 = Math.Pow(tmp3, 2);
            tmp2 = tmp2 + tmp4;
            tmp2 = Math.Abs(tmp2);
            tmp2 = Math.Pow(tmp2, -0.5);
            tmp1 = tmp1 * tmp2;
            tmp0 = Math.Sin(tmp0);
            tmp1 = tmp1 * tmp0;
            var rotor12 = -1 * tmp1;

            tmp1 = tmp3 * tmp2;
            tmp0 = tmp1 * tmp0;
            var rotor13 = -1 * tmp0;

            //Finish GMac Macro Code Generation, 2018-01-15T14:29:54.1128010+02:00

            return new TrstMap3D
            {
                _translate1X = -section1StartX,
                _rotor0 = rotor0,
                _rotor12 = rotor12,
                _rotor13 = rotor13,
                _rotor23 = rotor23,
                _scaleFactor = section2Length * section1LengthInv,
                _translate2X = section2Start.X,
                _translate2Y = section2Start.Y,
                _translate2Z = section2Start.Z
            };
        }


        public double XSectionLength { get; set; } = 1.0d;

        public Tuple3D XSectionStart => new Tuple3D(0, 0, 0);

        public Tuple3D XSectionEnd => new Tuple3D(XSectionLength, 0, 0);

        public Tuple3D FinalSectionStart { get; set; }

        public Tuple3D FinalSectionEnd { get; set; }


        public Tuple3D MapPoint(Tuple3D point)
        {
            //Apply the first translation
            var pointX = point.X + _translate1X;
            var pointY = point.Y + _translate1Y;
            var pointZ = point.Z + _translate1Z;

            //Apply the rotation

            //Begin GMac Macro Code Generation, 2018-01-19T22:16:17.1538435+02:00
            //Macro: Geometry3D.Cga5d.ApplyRotor_Point
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 3 total.
            //Computations: 1.19512195121951 average, 49 total.
            //Memory Reads: 2 average, 82 total.
            //Memory Writes: 41 total.
            //
            //Macro Binding Data: 
            //   result.#e1# <=> <Variable> var x
            //   result.#e2# <=> <Variable> var y
            //   result.#e3# <=> <Variable> var z
            //   rotor.#E0# <=> <Variable> _rotor0
            //   rotor.#e1^e2# <=> <Variable> _rotor12
            //   rotor.#e1^e3# <=> <Variable> _rotor13
            //   rotor.#e2^e3# <=> <Variable> _rotor23
            //   point.#e1# <=> <Variable> pointX
            //   point.#e2# <=> <Variable> pointY
            //   point.#e3# <=> <Variable> pointZ

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;

            tmp0 = _rotor0 * pointX;
            tmp1 = _rotor12 * pointY;
            tmp0 = tmp0 + tmp1;
            tmp1 = _rotor13 * pointZ;
            tmp0 = tmp0 + tmp1;
            tmp1 = _rotor0 * tmp0;
            tmp2 = -1 * _rotor12 * pointX;
            tmp3 = _rotor0 * pointY;
            tmp2 = tmp2 + tmp3;
            tmp3 = _rotor23 * pointZ;
            tmp2 = tmp2 + tmp3;
            tmp3 = _rotor12 * tmp2;
            tmp1 = tmp1 + tmp3;
            tmp3 = -1 * _rotor13 * pointX;
            tmp4 = -1 * _rotor23 * pointY;
            tmp3 = tmp3 + tmp4;
            tmp4 = _rotor0 * pointZ;
            tmp3 = tmp3 + tmp4;
            tmp4 = _rotor13 * tmp3;
            tmp1 = tmp1 + tmp4;
            tmp4 = _rotor23 * pointX;
            tmp5 = -1 * _rotor13 * pointY;
            tmp4 = tmp4 + tmp5;
            tmp5 = _rotor12 * pointZ;
            tmp4 = tmp4 + tmp5;
            tmp5 = _rotor23 * tmp4;
            var x = tmp1 + tmp5;

            tmp1 = -1 * _rotor12 * tmp0;
            tmp5 = _rotor0 * tmp2;
            tmp1 = tmp1 + tmp5;
            tmp5 = _rotor23 * tmp3;
            tmp1 = tmp1 + tmp5;
            tmp5 = -1 * _rotor13 * tmp4;
            var y = tmp1 + tmp5;

            tmp0 = -1 * _rotor13 * tmp0;
            tmp1 = -1 * _rotor23 * tmp2;
            tmp0 = tmp0 + tmp1;
            tmp1 = _rotor0 * tmp3;
            tmp0 = tmp0 + tmp1;
            tmp1 = _rotor12 * tmp4;
            var z = tmp0 + tmp1;

            //Finish GMac Macro Code Generation, 2018-01-19T22:16:17.1698546+02:00

            //Apply the uniform scaling and second translation
            return new Tuple3D(
                x * _scaleFactor + _translate2X,
                y * _scaleFactor + _translate2Y,
                z * _scaleFactor + _translate2Z
            );
        }
    }
}

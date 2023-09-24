using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Planes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Planes.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Projections
{
    /// <summary>
    /// This class is used to compute the projections of geometric objects
    /// in 3D on planes
    /// </summary>
    public class GcPlaneProjector3D
    {
        public Plane3D Plane { get; private set; }


        public GcPlaneProjector3D SetPlaneFromPoints(IFloat64Vector3D p1, IFloat64Vector3D p2, IFloat64Vector3D p3)
        {
            Plane = new Plane3D(
                p1,
                p1.GetDirectionTo(p2),
                p2.GetDirectionTo(p3)
            );

            return this;
        }

        public GcPlaneProjector3D SetPlaneFromTriangle(ITriangle3D triangle)
        {
            Plane = triangle.ToPlane();

            return this;
        }

        public GcPlaneProjector3D SetPlane(IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2)
        {
            Plane = new Plane3D(origin, direction1, direction2);

            return this;
        }

        public GcPlaneProjector3D SetPlane(IPlane3D plane)
        {
            Plane = plane.ToPlane();

            return this;
        }


        /// <summary>
        /// Compute the projection component of the given vector on the plane
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Float64Vector3D ComputeProjection(IFloat64Vector3D vector)
        {
            //Begin GMac Macro Code Generation, 2018-10-20T20:49:38.2694768+02:00
            //Macro: cemsim.hga4d.ProjectVectorOnPlaneDirections3D
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 3 total.
            //Computations: 1.23076923076923 average, 48 total.
            //Memory Reads: 1.82051282051282 average, 71 total.
            //Memory Writes: 39 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: x
            //   result.#e2# = variable: y
            //   result.#e3# = variable: z
            //   vector.#e1# = variable: vector.ItemX
            //   vector.#e2# = variable: vector.ItemY
            //   vector.#e3# = variable: vector.ItemZ
            //   planeVector1.#e1# = variable: Plane.Direction1X
            //   planeVector1.#e2# = variable: Plane.Direction1Y
            //   planeVector1.#e3# = variable: Plane.Direction1Z
            //   planeVector2.#e1# = variable: Plane.Direction2X
            //   planeVector2.#e2# = variable: Plane.Direction2Y
            //   planeVector2.#e3# = variable: Plane.Direction2Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0018 = Times[-1,LLDI0008,LLDI000A]
            tmp0 = -1 * Plane.Direction1Y * Plane.Direction2X;

            //Sub-expression: LLDI0019 = Times[LLDI0007,LLDI000B]
            tmp1 = Plane.Direction1X * Plane.Direction2Y;

            //Sub-expression: LLDI001A = Plus[LLDI0018,LLDI0019]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001B = Power[LLDI001A,2]
            tmp1 = tmp0 * tmp0;

            //Sub-expression: LLDI001C = Times[-1,LLDI001B]
            tmp1 = -tmp1;

            //Sub-expression: LLDI001D = Times[-1,LLDI0009,LLDI000A]
            tmp2 = -1 * Plane.Direction1Z * Plane.Direction2X;

            //Sub-expression: LLDI001E = Times[LLDI0007,LLDI000C]
            tmp3 = Plane.Direction1X * Plane.Direction2Z;

            //Sub-expression: LLDI001F = Plus[LLDI001D,LLDI001E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0020 = Power[LLDI001F,2]
            tmp3 = tmp2 * tmp2;

            //Sub-expression: LLDI0021 = Times[-1,LLDI0020]
            tmp3 = -tmp3;

            //Sub-expression: LLDI0022 = Plus[LLDI001C,LLDI0021]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0023 = Times[-1,LLDI0009,LLDI000B]
            tmp3 = -1 * Plane.Direction1Z * Plane.Direction2Y;

            //Sub-expression: LLDI0024 = Times[LLDI0008,LLDI000C]
            tmp4 = Plane.Direction1Y * Plane.Direction2Z;

            //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0026 = Power[LLDI0025,2]
            tmp4 = tmp3 * tmp3;

            //Sub-expression: LLDI0027 = Times[-1,LLDI0026]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0028 = Plus[LLDI0022,LLDI0027]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0029 = Power[LLDI0028,-1]
            tmp1 = 1 / tmp1;

            //Sub-expression: LLDI002A = Times[LLDI001A,LLDI0029]
            tmp4 = tmp0 * tmp1;

            //Sub-expression: LLDI002B = Times[LLDI0004,LLDI002A]
            tmp5 = vector.X * tmp4;

            //Sub-expression: LLDI002C = Times[LLDI0025,LLDI0029]
            tmp6 = tmp3 * tmp1;

            //Sub-expression: LLDI002D = Times[-1,LLDI0006,LLDI002C]
            tmp7 = -1 * vector.Z * tmp6;

            //Sub-expression: LLDI002E = Plus[LLDI002B,LLDI002D]
            tmp5 = tmp5 + tmp7;

            //Sub-expression: LLDI002F = Times[-1,LLDI001A,LLDI002E]
            tmp7 = -1 * tmp0 * tmp5;

            //Sub-expression: LLDI0030 = Times[LLDI001F,LLDI0029]
            tmp1 = tmp2 * tmp1;

            //Sub-expression: LLDI0031 = Times[LLDI0004,LLDI0030]
            tmp8 = vector.X * tmp1;

            //Sub-expression: LLDI0032 = Times[LLDI0005,LLDI002C]
            tmp6 = vector.Y * tmp6;

            //Sub-expression: LLDI0033 = Plus[LLDI0031,LLDI0032]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0034 = Times[-1,LLDI001F,LLDI0033]
            tmp8 = -1 * tmp2 * tmp6;

            //Output: LLDI0001 = Plus[LLDI002F,LLDI0034]
            var x = tmp7 + tmp8;

            //Sub-expression: LLDI0035 = Times[-1,LLDI0005,LLDI002A]
            tmp4 = -1 * vector.Y * tmp4;

            //Sub-expression: LLDI0036 = Times[-1,LLDI0006,LLDI0030]
            tmp1 = -1 * vector.Z * tmp1;

            //Sub-expression: LLDI0037 = Plus[LLDI0035,LLDI0036]
            tmp1 = tmp4 + tmp1;

            //Sub-expression: LLDI0038 = Times[LLDI001A,LLDI0037]
            tmp0 = tmp0 * tmp1;

            //Sub-expression: LLDI0039 = Times[-1,LLDI0025,LLDI0033]
            tmp4 = -1 * tmp3 * tmp6;

            //Output: LLDI0002 = Plus[LLDI0038,LLDI0039]
            var y = tmp0 + tmp4;

            //Sub-expression: LLDI003A = Times[LLDI001F,LLDI0037]
            tmp0 = tmp2 * tmp1;

            //Sub-expression: LLDI003B = Times[LLDI0025,LLDI002E]
            tmp1 = tmp3 * tmp5;

            //Output: LLDI0003 = Plus[LLDI003A,LLDI003B]
            var z = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2018-10-20T20:49:38.3300163+02:00

            return Float64Vector3D.Create(x, y, z);
        }

        /// <summary>
        /// Compute the orthogonal projection and rejection components of the
        /// given vector on the plane
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Tuple<Float64Vector3D, Float64Vector3D> ComputeComponents(IFloat64Vector3D vector)
        {
            //Begin GMac Macro Code Generation, 2018-10-20T20:49:38.2694768+02:00
            //Macro: cemsim.hga4d.ProjectVectorOnPlaneDirections3D
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 3 total.
            //Computations: 1.23076923076923 average, 48 total.
            //Memory Reads: 1.82051282051282 average, 71 total.
            //Memory Writes: 39 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: x
            //   result.#e2# = variable: y
            //   result.#e3# = variable: z
            //   vector.#e1# = variable: vector.ItemX
            //   vector.#e2# = variable: vector.ItemY
            //   vector.#e3# = variable: vector.ItemZ
            //   planeVector1.#e1# = variable: Plane.Direction1X
            //   planeVector1.#e2# = variable: Plane.Direction1Y
            //   planeVector1.#e3# = variable: Plane.Direction1Z
            //   planeVector2.#e1# = variable: Plane.Direction2X
            //   planeVector2.#e2# = variable: Plane.Direction2Y
            //   planeVector2.#e3# = variable: Plane.Direction2Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0018 = Times[-1,LLDI0008,LLDI000A]
            tmp0 = -1 * Plane.Direction1Y * Plane.Direction2X;

            //Sub-expression: LLDI0019 = Times[LLDI0007,LLDI000B]
            tmp1 = Plane.Direction1X * Plane.Direction2Y;

            //Sub-expression: LLDI001A = Plus[LLDI0018,LLDI0019]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001B = Power[LLDI001A,2]
            tmp1 = tmp0 * tmp0;

            //Sub-expression: LLDI001C = Times[-1,LLDI001B]
            tmp1 = -tmp1;

            //Sub-expression: LLDI001D = Times[-1,LLDI0009,LLDI000A]
            tmp2 = -1 * Plane.Direction1Z * Plane.Direction2X;

            //Sub-expression: LLDI001E = Times[LLDI0007,LLDI000C]
            tmp3 = Plane.Direction1X * Plane.Direction2Z;

            //Sub-expression: LLDI001F = Plus[LLDI001D,LLDI001E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0020 = Power[LLDI001F,2]
            tmp3 = tmp2 * tmp2;

            //Sub-expression: LLDI0021 = Times[-1,LLDI0020]
            tmp3 = -tmp3;

            //Sub-expression: LLDI0022 = Plus[LLDI001C,LLDI0021]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0023 = Times[-1,LLDI0009,LLDI000B]
            tmp3 = -1 * Plane.Direction1Z * Plane.Direction2Y;

            //Sub-expression: LLDI0024 = Times[LLDI0008,LLDI000C]
            tmp4 = Plane.Direction1Y * Plane.Direction2Z;

            //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0026 = Power[LLDI0025,2]
            tmp4 = tmp3 * tmp3;

            //Sub-expression: LLDI0027 = Times[-1,LLDI0026]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0028 = Plus[LLDI0022,LLDI0027]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0029 = Power[LLDI0028,-1]
            tmp1 = 1 / tmp1;

            //Sub-expression: LLDI002A = Times[LLDI001A,LLDI0029]
            tmp4 = tmp0 * tmp1;

            //Sub-expression: LLDI002B = Times[LLDI0004,LLDI002A]
            tmp5 = vector.X * tmp4;

            //Sub-expression: LLDI002C = Times[LLDI0025,LLDI0029]
            tmp6 = tmp3 * tmp1;

            //Sub-expression: LLDI002D = Times[-1,LLDI0006,LLDI002C]
            tmp7 = -1 * vector.Z * tmp6;

            //Sub-expression: LLDI002E = Plus[LLDI002B,LLDI002D]
            tmp5 = tmp5 + tmp7;

            //Sub-expression: LLDI002F = Times[-1,LLDI001A,LLDI002E]
            tmp7 = -1 * tmp0 * tmp5;

            //Sub-expression: LLDI0030 = Times[LLDI001F,LLDI0029]
            tmp1 = tmp2 * tmp1;

            //Sub-expression: LLDI0031 = Times[LLDI0004,LLDI0030]
            tmp8 = vector.X * tmp1;

            //Sub-expression: LLDI0032 = Times[LLDI0005,LLDI002C]
            tmp6 = vector.Y * tmp6;

            //Sub-expression: LLDI0033 = Plus[LLDI0031,LLDI0032]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0034 = Times[-1,LLDI001F,LLDI0033]
            tmp8 = -1 * tmp2 * tmp6;

            //Output: LLDI0001 = Plus[LLDI002F,LLDI0034]
            var x = tmp7 + tmp8;

            //Sub-expression: LLDI0035 = Times[-1,LLDI0005,LLDI002A]
            tmp4 = -1 * vector.Y * tmp4;

            //Sub-expression: LLDI0036 = Times[-1,LLDI0006,LLDI0030]
            tmp1 = -1 * vector.Z * tmp1;

            //Sub-expression: LLDI0037 = Plus[LLDI0035,LLDI0036]
            tmp1 = tmp4 + tmp1;

            //Sub-expression: LLDI0038 = Times[LLDI001A,LLDI0037]
            tmp0 = tmp0 * tmp1;

            //Sub-expression: LLDI0039 = Times[-1,LLDI0025,LLDI0033]
            tmp4 = -1 * tmp3 * tmp6;

            //Output: LLDI0002 = Plus[LLDI0038,LLDI0039]
            var y = tmp0 + tmp4;

            //Sub-expression: LLDI003A = Times[LLDI001F,LLDI0037]
            tmp0 = tmp2 * tmp1;

            //Sub-expression: LLDI003B = Times[LLDI0025,LLDI002E]
            tmp1 = tmp3 * tmp5;

            //Output: LLDI0003 = Plus[LLDI003A,LLDI003B]
            var z = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2018-10-20T20:49:38.3300163+02:00

            return Tuple.Create(
                Float64Vector3D.Create(x, y, z),
                Float64Vector3D.Create(vector.X - x, 
                    vector.Y - y, 
                    vector.Z - z)
            );
        }

        /// <summary>
        /// Compute the orthogonal rejection component of the given vector on
        /// the plane
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Float64Vector3D ComputeRejection(IFloat64Vector3D vector)
        {
            //Begin GMac Macro Code Generation, 2018-10-20T20:49:38.2694768+02:00
            //Macro: cemsim.hga4d.ProjectVectorOnPlaneDirections3D
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 3 total.
            //Computations: 1.23076923076923 average, 48 total.
            //Memory Reads: 1.82051282051282 average, 71 total.
            //Memory Writes: 39 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: x
            //   result.#e2# = variable: y
            //   result.#e3# = variable: z
            //   vector.#e1# = variable: vector.ItemX
            //   vector.#e2# = variable: vector.ItemY
            //   vector.#e3# = variable: vector.ItemZ
            //   planeVector1.#e1# = variable: Plane.Direction1X
            //   planeVector1.#e2# = variable: Plane.Direction1Y
            //   planeVector1.#e3# = variable: Plane.Direction1Z
            //   planeVector2.#e1# = variable: Plane.Direction2X
            //   planeVector2.#e2# = variable: Plane.Direction2Y
            //   planeVector2.#e3# = variable: Plane.Direction2Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0018 = Times[-1,LLDI0008,LLDI000A]
            tmp0 = -1 * Plane.Direction1Y * Plane.Direction2X;

            //Sub-expression: LLDI0019 = Times[LLDI0007,LLDI000B]
            tmp1 = Plane.Direction1X * Plane.Direction2Y;

            //Sub-expression: LLDI001A = Plus[LLDI0018,LLDI0019]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001B = Power[LLDI001A,2]
            tmp1 = tmp0 * tmp0;

            //Sub-expression: LLDI001C = Times[-1,LLDI001B]
            tmp1 = -tmp1;

            //Sub-expression: LLDI001D = Times[-1,LLDI0009,LLDI000A]
            tmp2 = -1 * Plane.Direction1Z * Plane.Direction2X;

            //Sub-expression: LLDI001E = Times[LLDI0007,LLDI000C]
            tmp3 = Plane.Direction1X * Plane.Direction2Z;

            //Sub-expression: LLDI001F = Plus[LLDI001D,LLDI001E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0020 = Power[LLDI001F,2]
            tmp3 = tmp2 * tmp2;

            //Sub-expression: LLDI0021 = Times[-1,LLDI0020]
            tmp3 = -tmp3;

            //Sub-expression: LLDI0022 = Plus[LLDI001C,LLDI0021]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0023 = Times[-1,LLDI0009,LLDI000B]
            tmp3 = -1 * Plane.Direction1Z * Plane.Direction2Y;

            //Sub-expression: LLDI0024 = Times[LLDI0008,LLDI000C]
            tmp4 = Plane.Direction1Y * Plane.Direction2Z;

            //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0026 = Power[LLDI0025,2]
            tmp4 = tmp3 * tmp3;

            //Sub-expression: LLDI0027 = Times[-1,LLDI0026]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0028 = Plus[LLDI0022,LLDI0027]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0029 = Power[LLDI0028,-1]
            tmp1 = 1 / tmp1;

            //Sub-expression: LLDI002A = Times[LLDI001A,LLDI0029]
            tmp4 = tmp0 * tmp1;

            //Sub-expression: LLDI002B = Times[LLDI0004,LLDI002A]
            tmp5 = vector.X * tmp4;

            //Sub-expression: LLDI002C = Times[LLDI0025,LLDI0029]
            tmp6 = tmp3 * tmp1;

            //Sub-expression: LLDI002D = Times[-1,LLDI0006,LLDI002C]
            tmp7 = -1 * vector.Z * tmp6;

            //Sub-expression: LLDI002E = Plus[LLDI002B,LLDI002D]
            tmp5 = tmp5 + tmp7;

            //Sub-expression: LLDI002F = Times[-1,LLDI001A,LLDI002E]
            tmp7 = -1 * tmp0 * tmp5;

            //Sub-expression: LLDI0030 = Times[LLDI001F,LLDI0029]
            tmp1 = tmp2 * tmp1;

            //Sub-expression: LLDI0031 = Times[LLDI0004,LLDI0030]
            tmp8 = vector.X * tmp1;

            //Sub-expression: LLDI0032 = Times[LLDI0005,LLDI002C]
            tmp6 = vector.Y * tmp6;

            //Sub-expression: LLDI0033 = Plus[LLDI0031,LLDI0032]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0034 = Times[-1,LLDI001F,LLDI0033]
            tmp8 = -1 * tmp2 * tmp6;

            //Output: LLDI0001 = Plus[LLDI002F,LLDI0034]
            var x = tmp7 + tmp8;

            //Sub-expression: LLDI0035 = Times[-1,LLDI0005,LLDI002A]
            tmp4 = -1 * vector.Y * tmp4;

            //Sub-expression: LLDI0036 = Times[-1,LLDI0006,LLDI0030]
            tmp1 = -1 * vector.Z * tmp1;

            //Sub-expression: LLDI0037 = Plus[LLDI0035,LLDI0036]
            tmp1 = tmp4 + tmp1;

            //Sub-expression: LLDI0038 = Times[LLDI001A,LLDI0037]
            tmp0 = tmp0 * tmp1;

            //Sub-expression: LLDI0039 = Times[-1,LLDI0025,LLDI0033]
            tmp4 = -1 * tmp3 * tmp6;

            //Output: LLDI0002 = Plus[LLDI0038,LLDI0039]
            var y = tmp0 + tmp4;

            //Sub-expression: LLDI003A = Times[LLDI001F,LLDI0037]
            tmp0 = tmp2 * tmp1;

            //Sub-expression: LLDI003B = Times[LLDI0025,LLDI002E]
            tmp1 = tmp3 * tmp5;

            //Output: LLDI0003 = Plus[LLDI003A,LLDI003B]
            var z = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2018-10-20T20:49:38.3300163+02:00

            return Float64Vector3D.Create(vector.X - x,
                vector.Y - y,
                vector.Z - z);
        }
    }
}

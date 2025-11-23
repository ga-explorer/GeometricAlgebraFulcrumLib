using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;

public static class LinFloat64FrameInterpolationUtils
{
    public static LinFloat64Vector3D InterpolateByRotation(this double t, LinFloat64Vector3D u1, LinFloat64Vector3D u2, double tolerance = Float64Utils.DefaultTolerance)
    {
        // --- Normalize inputs (work with unit vectors) ---
        var u1X = u1.X;
        var u1Y = u1.Y;
        var u1Z = u1.Z;

        var u2X = u2.X;
        var u2Y = u2.Y;
        var u2Z = u2.Z;

        var u1Len = Math.Sqrt(u1X * u1X + u1Y * u1Y + u1Z * u1Z);
        var u2Len = Math.Sqrt(u2X * u2X + u2Y * u2Y + u2Z * u2Z);

        // both zero -> arbitrary vector
        if (u1Len < tolerance && u2Len < tolerance)
            return LinFloat64Vector3D.E1;

        // if u1 zero, start from u2 (no meaningful plane)
        if (u1Len < tolerance)
            return LinFloat64Vector3D.Create(u2X / u2Len, u2Y / u2Len, u2Z / u2Len);

        // normalize
        u1X /= u1Len;
        u1Y /= u1Len;
        u1Z /= u1Len;

        // If u2 is zero, just return u1 (nothing to go to)
        if (u2Len < tolerance)
            return LinFloat64Vector3D.Create(u1X, u1Y, u1Z);

        u2X /= u2Len;
        u2Y /= u2Len;
        u2Z /= u2Len;

        // --- Dot and angle ---
        var dot = u1X * u2X + u1Y * u2Y + u1Z * u2Z;
        if (dot > 1.0) dot = 1.0;
        if (dot < -1.0) dot = -1.0;

        // principal angle in [0, pi]
        var theta = Math.Acos(dot);

        // If nearly identical, return u1 (or small smooth step)
        if (theta < 1e-15)
            return LinFloat64Vector3D.Create(u1X, u1Y, u1Z);

        // --- Cross product to get rotation axis ---
        var cx = u1Y * u2Z - u1Z * u2Y;
        var cy = u1Z * u2X - u1X * u2Z;
        var cz = u1X * u2Y - u1Y * u2X;
        var cLenSq = cx * cx + cy * cy + cz * cz;

        // rotation axis (unit)
        double nx, ny, nz;

        // non-degenerate plane: axis is cross(u1,u2)
        if (cLenSq > tolerance * tolerance)
        {
            var cLen = Math.Sqrt(cLenSq);
            nx = cx / cLen; ny = cy / cLen; nz = cz / cLen;
        }
        else
        {
            // cross is (near) zero -> u1 and u2 are parallel or antiparallel.

            // parallel and same direction -> nothing to do
            if (dot > 0.0)
                return LinFloat64Vector3D.Create(u1X, u1Y, u1Z);

            // antiparallel: choose a stable axis perpendicular to u1
            // pick coordinate axis least aligned with u1 as helper
            var ax = Math.Abs(u1X);
            var ay = Math.Abs(u1Y);
            var az = Math.Abs(u1Z);

            double tx, ty, tz;
            if (ax <= ay && ax <= az)
            {
                tx = 1.0; ty = 0.0; tz = 0.0;
            }
            else if (ay <= az)
            {
                tx = 0.0; ty = 1.0; tz = 0.0;
            }
            else
            {
                tx = 0.0; ty = 0.0; tz = 1.0;
            }

            // axis = normalize(u1 × temp)
            cx = u1Y * tz - u1Z * ty;
            cy = u1Z * tx - u1X * tz;
            cz = u1X * ty - u1Y * tx;

            var cLen = Math.Sqrt(cx * cx + cy * cy + cz * cz);

            // extremely degenerate (shouldn't happen), fallback
            if (cLen < tolerance)
            {
                // choose any orthonormal axis
                if (Math.Abs(u1X) < Math.Abs(u1Y) && Math.Abs(u1X) < Math.Abs(u1Z))
                {
                    nx = 1.0;
                    ny = 0.0;
                    nz = 0.0;
                }
                else if (Math.Abs(u1Y) < Math.Abs(u1Z))
                {
                    nx = 0.0;
                    ny = 1.0;
                    nz = 0.0;
                }
                else
                {
                    nx = 0.0;
                    ny = 0.0;
                    nz = 1.0;
                }
            }
            else
            {
                nx = cx / cLen; ny = cy / cLen; nz = cz / cLen;
            }

            // set theta to pi (antiparallel), dot is approx -1
            theta = Math.PI;
        }

        // --- Interpolated angle ---
        var th = theta * t;
        var cosTh = Math.Cos(th);
        var sinTh = Math.Sin(th);

        // For Rodriguez' formula we need n x u1.
        // But since n is perpendicular to the plane of u1,u2,
        // n·u1 = 0 so the Rodriguez formula reduces:
        // u(t) = u1*cos(th) + (n × u1)*sin(th)
        var nxuX = ny * u1Z - nz * u1Y;
        var nxuY = nz * u1X - nx * u1Z;
        var nxuZ = nx * u1Y - ny * u1X;

        var rx = u1X * cosTh + nxuX * sinTh;
        var ry = u1Y * cosTh + nxuY * sinTh;
        var rz = u1Z * cosTh + nxuZ * sinTh;

        // Normalise result to avoid tiny round-off
        var rLen = Math.Sqrt(rx * rx + ry * ry + rz * rz);

        // shouldn't happen, fallback
        if (rLen < tolerance)
            return LinFloat64Vector3D.Create(u1X, u1Y, u1Z);

        rx /= rLen; ry /= rLen; rz /= rLen;

        return LinFloat64Vector3D.Create(rx, ry, rz);
    }

    /// <summary>
    /// Returns a 3x3 rotation matrix R (double[3,3]) such that
    /// R * u1 = interpolated vector = rotate(u1, axis n, angle = theta * t)
    /// </summary>
    /// <param name="t"></param>
    /// <param name="u1"></param>
    /// <param name="u2"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static double[,] GetRotationMatrixInterpolation(this double t, LinFloat64Vector3D u1, LinFloat64Vector3D u2, double tolerance = Float64Utils.DefaultTolerance)
    {
        // Unpack
        var u1X = u1.X;
        var u1Y = u1.Y;
        var u1Z = u1.Z;

        var u2X = u2.X;
        var u2Y = u2.Y;
        var u2Z = u2.Z;

        // Lengths
        var u1Len = Math.Sqrt(u1X * u1X + u1Y * u1Y + u1Z * u1Z);
        var u2Len = Math.Sqrt(u2X * u2X + u2Y * u2Y + u2Z * u2Z);

        // If both zero, return identity
        if (u1Len < tolerance && u2Len < tolerance)
            return new double[,]
            {
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 }
            };

        // If u1 is zero, treat as no meaningful rotation (return matrix rotating nothing)
        if (u1Len < tolerance)
        {
            // If u2 non-zero, return matrix that maps x-axis to u2 (optional).
            // Simpler: return identity so caller can handle special-case vector interpolation.
            return new double[,]
            {
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 }
            };
        }

        // Normalize inputs (u1 guaranteed non-zero)
        u1X /= u1Len; 
        u1Y /= u1Len; 
        u1Z /= u1Len;

        if (u2Len < tolerance)
        {
            // u2 zero -> no target; return identity
            return new double[,]
            {
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 }
            };
        }

        u2X /= u2Len; 
        u2Y /= u2Len; 
        u2Z /= u2Len;

        // Dot, clamp and principal angle theta in [0, pi]
        var dot = u1X * u2X + u1Y * u2Y + u1Z * u2Z;
        if (dot > 1.0) dot = 1.0;
        if (dot < -1.0) dot = -1.0;

        var theta = Math.Acos(dot);

        // If almost identical, return identity (no rotation)
        if (theta < 1e-15)
            return new double[,]
            {
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { 0, 0, 1 }
            };

        // Compute cross = u1 x u2
        var cx = u1Y * u2Z - u1Z * u2Y;
        var cy = u1Z * u2X - u1X * u2Z;
        var cz = u1X * u2Y - u1Y * u2X;
        var cLenSq = cx * cx + cy * cy + cz * cz;

        double nx, ny, nz; // rotation axis
        if (cLenSq > tolerance * tolerance)
        {
            var cLen = Math.Sqrt(cLenSq);
            nx = cx / cLen; ny = cy / cLen; nz = cz / cLen;
        }
        else
        {
            // cross ~ 0 --> parallel or antiparallel
            if (dot > 0.0)
            {
                // parallel, same direction -> identity
                return new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            }

            // antiparallel: pick stable perpendicular axis (least aligned coordinate axis)
            double ax = Math.Abs(u1X), ay = Math.Abs(u1Y), az = Math.Abs(u1Z);
            double tx, ty, tz;
            if (ax <= ay && ax <= az)
            {
                tx = 1.0; 
                ty = 0.0; 
                tz = 0.0;
            }
            else if (ay <= az)
            {
                tx = 0.0; 
                ty = 1.0; 
                tz = 0.0;
            }
            else
            {
                tx = 0.0; 
                ty = 0.0; 
                tz = 1.0;
            }

            // axis = normalize(u1 x temp)
            cx = u1Y * tz - u1Z * ty;
            cy = u1Z * tx - u1X * tz;
            cz = u1X * ty - u1Y * tx;
            var cLen = Math.Sqrt(cx * cx + cy * cy + cz * cz);
            if (cLen < tolerance)
            {
                // extremely degenerate: pick canonical axis
                if (Math.Abs(u1X) < Math.Abs(u1Y) && Math.Abs(u1X) < Math.Abs(u1Z))
                {
                    nx = 1.0;
                    ny = 0.0;
                    nz = 0.0;
                }
                else if (Math.Abs(u1Y) < Math.Abs(u1Z))
                {
                    nx = 0.0;
                    ny = 1.0;
                    nz = 0.0;
                }
                else
                {
                    nx = 0.0;
                    ny = 0.0;
                    nz = 1.0;
                }
            }
            else
            {
                nx = cx / cLen; ny = cy / cLen; nz = cz / cLen;
            }

            // ensure theta = pi for antiparallel
            theta = Math.PI;
        }

        // Interpolated rotation angle phi = theta * t
        var phi = theta * t;
        var c = Math.Cos(phi);       // cos(phi)
        var s = Math.Sin(phi);       // sin(phi)
        var oneMinusC = 1.0 - c;

        // Build outer product n n^T and skew-symmetric [n]_x
        // nn^T:
        double nn00 = nx * nx, nn01 = nx * ny, nn02 = nx * nz;
        double nn11 = ny * ny, nn12 = ny * nz;
        var nn22 = nz * nz;

        // [n]_x:
        double k01 = -nz, k02 = ny;
        double k10 = nz, k12 = -nx;
        double k20 = -ny, k21 = nx;

        // Rodriguez: R = I*c + (1-c) * n n^T + s * [n]_x
        var r = new double[3, 3];

        r[0, 0] = c + oneMinusC * nn00 + s * 0.0;
        r[0, 1] = oneMinusC * nn01 + s * k01;
        r[0, 2] = oneMinusC * nn02 + s * k02;

        r[1, 0] = oneMinusC * nn01 + s * k10;
        r[1, 1] = c + oneMinusC * nn11 + s * 0.0;
        r[1, 2] = oneMinusC * nn12 + s * k12;

        r[2, 0] = oneMinusC * nn02 + s * k20;
        r[2, 1] = oneMinusC * nn12 + s * k21;
        r[2, 2] = c + oneMinusC * nn22 + s * 0.0;

        return r;
    }



    /// <summary>
    /// This is most suitable for animations
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static Triplet<LinFloat64Vector3D> ToSmoothOrthonormalFrame(this ITriplet<LinFloat64Vector3D> frame, double tolerance = Float64Utils.DefaultTolerance)
    {
        var (u1, v1, w1) = frame.ToTriplet();

        // --- Step 1: Normalize u1 with smooth fallback ---
        var uLen = u1.L2Norm();
        LinFloat64Vector3D u2;

        if (uLen > tolerance)
        {
            var invLen = 1.0 / uLen;
            u2 = LinFloat64Vector3D.Create(u1.X * invLen, u1.Y * invLen, u1.Z * invLen);
        }
        else
        {
            var fallback = (v1.L2NormSquared() > w1.L2NormSquared() ? v1 : w1);
            if (fallback.L2NormSquared() < tolerance)
                fallback = LinFloat64Vector3D.Create(1, 0, 0);

            var blend = SafeAxis(fallback);
            var uBlend = LinFloat64Vector3D.Create(
                fallback.X + blend.X,
                fallback.Y + blend.Y,
                fallback.Z + blend.Z
            );
            var invLen = 1.0 / Math.Sqrt(uBlend.X * uBlend.X + uBlend.Y * uBlend.Y + uBlend.Z * uBlend.Z);
            u2 = LinFloat64Vector3D.Create(uBlend.X * invLen, uBlend.Y * invLen, uBlend.Z * invLen);
        }

        // --- Step 2: Build v2 (orthogonalized & blended) ---
        var uvDot = v1.X * u2.X + v1.Y * u2.Y + v1.Z * u2.Z;
        var vx = v1.X - uvDot * u2.X;
        var vy = v1.Y - uvDot * u2.Y;
        var vz = v1.Z - uvDot * u2.Z;
        var vLen = Math.Sqrt(vx * vx + vy * vy + vz * vz);

        LinFloat64Vector3D v2;
        if (vLen > tolerance)
        {
            var invLen = 1.0 / vLen;
            v2 = LinFloat64Vector3D.Create(vx * invLen, vy * invLen, vz * invLen);
        }
        else
        {
            var fallback = SafeAxis(u2);
            var blend = LinFloat64Vector3D.Create(
                v1.X + fallback.X,
                v1.Y + fallback.Y,
                v1.Z + fallback.Z
            );
            var buDot = blend.X * u2.X + blend.Y * u2.Y + blend.Z * u2.Z;
            blend = LinFloat64Vector3D.Create(blend.X - buDot * u2.X, blend.Y - buDot * u2.Y, blend.Z - buDot * u2.Z);
            var lenB = Math.Sqrt(blend.X * blend.X + blend.Y * blend.Y + blend.Z * blend.Z);
            var invLenB = 1.0 / lenB;
            v2 = LinFloat64Vector3D.Create(blend.X * invLenB, blend.Y * invLenB, blend.Z * invLenB);
        }

        // --- Step 3: Build w2 (cross with blending) ---
        var wx = u2.Y * v2.Z - u2.Z * v2.Y;
        var wy = u2.Z * v2.X - u2.X * v2.Z;
        var wz = u2.X * v2.Y - u2.Y * v2.X;
        var wLen = Math.Sqrt(wx * wx + wy * wy + wz * wz);

        LinFloat64Vector3D w2;
        if (wLen > tolerance)
        {
            var invLen = 1.0 / wLen;
            w2 = LinFloat64Vector3D.Create(wx * invLen, wy * invLen, wz * invLen);
        }
        else
        {
            // Blend with original w1
            var wuDot = w1.X * u2.X + w1.Y * u2.Y + w1.Z * u2.Z;
            var wvDot = w1.X * v2.X + w1.Y * v2.Y + w1.Z * v2.Z;
            var wOrthoX = w1.X - wuDot * u2.X - wvDot * v2.X;
            var wOrthoY = w1.Y - wuDot * u2.Y - wvDot * v2.Y;
            var wOrthoZ = w1.Z - wuDot * u2.Z - wvDot * v2.Z;

            var lenOrtho = Math.Sqrt(wOrthoX * wOrthoX + wOrthoY * wOrthoY + wOrthoZ * wOrthoZ);
            if (lenOrtho < tolerance)
            {
                // Final smooth fallback: u2 × safeAxis
                var fallback = SafeAxis(u2);
                wx = u2.Y * fallback.Z - u2.Z * fallback.Y;
                wy = u2.Z * fallback.X - u2.X * fallback.Z;
                wz = u2.X * fallback.Y - u2.Y * fallback.X;
                var invLenF = 1.0 / Math.Sqrt(wx * wx + wy * wy + wz * wz);
                w2 = LinFloat64Vector3D.Create(wx * invLenF, wy * invLenF, wz * invLenF);
            }
            else
            {
                var invLenO = 1.0 / lenOrtho;
                w2 = LinFloat64Vector3D.Create(wOrthoX * invLenO, wOrthoY * invLenO, wOrthoZ * invLenO);
            }
        }

        return new Triplet<LinFloat64Vector3D>(u2, v2, w2);

        // --- Helper: Safe axis for fallback ---
        static LinFloat64Vector3D SafeAxis(LinFloat64Vector3D refVec)
        {
            if (Math.Abs(refVec.X) < Math.Abs(refVec.Y) && Math.Abs(refVec.X) < Math.Abs(refVec.Z))
                return LinFloat64Vector3D.Create(1, 0, 0);
            if (Math.Abs(refVec.Y) < Math.Abs(refVec.Z))
                return LinFloat64Vector3D.Create(0, 1, 0);
            return LinFloat64Vector3D.Create(0, 0, 1);
        }
    }

    /// <summary>
    /// Interpolate two orthonormal frames
    /// </summary>
    /// <param name="t"></param>
    /// <param name="frame1"></param>
    /// <param name="frame2"></param>
    /// <returns></returns>
    public static Pair<LinFloat64Vector3D> InterpolateOrthonormalFrames(this double t, IPair<LinFloat64Vector3D> frame1, IPair<LinFloat64Vector3D> frame2)
    {
        // Relative rotation: rotationMatrix = B2 * B1^T
        //var rotationMatrix = Multiply(B2, Transpose(B1));
        var rotationMatrix =
            frame1.GetFrameToFrameRotationMatrix(frame2);

        // Convert to quaternion
        var q = RotationMatrixToQuaternion(rotationMatrix);

        // Slerp between identity (1,0,0,0) and q
        var qSlerp = Slerp([1, 0, 0, 0], q, t);

        // Convert interpolated quaternion back to rotation matrix
        rotationMatrix = QuaternionToRotationMatrix(qSlerp);

        // Apply final rotation matrix to frame1 (matrix-free)
        return frame1.MapFrameUsing(rotationMatrix);
    }

    /// <summary>
    /// Interpolate two orthonormal frames
    /// </summary>
    /// <param name="t"></param>
    /// <param name="frame1"></param>
    /// <param name="frame2"></param>
    /// <returns></returns>
    public static Triplet<LinFloat64Vector3D> InterpolateOrthonormalFrames(this double t, ITriplet<LinFloat64Vector3D> frame1, ITriplet<LinFloat64Vector3D> frame2)
    {
        // Relative rotation: rotationMatrix = B2 * B1^T
        //var rotationMatrix = Multiply(B2, Transpose(B1));
        var rotationMatrix =
            frame1.GetFrameToFrameRotationMatrix(frame2);

        // Convert to quaternion
        var q = RotationMatrixToQuaternion(rotationMatrix);

        // Slerp between identity (1,0,0,0) and q
        var qSlerp = Slerp([1, 0, 0, 0], q, t);

        // Convert interpolated quaternion back to rotation matrix
        rotationMatrix = QuaternionToRotationMatrix(qSlerp);

        // Apply final rotation matrix to frame1 (matrix-free)
        return frame1.MapFrameUsing(rotationMatrix);
    }

    public static double[,] GetFrameToFrameRotationMatrix(this IPair<LinFloat64Vector3D> frame1, IPair<LinFloat64Vector3D> frame2)
    {
        var (u1, v1) = frame1.ToPair();
        var (u2, v2) = frame2.ToPair();

        var w1 = u1.VectorUnitCross(v1);
        var w2 = u2.VectorUnitCross(v2);

        // Relative rotation: rotationMatrix = B2 * B1^T
        //var rotationMatrix = Multiply(B2, Transpose(B1));
        var rotationMatrix = new double[3, 3];

        // Row 0
        rotationMatrix[0, 0] = u2.X * u1.X + v2.X * v1.X + w2.X * w1.X;
        rotationMatrix[0, 1] = u2.X * u1.Y + v2.X * v1.Y + w2.X * w1.Y;
        rotationMatrix[0, 2] = u2.X * u1.Z + v2.X * v1.Z + w2.X * w1.Z;

        // Row 1
        rotationMatrix[1, 0] = u2.Y * u1.X + v2.Y * v1.X + w2.Y * w1.X;
        rotationMatrix[1, 1] = u2.Y * u1.Y + v2.Y * v1.Y + w2.Y * w1.Y;
        rotationMatrix[1, 2] = u2.Y * u1.Z + v2.Y * v1.Z + w2.Y * w1.Z;

        // Row 2
        rotationMatrix[2, 0] = u2.Z * u1.X + v2.Z * v1.X + w2.Z * w1.X;
        rotationMatrix[2, 1] = u2.Z * u1.Y + v2.Z * v1.Y + w2.Z * w1.Y;
        rotationMatrix[2, 2] = u2.Z * u1.Z + v2.Z * v1.Z + w2.Z * w1.Z;

        return rotationMatrix;
    }

    public static double[,] GetFrameToFrameRotationMatrix(this ITriplet<LinFloat64Vector3D> frame1, ITriplet<LinFloat64Vector3D> frame2)
    {
        var (u1, v1, w1) = frame1.ToTriplet();
        var (u2, v2, w2) = frame2.ToTriplet();

        // Relative rotation: rotationMatrix = B2 * B1^T
        //var rotationMatrix = Multiply(B2, Transpose(B1));
        var rotationMatrix = new double[3, 3];

        // Row 0
        rotationMatrix[0, 0] = u2.X * u1.X + v2.X * v1.X + w2.X * w1.X;
        rotationMatrix[0, 1] = u2.X * u1.Y + v2.X * v1.Y + w2.X * w1.Y;
        rotationMatrix[0, 2] = u2.X * u1.Z + v2.X * v1.Z + w2.X * w1.Z;

        // Row 1
        rotationMatrix[1, 0] = u2.Y * u1.X + v2.Y * v1.X + w2.Y * w1.X;
        rotationMatrix[1, 1] = u2.Y * u1.Y + v2.Y * v1.Y + w2.Y * w1.Y;
        rotationMatrix[1, 2] = u2.Y * u1.Z + v2.Y * v1.Z + w2.Y * w1.Z;

        // Row 2
        rotationMatrix[2, 0] = u2.Z * u1.X + v2.Z * v1.X + w2.Z * w1.X;
        rotationMatrix[2, 1] = u2.Z * u1.Y + v2.Z * v1.Y + w2.Z * w1.Y;
        rotationMatrix[2, 2] = u2.Z * u1.Z + v2.Z * v1.Z + w2.Z * w1.Z;

        return rotationMatrix;
    }

    public static Pair<LinFloat64Vector3D> MapFrameUsing(this IPair<LinFloat64Vector3D> frame, double[,] matrix)
    {
        var (u1, v1) = frame.ToPair();

        var w1 = u1.VectorUnitCross(v1);

        var u = LinFloat64Vector3D.Create(
            matrix[0, 0] * u1.X + matrix[0, 1] * v1.X + matrix[0, 2] * w1.X,
            matrix[1, 0] * u1.Y + matrix[1, 1] * v1.Y + matrix[1, 2] * w1.Y,
            matrix[2, 0] * u1.Z + matrix[2, 1] * v1.Z + matrix[2, 2] * w1.Z
        );

        var v = LinFloat64Vector3D.Create(
            matrix[0, 0] * u1.X + matrix[0, 1] * v1.X + matrix[0, 2] * w1.X,
            matrix[1, 0] * u1.Y + matrix[1, 1] * v1.Y + matrix[1, 2] * w1.Y,
            matrix[2, 0] * u1.Z + matrix[2, 1] * v1.Z + matrix[2, 2] * w1.Z
        );

        return new Pair<LinFloat64Vector3D>(u, v);
    }

    public static Triplet<LinFloat64Vector3D> MapFrameUsing(this ITriplet<LinFloat64Vector3D> frame, double[,] matrix)
    {
        var (u1, v1, w1) = frame.ToTriplet();

        var u = LinFloat64Vector3D.Create(
            matrix[0, 0] * u1.X + matrix[0, 1] * v1.X + matrix[0, 2] * w1.X,
            matrix[1, 0] * u1.Y + matrix[1, 1] * v1.Y + matrix[1, 2] * w1.Y,
            matrix[2, 0] * u1.Z + matrix[2, 1] * v1.Z + matrix[2, 2] * w1.Z
        );

        var v = LinFloat64Vector3D.Create(
            matrix[0, 0] * u1.X + matrix[0, 1] * v1.X + matrix[0, 2] * w1.X,
            matrix[1, 0] * u1.Y + matrix[1, 1] * v1.Y + matrix[1, 2] * w1.Y,
            matrix[2, 0] * u1.Z + matrix[2, 1] * v1.Z + matrix[2, 2] * w1.Z
        );

        var w = LinFloat64Vector3D.Create(
            matrix[0, 0] * u1.X + matrix[0, 1] * v1.X + matrix[0, 2] * w1.X,
            matrix[1, 0] * u1.Y + matrix[1, 1] * v1.Y + matrix[1, 2] * w1.Y,
            matrix[2, 0] * u1.Z + matrix[2, 1] * v1.Z + matrix[2, 2] * w1.Z
        );

        return new Triplet<LinFloat64Vector3D>(u, v, w);
    }


    private static double[] RotationMatrixToQuaternion(double[,] rotationMatrix)
    {
        var trace = rotationMatrix[0, 0] + rotationMatrix[1, 1] + rotationMatrix[2, 2];
        var q = new double[4];

        if (trace > 0)
        {
            var s = Math.Sqrt(trace + 1.0) * 2.0;
            q[0] = 0.25 * s;
            q[1] = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) / s;
            q[2] = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) / s;
            q[3] = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) / s;
        }
        else if ((rotationMatrix[0, 0] > rotationMatrix[1, 1]) & (rotationMatrix[0, 0] > rotationMatrix[2, 2]))
        {
            var s = Math.Sqrt(1.0 + rotationMatrix[0, 0] - rotationMatrix[1, 1] - rotationMatrix[2, 2]) * 2.0;
            q[0] = (rotationMatrix[2, 1] - rotationMatrix[1, 2]) / s;
            q[1] = 0.25 * s;
            q[2] = (rotationMatrix[0, 1] + rotationMatrix[1, 0]) / s;
            q[3] = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;
        }
        else if (rotationMatrix[1, 1] > rotationMatrix[2, 2])
        {
            var s = Math.Sqrt(1.0 + rotationMatrix[1, 1] - rotationMatrix[0, 0] - rotationMatrix[2, 2]) * 2.0;
            q[0] = (rotationMatrix[0, 2] - rotationMatrix[2, 0]) / s;
            q[1] = (rotationMatrix[0, 1] + rotationMatrix[1, 0]) / s;
            q[2] = 0.25 * s;
            q[3] = (rotationMatrix[1, 2] + rotationMatrix[2, 1]) / s;
        }
        else
        {
            var s = Math.Sqrt(1.0 + rotationMatrix[2, 2] - rotationMatrix[0, 0] - rotationMatrix[1, 1]) * 2.0;
            q[0] = (rotationMatrix[1, 0] - rotationMatrix[0, 1]) / s;
            q[1] = (rotationMatrix[0, 2] + rotationMatrix[2, 0]) / s;
            q[2] = (rotationMatrix[1, 2] + rotationMatrix[2, 1]) / s;
            q[3] = 0.25 * s;
        }

        Normalize(q);
        return q;
    }

    private static double[,] QuaternionToRotationMatrix(double[] q)
    {
        double w = q[0], x = q[1], y = q[2], z = q[3];
        var rotationMatrix = new double[3, 3];

        rotationMatrix[0, 0] = 1 - 2 * (y * y + z * z);
        rotationMatrix[0, 1] = 2 * (x * y - z * w);
        rotationMatrix[0, 2] = 2 * (x * z + y * w);

        rotationMatrix[1, 0] = 2 * (x * y + z * w);
        rotationMatrix[1, 1] = 1 - 2 * (x * x + z * z);
        rotationMatrix[1, 2] = 2 * (y * z - x * w);

        rotationMatrix[2, 0] = 2 * (x * z - y * w);
        rotationMatrix[2, 1] = 2 * (y * z + x * w);
        rotationMatrix[2, 2] = 1 - 2 * (x * x + y * y);

        return rotationMatrix;
    }

    private static double[] Slerp(double[] q1, double[] q2, double t)
    {
        var dot = q1[0] * q2[0] + q1[1] * q2[1] + q1[2] * q2[2] + q1[3] * q2[3];
        if (dot < 0.0) { for (var i = 0; i < 4; i++) q2[i] = -q2[i]; dot = -dot; }

        const double dotThreshold = 0.9995;
        if (dot > dotThreshold)
        {
            // Linear interpolation for very small angles
            var result = new double[4];
            for (var i = 0; i < 4; i++)
                result[i] = q1[i] + t * (q2[i] - q1[i]);
            Normalize(result);
            return result;
        }

        var theta0 = Math.Acos(dot);
        var theta = theta0 * t;
        var sinTheta = Math.Sin(theta);
        var sinTheta0 = Math.Sin(theta0);

        var s0 = Math.Cos(theta) - dot * sinTheta / sinTheta0;
        var s1 = sinTheta / sinTheta0;

        var q = new double[4];
        for (var i = 0; i < 4; i++)
            q[i] = (s0 * q1[i]) + (s1 * q2[i]);
        return q;
    }

    private static void Normalize(double[] q)
    {
        var norm =
            Math.Sqrt(
                q[0] * q[0] + q[1] * q[1] + q[2] * q[2] + q[3] * q[3]
            );

        for (var i = 0; i < 4; i++)
            q[i] /= norm;
    }
}
﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SharpGLTF.Transforms
{
    /// <summary>
    /// Represents an affine transform in 3D space, with two mutually exclusive representantions:<br/>
    /// <list type="bullet">
    /// <item>
    /// As a 4x3 Matrix. When <see cref="IsMatrix"/> is true.<br/>
    /// Publicly exposed as <see cref="Matrix"/>.
    /// </item>
    /// <item>
    /// As a Scale/Rotation/Translation chain. When <see cref="IsSRT"/> is true.<br/>
    /// Publicly exposed as: <see cref="Scale"/>, <see cref="Rotation"/>, <see cref="Translation"/>.
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Depending on how <see cref="AffineTransform"/> structures are created, the underlaying<br/>
    /// fields must be interprested as a Matrix4x3 or a Scale/Rotation/Translation chain.
    /// </para>
    /// <para>
    /// This approach allows <see cref="AffineTransform"/> preserving the source transform, avoiding loosing<br/>
    /// precission when decomposing a matrix, or creating a matrix from a SRT chain.
    /// </para>
    /// <para>
    /// Decomposing matrices is tricky because not all valid matrices can be decomposed; in particular<br/>
    /// squewed matrices will fail to decompose. See <see href="https://github.com/vpenades/SharpGLTF/issues/41"/>.
    /// </para>
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("AffineTransform {ToDebuggerDisplayString(),nq}")]
    public readonly struct AffineTransform : IEquatable<AffineTransform>
    {
        #region diagnostics

        internal string ToDebuggerDisplayString()
        {
            if (!IsValid) return "INVALID";

            if (IsIdentity) return "IDENTITY";

            if (TryDecompose(out var decomposed))
            {
                var s = decomposed._GetScale();
                var r = decomposed._GetRotation();

                var txt = string.Empty;

                var ss = Vector3.Max(Vector3.Zero, s - Vector3.One);
                var hasScale = ss.X > 0.000001f || ss.Y > 0.000001f || ss.Z > 0.000001f;

                if (hasScale) txt += $"𝐒:{s} ";
                if (r != Quaternion.Identity) txt += $"𝐑:{r} ";
                if (decomposed.Translation != Vector3.Zero) txt += $"𝚻:{decomposed.Translation} ";

                return txt;
            }
            else
            {
                return "Skewed Matrix ";
            }
        }

        #endregion

        #region constants

        private const string _CannotDecomposeError = "Matrix is invalid or skewed.";

        private const string _RequiresSRTError = "Needs to be in SRT representation. Call GetDecomposed() first.";

        public static readonly AffineTransform Identity = new AffineTransform(null, null, null);

        private const int DATA_UNDEFINED = 0;
        private const int DATA_SRT = 1;
        private const int DATA_MAT = 2;

        #endregion

        #region factories

        public static implicit operator AffineTransform((Quaternion r, Vector3 t) xform)
        {
            return new AffineTransform(null, xform.r, xform.t);
        }

        public static implicit operator AffineTransform(Matrix4x4 matrix)
        {
            return new AffineTransform(matrix);
        }

        public static AffineTransform CreateDecomposed(Matrix4x4 matrix)
        {
            if (!Matrix4x4.Decompose(matrix, out var s, out var r, out var t)) throw new ArgumentException("Can't decompose", nameof(matrix));

            return new AffineTransform(s, r, t);
        }

        public static AffineTransform CreateFromAny(Matrix4x4? matrix, Vector3? scale, Quaternion? rotation, Vector3? translation)
        {
            if (matrix.HasValue)
            {
                Guard.MustBeNull(scale, nameof(scale));
                Guard.MustBeNull(scale, nameof(rotation));
                Guard.MustBeNull(scale, nameof(translation));
                return new AffineTransform(matrix.Value);
            }
            else
            {
                return new AffineTransform(scale, rotation, translation);
            }
        }

        public AffineTransform WithScale(Vector3 scale)
        {
            if (_Representation == DATA_UNDEFINED) return new AffineTransform(scale, null, null);

            var tmp = this;
            if (tmp.IsMatrix) tmp = tmp.GetDecomposed();
            return new AffineTransform(scale, tmp.Rotation, tmp.Translation);
        }

        public AffineTransform WithRotation(Quaternion rotation)
        {
            if (_Representation == DATA_UNDEFINED) return new AffineTransform(null, rotation, null);

            var tmp = this;
            if (tmp.IsMatrix) tmp = tmp.GetDecomposed();
            return new AffineTransform(tmp.Scale, rotation, tmp.Translation);
        }

        public AffineTransform WithTranslation(Vector3 translation)
        {
            if (_Representation == DATA_UNDEFINED) return new AffineTransform(null, null, translation);

            if (this.IsSRT) return new AffineTransform(this.Scale, this.Rotation, translation);
            var tmp = this.Matrix;
            tmp.Translation = translation;
            return tmp;
        }

        #endregion

        #region constructors

        public AffineTransform(Vector3? scale, Quaternion? rotation, Vector3? translation)
            : this(scale ?? Vector3.One, rotation ?? Quaternion.Identity, translation ?? Vector3.Zero)
        { }

        public AffineTransform(Vector3 scale, Quaternion rotation, Vector3 translation)
        {
            rotation = rotation.Sanitized();

            Guard.IsTrue(scale._IsFinite(), nameof(scale));
            Guard.IsTrue(rotation._IsFinite(), nameof(rotation));
            Guard.IsTrue(translation._IsFinite(), nameof(translation));

            _Representation = DATA_SRT;

            _M11 = scale.X;
            _M12 = scale.Y;
            _M13 = scale.Z;

            _M21 = rotation.X;
            _M22 = rotation.Y;
            _M23 = rotation.Z;
            _M31 = rotation.W;

            _M32 = 0; // unused
            _M33 = 0; // unused

            _Translation = translation;
        }

        public AffineTransform(Matrix4x4 matrix)
        {
            Guard.IsTrue(matrix._IsFinite(), nameof(matrix));
            Guard.MustBeEqualTo(matrix.M14, 0, nameof(matrix.M14));
            Guard.MustBeEqualTo(matrix.M24, 0, nameof(matrix.M24));
            Guard.MustBeEqualTo(matrix.M34, 0, nameof(matrix.M34));
            Guard.MustBeEqualTo(matrix.M44, 1, nameof(matrix.M44));

            _Representation = DATA_MAT;

            _M11 = matrix.M11;
            _M12 = matrix.M12;
            _M13 = matrix.M13;

            _M21 = matrix.M21;
            _M22 = matrix.M22;
            _M23 = matrix.M23;

            _M31 = matrix.M31;
            _M32 = matrix.M32;
            _M33 = matrix.M33;

            _Translation = matrix.Translation;
        }

        #endregion

        #region data

        /// <summary>
        /// Determines what's represented by the data fields:<br/>
        /// <list type="bullet">
        /// <item><see cref="DATA_UNDEFINED"/> - Not defined.</item>
        /// <item><see cref="DATA_MAT"/> - Fields must be interpreted as a 4x3 Matrix.</item>
        /// <item><see cref="DATA_SRT"/> - Fields must be interpreted as a Scale, Rotation and Translation chain.</item>
        /// </list>
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly Int32 _Representation;

        /// <summary>
        /// Matrix:  M11<br/>
        /// SRT: Scale.X
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M11;

        /// <summary>
        /// Matrix:  M12<br/>
        /// SRT: Scale.Y
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M12;

        /// <summary>
        /// Matrix:  M13<br/>
        /// SRT: Scale.Z
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M13;

        /// <summary>
        /// Matrix:  M21<br/>
        /// SRT: Rotation.X
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M21;

        /// <summary>
        /// Matrix:  M22<br/>
        /// SRT: Rotation.Y
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M22;

        /// <summary>
        /// Matrix:  M23<br/>
        /// SRT: Rotation.Z
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M23;

        /// <summary>
        /// Matrix:  M31<br/>
        /// SRT: Rotation.W
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M31;

        /// <summary>
        /// Matrix:  M32<br/>
        /// SRT: unused
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M32;

        /// <summary>
        /// Matrix:  M32<br/>
        /// SRT: unused
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly float _M33;

        /// <summary>
        /// Matrix and SRT: Translation
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly Vector3 _Translation;

        public override int GetHashCode()
        {
            // we can only use the translation as hash code because it's the only value that
            // is the same on SRT and Matrix representations.... otherwhise we would have
            // to take the hash code of the matrix representation.
            return _Translation.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is AffineTransform other && this.Equals(other);
        }

        public bool Equals(AffineTransform other)
        {
            if (this.IsSRT && other.IsSRT)
            {
                if (this._Translation != other._Translation) return false;
                if (this._M11 != other._M11) return false;
                if (this._M12 != other._M12) return false;
                if (this._M13 != other._M13) return false;

                if (this._M21 != other._M21) return false;
                if (this._M22 != other._M22) return false;
                if (this._M23 != other._M23) return false;
                if (this._M31 != other._M31) return false;

                System.Diagnostics.Debug.Assert(this._M32 == other._M32);
                System.Diagnostics.Debug.Assert(this._M33 == other._M33);
            }

            return this.Matrix.Equals(other.Matrix);
        }

        #endregion

        #region properties

        public bool IsValid
        {
            get
            {
                if (_Representation == DATA_UNDEFINED) return false;

                if (!Translation._IsFinite()) return false;

                if (!_M11._IsFinite()) return false;
                if (!_M12._IsFinite()) return false;
                if (!_M13._IsFinite()) return false;

                if (!_M21._IsFinite()) return false;
                if (!_M22._IsFinite()) return false;
                if (!_M23._IsFinite()) return false;

                if (!_M31._IsFinite()) return false;
                if (!_M32._IsFinite()) return false;
                if (!_M33._IsFinite()) return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AffineTransform"/> represents a <see cref="Matrix4x4"/>.
        /// </summary>
        public bool IsMatrix => _Representation == DATA_MAT;

        /// <summary>
        /// Gets a value indicating whether this <see cref="AffineTransform"/> represents a SRT chain.
        /// </summary>
        public bool IsSRT => _Representation == DATA_SRT;

        /// <summary>
        /// Gets the scale.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When the internal representation is a Matrix and cannot be decomposed.
        /// </exception>
        public Vector3 Scale => _GetScale();

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// When the internal representation is a Matrix and cannot be decomposed.
        /// </exception>
        public Quaternion Rotation => _GetRotation();

        /// <summary>
        /// Gets the translation
        /// </summary>
        public Vector3 Translation => _Translation;

        /// <summary>
        /// Gets the <see cref="Matrix4x4"/> transform of the current <see cref="AffineTransform"/>
        /// </summary>
        public Matrix4x4 Matrix => _GetMatrix();

        /// <summary>
        /// Gets a value indicating whether this transform can be decomposed to SRT without precission loss.
        /// </summary>
        public bool IsLosslessDecomposable
        {
            get
            {
                _VerifyDefined();

                if (IsSRT) return true;

                // row 1
                if (_M11 != 0) return false;
                if (_M12 == 0) return false;
                if (_M13 == 0) return false;

                // row 2
                if (_M21 == 0) return false;
                if (_M22 != 0) return false;
                if (_M23 == 0) return false;

                // row 3
                if (_M31 == 0) return false;
                if (_M32 == 0) return false;
                if (_M33 != 0) return false;

                return true;
            }
        }

        public bool IsIdentity
        {
            get
            {
                if (IsSRT)
                {
                    if (Translation != Vector3.Zero) return false;

                    // scale
                    if (_M11 != 1) return false;
                    if (_M12 != 1) return false;
                    if (_M13 != 1) return false;

                    // rotation
                    if (_M21 != 0) return false;
                    if (_M22 != 0) return false;
                    if (_M23 != 0) return false;
                    if (_M31 != 1) return false;
                }
                else if (IsMatrix)
                {
                    if (Translation != Vector3.Zero) return false;

                    // row 1
                    if (_M11 != 1) return false;
                    if (_M12 != 0) return false;
                    if (_M13 != 0) return false;

                    // row 2
                    if (_M21 != 0) return false;
                    if (_M22 != 1) return false;
                    if (_M23 != 0) return false;

                    // row 3
                    if (_M31 != 0) return false;
                    if (_M32 != 0) return false;
                    if (_M33 != 1) return false;
                }
                else
                {
                    _VerifyDefined();
                }

                return true;
            }
        }

        #endregion

        #region API

        private void _VerifyDefined()
        {
            if (_Representation == DATA_UNDEFINED) throw new InvalidOperationException("Undefined");
        }

        private Matrix4x4 _GetMatrix()
        {
            if (IsMatrix)
            {
                return new Matrix4x4
                (
                    _M11, _M12, _M13, 0,
                    _M21, _M22, _M23, 0,
                    _M31, _M32, _M33, 0,
                    _Translation.X,
                    _Translation.Y,
                    _Translation.Z, 1
                );
            }
            else if (IsSRT)
            {
                var m = Matrix4x4.CreateScale(this.Scale) * Matrix4x4.CreateFromQuaternion(this.Rotation);
                m.Translation = this.Translation;
                return m;
            }
            else
            {
                _VerifyDefined();
                return default;
            }
        }

        private Vector3 _GetScale()
        {
            if (IsSRT) return new Vector3(_M11, _M12, _M13);
            throw new InvalidOperationException(_RequiresSRTError);
        }

        private Quaternion _GetRotation()
        {
            if (IsSRT) return new Quaternion(_M21, _M22, _M23, _M31);
            throw new InvalidOperationException(_RequiresSRTError);
        }

        public AffineTransform GetDecomposed()
        {
            return TryDecompose(out AffineTransform xform)
                ? xform
                : throw new InvalidOperationException(_CannotDecomposeError);
        }

        public bool TryDecompose(out AffineTransform transform)
        {
            if (IsSRT) { transform = this; return true; }

            if (IsLosslessDecomposable)
            {
                transform = new AffineTransform
                    (
                    new Vector3(_M11, _M22, _M33),
                    Quaternion.Identity,
                    this.Translation
                    );

                return true;
            }

            var x = Matrix4x4.Decompose(Matrix, out var s, out var r, out var t);

            transform = x ? new AffineTransform(s, r, t) : this;

            return x;
        }

        public bool TryDecompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            if (IsSRT)
            {
                scale = _GetScale();
                rotation = _GetRotation();
                translation = _Translation;
                return true;
            }

            if (IsLosslessDecomposable)
            {
                scale = new Vector3(_M11, _M22, _M33);
                rotation = Quaternion.Identity;
                translation = _Translation;
                return true;
            }

            return Matrix4x4.Decompose(Matrix, out scale, out rotation, out translation);
        }

        public static AffineTransform Blend(ReadOnlySpan<AffineTransform> transforms, ReadOnlySpan<float> weights)
        {
            if (transforms.Length == 0) return Identity;
            if (transforms.Length == 1) return transforms[0];

            var sss = Vector3.Zero;
            var rrr = default(Quaternion);
            var ttt = Vector3.Zero;

            float tweight = 0;

            for (int i = 0; i < transforms.Length; ++i)
            {
                Guard.IsTrue(transforms[i].IsValid, nameof(transforms));
                Guard.IsTrue(transforms[i].TryDecompose(out var s, out var r, out var t), $"Can't decompose [{i}]");

                var w = weights[i];

                sss += s * w;
                ttt += t * w;

                tweight += w;
                if (i == 0) rrr = r;
                else rrr = Quaternion.Slerp(r, rrr, w / tweight);
            }

            rrr = Quaternion.Normalize(rrr);

            return new  AffineTransform(sss, rrr, ttt);
        }

        public static AffineTransform operator *(in AffineTransform a, in AffineTransform b)
        {
            return Multiply(a, b);
        }

        /// <summary>
        /// Multiplies <paramref name="a"/> by <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The left transform.</param>
        /// <param name="b">The right transform.</param>
        /// <returns>
        /// <para>A new <see cref="AffineTransform"/> structure.</para>
        /// <para>
        /// The returned value will use a decomposed<br/>
        /// representation it these two conditions are met:<br/>
        /// <list type="number">
        /// <item>Arguments <paramref name="a"/> and <paramref name="b"/> are also in decomposed form.</item>
        /// <item>The result of the operation is decomposable.</item>
        /// </list>
        /// Otherwise the returned value will use a Matrix representation.
        /// </para>
        /// </returns>
        public static AffineTransform Multiply(in AffineTransform a, in AffineTransform b)
        {
            // if any of the two operators is a matrix, do a matrix multiplication.
            if (a.IsMatrix || b.IsMatrix)
            {
                return new AffineTransform(a.Matrix * b.Matrix);
            }

            Guard.IsFalse(a._Representation == DATA_UNDEFINED, nameof(a));
            Guard.IsFalse(b._Representation == DATA_UNDEFINED, nameof(b));

            // if the B operator has an uneven scale AND a rotation, do a matrix multiplication
            // which produces a squeezed matrix and cannot be decomposed.

            var sb = b.Scale;

            if (!(sb.X == sb.Y && sb.X == sb.Z) && b.Rotation != Quaternion.Identity)
            {
                return new AffineTransform(a.Matrix * b.Matrix);
            }

            // we're safe to make a decomposed multiplication

            var s = _Vector3Transform(b.Scale * _Vector3Transform(a.Scale, a.Rotation), Quaternion.Inverse(a.Rotation));

            var r = Quaternion.Multiply(b.Rotation, a.Rotation);

            var t
                = b.Translation
                + _Vector3Transform(a.Translation * b.Scale, b.Rotation);

            return new AffineTransform(s, r, t);
        }

        /// <summary>
        /// This method is equivalent to System.Numerics.Vector3.Transform(Vector3 v, Quaternion q)
        /// </summary>
        /// <param name="v">The vector to transform</param>
        /// <param name="q">The transform rotation</param>
        /// <returns>The rotated vector</returns>
        private static Vector3 _Vector3Transform(Vector3 v, Quaternion q)
        {
            // Extract the vector part of the quaternion
            var u = new Vector3(q.X, q.Y, q.Z);

            // Extract the scalar part of the quaternion
            var s = q.W;

            // Do the math
            return (2.0f * Vector3.Dot(u, v) * u)
                + (((s * s) - Vector3.Dot(u, u)) * v)
                + (2.0f * s * Vector3.Cross(u, v));
        }

        #endregion
    }
}

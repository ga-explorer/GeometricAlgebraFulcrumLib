﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using SEGMENT = System.ArraySegment<float>;
using SPARSE = SharpGLTF.Transforms.SparseWeight8;

namespace SharpGLTF.Animations
{
    /// <summary>
    /// This class is meant to be used internally.
    /// </summary>
    /// <remarks>
    /// Functions of this class are being used by:
    /// <see cref="AnimatableProperty{T}.UseTrackBuilder(string)"/>
    /// which is the public API.
    /// </remarks>
    static class CurveFactory
    {
        public static CurveBuilder<T> CreateCurveBuilder<T>()
            where T : struct
        {
            if (typeof(T) == typeof(Vector3)) return new Vector3CurveBuilder() as CurveBuilder<T>;
            if (typeof(T) == typeof(Quaternion)) return new QuaternionCurveBuilder() as CurveBuilder<T>;
            if (typeof(T) == typeof(SPARSE)) return new SparseCurveBuilder() as CurveBuilder<T>;
            if (typeof(T) == typeof(SEGMENT)) return new SegmentCurveBuilder() as CurveBuilder<T>;

            throw new InvalidOperationException($"{typeof(T).Name} not supported.");
        }

        public static CurveBuilder<T> CreateCurveBuilder<T>(ICurveSampler<T> curve)
            where T : struct
        {
            if (curve is Vector3CurveBuilder v3cb) return v3cb.Clone() as CurveBuilder<T>;
            if (curve is QuaternionCurveBuilder q4cb) return q4cb.Clone() as CurveBuilder<T>;
            if (curve is SparseCurveBuilder sscb) return sscb.Clone() as CurveBuilder<T>;
            if (curve is SegmentCurveBuilder xscb) return xscb.Clone() as CurveBuilder<T>;

            if (typeof(T) == typeof(Vector3))
            {
                var cb = new Vector3CurveBuilder();
                cb.SetCurve(curve as ICurveSampler<Vector3>);
                return cb as CurveBuilder<T>;
            }

            if (typeof(T) == typeof(Quaternion))
            {
                var cb = new QuaternionCurveBuilder();
                cb.SetCurve(curve as ICurveSampler<Quaternion>);
                return cb as CurveBuilder<T>;
            }

            if (typeof(T) == typeof(SEGMENT))
            {
                var cb = new SegmentCurveBuilder();
                cb.SetCurve(curve as ICurveSampler<SEGMENT>);
                return cb as CurveBuilder<T>;
            }

            if (typeof(T) == typeof(SPARSE))
            {
                var cb = new SparseCurveBuilder();
                cb.SetCurve(curve as ICurveSampler<SPARSE>);
                return cb as CurveBuilder<T>;
            }

            throw new InvalidOperationException($"{typeof(T).Name} not supported.");
        }
    }

    [System.Diagnostics.DebuggerTypeProxy(typeof(Diagnostics._CurveBuilderDebugProxyVector3))]
    sealed class Vector3CurveBuilder : CurveBuilder<Vector3>, ICurveSampler<Vector3>
    {
        #region lifecycle
        public Vector3CurveBuilder() { }

        private Vector3CurveBuilder(Vector3CurveBuilder other)
            : base(other) { }

        public override CurveBuilder<Vector3> Clone() { return new Vector3CurveBuilder(this); }

        #endregion

        #region API

        protected override bool AreEqual(Vector3 left, Vector3 right) { return left == right; }
        protected override Vector3 CloneValue(Vector3 value) { return value; }
        protected override Vector3 CreateValue(IReadOnlyList<float> values)
        {
            Guard.NotNull(values, nameof(values));
            Guard.IsTrue(values.Count == 3, nameof(values));
            return new Vector3(values[0], values[1], values[2]);
        }

        protected override Vector3 GetTangent(Vector3 fromValue, Vector3 toValue)
        {
            return CurveSampler.CreateTangent(fromValue, toValue);
        }

        public override Vector3 GetPoint(Single offset)
        {
            var sample = FindSample(offset);

            switch (sample.A.Degree)
            {
                case 0:
                    return sample.A.Point;

                case 1:
                    return Vector3.Lerp(sample.A.Point, sample.B.Point, sample.Amount);

                case 3:
                    return CurveSampler.InterpolateCubic
                            (
                            sample.A.Point, sample.A.OutgoingTangent,
                            sample.B.Point, sample.B.IncomingTangent,
                            sample.Amount
                            );

                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }

    [System.Diagnostics.DebuggerTypeProxy(typeof(Diagnostics._CurveBuilderDebugProxyQuaternion))]
    sealed class QuaternionCurveBuilder : CurveBuilder<Quaternion>, ICurveSampler<Quaternion>
    {
        #region lifecycle

        public QuaternionCurveBuilder() { }

        private QuaternionCurveBuilder(QuaternionCurveBuilder other)
            : base(other) { }

        public override CurveBuilder<Quaternion> Clone() { return new QuaternionCurveBuilder(this); }

        #endregion

        #region API

        protected override bool AreEqual(Quaternion left, Quaternion right) { return left == right; }
        protected override Quaternion CloneValue(Quaternion value) { return value; }
        protected override Quaternion CreateValue(IReadOnlyList<float> values)
        {
            Guard.NotNull(values, nameof(values));
            Guard.IsTrue(values.Count == 4, nameof(values));
            return new Quaternion(values[0], values[1], values[2], values[3]);
        }

        protected override Quaternion GetTangent(Quaternion fromValue, Quaternion toValue)
        {
            return CurveSampler.CreateTangent(fromValue, toValue);
        }

        public override Quaternion GetPoint(float offset)
        {
            var sample = FindSample(offset);

            switch (sample.A.Degree)
            {
                case 0:
                    return sample.A.Point;

                case 1:
                    return Quaternion.Slerp(sample.A.Point, sample.B.Point, sample.Amount);

                case 3:
                    return CurveSampler.InterpolateCubic
                            (
                            sample.A.Point, sample.A.OutgoingTangent,
                            sample.B.Point, sample.B.IncomingTangent,
                            sample.Amount
                            );

                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }

    [System.Diagnostics.DebuggerTypeProxy(typeof(Diagnostics._CurveBuilderDebugProxySparse))]
    sealed class SparseCurveBuilder : CurveBuilder<SPARSE>, ICurveSampler<SPARSE>
    {
        #region lifecycle

        public SparseCurveBuilder() { }

        private SparseCurveBuilder(SparseCurveBuilder other)
            : base(other)
        {
        }

        public override CurveBuilder<SPARSE> Clone() { return new SparseCurveBuilder(this); }

        #endregion

        #region API

        protected override bool AreEqual(SPARSE left, SPARSE right) { return left.Equals(right); }
        protected override SPARSE CloneValue(SPARSE value) { return value; }
        protected override SPARSE CreateValue(IReadOnlyList<float> values) { return SPARSE.Create(values); }

        protected override SPARSE GetTangent(SPARSE fromValue, SPARSE toValue)
        {
            return SPARSE.Subtract(toValue, fromValue);
        }

        public override SPARSE GetPoint(Single offset)
        {
            var sample = FindSample(offset);

            switch (sample.A.Degree)
            {
                case 0:
                    return sample.A.Point;

                case 1:
                    return SPARSE.InterpolateLinear(sample.A.Point, sample.B.Point, sample.Amount);

                case 3:
                    return SPARSE.InterpolateCubic
                            (
                            sample.A.Point, sample.A.OutgoingTangent,
                            sample.B.Point, sample.B.IncomingTangent,
                            sample.Amount
                            );

                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }

    [System.Diagnostics.DebuggerTypeProxy(typeof(Diagnostics._CurveBuilderDebugProxySparse))]
    sealed class SegmentCurveBuilder : CurveBuilder<SEGMENT>, ICurveSampler<SEGMENT>
    {
        #region lifecycle

        public SegmentCurveBuilder() { }

        private SegmentCurveBuilder(SegmentCurveBuilder other)
            : base(other)
        {
        }

        public override CurveBuilder<SEGMENT> Clone() { return new SegmentCurveBuilder(this); }

        #endregion

        #region API

        protected override bool AreEqual(SEGMENT left, SEGMENT right) { return left.AsSpan().SequenceEqual(right); }
        protected override SEGMENT CloneValue(SEGMENT value)
        {
            Guard.IsTrue(value.Count > 0, nameof(value));
            return new SEGMENT(value.ToArray());
        }

        protected override SEGMENT CreateValue(IReadOnlyList<float> values)
        {
            Guard.NotNull(values, nameof(values));
            Guard.IsTrue(values.Count > 0, nameof(values));
            return new SEGMENT(values.ToArray());
        }

        protected override SEGMENT GetTangent(SEGMENT fromValue, SEGMENT toValue)
        {
            return new SEGMENT(CurveSampler.Subtract(toValue, fromValue));
        }

        public override SEGMENT GetPoint(Single offset)
        {
            var sample = FindSample(offset);

            switch (sample.A.Degree)
            {
                case 0:
                    return sample.A.Point;

                case 1:
                    return new SEGMENT(CurveSampler.InterpolateLinear(sample.A.Point, sample.B.Point, sample.Amount));

                case 3:
                    return new SEGMENT(CurveSampler.InterpolateCubic
                            (
                            sample.A.Point, sample.A.OutgoingTangent,
                            sample.B.Point, sample.B.IncomingTangent,
                            sample.Amount
                            ));
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }
}

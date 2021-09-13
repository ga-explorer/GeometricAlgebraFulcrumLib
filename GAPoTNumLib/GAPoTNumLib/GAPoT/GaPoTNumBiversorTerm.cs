using System;
using System.Diagnostics;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumBiversorTerm
    {
        public static GeoPoTNumBiversorTerm operator -(GeoPoTNumBiversorTerm t)
        {
            return new GeoPoTNumBiversorTerm(t.TermId1, t.TermId2, -t.Value);
        }

        public static GeoPoTNumBiversorTerm operator +(GeoPoTNumBiversorTerm t1, GeoPoTNumBiversorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GeoPoTNumBiversorTerm(t1.TermId1, t1.TermId2, t1.Value + t2.Value);
        }

        public static GeoPoTNumBiversorTerm operator -(GeoPoTNumBiversorTerm t1, GeoPoTNumBiversorTerm t2)
        {
            if (t1.TermId1 != t2.TermId1 || t1.TermId2 != t2.TermId2)
                throw new InvalidOperationException();

            return new GeoPoTNumBiversorTerm(t1.TermId1, t1.TermId2, t1.Value - t2.Value);
        }

        public static GeoPoTNumBiversorTerm operator *(GeoPoTNumBiversorTerm t, double s)
        {
            return new GeoPoTNumBiversorTerm(t.TermId1, t.TermId2, s * t.Value);
        }

        public static GeoPoTNumBiversorTerm operator *(double s, GeoPoTNumBiversorTerm t)
        {
            return new GeoPoTNumBiversorTerm(t.TermId1, t.TermId2, s * t.Value);
        }

        public static GeoPoTNumBiversorTerm operator /(GeoPoTNumBiversorTerm t, double s)
        {
            s = 1.0d / s;

            return new GeoPoTNumBiversorTerm(t.TermId1, t.TermId2, s * t.Value);
        }


        public int TermId1 { get; }

        public int TermId2 { get; }

        public double Value { get; }

        public bool IsScalar 
            => TermId1 == TermId2;

        public bool IsNonScalar
            => TermId1 != TermId2;

        public bool IsPhasor
            => TermId1 % 2 == 1 && TermId2 == TermId1 + 1;


        internal GeoPoTNumBiversorTerm(double value)
        {
            TermId1 = 0;
            TermId2 = 0;
            Value = value;
        }

        internal GeoPoTNumBiversorTerm(int id1, int id2, double value)
        {
            Debug.Assert(id1 == id2 || (id1 > 0 && id2 > 0));

            if (id1 == id2)
            {
                TermId1 = 0;
                TermId2 = 0;
                Value = value;
            }
            else if (id1 < id2)
            {
                TermId1 = id1;
                TermId2 = id2;
                Value = value;
            }
            else
            {
                TermId1 = id2;
                TermId2 = id1;
                Value = -value;
            }
        }


        public double Norm()
        {
            return Math.Abs(Value);
        }

        public double Norm2()
        {
            return Value * Value;
        }

        public GeoPoTNumBiversorTerm Reverse()
        {
            return IsScalar 
                ? this 
                : new GeoPoTNumBiversorTerm(TermId1, TermId2, -Value);
        }

        public GeoPoTNumBiversorTerm ScaledReverse(double s)
        {
            return IsScalar 
                ? new GeoPoTNumBiversorTerm(TermId1, TermId2, Value * s)
                : new GeoPoTNumBiversorTerm(TermId1, TermId2, -Value * s);
        }

        public GeoPoTNumMultivectorTerm ToMultivectorTerm()
        {
            if (TermId1 == TermId2)
                return new GeoPoTNumMultivectorTerm(0, Value);

            var idsPattern = (1 << (TermId1 - 1)) + (1 << (TermId2 - 1));

            return new GeoPoTNumMultivectorTerm(
                idsPattern,
                Value
            );
        }

        public string ToText()
        {
            //if (Value == 0)
            //    return "0";

            return IsScalar
                ? $"{Value:G} <>"
                : $"{Value:G} <{TermId1},{TermId2}>";
        }

        public string ToLaTeX()
        {
            //if (Value.IsNearZero())
            //    return "0";

            var valueText = Value.GetLaTeXNumber();
            var basisText = $"{TermId1},{TermId2}".GetLaTeXBasisName();

            return IsScalar
                ? $@"{valueText}"
                : $@"\left( {valueText} \right) {basisText}";
        }
 
        public GeoPoTNumBiversorTerm OffsetId(int delta)
        {
            if (IsScalar)
                return this;

            var id1 = TermId1 + delta;
            var id2 = TermId2 + delta;

            return new GeoPoTNumBiversorTerm(id1, id2, Value);
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}
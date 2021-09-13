using System;
using System.Diagnostics;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumVectorTerm
    {
        public static GeoPoTNumVectorTerm operator -(GeoPoTNumVectorTerm t)
        {
            return new GeoPoTNumVectorTerm(t.TermId, -t.Value);
        }

        public static GeoPoTNumVectorTerm operator +(GeoPoTNumVectorTerm t1, GeoPoTNumVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GeoPoTNumVectorTerm(t1.TermId, t1.Value + t2.Value);
        }

        public static GeoPoTNumVectorTerm operator -(GeoPoTNumVectorTerm t1, GeoPoTNumVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GeoPoTNumVectorTerm(t1.TermId, t1.Value - t2.Value);
        }

        public static GeoPoTNumVectorTerm operator *(GeoPoTNumVectorTerm t, double s)
        {
            return new GeoPoTNumVectorTerm(t.TermId, s * t.Value);
        }

        public static GeoPoTNumVectorTerm operator *(double s, GeoPoTNumVectorTerm t)
        {
            return new GeoPoTNumVectorTerm(t.TermId, s * t.Value);
        }

        public static GeoPoTNumVectorTerm operator /(GeoPoTNumVectorTerm t, double s)
        {
            s = 1.0d / s;

            return new GeoPoTNumVectorTerm(t.TermId, s * t.Value);
        }


        public int TermId { get; }

        public double Value { get; set; }


        internal GeoPoTNumVectorTerm(int id)
        {
            Debug.Assert(id > 0);

            TermId = id;
            Value = 0;
        }

        internal GeoPoTNumVectorTerm(int id, double value)
        {
            Debug.Assert(id > 0);

            TermId = id;
            Value = value;
        }

        
        public bool IsZero()
        {
            return Value == 0;
        }

        public double Norm()
        {
            return Math.Abs(Value);
        }

        public double Norm2()
        {
            return Value * Value;
        }

        public GeoPoTNumMultivectorTerm ToMultivectorTerm()
        {
            return new GeoPoTNumMultivectorTerm(1 << (TermId - 1) , Value);
        }

        public GeoPoTNumVectorTerm Round(int places)
        {
            return new GeoPoTNumVectorTerm(TermId, Math.Round(Value, places));
        }

        public string ToText()
        {
            //if (Value == 0)
            //    return "0";

            return $"{Value:G} <{TermId}>";
        }

        public string ToLaTeX()
        {
            //if (Value == 0)
            //    return "0";

            var valueText = Value.GetLaTeXNumber();
            var basisText = TermId.ToString().GetLaTeXBasisName();

            return $@"\left( {valueText} \right) {basisText}";
        }

        public GeoPoTNumVectorTerm OffsetTermId(int delta)
        {
            var id = TermId + delta;

            return new GeoPoTNumVectorTerm(id, Value);
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}
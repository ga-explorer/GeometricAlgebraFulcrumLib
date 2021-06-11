using System;
using System.Diagnostics;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumVectorTerm
    {
        public static GaPoTNumVectorTerm operator -(GaPoTNumVectorTerm t)
        {
            return new GaPoTNumVectorTerm(t.TermId, -t.Value);
        }

        public static GaPoTNumVectorTerm operator +(GaPoTNumVectorTerm t1, GaPoTNumVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumVectorTerm(t1.TermId, t1.Value + t2.Value);
        }

        public static GaPoTNumVectorTerm operator -(GaPoTNumVectorTerm t1, GaPoTNumVectorTerm t2)
        {
            if (t1.TermId != t2.TermId)
                throw new InvalidOperationException();

            return new GaPoTNumVectorTerm(t1.TermId, t1.Value - t2.Value);
        }

        public static GaPoTNumVectorTerm operator *(GaPoTNumVectorTerm t, double s)
        {
            return new GaPoTNumVectorTerm(t.TermId, s * t.Value);
        }

        public static GaPoTNumVectorTerm operator *(double s, GaPoTNumVectorTerm t)
        {
            return new GaPoTNumVectorTerm(t.TermId, s * t.Value);
        }

        public static GaPoTNumVectorTerm operator /(GaPoTNumVectorTerm t, double s)
        {
            s = 1.0d / s;

            return new GaPoTNumVectorTerm(t.TermId, s * t.Value);
        }


        public int TermId { get; }

        public double Value { get; set; }


        internal GaPoTNumVectorTerm(int id)
        {
            Debug.Assert(id > 0);

            TermId = id;
            Value = 0;
        }

        internal GaPoTNumVectorTerm(int id, double value)
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

        public GaPoTNumMultivectorTerm ToMultivectorTerm()
        {
            return new GaPoTNumMultivectorTerm(1 << (TermId - 1) , Value);
        }

        public GaPoTNumVectorTerm Round(int places)
        {
            return new GaPoTNumVectorTerm(TermId, Math.Round(Value, places));
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

        public GaPoTNumVectorTerm OffsetTermId(int delta)
        {
            var id = TermId + delta;

            return new GaPoTNumVectorTerm(id, Value);
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}
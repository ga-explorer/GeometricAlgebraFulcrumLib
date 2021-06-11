using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumMultivectorTerm
    {
        public static GaPoTNumVectorTerm operator -(GaPoTNumMultivectorTerm t)
        {
            return new GaPoTNumVectorTerm(t.IDsPattern, -t.Value);
        }

        public static GaPoTNumMultivectorTerm operator +(GaPoTNumMultivectorTerm t1, GaPoTNumMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GaPoTNumMultivectorTerm(t1.IDsPattern, t1.Value + t2.Value);
        }

        public static GaPoTNumMultivectorTerm operator -(GaPoTNumMultivectorTerm t1, GaPoTNumMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GaPoTNumMultivectorTerm(t1.IDsPattern, t1.Value - t2.Value);
        }

        public static GaPoTNumMultivectorTerm operator *(GaPoTNumMultivectorTerm t, double s)
        {
            return new GaPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        public static GaPoTNumMultivectorTerm operator *(double s, GaPoTNumMultivectorTerm t)
        {
            return new GaPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        public static GaPoTNumMultivectorTerm operator /(GaPoTNumMultivectorTerm t, double s)
        {
            s = 1.0d / s;

            return new GaPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        
        
        public int IDsPattern { get; }

        public double Value { get; set; }


        public GaPoTNumMultivectorTerm(int idsPattern)
        {
            Debug.Assert(idsPattern >= 0);

            IDsPattern = idsPattern;
            Value = 0;
        }

        public GaPoTNumMultivectorTerm(int idsPattern, double value)
        {
            Debug.Assert(idsPattern >= 0);

            IDsPattern = idsPattern;
            Value = value;
        }


        public IEnumerable<int> GetTermIDs()
        {
            var idsPattern = IDsPattern;
            var i = 1;
            while (idsPattern > 0)
            {
                if ((idsPattern & 1) != 0)
                    yield return i;

                i++;
                idsPattern >>= 1;
            }
        }

        public int GetGrade()
        {
            return IDsPattern.CountOnes();
        }

        
        public double Norm()
        {
            return Math.Abs(Value);
        }

        public double Norm2()
        {
            return Value * Value;
        }

        public GaPoTNumMultivectorTerm Op(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroOp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(0, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Sp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Lcp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Rcp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroERcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Fdp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroEFdp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Hip(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroEHip(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Cp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroECp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Acp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GaPoTNumUtils.IsNonZeroEAcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Gp(GaPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (value == 0.0d)
                return new GaPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GaPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GaPoTNumMultivectorTerm(idsPattern, value);
        }

        public GaPoTNumMultivectorTerm Reverse()
        {
            return new GaPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeReverse() ? -Value : Value
            );
        }

        public GaPoTNumMultivectorTerm GradeInvolution()
        {
            return new GaPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeGradeInv() ? -Value : Value
            );
        }

        public GaPoTNumMultivectorTerm CliffordConjugate()
        {
            return new GaPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeCliffConj() ? -Value : Value
            );
        }

        public GaPoTNumMultivectorTerm ScaledReverse(double s)
        {
            var value = (IDsPattern.BasisBladeHasNegativeReverse() ? -Value : Value) * s;
            
            return new GaPoTNumMultivectorTerm(IDsPattern, value);
        }

        public GaPoTNumMultivectorTerm Round(int places)
        {
            return new GaPoTNumMultivectorTerm(IDsPattern, Math.Round(Value, places));
        }

        public GaPoTNumVectorTerm ToVectorTerm()
        {
            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 1)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to vector term");

            return new GaPoTNumVectorTerm(termIDsArray[0], Value);
        }

        public GaPoTNumBiversorTerm ToBiversorTerm()
        {
            if (IDsPattern == 0)
                return new GaPoTNumBiversorTerm(Value);

            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 2)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to biversor term");

            return new GaPoTNumBiversorTerm(termIDsArray[0], termIDsArray[1], Value);
        }


        public string ToText()
        {
            return $"{Value:G} <{GetTermIDs().Concatenate(",")}>";
        }
        
        public string ToLaTeX()
        {
            var valueText = Value.GetLaTeXNumber();
            var basisText = $"{GetTermIDs().Concatenate(",")}".GetLaTeXBasisName();

            return $@"\left( {valueText} \right) {basisText}";
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}
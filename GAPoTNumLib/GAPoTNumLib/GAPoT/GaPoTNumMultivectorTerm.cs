using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumMultivectorTerm
    {
        public static GeoPoTNumVectorTerm operator -(GeoPoTNumMultivectorTerm t)
        {
            return new GeoPoTNumVectorTerm(t.IDsPattern, -t.Value);
        }

        public static GeoPoTNumMultivectorTerm operator +(GeoPoTNumMultivectorTerm t1, GeoPoTNumMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GeoPoTNumMultivectorTerm(t1.IDsPattern, t1.Value + t2.Value);
        }

        public static GeoPoTNumMultivectorTerm operator -(GeoPoTNumMultivectorTerm t1, GeoPoTNumMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GeoPoTNumMultivectorTerm(t1.IDsPattern, t1.Value - t2.Value);
        }

        public static GeoPoTNumMultivectorTerm operator *(GeoPoTNumMultivectorTerm t, double s)
        {
            return new GeoPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        public static GeoPoTNumMultivectorTerm operator *(double s, GeoPoTNumMultivectorTerm t)
        {
            return new GeoPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        public static GeoPoTNumMultivectorTerm operator /(GeoPoTNumMultivectorTerm t, double s)
        {
            s = 1.0d / s;

            return new GeoPoTNumMultivectorTerm(t.IDsPattern, s * t.Value);
        }

        
        
        public int IDsPattern { get; }

        public double Value { get; set; }


        public GeoPoTNumMultivectorTerm(int idsPattern)
        {
            Debug.Assert(idsPattern >= 0);

            IDsPattern = idsPattern;
            Value = 0;
        }

        public GeoPoTNumMultivectorTerm(int idsPattern, double value)
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

        public GeoPoTNumMultivectorTerm Op(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroOp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(0, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Sp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Lcp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Rcp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroERcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Fdp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroEFdp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Hip(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroEHip(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Cp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroECp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Acp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (!GeoPoTNumUtils.IsNonZeroEAcp(term1.IDsPattern, term2.IDsPattern) || value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Gp(GeoPoTNumMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = term1.Value * term2.Value;

            if (value == 0.0d)
                return new GeoPoTNumMultivectorTerm(idsPattern, 0.0d);

            if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = -value;

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Reverse()
        {
            return new GeoPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeReverse() ? -Value : Value
            );
        }

        public GeoPoTNumMultivectorTerm GradeInvolution()
        {
            return new GeoPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeGradeInv() ? -Value : Value
            );
        }

        public GeoPoTNumMultivectorTerm CliffordConjugate()
        {
            return new GeoPoTNumMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeHasNegativeCliffConj() ? -Value : Value
            );
        }

        public GeoPoTNumMultivectorTerm ScaledReverse(double s)
        {
            var value = (IDsPattern.BasisBladeHasNegativeReverse() ? -Value : Value) * s;
            
            return new GeoPoTNumMultivectorTerm(IDsPattern, value);
        }

        public GeoPoTNumMultivectorTerm Round(int places)
        {
            return new GeoPoTNumMultivectorTerm(IDsPattern, Math.Round(Value, places));
        }

        public GeoPoTNumVectorTerm ToVectorTerm()
        {
            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 1)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to vector term");

            return new GeoPoTNumVectorTerm(termIDsArray[0], Value);
        }

        public GeoPoTNumBiversorTerm ToBiversorTerm()
        {
            if (IDsPattern == 0)
                return new GeoPoTNumBiversorTerm(Value);

            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 2)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to biversor term");

            return new GeoPoTNumBiversorTerm(termIDsArray[0], termIDsArray[1], Value);
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
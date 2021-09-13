using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumMultivector : IEnumerable<GeoPoTNumMultivectorTerm>
    {
        public static GeoPoTNumMultivector CreateZero()
        {
            return new GeoPoTNumMultivector();
        }
        
        public static GeoPoTNumMultivector CreateOne()
        {
            return new GeoPoTNumMultivector(new []
            {
                new GeoPoTNumMultivectorTerm(0, 1.0d)
            });
        }
        
        public static GeoPoTNumMultivector CreateSimpleRotor(GeoPoTNumVector sourceVector, GeoPoTNumVector targetVector)
        {
            var invNorm1 = 1.0d / sourceVector.Norm();
            var invNorm2 = 1.0d / targetVector.Norm();
            var cosAngle = sourceVector.DotProduct(targetVector) * invNorm1 * invNorm2;

            if (cosAngle == 1.0d)
                return new GeoPoTNumMultivector().SetTerm(0, 1.0d);
            
            //TODO: Handle the case for cosAngle == -1
            
            var cosHalfAngle = Math.Sqrt(0.5d * (1.0d + cosAngle));
            var sinHalfAngle = Math.Sqrt(0.5d * (1.0d - cosAngle));
            var rotationBlade = sourceVector.Op(targetVector);

            var rotationBladeScalar = 
                sinHalfAngle / 
                Math.Sqrt(Math.Abs(rotationBlade.Gp(rotationBlade).GetTermValue(0)));

            var rotor= cosHalfAngle - rotationBladeScalar * rotationBlade;

            //var rotationAngle = Math.Acos(DotProduct(v2) * invNorm1 * invNorm2) / 2;
            //var unitBlade = rotationBlade.ScaleBy(rotationBladeInvNorm);
            //var unitBladeNorm = unitBlade.Gp(unitBlade).TermsToText();
            //var rotor= Math.Cos(rotationAngle) - (rotationBladeInvNorm * Math.Sin(rotationAngle)) * rotationBlade;

            //Normalize rotor
            //var invRotorNorm = 1.0d / Math.Sqrt(rotor.Gp(rotor.Reverse()).GetTermValue(0));

            //rotor.IsSimpleRotor();
            
            return rotor;
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GeoPoTNumMultivector CreateSimpleRotor(double rotationAngle, GeoPoTNumMultivector rotationBlade)
        {
            var cosHalfAngle = Math.Cos(rotationAngle / 2.0d);
            var sinHalfAngle = Math.Sin(rotationAngle / 2.0d);

            var rotationBladeScalar =
                sinHalfAngle / 
                Math.Sqrt(Math.Abs(rotationBlade.Gp(rotationBlade).GetTermValue(0)));

            var rotor=  
                cosHalfAngle + rotationBladeScalar * rotationBlade;

            //rotor.IsSimpleRotor();

            return rotor;
        }

        public static GeoPoTNumMultivector CreateSimpleRotor(GeoPoTNumVector inputVector1, GeoPoTNumVector inputVector2, GeoPoTNumVector rotatedVector1, GeoPoTNumVector rotatedVector2)
        {
            var inputFrame = GeoPoTNumFrame.Create(inputVector1, inputVector2);
            var rotatedFrame = GeoPoTNumFrame.Create(rotatedVector1, rotatedVector2);

            return GeoPoTNumRotorsSequence.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static GeoPoTNumMultivector CreateSimpleRotor(int baseSpaceDimensions, GeoPoTNumVector inputVector1, GeoPoTNumVector inputVector2, GeoPoTNumVector rotatedVector1, GeoPoTNumVector rotatedVector2)
        {
            var inputFrame = GeoPoTNumFrame.Create(inputVector1, inputVector2);
            var rotatedFrame = GeoPoTNumFrame.Create(rotatedVector1, rotatedVector2);

            return GeoPoTNumRotorsSequence.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();
        }

        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static GeoPoTNumMultivector CreateGivensRotor(int i, int j, double angle)
        {
            Debug.Assert(i > 0 && j > i);

            var cosHalfAngle = Math.Cos(angle / 2.0d);
            var sinHalfAngle = Math.Sin(angle / 2.0d);

            var bladeId = (1 << (i - 1)) | (1 << (j - 1));

            return new GeoPoTNumMultivector()
                .AddTerm(0, cosHalfAngle)
                .AddTerm(bladeId, sinHalfAngle);
        }

        
        public static GeoPoTNumMultivector operator -(GeoPoTNumMultivector v)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(term.IDsPattern, -term.Value);

            return result;
        }

        public static GeoPoTNumMultivector operator +(GeoPoTNumMultivector v1, GeoPoTNumMultivector v2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            return result;
        }

        public static GeoPoTNumMultivector operator +(GeoPoTNumMultivector v1, double v2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, v2);

            return result;
        }

        public static GeoPoTNumMultivector operator +(double v2, GeoPoTNumMultivector v1)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, v2);

            return result;
        }

        public static GeoPoTNumMultivector operator -(GeoPoTNumMultivector v1, GeoPoTNumMultivector v2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    -term.Value
                );

            return result;
        }

        public static GeoPoTNumMultivector operator -(GeoPoTNumMultivector v1, double v2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, -v2);

            return result;
        }

        public static GeoPoTNumMultivector operator -(double v1, GeoPoTNumMultivector v2)
        {
            var result = new GeoPoTNumMultivector();

            result.AddTerm(0, v1);

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    -term.Value
                );

            return result;
        }
        
        public static GeoPoTNumMultivector operator *(GeoPoTNumMultivector v1, GeoPoTNumMultivector v2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in v1._termsDictionary.Values)
            foreach (var term2 in v2._termsDictionary.Values)
            {
                var term = term1.Gp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }
        
        public static GeoPoTNumMultivector operator *(GeoPoTNumMultivector v, double s)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value * s
                );

            return result;
        }

        public static GeoPoTNumMultivector operator *(double s, GeoPoTNumMultivector v)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value * s
                );

            return result;
        }
        
        public static GeoPoTNumMultivector operator /(GeoPoTNumMultivector v1, GeoPoTNumMultivector v2)
        {
            return v1 * v2.Inverse();
        }

        public static GeoPoTNumMultivector operator /(GeoPoTNumMultivector v, double s)
        {
            s = 1.0d / s;
            
            var result = new GeoPoTNumMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value * s
                );

            return result;
        }

        public static GeoPoTNumMultivector operator /(double s, GeoPoTNumMultivector v)
        {
            return s * v.Inverse();
        }

        
        private readonly Dictionary<int, GeoPoTNumMultivectorTerm> _termsDictionary
            = new Dictionary<int, GeoPoTNumMultivectorTerm>();


        public int Count 
            => _termsDictionary.Count;

        
        internal GeoPoTNumMultivector()
        {
        }

        internal GeoPoTNumMultivector(IEnumerable<GeoPoTNumMultivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GeoPoTNumMultivector SetToZero()
        {
            _termsDictionary.Clear();

            return this;
        }
        
        public bool IsZero()
        {
            return Norm2().IsNearZero();
        }

        public bool IsRotor()
        {
            if (GetNonZeroTerms().Select(term => term.GetGrade()).Any(grade => grade % 2 != 0))
                return false;

            var s = Gp(Reverse()) - 1.0d;

            return s.IsZero();
        }

        public bool IsSimpleRotor()
        {
            if (GetNonZeroTerms().Select(term => term.GetGrade()).Any(grade => grade != 0 && grade != 2))
                return false;

            var s = Gp(Reverse()) - 1.0d;

            return s.IsZero();
        }

        public GeoPoTNumMultivector SetTerm(int idsPattern, double value)
        {
            Debug.Assert(idsPattern >= 0);

            if (_termsDictionary.ContainsKey(idsPattern))
                _termsDictionary[idsPattern].Value = value;
            else
                _termsDictionary.Add(idsPattern, new GeoPoTNumMultivectorTerm(idsPattern, value));

            return this;
        }

        public GeoPoTNumMultivector AddTerm(GeoPoTNumMultivectorTerm term)
        {
            var idsPattern = term.IDsPattern;

            if (_termsDictionary.TryGetValue(idsPattern, out var oldTerm))
                _termsDictionary[idsPattern] = 
                    new GeoPoTNumMultivectorTerm(idsPattern, oldTerm.Value + term.Value);
            else
                _termsDictionary.Add(idsPattern, new GeoPoTNumMultivectorTerm(idsPattern, term.Value));

            return this;
        }

        public GeoPoTNumMultivector AddTerm(int idsPattern, double value)
        {
            Debug.Assert(idsPattern >= 0);

            if (_termsDictionary.TryGetValue(idsPattern, out var oldTerm))
                _termsDictionary[idsPattern] = 
                    new GeoPoTNumMultivectorTerm(idsPattern, oldTerm.Value + value);
            else
                _termsDictionary.Add(idsPattern, new GeoPoTNumMultivectorTerm(idsPattern, value));

            return this;
        }

        public GeoPoTNumMultivector AddTerms(IEnumerable<GeoPoTNumMultivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }

        
        public IEnumerable<GeoPoTNumMultivectorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsNearZero());
        }

        public IEnumerable<GeoPoTNumMultivectorTerm> GetGradeOrderedTerms()
        {
            var bitsCount = _termsDictionary.Keys.Max().LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return _termsDictionary.Values;

            return _termsDictionary
                .Values
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.GetGrade())
                .ThenByDescending(t => t.IDsPattern.ReverseBits(bitsCount));
        }

        public IEnumerable<GeoPoTNumMultivectorTerm> GetNonZeroTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsNearZero());
        }

        public IEnumerable<GeoPoTNumMultivectorTerm> GetTermsOfGrade(int grade)
        {
            Debug.Assert(grade >= 0);
            
            return GetTerms().Where(t => t.GetGrade() == grade);
        }

        public double GetTermValue(int idsPattern)
        {
            Debug.Assert(idsPattern >= 0);
            
            return _termsDictionary.TryGetValue(idsPattern, out var term) 
                ? term.Value 
                : 0.0d;
        }

        public double GetScalar()
        {
            return GetTermValue(0);
        }

        public GeoPoTNumMultivectorTerm GetTerm(int idsPattern)
        {
            var value = GetTermValue(idsPattern);

            return new GeoPoTNumMultivectorTerm(idsPattern, value);
        }

        public GeoPoTNumMultivector GetKVectorPart(int grade)
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Where(t => t.GetGrade() == grade)
            );
        }

        public GeoPoTNumVector GetVectorPart()
        {
            return new GeoPoTNumVector(
                GetTermsOfGrade(1).Select(t => t.ToVectorTerm())
            );
        }

        public GeoPoTNumBiversor GetBiversorPart()
        {
            var biversor = new GeoPoTNumBiversor();

            var scalarValue = GetTermValue(0);

            if (scalarValue != 0.0d)
                biversor.AddTerm(new GeoPoTNumBiversorTerm(scalarValue));

            biversor.AddTerms(
                GetTermsOfGrade(2).Select(t => t.ToBiversorTerm())
            );

            return biversor;
        }

        /// <summary>
        /// Assuming this multivector is a simple rotor, this extracts its angle and 2-blade of rotation
        /// </summary>
        /// <returns></returns>
        public Tuple<double, GeoPoTNumMultivector> GetSimpleRotorAngleBlade()
        {
            var scalarPart = GetTermValue(0);
            var bivectorPart = new GeoPoTNumMultivector(GetTermsOfGrade(2));

            var halfAngle = Math.Acos(scalarPart);
            var angle = 2.0d * halfAngle;
            var blade = bivectorPart / Math.Sin(halfAngle);

            return new Tuple<double, GeoPoTNumMultivector>(angle, blade);
        }

        public GeoPoTNumMultivector Op(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Op(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }
        
        public GeoPoTNumMultivector Op(GeoPoTNumVector v)
        {
            return Op(v.ToMultivector());
        }

        public GeoPoTNumMultivector Sp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            {
                if (!mv2._termsDictionary.TryGetValue(term1.IDsPattern, out var term2)) 
                    continue;
                
                var value = term1.Value * term2.Value;

                if (value == 0)
                    continue;
                    
                if (GeoPoTNumUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                    value = -value;

                result.AddTerm(new GeoPoTNumMultivectorTerm(0, value));
            }

            return result;
        }

        public GeoPoTNumMultivector Lcp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Lcp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Rcp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Rcp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Fdp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Fdp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Hip(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Hip(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Acp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Acp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Cp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Cp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Gp(GeoPoTNumMultivector mv2)
        {
            var result = new GeoPoTNumMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Gp(term2);

                if (term.Value != 0)
                    result.AddTerm(term);
            }

            return result;
        }

        public GeoPoTNumMultivector Add(GeoPoTNumMultivector v)
        {
            return this + v;
        }

        public GeoPoTNumMultivector Subtract(GeoPoTNumMultivector v)
        {
            return this - v;
        }

        public GeoPoTNumMultivector Negative()
        {
            return -this;
        }

        public GeoPoTNumMultivector ScaleBy(double s)
        {
            return s * this;
        }

        public GeoPoTNumMultivector MapScalars(Func<double, double> mappingFunc)
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(
                    t => new GeoPoTNumMultivectorTerm(t.IDsPattern, mappingFunc(t.Value))
                )
            );
        }

        public GeoPoTNumMultivector Reverse()
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(t => t.Reverse())
            );
        }

        public GeoPoTNumMultivector GradeInvolution()
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(t => t.GradeInvolution())
            );
        }

        public GeoPoTNumMultivector CliffordConjugate()
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(t => t.CliffordConjugate())
            );
        }

        public GeoPoTNumMultivector ScaledReverse(double s)
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(t => t.ScaledReverse(s))
            );
        }

        public GeoPoTNumMultivector OrthogonalComplement(GeoPoTNumMultivector blade)
        {
            return Gp(blade.Inverse());
        }

        public GeoPoTNumMultivector Round(int places)
        {
            return new GeoPoTNumMultivector(
                _termsDictionary.Values.Select(t => t.Round(places)).Where(t => !t.Value.IsNearZero())
            );
        }

        public double Norm()
        {
            return Math.Sqrt(Norm2());
        }

        public double Norm2()
        {
            return _termsDictionary
                .Values
                .Sum(term => term.Value * term.Value);
        }

        public GeoPoTNumMultivector Inverse()
        {
            var norm2 = 0.0d;
            var termsArray = new GeoPoTNumMultivectorTerm[_termsDictionary.Count];

            var i = 0;
            foreach (var term in _termsDictionary.Values)
            {
                termsArray[i] = new GeoPoTNumMultivectorTerm(
                    term.IDsPattern,
                    term.IDsPattern.BasisBladeHasNegativeReverse()
                        ? -term.Value 
                        : term.Value
                );

                norm2 += term.Value * term.Value;

                i++;
            }

            var invNorm2 = 1.0d / norm2;

            foreach (var term in termsArray)
                term.Value *= invNorm2;
            
            return new GeoPoTNumMultivector(termsArray);
        }

        public GeoPoTNumMultivector DivideByNorm()
        {
            return this / Norm();
        }

        public GeoPoTNumMultivector DivideByNorm2()
        {
            return this / Norm2();
        }

        public GeoPoTNumMultivector ApplyRotor(GeoPoTNumMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return r1.Gp(this).Gp(r2);
        }


        public string ToText()
        {
            return TermsToText();
        }

        public string TermsToText()
        {
            var termsArray = 
                GetGradeOrderedTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }

        public string ToLaTeX()
        {
            return TermsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            var termsArray = 
                GetGradeOrderedTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : string.Join(" + ", termsArray.Select(t => t.ToLaTeX()));
        }

        public string ToLaTeXEquationsArray(string multivectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            var termsArray = 
                GetNonZeroTerms()
                    .OrderBy(t => t.GetGrade())
                    .ThenBy(t => t.IDsPattern)
                    .ToArray();

            var j = 0;
            foreach (var term in termsArray)
            {
                var termLaTeX = term
                        .ToLaTeX()
                        .Replace(@"\sigma_", $"{basisName}_");

                var line = j == 0
                    ? $@"{multivectorName} & = & {termLaTeX}"
                    : $@" & + & {termLaTeX}";

                if (j < termsArray.Length - 1)
                    line += @"\\";

                textComposer.AppendLine(line);

                j++;
            }

            textComposer.AppendLine(@"\end{eqnarray*}");

            return textComposer.ToString();
        }

        public override string ToString()
        {
            return TermsToText();
        }

        public IEnumerator<GeoPoTNumMultivectorTerm> GetEnumerator()
        {
            return _termsDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
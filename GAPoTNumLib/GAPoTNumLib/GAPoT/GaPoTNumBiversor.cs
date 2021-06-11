using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GAPoTNumLib.Interop.MATLAB;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumBiversor : IEnumerable<GaPoTNumBiversorTerm>
    {
        public static GaPoTNumBiversor operator -(GaPoTNumBiversor v)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values.Select(t => -t)
            );
        }

        public static GaPoTNumBiversor operator +(GaPoTNumBiversor v1, GaPoTNumBiversor v2)
        {
            var biversor = new GaPoTNumBiversor();

            biversor.AddTerms(v1._termsDictionary.Values);
            biversor.AddTerms(v2._termsDictionary.Values);

            return biversor;
        }

        public static GaPoTNumBiversor operator -(GaPoTNumBiversor v1, GaPoTNumBiversor v2)
        {
            var biversor = new GaPoTNumBiversor();

            biversor.AddTerms(v1._termsDictionary.Values);
            biversor.AddTerms(v2._termsDictionary.Values.Select(t => -t));

            return biversor;
        }

        public static GaPoTNumBiversor operator +(GaPoTNumBiversor v, double s)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, s);
        }

        public static GaPoTNumBiversor operator +(double s, GaPoTNumBiversor v)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, s);
        }

        public static GaPoTNumBiversor operator -(GaPoTNumBiversor v, double s)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, -s);
        }

        public static GaPoTNumBiversor operator -(double s, GaPoTNumBiversor v)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values.Select(t => -t)
            ).AddTerm(0,0, s);
        }

        public static GaPoTNumBiversor operator *(GaPoTNumBiversor v, double s)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GaPoTNumBiversor operator *(double s, GaPoTNumBiversor v)
        {
            return new GaPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GaPoTNumBiversor operator /(GaPoTNumBiversor v, double s)
        {
            s = 1.0d / s;

            return new GaPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GaPoTNumBiversor operator /(double s, GaPoTNumBiversor v)
        {
            return s * v.Inverse();
        }


        private readonly Dictionary2Keys<int, GaPoTNumBiversorTerm> _termsDictionary
            = new Dictionary2Keys<int, GaPoTNumBiversorTerm>();


        public double this[int id1, int id2]
        {
            get
            {
                (id1, id2) = GaPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

                return _termsDictionary
                    .Values
                    .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                    .Select(v => v.Value)
                    .Sum();
            }
        }


        internal GaPoTNumBiversor()
        {
        }

        internal GaPoTNumBiversor(IEnumerable<GaPoTNumBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GaPoTNumBiversor AddTerm(GaPoTNumBiversorTerm term)
        {
            //The input term IDs is already validation by construction, no validation is needed here
            var id1 = term.TermId1;
            var id2 = term.TermId2;

            if (_termsDictionary.TryGetValue(id1, id2, out var oldTerm))
                _termsDictionary[id1, id2] = 
                    new GaPoTNumBiversorTerm(id1, id2, oldTerm.Value + term.Value);
            else
                _termsDictionary.Add(id1, id2, term);

            return this;
        }

        public GaPoTNumBiversor AddTerm(int id1, int id2, double value)
        {
            return AddTerm(
                new GaPoTNumBiversorTerm(id1, id2, value)
            );
        }

        public GaPoTNumBiversor AddTerms(IEnumerable<GaPoTNumBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public IEnumerable<GaPoTNumBiversorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsNearZero());
        }

        public double GetTermValuesSum()
        {
            return _termsDictionary
                .Values
                .Select(v => v.Value)
                .Sum();
        }


        public double GetTermValue(int id1, int id2)
        {
            (id1, id2) = GaPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

            return _termsDictionary
                .Values
                .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                .Select(v => v.Value)
                .Sum();
        }

        public GaPoTNumBiversorTerm GetTerm(int id1, int id2)
        {
            var value = GetTermValue(id1, id2);

            return new GaPoTNumBiversorTerm(id1, id2, value);
        }


        public double GetActiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsScalar)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetNonActiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsNonScalar)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetReactiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsPhasor)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetReactiveFundamentalTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.TermId1 == 1 && t.TermId2 == 2)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetHarmTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
                .Select(v => v.Value)
                .Sum();
        }

        public double GetScalar()
        {
            return GetTermValue(0, 0);
        }


        public GaPoTNumBiversor GetBivectorPart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar)
            );
        }

        public GaPoTNumBiversor GetTermPart(int id1, int id2)
        {
            (id1, id2) = GaPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == id1 && t.TermId2 == id2)
            );
        }

        public GaPoTNumBiversor GetActivePart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsScalar)
            );
        }

        public GaPoTNumBiversor GetNonActivePart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar)
            );
        }

        public GaPoTNumBiversor GetReactivePart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsPhasor)
            );
        }

        public GaPoTNumBiversor GetReactiveFundamentalPart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == 1 && t.TermId2 == 2)
            );
        }

        public GaPoTNumBiversor GetHarmPart()
        {
            return new GaPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
            );
        }


        public GaPoTNumBiversor Reverse()
        {
            var result = new GaPoTNumBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(pair.Value);
                else
                    result.AddTerm(-pair.Value);
            }

            return result;
        }

        public GaPoTNumBiversor Negative()
        {
            var result = new GaPoTNumBiversor();

            foreach (var pair in _termsDictionary) 
                result.AddTerm(-pair.Value);

            return result;
        }

        public GaPoTNumBiversor ScaleBy(double s)
        {
            var result = new GaPoTNumBiversor();

            foreach (var pair in _termsDictionary) 
                result.AddTerm(s * pair.Value);

            return result;
        }

        public GaPoTNumBiversor NegativeReverse()
        {
            var result = new GaPoTNumBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(-pair.Value);
                else
                    result.AddTerm(pair.Value);
            }

            return result;
        }

        public double Norm()
        {
            return Math.Sqrt(Norm2());
        }

        public double Norm2()
        {
            return _termsDictionary
                .Values
                .Select(t => t.Value * t.Value)
                .Sum();
        }

        public GaPoTNumBiversor Inverse()
        {
            var norm2 = Norm2();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var value = 1.0d / norm2;

            return new GaPoTNumBiversor(
                _termsDictionary
                    .Values
                    .Select(t => t.ScaledReverse(value))
            );
        }

        public GaPoTNumBiversor DivideByNorm()
        {
            return this / Norm();
        }

        public GaPoTNumBiversor DivideByNorm2()
        {
            return this / Norm2();
        }

        public GaPoTNumVector Gp(GaPoTNumVector v)
        {
            return this * v;
        }


        public GaNumMatlabSparseMatrixData TermsToMatlabArray(int rowsCount)
        {
            return GetTerms().TermsToMatlabArray(rowsCount);
        }

        public GaPoTNumMultivector ToMultivector()
        {
            return new GaPoTNumMultivector(
                GetTerms().Select(t => t.ToMultivectorTerm())
            );
        }

        public string ToText()
        {
            return TermsToText();
        }

        public string TermsToText()
        {
            var termsArray = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId1)
                .ThenBy(t => t.TermId2)
                .ToArray();

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
            var termsArray = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId1)
                .ThenBy(t => t.TermId2)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : string.Join(" + ", termsArray.Select(t => t.ToLaTeX()));
        }

        public override string ToString()
        {
            return TermsToText();
        }

        public IEnumerator<GaPoTNumBiversorTerm> GetEnumerator()
        {
            return _termsDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
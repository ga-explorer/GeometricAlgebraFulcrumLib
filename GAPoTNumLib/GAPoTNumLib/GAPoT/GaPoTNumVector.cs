using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.Interop.MATLAB;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumVector : IEnumerable<GeoPoTNumVectorTerm>
    {
        public static GeoPoTNumVector CreateZero()
        {
            return new GeoPoTNumVector();
        }

        public static GeoPoTNumVector CreateAutoVector(int n)
        {
            return new GeoPoTNumVector(
                Enumerable.Repeat(1.0d, n)
            );
        }

        public static GeoPoTNumVector CreateUnitAutoVector(int n)
        {
            var d = 1.0d / Math.Sqrt(n);

            return new GeoPoTNumVector(
                Enumerable.Repeat(d, n)
            );
        }

        public static GeoPoTNumVector operator -(GeoPoTNumVector v)
        {
            var result = new GeoPoTNumVector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(term.TermId, -term.Value);

            return result;
        }

        public static GeoPoTNumVector operator +(GeoPoTNumVector v1, GeoPoTNumVector v2)
        {
            var result = new GeoPoTNumVector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value
                );

            return result;
        }

        public static GeoPoTNumVector operator -(GeoPoTNumVector v1, GeoPoTNumVector v2)
        {
            var result = new GeoPoTNumVector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    -term.Value
                );

            return result;
        }

        /// <summary>
        /// The geometric product of two GAPoT vectors is a biversor
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static GeoPoTNumBiversor operator *(GeoPoTNumVector v1, GeoPoTNumVector v2)
        {
            var biversor = new GeoPoTNumBiversor();

            foreach (var term1 in v1.GetTerms())
            {
                foreach (var term2 in v2.GetTerms())
                {
                    var scalarValue = term1.Value * term2.Value;

                    biversor.AddTerm(
                        term1.TermId, 
                        term2.TermId, 
                        scalarValue
                    );
                }
            }

            return biversor;
        }

        public static GeoPoTNumVector operator *(GeoPoTNumBiversor bv1, GeoPoTNumVector v2)
        {
            var vector = new GeoPoTNumVector();

            foreach (var term1 in bv1.GetTerms())
            {
                foreach (var term2 in v2.GetTerms())
                {
                    var scalarValue = term1.Value * term2.Value;

                    if (term1.IsScalar)
                    {
                        vector.AddTerm(
                            term2.TermId, 
                            scalarValue
                        );

                        continue;
                    }

                    if (term1.TermId1 == term2.TermId)
                    {
                        vector.AddTerm(
                            term1.TermId2, 
                            -scalarValue
                        );

                        continue;
                    }
                    
                    if (term1.TermId2 == term2.TermId)
                    {
                        vector.AddTerm(
                            term1.TermId1, 
                            scalarValue
                        );
                    }
                }
            }

            return vector;
        }

        public static GeoPoTNumVector operator *(GeoPoTNumVector v1, GeoPoTNumBiversor bv2)
        {
            var vector = new GeoPoTNumVector();

            foreach (var term1 in v1.GetTerms())
            {
                foreach (var term2 in bv2.GetTerms())
                {
                    var scalarValue = term1.Value * term2.Value;

                    if (term2.IsScalar)
                    {
                        vector.AddTerm(
                            term1.TermId, 
                            scalarValue
                        );

                        continue;
                    }

                    if (term1.TermId == term2.TermId1)
                    {
                        vector.AddTerm(
                            term2.TermId2, 
                            scalarValue
                        );

                        continue;
                    }
                    
                    if (term1.TermId == term2.TermId2)
                    {
                        vector.AddTerm(
                            term2.TermId1, 
                            -scalarValue
                        );

                    }
                }
            }

            return vector;
        }

        public static GeoPoTNumVector operator *(GeoPoTNumVector v, double s)
        {
            var result = new GeoPoTNumVector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value * s
                );

            return result;
        }

        public static GeoPoTNumVector operator *(double s, GeoPoTNumVector v)
        {
            var result = new GeoPoTNumVector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value * s
                );

            return result;
        }

        /// <summary>
        /// The geometric product of two GAPoT vectors is a biversor
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static GeoPoTNumBiversor operator /(GeoPoTNumVector v1, GeoPoTNumVector v2)
        {
            return v1 * v2.Inverse();
        }

        public static GeoPoTNumVector operator /(GeoPoTNumBiversor bv1, GeoPoTNumVector v2)
        {
            return bv1 * v2.Inverse();
        }

        public static GeoPoTNumVector operator /(GeoPoTNumVector v1, GeoPoTNumBiversor bv2)
        {
            return v1 * bv2.Inverse();
        }

        public static GeoPoTNumVector operator /(GeoPoTNumVector v, double s)
        {
            s = 1.0d / s;

            var result = new GeoPoTNumVector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.TermId,
                    term.Value * s
                );

            return result;
        }

        public static GeoPoTNumVector operator /(double s, GeoPoTNumVector v)
        {
            return s * v.Inverse();
        }


        private readonly SortedDictionary<int, GeoPoTNumVectorTerm> _termsDictionary
            = new SortedDictionary<int, GeoPoTNumVectorTerm>();


        public int Count 
            => _termsDictionary.Count;

        public double this[int index] 
            => _termsDictionary.TryGetValue(index, out var value) 
                ? value.Value 
                : 0;


        public GeoPoTNumVector()
        {
        }

        public GeoPoTNumVector(params double[] valuesList)
        {
            for (var i = 0; i < valuesList.Length; i++)
                AddTerm(i + 1, valuesList[i]);
        }

        public GeoPoTNumVector(IEnumerable<double> valuesList)
        {
            var i = 1;
            foreach (var value in valuesList)
            {
                AddTerm(i, value);

                i++;
            }
        }

        public GeoPoTNumVector(IEnumerable<GeoPoTNumVectorTerm> termsList)
        {
            AddTerms(termsList);
        }


        public GeoPoTNumVector SetToZero()
        {
            _termsDictionary.Clear();

            return this;
        }


        public bool IsZero()
        {
            return Norm2().IsNearZero();
        }

        //public bool HasDcTerm()
        //{
        //    return _termsDictionary.TryGetValue(0, out var term) && 
        //           term.Value != 0;
        //}

        public GeoPoTNumVector SetTerm(int id, double value)
        {
            Debug.Assert(id > 0);

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id].Value = value;
            else
                _termsDictionary.Add(id, new GeoPoTNumVectorTerm(id, value));

            return this;
        }

        public GeoPoTNumVector SetPolarPhasor(int id, double magnitude, double phase)
        {
            return SetRectPhasor(
                id,
                magnitude * Math.Cos(phase),
                magnitude * Math.Sin(phase)
            );
        }

        public GeoPoTNumVector SetRectPhasor(int id, double x, double y)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            SetTerm(id, x);

            SetTerm(id + 1, -y);

            return this;
        }


        public GeoPoTNumVector AddTerm(int id, double value)
        {
            Debug.Assert(id > 0);

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id].Value += value;
            else
                _termsDictionary.Add(id, new GeoPoTNumVectorTerm(id, value));

            return this;
        }
        
        public GeoPoTNumVector AddTerm(GeoPoTNumVectorTerm term)
        {
            return AddTerm(term.TermId, term.Value);
        }
        
        public GeoPoTNumVector AddTerms(IEnumerable<GeoPoTNumVectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term.TermId, term.Value);

            return this;
        }

        public GeoPoTNumVector AddTerms(GeoNumMatlabSparseMatrixData matlabArray)
        {
            for (var sparseIndex = 0; sparseIndex < matlabArray.ItemsCount; sparseIndex++)
                AddTerm(
                    matlabArray.RowIndicesArray[sparseIndex],
                    matlabArray.ValuesArray[sparseIndex]
                );

            return this;
        }

        public GeoPoTNumVector AddPolarPhasor(int id, double magnitude, double phase)
        {
            return AddRectPhasor(
                id,
                magnitude * Math.Cos(phase),
                magnitude * Math.Sin(phase)
            );
        }

        public GeoPoTNumVector AddPolarPhasor(GeoPoTNumPolarPhasor phasor)
        {
            return AddPolarPhasor(phasor.Id, phasor.Magnitude, phasor.Phase);
        }

        public GeoPoTNumVector AddPolarPhasors(GeoNumMatlabSparseMatrixData matlabArray)
        {
            var array = matlabArray.GetArray();

            for (var i = 0; i < matlabArray.RowsCount; i++)
                AddPolarPhasor(
                    i * 2 + 1, 
                    array[i, 0],
                    array[i, 1]
                );

            return this;
        }

        public GeoPoTNumVector AddRectPhasor(int id, double x, double y)
        {
            Debug.Assert(id > 0 && id % 2 == 1);
            
            AddTerm(id, x);

            AddTerm(id + 1, -y);

            return this;
        }

        public GeoPoTNumVector AddRectPhasor(GeoPoTNumRectPhasor phasor)
        {
            return AddRectPhasor(phasor.Id, phasor.XValue, phasor.YValue);
        }


        public GeoPoTNumVectorTerm GetTerm(int id)
        {
            Debug.Assert(id > 0);
            
            if (_termsDictionary.TryGetValue(id, out var term))
                return new GeoPoTNumVectorTerm(term.TermId, term.Value);

            return new GeoPoTNumVectorTerm(id);
        }

        public GeoPoTNumPolarPhasor GetPolarPhasor(int id)
        {
            return GetRectPhasor(id).ToPolarPhasor();
        }

        public GeoPoTNumRectPhasor GetRectPhasor(int id)
        {
            Debug.Assert(id > 0 && id % 2 == 1);
            
            var x = this[id];
            var y = -this[id + 1];

            return new GeoPoTNumRectPhasor(id, x, y);
        }

        public GeoPoTNumVector AddRectPhasors(GeoNumMatlabSparseMatrixData matlabArray)
        {
            var array = matlabArray.GetArray();

            for (var i = 0; i < matlabArray.RowsCount; i++)
                AddRectPhasor(
                    i * 2 + 1, 
                    array[i, 0],
                    array[i, 1]
                );

            return this;
        }

        public GeoPoTNumVector GetPartByTermIDs(params int[] termIDsList)
        {
            return new GeoPoTNumVector(
                GetTerms().Where(t => termIDsList.Contains(t.TermId))
            );
        }
        
        public GeoPoTNumVector GetPartByTermIDsRange(int minTermId, int maxTermId)
        {
            return new GeoPoTNumVector(
                GetTerms().Where(t => t.TermId >= minTermId && t.TermId <= maxTermId)
            );
        }
        
        public GeoPoTNumVector GetOffsetPartByTermIDsRange(int minTermId, int maxTermId)
        {
            var termsList = 
                GetTerms()
                    .Where(t => t.TermId >= minTermId && t.TermId <= maxTermId)
                    .Select(t => t.OffsetTermId(1 - minTermId));

            return new GeoPoTNumVector(termsList);
        }

        public GeoPoTNumVector[] GetParts(params int[] partLengthsArray)
        {
            var results = new GeoPoTNumVector[partLengthsArray.Length];

            var termId1 = 1;
            for (var i = 0; i < partLengthsArray.Length; i++)
            {
                var termId2 = termId1 + partLengthsArray[i] - 1;

                results[i] = GetPartByTermIDsRange(termId1, termId2);

                termId1 = termId2 + 1;
            }

            return results;
        }

        public GeoPoTNumVector[] GetOffsetParts(params int[] partLengthsArray)
        {
            var results = new GeoPoTNumVector[partLengthsArray.Length];

            var termId1 = 1;
            for (var i = 0; i < partLengthsArray.Length; i++)
            {
                var termId2 = termId1 + partLengthsArray[i] - 1;

                results[i] = GetOffsetPartByTermIDsRange(termId1, termId2);

                termId1 = termId2 + 1;
            }

            return results;
        }

        public GeoPoTNumBiversor[] GetPartsImpedance(GeoPoTNumVector current, params int[] partLengthsArray)
        {
            var mvU = GetParts(partLengthsArray);
            var mvI = current.GetParts(partLengthsArray).Inverse();

            return mvU.Gp(mvI);
        }

        public IEnumerable<GeoPoTNumVectorTerm> GetTerms()
        {
            return _termsDictionary.Values;
        }

        //public GeoPoTNumVectorTerm GetDcTerm()
        //{
        //    return GetTerm(0);
        //}

        public IEnumerable<GeoPoTNumPolarPhasor> GeTPolarPhasors()
        {
            return GeTRectPhasors().Select(p => p.ToPolarPhasor());
        }

        public IEnumerable<GeoPoTNumRectPhasor> GeTRectPhasors()
        {
            var phasorsDict = new Dictionary<int, GeoPoTNumRectPhasor>();

            foreach (var term in _termsDictionary.Values)
            {
                var id = term.TermId;
                var x = 0.0d;
                var y = 0.0d;

                if (id % 2 == 1)
                {
                    x = term.Value;
                }
                else
                {
                    id--;
                    y = -term.Value;
                }

                if (phasorsDict.TryGetValue(id, out var phasor))
                {
                    phasor.XValue += x;
                    phasor.YValue += y;
                }
                else
                {
                    phasorsDict.Add(id, new GeoPoTNumRectPhasor(id, x, y));
                }
            }

            return phasorsDict.Values;
        }


        public double DotProduct(GeoPoTNumVector v)
        {
            var result = 0.0d;

            foreach (var term1 in _termsDictionary.Values)
            {
                if (v._termsDictionary.TryGetValue(term1.TermId, out var term2)) 
                    result += term1.Value * term2.Value;
            }

            return result;
        }

        public double GetAngle(GeoPoTNumVector v)
        {
            return Math.Acos(DotProduct(v) / Math.Sqrt(Norm2() * v.Norm2()));
        }

        public GeoPoTNumMultivector Op(GeoPoTNumVector v)
        {
            return ToMultivector().Op(v.ToMultivector());
        }
        
        public GeoPoTNumMultivector Op(GeoPoTNumMultivector v)
        {
            return ToMultivector().Op(v);
        }
        
        public GeoPoTNumMultivector Gp(GeoPoTNumMultivector v)
        {
            return ToMultivector().Gp(v);
        }
        
        public GeoPoTNumBiversor Gp(GeoPoTNumVector v)
        {
            return this * v;
        }

        public GeoPoTNumVector Gp(GeoPoTNumBiversor bv)
        {
            return this * bv;
        }

        public GeoPoTNumVector Add(GeoPoTNumVector v)
        {
            return this + v;
        }

        public GeoPoTNumVector Subtract(GeoPoTNumVector v)
        {
            return this - v;
        }

        public GeoPoTNumVector Negative()
        {
            return -this;
        }

        public GeoPoTNumVector ScaleBy(double s)
        {
            return s * this;
        }

        public GeoPoTNumVector Reverse()
        {
            return this;
        }

        public GeoPoTNumMultivector GetRotorToVector(GeoPoTNumVector v2)
        {
            return GeoPoTNumMultivector.CreateSimpleRotor(this, v2);
        }
        
        public GeoPoTNumVector ApplyRotor(GeoPoTNumMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();
            var v = ToMultivector();

            var mv = r1.Gp(v).Gp(r2);

            return mv.GetVectorPart();
        }

        public GeoPoTNumVector ApplyRotor(GeoPoTNumBiversor rotor)
        {
            var r1 = rotor.ToMultivector();
            var r2 = rotor.Reverse().ToMultivector();
            var v = ToMultivector();

            var mv = r1.Gp(v).Gp(r2);

            return mv.GetVectorPart();
        }

        public GeoPoTNumVector GetProjectionOnFrame(GeoPoTNumFrame frame)
        {
            var vector = new GeoPoTNumVector();

            foreach (var term in _termsDictionary.Values)
            {
                var i = term.TermId - 1;
                var v = term.Value;

                vector += v * frame[i];
            }

            return vector;
        }

        public GeoPoTNumVector GetProjectionOnBlade(GeoPoTNumMultivector blade)
        {
            return ToMultivector().Lcp(blade).Lcp(blade.Inverse()).GetVectorPart();
        }

        public GeoPoTNumVector Round(int places)
        {
            return new GeoPoTNumVector(
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
                .Select(p => p.Value * p.Value)
                .Sum();
        }

        public GeoPoTNumVector Inverse()
        {
            var norm2 = Norm2();

            var result = new GeoPoTNumVector();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var invNorm2 = 1.0d / norm2;

            foreach (var term in _termsDictionary.Values)
                result.SetTerm(
                    term.TermId,
                    term.Value * invNorm2
                );

            return result;
        }

        public GeoPoTNumVector DivideByNorm()
        {
            return this / Norm();
        }

        public GeoPoTNumVector DivideByNorm2()
        {
            return this / Norm2();
        }

        public GeoPoTNumVector OffsetTermIDs(int delta)
        {
            return new GeoPoTNumVector(
                _termsDictionary
                    .Values
                    .Select(t => t.OffsetTermId(delta))
            );
        }


        public GeoNumMatlabSparseMatrixData TermsToMatlabArray(int rowsCount)
        {
            return GetTerms().TermsToMatlabArray(rowsCount);
        }

        public GeoNumMatlabSparseMatrixData PartsTermsToMatlabArray(int rowsCount, params int[] partLengthArray)
        {
            return GetOffsetParts(partLengthArray).TermsToMatlabArray(rowsCount);
        }

        public GeoNumMatlabSparseMatrixData PolarPhasorsToMatlabArray(int rowsCount)
        {
            return GeTPolarPhasors().PolarPhasorsToMatlabArray(rowsCount);
        }

        public GeoNumMatlabSparseMatrixData RectPhasorsToMatlabArray(int rowsCount)
        {
            return GeTRectPhasors().RectPhasorsToMatlabArray(rowsCount);
        }


        public GeoPoTNumMultivector ToMultivector()
        {
            return new GeoPoTNumMultivector(
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
                .Where(t => !t.IsZero())
                .OrderBy(t => t.TermId)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }

        public string PolarPhasorsToText()
        {
            //var dcTerm = GetTerm(0);

            var termsArray = GeTPolarPhasors()
                .Where(t => !t.IsZero())
                .OrderBy(t => t.Id)
                .ToArray();

            //if (dcTerm.IsZero())
                return termsArray.Length == 0
                    ? "0"
                    : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);

            //return termsArray.Length == 0
            //    ? dcTerm.ToText()
            //    : dcTerm.ToText() + ", " + termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }

        public string RectPhasorsToText()
        {
            //var dcTerm = GetTerm(0);

            var termsArray = GeTRectPhasors()
                .Where(t => !t.IsZero())
                .OrderBy(t => t.Id)
                .ToArray();

            //if (dcTerm.IsZero())
                return termsArray.Length == 0
                    ? "0"
                    : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);

            //return termsArray.Length == 0
            //    ? dcTerm.ToText()
            //    : dcTerm.ToText() + ", " + termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }


        public string ToLaTeX()
        {
            return PolarPhasorsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            var termsArray = GetTerms()
                .Where(t => !t.IsZero())
                .OrderBy(t => t.TermId)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }

        public string PolarPhasorsToLaTeX()
        {
            //var dcTerm = GetTerm(0);

            var termsArray = GeTPolarPhasors()
                .Where(t => !t.IsZero())
                .OrderBy(t => t.Id)
                .ToArray();

            //if (dcTerm.IsZero())
                return termsArray.Length == 0
                    ? "0"
                    : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");

            //return termsArray.Length == 0
            //    ? dcTerm.ToLaTeX()
            //    : dcTerm.ToLaTeX() + " + " + termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }

        public string RectPhasorsToLaTeX()
        {
            //var dcTerm = GetTerm(0);

            var termsArray = GeTRectPhasors()
                .Where(t => !t.IsZero())
                .OrderBy(t => t.Id)
                .ToArray();

            //if (dcTerm.IsZero())
                return termsArray.Length == 0
                    ? "0"
                    : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");

            //return termsArray.Length == 0
            //    ? dcTerm.ToLaTeX()
            //    : dcTerm.ToLaTeX() + " + " + termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }

        public override string ToString()
        {
            return ToText();
        }

        public IEnumerator<GeoPoTNumVectorTerm> GetEnumerator()
        {
            return _termsDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _termsDictionary.Values.GetEnumerator();
        }
    }
}

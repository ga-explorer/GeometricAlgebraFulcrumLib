namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public sealed partial class GaLibraryComposer
    {
        #region GaClcCode
        internal const string GaClcCodeTemplates =
@"
delimiters < >

begin factor_struct_member
    f<num> : Multivector
end factor_struct_member

begin factor_struct
structure BladeFactorStruct(
    <members>
    )
end factor_struct

begin factor_macro_step
let final.f<num> = (inputVectors.f<num> lcp B) lcp B
let B = final.f<num> lcp B

end factor_macro_step

begin factor_macro
macro Factor<num>(B : Multivector, inputVectors : BladeFactorStruct) : BladeFactorStruct
begin
    declare final : BladeFactorStruct
    
    <steps>
    
    let final.f<num> = B
    
    return final
end
end factor_macro

begin vectors_op_macro
macro VectorsOP(<vop_inputs>) : Multivector
begin
    return <vop_expr>
end
end vectors_op_macro

begin sdf_opns_macro
macro SdfOpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
begin
    let mv1 = PointToMultivector3D(x, y, z) op mv
    
    return mv1 sp reverse(mv1)
end
end sdf_opns_macro

begin sdf_ipns_macro
macro SdfIpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
begin
    let mv1 = PointToMultivector3D(x, y, z) lcp mv
    
    return mv1 sp reverse(mv1)
end
end sdf_ipns_macro

begin sdf_ray_step_result_struct
structure SdfRayStepResult (sdf0 : scalar, sdf1 : scalar)
end sdf_ray_step_result_struct

begin sdf_normal_result_struct
structure SdfNormalResult (d1 : scalar, d2 : scalar, d3 : scalar, d4 : scalar)
end sdf_normal_result_struct

begin sdf_ray_steps_macro
macro cga5d.SdfRayStep<kind>(
    mv : Multivector,
    rayOriginX : scalar,
	rayOriginY : scalar,
	rayOriginZ : scalar,
    rayDirectionX : scalar,
	rayDirectionY : scalar,
	rayDirectionZ : scalar,
    dt : scalar,
    t0 : scalar
) : SdfRayStepResult
begin
    let x0 = rayOriginX + (t0 * rayDirectionX)
	let y0 = rayOriginY + (t0 * rayDirectionY)
	let z0 = rayOriginZ + (t0 * rayDirectionZ)
	
    let x1 = x0 + (dt * rayDirectionX)
	let y1 = y0 + (dt * rayDirectionY)
	let z1 = z0 + (dt * rayDirectionZ)
    
    let sdf0 = Sdf<kind>(mv, x0, y0, z0)
    let sdf1 = Sdf<kind>(mv, x1, y1, z1)
    
    return SdfRayStepResult(sdf0, sdf1)
end
end sdf_ray_steps_macro

begin sdf_normal_macro
macro cga5d.SdfNormal<kind>(
    mv : Multivector,
    x : scalar,
	y : scalar,
	z : scalar,
    dt : scalar
) : SdfNormalResult
begin
    let d1 = Sdf<kind>(mv, x + dt, y - dt, z - dt)
    let d2 = Sdf<kind>(mv, x - dt, y - dt, z + dt)
    let d3 = Sdf<kind>(mv, x - dt, y + dt, z - dt)
    let d4 = Sdf<kind>(mv, x + dt, y + dt, z + dt)
    
    return SdfNormalResult(d1, d2, d3, d4)
end
end sdf_normal_macro
";
        #endregion


        //        #region General
//        internal const string GeneralTemplates =
//@"
//delimiters #
//
//begin main_case1
//case #caseid#:
//    return #name##num#(Scalars);
//end main_case1
//
//begin main_case2
//case #caseid#:
//    return #name##num#(Scalars, #arg2#);
//end main_case2
//
//begin main_case3
//case #caseid#:
//    return new #signature#kVector(#grade#, #name##num#(Scalars));
//end main_case3
//
//begin main_case4
//case #caseid#:
//    return new #signature#kVector(#grade#, #name##num#(Scalars, #arg2#));
//end main_case4
//";
//        #endregion

        #region Frame Utils

        internal const string FrameUtilsTemplates =
@"
delimiters #

begin frame_utils
namespace #signature#
{
    public static partial class #signature#Utils
    {
        private static int[][] ChooseList { get; } 
            =
            {
                new[] {1},
                new[] {1, 1},
                new[] {1, 2, 1},
                new[] {1, 3, 3, 1},
                new[] {1, 4, 6, 4, 1},
                new[] {1, 5, 10, 10, 5, 1},
                new[] {1, 6, 15, 20, 15, 6, 1},
                new[] {1, 7, 21, 35, 35, 21, 7, 1},
                new[] {1, 8, 28, 56, 70, 56, 28, 8, 1},
                new[] {1, 9, 36, 84, 126, 126, 84, 36, 9, 1},
                new[] {1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1},
                new[] {1, 11, 55, 165, 330, 462, 462, 330, 165, 55, 11, 1},
                new[] {1, 12, 66, 220, 495, 792, 924, 792, 495, 220, 66, 12, 1},
                new[] {1, 13, 78, 286, 715, 1287, 1716, 1716, 1287, 715, 286, 78, 13, 1},
                new[] {1, 14, 91, 364, 1001, 2002, 3003, 3432, 3003, 2002, 1001, 364, 91, 14, 1},
                new[] {1, 15, 105, 455, 1365, 3003, 5005, 6435, 6435, 5005, 3003, 1365, 455, 105, 15, 1},
                new[] {1, 16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1},
                new[] {1, 17, 136, 680, 2380, 6188, 12376, 19448, 24310, 24310, 19448, 12376, 6188, 2380, 680, 136, 17, 1},
                new[] {1, 18, 153, 816, 3060, 8568, 18564, 31824, 43758, 48620, 43758, 31824, 18564, 8568, 3060, 816, 153, 18, 1},
                new[] {1, 19, 171, 969, 3876, 11628, 27132, 50388, 75582, 92378, 92378, 75582, 50388, 27132, 11628, 3876, 969, 171, 19, 1},
                new[] {1, 20, 190, 1140, 4845, 15504, 38760, 77520, 125970, 167960, 184756, 167960, 125970, 77520, 38760, 15504, 4845, 1140, 190, 20, 1},
                new[] {1, 21, 210, 1330, 5985, 20349, 54264, 116280, 203490, 293930, 352716, 352716, 293930, 203490, 116280, 54264, 20349, 5985, 1330, 210, 21, 1},
                new[] {1, 22, 231, 1540, 7315, 26334, 74613, 170544, 319770, 497420, 646646, 705432, 646646, 497420, 319770, 170544, 74613, 26334, 7315, 1540, 231, 22, 1},
                new[] {1, 23, 253, 1771, 8855, 33649, 100947, 245157, 490314, 817190, 1144066, 1352078, 1352078, 1144066, 817190, 490314, 245157, 100947, 33649, 8855, 1771, 253, 23, 1},
                new[] {1, 24, 276, 2024, 10626, 42504, 134596, 346104, 735471, 1307504, 1961256, 2496144, 2704156, 2496144, 1961256, 1307504, 735471, 346104, 134596, 42504, 10626, 2024, 276, 24, 1},
                new[] {1, 25, 300, 2300, 12650, 53130, 177100, 480700, 1081575, 2042975, 3268760, 4457400, 5200300, 5200300, 4457400, 3268760, 2042975, 1081575, 480700, 177100, 53130, 12650, 2300, 300, 25, 1}
            };

        public static double ScalarEpsilon { get; set; } = 1e-12;

        public static int VectorSpaceDimensions { get; }

        public static int GaSpaceDimensions { get; } 

        public static int MaxGradesPerMultivector { get; }

        internal static int[][] IdLookupTable { get; }

        internal static int[] GradeLookupTable { get; }
        
        internal static int[] IndexLookupTable { get; }
        
        internal static int[] KVectorSizesLookupTable { get; }


        static #signature#Utils()
        {
            VectorSpaceDimensions = #vspacedim#;
            MaxGradesPerMultivector = 1 + VectorSpaceDimensions;
            GaSpaceDimensions = 1 << VectorSpaceDimensions;

            IdLookupTable = new int[MaxGradesPerMultivector][];
            GradeLookupTable = new int[GaSpaceDimensions];
            IndexLookupTable = new int[GaSpaceDimensions];
            KVectorSizesLookupTable = new int[MaxGradesPerMultivector];

            var gradeCount = new int[MaxGradesPerMultivector];
            for (var id = 0; id < GaSpaceDimensions; id++)
            {
                var grade = id.CountOnes();
                var index = gradeCount[grade];

                GradeLookupTable[id] = grade;
                IndexLookupTable[id] = index;

                gradeCount[grade] = index + 1;
            }

            gradeCount = new int[MaxGradesPerMultivector];
            for (var id = 0; id < GaSpaceDimensions; id++)
            {
                var grade = GradeLookupTable[id];
                var index = gradeCount[grade];

                if (index == 0)
                {
                    var kVectorSize = Choose(VectorSpaceDimensions, grade);
                    
                    IdLookupTable[grade] = new int[kVectorSize];
                    KVectorSizesLookupTable[grade] = kVectorSize;
                }

                IdLookupTable[grade][index] = id;

                gradeCount[grade] = index + 1;
            }
        }


        public static int Choose(this int n, int r)
        {
            return ChooseList[n][r];
        }

        /// <summary>
        /// Count the number of ones in the given bit pattern
        /// </summary>
        /// <param name=""bitPattern""></param>
        /// <returns></returns>
        public static int CountOnes(this int bitPattern)
        {
            var onesCount = 0;

            while (bitPattern > 0)
            {
                // clear the least significant bit set
                bitPattern &= bitPattern - 1;

                onesCount++;
            }

            return onesCount;
        }

        public static bool IsNearZero(this double scalar)
        {
            return scalar >= -ScalarEpsilon && scalar <= ScalarEpsilon;
        }
    }
}
end frame_utils
";
        #endregion

        #region KVector
        internal const string KVectorTemplates =
@"
delimiters #

begin kvector_file_start
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace #signature#
{
    /// <summary>
    /// This class represents a k-vector in the #signature# signature with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class #signature#kVector
        : I#signature#Multivector
    {
end kvector_file_start

begin kvector
/// <summary>
/// Ordered coefficients of k-vector in the additive representation. 
/// </summary>
internal #double#[] Scalars { get; }

public int StoredTermsCount
	=> Scalars.Length;

/// <summary>
/// Grade of blade.
/// </summary>
public int Grade { get; }

/// <summary>
/// Get or set the scalar coefficient of a basis blade of a given
/// grade and index. Setting the scalar of a grade != Grade raises
/// an exception.
/// </summary>
/// <param name=""grade""></param>
/// <param name=""index""></param>
/// <returns></returns>
public #double# this[int grade, int index]
{
	get
	{
		if (grade != Grade) 
			return 0;

		return Scalars[index];
	}
	set
	{
		if (grade != Grade)
			throw new IndexOutOfRangeException();

		Scalars[index] = value;
	}
}

public #double# KVectorsCount 
	=> 1;

public IEnumerable<int> KVectorGrades
{
	get { yield return Grade; }
}

public IEnumerable<#signature#kVector> KVectors
{
	get { yield return this; }
}

/// <summary>
/// Get or set the scalar coefficient of a basis blade of a given ID
/// Setting the scalar of a grade != Grade raises an exception.
/// </summary>
/// <param name=""id""></param>
/// <returns></returns>
public #double# this[int id]
{
	get
	{
		var grade = #signature#Utils.GradeLookupTable[id];

		if (grade != Grade) 
			return 0;

		var index = #signature#Utils.IndexLookupTable[id];

		return Scalars[index];
	}
	set
	{
		var grade = #signature#Utils.GradeLookupTable[id];

		if (grade != Grade)
			throw new IndexOutOfRangeException();

		var index = #signature#Utils.IndexLookupTable[id];

		Scalars[index] = value;
	}
}

/// <summary>
/// This k-vector is a zero k-vector: it has no internal coefficients and its grade is any legal grade
/// This kind of k-vector should be treated separately in operations on k-vectors
/// </summary>
public bool IsZero 
	=> Scalars.Length == 0;

/// <summary>
/// True if this k-vector is a null k-vector
/// </summary>
public bool IsNull
    => IsZero || Norm2.IsNearZero();

public bool IsScalar 
	=> IsZero || Grade == 0;

public bool IsVector 
	=> IsZero || Grade == 1;

public bool IsPseudoVector 
	=> IsZero || Grade == #signature#Utils.VectorSpaceDimensions - 1;

public bool IsPseudoScalar 
	=> IsZero || Grade == #signature#Utils.VectorSpaceDimensions;

public string[] BasisBladesNames 
	=> BasisBladesNamesArray[Grade];

/// <summary>
/// True if the coefficients represent a blade; not a general non-simple k-vector.
/// </summary>
public bool IsBlade 
	=> SelfDPGrade() == 0;

/// <summary>
/// True if the coefficients represent a general non-simple k-vector; not a blade.
/// </summary>
public bool IsNonBlade 
	=> SelfDPGrade() != 0;

/// <summary>
/// Create a k-vector and initialize its coefficients to zero.
/// </summary>
internal #signature#kVector(int grade)
{
	Grade = grade;
	Scalars = new #double#[#signature#Utils.KVectorSizesLookupTable[grade]];
}

/// <summary>
/// Create a k-vector and initialize its coefficients by the given array. 
/// </summary>
internal #signature#kVector(int grade, #double#[] scalars)
{
	if (scalars.Length != #signature#Utils.KVectorSizesLookupTable[grade])
		throw new ArgumentException(@""The given array has the wrong number of items for this grade"", nameof(scalars));

	Grade = grade;
	Scalars = scalars;
}

/// <summary>
/// Create a term
/// </summary>
/// <param name=""grade""></param>
/// <param name=""index""></param>
/// <param name=""scalar""></param>
public #signature#kVector(int grade, int index, #double# scalar)
{
	Grade = grade;
	Scalars = new #double#[#signature#Utils.KVectorSizesLookupTable[grade]];
	Scalars[index] = scalar;
}

/// <summary>
/// Create a term
/// </summary>
/// <param name=""id""></param>
/// <param name=""scalar""></param>
public #signature#kVector(int id, #double# scalar)
{
	Grade = #signature#Utils.GradeLookupTable[id];
	Scalars = new #double#[#signature#Utils.KVectorSizesLookupTable[Grade]];

	var index = #signature#Utils.IndexLookupTable[id];
	Scalars[index] = scalar;
}

/// <summary>
/// Create a scalar blade (a 0-blade)
/// </summary>
public #signature#kVector(#double# scalar)
{
	Grade = 0;
	Scalars = new [] { scalar };
}

/// <summary>
/// Create a zero k-vector
/// </summary>
internal #signature#kVector()
{
	Grade = 0;
	Scalars = new #double#[0];
}


public IEnumerable<Tuple<int, #double#>> GetStoredTermsById()
{
	var idTable = #signature#Utils.IdLookupTable[Grade];
	for (var index = 0; index <= StoredTermsCount; index++)
	{
		var scalar = Scalars[index];
		var id = idTable[index];

		yield return new Tuple<int, #double#>(id, scalar);
	}
}

public IEnumerable<Tuple<int, int, #double#>> GetStoredTermsByGradeIndex()
{
	for (var index = 0; index <= StoredTermsCount; index++)
	{
		var scalar = Scalars[index];

		yield return new Tuple<int, int, #double#>(Grade, index, scalar);
	}
}

public IEnumerable<Tuple<int, #double#>> GetNonZeroTermsById()
{
	var idTable = #signature#Utils.IdLookupTable[Grade];
	for (var index = 0; index <= StoredTermsCount; index++)
	{
		var scalar = Scalars[index];
		if (scalar.IsNearZero())
			continue;

		var id = idTable[index];
		yield return new Tuple<int, #double#>(id, scalar);
	}
}

public IEnumerable<Tuple<int, int, #double#>> GetNonZeroTermsByGradeIndex()
{
	for (var index = 0; index <= StoredTermsCount; index++)
	{
		var scalar = Scalars[index];
		if (scalar.IsNearZero())
			continue;

		yield return new Tuple<int, int, #double#>(Grade, index, scalar);
	}
}

public #signature#kVector GetKVector(int grade)
{
	if (grade == Grade)
		return this;

	return new #signature#kVector(grade);
}

/// <summary>
/// Test if this k-vector is of a given grade. A zero k-vector is assumed to have any grade
/// </summary>
public bool IsOfGrade(int grade)
{
	return Grade == grade || (grade >= 0 && grade <= #signature#Utils.VectorSpaceDimensions && IsZero);
}

/// <summary>
/// If this blade is of grade 1 convert it to a vector
/// </summary>
/// <returns></returns>
public #signature#Vector ToVector()
{
	if (Grade == 1)
		return new #signature#Vector(Scalars);

	if (IsZero)
		return new #signature#Vector();

	throw new InvalidDataException(""Internal error. Grade not acceptable!"");
}


public #signature#kVector Meet(#signature#kVector bladeB)
{
	//blade A1 is the part of A not in B
	var bladeA1 = DPDual(bladeB).DP(this);

	return bladeA1.ELCP(bladeB);
}

public #signature#kVector Join(#signature#kVector bladeB)
{
	//blade A1 is the part of A not in B
	var bladeA1 = DPDual(bladeB).DP(this);

	return bladeA1.OP(bladeB);
}

public void MeetJoin(#signature#kVector bladeB, out #signature#kVector bladeMeet, out #signature#kVector bladeJoin)
{
	//blade A1 is the part of A not in B
	var bladeA1 = DPDual(bladeB).DP(this);

	bladeMeet = bladeA1.ELCP(bladeB);
	bladeJoin = bladeA1.OP(bladeB);
}

public void Meet(#signature#kVector bladeB, out #signature#kVector bladeA1, out #signature#kVector bladeB1, out #signature#kVector bladeMeet)
{
	//blade A1 is the part of A not in B
	bladeA1 = DPDual(bladeB).DP(this);

	bladeMeet = bladeA1.ELCP(bladeB);
	bladeB1 = bladeMeet.ELCP(bladeB);
}

public void MeetJoin(#signature#kVector bladeB, out #signature#kVector bladeA1, out #signature#kVector bladeB1, out #signature#kVector bladeMeet, out #signature#kVector bladeJoin)
{
	//blade A1 is the part of A not in B
	bladeA1 = DPDual(bladeB).DP(this);

	bladeMeet = bladeA1.ELCP(bladeB);
	bladeJoin = bladeA1.OP(bladeB);
	bladeB1 = bladeMeet.ELCP(bladeB);
}


public override bool Equals(object obj)
{
	return !ReferenceEquals(obj, null) && Equals(obj as #signature#kVector);
}

public override int GetHashCode()
{
	return Grade.GetHashCode() ^ Scalars.GetHashCode();
}

public override string ToString()
{
	if (IsZero)
		return default(#double#).ToString(CultureInfo.InvariantCulture);

	if (IsScalar)
		return Scalars[0].ToString(CultureInfo.InvariantCulture);

	var s = new StringBuilder();

	for (var i = 0; i < StoredTermsCount; i++)
	{
		s.Append(""("")
			.Append(Scalars[i].ToString(CultureInfo.InvariantCulture))
			.Append("" "")
			.Append(BasisBladesNames[i])
			.Append("") + "");
	}

	s.Length -= 3;

	return s.ToString();
}
end kvector
";
        #endregion

        #region KVector Static
        internal const string KVectorStaticTemplates =
            @"
delimiters #

begin static_basisblade_name
new [] { #names# }
end static_basisblade_name

begin static_basisblade_declare
public static #signature#kVector E#id# { get; } = new #signature#kVector(#grade#, new[] { #scalars# });
end static_basisblade_declare

begin static
/// <summary>
/// An array of arrays containing basis blades names for this signature grouped by grade
/// </summary>
private static string[][] BasisBladesNamesArray { get; } 
    = 
    {
        #basisnames#
    };

#basisblades#

end static
";
        #endregion

        #region Multivector
        internal const string MultivectorTemplates =
@"
delimiters #

begin multivector
using System;
using System.Collections.Generic;
using System.Linq;

namespace #signature#
{
    /// <summary>
    /// This interface represents a multivector in the #signature# signature.
    /// </summary>
    public interface I#signature#Multivector
    {
        /// <summary>
        /// The number of current terms stored in this multivectors
        /// </summary>
        int StoredTermsCount { get; }

        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given ID
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        #double# this[int id] { get; }

        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given
        /// grade and index
        /// </summary>
        /// <param name=""grade""></param>
        /// <param name=""index""></param>
        /// <returns></returns>
        #double# this[int grade, int index] { get; }

        /// <summary>
        /// Get the number of k-vectors stored in this multivector 
        /// </summary>
        #double# KVectorsCount { get; }

        /// <summary>
        /// Get the grades of the k-vectors stored in this multivector 
        /// </summary>
        IEnumerable<int> KVectorGrades { get; }

        /// <summary>
        /// Get the k-vectors stored in this multivector 
        /// </summary>
        IEnumerable<#signature#kVector> KVectors { get; }

        /// <summary>
        /// Get all (id, scalar) terms stored in this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, #double#>> GetStoredTermsById();

        /// <summary>
        /// Get all (grade, index, scalar) terms stored in this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, int, #double#>> GetStoredTermsByGradeIndex();

        /// <summary>
        /// Get all (id, scalar) terms with non zero scalar value
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, #double#>> GetNonZeroTermsById();

        /// <summary>
        /// Get all (grade, index, scalar) terms with non zero scalar value
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, int, #double#>> GetNonZeroTermsByGradeIndex();

        /// <summary>
        /// Get a k-vector of a given grade
        /// </summary>
        /// <param name=""grade""></param>
        /// <returns></returns>
        #signature#kVector GetKVector(int grade);
    }

    /// <summary>
    /// This class represents a multivector in the #signature# signature. A multivector
    /// contains an array of n+1 k-vectors of grades 0 to n. If a k-vector is not
    /// needed, a null is stored in the internal KVectorsArray instead to save
    /// memory.
    /// </summary>
    public sealed partial class #signature#Multivector 
        : I#signature#Multivector
    {
        public static #signature#kVector Zero { get; }
            = new #signature#kVector();

        #static_multivector_members#


        /// <summary>
        /// The internal array holding k-vectors of this multivector
        /// </summary>
        internal #signature#kVector[] KVectorsArray { get; }
	        = new #signature#kVector[6];

        public int StoredTermsCount 
            => KVectors.Sum(v => v.StoredTermsCount);

        public #double# this[int grade, int index]
        {
	        get
	        {
		        var kVector = KVectorsArray[grade];

		        if (ReferenceEquals(kVector, null)) 
			        return 0;

		        return kVector.Scalars[index];
	        }
	        set
	        {
		        var kVector = KVectorsArray[grade];

		        if (ReferenceEquals(kVector, null))
		        {
			        if (value == 0) 
				        return;

			        kVector = new #signature#kVector(grade);

			        KVectorsArray[grade] = kVector;
		        }

		        kVector.Scalars[index] = value;
	        }
        }

        public #double# this[int id]
        {
	        get
	        {
		        var grade = #signature#Utils.GradeLookupTable[id];
		        var index = #signature#Utils.IndexLookupTable[id];

		        return this[grade, index];
	        }
	        set
	        {
		        var grade = #signature#Utils.GradeLookupTable[id];
		        var index = #signature#Utils.IndexLookupTable[id];

		        this[grade, index] = value;
	        }
        }

        public #double# KVectorsCount 
	        => KVectorsArray
		        .Count(v => !ReferenceEquals(v, null));

        public IEnumerable<int> KVectorGrades
	        => KVectorsArray
		        .Where(v => !ReferenceEquals(v, null))
		        .Select(v => v.Grade);

        public IEnumerable<#signature#kVector> KVectors
	        => KVectorsArray
		        .Where(v => !ReferenceEquals(v, null));


        /// <summary>
        /// Create a zero multivector
        /// </summary>
        public #signature#Multivector()
        {
        }

        /// <summary>
        /// Create a scalar multivector
        /// </summary>
        /// <param name=""scalar""></param>
        public #signature#Multivector(#double# scalar)
        {
	        KVectorsArray[0] = new #signature#kVector(scalar);
        }

        /// <summary>
        /// Create a multivector containing a single k-vector
        /// </summary>
        /// <param name=""kVector""></param>
        public #signature#Multivector(#signature#kVector kVector)
        {
	        KVectorsArray[kVector.Grade] = kVector;
        }

        /// <summary>
        /// Create a multivector and fill its terms using a list of
        /// (grade, index, scalar) tuples
        /// </summary>
        /// <param name=""terms""></param>
        public #signature#Multivector(IEnumerable<Tuple<int, int, #double#>> terms)
        {
            foreach (var (grade, index, scalar) in terms)
                this[grade, index] = scalar;
        }

        /// <summary>
        /// Create a multivector and fill its terms using a list of
        /// (id, scalar) tuples
        /// </summary>
        /// <param name=""terms""></param>
        public #signature#Multivector(IEnumerable<Tuple<int, #double#>> terms)
        {
            foreach (var (id, scalar) in terms)
                this[id] = scalar;
        }


        public IEnumerable<Tuple<int, #double#>> GetStoredTermsById()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                var idTable = #signature#Utils.IdLookupTable[grade];
                for (var index = 0; index < kVector.StoredTermsCount; index++)
                    yield return new Tuple<int, #double#>(
                        idTable[index], kVector[index]
                    );
            }
        }

        public IEnumerable<Tuple<int, int, #double#>> GetStoredTermsByGradeIndex()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                for (var index = 0; index < kVector.StoredTermsCount; index++)
                    yield return new Tuple<int, int, #double#>(
                        grade, index, kVector[index]
                    );
            }
        }

        public IEnumerable<Tuple<int, #double#>> GetNonZeroTermsById()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                var idTable = #signature#Utils.IdLookupTable[grade];
                for (var index = 0; index < kVector.StoredTermsCount; index++)
                {
                    var scalar = kVector[index];
                    if (scalar.IsNearZero())
                        continue;

                    yield return new Tuple<int, #double#>(
                        idTable[index], scalar
                    );
                }
            }
        }

        public IEnumerable<Tuple<int, int, #double#>> GetNonZeroTermsByGradeIndex()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                for (var index = 0; index < kVector.StoredTermsCount; index++)
                {
                    var scalar = kVector[index];
                    if (scalar.IsNearZero())
                        continue;

                    yield return new Tuple<int, int, #double#>(
                        grade, index, scalar
                    );
                }
            }
        }

        public #signature#kVector GetKVector(int grade)
        {
	        return KVectorsArray[grade] 
		           ?? new #signature#kVector(grade);
        }

        /// <summary>
        /// Set a k-vector of a given grade
        /// </summary>
        /// <param name=""grade""></param>
        /// <param name=""kVector""></param>
        /// <returns></returns>
        public #signature#Multivector SetKVector(int grade, #signature#kVector kVector)
        {
	        KVectorsArray[grade] = kVector;
	        return this;
        }

        /// <summary>
        /// Remove a k-vector of a given grade
        /// </summary>
        /// <param name=""grade""></param>
        /// <returns></returns>
        public #signature#Multivector RemoveKVector(int grade)
        {
	        KVectorsArray[grade] = null;
	        return this;
        }

        /// <summary>
        /// Set a k-vector of a given grade
        /// </summary>
        /// <param name=""grade""></param>
        /// <param name=""scalars""></param>
        /// <returns></returns>
        internal #signature#kVector SetKVector(int grade, #double#[] scalars)
        {
	        var kVector = new #signature#kVector(grade, scalars);

	        KVectorsArray[grade] = kVector;

	        return kVector;
        }
    }
}
end multivector
";
        #endregion

        #region Vector
        internal const string VectorTemplates =
@"
delimiters #

begin vector
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace #signature#
{
    /// <summary>
    /// This class represents a mutable vector in the #signature# signature
    /// </summary>
    public sealed class #signature#Vector
    {
        public static #signature#Vector[] BasisVectors()
        {
            return new[]
            {
                #basis_vectors#
            };
        }


        #members_declare#

        public #double# NormSquared
        {
            get { return #norm2#; }
        }

        public #double# EuclideanNormSquared
        {
            get { return #enorm2#; }
        }

        public #double# EuclideanNorm
        {
            get { return Math.Sqrt(#enorm2#); }
        }


        public #signature#Vector()
        {
        }

        public #signature#Vector(#init_inputs#)
        {
            #init_assign#
        }

        public #signature#Vector(#double#[] c)
        {
            #init_assign_array#
        }

        /// <summary>
        /// Convert this to a unit-vector in the Euclidean space
        /// </summary>
        /// <returns></returns>
        public #double# Normalize()
        {
            var scalar = EuclideanNorm;
            var invScalar = 1.0D / scalar;

            #normalize#

            return scalar;
        }

        public #double#[] ToArray()
        {
            return new[] { #members_list# };
        }

        public #signature#kVector ToBlade()
        {
            return new #signature#kVector(1, new[] { #members_list# });
        }
    }
}

end vector

begin factored_blade
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace #signature#
{
    /// <summary>
    /// Represents a factored blade in Euclidean space
    /// </summary>
    public sealed class #signature#FactoredBlade
    {
        public #double# Norm { get; private set; }

        public #signature#Vector[] Vectors { get; private set; }


        public int Grade { get { return Vectors.Length; } }


        internal #signature#FactoredBlade(#double# norm)
        {
            Norm = norm;
            Vectors = new #signature#Vector[0];
        }

        internal #signature#FactoredBlade(#double# norm, #signature#Vector vector)
        {
            Norm = norm;
            Vectors = new [] { vector };
        }

        internal #signature#FactoredBlade(#double# norm, #signature#Vector[] vectors)
        {
            Norm = norm;
            Vectors = vectors;
        }

        /// <summary>
        /// Convert each vector to a normal vector (assuming Euclidean space) and factor 
        /// the squared norms to the NormSquared member
        /// </summary>
        /// <returns></returns>
        public #signature#FactoredBlade Normalize()
        {
            for (var idx = 0; idx < Vectors.Length; idx++)
                Norm *= Vectors[idx].Normalize();

            return this;
        }

        public #signature#kVector ToBlade()
        {
            return #signature#kVector.OP(Vectors).Times(Norm);
        }
    }
}

end factored_blade
";
        #endregion

        #region Outermorphism
        internal const string OutermorphismTemplates = @"
delimiters #

begin om_file_start
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace #signature#
{
    /// <summary>
    /// This class represents a mutable outermorphism in the #signature# signature by only storing a #grade# by #grade#
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class #signature#Outermorphism
    {
end om_file_start

begin om_apply
public static #double#[] Map_#grade#(#double#[,] omScalars, #double#[] kVectorScalars)
{
    var mappedKVectorScalars = new #double#[#num#];

    #computations#
    return mappedKVectorScalars;
}


end om_apply

begin om_apply_code_case
case #grade#:
    return new #signature#kVector(#grade#, Map_#grade#(Scalars, blade.Scalars));
end om_apply_code_case

begin outermorphism
public #double#[,] Scalars { get; private set; }


public #signature#Outermorphism()
{
    Scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];
}

private #signature#Outermorphism(#double#[,] scalars)
{
    Scalars = scalars;
}


public #signature#Outermorphism Transpose()
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions,
        #signature#Utils.VectorSpaceDimensions
    ];

    #transpose_code#

    return new #signature#Outermorphism(scalars);
}

public #double# MetricDeterminant()
{
    #double# det = 0;

    #metric_det_code#

    return det;
}

public #signature#kVector Map(#signature#kVector blade)
{
    if (blade.IsZero)
        return #signature#Multivector.Zero;

    switch (blade.Grade)
    {
        case 0:
            return blade;
        #apply_cases_code#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}


public static #signature#Outermorphism operator +(#signature#Outermorphism om1, #signature#Outermorphism om2)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #plus_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator -(#signature#Outermorphism om1, #signature#Outermorphism om2)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #subt_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator *(#signature#Outermorphism om1, #signature#Outermorphism om2)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #compose_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator *(#double# scalar, #signature#Outermorphism om)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #times_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator *(#signature#Outermorphism om, #double# scalar)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #times_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator /(#signature#Outermorphism om, #double# scalar)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #divide_code#

    return new #signature#Outermorphism(scalars);
}

public static #signature#Outermorphism operator -(#signature#Outermorphism om)
{
    var scalars = new #double#[
        #signature#Utils.VectorSpaceDimensions, 
        #signature#Utils.VectorSpaceDimensions
    ];

    #negative_code#

    return new #signature#Outermorphism(scalars);
}

end outermorphism
";
        #endregion

        #region KVector Equals
        internal const string KVectorEqualsTemplates = @"
delimiters #

begin equals_case
c = scalars1[#num#] - scalars2[#num#];
if (!c.IsNearZero()) 
    return false;

end equals_case

begin equals
private static bool Equals#num#(#double#[] scalars1, #double#[] scalars2)
{
    #double# c;

    #cases#
    return true;
}


end equals

begin main_equals_case
case #grade#:
    return Equals#num#(Scalars, blade2.Scalars);
end main_equals_case

begin main_equals
public bool Equals(#signature#kVector blade2)
{
    if ((object)blade2 == null) 
        return false;

    if (ReferenceEquals(this, blade2)) 
        return true;

    if (IsZero) 
        return blade2.IsZero;

    if (blade2.IsZero) 
        return IsZero;

    if (Grade != blade2.Grade) 
        return false;

    switch (Grade)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end main_equals
";
        #endregion

        #region KVector IsZero
        internal const string KVectorIsZeroTemplates = @"
delimiters #

begin iszero_case
!scalars[#num#].IsNearZero()
end iszero_case

begin trimscalars_case
scalars[#num#].IsNearZero() ? 0.0D : scalars[#num#]
end trimscalars_case

begin iszero
private static bool IsNearZero#num#(#double#[] scalars)
{
    return !(
        #iszero_case#
        );
}

private static #double#[] TrimNearZero#num#(#double#[] scalars)
{
    return new[]
    {
        #trimscalars_case#
    };
}


end iszero

begin main_iszero_case
case #grade#:
    return IsNearZero#num#(Scalars);
end main_iszero_case

begin main_trimscalars_case
case #grade#:
    return new #signature#kVector(#grade#, TrimNearZero#num#(Scalars));
end main_trimscalars_case

begin main_iszero
public bool IsNearZero
{
    get
    {
        if (IsZero)
            return true;

        switch (Grade)
        {
            #main_iszero_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}

/// <summary>
/// Set all near-zero coefficients to zero. If all coefficients are near zero a Zero Multivector is returned
/// </summary>
public #signature#kVector TrimNearZero
{
    get
    {
        if (IsZero)
            return #signature#Multivector.Zero;

        switch (Grade)
        {
            #main_trimscalars_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}

end main_iszero";
        #endregion

        #region KVector Involutions
        internal const string KVectorInvolutionsTemplates = @"
delimiters #

begin negative_case
-scalars[#num#]
end negative_case

begin negative
private static #double#[] Negative#num#(#double#[] scalars)
{
    return new[]
    {
        #cases#
    };
}


end negative

begin main_negative_case
case #grade#:
    return new #signature#kVector(#grade#, Negative#num#(Scalars));

end main_negative_case

begin main_negative_case2
case #grade#:
    return this;

end main_negative_case2

begin main_involution
public #signature#kVector #name#
{
    get
    {
        if (IsZero)
            return this;

        switch (Grade)
        {
            #cases#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}


end main_involution
";
        #endregion

        #region KVector Norm
        internal const string KVectorNormTemplates =
@"
delimiters #

begin norm
private static #double# #name#_#grade#(#double#[] scalars)
{
    var result = 0.0D;

    #computations#
    return result;
}


end norm

begin main_norm_case
case #grade#:
    return #name#_#grade#(Scalars);
end main_norm_case

begin main_norm
public #double# #name#
{
    get
    {
        if (IsZero)
            return 0.0D;

        switch (Grade)
        {
            #main_norm_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}


end main_norm
";
        #endregion

        #region KVector Misc

        internal const string KVectorSdfTemplates = 
@"
delimiters #

";
        #endregion

        #region KVector Misc
        internal const string KVectorMiscTemplates =
@"
delimiters #

begin add_case
scalars1[#index#] + scalars2[#index#]
end add_case

begin subt_case
scalars1[#index#] - scalars2[#index#]
end subt_case

begin times_case
scalar * scalars[#index#]
end times_case

begin misc
private static #double#[] Add#num#(#double#[] scalars1, #double#[] scalars2)
{
    return new[]
    {
        #addcases#
    };
}

private static #double#[] Subtract#num#(#double#[] scalars1, #double#[] scalars2)
{
    return new[]
    {
        #subtcases#
    };
}

private static #double#[] Times#num#(#double#[] scalars, #double# scalar)
{
    return new[]
    {
        #timescases#
    };
}


end misc

begin edual
private static #double#[] EuclideanDual#grade#(#double#[] scalars)
{
    var c = new #double#[#num#];

    #computations#
    return c;
}


end edual

//begin self_dp_grade_case
// Input Grade: #ingrade#
//Output Grade: #outgrade#
//#computations#
//end self_dp_grade_case

begin self_dp_grade
private static int SelfDPGrade#grade#(#double#[] scalars)
{
    #double# c = 0.0D;

    #computations#
    return 0;
}


end self_dp_grade


begin main_add_case
case #grade#:
    return new #signature#kVector(#grade#, Add#num#(Scalars, blade2.Scalars));
end main_add_case

begin main_subt_case
case #grade#:
    return new #signature#kVector(#grade#, Subtract#num#(Scalars, blade2.Scalars));
end main_subt_case

begin main_times_case
case #grade#:
    return new #signature#kVector(#grade#, Times#num#(Scalars, scalar));
end main_times_case

begin main_divide_case
case #grade#:
    return new #signature#kVector(#grade#, Times#num#(Scalars, 1.0D / scalar));
end main_divide_case

begin main_inverse_case
case #grade#:
    return new #signature#kVector(#grade#, Times#num#(Scalars, #sign#1.0D / scalar));
end main_inverse_case

begin main_edual_case
case #grade#:
    return new #signature#kVector(#invgrade#, EuclideanDual#grade#(Scalars));
end main_edual_case

begin main_self_dp_grade_case
case #grade#:
    return SelfDPGrade#grade#(Scalars);
end main_self_dp_grade_case


begin main_self_dp_grade
/// <summary>
/// The grade of the delta product of this k-vector with itself. If the grade is 0 this 
/// k-vector is a blade
/// </summary>
/// <returns></returns>
public int SelfDPGrade()
{
    if (Grade <= 1 || Grade >= #signature#Utils.VectorSpaceDimensions - 1 || IsZero)
        return 0;

    switch (Grade)
    {
        #main_self_dp_grade_cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end main_self_dp_grade

begin misc_main
public #signature#kVector Add(#signature#kVector blade2)
{
    if (blade2.IsZero)
        return this;

    if (IsZero)
        return blade2;

    if (Grade != blade2.Grade)
        throw new InvalidOperationException(""Can't add two non-zero blades of different grades"");

    switch (Grade)
    {
        #main_add_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

public #signature#kVector Subtract(#signature#kVector blade2)
{
    if (blade2.IsZero)
        return this;

    if (IsZero)
        return blade2.Negative;

    if (Grade != blade2.Grade)
        throw new InvalidOperationException(""Can't subtract two non-zero blades of different grades"");

    switch (Grade)
    {
        #main_subt_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

public #signature#kVector Times(#double# scalar)
{
    switch (Grade)
    {
        #main_times_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

public #signature#kVector Divide(#double# scalar)
{
    switch (Grade)
    {
        #main_divide_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

public #signature#kVector Inverse
{
    get
    {
        var scalar = #norm2_opname#;

        if (scalar.IsNearZero())
            throw new InvalidOperationException(""Null blade has no inverse"");

        switch (Grade)
        {
            #main_inverse_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}

public #signature#kVector EuclideanInverse
{
    get
    {
        var scalar = #emag2_opname#;

        if (scalar.IsNearZero())
            throw new InvalidOperationException(""Null blade has no inverse"");

        switch (Grade)
        {
            #main_inverse_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}

public #signature#kVector EuclideanDual
{
    get
    {
        if (IsZero)
            return #signature#Multivector.Zero;

        switch (Grade)
        {
            #main_edual_case#
        }

        throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
    }
}

end misc_main
";
        #endregion

        #region KVector BilinearProducts
        internal const string KVectorBilinearProductsTemplates =
@"
delimiters #

begin bilinearproduct
private static #double#[] #name#(#double#[] scalars1, #double#[] scalars2)
{
    var c = new #double#[#num#];

    #computations#
    return c;
}


end bilinearproduct

begin bilinearproduct_main_case
//grade1: #g1#, grade2: #g2#
case #id#:
    return new #signature#kVector(#grade#, #name#(Scalars, blade2.Scalars));

end bilinearproduct_main_case

begin bilinearproduct_main
public #signature#kVector #name#(#signature#kVector blade2)
{
    if (IsZero || blade2.IsZero || #zerocond#)
        return #signature#Multivector.Zero;

    var id = Grade + blade2.Grade * (#signature#Utils.VectorSpaceDimensions + 1);

    switch (id)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end bilinearproduct_main

begin gp_case
new #signature#kVector(#grade#, #name#(scalars1, scalars2))
end gp_case

begin gp
private static #signature#kVector[] #name#(#double#[] scalars1, #double#[] scalars2)
{
    return new[]
    {
        #gp_case#
    };
}

end gp

begin gp_main_case
//grade1: #g1#, grade2: #g2#
case #id#:
    return #name#(Scalars, blade2.Scalars);
end gp_main_case

begin gp_main
public #signature#kVector[] #name#(#signature#kVector blade2)
{
    if (IsZero || blade2.IsZero)
        return new #signature#kVector[0];

    var id = Grade + blade2.Grade * (#signature#Utils.VectorSpaceDimensions + 1);

    switch (id)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end gp_main

begin self_bilinearproduct
private static #double#[] #name#(#double#[] scalars)
{
    var c = new #double#[#num#];

    #computations#
    return c;
}


end self_bilinearproduct

begin selfgp_case
new #signature#kVector(#grade#, #name#(scalars))
end selfgp_case

begin selfgp
private static #signature#kVector[] #name#(#double#[] scalars)
{
    return new[]
    {
        #selfgp_case#
    };
}

end selfgp

begin selfgp_main_case
//grade: #grade#
case #grade#:
    return #name#(Scalars);
end selfgp_main_case

begin selfgp_main
public #signature#kVector[] #name#()
{
    if (IsZero)
        return new #signature#kVector[0];

    switch (Grade)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end selfgp_main


begin dp_case
scalars = #name#(scalars1, scalars2);
if (IsNearZero#num#(scalars) == false)
    return new #signature#kVector(#grade#, scalars);


end dp_case

begin dp
private static #signature#kVector #name#(#double#[] scalars1, #double#[] scalars2)
{
    //Try all Euclidean geometric products for these two input grades starting from largest to smallest
    //output grade

    #double#[] scalars;

    #dp_case#
    return #signature#Multivector.Zero;
}


end dp

begin dp_main_case
//grade1: #g1#, grade2: #g2#
case #id#:
    return #name#(Scalars, blade2.Scalars);
end dp_main_case

begin dp_main
public #signature#kVector #name#(#signature#kVector blade2)
{
    if (IsZero || blade2.IsZero)
        return #signature#Multivector.Zero;

    var id = Grade + blade2.Grade * (#signature#Utils.VectorSpaceDimensions + 1);

    switch (id)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}
end dp_main

begin applyversor_main_case
//grade1: #g1#, grade2: #g2#
case #id#:
    return new #signature#kVector(#grade#, #name#(Scalars, blade2.Scalars));
end applyversor_main_case

begin applyversor_main
public #signature#kVector #name#(#signature#kVector blade2)
{
    if (blade2.IsZero)
        return #signature#Multivector.Zero;

    var id = Grade + blade2.Grade * (#signature#Utils.VectorSpaceDimensions + 1);

    switch (id)
    {
        #cases#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end applyversor_main

begin op_vectors
private static #signature#kVector OP#grade#(#signature#Vector[] vectors)
{
    var scalars = new #double#[#num#];

    #computations#
    return new #signature#kVector(#grade#, scalars);
}


end op_vectors

begin op_vectors_main_case
case #grade#:
    return OP#grade#(vectors);
end op_vectors_main_case

begin op_vectors_main
public static #signature#kVector OP(#signature#Vector[] vectors)
{
    switch (vectors.Length)
    {
        case 0:
            return #signature#Multivector.Zero;
        case 1:
            return vectors[0].ToBlade();
        #op_vectors_main_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}


end op_vectors_main
";
        #endregion

        #region KVector Factorization

        internal const string KVectorFactorizationTemplates =
@"
delimiters #

begin factor
private static #signature#Vector[] Factor#id#(#double#[] scalars)
{
    var vectors = new[] 
    {
        #newvectors#
    };

    #computations#

    return vectors;
}


end factor

begin maxcoefid_case
c = Math.Abs(scalars[#index#]);
if (c > maxCoef)
{
    maxCoef = c;
    maxCoefId = #id#;
}

end maxcoefid_case

begin maxcoefid
private static int MaxCoefId#grade#(#double#[] scalars)
{
    var c = Math.Abs(scalars[0]);
    var maxCoef = c;
    var maxCoefId = #initid#;

    #maxcoefid_case#
    c = Math.Abs(scalars[#maxindex#]);
    if (c > maxCoef)
        maxCoefId = #maxid#;

    return maxCoefId;
}


end maxcoefid

begin factorgrade_case
case #id#:
    return Factor#id#(scalars);
end factorgrade_case

begin factorgrade
private static #signature#Vector[] FactorGrade#grade#(#double#[] scalars)
{
    var maxCoefId = MaxCoefId#grade#(scalars);

    switch (maxCoefId)
    {
        #factorgrade_case#
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}


end factorgrade

begin factor_main_case
case #grade#:
    return new #signature#FactoredBlade(#name#_#grade#(Scalars), FactorGrade#grade#(Scalars));

end factor_main_case

begin factor_main
public #signature#FactoredBlade Factor()
{
    if (IsZero)
        return new #signature#FactoredBlade(0.0D);

    switch (Grade)
    {
        case 0:
            return new #signature#FactoredBlade(Scalars[0]);

        case 1:
            var vector = ToVector();
            var norm = vector.Normalize();
            return new #signature#FactoredBlade(norm, vector);

        #factor_main_case#
        case #maxgrade#:
            return new #signature#FactoredBlade(Scalars[0],  #signature#Vector.BasisVectors());
    }

    throw new InvalidDataException(""Internal error. Blade grade not acceptable!"");
}

end factor_main
";
        #endregion
    }
}

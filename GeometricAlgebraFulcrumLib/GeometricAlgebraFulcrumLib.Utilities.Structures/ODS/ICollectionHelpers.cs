namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

internal static class CollectionHelpers
{
    public static void ThrowIfInsufficientArray<T>(ICollection<T> col, T[] arr, int index)
    {
        if (arr == null)
            throw new ArgumentNullException();

        if (index < 0)
            throw new ArgumentOutOfRangeException();

        if (col.Count > arr.Length - index)
            throw new ArgumentException();
    }
}
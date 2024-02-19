using System.Collections.Generic;

namespace DataStructuresLib.ODS;

partial class HashTable<T>
{
    internal class InnerHashTable
    {
        // table-size is table.Length
        internal uint Count;
        internal uint Limit;
        internal KeyValuePair<uint, T>?[] Table;
        // hashing arguments
        internal uint A;
        internal uint B;
        internal int Width;

        internal InnerHashTable(uint length)
        {
            Count = length;
            Limit = 2 * length;
            var hashSize = BitHacks.RoundToPower(2 * Limit * (Limit - 1));
            Width = BitHacks.Power2Msb(hashSize);
            Table = new KeyValuePair<uint, T>?[hashSize];
        }

        internal void Clear()
        {
            Table = new KeyValuePair<uint, T>?[Table.Length];
        }

        internal uint AllocatedSize => (uint)Table.Length;

        internal void RemoveAt(int idx)
        {
            Count--;
            Table[idx] = null;
        }

        internal bool IsDeleted(int idx)
        {
            return Table[idx] == null;
        }

        internal bool IsContained(int idx)
        {
            return Table[idx] != null;
        }

        internal void RehashWith(uint key, T value, HashTable<T> parent, KeyValuePair<uint, T>?[] oldTable, int size)
        {
            var tempList = new KeyValuePair<uint, T>[Count];
            for (int i = 0, j = 0; i < oldTable.Length; i++)
            {
                if (oldTable[i] != null)
                {
                    tempList[j] = oldTable[i].Value;
                    j++;
                }
            }
            tempList[Count-1] = new KeyValuePair<uint, T>(key, value);
            // we've got temp list ready, now try and find suitable function
            while (true)
            {
                InitializeRandomHash(parent);
                var newTable = new KeyValuePair<uint, T>?[size];
                // put them pairs where they belong
                for (var i = 0; i < tempList.Length; i++)
                {
                    var index = GetHash(tempList[i].Key);
                    if (newTable[index] != null)
                        goto Failed;
                    newTable[index] = tempList[i];
                }
                Table = newTable;
                break;
                Failed:
                continue;
            }
        }

        internal uint GetHash(uint x)
        {
#if TEST
                return ((a * x + b) % 997) % (uint)Math.Pow(2, this.width);
#else
            return ((A * x + B) >> (31 - Width)) >> 1;
#endif
        }

        internal void InitializeRandomHash(HashTable<T> hashTable)
        {
            hashTable.InitializeRandomHash(out A, out B);
        }
    }
}
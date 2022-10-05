using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace SharpGLTF.Schema2
{
    public static partial class Toolkit
    {
        public static Accessor CreateVertexAccessor(this ModelRoot root, Memory.MemoryAccessor memAccessor)
        {
            Guard.NotNull(root, nameof(root));
            Guard.NotNull(memAccessor, nameof(memAccessor));

            var accessor = root.CreateAccessor(memAccessor.Attribute.Name);

            accessor.SetVertexData(memAccessor);

            return accessor;
        }

        public static IEnumerable<Byte> ToBytes(this IReadOnlyList<long> datas)
        {
            foreach (var data in datas)
            {
                foreach (var _byte in BitConverter.GetBytes(data))
                {
                    yield return _byte;
                }
            }
        }

        public static unsafe BufferView CreateBufferView<T>(this ModelRoot root, IReadOnlyList<T> data)
            where T : unmanaged
        {
            if (typeof(T) == typeof(long))
            {
                return root.UseBufferView((data as IReadOnlyList<long>).ToBytes().ToArray());
            }

            var view = root.CreateBufferView(sizeof(T) * data.Count);

            if (typeof(T) == typeof(int))
            {
                new Memory.IntegerArray(view.Content, IndexEncodingType.UNSIGNED_INT).Fill(data as IReadOnlyList<int>);
                return view;
            }
            if (typeof(T) == typeof(Single))
            {
                new Memory.ScalarArray(view.Content).Fill(data as IReadOnlyList<Single>);
                return view;
            }

            if (typeof(T) == typeof(Vector2))
            {
                new Memory.Vector2Array(view.Content).Fill(data as IReadOnlyList<Vector2>);
                return view;
            }

            if (typeof(T) == typeof(Vector3))
            {
                new Memory.Vector3Array(view.Content).Fill(data as IReadOnlyList<Vector3>);
                return view;
            }

            if (typeof(T) == typeof(Vector4))
            {
                new Memory.Vector4Array(view.Content).Fill(data as IReadOnlyList<Vector4>);
                return view;
            }

            if (typeof(T) == typeof(Quaternion))
            {
                new Memory.QuaternionArray(view.Content).Fill(data as IReadOnlyList<Quaternion>);
                return view;
            }

            if (typeof(T) == typeof(Matrix4x4))
            {
                new Memory.Matrix4x4Array(view.Content).Fill(data as IReadOnlyList<Matrix4x4>);
                return view;
            }

            throw new ArgumentException(typeof(T).Name);
        }
    }
}

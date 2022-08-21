using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.IO
{
    public class ExtendedBinaryWriter : BinaryWriter
    {
        private static readonly byte[] zeroBytes = new byte[1024];

        public ExtendedBinaryWriter(Stream output) : base(output)
        {
        }

        public int WriteStructArray<T>(T[] arr) where T : struct
        {
            Span<byte> byteSpan = MemoryMarshal.AsBytes<T>(arr);
            Write(byteSpan);
            return byteSpan.Length;
        }

        public int WriteStruct<T>(T val) where T : struct
        {
            int length = Marshal.SizeOf<T>();
            var span = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref val, 1));
            Write(span);
            return length;
        }

        public void WriteFixedLengthString(string value, int length)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            int remaining = length - bytes.Length;
            if (remaining < 0)
                throw new ArgumentOutOfRangeException($"value string {value} is longer than provided length {length}");
            BaseStream.Write(bytes, 0, bytes.Length);
            Pad(remaining);
        }

        public void WriteNullTerminatedString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            BaseStream.Write(bytes);
            Write((byte)0);
        }

        public int WriteObjectArray<T>(T[] data) where T : IBinarySerializable, new()
        {
            long start = BaseStream.Position;
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Save(this);
            }
            long end = BaseStream.Position;
            return (int)(end - start);
        }

        public void AlignStream(int alignBase = 16)
        {
            long pos = BaseStream.Position;
            if (pos % alignBase != 0)
            {
                int fixup = (int)(alignBase - (pos % alignBase));
                BaseStream.Write(zeroBytes, 0, fixup);
            }
        }

        public void Pad(int length)
        {
            while (length > 0)
            {
                Write(zeroBytes, 0, length > 1024 ? 1024 : length);
                length -= 1024;
            }
        }
    }
}

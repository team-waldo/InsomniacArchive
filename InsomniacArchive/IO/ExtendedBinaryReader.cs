using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.IO
{
    public class ExtendedBinaryReader : BinaryReader
    {
        public ExtendedBinaryReader(Stream input) : base(input)
        {

        }

        public string ReadStringToNull()
        {
            List<byte> bytes = new List<byte>();
            byte b;
            while ((b = ReadByte()) != 0)
            {
                bytes.Add(b);
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        public void CheckSignature(string sig)
        {
            var sigBytes = Encoding.UTF8.GetBytes(sig);

            var data = ReadBytes(sigBytes.Length);

            if (!data.SequenceEqual(sigBytes))
            {
                var enc = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
                throw new IOException($"Invalid signature. Expected {sig}, got {enc.GetString(data)}");
            }
        }

        public T ReadStruct<T>() where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            Span<byte> arr = stackalloc byte[size];
            BaseStream.Read(arr);
            return MemoryMarshal.Cast<byte, T>(arr)[0];
        }

        public T[] ReadStructArray<T>(int totalSize) where T : struct
        {
            T[] arr = new T[totalSize / Marshal.SizeOf(typeof(T))];
            Read(MemoryMarshal.Cast<T, byte>(arr));
            return arr;
        }

        public string ReadStringElsewhere(int position)
        {
            long prevPos = BaseStream.Position;

            BaseStream.Position = position;
            string result = ReadStringToNull();

            BaseStream.Position = prevPos;
            return result;
        }

        public string ReadFixedLengthAsciiString(int length)
        {
            byte[] bytes = ReadBytes(length);
            int zeroIndex = Array.IndexOf<byte>(bytes, 0);
            return Encoding.ASCII.GetString(bytes, 0, zeroIndex);
        }

        public T[] ReadObjectArray<T>(int count) where T : IBinarySerializable, new()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                T obj = new T();
                obj.Load(this);
                result[i] = obj;
            }
            return result;
        }

        public void AlignStream(int alignBase = 16)
        {
            long pos = BaseStream.Position;
            if (pos % alignBase != 0)
            {
                pos = pos - pos % alignBase + alignBase;
                BaseStream.Position = pos;
            }
        }

        public void CheckedAlignStream(int alignBase)
        {
            long pos = BaseStream.Position;
            if (pos % alignBase != 0)
            {
                int count = (int)(alignBase - pos % alignBase);
                for (int i = 0; i < count; i++)
                {
                    if (ReadByte() != 0)
                    {
                        throw new IOException($"CheckedAlign has read a non-null byte at {BaseStream.Position - 1}");
                    }
                }
            }
        }
    }
}

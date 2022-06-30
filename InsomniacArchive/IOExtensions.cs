using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive
{
    internal static class IOExtensions
    {
        private static readonly byte[] zeroBytes = new byte[1024];

        public static string ReadStringToNull(this BinaryReader br)
        {
            List<byte> bytes = new List<byte>();
            byte b;
            while ((b = br.ReadByte()) != 0)
            {
                bytes.Add(b);
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static void CheckSignature(this BinaryReader br, string sig)
        {
            var sigBytes = Encoding.UTF8.GetBytes(sig);

            var data = br.ReadBytes(sigBytes.Length);

            if (!Enumerable.SequenceEqual(data, sigBytes))
            {
                var enc = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
                throw new IOException($"Invalid signature. Expected {sig}, got {enc.GetString(data)}");
            }
        }

        public static T ReadStruct<T>(this BinaryReader br) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            Span<byte> arr = stackalloc byte[size];
            br.BaseStream.Read(arr);
            return MemoryMarshal.Cast<byte, T>(arr)[0];
        }

        public static T[] ReadStructArray<T>(this BinaryReader br, int totalSize) where T : struct
        {
            T[] arr = new T[totalSize / Marshal.SizeOf(typeof(T))];
            br.Read(MemoryMarshal.Cast<T, byte>(arr));
            return arr;
        }

        public static int WriteStructArray<T>(this BinaryWriter bw, T[] arr) where T : struct
        {
            Span<byte> byteSpan = MemoryMarshal.AsBytes<T>(arr);
            bw.Write(byteSpan);
            return byteSpan.Length;
        }

        public static int WriteStruct<T>(this BinaryWriter bw, T val) where T : struct
        {
            int length = Marshal.SizeOf<T>();
            var span = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref val, 1));
            bw.Write(span);
            return length;
        }

        public static string ReadFixedLengthAsciiString(this BinaryReader br, int length)
        {
            byte[] bytes = br.ReadBytes(length);
            int zeroIndex = Array.IndexOf<byte>(bytes, 0);
            return Encoding.ASCII.GetString(bytes, 0, zeroIndex);
        }

        public static void WriteFixedLengthString(this BinaryWriter bw, string value, int length)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            int remaining = length - bytes.Length;
            if (remaining < 0)
                throw new ArgumentOutOfRangeException($"value string {value} is longer than provided length {length}");
            bw.BaseStream.Write(bytes, 0, bytes.Length);
            bw.Pad(remaining);
        }

        public static void WriteNullTerminatedString(this BinaryWriter bw, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            bw.BaseStream.Write(bytes);
            bw.Write((byte)0);
        }

        public static T[] ReadObjectArray<T>(this BinaryReader br, int count) where T : IBinarySerializable, new()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                T obj = new T();
                obj.Load(br);
                result[i] = obj;
            }
            return result;
        }

        public static int WriteObjectArray<T>(this BinaryWriter bw, T[] data) where T : IBinarySerializable, new()
        {
            long start = bw.BaseStream.Position;
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Save(bw);
            }
            long end = bw.BaseStream.Position;
            return (int)(end - start);
        }

        public static void AlignStream(this BinaryReader br, int alignBase = 16)
        {
            long pos = br.BaseStream.Position;
            if (pos % alignBase != 0)
            {
                pos = pos - (pos % alignBase) + alignBase;
                br.BaseStream.Position = pos;
            }
        }

        public static void AlignStream(this BinaryWriter bw, int alignBase = 16)
        {
            long pos = bw.BaseStream.Position;
            if (pos % alignBase != 0)
            {
                int fixup = (int)(alignBase - (pos % alignBase));
                bw.BaseStream.Write(zeroBytes, 0, fixup);
            }
        }

        public static void Pad(this BinaryWriter bw, int length)
        {
            while (length > 0)
            {
                bw.Write(zeroBytes, 0, length > 1024 ? 1024 : length);
                length -= 1024;
            }
        }
    }
}

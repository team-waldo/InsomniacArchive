using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InsomniacArchive.Section
{
    internal abstract class StringDataSection : StructSection<byte>
    {
        public unsafe string GetString(int offset)
        {
            fixed (byte* ptr = &Data[offset])
            {
                return Marshal.PtrToStringUTF8((IntPtr)ptr);
            }
        }
    }
}

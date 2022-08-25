using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

using InsomniacArchive.FileTypes;
using InsomniacArchive.IO;

namespace InsomniacArchive.Serialization
{
    internal static class ObjectSerializer
    {
        public static JsonObject Deserialize(DatBinaryReader br)
        {
            return new ObjectDeserializerImpl(br).DeserializeObject();
        }

        private class ObjectDeserializerImpl
        {
            private readonly DatBinaryReader br;

            public ObjectDeserializerImpl(DatBinaryReader br)
            {
                this.br = br;
            }

            internal JsonNode Deserialize(NodeType type)
            {
                if (type == NodeType.NULL)
                {
                    br.ReadByte();
                    return null;
                }

                JsonNode result = type switch
                {
                    NodeType.UINT8 => JsonValue.Create(br.ReadByte()),
                    NodeType.UINT16 => JsonValue.Create(br.ReadUInt16()),
                    NodeType.UINT32 => JsonValue.Create(br.ReadUInt32()),
                    NodeType.INT8 => JsonValue.Create(br.ReadSByte()),
                    NodeType.INT16 => JsonValue.Create(br.ReadInt16()),
                    NodeType.INT32 => JsonValue.Create(br.ReadInt32()),
                    NodeType.FLOAT => JsonValue.Create(br.ReadSingle()),
                    NodeType.STRING => DeserializeString(),
                    NodeType.OBJECT => DeserializeObject(),
                    NodeType.BOOLEAN => JsonValue.Create(br.ReadBoolean()),
                    NodeType.INSTANCE_ID => JsonValue.Create(br.ReadUInt64()),
                    _ => throw new NotImplementedException(type.ToString()),
                };

                return result;
            }

            internal JsonValue DeserializeString()
            {
                int length = br.ReadInt32();
                uint crc32 = br.ReadUInt32();
                ulong crc64 = br.ReadUInt64();
                string value = Encoding.UTF8.GetString(br.ReadBytes(length));
                byte nullbyte = br.ReadByte();
                br.AlignStream(4);

                return JsonValue.Create(value);
            }

            internal JsonArray DeserializeArray(NodeType itemType, int itemCount)
            {
                JsonArray arr = new();

                for (int i = 0; i < itemCount; i++)
                {
                    JsonNode node = Deserialize(itemType);
                    arr.Add(node);
                }

                return arr;
            }

            internal JsonObject DeserializeObject()
            {
                JsonObject obj = new();

                var header = br.ReadStruct<SerializedObjectHeader>();

                if (header.zero != 0 || header.unk4 != 0x03150044)
                {
                    throw new IOException("Unknown ConfigObject header");
                }

                long startPos = br.BaseStream.Position;

                var childEntries = br.ReadStructArray<SerializedObjectChildEntry>(8 * header.childCount);
                var childNameOffsetArray = br.ReadStructArray<int>(4 * header.childCount);

                for (int i = 0; i < childEntries.Length; i++)
                {
                    var childEntry = childEntries[i];
                    string name = br.GetHashedStringLiteral(childEntry.nameHash);
                    // string same_name = br.ReadStringElsewhere(childNameOffsetArray[i]);

                    JsonNode child;
                    if (childEntry.ItemCount > 1)
                    {
                        child = DeserializeArray(childEntry.nodeType, childEntry.ItemCount);
                    }
                    else
                    {
                        child = Deserialize(childEntry.nodeType);
                    }

                    obj[name] = child;
                }

                br.CheckedAlignStream(4);

                long endPos = br.BaseStream.Position;

                if (endPos - startPos != header.dataSize)
                {
                    throw new IOException("Failed to fully deserialize ConfigObject");
                }

                return obj;
            }
        }

#pragma warning disable CS0649 // variable never assigned to

        private record struct SerializedObjectHeader
        {
            public int zero;
            public int unk4;
            public int childCount;
            public int dataSize;
        }

        private record struct SerializedObjectChildEntry
        {
            public uint nameHash;
            public short flag;
            public byte unk1;
            public NodeType nodeType;

            public int ItemCount => flag >> 4;

            public bool IsArray => ItemCount > 1;
        }

#pragma warning restore CS0649

        private enum NodeType : byte
        {
            UINT8 = 0x00,
            UINT16 = 0x01,
            UINT32 = 0x02,
            INT8 = 0x04,
            INT16 = 0x05,
            INT32 = 0x06,
            FLOAT = 0x08,
            STRING = 0x0A,
            OBJECT = 0x0D,
            BOOLEAN = 0x0F,
            INSTANCE_ID = 0x11, // 8 byte
            NULL = 0x13, // 1 byte, always zero. maybe null?
        }
    }
}

﻿using System.IO;

namespace Campari
{
    public static class Bytecode
    {
        public static uint[] ReadBytecode(FileInfo fileInfo)
        {
            byte[] data;
            int size;
            using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                size = (int)stream.Length;
                data = new byte[size];
                stream.Read(data, 0, size);
            }

            uint[] uintData = new uint[size / 4];
            using (var memoryStream = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    for (int i = 0; i < size / 4; i++)
                    {
                        uintData[i] = reader.ReadUInt32();
                    }
                }
            }

            return uintData;
        }
    }
}

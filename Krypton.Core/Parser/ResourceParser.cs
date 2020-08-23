using System.IO;
using System.Linq;
using System.Text;

namespace Krypton.Core.Parser
{
    public class ResourceParser
    {
        public int[] MethodKeys { get; set; }
        public byte[] Operands { get; set; }
        public string[] Strings { get; set; }
        public BinaryReader Reader { get; set; }

        public ResourceParser Parse(DevirtualizationCtx Ctx)
        {
            var resource = Ctx.Module.Resources.First(q => q.Name.Length == 37);
            if (resource == null)
                throw new DevirtualizationException("Could not locate Resource!");

            var data = resource.GetData();
            Ctx.Options.Logger.Success(
                $"Located Resource With Name {resource.Name} And Byte Data Length {data.Length}");

            Reader = new BinaryReader(new MemoryStream(data));
            Operands = new byte[255];

            var length = ReadEncryptedByte();
            for (var i = 0; i < length; i++)
            {
                var index = Reader.ReadByte();
                Operands[index] = Reader.ReadByte();
            }

            length = ReadEncryptedByte();
            Strings = new string[length];
            for (var i = 0; i < length; i++)
                Strings[i] = Encoding.Unicode.GetString(Reader.ReadBytes(ReadEncryptedByte()));

            length = ReadEncryptedByte();
            MethodKeys = new int[length];
            for (var i = 0; i < length; i++) MethodKeys[i] = ReadEncryptedByte();

            var pos = (int) Reader.BaseStream.Position;
            for (var i = 0; i < length; i++)
            {
                var res = MethodKeys[i];
                MethodKeys[i] = pos;
                pos += res;
            }

            return this;
        }

        public int ReadEncryptedByte()
        {
            var flag = false;
            var num = 0U;
            var num2 = Reader.ReadByte();
            num |= num2 & 63U;
            if ((num2 & 64U) != 0U) flag = true;
            if (num2 < 128U)
            {
                if (flag)
                    return ~(int) num;
                return (int) num;
            }

            var num3 = 0;
            for (;;)
            {
                var num4 = (uint) Reader.ReadByte();
                num |= (num4 & 127U) << (7 * num3 + 6);
                if (num4 < 128U) break;
                num3++;
            }

            if (flag) return ~(int) num;
            return (int) num;
        }
    }
}
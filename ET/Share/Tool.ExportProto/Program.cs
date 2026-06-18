using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;

namespace Tool.ExportProto
{
    internal class Option
    {
        [Option("Type", Required = true)]
        public string Type { get; set; } = string.Empty;

        [Option("ProtoPath", Required = true)]
        public string ProtoPath { set; get; } = string.Empty;

        [Option("ExportPath", Required = true)]
        public string ExportPath { set; get; } = string.Empty;
    }

    internal class OpcodeInfo
    {
        public string Name;
        public int Opcode;
    }

    internal partial class Program
    {
        private static readonly char[] splitChars = [' ', '\t'];

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Option>(args)
                .WithParsed(option =>
                {
                    Export(option.Type, option.ProtoPath, option.ExportPath);
                })
                .WithNotParsed(errors =>
                {
                    foreach (Error error in errors)
                    {
                        Console.Error.WriteLine(error.Tag);
                    }
                });
        }

        private static void Export(string type, string protoPath, string exportPath)
        {
            if (type == "File")
            {
                Proto2CS(protoPath, exportPath);
                return;
            }
            if (type == "Directory")
            {
                Directory.GetFiles(protoPath, "*.proto").ToList().ForEach(file => Proto2CS(file, Path.Combine(exportPath, Path.GetFileNameWithoutExtension(file) + ".cs")));
                return;
            }
        }

        private static void Proto2CS(string protoPath, string csPath)
        {
            string protoText = File.ReadAllText(protoPath);
            string protoName = Path.GetFileNameWithoutExtension(protoPath);
            bool isMsgStart = false;
            string msgName = string.Empty;
            string protoId = string.Empty;
            string responseType = string.Empty;
            Regex protoIdRegex = ProtoIdRegex();
            Regex responseTypeRegex = ResponseTypeRegex();
            StringBuilder sbDispose = new();
            StringBuilder sb = new StringBuilder();
            HashSet<int> opcodes = new HashSet<int>();
            List<OpcodeInfo> msgOpcode = [];

            sb.Append("using MemoryPack;\n");
            sb.Append("using ProtoBuf;\n");
            sb.Append("using System.Collections.Generic;\n\n");
            sb.Append($"namespace ET\n");
            sb.Append("{\n");
            string[] array = protoText.Split('\n');
            for (int i = 0; i < array.Length; i++)
            {
                string line = array[i];
                string newline = line.Trim();
                if (string.IsNullOrEmpty(newline))
                {
                    continue;
                }

                if (newline.StartsWith("//"))
                {
                    Match idMatch = protoIdRegex.Match(newline);
                    if (idMatch.Success)
                    {
                        protoId = idMatch.Groups["id"].Value;
                        continue;
                    }

                    Match typeMath = responseTypeRegex.Match(newline);
                    if (typeMath.Success)
                    {
                        responseType = typeMath.Groups["type"].Value;
                        continue;
                    }

                    if (!isMsgStart)
                    {
                        if (newline.StartsWith("///"))
                        {
                            sb.Append("\t/// <summary>\n");
                            sb.Append($"\t/// {newline.TrimStart('/', ' ')}\n");
                            sb.Append("\t/// </summary>\n");
                        }
                        else
                        {
                            sb.Append($"\t// {newline.TrimStart('/', ' ')}\n");
                        }
                        continue;
                    }
                }

                if (newline.StartsWith("message"))
                {
                    Match match = MessgeClassRegex().Match(newline);
                    if (match.Success)
                    {
                        isMsgStart = true;
                        msgName = match.Groups["class"].Value;
                        string parentClass = match.Groups[1].Value;
                        msgOpcode.Add(new OpcodeInfo() { Name = msgName, Opcode = string.IsNullOrWhiteSpace(protoId) ? 0 : int.Parse(protoId) });
                        opcodes.Add(msgOpcode.Last().Opcode);

                        sb.Append("\t[ProtoContract]\n");
                        if (!string.IsNullOrWhiteSpace(protoId))
                        {
                            sb.Append($"\t[ProtoId({protoName}.{msgName})]\n");
                        }
                        if (!string.IsNullOrEmpty(responseType))
                        {
                            sb.Append($"\t[ResponseType(nameof({responseType}))]\n");
                        }

                        sb.Append($"\tpublic partial class {msgName} : MessageObject");
                        sb.Append(string.IsNullOrWhiteSpace("") ? "\n" : $", {""}\n");
                        if (newline.Contains('{'))
                        {
                            sbDispose.Clear();
                            sb.Append("\t{\n");
                            sb.Append($"\t\tpublic static {msgName} Create(bool isFromPool = false)\n\t\t{{\n\t\t\treturn ObjectPool.Instance.Fetch(typeof({msgName}), isFromPool) as {msgName};\n\t\t}}\n\n");
                        }
                        continue;
                    }

                    throw new Exception($"line {i + 1} parse error");
                }

                if (isMsgStart)
                {
                    if (newline.StartsWith('{'))
                    {
                        sbDispose.Clear();
                        sb.Append("\t{\n");
                        sb.Append($"\t\tpublic static {msgName} Create(bool isFromPool = false)\n\t\t{{\n\t\t\treturn ObjectPool.Instance.Fetch(typeof({msgName}), isFromPool) as {msgName};\n\t\t}}\n\n");
                        continue;
                    }

                    if (newline.StartsWith('}'))
                    {
                        isMsgStart = false;
                        protoId = string.Empty;
                        responseType = string.Empty;

                        // 加了no dispose则自己去定义dispose函数，不要自动生成
                        if (!newline.Contains("// no dispose"))
                        {
                            sb.Append($"\t\tpublic override void Dispose()\n\t\t{{\n\t\t\tif (!this.IsFromPool)\n\t\t\t{{\n\t\t\t\treturn;\n\t\t\t}}\n\n\t\t\t{sbDispose.ToString().TrimEnd('\t')}\n\t\t\tObjectPool.Instance.Recycle(this);\n\t\t}}\n");
                        }

                        sb.Append("\t}\n\n");
                        continue;
                    }

                    if (newline.StartsWith("//"))
                    {
                        sb.Append("\t\t/// <summary>\n");
                        sb.Append($"\t\t/// {newline.TrimStart('/', ' ')}\n");
                        sb.Append("\t\t/// </summary>\n");
                        continue;
                    }

                    string memberStr;
                    if (newline.Contains("//"))
                    {
                        string[] lineSplit = newline.Split("//");
                        memberStr = lineSplit[0].Trim();
                        sb.Append("\t\t/// <summary>\n");
                        sb.Append($"\t\t/// {lineSplit[1].Trim()}\n");
                        sb.Append("\t\t/// </summary>\n");
                    }
                    else
                    {
                        memberStr = newline;
                    }

                    if (memberStr.StartsWith("map"))
                    {
                        Map(sb, memberStr, sbDispose);
                        continue;
                    }

                    if (memberStr.StartsWith("repeated"))
                    {
                        Repeated(sb, memberStr, sbDispose);
                        continue;
                    }

                    Members(sb, memberStr, sbDispose);
                }
            }

            sb.Append("\tpublic static class " + protoName + "\n\t{\n");
            msgOpcode.Where(op => op.Opcode != 0).ToList().ForEach(info => sb.Append($"\t\tpublic const int {info.Name} = {info.Opcode};\n"));
            sb.Append("\t}\n");
            sb.Append('}');
            sb.Replace("\t", "    ");
            string result = sb.ToString().ReplaceLineEndings("\r\n");
            string dir = Path.GetDirectoryName(csPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(csPath, result);
            Console.WriteLine($"proto {protoPath} generate to {csPath} succeed!");
        }

        private static void Map(StringBuilder sb, string newline, StringBuilder sbDispose)
        {
            int start = newline.IndexOf('<') + 1;
            int end = newline.IndexOf('>');
            string types = newline.Substring(start, end - start);
            string[] ss = types.Split(',');
            string keyType = ConvertType(ss[0].Trim());
            string valueType = ConvertType(ss[1].Trim());
            string tail = newline[(end + 1)..];
            ss = tail.Trim().Replace(";", "").Split(' ');
            string v = ss[0];
            int n = int.Parse(ss[2]);

            sb.Append("\t\t[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]\n");
            sb.Append($"\t\t[ProtoMember({n})]\n");
            sb.Append($"\t\tpublic Dictionary<{keyType}, {valueType}> {v} {{ get; set; }} = new();\n");

            sbDispose.Append($"this.{v}.Clear();\n\t\t\t");
        }

        private static void Repeated(StringBuilder sb, string newline, StringBuilder sbDispose)
        {
            try
            {
                int index = newline.IndexOf(';');
                newline = newline.Remove(index);
                string[] ss1 = newline.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string[] ss2 = ss1[0].Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                string type = ss2[1];
                type = ConvertType(type);
                string name = ss2[2];
                int n = int.Parse(ss1[1]);

                sb.Append($"\t\t[ProtoMember({n})]\n");
                sb.Append($"\t\tpublic List<{type}> {name} {{ get; set; }} = new();\n\n");

                sbDispose.Append($"this.{name}.Clear();\n\t\t\t");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{newline}\n {e}");
            }
        }

        private static void Members(StringBuilder sb, string newline, StringBuilder sbDispose)
        {
            try
            {
                int index = newline.IndexOf(';');
                newline = newline.Remove(index);
                string[] ss1 = newline.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string[] ss2 = ss1[0].Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                string type = ss2[0];
                string name = ss2[1];
                int n = int.Parse(ss1[1]);
                string typeCs = ConvertType(type);

                sb.Append($"\t\t[ProtoMember({n})]\n");
                sb.Append($"\t\tpublic {typeCs} {name} {{ get; set; }}\n\n");

                switch (typeCs)
                {
                    case "bytes":
                        {
                            break;
                        }
                    default:
                        sbDispose.Append($"this.{name} = default;\n\t\t\t");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{newline}\n {e}");
            }
        }

        private static string ConvertType(string type)
        {
            return type switch
            {
                "int16" => "short",
                "int32" => "int",
                "bytes" => "byte[]",
                "uint32" => "uint",
                "long" => "long",
                "int64" => "long",
                "uint64" => "ulong",
                "uint16" => "ushort",
                _ => type
            };
        }

        [GeneratedRegex(@"//\s*ProtoId +(?<id>-?\d+)")]
        private static partial Regex ProtoIdRegex();

        [GeneratedRegex(@"//\s*ResponseType +(?<type>\w+)")]
        private static partial Regex ResponseTypeRegex();

        [GeneratedRegex(@"message +(?<class>\w+)")]
        private static partial Regex MessgeClassRegex();
    }
}
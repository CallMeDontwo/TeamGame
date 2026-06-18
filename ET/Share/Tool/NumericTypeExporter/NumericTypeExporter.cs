using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ET
{
    public static class NumericTypeExporter
    {
        private const string ExcelPath = "../Unity/Assets/Config/Excel/NumericTypeConfig.xlsx";
        private const string OutputPath = "../Unity/Assets/Scripts/Model/Share/Module/Numeric/NumericType.cs";

        public static void Export()
        {
            try
            {
                Log.Info("NumericTypeExporter 开始导出...");

                // 1. 读取 Excel
                string excelFullPath = Path.GetFullPath(ExcelPath);
                if (!File.Exists(excelFullPath))
                {
                    Log.Error($"NumericTypeConfig.xlsx 未找到: {excelFullPath}");
                    return;
                }

                List<(int Id, string Name)> items = new();
                using Stream stream = new FileStream(excelFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var package = new OfficeOpenXml.ExcelPackage(stream);
                var ws = package.Workbook.Worksheets[0];

                // 从第6行开始读（Row 3=中文, Row 4=字段名, Row 5=类型, Row 6+=数据）
                for (int row = 6; row <= ws.Dimension.End.Row; row++)
                {
                    var idCell = ws.Cells[row, 3].Value;   // Col C = Id
                    var nameCell = ws.Cells[row, 4].Value; // Col D = Name

                    if (idCell == null || nameCell == null) continue;

                    int id = Convert.ToInt32(idCell);
                    string name = nameCell.ToString().Trim();

                    if (string.IsNullOrEmpty(name)) continue;

                    // 确保是 PascalCase
                    if (char.IsLower(name[0]))
                        name = char.ToUpper(name[0]) + name.Substring(1);

                    items.Add((id, name));
                }

                if (items.Count == 0)
                {
                    Log.Error("NumericTypeConfig 表中无数据");
                    return;
                }

                // 2. 生成 NumericType.cs
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("namespace ET");
                sb.AppendLine("{");
                sb.AppendLine("\tpublic static class NumericType");
                sb.AppendLine("\t{");
                sb.AppendLine($"\t\tpublic const int Max = {items.Count * 1000 + 10000};");
                sb.AppendLine();

                foreach (var (id, name) in items)
                {
                    sb.AppendLine($"\t\tpublic const int {name} = {id};");
                }

                sb.AppendLine("\t}");
                sb.AppendLine("}");

                string outputDir = Path.GetDirectoryName(Path.GetFullPath(OutputPath));
                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                File.WriteAllText(Path.GetFullPath(OutputPath), sb.ToString(), Encoding.UTF8);

                Log.Info($"NumericType.cs 已生成，共 {items.Count} 个数值类型");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}

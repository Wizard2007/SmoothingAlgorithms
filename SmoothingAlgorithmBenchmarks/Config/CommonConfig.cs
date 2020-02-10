using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Reports;

namespace SmoothingAlgorithmBenchmarks.Configs
{
    public class CommonConfig : ManualConfig
    {
        public CommonConfig()
        {
            UnionRule = ConfigUnionRule.Union;
            Options = Options | ConfigOptions.JoinSummary;
            SummaryStyle = SummaryStyle.Default.WithMaxParameterColumnWidth(50);

            Add(TargetMethodColumn.Method,
                StatisticColumn.Mean,
                StatisticColumn.Min,
                StatisticColumn.Max,
                StatisticColumn.Error,
                StatisticColumn.StdDev);

            Add(CsvExporter.Default);
            Add(CsvMeasurementsExporter.Default);
            Add(HtmlExporter.Default);
            Add(AsciiDocExporter.Default);
            Add(MarkdownExporter.Default);
        }
    }
}
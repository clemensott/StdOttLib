using System.Collections.Generic;
using System.IO;

namespace StdOttStandard.Converter.MultipleInputs
{
    public static class MultipleValuesConverterGenerator
    {
        public static void Generate(int start, int end)
        {
            const string wpfDirName = "MultipleInputsConverterWPF";
            const string uwpDirName = "MultipleInputsConverterUWP";

            DirectoryInfo wpfDir = Directory.Exists(wpfDirName) ?
                new DirectoryInfo(wpfDirName) : Directory.CreateDirectory(wpfDirName);

            for (int i = start; i <= end; i++)
            {
                string code = string.Join("\r\n", GetMultipleValueConverter(i, false));
                string filePath = Path.Combine(wpfDir.FullName, $"MultipleInputs{i}Converter.cs");

                File.WriteAllText(filePath, code);
            }

            DirectoryInfo uwpDir = Directory.Exists(uwpDirName) ?
              new DirectoryInfo(uwpDirName) : Directory.CreateDirectory(uwpDirName);

            for (int i = start; i < end; i++)
            {
                string code = string.Join("\r\n", GetMultipleValueConverter(i, true));
                string filePath = Path.Combine(uwpDir.FullName, $"MultipleInputs{i}Converter.cs");

                File.WriteAllText(filePath, code);
            }
        }

        public static IEnumerable<string> GetMultipleValueConverter(int c, bool uwp)
        {
            yield return "using StdOttStandard.Converter.MultipleInputs;";

            if (uwp) yield return "using Windows.UI.Xaml;";
            else yield return "using System.Windows;";

            yield return "";

            if (uwp) yield return "namespace StdOttUwp.Converters";
            else yield return "namespace StdOttFramework.Converters";

            yield return "{";
            yield return $"\tpublic class MultipleInputs{c}Converter : MultipleInputsConverter<MultiplesInputsConvert{c}EventArgs>";
            yield return "\t{";

            for (int i = 0; i < c; i++)
            {
                yield return $"\t\tpublic static readonly DependencyProperty Input{i}Property =";
                yield return $"\t\t\tDependencyProperty.Register(\"Input{i}\", typeof(object), typeof(MultipleInputs{c}Converter),";
                yield return $"\t\t\t\tnew PropertyMetadata(null, new PropertyChangedCallback(OnInput{i}PropertyChanged)));\r\n";
                yield return "";
                yield return $"\t\tprivate static void OnInput{i}PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)";
                yield return "\t\t{";
                yield return $"\t\t\t((MultipleInputs{c}Converter)sender).SetOutput({i}, e.OldValue);";
                yield return "\t\t}";
                yield return "";
            }

            for (int i = 0; i < c; i++)
            {
                yield return $"\t\tpublic object Input{i}";
                yield return "\t\t{";
                yield return $"\t\t\tget => GetValue(Input{i}Property);";
                yield return $"\t\t\tset => SetValue(Input{i}Property, value);";
                yield return "\t\t}";
                yield return "";
            }
            
            yield return "\t\tprotected override void SetOutputNonRef(int changedIndex, object oldValue)";
            yield return "\t\t{";
            yield return $"\t\t\tMultiplesInputsConvert{c}EventArgs args = new MultiplesInputsConvert{c}EventArgs(changedIndex, oldValue)";
            yield return "\t\t\t{";

            for (int i = 0; i < c; i++)
            {
                yield return $"\t\t\t\tInput{i} = Input{i},";
            }

            yield return "\t\t\t};";
            yield return "";
            yield return "\t\t\tOutput = GetLastConvert()(this, args);";
            yield return "\t\t}";
            yield return "";
            yield return "\t\tprotected override void SetOutputRef(int changedIndex, object oldValue)";
            yield return "\t\t{";
            yield return $"\t\t\tMultiplesInputsConvert{c}EventArgs args = new MultiplesInputsConvert{c}EventArgs(changedIndex, oldValue)";
            yield return "\t\t\t{";

            for (int i = 0; i < c; i++)
            {
                yield return $"\t\t\t\tInput{i} = Input{i},";
            }

            yield return "\t\t\t};";
            yield return "";
            yield return "\t\t\tOutput = GetLastConvertRef()(this, args);";
            yield return "";

            for (int i = 0; i < c; i++)
            {
                yield return $"\t\t\tif (!Equals(Input{i}, args.Input{i})) Input{i} = args.Input{i};";
            }
            
            yield return "\t\t}";
            yield return "\t}";
            yield return "}";
            yield return "";
        }
    }
}

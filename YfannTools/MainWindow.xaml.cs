using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;

namespace YfannTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bu=sender as Button;
            if (bu.Tag.ToString() == "Parse")
            {
                string str = textSource.Text;
                StringBuilder sb = new StringBuilder();
                using (StringReader reader = new StringReader(str))
                {
                    string line;
                    if (chbCsharpType.IsChecked.Value)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            line = Regex.Replace(line, @"""", "\"\"");
                            line = Regex.Replace(line, @"^\s+(?=[<|\W])", "");
                            line = Regex.Replace(line, @"\s+$", "");
                            line = "sb.Append(@\"" + line + "\");";
                            sb.AppendLine(line);
                        }
                    }
                    if (chbSqlType.IsChecked.Value)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            line = Regex.Replace(line,@"^\s+","");
                            line = Regex.Replace(line,@"\s+$","");
                            line = "+N' " + line + " '";
                            sb.AppendLine(line);
                        }
                    }
                }
                textTarget.Text = sb.ToString();
            }
        }
    }
}

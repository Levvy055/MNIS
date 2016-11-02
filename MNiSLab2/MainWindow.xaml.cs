using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MNiSLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Tfs = new List<TextBox>();
            InitializeComponent();
            UpdateGrid(2);
        }

        private void UpdateGrid(int count)
        {
            if (count > 100 || count == 0) { return; }
            MGrid.Children.Clear();
            MGrid.RowDefinitions.Clear();
            Tfs.Clear();
            for (var i = 0; i < count; i++)
            {
                MGrid.RowDefinitions.Add(new RowDefinition());
                var tf = new TextBox();
                MGrid.Children.Add(tf);
                Grid.SetRow(tf, i);
                Tfs.Add(tf);
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EqCount.Text))
            {
                var eqCount = 1;
                int.TryParse(EqCount.Text, out eqCount);
                UpdateGrid(eqCount);
            }
        }

        private void CountBtnClick(object sender, RoutedEventArgs e)
        {
            var eqs = new Eq[Tfs.Count];
            for (var t = 0; t < Tfs.Count; t++)
            {
                var tf = Tfs[t];
                var txt = tf.Text;
                if (string.IsNullOrWhiteSpace(txt))
                {
                    return;
                }
                var vars = Eq.GetVars(txt);
                var eq = new Eq(vars);
                eqs[t] = eq;
            }
            var eqSystem = new EqSystem(eqs);
            eqSystem.GetResults();
        }

        private List<TextBox> Tfs { get; set; }
    }
}

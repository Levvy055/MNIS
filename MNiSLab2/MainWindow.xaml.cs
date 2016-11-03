using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MNiSLab2
{
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
                tf.MinWidth = 200;
                tf.MinHeight = 30;
                tf.FontSize = 16d;
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
                char[] literals;
                var vars = Eq.GetVars(txt, out literals);
                Literals = literals;
                var eq = new Eq(vars);
                eqs[t] = eq;
            }
            var eqSystem = new EqSystem(eqs);
            var res = eqSystem.GetResults();
            if (res != null)
            {
                ShowResults(res);
            }
        }

        private void ShowResults(decimal[] res)
        {
            RGrid.Children.Clear();
            RGrid.RowDefinitions.Clear();
            for (var i = 0; i < res.Length; i++)
            {
                var tf = new Label();
                tf.FontSize = 20;
                tf.Content = Literals[i] + " = " + decimal.Round(res[i], 2, MidpointRounding.AwayFromZero);
                RGrid.RowDefinitions.Add(new RowDefinition());
                RGrid.Children.Add(tf);
                Grid.SetRow(tf, i);
            }
        }

        private List<TextBox> Tfs { get; set; }
        public char[] Literals { get; set; }
    }
}

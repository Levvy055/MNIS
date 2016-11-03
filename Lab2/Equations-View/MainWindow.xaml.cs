using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Equations;

namespace EquationsView
{
    public partial class MainWindow : Window
    {
        const int DEFAULT_START_EQ_COUNT = 2;

        public MainWindow()
        {
            Tfs = new List<TextBox>();
            InitializeComponent();
            EqCount.Text = DEFAULT_START_EQ_COUNT.ToString();
            UpdateGrid(DEFAULT_START_EQ_COUNT);
            FocusManager.SetFocusedElement(this, EqCount);
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
                var tf = new TextBox
                {
                    MinWidth = 200,
                    MinHeight = 30,
                    FontSize = 16d
                };
                if (i == count - 1)
                {
                    tf.KeyUp += (sender, args) => { if (args.Key == Key.Enter) { CountBtn_Click(tf, args); } };
                }
                MGrid.Children.Add(tf);
                Grid.SetRow(tf, i);
                Tfs.Add(tf);
            }
        }

        private void EqsCount_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EqCount.Text))
            {
                return;
            }
            int eqCount;
            int.TryParse(EqCount.Text, out eqCount);
            UpdateGrid(eqCount);
        }

        private void CountBtn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                var eqs = new Eq[Tfs.Count];
                char[] literals = { };
                for (var t = 0; t < Tfs.Count; t++)
                {
                    var tf = Tfs[t];
                    var txt = tf.Text;
                    if (string.IsNullOrWhiteSpace(txt))
                    {
                        return;
                    }
                    var vars = Eq.GetVarsFromStringEquation(txt, out literals);
                    if (vars == null)
                    {
                        throw new ArgumentException("Zly format wejsciowy rownania. Podano: " + txt);
                    }
                    var eq = new Eq(vars);
                    eqs[t] = eq;
                }
                var solveMethod = EqMgauss.IsChecked == true ? SolvingType.GaussElimination : SolvingType.Determinants;
                var eqSystem = new EqSystem(eqs, literals, solveMethod);
                var res = eqSystem.GetResults();
                if (res != null)
                {
                    ShowResults(res);
                }
            /*}
            catch (IndexOutOfRangeException ex)
            {
                EqsCount_KeyUp(null, null);
                MessageBox.Show("Zla ilosc zmiennych!", "Bledo!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                EqsCount_KeyUp(null, null);
                MessageBox.Show(ex.Message, "Bledo!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void ShowResults(Dictionary<char, decimal> res)
        {
            RGrid.Children.Clear();
            RGrid.RowDefinitions.Clear();
            var i = 0;
            foreach (var rPair in res)
            {
                var tf = new Label
                {
                    FontSize = 20,
                    Content = rPair.Key + " = " + decimal.Round(rPair.Value, 2, MidpointRounding.AwayFromZero)
                };
                RGrid.RowDefinitions.Add(new RowDefinition());
                RGrid.Children.Add(tf);
                Grid.SetRow(tf, i);
                i++;
            }
        }

        private List<TextBox> Tfs { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Fisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Variant;
        public MainWindow()
        {
            InitializeComponent();           
            
        }



        private void DrawGraphic(List<double> x, List<double> y)
        {
            Graphic.Plot.AddScatter(x.ToArray(), y.ToArray(), System.Drawing.Color.FromArgb(255, 0, 0, 255), 1, 1);
            Graphic.Refresh();
        }

        private void Variant_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Text = sender as TextBox;
                var variant = Text?.Text;
                Variant = int.Parse(variant);
                Model model = new Model(Variant);
                DrawGraphic(model.x, model.y);
                var newY = model.Calculate(Variant);
                DrawGraphic(model.x, newY);
                MessageBox.Show(model.FindFisher(newY).ToString());
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }
    }
}

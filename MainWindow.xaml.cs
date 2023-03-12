using System;
using System.Collections.Generic;
using System.Drawing;
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


        //метод для отрисовки графика
        private void DrawGraphic(List<double> x, List<double> y, Color color)
        {
            Graphic.Plot.AddScatter(x.ToArray(), y.ToArray(), color, 1, 1);
            Graphic.Refresh();
        }
        //обработка события изменения текста в поле для ввода варианта
        private void Variant_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Text = sender as TextBox;
                var variant = Text?.Text;
                Variant = int.Parse(variant);
                Model model = new Model(Variant);
                DrawGraphic(model.x, model.y, Color.DarkViolet);
                var newY = model.Calculate(Variant);
                DrawGraphic(model.x, newY, Color.Crimson);
                MessageBox.Show(model.FindFisher(newY).ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}

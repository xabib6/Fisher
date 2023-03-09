using ScottPlot.Statistics.Interpolation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Fisher
{
    // Основной класс для проведения расчётов
    internal class Model
    {
        public List<double> x;
        public List<double> y;


        //Конструктор, подставляющий исходные данные в зависимости от варианта
        public Model(int variant) 
        { 
            switch (variant)
            {
                case  8:
                    x = new List<double>() {1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    y = new List<double>() {0.385, 0.412, 0.44, 0.454, 0.469, 0.493, 0.528, 0.50, 0.485, 0.47, 0.465, 0.454, 0.441, 0.427, 0.412, 0.398, 0.384, 0.371, 0.359, 0.348 };
                    break;
                case 19:
                    x = new List<double>() { 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0, 5.5, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0, 10.5, 11 };
                    y = new List<double>() {29.5, 29.6, 29.7, 29.8, 29.9, 31, 31.1, 31.8, 32.6, 33.8, 34.7, 35.8, 36.8, 37.7, 38.3, 38.7, 38.9, 39.2, 39.3, 39.3 };
                    break;
                case 16:
                    x = new List<double>() {0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2 };
                    y = new List<double>() { 1.21, 4.79, 10.62, 18.49, 28.06, 38.96, 50.75, 62.96, 75.1, 86.69, 97.26, 106.4, 113.73, 118.9, 121.9, 122.4, 120.5, 116.2, 109.7, 101.3 };
                    break;
                    case 25:
                    x = new List<double>() { 1, 2, 3, 4, 5, 6, 7.5, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    y = new List<double>() { 14.0, 12.8, 11.2, 11.0, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 10.8, 11.0, 11.2, 12.8, 14.0 };
                    break;
                case 26:
                    x = new List<double>() {  1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    y = new List<double>() { 17.1, 15.0, 13.1, 12.6, 12.5, 12.8, 13.0, 13.6, 13.9, 14.2, 14.6, 14.8,
15.2, 15.4, 15.6, 15.8, 16, 16.2, 16.4, 16.5};
                    break;
                default:
                    throw new NotImplementedException("Введите данные для своего варианта");
            }
        }
        //основной метод для расчёта точек нового графика
        public List<double> Calculate(int variant)
        {
            var matrix = new double[3, 3];        

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i + j == 4)
                    {
                        matrix[i, j] = x.Count;
                    }
                    else
                    {
                        matrix[i, j] = FindSumX(i, j);
                    }
                }
            }

            var matrixA =(double[,])matrix.Clone();
            var matrixB =(double[,])matrix.Clone();
            var matrixC =(double[,])matrix.Clone();
            var p = new double[3];

            for (int i = 0; i < p.Length; i++)
            {
                p[i] = FindSumXY(i);
                matrixA[i, 0] = p[i];
                matrixB[i, 1] = p[i];
                matrixC[i, 2] = p[i];
            }
            var det = FindDet(matrix);
            var detA = FindDet(matrixA);
            var detB = FindDet(matrixB);    
            var detC = FindDet(matrixC);

            var A = detA / det;
            var B = detB / det;
            var C = detC / det;


            var newY = new List<double>();

            for (int i = 0; i < x.Count; i++)
            {
                var y = ((A * x[i] * x[i] + B * x[i] + C));
                newY.Add(y);
            }
            return newY;
        }

        //метод для нахождения элементов матрицы
        private double FindSumX(int xPos, int yPos)
        {
            double sum = 0;

            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], 4 - xPos - yPos);
            }
            return sum;
        }
        //метод для нахождения свободных членов
        private double FindSumXY(int xPos)
        {
            double sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(x[i], 2 - xPos) * y[i];
            }
            return sum;
        }

        //метод для нахождения детерминанта матрицы
        private double FindDet(double[,] M)
        {
            return M[0, 0] * M[1, 1] * M[2, 2]
                 + M[0, 1] * M[1, 2] * M[2, 0]
                 + M[0, 2] * M[1, 0] * M[2, 1]
                 - M[0, 2] * M[1, 1] * M[2, 0]
                 - M[0, 0] * M[1, 2] * M[2, 1]
                 - M[0, 1] * M[1, 0] * M[2, 2];
        }
        //метод для нахождения коэффициента Фишера
        public double FindFisher(List<double> newY)
        {
            var sum = 0d;
            for (int i = 0; i < newY.Count; i++)
            {
                sum += newY[i];
            }
            var Avg = sum/newY.Count;

            sum = 0d;
            var sumOst = 0d;
            for (int i = 0; i < newY.Count; i++)
            {
                sum += Math.Pow((newY[i] - Avg), 2);
                sumOst += Math.Pow((newY[i] - y[i]), 2);
            }
            sum  = sum / (newY.Count - 1);
            sumOst = sumOst / (newY.Count - 1);

            return sum/sumOst;
    
        }
        
        private void ReplaceColumn(int row, int column, double[,] mainMatrix, double[,] subMatrix, double p)
        {            
                subMatrix[row, column] = p;
        }
    }
}

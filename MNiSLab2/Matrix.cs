using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNiSLab2
{
    public class Matrix
    {
        private int rc_matrix;
        private double[,] matrix; 

        public Matrix(double[,] double_array)
        {
            matrix = double_array;
            rc_matrix = matrix.GetLength(0);
        }

        public double getElem(int row, int column)
        {
            return matrix[row, column];
        }

        public void setElem(double value, int row, int column)
        {
            matrix[row, column] = value;
        }

        public double Determinant()
        {
            var det = 0d;
            var value = 0d;
            int i, j, k;

            i = rc_matrix;
            j = rc_matrix;
            int n = i;
            
            for (i = 0; i < n - 1; i++)
            {
                for (j = i + 1; j < n; j++)
                {
                    det = (this.getElem(j, i) / this.getElem(i, i));

                    for (k = i; k < n; k++)
                    {
                        value = this.getElem(j, k) - det * this.getElem(i, k);

                        this.setElem(value, j, k);
                    }
                }
            }
            det = 1;
            for (i = 0; i < n; i++)
                det = det * this.getElem(i, i);
            return det;
        }
    }

}

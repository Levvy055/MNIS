namespace EquationsView
{
    public class Matrix
    {
        private int rc_matrix;
        private decimal[,] _matrix;

        public Matrix(decimal[,] matrix)
        {
            Base = matrix;
            ConvertToSymMatrix();
        }

        private void ConvertToSymMatrix()
        {
            var m = (decimal[,])Base.Clone();
            rc_matrix = Base.GetLength(0);
            _matrix = new decimal[rc_matrix, rc_matrix];
            for (var i = 0; i < rc_matrix; i++)
            {
                for (var j = 0; j < rc_matrix; j++)
                {
                    _matrix[i, j] = m[i, j];
                }
            }
        }


        public decimal getElem(int row, int column)
        {
            return _matrix[row, column];
        }

        public void setElem(decimal value, int row, int column)
        {
            _matrix[row, column] = value;
        }

        public decimal Determinant()
        {
            ConvertToSymMatrix();
            decimal det = 0;
            var n = rc_matrix;

            for (var i = 0; i < n - 1; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    det = (this.getElem(j, i) / this.getElem(i, i));

                    for (var k = i; k < n; k++)
                    {
                        var value = this.getElem(j, k) - det * this.getElem(i, k);
                        this.setElem(value, j, k);
                    }
                }
            }
            det = 1;
            for (var i = 0; i < n; i++)
            {
                det = det * this.getElem(i, i);
            }
            return (decimal)det;
        }

        public Matrix Clone()
        {
            return new Matrix((decimal[,])Base.Clone());
        }

        public decimal[,] Base { get; private set; }
    }
}

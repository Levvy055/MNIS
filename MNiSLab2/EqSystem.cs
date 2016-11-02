using System.Collections.Generic;
using System.Linq;

namespace MNiSLab2
{
    public class EqSystem
    {
        public EqSystem(Eq[] eqs)
        {
            Eqs = eqs;
            var varss = new decimal[Size, Size + 1];
            for (var i = 0; i < Size; i++)
            {
                var eqVars = new decimal[Eqs[i].Vars.Length];
                for (var j = 0; j < Eqs[i].Vars.Count(); j++)
                {
                    eqVars[j] = (decimal) Eqs[i].Vars[j];
                }
                for (var j = 0; j <= Size; j++)
                {
                    varss[i, j] = eqVars[j];
                }
            }
            EqsMatrix = new Matrix(varss);
            CountMainDeterminant();
            CountOtherDeterminants();
        }

        private void CountMainDeterminant()
        {
            MainDeterminant = CountDet(0);
        }

        private void CountOtherDeterminants()
        {
            OtherDeterminants = new decimal[Size];
            for (var i = 0; i < Size; i++)
            {
                OtherDeterminants[i] = CountDet(i + 1);
            }
        }

        private decimal CountDet(int i)
        {
            if (i == 0)
            {
                return EqsMatrix.Determinant();
            }
            var matrix = EqsMatrix.Clone();
            for (var j = 0; j < Size; j++)
            {
                var c = matrix.Base[j, Size];
                matrix.Base[j, i - 1] = c;
            }
            return matrix.Determinant();
        }

        public decimal[] GetResults()
        {

            var list = new List<decimal>();
            for (var i = 0; i < Size; i++)
            {
                list.Add(OtherDeterminants[i] / MainDeterminant);
            }
            return list.ToArray();
        }

        private Matrix EqsMatrix { get; set; }
        private Eq[] Eqs { get; set; }
        private int Size { get { return Eqs.Count(); } }
        private decimal MainDeterminant { get; set; }
        private decimal[] OtherDeterminants { get; set; }
    }
}

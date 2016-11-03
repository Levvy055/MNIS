using System;
using System.Collections.Generic;
using System.Linq;

namespace Equations
{
    public class EqSystem
    {
        private char[] _literals;
        private readonly SolvingType _solvingType;

        public EqSystem(Eq[] eqs, char[] literals, SolvingType solvingType)
        {
            Eqs = eqs;
            _literals = literals;
            _solvingType = solvingType;
            var varss = new decimal[Size, Size + 1];
            for (var i = 0; i < Size; i++)
            {
                var eqVars = new decimal[Eqs[i].Vars.Length];
                for (var j = 0; j < Eqs[i].Vars.Count(); j++)
                {
                    eqVars[j] = (decimal)Eqs[i].Vars[j];
                }
                for (var j = 0; j <= Size; j++)
                {
                    varss[i, j] = eqVars[j];
                }
            }
            EqsMatrix = new Matrix(varss);
            switch (solvingType)
            {
                case SolvingType.Determinants:
                    CountDeterminantsMethod();
                    break;
                case SolvingType.GaussElimination:
                    CountGaussEliminationMethod();
                    break;
            }
        }

        #region determinant method
        private void CountDeterminantsMethod()
        {
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

        #endregion

        #region gauss elimination method
        private void CountGaussEliminationMethod()
        {
            var m = EqsMatrix.Base;
            for (var c = 0; c < Size; c++)
            {
                if (m[c, c] != 0) continue;
                var r = c + 1;
                for (; r < c; r++)
                {
                    if (m[r, c] != 0)
                    {
                        break;
                    }
                }
                if (m[r, c] != 0)
                {
                    var t = new decimal[Size + 1];
                    for (var i = 0; i < r + 1; i++)
                    {
                        t[i] = m[r, i];
                        m[r, i] = m[c, i];
                        m[c, i] = t[i];
                    }
                }
                else
                {
                    throw new Exception("Is not 0. " + m[r, c]);
                }
            }

            for (var sr = 0; sr < Size - 1; sr++)
            {
                for (var dr = sr + 1; dr < Size; dr++)
                {
                    var dv = m[sr, sr];
                    var sv = m[dr, sr];
                    for (var i = 0; i < Size + 1; i++)
                    {
                        m[dr, i] = m[dr, i] * dv - m[sr, i] * sv;
                    }
                }
            }
            for (var r = Size - 1; r >= 0; r--)
            {
                var v = m[r, r];
                if (v == 0)
                {
                    throw new Exception("v is  0 for row&column = " + r);
                }
                for (var i = 0; i < Size; i++)
                {
                    m[r, i] /= v;
                }
                for (var dr = 0; dr < r; dr++)
                {
                    m[dr, Size] -= m[dr, r] * m[r, Size];
                    m[dr, r] = 0;
                }
            }
            EqsMatrix = new Matrix(m);
        }
        #endregion

        public Dictionary<char, decimal> GetResults()
        {
            var list = new Dictionary<char, decimal>();
            switch (_solvingType)
            {
                case SolvingType.Determinants:
                    for (var i = 0; i < Size; i++)
                    {
                        list.Add(_literals[i], OtherDeterminants[i] / MainDeterminant);
                    }
                    break;
                case SolvingType.GaussElimination:
                    for (var i = 0; i < Size; i++)
                    {
                        list.Add(_literals[i], EqsMatrix.Base[i, Size]);
                    }
                    break;
            }
            return list;
        }

        private Matrix EqsMatrix { get; set; }
        private Eq[] Eqs { get; set; }

        private int Size => Eqs.Count();

        private decimal MainDeterminant { get; set; }
        private decimal[] OtherDeterminants { get; set; }
    }
}

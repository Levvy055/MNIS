using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNiSLab2
{
    public class EqSystem
    {
        public EqSystem(Eq[] eqs)
        {
            Eqs = eqs;
        }

        public double[] GetResults()
        {
            var s=Eqs.Count();
            double[,] varss = new double[s,s];
            for (var i = 0; i < s; i++)
            {
                var vs=Eqs[i].Vars;
                for (var j = 0; j < s; j++)
                {
                    varss[i, j] = vs[j];
                }
            }
            var mW = new Matrix(varss);
            W = mW.Determinant();
            return null;
        }

        private Eq[] Eqs { get; set; }
        private double W { get; set; }
    }
}

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
                
            }
            var mW = new Matrix(varss);
            W = mW.Determinant();
            Debug.WriteLine(W);
            return null;
        }

        private Eq[] Eqs { get; set; }
        private double W { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNiSLab2
{
    public class Eq
    {
        public double[] Vars { get; private set; }
        public Eq(double[] vars)
        {
            Vars = vars;
        }

        public static double[] GetVars(string txt)
        {
            var list = new List<double>();
            if (!txt.Contains('='))
            {
                return null;
            }
            var splitted = txt.Split(new char[] { '+', '-', '=' });
            for (var i = 0; i < splitted.Count(); i++)
            {
                var v = splitted[i];
                var d = 0d;
                if (double.TryParse(v, out d))
                {
                    list.Add(d);
                }
                else if (v.Length > 1)
                {
                    v = v.Substring(0, v.Length - 1);
                    if (double.TryParse(v, out d))
                    {
                        list.Add(d);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return list.ToArray();
        }
    }
}

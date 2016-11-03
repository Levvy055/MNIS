using System.Collections.Generic;
using System.Linq;

namespace EquationsView
{
    public class Eq
    {
        public double[] Vars { get; private set; }
        public Eq(double[] vars)
        {
            Vars = vars;
        }

        public static double[] GetVars(string txt, out char[] literals)
        {
            var list = new List<double>();
            var splitted = GetSplitted(txt);
            if (splitted == null)
            {
                literals = null;
                return null;
            }
            var literalsList = new List<char>();
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
                    var v1 = v.Substring(0, v.Length - 1);
                    if (double.TryParse(v1, out d))
                    {
                        literalsList.Add(v[v.Length - 1]);
                        list.Add(d);
                    }
                    else if (v[0] == '-'&&v.Length==2)
                    {
                        literalsList.Add(v[1]);
                        list.Add(-1);
                    }
                    else
                    {
                        literals = null;
                        return null;
                    }
                }
                else
                {
                    char l;
                    if (char.TryParse(v, out l))
                    {
                        literalsList.Add(l);
                        list.Add(1);
                    }
                    else
                    {
                        literals = null;
                        return null;
                    }
                }
            }
            literals = literalsList.ToArray();
            return list.ToArray();
        }

        private static string[] GetSplitted(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt) || !txt.Contains('='))
            {
                return null;
            }
            if (txt.Contains("-"))
            {
                txt = txt.Replace("-", "+-");
            }
            var res = txt.Split(new char[] { '+', '=' });
            var splitted = new List<string>();
            foreach (var s in res)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    splitted.Add(s);
                }
            }
            res = splitted.ToArray();
            return res;
        }
    }
}

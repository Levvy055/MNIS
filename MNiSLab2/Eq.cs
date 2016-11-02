﻿using System.Collections.Generic;
using System.Linq;

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
            var splitted = GetSplitted(txt);
            if (splitted == null)
            {
                return null;
            }
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

        private static string[] GetSplitted(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)||!txt.Contains('='))
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

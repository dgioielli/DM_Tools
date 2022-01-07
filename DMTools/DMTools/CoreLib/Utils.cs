using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools.CoreLib
{
    public static  class Utils
	{

		public static string SeparadorPadrao { get; set; }
		public static string SeparadorErrado { get; set; }

		public static double ToDouble(this string txt)
		{
			if (SeparadorPadrao == null || SeparadorPadrao == "")
				defineSeparador();
			double temp = 0;
			if (txt.Contains(SeparadorPadrao) && txt.Contains(SeparadorErrado))
			{
				int nP = txt.IndexOf(SeparadorPadrao), nE = txt.IndexOf(SeparadorErrado);
				if (nP > nE)
					txt = txt.Replace(SeparadorErrado, "");
				else txt = txt.Replace(SeparadorPadrao, "");
			}
			if (txt.Contains(SeparadorErrado))
				txt = txt.Replace(SeparadorErrado, SeparadorPadrao);
			if (double.TryParse(txt, out temp))
				return temp;
			return temp;
		}

		private static void defineSeparador()
		{
			string txt = (1.01).ToString("n2");
			if (txt.Contains(","))
			{ SeparadorPadrao = ","; SeparadorErrado = "."; }
			else if (txt.Contains("."))
			{ SeparadorPadrao = "."; SeparadorErrado = ","; }
		}

		public static bool ToBool(this string str)
		{
			return true.ToString() == str;
		}

		public static int ToInt(this string str)
		{
			return (int)str.ToDouble();
		}
	}
}

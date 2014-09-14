using System;
using System.Collections.Generic;

namespace SharedPCL
{
	public class HelperFunctions
	{
		public HelperFunctions ()
		{
			
		}

		public static List<T> Shuffle<T>(List<T> listToShuffle)
		{
			Random rng = new Random ();
			int n = listToShuffle.Count;

			while (n > 1) {
				n--;
				int k = rng.Next (n + 1);
				T value = listToShuffle [k];
				listToShuffle [k] = listToShuffle [n];
				listToShuffle [n] = value;
			}

			return listToShuffle;
		}
	}
}


using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
	public class QuickSortRandomPivot<T> : QuickSort<T> where T : IComparable
	{
		protected override T GetPivot(List<T> elementsToSort, int start, int end)
		{
			Random random = new Random ();
			int pivotIndex = random.Next (start, end);
			Swap (elementsToSort, start, pivotIndex);	//Swap pivot to start position 
			return elementsToSort [start];
		}
	}
}


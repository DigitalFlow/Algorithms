using System;
using Algorithms.Sorting;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
	public class QuickSortFirstElementPivot<T> : QuickSort<T> where T : IComparable
	{
		protected override T GetPivot(List<T> elementsToSort, int start, int end)
		{
			return elementsToSort [start];
		}
	}
}


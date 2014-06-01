using System;
using System.Linq;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
	public class QuickSortMedianOfThreePivot<T> : QuickSort<T> where T : IComparable
	{
		protected override T GetPivot(List<T> elementsToSort, int start, int end)
		{
			Random random = new Random ();
			var threeRandomElements = new SortedDictionary<T, int> ();	//Use a sorted dictionary, median will be on position 1 since we have position 0, 1 and 2
			while(threeRandomElements.Count < 3)
			{
				int randomIndex = random.Next (start, end);
				if(!threeRandomElements.ContainsValue(randomIndex))
				{
					threeRandomElements.Add (elementsToSort[randomIndex], randomIndex);
				}
			}
			int pivotIndex = threeRandomElements.Values.ElementAt(1); 		//Get the index of the median
			Swap (elementsToSort, start, pivotIndex);						//Swap pivot to start position
			return elementsToSort [pivotIndex];
		}
	}
}

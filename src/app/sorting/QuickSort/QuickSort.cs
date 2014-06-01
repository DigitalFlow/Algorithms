using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
	public abstract class QuickSort<T> where T : IComparable
	{
		public List<T> Sort(List<T> elementsToSort)
		{
			return Sort (elementsToSort, 0, elementsToSort.Count-1);
		}

		protected abstract T GetPivot (List<T> elementsToSort, int start, int end);

		protected List<T> Sort(List<T> elementsToSort, int start, int end)
		{
			if (start < end) 
			{
				int leftRunner = start;
				int rightRunner = end;
				T pivot = GetPivot(elementsToSort, start, end);

				while (leftRunner <= rightRunner) {
					while (leftRunner <= rightRunner && elementsToSort [leftRunner].CompareTo(pivot) <= 0) {	//Move from the begining to the rightRunner until an element is larger than pivot
						leftRunner++;
					}
					while (leftRunner <= rightRunner && elementsToSort [rightRunner].CompareTo(pivot) > 0) {	//Move from the end to the leftRunner until an element is smaller or equal to the pivot
						rightRunner--;
					}
					if (leftRunner < rightRunner) {
						Swap (elementsToSort, leftRunner, rightRunner);
					}
				}

				if (start < rightRunner) {
					Swap (elementsToSort, start, rightRunner);
					Sort (elementsToSort, start, rightRunner - 1);
				}

				if (rightRunner < end) {
					Sort (elementsToSort, rightRunner + 1, end);
				}
			}
			return elementsToSort;
		}

		protected void Swap (List<T> elementsToSort, int firstIndex, int secondIndex)
		{
			T tempElement = elementsToSort[firstIndex];
			elementsToSort [firstIndex] = elementsToSort[secondIndex];
			elementsToSort [secondIndex] = tempElement;
		}
	}
}


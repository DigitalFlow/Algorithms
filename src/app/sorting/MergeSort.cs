using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
	public class MergeSort<T> where T : IComparable
	{
		public List<T> Sort(List<T> elementsToSort)
		{
			return Sort (elementsToSort, new List<T>(elementsToSort), 0, elementsToSort.Count - 1);
		}

		private List<T> Sort(List<T> inputList, List<T> tempList, int start, int end)
		{
			if (start < end) {
				int median = CalculateMedian (start, end);
				Sort (inputList, tempList, start, median);
				Sort (inputList, tempList, median + 1, end);
				var leftIndex = start;
				var rightIndex = median + 1;
				var elementIndex = start;
				while (leftIndex <= median && rightIndex <= end) {
					if (inputList [leftIndex].CompareTo(inputList [rightIndex]) <= 0) {
						tempList[elementIndex++] = inputList [leftIndex++];
					} else {
						tempList[elementIndex++] = inputList [rightIndex++];
					}
				}
				for (int index = leftIndex; index <= median; index++) {
					var position = elementIndex + (index - leftIndex);
					inputList [position] = inputList [index];
				}
				for (int index = start; index < elementIndex; index++) {
					inputList [index] = tempList [index];
				}
			}
			return inputList;
		}

		private int CalculateMedian (int start, int end)
		{	
			return (int)Math.Ceiling(start + end - 1.0) / 2;
		}
	}
}


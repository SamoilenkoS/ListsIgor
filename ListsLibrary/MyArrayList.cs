using System;
using System.Collections.Generic;
using System.Text;

namespace ListsLibrary
{
    public class MyArrayList : IList
    {
        private const int DefaultSize = 4;
        private int[] _array;
        private int _currentCount;

        public int Capacity => _array.Length;
        public int Length => _currentCount;//orient on it while going through elements

        public int this[int index]//protection from index out of range
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public MyArrayList()
        {
            _array = new int[DefaultSize];
        }

        public MyArrayList(int capacity)
        {
            _array = new int[capacity];
        }

        public MyArrayList(int[] array)
        {
            _array = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                _array[i] = array[i];
            }
            _currentCount = array.Length;
        }

        public void AddFront(int value)
        {
            AddByIndex(0, value);
        }

        public void AddBack(int value)
        {
            AddByIndex(_array.Length, value);
        }

        public void AddByIndex(int index, int value)
        {
            int[] arrayTemp = new int[] { value };
            AddByIndex(index, arrayTemp);
        }

        public void RemoveFrontElement()
        {
            RemoveByIndexNElements(0, 1);
        }

        public void RemoveBackElement()
        {
            RemoveByIndexNElements(_array.Length - 1, 1);
        }

        public void RemoveByIndexElement(int index)
        {
            RemoveByIndexNElements(index, 1);
        }

        public void RemoveFrontNElements(int value)
        {
            RemoveByIndexNElements(0, value);
        }

        public void RemoveBackNElements(int value)
        {
            RemoveByIndexNElements(_array.Length - value, value);
        }

        public void RemoveByIndexNElements(int index, int value)
        {
            int removeEndElement = index + value;

            if (removeEndElement <= _array.Length)
            {
                int[] arrayTemp = new int[_array.Length - value];
                int j = 0;
                for (int i = 0; i < _array.Length; i++)
                {
                    if (i < index || i >= removeEndElement)
                    {
                        arrayTemp[j++] = _array[i];
                    }
                }

                _array = arrayTemp;
            }
            else
            {
                throw new ArgumentException("Too much value");
            }
        }

        public int FirstIndexByValue(int value)
        {
            int index = 0;
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == value)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public void Reverse()
        {
            int[] arrayTemp = new int[_array.Length];
            int j = arrayTemp.Length - 1;

            for (int i = 0; i < _array.Length; i++)
            {
                arrayTemp[j] = _array[i];
                j--;
            }

            _array = arrayTemp;
        }

        public int GetMaxElementValue()
        {
            int max = _array[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (max < _array[i])
                {
                    max = _array[i];
                }
            }

            return max;
        }

        public int GetMinElementValue()
        {
            int min = _array[0];
            for (int i = 0; i < _array.Length; i++)
            {
                if (min > _array[i])
                {
                    min = _array[i];
                }
            }

            return min;
        }

        public int GetMaxElementIndex()
        {
            return FirstIndexByValue(GetMaxElementValue());
        }

        public int GetMinElementIndex()
        {
            return FirstIndexByValue(GetMinElementValue());
        }

        public void Sort(bool ascending = true)//CompareTo()
        {
            int[] arrayTemp = new int[_array.Length];
            int j;
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                if (ascending == true)
                {
                    j = GetMinElementIndex();
                }
                else
                {
                    j = GetMaxElementIndex();
                }

                arrayTemp[i] = _array[j];
                RemoveByIndexElement(j);
            }

            _array = arrayTemp;
        }

        //public void SortAscending()
        //{
        //    Sort(true);
        //}

        //public void SortDescending()
        //{
        //    Sort(false);
        //}

        public int DeleteByValueFirst(int value)
        {
            int index = FirstIndexByValue(value);
            RemoveByIndexElement(index);

            return index;
        }

        public int DeleteByValueAll(int value)
        {
            int count = 0;
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == value)
                {
                    count++;
                }
            }
            for (int i = 0; i < count; i++)
            {
                DeleteByValueFirst(value);
            }

            return count;
        }

        public void AddFrontArray(int[] array)
        {
            AddByIndex(0, array);
        }

        public void AddBackArray(int[] array)
        {
            AddByIndex(_array.Length, array);
        }

        public void AddByIndex(int index, int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array is null");
            }

            int[] arrayTemp = new int[_array.Length + array.Length];
            int addArrayEnd = index + array.Length - 1;
            int oldArrayCount = 0;
            int addArrayCount = 0;

            for (int i = 0; i < arrayTemp.Length; i++)//remove if/else from code
            {
                if (i >= index && i <= addArrayEnd)
                {
                    arrayTemp[i] = array[addArrayCount++];
                }
                else
                {
                    arrayTemp[i] = _array[oldArrayCount++];
                }
            }

            _array = arrayTemp;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

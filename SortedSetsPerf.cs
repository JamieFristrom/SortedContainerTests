using System;
using System.Collections.Generic;
using System.Diagnostics;

struct ValueStruct
{
    public int int1;
    public int int2, int3, int4;//,int5,int6,int7,int8;
}

class NonvalueClass
{
    public int int1;
    public int int2, int3, int4;//,int5,int6,int7,int8;
}


namespace SortedContainerTests
{

    class MyStructComparer : IComparer<ValueStruct>
    {
        public int Compare(ValueStruct x, ValueStruct y)
        {
            return x.int1.CompareTo(y.int1);
        }
    }

    class MyClassComparer : IComparer<NonvalueClass>
    {
        public int Compare(NonvalueClass x, NonvalueClass y)
        {
            return x.int1.CompareTo(y.int1);
        }
    }


    class Program
    {
        const int n = 250000;

        static void Main(string[] args)
        {
            var sortedDictIntKey = new SortedDictionary<int, ValueStruct>();
            var sortedListIntKey = new SortedList<int, ValueStruct>();

            var sortedDictIntKeyCl = new SortedDictionary<int, NonvalueClass>();
            var sortedListIntKeyCl = new SortedList<int, NonvalueClass>();

            var sortedSetStruct = new SortedSet<ValueStruct>(new MyStructComparer());
            var sortedSetClass = new SortedSet<NonvalueClass>(new MyClassComparer());

            var savedKeys = new List<int>();

            var mySortedSetList = new List<ValueStruct>();
            var mySortedSetListClass = new List<NonvalueClass>();

            var myComparer = new MyStructComparer();
            var myClassComparer = new MyClassComparer();

            // save some random keys just to make 100% sure our test sets are the same; theoretically unnecessary
            Random myRandom = new Random(1);
            Stopwatch ticker;
            for (int i = 0; i < 400000; i++)
            {
                int randomKey = myRandom.Next() % 1000000;
                savedKeys.Add(randomKey);
            }

            for( int n = 5; n <= 200000; n *= 2 )
            {
                Console.Write( n + ", " );
                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    if (!sortedListIntKey.ContainsKey(randomKey))
                        sortedListIntKey.Add(randomKey, new ValueStruct() { int1 = i, int2 = randomKey });
                }
                ticker.Stop();
                //Console.WriteLine("Sorted Int key struct List: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                int accumulator;
                int lastval;
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                var sortedListValuesStruct = sortedListIntKey.Values;
                for( int i=0; i<sortedListValuesStruct.Count ; i++ )
                {
                    int nextVal = sortedListValuesStruct[i].int2;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;
                }
                ticker.Stop();
                Console.Write( ticker.ElapsedTicks + ", " );
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                sortedListIntKey.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    if (!sortedListIntKeyCl.ContainsKey(randomKey))
                        sortedListIntKeyCl.Add(randomKey, new NonvalueClass() { int1 = i, int2 = randomKey });
                }
                ticker.Stop();
//                Console.WriteLine("Sorted Int key class List: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                var sortedListValuesClass = sortedListIntKeyCl.Values;
                for( int i=0; i<sortedListValuesClass.Count; i++ )
                {
                    int nextVal = sortedListValuesClass[i].int2;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                sortedListIntKeyCl.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    if (!sortedDictIntKey.ContainsKey(randomKey))
                        sortedDictIntKey.Add(randomKey, new ValueStruct() { int1 = i, int2 = randomKey });
                }
                ticker.Stop();
//                Console.WriteLine("Sorted Int key struct Dictionary: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                foreach (var iter in sortedDictIntKey)
                {
                    int nextVal = iter.Value.int2;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;
                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                sortedDictIntKey.Clear();
                GC.Collect();

                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    if (!sortedDictIntKeyCl.ContainsKey(randomKey))
                        sortedDictIntKeyCl.Add(randomKey, new NonvalueClass() { int1 = i, int2 = randomKey });
                }
                ticker.Stop();
//                Console.WriteLine("Sorted Int key class Dictionary: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                foreach (var iter in sortedDictIntKeyCl)
                {
                    int nextVal = iter.Value.int2;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                sortedDictIntKeyCl.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    var item = new ValueStruct() { int1 = randomKey, int2 = i };
                    int idx = mySortedSetList.BinarySearch(item, myComparer);
                    if (idx < 0)
                        mySortedSetList.Insert( ~idx, item );
                }
                ticker.Stop();
//                Console.WriteLine("My hand sorted Set with structs: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );

                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                for( int i=0; i< mySortedSetList.Count; i++ )
//                foreach (var iter in mySortedSetList)
                {
                    int nextVal = mySortedSetList[i].int1;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                mySortedSetList.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    var item = new NonvalueClass() { int1 = randomKey, int2 = i };
                    int idx = mySortedSetListClass.BinarySearch(item, myClassComparer);
                    if (idx < 0)
                        mySortedSetListClass.Insert(~idx, item);
                }
                ticker.Stop();
//                Console.WriteLine("My hand sorted Set with classes: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                for( int i=0; i< mySortedSetListClass.Count; i++ )
//                foreach (var iter in mySortedSetListClass)
                {
                    int nextVal = mySortedSetListClass[i].int1;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                mySortedSetListClass.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    var newItem = new ValueStruct() { int1 = randomKey, int2 = i };
                    if (!sortedSetStruct.Contains(newItem))
                        sortedSetStruct.Add(newItem);
                }
                ticker.Stop();
//                Console.WriteLine("C# Sorted Set with structs: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                foreach (var iter in sortedSetStruct)
                {
                    int nextVal = iter.int1;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                sortedSetStruct.Clear();
                GC.Collect();


                ticker = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    int randomKey = savedKeys[i];
                    var newItem = new NonvalueClass() { int1 = randomKey, int2 = i };
                    if (!sortedSetClass.Contains(newItem))
                        sortedSetClass.Add(newItem);
                }
                ticker.Stop();
//                Console.WriteLine("C# Sorted Set with classes: " + ticker.ElapsedTicks);
                Console.Write( ticker.ElapsedTicks + ", " );
                ticker = Stopwatch.StartNew();
                accumulator = 0;
                lastval = Int32.MinValue;
                foreach (var iter in sortedSetClass)
                {
                    int nextVal = iter.int1;
                    Debug.Assert(nextVal > lastval);
                    accumulator += nextVal;
                    lastval = nextVal;

                }
                ticker.Stop();
//                Console.WriteLine("Iteration time: " + ticker.ElapsedTicks + " check: " + accumulator);
                Console.Write( ticker.ElapsedTicks + ", " );
                sortedSetClass.Clear();
                GC.Collect();
                
                Console.WriteLine();
            }
        }
    }
}

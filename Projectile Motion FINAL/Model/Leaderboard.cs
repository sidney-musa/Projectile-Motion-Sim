using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Projectile_Motion_FINAL.Model
{
    internal class Leaderboard
    {
       private List<double> dataList = [];
       private double first;
        private double second;
        private double third;
        private double fourth;
        private double fifth;

        public Leaderboard() 
        {
            DataList = dataList;
            first = 0;
            second = 0;
            third = 0;
            fourth = 0;
            fifth = 0;
        }
        public List<double> DataList { get => dataList; set => dataList = value; }
        public double First { get => first; set => first = value; }
        public double Second { get => second; set => second = value; }
        public double Third { get => third; set => third = value; }
        public double Fourth { get => fourth; set => fourth = value; }
        public double Fifth { get => fifth; set => fifth = value; }

     

        public List<double> AddItems() // reading ranges from store.txt
        {
            string fileName = @"C:\comp sci\Projectile Motion FINAL\Projectile Motion FINAL\Model\Store.txt";
            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    while (streamReader.EndOfStream == false)
                    {
                        string line = streamReader.ReadLine();
                        DataList.Add(Convert.ToDouble(line));
                    }
                    
                }    
            }
            return DataList;
        }
     
        List<double> firstHalf = [];
        List<double> secondHalf = [];
        List<double> merged = [];
        List<double> store = [];
        int mergedIndex = 0;

        /// <summary>
        /// merges the items in firstHalf and secondHalf passed from Divide
        /// </summary>
      
        List<double> Merge(List<double> firstHalf, List<double> secondHalf)
        {
            try
            {
                int left = 0;//index of firstHalf
                int right = 0;//index of secondHalf
                mergedIndex = 0;//index of merged

                while (left < firstHalf.Count && right < secondHalf.Count)//while there is still data in both lists to be sorted
                {
                    if (firstHalf.ElementAt(left) < secondHalf.ElementAt(right)) // firstHalf is smaller than secondHalf
                    {
                        if (merged.Count == 0)//in the case that this is the first value to be added
                        {
                            merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                            left++;
                            mergedIndex++;
                        }
                        else
                        {
                            if (firstHalf.ElementAt(left) < merged.ElementAt(mergedIndex))// if its the smallest number so far
                            {
                                merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                                left++;
                                mergedIndex++;
                            }
                            else
                            {
                                if (firstHalf.ElementAt(left) >= merged.ElementAt(merged.Count - 1))// if its the biggest number so far
                                {
                                    mergedIndex++;
                                    merged.Add(firstHalf.ElementAt(left));
                                    left++;
                                }
                                else
                                {
                                    for (mergedIndex = 0; mergedIndex < merged.Count - 1; mergedIndex++) //loops through list until correct index is found
                                    {
                                        if (firstHalf.ElementAt(left) < merged.ElementAt(mergedIndex))
                                        {
                                            merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                                            mergedIndex = merged.Count;
                                            left++;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else if (firstHalf.ElementAt(left) > secondHalf.ElementAt(right)) //secondHalf is smaller than firstHalf
                    {
                        if (merged.Count == 0) //in the case that this is the first value to be added
                        {
                            merged.Insert(mergedIndex, secondHalf.ElementAt(right));
                            right++;
                            mergedIndex++;
                            store.RemoveAt(store.Count - 1);

                        }
                        else
                        {
                            if (secondHalf.ElementAt(right) < merged.ElementAt(mergedIndex))//if its the smallest to be added so far
                            {
                                merged.Insert(mergedIndex, secondHalf.ElementAt(right));
                                right++;
                                mergedIndex++;
                                store.RemoveAt(store.Count - 1);
                            }
                            else
                            {
                                if (secondHalf.ElementAt(right) >= merged.ElementAt(merged.Count - 1))//if its the biggest to be added so far
                                {
                                    mergedIndex++;
                                    merged.Add(secondHalf.ElementAt(right));
                                    right++;
                                    store.RemoveAt(store.Count - 1);

                                }
                                else
                                {
                                    for (mergedIndex = 0; mergedIndex < merged.Count - 1; mergedIndex++) //loops through list until correct index is found
                                    {
                                        if (secondHalf.ElementAt(right) < merged.ElementAt(mergedIndex))
                                        {
                                            merged.Insert(mergedIndex, secondHalf.ElementAt(right));
                                            mergedIndex = merged.Count;
                                            right++;
                                            store.RemoveAt(store.Count - 1);
                                        }

                                    }

                                }
                            }
                        }

                    }
                    else if (firstHalf.ElementAt(left) == secondHalf.ElementAt(right)) // firstHalf is the same as secondHalf
                    {
                        if (merged.Count == 0)//in the case that this is the first value to be added
                        {
                            merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                            merged.Insert(mergedIndex + 1, secondHalf.ElementAt(right));
                            left++;
                            right++;
                            mergedIndex++;
                            store.RemoveAt(store.Count - 1);
                        }
                        else
                        {
                            if (firstHalf.ElementAt(left) < merged.ElementAt(mergedIndex))// if its the smallest number so far
                            {
                                merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                                merged.Insert(mergedIndex + 1, secondHalf.ElementAt(right));
                                left++;
                                right++;
                                mergedIndex++;
                                store.RemoveAt(store.Count - 1);
                            }
                            else
                            {
                                if (firstHalf.ElementAt(left) > merged.ElementAt(merged.Count - 1))// if its the biggest number so far
                                {
                                    mergedIndex = merged.Count;
                                    merged.Add(firstHalf.ElementAt(left));
                                    merged.Add(secondHalf.ElementAt(right));
                                    left++;
                                    right++;
                                    mergedIndex++;
                                    store.RemoveAt(store.Count - 1);
                                }
                                else
                                {
                                    for (mergedIndex = 0; mergedIndex < merged.Count - 1; mergedIndex++) //loops through list until correct index is found
                                    {
                                        if (firstHalf.ElementAt(left) < merged.ElementAt(mergedIndex))
                                        {
                                            merged.Insert(mergedIndex, firstHalf.ElementAt(left));
                                            merged.Insert(mergedIndex + 1, secondHalf.ElementAt(right));
                                            left++;
                                            right++;
                                            mergedIndex++;
                                            store.RemoveAt(store.Count - 1);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                while (left < firstHalf.Count) // checks if any items left in firstHalf
                {
                    store.Add(firstHalf.ElementAt(left)); //adds them to store
                    left++;
                    mergedIndex++;
                }
                while (right < secondHalf.Count) // checks if any items left in secondHalf
                {
                    store.Add(secondHalf.ElementAt(right)); //adds them to store
                    right++;
                    mergedIndex++;
                }

                return merged;
            }
            catch (Exception) // ensures the whole program doesnt crash if exception raised in leaderboard
            {
                merged = [];
                first = 0;
                second = 0;
                third = 0;
                fourth = 0;
                fifth = 0;
                return merged;
            }
           
        }

        /// <summary>
        /// Recursive algortithm that splits the list in half until there is a pair in each half to be sorted
        /// </summary>
        public List<double> Divide(List<double> dataList) 
        { 
            while (dataList.Count != 0) //checks if there is anything left to be sorted
            {

                if (dataList.Count == 1) //checks if there is a pair to be merged
                {
                    return Merge(firstHalf, secondHalf);//calls merge on pair
                }
                else
                {
                    firstHalf = [];// empties list on each call of divide

                    if (store == dataList) // checks if the stored values are copied into dataList to be divided
                    {
                        store = [];// if store has copied into dataList, emptied
                    }
                    secondHalf = []; // empties list on call of divide

                    int halved = dataList.Count / 2;// finds middle of list

                    for (int i = 0; i < halved; i++)//creates list of firstHalf
                    {
                        double aaa = dataList[i];
                        firstHalf.Add(aaa);

                    }
                    for (int i = halved; i < dataList.Count; i++)// creates list of secondHalf
                    {
                        double bbb = dataList[i];

                        secondHalf.Add(bbb);
                        store.Add(bbb);

                    }
                    dataList = Divide(firstHalf);// sets dataList to firstHalf and calls divide on it
                    dataList = Divide(store);//sets dataList to store and calls divide on it

                }
            }
            if (merged.Count > 5)// setting values for databinding
            {
                First = merged.ElementAt(0);
                Second = merged.ElementAt(1);
                Third = merged.ElementAt(2);
                Fourth = merged.ElementAt(3);
                Fifth = merged.ElementAt(4);
                return merged;
            }
            else
            {
                return merged;
            }
        }
       
    }
}


    


using System.Collections.Generic;


namespace SSAx.PrimitiveExtensions
{

    public static class ArrayExtensions
    {
        //The following code is drawn from a posting by Anthony Pegram (https://stackoverflow.com/users/414076/anthony-pegram) 
        //posted to Stack Overflow (https://stackoverflow.com/questions/3319586/getting-all-possible-combinations-from-a-list-of-numbers/3319689). 
        //Per Stack Exchange’s Terms of Service, all user contributions to Stack Overflow are licensed under the Creative Commons Attribution ShareAlike 3.0 License (CC-BY-SA 3.0), available at https://creativecommons.org/licenses/by-sa/3.0/legalcode .”
        /// <summary>
        /// This method can be called on an array.
        /// It returns subset of an array as a list of array. (for {a,b} it returns {a,b},{a},{b})
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray"></param>
        /// <returns></returns>
        public static List<T[]> CreateSubsets<T>(this T[] originalArray)
        {
            List<T[]> subsets = new List<T[]>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                int subsetCount = subsets.Count;
                subsets.Add(new T[] { originalArray[i] });

                for (int j = 0; j < subsetCount; j++)
                {
                    T[] newSubset = new T[subsets[j].Length + 1];
                    subsets[j].CopyTo(newSubset, 0);
                    newSubset[newSubset.Length - 1] = originalArray[i];
                    subsets.Add(newSubset);
                }
            }
            
            return subsets;
        }
    }
}

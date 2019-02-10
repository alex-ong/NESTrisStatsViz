using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Extensions
    {

        public static List<T> LastN<T>(this List<T> list, int n)
        {
            int count = list.Count;
            int amount = Mathf.Min(n, count);
            int startIndex = list.Count - amount;
            return list.GetRange(startIndex, amount);
        }
    }
}
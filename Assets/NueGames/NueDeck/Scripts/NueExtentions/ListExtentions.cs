using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


namespace NueGames.NueDeck.Scripts.NueExtentions
{
    public static class ListExtentions
    {
        /// <summary>
        ///     Returns a random item from inside the
        ///     <typeparam name="T">List</typeparam>
        ///     >
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this List<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("List is Empty");

            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

#if UNITY_EDITOR
        
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:"+ typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for(int i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
 
            return a;
 
        }
#endif

        /// <summary>
        ///     Returns and Remove a random item from inside the
        ///     <typeparam name="T">List</typeparam>
        ///     >
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItemRemove<T>(this List<T> list)
        {
            var item = list.RandomItem();
            list.Remove(item);
            return item;
        }

        /// <summary>
        ///     Shuffles the List using Fisher Yates algorithm: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle.
        /// </summary>
        public static void Shuffle<T>(this List<T> list)
        {
            var n = list.Count;
            for (var i = 0; i <= n - 2; i++)
            {
                //random index
                var rdn = Random.Range(0, n - i);

                //swap positions
                (list[i], list[i + rdn]) = (list[i + rdn], list[i]);
            }
        }

        /// <summary>
        ///     Adds an item at the beginning of the List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddToFront<T>(this List<T> list, T item) => list.Insert(0, item);

        /// <summary>
        ///     Add an item in before another item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="newItem"></param>
        public static void AddBeforeOf<T>(this List<T> list, T item, T newItem)
        {
            var targetPosition = list.IndexOf(item);
            list.Insert(targetPosition, newItem);
        }

        /// <summary>
        ///     Add an item in after another item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="newItem"></param>
        public static void AddAfterOf<T>(this List<T> list, T item, T newItem)
        {
            var targetPosition = list.IndexOf(item) + 1;
            list.Insert(targetPosition, newItem);
        }

        /// <summary>
        ///     Prints the list in the following format: [item[0], item[1], ... , item[Count-1]]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Print<T>(this List<T> list, string log = "")
        {
            log += "[";
            for (var i = 0; i < list.Count; i++)
            {
                log += list[i].ToString();
                log += i != list.Count - 1 ? ", " : "]";
            }

            Debug.Log(log);
        }
    }
}
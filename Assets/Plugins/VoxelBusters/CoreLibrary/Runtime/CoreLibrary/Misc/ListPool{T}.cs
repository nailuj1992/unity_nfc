using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.CoreLibrary
{
    public static class ListPool<T>
    {
        #region Static fields

        private     static  readonly    ObjectPool<List<T>>     s_listPool      = new ObjectPool<List<T>>(createFunc: OnCreateItem, actionOnGet: null, actionOnAdd: OnReleaseItem);

        #endregion

        #region Static methods

        public static List<T> Get()
        {
            return s_listPool.Get();
        }

        public static void Release(List<T> obj)
        {
            s_listPool.Add(obj);
        }

        private static List<T> OnCreateItem()
        {
            return new List<T>(capacity: 8);
        }

        private static void OnReleaseItem(List<T> item)
        {
            item.Clear();
        }

        #endregion
    }
}
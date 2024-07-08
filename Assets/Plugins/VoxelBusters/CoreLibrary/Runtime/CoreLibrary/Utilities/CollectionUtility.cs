using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.CoreLibrary
{
    public static class CollectionUtility
    {
        #region Extension methods

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return (list == null) || (list.Count == 0);
        }

        public static bool AddUnique<T>(this IList<T> list, T item)
        {
            if (null == list) return false;

            if (!list.Contains(item))
            {
                list.Add(item);
                return true;
            }
            return false;
        }

		public static IList<T> Add<T>(this IList<T> list, System.Func<bool> condition, System.Func<T> getItem)
		{
			if ((list != null) && condition())
			{
				list.Add(getItem());
			}
			return list;
		}

        public static T GetItemAt<T>(this IList<T> list, int index, bool throwError = true)
        {
			// Check whether item is within bounds
			if (throwError || ((index >= 0) && (index < list.Count)))
			{
				return list[index];
			}
			return default(T);
        }

		public static void AddFirst<T>(this IList<T> list, T item)
		{
			list.Insert(0, item);
		}

		public static void AddLast<T>(this IList<T> list, T item)
		{
			list.Add(item);
		}

		public static T PopFirst<T>(this IList<T> list)
		{
			var		item	= list[0];
			list.RemoveAt(0);

			return item;
		}

		public static T PopLast<T>(this IList<T> list)
		{
			int		lastIndex	= list.Count - 1;
			var		item		= list[lastIndex];
			list.RemoveAt(lastIndex);

			return item;
		}

        #endregion

        #region IDictionary methods

        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return (dict == null) || (dict.Count == 0);
        }

        public static bool ContainsKeyPath(this IDictionary dictionary, string keyPath)
		{
			if (string.IsNullOrEmpty(keyPath)) return false;
		
			try
			{
				var		pathComponents	= keyPath.Split('/');
				int		count			= pathComponents.Length;
				var		currentDict		= dictionary;

				for (int pIter = 0; pIter < count; pIter++)
				{
					string	key		= pathComponents[pIter];
					if (currentDict == null || !currentDict.Contains(key))
					{
						return false;
					}

					// Update reference to object at current key path
					currentDict		= currentDict[key] as IDictionary;
				}
				return true;
			}
			catch (System.Exception exception)
			{
				Debug.LogWarning($"[CoreLibrary] {exception.Message}");
				return false;
			}
		}
	
		public static T GetIfAvailable<T>(this IDictionary dictionary, string key, T defaultValue = default(T))
		{
			if (key == null || !dictionary.Contains(key)) return defaultValue;

			object	value		= dictionary[key];
			var 	targetType	= typeof(T);

			if (value == null)
			{
				return defaultValue;
			}
			if (targetType.IsInstanceOfType(value))
			{
				return (T)value;
			}

#if !NETFX_CORE
			if (targetType.IsEnum)
#else
			if (targetType.GetTypeInfo().IsEnum)
#endif
			{
				return (T)System.Enum.ToObject(targetType, value);
			}
			else
			{
				return (T)System.Convert.ChangeType(value, targetType);
			}
		}

		public static T GetIfAvailable<T>(this IDictionary dictionary, string key, string path)
		{
			// Trim path at start
			if (path != null)
			{
				// Trim start and end slash if exists.
				path	= path.TrimStart('/').TrimEnd('/');
			}

			if (!string.IsNullOrEmpty(key))
			{
				if (string.IsNullOrEmpty(path))
				{
					return dictionary.GetIfAvailable<T>(key);
				}
				else
				{
					var		pathComponents	= path.Split('/');
					var		currentDict		= dictionary;

					// Here traverse to the path
					foreach (string each in pathComponents)
					{
						if (currentDict.Contains(each))
						{
							currentDict		= currentDict[each] as IDictionary;
						}
						else
						{
							Debug.LogError($"[CoreLibrary]Path not found. Path={path}");
							return default(T);
						}
					}
					return currentDict.GetIfAvailable<T>(key);
				}
			}
			else
			{
				return default(T);
			}
		}

		public static string GetKey<T>(this IDictionary dictionary, T value)
		{
			string	key	= null;
			if (value != null)
			{
				var		keys	= dictionary.Keys;
				foreach (string eachKey in keys)	
				{
					var		eachValue	= dictionary[eachKey] as object;
					if (eachValue != null && eachValue.Equals(value))
					{
						key		= eachKey;
						break;
					}
				}
			}

			return key;
		}

        #endregion
    }
}
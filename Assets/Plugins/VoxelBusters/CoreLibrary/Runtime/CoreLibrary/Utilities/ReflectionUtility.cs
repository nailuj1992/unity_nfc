using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace VoxelBusters.CoreLibrary
{
    public static class ReflectionUtility
    {
        #region Constants

        public  const   string  kAssemblyCSharpFirstPass    = "Assembly-CSharp-firstpass";

        public  const   string  kAssemblyCSharp             = "Assembly-CSharp";

        #endregion

        #region Type methods

		[System.Obsolete("Use method GetTypeFromAssemblyCSharp instead.", false)]
		public static Type GetTypeFromCSharpAssembly(string typeName)
		{
			return GetType(kAssemblyCSharp, typeName);
		}

		[System.Obsolete("Use method GetTypeFromAssemblyCSharp instead.", false)]
        public static Type GetTypeFromCSharpFirstPassAssembly(string typeName)
        {
			return GetType(kAssemblyCSharpFirstPass, typeName);
        }

        public static Type GetTypeFromAssemblyCSharp(string typeName, bool includeFirstPass = false)
        {
			Type	targetType		= null;
            if (includeFirstPass)
			{
				targetType			= GetType(kAssemblyCSharpFirstPass, typeName);
			};
			if (targetType == null)
			{
				targetType			= GetType(kAssemblyCSharp, typeName);
			}
			return targetType;
        }

        public static Type GetType(string assemblyName, string typeName)
        {
            var		targetAssembly	= FindAssemblyWithName(assemblyName);
            if (targetAssembly != null)
            {
                return targetAssembly.GetType(typeName, false);
            }

            return null;
        }

        public static Assembly FindAssemblyWithName(string assemblyName)
        {
            return Array.Find(AppDomain.CurrentDomain.GetAssemblies(), (item) =>
            {
                return string.Equals(item.GetName().Name, assemblyName);
            });
        }

		#endregion

		#region Create methods

		public static object CreateInstance(Type type, bool nonPublic = true)
		{
			return Activator.CreateInstance(type, nonPublic);
		}

		public static object CreateInstance(Type type, params object[] args)
		{
			return Activator.CreateInstance(type, args);
		}

        #endregion

        #region Invoke methods

        public static T InvokeStaticMethod<T>(this Type type, string method, params object[] parameters)
        {
            return (T)type.GetMethod(method, BindingFlags.Public | BindingFlags.Static).Invoke(null, parameters);
        }

        #endregion

        #region Constraints methods

        // Credits: Thomas Hourdel
        public static string GetFieldName<TInstance, TProperty>(Expression<Func<TInstance, TProperty>> fieldAccess)
		{
			var		memberExpression	= fieldAccess.Body as MemberExpression;
			if (memberExpression != null)
			{
				return memberExpression.Member.Name;
			}
			throw new InvalidOperationException("Member expression expected");
		}

		public static FieldInfo GetField(Type type, string fieldName)
		{
			return type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public static TAttribute GetAttribute<TAttribute>(FieldInfo field) where TAttribute : Attribute
		{
			return (TAttribute)Attribute.GetCustomAttribute(field, typeof(TAttribute));
		}

		// MinAttribute
		public static void ConstrainMin<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, float value)
		{
			var		fieldName	= GetFieldName(fieldAccess);
			var		fieldInfo	= GetField(typeof(TInstance), fieldName);
			fieldInfo.SetValue(instance, Mathf.Max(value, GetAttribute<MinAttribute>(fieldInfo).min));
		}

		public static void ConstrainMin<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, int value)
		{
			var		fieldName	= GetFieldName(fieldAccess);
			var		fieldInfo	= GetField(typeof(TInstance), fieldName);
			fieldInfo.SetValue(instance, (int)Mathf.Max(value, GetAttribute<MinAttribute>(fieldInfo).min));
		}

		// RangeAttribute
		public static void ConstrainRange<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, float value)
		{
			var		fieldName	= GetFieldName(fieldAccess);
			var		fieldInfo	= GetField(typeof(TInstance), fieldName);
			var		attribute	= GetAttribute<RangeAttribute>(fieldInfo);
			fieldInfo.SetValue(instance, Mathf.Clamp(value, attribute.min, attribute.max));
		}

		public static void ConstrainRange<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, int value)
		{
			var		fieldName	= GetFieldName(fieldAccess);
			var		fieldInfo	= GetField(typeof(TInstance), fieldName);
			var		attribute	= GetAttribute<RangeAttribute>(fieldInfo);
			fieldInfo.SetValue(instance, (int)Mathf.Clamp(value, attribute.min, attribute.max));
		}

		public static void ConstrainDefault<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, Func<bool> condition = null)
		{
			if ((condition != null) && !condition()) return;

			var		fieldName	= GetFieldName(fieldAccess);
			var		fieldInfo	= GetField(typeof(TInstance), fieldName);
			var		attribute	= GetAttribute<DefaultValueAttribute>(fieldInfo);
			if (attribute != null)
			{
				fieldInfo.SetValue(instance, attribute.GetValue(fieldInfo.FieldType));
			}
		}

		public static void ConstrainDefault<TInstance, TProperty>(TInstance instance, Expression<Func<TInstance, TProperty>> fieldAccess, string value)
		{
			if (!string.IsNullOrEmpty(value)) return;

			ConstrainDefault(instance, fieldAccess);
		}

        #endregion
    }
}
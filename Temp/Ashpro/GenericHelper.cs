using System;
using System.Runtime.InteropServices;

namespace Ashpro
{
	public static class GenericHelper
	{
		private interface IHasValue<T>
		{
			bool HasValue(T value);
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct StructHasValue<T> : IHasValue<T>, IHasValue<T?> where T : struct
		{
			public bool HasValue(T value)
			{
				return true;
			}

			public bool HasValue(T? value)
			{
				return value.HasValue;
			}
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct ClassHasValue<T> : IHasValue<T> where T : class
		{
			public bool HasValue(T value)
			{
				return value != null;
			}
		}

		private static class ObjectValue<T>
		{
			public static readonly IHasValue<T> Value = CreateObjectValue();

			private static IHasValue<T> CreateObjectValue()
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					Type underlyingType = Nullable.GetUnderlyingType(typeFromHandle);
					return (IHasValue<T>)Activator.CreateInstance(typeof(StructHasValue<>).MakeGenericType(underlyingType ?? typeFromHandle));
				}
				return (IHasValue<T>)Activator.CreateInstance(typeof(ClassHasValue<>).MakeGenericType(typeFromHandle));
			}
		}

		public static bool HasValue<T>(T value)
		{
			return ObjectValue<T>.Value.HasValue(value);
		}
	}
}

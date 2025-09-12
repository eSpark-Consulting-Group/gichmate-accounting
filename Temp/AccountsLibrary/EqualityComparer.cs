using System;
using System.Collections.Generic;

namespace AccountsLibrary
{
	public sealed class EqualityComparer<T, TKey> : IEqualityComparer<T>
	{
		private readonly Func<T, TKey> _keySelector;

		private readonly IEqualityComparer<TKey> _keyComparer;

		public EqualityComparer(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			_keySelector = keySelector;
			_keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
		}

		public EqualityComparer(Func<T, TKey> keySelector)
			: this(keySelector, (IEqualityComparer<TKey>)null)
		{
		}

		public bool Equals(T x, T y)
		{
			if (!GenericHelper.HasValue(x))
			{
				return !GenericHelper.HasValue(y);
			}
			if (!GenericHelper.HasValue(y))
			{
				return false;
			}
			TKey val = _keySelector(x);
			TKey val2 = _keySelector(y);
			if (!GenericHelper.HasValue(val))
			{
				return !GenericHelper.HasValue(val2);
			}
			if (!GenericHelper.HasValue(val2))
			{
				return false;
			}
			return _keyComparer.Equals(val, val2);
		}

		public int GetHashCode(T obj)
		{
			if (!GenericHelper.HasValue(obj))
			{
				return 0;
			}
			TKey val = _keySelector(obj);
			if (!GenericHelper.HasValue(val))
			{
				return 0;
			}
			return _keyComparer.GetHashCode(val);
		}
	}
	public static class EqualityComparer
	{
		public static IEqualityComparer<T> Create<T, TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new EqualityComparer<T, TKey>(keySelector, keyComparer);
		}

		public static IEqualityComparer<T> Create<T, TKey>(Func<T, TKey> keySelector)
		{
			return Create(keySelector, null);
		}
	}
}

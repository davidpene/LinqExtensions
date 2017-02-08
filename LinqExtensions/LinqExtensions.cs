using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Extensions
{
	public static class LinqExtensions
	{
		/// <summary>
		/// Iterates over the given collection and performs the provided action
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="action"></param>
		public static void Action<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}

		/// <summary>
		/// Returns a collection of unique items based on the given key function.
		///
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection"></param>
		/// <param name="groupFunc"></param>
		/// <returns> IEnumerable </returns>
		public static IEnumerable<T> Distinct<TKey, T>(this IEnumerable<T> collection, Func<T, TKey> groupFunc)
		{
			return collection.GroupBy(groupFunc).Select(group => group.First());
		}

		///  <summary>
		///  Returns a collection of unique items based on the given key function.
		///  Takes the greatest value of each group based on the ordering function
		/// 
		///  </summary>
		///  <typeparam name="TKey"></typeparam>
		///  <typeparam name="TOrder"></typeparam>
		///  <typeparam name="T"></typeparam>
		///  <param name="collection"></param>
		///  <param name="groupFunc"></param>
		///  <param name="orderFunc"></param>
		/// <param name="takeMin"> defaults to false </param>
		/// <returns> IEnumerable </returns>
		public static IEnumerable<T> Distinct<TKey, TOrder, T>(this IEnumerable<T> collection, Func<T, TKey> groupFunc,
			Func<T, TOrder> orderFunc, bool takeMin = false)
		{
			var groupedCollection = collection
				.GroupBy(groupFunc);

			var orderedCollection = takeMin
				? groupedCollection.Select(group => group
					.OrderBy(orderFunc).FirstOrDefault())
				: groupedCollection.Select(group => group
					.OrderByDescending(orderFunc).FirstOrDefault());

			return orderedCollection;
		}

		/// <summary>
		/// Determines if a collection contains a value based on some selection function
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="collection"></param>
		/// <param name="selectFunc"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool Contains<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> selectFunc, TKey value)
		{
			return collection.Select(selectFunc).Contains(value);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExtensions
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
			var knownElements = new HashSet<TKey>();
			return collection.Where(element => knownElements.Add(groupFunc(element)));
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
		/// <param name="orderByAsc"> defaults to false </param>
		/// <returns> IEnumerable </returns>
		public static IEnumerable<T> Distinct<TKey, TOrder, T>(this IEnumerable<T> collection, Func<T, TKey> groupFunc,
			Func<T, TOrder> orderFunc, bool orderByAsc = false)
		{
			var groupedCollection = collection
				.GroupBy(groupFunc);

			var orderedCollection = orderByAsc
				? groupedCollection.Select(group => group
					.OrderBy(orderFunc))
				: groupedCollection.Select(group => group
					.OrderByDescending(orderFunc));

			return orderedCollection.FirstOrDefault();
		}
	}
}

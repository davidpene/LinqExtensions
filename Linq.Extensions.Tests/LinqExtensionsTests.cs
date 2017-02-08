using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Linq.Extensions.Tests
{
	public class LinqExtensionsTests
	{
		[Fact]
		public void Action_Test()
		{
			var list = new[] {1, 2, 3};
			var newList = new List<int>();

			list.Action(item => newList.Add(item));

			Assert.Equal(list.Count(), newList.Count);
			Assert.True(newList.All(item => list.Contains(item)));
		}

		[Fact]
		public void Distinct_Returns_Unique_Elements_Based_On_Some_Property()
		{
			var list = new[] {new Paper(1, 1), new Paper(1, 3), new Paper(1, 3)};

			var distinctItems = list.Distinct(item => item.Height);

			// should contain only 2 items - Paper(1,1) and Paper(1,3)
			Assert.Equal(2, distinctItems.Count());
			Assert.True(distinctItems.Contains(new Paper(1, 1)));
			Assert.True(distinctItems.Contains(new Paper(1, 3)));
		}

		[Fact]
		public void
			Distinct_Returns_Unique_Elements_Based_On_Some_Property_Using_The_Max_Of_That_Property_To_Rid_Of_Duplicates()
		{
			var list = new[] {new Paper(1, 1), new Paper(1, 3), new Paper(2, 3)};

			var distinctItems = list.Distinct(item => item.Height, item => item.Width).ToList();

			// distinct items based on property Height if there are multiple then take highest based on Width
			// should contain only 2 items - Paper(1,1) and Paper(2,3) e.g Because 2 > 1 it took Paper(2,3) over Paper(1,3)
			Assert.Equal(2, distinctItems.Count);
			Assert.True(distinctItems.Contains(new Paper(1, 1)));
			Assert.True(distinctItems.Contains(new Paper(2, 3)));
		}

		[Fact]
		public void
			Distinct_Returns_Unique_Elements_Based_On_Some_Property_Using_The_Min_Of_That_Property_To_Rid_Of_Duplicates()
		{
			var list = new[] {new Paper(1, 1), new Paper(1, 3), new Paper(2, 3)};

			var distinctItems = list.Distinct(item => item.Height, item => item.Width, takeMin: true).ToList();

			// distinct items based on property Height if there are multiple then take lowest based on Width
			// should contain only 2 items - Paper(1,1) and Paper(1,3) e.g Because 1 < 2 it took Paper(1,3) over Paper(2,3)
			Assert.Equal(2, distinctItems.Count);
			Assert.True(distinctItems.Contains(new Paper(1, 1)));
			Assert.True(distinctItems.Contains(new Paper(1, 3)));
		}

		[Theory]
		[InlineData(1, 1, true)]
		[InlineData(1, 2, false)]
		[InlineData(-11, -11, true)]
		[InlineData(-1, 1, false)]
		public void Contains_Test(int actualHeight, int expectedHeight, bool shouldBeTrue)
		{
			var list = new[] {new Paper(0, actualHeight),};

			Assert.Equal(shouldBeTrue, list.Contains(paper => paper.Height, expectedHeight));
		}
	}
}

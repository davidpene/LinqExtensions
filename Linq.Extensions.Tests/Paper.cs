using System;

namespace Linq.Extensions.Tests
{
	public class Paper : IEquatable<Paper>
	{
		public Paper(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Width { get; }
		public int Height { get; }

		public bool Equals(Paper other)
		{
			return IsEqual(other);
		}

		public override bool Equals(object obj)
		{
			return obj is Paper && IsEqual((Paper)obj);
		}

		private bool IsEqual(Paper other)
		{
			return Width == other.Width && Height == other.Height;
		}
	}
}

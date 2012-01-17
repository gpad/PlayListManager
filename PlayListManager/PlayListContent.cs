using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayListManager
{
	public class PlayListContent
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public IEnumerable<PlayListItem> Items { get; private set; }

		public PlayListContent(int id, string name, IEnumerable<PlayListItem> items)
		{
			Id = id;
			Name = name;
			Items = items.ToArray();
		}

		public bool Equals(PlayListContent other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			
			return other.Id == Id && Equals(other.Name, Name) && (other.Items.Except(Items).Count() == 0) && (Items.Except(other.Items).Count() == 0);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PlayListContent)) return false;
			return Equals((PlayListContent)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = Id;
				result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				result = (result * 397) ^ (Items != null ? Items.GetHashCode() : 0);
				return result;
			}
		}

		public override string ToString()
		{
			return string.Format("Id: {0}, Name: {1}, {2}", Id, Name, Format(Items));
		}

		private static string Format(IEnumerable<PlayListItem> items)
		{
			return items.Aggregate("", (acc, next) => next + " - " + acc);
		}

		public PlayListContent AddItem(PlayListItem newItem)
		{
			return new PlayListContent(Id, Name, Items.Concat(new[] {newItem}));
		}
	}
}

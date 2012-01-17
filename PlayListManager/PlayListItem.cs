using System;

namespace PlayListManager
{
	public class PlayListItem
	{
		public bool Equals(PlayListItem other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.ImageId, ImageId) && Equals(other.Name, Name);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (PlayListItem)) return false;
			return Equals((PlayListItem) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((ImageId != null ? ImageId.GetHashCode() : 0)*397) ^ (Name != null ? Name.GetHashCode() : 0);
			}
		}

		public PlayListItem(ImageId imageId, string name)
		{
			ImageId = imageId;
			Name = name;
		}

		public ImageId ImageId { get; private set; }

		public string Name { get; private set; }

		public override string ToString()
		{
			return string.Format("ImageId: {0}, Name: '{1}'", ImageId, Name);
		}
	}
}
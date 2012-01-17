namespace PlayListManager
{
	public class ImageId
	{
		public int Id { get; private set; }

		public ImageId(int id)
		{
			Id = id;
		}


		public bool Equals(ImageId other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id == Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (ImageId)) return false;
			return Equals((ImageId) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public override string ToString()
		{
			return string.Format("Id: {0}", Id);
		}
	}
}
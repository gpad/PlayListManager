using System.Collections.Generic;

namespace PlayListManager
{
	public class NewPlayList
	{
		public IEnumerable<ImageId> ImageIds { get; private set; }
		public string Name { get; private set; }

		public NewPlayList(string name, IEnumerable<ImageId> imageIds)
		{
			ImageIds = imageIds;
			Name = name;
		}

	}
}

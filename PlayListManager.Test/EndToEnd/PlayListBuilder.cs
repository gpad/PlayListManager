using System;
using System.Collections.Generic;

namespace PlayListManager.Test.EndToEnd
{
	internal class PlayListBuilder
	{
		private readonly List<ImageId> m_Images = new List<ImageId>();
		private readonly string m_Name = Guid.NewGuid().ToString();

		public PlayListBuilder AddImage(ImageId imageId)
		{
			m_Images.Add(imageId);
			return this;
		}

		public NewPlayList Build()
		{
			return new NewPlayList(m_Name, m_Images);
		}
	}
}
using System;

namespace PlayListManager
{
	public class PlayListComboItem
	{
		public PlayListComboItem(PlayList playListContent)
		{
			PlayList = playListContent;
		}

		public PlayList PlayList { get; private set; }

		public override string ToString()
		{
			return PlayList.Content.Name;
		}
	}
}
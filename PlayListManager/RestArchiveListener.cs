using System;

namespace PlayListManager
{
	public interface RestArchiveListener
	{
		void Error(ErrorDescription errorDescription);
		void PlayListsDownloaded(PlayList[] playLists);
		void NewPlayListUploaded(PlayList playList);
	}
}
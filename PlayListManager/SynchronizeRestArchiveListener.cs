using System;
using System.Windows.Forms;

namespace PlayListManager
{
	public class SynchronizeRestArchiveListener : RestArchiveListener
	{
		private readonly RestArchiveListener m_Listener;
		private readonly Control m_Control;

		public SynchronizeRestArchiveListener(Control control, RestArchiveListener listener)
		{
			m_Listener = listener;
			m_Control = control;
		}

		public void Error(ErrorDescription errorDescription)
		{
			Synchronize(() => m_Listener.Error(errorDescription));
		}

		public void PlayListsDownloaded(PlayList[] playLists)
		{
			Synchronize(() => m_Listener.PlayListsDownloaded(playLists));
		}

		public void NewPlayListUploaded(PlayList playList)
		{
			Synchronize(() => m_Listener.NewPlayListUploaded(playList));
		}

		private void Synchronize(Action action)
		{
			m_Control.BeginInvoke(action);
		}
	}
}
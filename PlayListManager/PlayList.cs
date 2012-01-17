using System;

namespace PlayListManager
{
	public class PlayList
	{
		private readonly ListenersList<PlayListListener> m_Listeners = new ListenersList<PlayListListener>();
		private readonly Archive m_Archive;

		public PlayList(PlayListContent content, Archive archive)
		{
			Content = content;
			m_Archive = archive;
		}

		public PlayListContent Content { get; private set; }

		public void AddItem(PlayListItem newItem)
		{
			Content = Content.AddItem(newItem);
			m_Listeners.Raise(l => l.ContentChanged(Content));
		}

		public void AddListener(PlayListListener playListListener)
		{
			m_Listeners.Add(playListListener);
		}

		public void RemoveListener(PlayListListener playListListener)
		{
			m_Listeners.Remove(playListListener);
		}

		public void Save()
		{
			m_Archive.Save(Content);
		}
	}
}
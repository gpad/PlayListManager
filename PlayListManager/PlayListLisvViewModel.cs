using System.Linq;
using System.Windows.Forms;

namespace PlayListManager
{
	public class PlayListLisvViewModel : ArchiveComboModelListener, PlayListListener
	{
		private readonly ListView m_ListView;
		private PlayList m_SelectedPlayList;

		public PlayListLisvViewModel(ListView listView)
		{
			m_ListView = listView;
		}

		public void SelectedPlayListChanged(PlayList playList)
		{
			UpdateListener(playList);
			UpdateListView(playList.Content);
		}

		public void ContentChanged(PlayListContent playListContent)
		{
			UpdateListView(playListContent);
		}

		public void AddNewItem(PlayListItem newItem)
		{
			m_SelectedPlayList.AddItem(newItem);
		}

		public void SaveCurrentPlayList()
		{
			m_SelectedPlayList.Save();
		}

		private void UpdateListener(PlayList playList)
		{
			if (m_SelectedPlayList != null)
				m_SelectedPlayList.RemoveListener(this);
			m_SelectedPlayList = playList;
			m_SelectedPlayList.AddListener(this);
		}

		private void UpdateListView(PlayListContent playListContent)
		{
			m_ListView.Items.Clear();
			m_ListView.Items.AddRange(playListContent.Items.Select(item => new ListViewItem { Text = item.Name, Tag = item }).ToArray());
		}

	}
}
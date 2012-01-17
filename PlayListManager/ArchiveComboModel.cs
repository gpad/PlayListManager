using System.Linq;
using System.Windows.Forms;

namespace PlayListManager
{
	public class ArchiveComboModel : RestArchiveListener
	{
		private readonly ComboBox m_ComboBoxArchive;
		private readonly ArchiveComboModelListener m_ArchiveComboModelListener;

		public ArchiveComboModel(ComboBox comboBoxArchive, ArchiveComboModelListener archiveComboModelListener)
		{
			m_ComboBoxArchive = comboBoxArchive;
			m_ArchiveComboModelListener = archiveComboModelListener;
			m_ComboBoxArchive.SelectedIndexChanged += (_, __) => SelectedPlayListChanged();
		}

		private void SelectedPlayListChanged()
		{
			m_ArchiveComboModelListener.SelectedPlayListChanged(GetSelectedPlayList());
		}

		private PlayList GetSelectedPlayList()
		{
			return ((PlayListComboItem) m_ComboBoxArchive.SelectedItem).PlayList;
		}

		public void Error(ErrorDescription errorDescription)
		{
			MessageBox.Show(errorDescription.ToString());
		}

		public void PlayListsDownloaded(PlayList[] playLists)
		{
			m_ComboBoxArchive.Items.Clear();
			m_ComboBoxArchive.Items.AddRange(playLists.Select(pl => new PlayListComboItem(pl)).ToArray());
			if (m_ComboBoxArchive.Items.Count > 0)
				m_ComboBoxArchive.SelectedItem = m_ComboBoxArchive.Items[0];
		}

		public void NewPlayListUploaded(PlayList playList)
		{
			var playListComboItem = new PlayListComboItem(playList);
			m_ComboBoxArchive.Items.Add(playListComboItem);
			m_ComboBoxArchive.SelectedItem = playListComboItem;
		}
	}
}



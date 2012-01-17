using System;
using System.Windows.Forms;

namespace PlayListManager
{
	public partial class MainForm : Form
	{
		private readonly Archive m_Archive;
		private readonly ArchiveComboModel m_ArchiveComboModel;
		private readonly PlayListLisvViewModel m_PlayListLisvViewModel;

		public MainForm()
		{
			InitializeComponent();

			m_PlayListLisvViewModel = new PlayListLisvViewModel(listViewPlayList);
			m_ArchiveComboModel = new ArchiveComboModel(comboBoxArchive, m_PlayListLisvViewModel);
			m_Archive = new RestArchive("http://127.0.0.1:3000", new SynchronizeRestArchiveListener(this, m_ArchiveComboModel));
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			m_Archive.Connect();
		}

		private void buttonAddPlayList_Click(object sender, EventArgs e)
		{
			m_Archive.AddNewPlayList(textBoxName.Text);
		}

		private void buttonAddImageId_Click(object sender, EventArgs e)
		{
			m_PlayListLisvViewModel.AddNewItem(new PlayListItem(GetNewImageId(), textBoxImageId.Text));
		}

		private ImageId GetNewImageId()
		{
			return new ImageId(int.Parse(textBoxImageId.Text));
		}

		private void buttonSavePlayList_Click(object sender, EventArgs e)
		{
			m_PlayListLisvViewModel.SaveCurrentPlayList();
		}
	}
}

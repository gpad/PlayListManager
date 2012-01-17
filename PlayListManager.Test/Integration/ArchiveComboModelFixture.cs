using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;

namespace PlayListManager.Test.Integration
{
	[TestFixture]
	public class ArchiveComboModelFixture
	{
		private Form m_Form;
		private ComboBox m_ComboBoxArchive;
		private ArchiveComboModelListener m_ArchiveComboModelListener;
		private ArchiveComboModel m_Model;

		[SetUp]
		public void Initialize()
		{
			m_Form = new Form();
			m_ComboBoxArchive = new ComboBox();
			m_Form.Controls.Add(m_ComboBoxArchive);
			m_ArchiveComboModelListener = MockRepository.GenerateMock<ArchiveComboModelListener>();
			m_Model = new ArchiveComboModel(m_ComboBoxArchive, m_ArchiveComboModelListener);
		}

		[TearDown]
		public void Destroy()
		{
			m_Form.Close();
			m_ArchiveComboModelListener.VerifyAllExpectations();
		}

		[Test]
		public void ShowInComboTheNameOfThePlayList()
		{
			const string name = "name of playlist";

			m_Model.PlayListsDownloaded(new[] { new PlayList(new PlayListContent(1, name, new PlayListItem[0]), null) });

			m_Form.Show();
			Assert.That(m_ComboBoxArchive.SelectedText, Is.EqualTo(name));
		}

		[Test]
		public void RaiseSelectedPlayListsChangedWhenChangeSelectionInCombo()
		{
			var playLists = new[]
			                	{
			                		new PlayList(new PlayListContent(1, "name", new PlayListItem[0]), null), 
									new PlayList(new PlayListContent(2, "name", new PlayListItem[0]), null),
			                	};
			m_ArchiveComboModelListener.Expect(l => l.SelectedPlayListChanged(playLists[0]));

			m_Model.PlayListsDownloaded(playLists);
		}


	}

}
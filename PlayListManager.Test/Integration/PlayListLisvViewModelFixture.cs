using System.Windows.Forms;
using NUnit.Framework;
using System.Linq;

namespace PlayListManager.Test.Integration
{
	[TestFixture]
	public class PlayListLisvViewModelFixture
	{
		private Form m_Form;
		private ListView m_ListViewPlayList;
		private PlayListListViewModel m_Model;

		[SetUp]
		public void Initialize()
		{
			m_Form = new Form();
			m_ListViewPlayList = new ListView();
			m_Form.Controls.Add(m_ListViewPlayList);
			m_Model = new PlayListListViewModel(m_ListViewPlayList);
		}

		[TearDown]
		public void Destroy()
		{
			m_Form.Close();
		}

		private PlayListItem[] GetPlayListItemsInListView()
		{
			return m_ListViewPlayList.Items.Cast<ListViewItem>().Select(lvi => lvi.Tag).Cast<PlayListItem>().ToArray();
		}

		[Test]
		public void UpdateListViewWhenContentOfPlayListChange()
		{
			var playList = new PlayList(new PlayListContent(2, "name", new PlayListItem[0]), null);
			m_Model.SelectedPlayListChanged(playList);
			var newItem = new PlayListItem(new ImageId(33), "trentatre");
			
			playList.AddItem(newItem);

			Assert.That(GetPlayListItemsInListView(), Is.EquivalentTo(new[] {newItem}));
		}

		[Test]
		public void UplaodListViewOnlyForChnageInSelectedPlayList()
		{
			var playList1 = new PlayList(new PlayListContent(1, "name", new PlayListItem[0]), null);
			var playList2 = new PlayList(new PlayListContent(2, "name", new PlayListItem[0]), null);
			m_Model.SelectedPlayListChanged(playList1);
			m_Model.SelectedPlayListChanged(playList2);
			m_Model.SelectedPlayListChanged(playList1);
			var newItem = new PlayListItem(new ImageId(33), "trentatre");

			playList2.AddItem(newItem);

			Assert.That(GetPlayListItemsInListView(), Is.Empty);
			
		}

		[Test]
		public void AddNewItemToSelectedPlayList()
		{
			var playLists = new[]
			                	{
			                		new PlayList(new PlayListContent(1, "name", new PlayListItem[0]), null), 
									new PlayList(new PlayListContent(2, "name", new PlayListItem[0]), null),
			                	};
			var newItem = new PlayListItem(new ImageId(33), "name33");
			m_Model.SelectedPlayListChanged(playLists[0]);

			m_Model.AddNewItem(newItem);

			Assert.That(playLists[0].Content, Is.EqualTo(new PlayListContent(1, "name", new[] { newItem })));
		}
	}
}

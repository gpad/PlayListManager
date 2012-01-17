using NUnit.Framework;
using Rhino.Mocks;

namespace PlayListManager.Test.Unit
{
	[TestFixture]
	public class PlayListFixture
	{
		[Test]
		public void RaiseContentChangeWhenAddItemToPlayList()
		{
			var newItem = new PlayListItem(new ImageId(30), "");
			var playListListener = MockRepository.GenerateMock<PlayListListener>();
			playListListener.Expect(l => l.ContentChanged(new PlayListContent(0, "", new []{newItem})));
			var playList = new PlayList(new PlayListContent(0, "", new PlayListItem[0]), null);
			playList.AddListener(playListListener);

			playList.AddItem(newItem);
		}
	}
}
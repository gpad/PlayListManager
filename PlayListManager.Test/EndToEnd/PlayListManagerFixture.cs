using NUnit.Framework;

namespace PlayListManager.Test.EndToEnd
{
	[TestFixture]
	public class PlayListManagerFixture
	{
		private PlayListBuilder m_PlayListBuilder;
		private readonly ImageId m_ImageId = new ImageId(30);
		private FakeMediaServer m_FakeServer;
		private ApplicationRunner m_Application;

		[SetUp]
		public void Initialize()
		{
			m_Application = new ApplicationRunner();
			m_FakeServer = new FakeMediaServer();
			m_PlayListBuilder = new PlayListBuilder();

			m_FakeServer.Reset();
		}

		[TearDown]
		public void Destroy()
		{
			m_Application.Stop();
		}

		[Test]
		public void ShowPlayListPresentInServerWhenConnected()
		{
			var examplePlayList = m_FakeServer.AddNewPlayList(m_PlayListBuilder
				.AddImage(m_ImageId)
				.Build());
			m_Application.Start();

			m_Application.Connect();

			m_Application.HasShownPlayLists(new[] {examplePlayList});
		}

		[Test]
		public void SelectFirstDownlaodedPlayList()
		{
			var examplePlayList = m_FakeServer.AddNewPlayList(m_PlayListBuilder
				.AddImage(m_ImageId)
				.Build());
			m_Application.Start();

			m_Application.Connect();

			m_Application.HasSelectedPlayList(examplePlayList);
		}

		[Test]
		public void ShowContentOfSelectePlayList()
		{
			var examplePlayList = m_FakeServer.AddNewPlayList(m_PlayListBuilder
				.AddImage(m_ImageId)
				.Build());
			m_Application.Start();

			m_Application.Connect();

			m_Application.HasShownContentOf(examplePlayList);
		}

		[Test]
		public void AddNewPlayListToServer()
		{
			m_FakeServer.AddNewPlayList(m_PlayListBuilder.AddImage(m_ImageId).Build());
			m_Application.Start();
			m_Application.Connect();

			const string name = "nuova playlist";
			m_Application.AddNewPlayList(name);

			m_Application.HasSelectedPlayListWithName(name);
		}

		[Test]
		public void AddItemToCurrentPlayList()
		{
			var pl = m_FakeServer.AddNewPlayList(m_PlayListBuilder.AddImage(m_ImageId).Build());
			m_Application.Start();
			m_Application.Connect();
			m_Application.HasShownPlayLists(new []{pl});

			var imageIdToAdd = new ImageId(31);
			m_Application.AddImageIdToSelectedPlayList(imageIdToAdd);

			m_Application.HasShown(imageIdToAdd);
		}

		[Test]
		public void SaveModifiedPlayList()
		{
			var pl = m_FakeServer.AddNewPlayList(m_PlayListBuilder.AddImage(m_ImageId).Build());
			m_Application.Start();
			m_Application.Connect();
			m_Application.HasShownPlayLists(new[] { pl });
			var imageIdToAdd = new ImageId(31);
			m_Application.AddImageIdToSelectedPlayList(imageIdToAdd);
			m_Application.HasShown(imageIdToAdd);

			m_Application.SaveCurrentPlayList();

			m_FakeServer.HasReceivedPlayListWith(imageIdToAdd);

		}
	}
}
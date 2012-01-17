using NUnit.Framework;

namespace PlayListManager.Test.Unit
{
	[TestFixture]
	class PlayListContentFixture
	{
		[Test]
		public void PlayListIsEqualByAttributes()
		{
			var pl1 = new PlayListContent(1, "name", new[] { new PlayListItem(new ImageId(1), "") });
			var pl2 = new PlayListContent(1, "name", new[] { new PlayListItem(new ImageId(1), "") });
			var pl3 = new PlayListContent(1, "name", new[] { new PlayListItem(new ImageId(1), ""), new PlayListItem(new ImageId(2), "") });
			var pl4 = new PlayListContent(1, "name", new[] { new PlayListItem(new ImageId(1), ""), new PlayListItem(new ImageId(2), "") });
			var pl5 = new PlayListContent(1, "name_", new[] { new PlayListItem(new ImageId(1), ""), new PlayListItem(new ImageId(2), "") });
			var pl6 = new PlayListContent(2, "name", new[] { new PlayListItem(new ImageId(1), ""), new PlayListItem(new ImageId(2), "") });

			Assert.That(pl1, Is.EqualTo(pl2));
			Assert.That(pl1, Is.Not.EqualTo(pl3));
			Assert.That(pl3, Is.EqualTo(pl4));
			Assert.That(pl4, Is.Not.EqualTo(pl5));
			Assert.That(pl4, Is.Not.EqualTo(pl6));
		}

		[Test]
		public void FormatPlayList()
		{
			var pl = new PlayListContent(1, "name", new[] { new PlayListItem(new ImageId(1), "") });

			Assert.That(pl.ToString(), Is.EqualTo("Id: 1, Name: name, ImageId: Id: 1, Name: '' - "));
		}

		[Test]
		public void ImageIdsAreEqualsByAttribute()
		{
			var ids1 = new[] { new ImageId(1) };
			var ids2 = new[] { new ImageId(1) };

			Assert.That(ids1, Is.EqualTo(ids2));
		}
	}
}

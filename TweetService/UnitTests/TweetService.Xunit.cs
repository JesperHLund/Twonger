using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedMessages;
using TweetService;
using TweetService.Controllers;
using Xunit;

namespace TweetService.Tests
{
    public class TweetControllerTests
    {
        [Fact]
        public void GetTweets_Returns_NotFound_When_NoTweetsFound()
        {
            // Arrange
            var databaseMock = new Mock<Database.Database>();
            var messageClientMock = new Mock<MessageClient>();

            var tweetController = new TweetController(databaseMock.Object, messageClientMock.Object);

            var userId = 123;
            var tweetId = 456;

            // Setup mock behavior
            databaseMock.Setup(db => db.GetNext100Tweets(userId, tweetId)).Returns(new List<Tweet>());

            // Act
            var result = tweetController.GetTweets(userId, tweetId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No more tweets", ((NotFoundObjectResult)result.Result).Value);
        }

        [Fact]
        public void PostTweet_Returns_True_When_TweetAddedSuccessfully()
        {
            // Arrange
            var databaseMock = new Mock<Database.Database.TweetContext>();
            var database = new Database.Database(databaseMock.Object);
            var messageClientMock = new Mock<MessageClient>();

            var tweet = new SharedMessages.Tweet("Test tweet", 123); // Corrected

            var tweetController = new TweetController(database, messageClientMock.Object);

            // Setup mock behavior
            databaseMock.Setup(db => db.Tweets.Add(It.IsAny<Tweet>())).Verifiable();
            databaseMock.Setup(db => db.SaveChanges()).Returns(1);

            // Act
            var result = tweetController.PostTweet(tweet);

            // Assert
            Assert.True(result);
            messageClientMock.Verify(mc => mc.Send(It.IsAny<TweetMessage>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void PostTweet_Returns_False_When_TweetNotAddedSuccessfully()
        {
            // Arrange
            var databaseMock = new Mock<Database.Database.TweetContext>();
            var database = new Database.Database(databaseMock.Object);
            var messageClientMock = new Mock<MessageClient>();

            var tweet = new SharedMessages.Tweet("Test tweet", 123); // Corrected

            var tweetController = new TweetController(database, messageClientMock.Object);

            // Setup mock behavior
            databaseMock.Setup(db => db.Tweets.Add(It.IsAny<Tweet>())).Verifiable();
            databaseMock.Setup(db => db.SaveChanges()).Returns(0);

            // Act
            var result = tweetController.PostTweet(tweet);

            // Assert
            Assert.False(result);
            messageClientMock.Verify(mc => mc.Send(It.IsAny<TweetMessage>(), It.IsAny<string>()), Times.Never);
        }
    }
}

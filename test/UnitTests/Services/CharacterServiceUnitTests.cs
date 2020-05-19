using GloomhavenTracker.Data;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Services.Classes;
using GloomhavenTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    public class CharacterServiceUnitTests
    {

        ICharacterService characterService;
        GloomhavenTrackerContext gloomhavenTrackerContext;
        DbContextOptions<GloomhavenTrackerContext> options;

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<GloomhavenTrackerContext>()
               .UseInMemoryDatabase(databaseName: "gloomhaven")
               .Options;
            gloomhavenTrackerContext = new GloomhavenTrackerContext(options);
            characterService = new CharacterService(gloomhavenTrackerContext);
        }

        [TearDown]
        public void TearDown()
        {
            gloomhavenTrackerContext.Database.EnsureDeleted();
        }

        [TestCase(1, 0)]
        [TestCase(2, 45)]
        [TestCase(3, 95)]
        [TestCase(4, 150)]
        [TestCase(5, 210)]
        [TestCase(6, 275)]
        [TestCase(7, 345)]
        [TestCase(8, 420)]
        [TestCase(9, 500)]
        public void Test_CalculateExperienceBasedOnLevel_ReturnsCorrectXp_WhenGivenAValidLevel(int level, int expectedXp)
        {
            //Arrange

            //Act
            var ret = characterService.CalculateExperienceBasedOnLevel(level);

            //Assert
            Assert.AreEqual(expectedXp, ret);
        }

        [Test]
        public void Test_GetAvailableLevels_ReturnsAListContainingNoLevels_WhenTheProsperityIs0()
        {
            //Arrange
            var partyId = 1;
            gloomhavenTrackerContext.Parties.Add(new Party()
            {
                Id = partyId,
                Prosperity = 0
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = characterService.GetAvailableLevels(partyId);

            //Assert
            Assert.IsEmpty(ret);
        }

        [Test]
        public void Test_GetAvailableLevels_ReturnsAListContaining1LevelWithValue1_WhenTheProsperityIs1()
        {
            //Arrange
            var partyId = 1;
            gloomhavenTrackerContext.Parties.Add(new Party()
            {
                Id = partyId,
                Prosperity = 1
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = characterService.GetAvailableLevels(partyId);

            //Assert
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(1, ret[0]);
        }

        [Test]
        public void Test_GetAvailableLevels_ReturnsAListContaining2LevelsWithValues1And2_WhenTheProsperityIs2()
        {
            //Arrange
            var partyId = 1;
            gloomhavenTrackerContext.Parties.Add(new Party()
            {
                Id = partyId,
                Prosperity = 2
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = characterService.GetAvailableLevels(partyId);

            //Assert
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(1, ret[0]);
            Assert.AreEqual(2, ret[1]);
        }
    }
}
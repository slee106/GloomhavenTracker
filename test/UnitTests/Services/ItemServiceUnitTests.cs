using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Data;
using GloomhavenTracker.Models.DatabaseModels;
using GloomhavenTracker.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace GloomhavenTracker.UnitTests.Services
{
    [TestFixture]
    public class ItemServiceUnitTests
    {
        GloomhavenTrackerContext gloomhavenTrackerContext;
        IItemService itemService;
        DbContextOptions<GloomhavenTrackerContext> options;

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<GloomhavenTrackerContext>()
            .UseInMemoryDatabase(databaseName: "gloomhaven")
            .Options;
            gloomhavenTrackerContext = new GloomhavenTrackerContext(options);
            itemService = new ItemService(gloomhavenTrackerContext);
        }

        [TearDown]
        public void TearDown()
        {
            gloomhavenTrackerContext.Database.EnsureDeleted();
        }

        #region TestCases
        [TestCase(20, -5)]
        [TestCase(19, -5)]
        [TestCase(18, -4)]
        [TestCase(17, -4)]
        [TestCase(16, -4)]
        [TestCase(15, -4)]
        [TestCase(14, -3)]
        [TestCase(13, -3)]
        [TestCase(12, -3)]
        [TestCase(11, -3)]
        [TestCase(10, -2)]
        [TestCase(9, -2)]
        [TestCase(8, -2)]
        [TestCase(7, -2)]
        [TestCase(6, -1)]
        [TestCase(5, -1)]
        [TestCase(4, -1)]
        [TestCase(3, -1)]
        [TestCase(2, 0)]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(-2, 0)]
        [TestCase(-3, 1)]
        [TestCase(-4, 1)]
        [TestCase(-5, 1)]
        [TestCase(-6, 1)]
        [TestCase(-7, 2)]
        [TestCase(-8, 2)]
        [TestCase(-9, 2)]
        [TestCase(-10, 2)]
        [TestCase(-11, 3)]
        [TestCase(-12, 3)]
        [TestCase(-13, 3)]
        [TestCase(-14, 3)]
        [TestCase(-15, 4)]
        [TestCase(-16, 4)]
        [TestCase(-17, 4)]
        [TestCase(-18, 4)]
        [TestCase(-19, 5)]
        [TestCase(-20, 5)]
        #endregion
        public void Test_CalculateShopDiscount_ReturnsCorrectValue_WhenCalledWithAnInt(int reputation, int expectedResult)
        {
            //Arrange

            //Act
            var ret = itemService.CalculateShopDiscount(reputation);

            //Assert
            Assert.AreEqual(expectedResult, ret);
        }

        [Test]
        public void Test_GetItemsWithAdjustedAmounts_ReturnsAnEmptyList_WhenCalledWithAnEmptyList()
        {
            //Arrange
            var listOfItems = new List<Item>();

            //Act
            var ret = itemService.GetItemsWithAdjustedAmounts(listOfItems);

            //Assert
            Assert.IsEmpty(ret);
        }

        [Test]
        public void Test_GetItemsWithAdjustedAmounts_ReturnsAListWithOneItemThathasntBeenChangedAnd1ForNumberAvailable_WhenCalledWithAListWithOneItemAndContextRetuns0ForCountOfCharacterItems()
        {
            //Arrange
            var listOfItems = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    NumberAvailable = 1
                }
            };
            gloomhavenTrackerContext.Items.Add(new Item()
            {
                Name = "TestName",
                CharacterItems = new List<CharacterItem>(),
                Available = true,
                Cost = 123,
                Description = "TestDescription",
                NumberAvailable = 1
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = itemService.GetItemsWithAdjustedAmounts(listOfItems);

            //Assert
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(1, ret[0].NumberAvailable);
        }

        [Test]
        public void Test_GetItemsWithAdjustedAmounts_ReturnsAListWith0ItemThathasntBeenChanged_WhenCalledWithAListWithOneItemAndContextRetuns1ForCountOfCharacterItems()
        {
            //Arrange
            var listOfItems = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    NumberAvailable = 1
                }
            };
            gloomhavenTrackerContext.Items.Add(new Item()
            {
                Name = "TestName",
                CharacterItems = new List<CharacterItem>()
                {
                    new CharacterItem()
                },
                Available = true,
                Cost = 123,
                Description = "TestDescription",
                NumberAvailable = 1
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = itemService.GetItemsWithAdjustedAmounts(listOfItems);

            //Assert
            Assert.IsEmpty(ret);
        }

        [Test]
        public void Test_GetItemsWithAdjustedAmounts_ReturnsAListWithTwoItemThathasntBeenChangedAnd2ForNumberAvailable_WhenCalledWithAListWithOneItemAndContextRetuns0ForCountOfCharacterItems()
        {
            //Arrange
            var listOfItems = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    NumberAvailable = 1
                },
                new Item()
                {
                    Id = 2,
                    NumberAvailable = 2
                }
            };
            gloomhavenTrackerContext.Items.AddRange(new List<Item>()
            {
                new Item()
                {
                    Name = "TestName",
                    CharacterItems = new List<CharacterItem>(),
                    Available = true,
                    Cost = 123,
                    Description = "TestDescription",
                    NumberAvailable = 1
                },
                new Item()
                {
                    Name = "TestName",
                    CharacterItems = new List<CharacterItem>(),
                    Available = true,
                    Cost = 123,
                    Description = "TestDescription",
                    NumberAvailable = 2
                }
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = itemService.GetItemsWithAdjustedAmounts(listOfItems);

            //Assert
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(1, ret[0].NumberAvailable);
            Assert.AreEqual(2, ret[1].NumberAvailable);
        }

        [Test]
        public void Test_GetItemsWithAdjustedAmounts_ReturnsAListWith1ItemThatHas1ForNumberAvailable_WhenCalledWithAListWithTwoItemsAndContextRetuns1ForCountOfCharacterItemsForEachItem()
        {
            //Arrange
            var listOfItems = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    NumberAvailable = 1
                },
                new Item()
                {
                    Id = 2,
                    NumberAvailable = 2
                }
            };
            gloomhavenTrackerContext.Items.AddRange(new List<Item>()
            {
                new Item()
                {
                    Name = "TestName",
                    CharacterItems = new List<CharacterItem>()
                    {
                        new CharacterItem()
                    },
                    Available = true,
                    Cost = 123,
                    Description = "TestDescription",
                    NumberAvailable = 1
                },
                new Item()
                {
                    Name = "TestName",
                    CharacterItems = new List<CharacterItem>()
                    {
                        new CharacterItem()
                    },
                    Available = true,
                    Cost = 123,
                    Description = "TestDescription",
                    NumberAvailable = 2
                }
            });
            gloomhavenTrackerContext.SaveChanges();

            //Act
            var ret = itemService.GetItemsWithAdjustedAmounts(listOfItems);

            //Assert
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(1, ret[0].NumberAvailable);
        }
    }
}

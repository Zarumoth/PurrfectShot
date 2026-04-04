using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data;
using PurrfectShot.Web.Infrastructure.Profiles;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Tests
{
    public class CatServiceTests
    {
        private readonly PurrfectShotDbContext dbContext;
        private readonly CatService catService;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<CatService>> mockLogger;

        public CatServiceTests()
        {
            // 1. InMemory DB Setup
            var options = new DbContextOptionsBuilder<PurrfectShotDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new PurrfectShotDbContext(options);

            // 2. AutoMapper Setup (v15+ standard)
            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<CatProfile>();

            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            this.mapper = mapperConfig.CreateMapper();

            // 3. Mock Logger
            this.mockLogger = new Mock<ILogger<CatService>>();

            // 4. Service Instantiation
            this.catService = new CatService(this.dbContext, this.mapper, this.mockLogger.Object);
        }

        // Tests basic functionality: Check if the method returns true when the record exists
        [Fact]
        public async Task ExistsByIdAsync_ShouldReturnTrue_WhenCatExists()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 1,
                Name = "Topcho",
                Breed = "Tuxie",
                Description = "The strongest and healthiest kitty tux boy",
                OwnerId = Guid.NewGuid().ToString()
            };

            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.ExistsByIdAsync(1);

            // Assert
            Assert.True(result);
        }

        // Tests edge case: Check if the method returns false when an invalid ID is provided
        [Fact]
        public async Task ExistsByIdAsync_ShouldReturnFalse_WhenCatDoesNotExist()
        {
            // Act
            var result = await catService.ExistsByIdAsync(999);

            // Assert
            Assert.False(result);
        }

        // Tests record creation: Verifies mapping, assignment of system fields (IsActive, OwnerId), and ID return
        [Fact]
        public async Task AddCatAsync_ShouldCreateCatAndReturnItsId()
        {
            // Arrange
            var inputModel = new CatInputModel
            {
                Name = "Topcho",
                Breed = "Tuxie",
                Description = "The strongest and healthiest kitty tux boy"
            };

            var userId = "test-user-id";

            // Act
            var resultId = await catService.AddCatAsync(inputModel, userId);
            var catInDb = await dbContext.Cats.FindAsync(resultId);

            // Assert
            Assert.NotEqual(0, resultId);
            Assert.NotNull(catInDb);
            Assert.Equal(userId, catInDb.OwnerId);
            Assert.True(catInDb.IsActive);
            Assert.Equal("Topcho", catInDb.Name);
            Assert.Equal("The strongest and healthiest kitty tux boy", catInDb.Description);
        }

        // Tests Soft Delete: Ensures the IsActive flag is set to false, but the record is not removed from the DB
        [Fact]
        public async Task ArchiveCatAsync_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 10,
                Name = "ArchiveMe",
                Breed = "Test Breed",
                Description = "This cat will be archived",
                IsActive = true,
                OwnerId = "user"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            // Act
            await catService.ArchiveCatAsync(10);
            var result = await dbContext.Cats.FindAsync(10);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsActive);
        }

        // Tests restore functionality: Ensures an archived cat becomes active again
        [Fact]
        public async Task RestoreArchivedCatAsync_ShouldSetIsActiveToTrue()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 20,
                Name = "Bigglesworth",
                Breed = "Test Breed",
                Description = "This cat will be restored",
                IsActive = false,
                OwnerId = "user"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            // Act
            await catService.RestoreArchivedCatAsync(20);
            var result = await dbContext.Cats.FindAsync(20);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsActive);
        }

        // Tests Search functionality: Verifies that filtering by SearchTerm is case-insensitive and checks both Name and Breed
        [Fact]
        public async Task GetFeaturedCatsAsync_ShouldFilterBySearchTermCorrectly()
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "Misty", Breed = "Siamese", Description = "Beautiful siamese cat", OwnerId = "u1" },
                new Cat { Id = 2, Name = "Shadow", Breed = "Black Cat", Description = "Sneaky black cat", OwnerId = "u2" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var (cats, totalCount) = await catService.GetFeaturedCatsAsync("mIsTy", 1, 10);

            // Assert
            Assert.Equal(1, totalCount);
            Assert.Single(cats);
            Assert.Equal("Misty", cats.First().Name);
        }

        // Tests Pagination: Verifies that skipping and taking records works properly
        [Fact]
        public async Task GetFeaturedCatsAsync_ShouldReturnCorrectPaginatedResult()
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "Cat1", Breed = "Breed1", Description = "Description 1", OwnerId = "u1" },
                new Cat { Id = 2, Name = "Cat2", Breed = "Breed2", Description = "Description 2", OwnerId = "u1" },
                new Cat { Id = 3, Name = "Cat3", Breed = "Breed3", Description = "Description 3", OwnerId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act (Page 2, Size 2 - should return exactly 1 item: Cat3)
            var result = await catService.GetFeaturedCatsAsync(searchTerm: null, currentPage: 2, pageSize: 2);

            // Assert
            Assert.Equal(3, result.TotalCount);
            Assert.Single(result.Cats);
            Assert.Equal("Cat3", result.Cats.First().Name);
        }

        // Tests Update functionality: Verifies that existing database records are modified via AutoMapper
        [Fact]
        public async Task UpdateCatAsync_ShouldModifyExistingRecord()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 5,
                Name = "Old Name",
                Breed = "Old Breed",
                Description = "Old valid description",
                OwnerId = "u"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            var editModel = new CatEditInputModel
            {
                Id = 5,
                Name = "New Name",
                Breed = "New Breed",
                Description = "New valid description",
                OwnerId = "u"
            };

            // Act
            await catService.UpdateCatAsync(editModel);

            dbContext.Entry(cat).Reload();

            // Assert
            Assert.Equal("New Name", cat.Name);
            Assert.Equal("New Breed", cat.Breed);
            Assert.Equal("New valid description", cat.Description);
        }

        // Tests Hard Delete: Verifies complete removal of the entity from the database
        [Fact]
        public async Task DeleteCatPermanentlyAsync_ShouldRemoveRecordFromDb()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 8,
                Name = "DeleteMe",
                Breed = "Test Breed",
                Description = "This cat will be deleted",
                OwnerId = "u"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            dbContext.ChangeTracker.Clear();

            // Act
            await catService.DeleteCatPermanentlyAsync(8);
            var result = await dbContext.Cats.FindAsync(8);

            // Assert
            Assert.Null(result);
        }

        // Tests IgnoreQueryFilters: Verifies that retrieving details works even if global filters exist
        [Fact]
        public async Task GetCatDetailsAsync_ShouldReturnViewModel_WhenCatExists()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 100,
                Name = "Garfield",
                Breed = "Tabby",
                Description = "Loves lasagna and hates mondays",
                OwnerId = "u"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetCatDetailsAsync(100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Garfield", result.Name);
            Assert.Equal("Tabby", result.Breed);
            Assert.Equal("Loves lasagna and hates mondays", result.Description);
        }

        // Tests retrieving data for dropdown menus: Should map to CatSelectViewModel
        [Fact]
        public async Task GetAllCatsForSelectAsync_ShouldReturnMappedViewModels()
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "Topcho", Breed = "Breed 1", Description = "Description 1", OwnerId = "u1" },
                new Cat { Id = 2, Name = "Mishy", Breed = "Breed 2", Description = "Description 2", OwnerId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetAllCatsForSelectAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.IsAssignableFrom<IEnumerable<CatSelectViewModel>>(result);
            Assert.Contains(result, c => c.Name == "Topcho");
        }

        // Tests mapping for Edit form: Should return populated CatEditInputModel
        [Fact]
        public async Task GetCatForEditAsync_ShouldReturnEditModel_WhenCatExists()
        {
            // Arrange
            var cat = new Cat
            {
                Id = 1,
                Name = "Topcho",
                Breed = "Tuxie",
                Description = "A valid test description",
                OwnerId = "u1"
            };
            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetCatForEditAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Topcho", result.Name);
            Assert.Equal("A valid test description", result.Description);
            Assert.Equal("u1", result.OwnerId);
        }

        // Tests mapping for Delete view: Specially checks if 'HasPhotos' logic from AutoMapper works
        [Fact]
        public async Task GetCatForDeleteAsync_ShouldReturnDeleteViewModel_WithCorrectPhotoStatus()
        {
            // Arrange
            var catWithPhoto = new Cat
            {
                Id = 1,
                Name = "Topcho",
                Breed = "Tuxie",
                Description = "A valid test description",
                OwnerId = "u1",
                Photos = new List<Photo>
        {
            new Photo
            {
                Id = Guid.NewGuid(),
                FilePath = "test.jpg",
                Caption = "Required Caption",
                PublisherId = "u1"
            }
        }
            };
            await dbContext.Cats.AddAsync(catWithPhoto);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetCatForDeleteAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Topcho", result.Name);
            Assert.True(result.HasPhotos);
        }

        // Tests Admin list retrieval: Should return all cats mapped to CatCardViewModel
        [Fact]
        public async Task GetAllCatsForAdminAsync_ShouldReturnAllCatsMappedToCard()
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "Active Cat", Breed = "Breed 1", Description = "Valid Description 1", IsActive = true, OwnerId = "u1" },
                new Cat { Id = 2, Name = "Archived Cat", Breed = "Breed 2", Description = "Valid Description 2", IsActive = false, OwnerId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetAllCatsForAdminAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.IsAssignableFrom<IEnumerable<CatCardViewModel>>(result);
        }

        // Tests safe execution: Service should not throw exception if updating non-existent cat
        [Fact]
        public async Task UpdateCatAsync_ShouldNotThrowException_WhenCatDoesNotExist()
        {
            // Arrange
            var editModel = new CatEditInputModel
            {
                Id = 999,
                Name = "Ghost",
                Breed = "None",
                Description = "Valid Description",
                OwnerId = "test_user"
            };

            // Act
            var exception = await Record.ExceptionAsync(() => catService.UpdateCatAsync(editModel));

            // Assert
            Assert.Null(exception);
        }

        // Tests safe execution: Service should not throw exception if archiving non-existent cat
        [Fact]
        public async Task ArchiveCatAsync_ShouldNotThrowException_WhenCatDoesNotExist()
        {
            // Act
            var exception = await Record.ExceptionAsync(() => catService.ArchiveCatAsync(999));

            // Assert
            Assert.Null(exception);
        }

        // Tests safe execution: Service should not throw exception if restoring non-existent cat
        [Fact]
        public async Task RestoreArchivedCatAsync_ShouldNotThrowException_WhenCatDoesNotExist()
        {
            // Act
            var exception = await Record.ExceptionAsync(() => catService.RestoreArchivedCatAsync(999));

            // Assert
            Assert.Null(exception);
        }

        // Tests Search functionality: What happens if we search for a term that matches nothing?
        [Fact]
        public async Task GetFeaturedCatsAsync_ShouldReturnEmpty_WhenSearchTermMatchesNothing()
        {
            // Arrange
            await dbContext.Cats.AddAsync(new Cat { Id = 1, Name = "Misty", Breed = "Siamese", Description = "Test description", OwnerId = "u1" });
            await dbContext.SaveChangesAsync();

            // Act
            var (cats, totalCount) = await catService.GetFeaturedCatsAsync("NonExistentName", 1, 10);

            // Assert
            Assert.Equal(0, totalCount);
            Assert.Empty(cats);
        }

        // Tests Search functionality: What happens if the search term is empty or whitespace?
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task GetFeaturedCatsAsync_ShouldReturnAllCats_WhenSearchTermIsEmpty(string? searchTerm)
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "A", Breed = "B", Description = "Description 1", OwnerId = "u1" },
                new Cat { Id = 2, Name = "C", Breed = "D", Description = "Description 2", OwnerId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var (cats, totalCount) = await catService.GetFeaturedCatsAsync(searchTerm, 1, 10);

            // Assert
            Assert.Equal(2, totalCount);
        }

        // Tests mapping logic: Ensure GetAllCatsForAdminAsync includes archived (inactive) cats
        [Fact]
        public async Task GetAllCatsForAdminAsync_ShouldReturnBothActiveAndInactiveCats()
        {
            // Arrange
            await dbContext.Cats.AddRangeAsync(
                new Cat { Id = 1, Name = "Active", Breed = "B", Description = "Desc", IsActive = true, OwnerId = "u1" },
                new Cat { Id = 2, Name = "Inactive", Breed = "B", Description = "Desc", IsActive = false, OwnerId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var result = await catService.GetAllCatsForAdminAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => !c.IsActive);
            Assert.Contains(result, c => c.IsActive);
        }
    }
}
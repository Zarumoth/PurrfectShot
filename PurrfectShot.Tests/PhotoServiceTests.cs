using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using Moq;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data;
using PurrfectShot.Web.Infrastructure.Profiles;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using System.Text;

namespace PurrfectShot.Tests
{
    public class PhotoServiceTests
    {
        private readonly PurrfectShotDbContext dbContext;
        private readonly PhotoService photoService;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<PhotoService>> mockLogger;
        private readonly Mock<IConfiguration> mockConfig;

        public PhotoServiceTests()
        {
            // 1. InMemory DB Setup
            var options = new DbContextOptionsBuilder<PurrfectShotDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new PurrfectShotDbContext(options);

            // 2. AutoMapper Setup
            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<PhotoProfile>();
            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            this.mapper = mapperConfig.CreateMapper();

            // 3. Mock
            this.mockLogger = new Mock<ILogger<PhotoService>>();

            this.mockConfig = new Mock<IConfiguration>();
            this.mockConfig.Setup(c => c["Cloudinary:CloudName"]).Returns("test");
            this.mockConfig.Setup(c => c["Cloudinary:ApiKey"]).Returns("test");
            this.mockConfig.Setup(c => c["Cloudinary:ApiSecret"]).Returns("test");

            // 4. Service Instantiation
            this.photoService = new PhotoService(this.dbContext, this.mapper, this.mockLogger.Object, this.mockConfig.Object);
        }

        // ==========================================
        // UPLOAD & FILE SYSTEM TESTS
        // ==========================================

        //// Tests successful photo upload: Mocks an IFormFile, uses a temp directory, and checks DB insertion
        //[Fact]
        //public async Task UploadPhotoAsync_ShouldSaveFileAndCreateDatabaseRecord()
        //{
        //    // Arrange
        //    string tempWwwRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        //    var userId = "test-user";

        //    // Mocking IFormFile (e.g. a .jpg image)
        //    var fileMock = new Mock<IFormFile>();
        //    var content = "Fake image content";
        //    var fileName = "test-cat.jpg";
        //    var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));

        //    fileMock.Setup(_ => _.FileName).Returns(fileName);
        //    fileMock.Setup(_ => _.Length).Returns(ms.Length);
        //    fileMock.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
        //            .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream));

        //    var inputModel = new PhotoInputModel
        //    {
        //        CatId = 1,
        //        Caption = "A very beautiful photo caption",
        //        ImageFile = fileMock.Object
        //    };

        //    // Act
        //    await photoService.UploadPhotoAsync(inputModel, userId, tempWwwRoot);
        //    var photoInDb = await dbContext.Photos.FirstOrDefaultAsync();

        //    // Assert
        //    Assert.NotNull(photoInDb);
        //    Assert.Equal(1, photoInDb.CatId);
        //    Assert.Equal("A very beautiful photo caption", photoInDb.Caption);
        //    Assert.Equal(userId, photoInDb.PublisherId);
        //    Assert.Contains(".jpg", photoInDb.FilePath);

        //    // Cleanup the temp folder
        //    if (Directory.Exists(tempWwwRoot)) Directory.Delete(tempWwwRoot, true);
        //}

        // Tests validation: Uploading a file with an invalid extension should throw InvalidOperationException
        [Fact]
        public async Task UploadPhotoAsync_ShouldThrowException_WhenFileExtensionIsInvalid()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.FileName).Returns("test-file.pdf"); // Invalid extension
            fileMock.Setup(_ => _.Length).Returns(100);

            var inputModel = new PhotoInputModel { CatId = 1, Caption = "Test caption here", ImageFile = fileMock.Object };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => photoService.UploadPhotoAsync(inputModel, "u1"));

            Assert.Contains("Невалиден формат", exception.Message);
        }

        // ==========================================
        // PHOTO DETAILS & FAVORITES TESTS
        // ==========================================

        // Tests details retrieval: Updated to use ApplicationUser
        [Fact]
        public async Task GetPhotoDetailsAsync_ShouldReturnCorrectViewModel_WithFavoriteStatus()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var userId = "user-1";

            var cat = new Cat { Id = 1, Name = "Tom", Breed = "Mix", Description = "Test description", OwnerId = "u2" };

            // Using ApplicationUser
            var photo = new Photo
            {
                Id = photoId,
                FilePath = "/images/test.jpg",
                Caption = "Test caption here",
                CatId = 1,
                Cat = cat,
                PublisherId = userId,
                Publisher = new ApplicationUser { Id = userId, UserName = "Pesho" },
                DateUploaded = new DateTime(2023, 5, 10)
            };

            await dbContext.Cats.AddAsync(cat);
            await dbContext.Photos.AddAsync(photo);
            await dbContext.UserFavoritePhotos.AddAsync(new UserFavoritePhoto { PhotoId = photoId, UserId = userId });
            await dbContext.SaveChangesAsync();

            // Act
            var result = await photoService.GetPhotoDetailsAsync(photoId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tom", result.CatName);
            Assert.True(result.IsFavorite);
            Assert.Equal("Pesho", result.PublisherName);
        }

        // Tests toggling favorites: Should add if not favorite, remove if already favorite
        [Fact]
        public async Task ToggleFavoriteAsync_ShouldAddOrRemoveFavoriteCorrectly()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var userId = "user-1";

            // Act 1: Toggle ON (Add)
            var isAdded = await photoService.ToggleFavoriteAsync(photoId, userId);
            var existsInDbAfterAdd = await dbContext.UserFavoritePhotos.AnyAsync();

            // Act 2: Toggle OFF (Remove)
            var isAddedSecondTime = await photoService.ToggleFavoriteAsync(photoId, userId);
            var existsInDbAfterRemove = await dbContext.UserFavoritePhotos.AnyAsync();

            // Assert
            Assert.True(isAdded);
            Assert.True(existsInDbAfterAdd);

            Assert.False(isAddedSecondTime);
            Assert.False(existsInDbAfterRemove);
        }

        // ==========================================
        // PROFILE PICTURE & CAT MODIFICATION TESTS
        // ==========================================

        // Tests setting a profile picture: Updates the Cat's MainPhotoId
        [Fact]
        public async Task SetProfilePicture_ShouldUpdateCatMainPhotoId()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var cat = new Cat { Id = 10, Name = "Garfield", Breed = "Tabby", Description = "Valid description", OwnerId = "u1" };
            var photo = new Photo { Id = photoId, CatId = 10, FilePath = "test.jpg", Caption = "Valid caption test", PublisherId = "u1" };

            await dbContext.Cats.AddAsync(cat);
            await dbContext.Photos.AddAsync(photo);
            await dbContext.SaveChangesAsync();

            // Act
            await photoService.SetProfilePicture(photoId);

            // Assert
            var updatedCat = await dbContext.Cats.FindAsync(10);
            Assert.Equal(photoId, updatedCat!.MainPhotoId);
        }

        // ==========================================
        // VOTING TESTS
        // ==========================================

        // Tests voting logic: Fixed the assertion sequence to handle reference updates
        [Fact]
        public async Task VoteForPhotoAsync_ShouldAddNewVote_OrUpdateExisting()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var userId = "user-voter";
            var voteModel = new VoteInputModel { PhotoId = photoId, Stars = 5 };

            // Act 1: First Vote (5 stars)
            await photoService.VoteForPhotoAsync(voteModel, userId, "Pesho");

            // Check immediately to avoid reference issues later
            var firstVoteValue = await dbContext.Votes.AsNoTracking().FirstOrDefaultAsync(v => v.UserId == userId);
            Assert.NotNull(firstVoteValue);
            Assert.Equal(5, firstVoteValue.Stars);

            // Act 2: Update Vote (Change to 3 stars)
            voteModel.Stars = 3;
            await photoService.VoteForPhotoAsync(voteModel, userId, "Pesho");

            // Assert 2: Check update
            var updatedVoteValue = await dbContext.Votes.AsNoTracking().FirstOrDefaultAsync(v => v.UserId == userId);
            var totalVotesInDb = await dbContext.Votes.CountAsync();

            Assert.Equal(1, totalVotesInDb);
            Assert.Equal(3, updatedVoteValue!.Stars);
        }

        // ==========================================
        // CALENDAR TESTS
        // ==========================================

        // Tests calendar grouping: Should group photos by Year and Month correctly
        [Fact]
        public async Task GetCalendarMonthsAsync_ShouldGroupPhotosByYearAndMonth()
        {
            // Arrange
            await dbContext.Photos.AddRangeAsync(
                new Photo { Id = Guid.NewGuid(), DateUploaded = new DateTime(2024, 1, 15), FilePath = "p1.jpg", Caption = "Test caption 1", PublisherId = "u1" },
                new Photo { Id = Guid.NewGuid(), DateUploaded = new DateTime(2024, 1, 20), FilePath = "p2.jpg", Caption = "Test caption 2", PublisherId = "u1" },
                new Photo { Id = Guid.NewGuid(), DateUploaded = new DateTime(2024, 2, 10), FilePath = "p3.jpg", Caption = "Test caption 3", PublisherId = "u1" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var result = await photoService.GetCalendarMonthsAsync();
            var list = result.ToList();

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Equal(2, list.First(m => m.Month == 1).PhotoCount);
            Assert.Equal(1, list.First(m => m.Month == 2).PhotoCount);
        }

        // ==========================================
        // CRUD & DELETION TESTS
        // ==========================================

        // Tests Update functionality
        [Fact]
        public async Task UpdatePhotoAsync_ShouldModifyCaption()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var photo = new Photo { Id = photoId, Caption = "Old caption", FilePath = "p.jpg", PublisherId = "u1" };
            await dbContext.Photos.AddAsync(photo);
            await dbContext.SaveChangesAsync();

            var editModel = new PhotoEditInputModel { Id = photoId, Caption = "New updated caption", CatId = 1, CatName = "N", ImageUrl = "I" };

            // Act
            await photoService.UpdatePhotoAsync(editModel);
            dbContext.ChangeTracker.Clear();
            var updatedPhoto = await dbContext.Photos.FindAsync(photoId);

            // Assert
            Assert.Equal("New updated caption", updatedPhoto!.Caption);
        }

        // Tests Delete functionality: Removes from DB and handles non-existent file gracefully
        [Fact]
        public async Task DeletePhotoAsync_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            var photoId = Guid.NewGuid();
            var photo = new Photo { Id = photoId, CatId = 1, FilePath = "https://cloud.com/p.jpg", Caption = "Test", PublisherId = "u1" };
            await dbContext.Photos.AddAsync(photo);
            await dbContext.SaveChangesAsync();

            // Act (МАХНАХМЕ wwwrootPath)
            var returnedCatId = await photoService.DeletePhotoAsync(photoId);

            // Assert
            Assert.Null(await dbContext.Photos.FindAsync(photoId));
        }


        // Tests Global Statistics logic
        [Fact]
        public async Task GetGlobalStatisticsAsync_ShouldReturnCorrectCounts()
        {
            // Arrange
            await dbContext.Photos.AddAsync(new Photo { Id = Guid.NewGuid(), FilePath = "p1.jpg", Caption = "Valid caption test", PublisherId = "u1" });
            await dbContext.Votes.AddRangeAsync(
                new Vote { Id = 111, Stars = 5, VoterName = "u1", UserId = "u1" },
                new Vote { Id = 222, Stars = 4, VoterName = "u2", UserId = "u2" }
            );
            await dbContext.SaveChangesAsync();

            // Act
            var (totalPhotos, totalVotes) = await photoService.GetGlobalStatisticsAsync();

            // Assert
            Assert.Equal(1, totalPhotos);
            Assert.Equal(2, totalVotes);
        }
    }
}
using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.AppServices.MoviesFeature;
using GRA.Application.AutoMapper.MoviesFeature;
using GRA.Application.ViewModels.MoviesFeature;
using GRA.Domain.Core.Handlers;
using GRA.Domain.Core.Requests;
using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Interfaces.Repositories.Write;
using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.ReadOnly.MoviesFeature;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Write.MoviesFeature;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GRA.Tests.AppService
{
    public class MovieAppServiceTest
    {
        private readonly IMovieAppService _movieAppService;
        private readonly DatabaseContext _context;

        public MovieAppServiceTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "myDatabase")
                .Options;

            _context = new DatabaseContext(options);

            var services = new ServiceCollection();
            services.AddScoped<IMovieRepositoryReadOnly, MovieRepositoryReadOnly>();
            services.AddScoped<IMovieRepositoryWrite, MovieRepositoryWrite>();
            services.AddScoped<IMovieAppService, MovieAppService>();
            services.AddScoped(_ => _context);

            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<INotificationHandler<DomainNotificationRequest>, DomainNotificationHandler>();

            services.AddAutoMapper(typeof(MovieMapping));

            var serviceProvider = services.BuildServiceProvider();

            _movieAppService = serviceProvider.GetService<IMovieAppService>();
        }

        [Fact]
        public async Task Insert_Update_Delete_GetAll_Get_By_Id_Valids()
        {
            // Arrange
            RegisterMovieViewModel viewModelInsert = new RegisterMovieViewModel
            {
                Title = "Title Test",
                Producer = "Producer Test",
                Studio = "Studio Test",
                Year = 2010,
                IsWinner = true
            };

            UpdateMovieViewModel viewModelUpdate = new UpdateMovieViewModel
            {
                Title = "Title Test Updated",
                Producer = "Producer Test Test",
                Studio = "Studio Test Test",
                Year = 2015,
                IsWinner = true
            };


            // Action
            var resultInsert = await _movieAppService.InsertAsync(viewModelInsert);

            var resultGetAll = await _movieAppService.GetAllAsync();

            var resultGetById = await _movieAppService.GetByIdAsync(resultGetAll.First().Id);

            viewModelUpdate.Id = resultGetById.Id;
            var resultUpdate = await _movieAppService.UpdateAsync(viewModelUpdate);

            var resultDelete = _movieAppService.DeleteById(viewModelUpdate.Id);

            // Assert
            Assert.NotNull(resultInsert);
            Assert.NotEqual(0, resultInsert.Value);

            Assert.NotNull(resultInsert);
            Assert.NotEqual(0, resultInsert.Value);

            Assert.NotNull(resultGetAll);

            Assert.NotNull(resultGetById);

            Assert.NotNull(resultUpdate);
            Assert.NotEqual(0, resultUpdate.Value);

            Assert.True(resultDelete);
        } 
    }
}

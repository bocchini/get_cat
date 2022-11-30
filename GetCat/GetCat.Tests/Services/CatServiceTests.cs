using System.Net;
using NSubstitute;
using Newtonsoft.Json;
using FluentAssertions;
using GetCat.Application.DTO;
using GetCat.Application.Wrapper;
using GetCat.Application.Services;
using GetCat.Application.Services.Interfaces;

namespace GetCat.Tests
{
    public class CatServiceTests
    {
        private readonly IHttpClientWrapper _clientWrapper;
        private readonly ICatService _catService;
        private readonly int page = 1;
        private readonly int limit = 1;

        public CatServiceTests()
        {
            _clientWrapper = Substitute.For<IHttpClientWrapper>();
            _catService = new CatService(_clientWrapper);
        }

        [Fact]
        public async void Deve_RetornarVazio_QuandoNaoStatusCode_NaoForSucesse()
        {
             _clientWrapper.GetAsync(Arg.Any<string>()).Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            });

            var result = await _catService.Get(limit, page);

            result.Should().BeEmpty();
        }

        [Fact]
        public async void Deve_Retornar_ListaDeGatos()
        {
            var cats = new List<CatsDto>()
            {
                new CatsDto() 
                {
                    id = "a1",
                    url = "http://cats.com",
                    height = 120,
                    width = 100
                },
                new CatsDto()
                {
                    id = "a21",
                    url = "http://cats.com.br",
                    height = 110,
                    width = 100
                },
            };
            _clientWrapper.GetAsync(Arg.Any<string>()).Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(cats))
            });

            var result = await _catService.Get(limit, page);

            result.Should().BeEquivalentTo(cats);
        }

        [Fact]
        public async void Deve_Retornar_Vazio_SeResultVirEmpty()
        {
            var page = 1;
            var limit = 1;
            _clientWrapper.GetAsync(Arg.Any<string>()).Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });

            var result = await _catService.Get(limit, page);

            result.Should().BeEmpty();
        }
    }
}
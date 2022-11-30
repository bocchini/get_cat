using NSubstitute;
using Newtonsoft.Json;
using FluentAssertions;
using GetCat.Controllers;
using GetCat.Application.DTO;
using GetCat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static NSubstitute.Arg;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace GetCat.Tests.Controllers;

public class CatControllerTests
{
    private readonly CatController _catController;
    private readonly ICatService _catService;

    public CatControllerTests()
    {
        _catService = Substitute.For<ICatService>();
        _catController = new CatController(_catService);
    }

    [Fact]
    public async void Deve_Retornar_Empty()
    {
        _catService.Get(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<CatsDto>());
        var response = (OkObjectResult)await _catController.Get(1, 1);

        using (new AssertionScope())
        {
            response.ContentTypes.Should().BeEmpty();
            response.StatusCode.Should().Be(200);
        }
    }

    [Fact]
    public async void Deve_Retornar_Nullo()
    {
        _catService.Get(Arg.Any<int>(), Arg.Any<int>()).Returns(Task.FromResult<IList<CatsDto>
            >(null));
        var response = (NotFoundObjectResult)await _catController.Get(1, 1);
     
        using (new AssertionScope())
        {
            response.ContentTypes.Should().BeEmpty();
            response.Value.Should().Be("Nenhum gato encontrado");
            response.StatusCode.Should().Be(404);
        }
    }

    public async void Deve_Retornar_ListaGatos()
    {
        var cats = new List<CatsDto>()
        {
            new CatsDto
            {
                id = "asd",
                url = "http://cats.com",
                height = 110,
                width = 50
            },
            new CatsDto
            {
                id = "gfd",
                url = "http://cats.com.br",
                height = 120,
                width = 80
            }
        };

        _catService.Get(Arg.Any<int> (), Arg.Any<int>())
            .Returns(cats);

        var result = await _catController.Get(1, 1);
        result.Should().BeEquivalentTo(cats);
    }
}

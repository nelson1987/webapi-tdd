using System.Net.Http.Json;
using Application;
using Domain;
using FluentAssertions;
using Presentation;
namespace IntegrationTests;

public class UsuarioControllerUnitTest : BaseIntegrationTest
{
    public UsuarioControllerUnitTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Get_ListarTodos_RetornaSucesso()
    {
        var response = await _client.GetAsync("/weatherforecast");
        // Assert
        response.Should().BeSuccessful();
        var users = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
        users.Should().NotBeNull();
        users.Should().NotBeEmpty();
    }

    [Fact(Skip = "Integration Tests")]
    public async Task Post_DadosValido_RetornaCreated()
    {
        var response = await _client.PostAsync(Constantes.POST_USUARIO_PATH, null);
        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", result);
    }

    [Fact]
    public async Task Post_DadosValido_SalvaNaTabela()
    {
        var command = new CreateUser.Command("email@email.com", "123456");
        var response = await _client.PostAsync(Constantes.POST_USUARIO_PATH, null);
        var result = await response.Content.ReadAsStringAsync();

        var product = DbContext
            .Set<Usuario>()
            .FirstOrDefault(p => p.Email == command.Email);

        Assert.NotNull(product);
    }

    public async Task Post_DadosInvalidos_RetornaBadRequest()
    {
        throw new NotImplementedException();
    }

    public async Task Post_DadosInvalidos_RetornaErro()
    {
        throw new NotImplementedException();
    }
}
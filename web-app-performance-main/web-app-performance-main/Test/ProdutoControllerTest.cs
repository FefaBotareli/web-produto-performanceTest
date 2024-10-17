using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoRepository> _productRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _productRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ListarProdutosOk()
        {
            //arrange 
            var produtos = new List<Produto>() {
                new Produto()
                {
                    QuantidadeEstoque = 1,
                    Id = 1,
                    Nome = "Livro azul",
                    Preco = 5,00
                }
            };
            _productRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);


            //Act
            var result = await _controller.GetProduto();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornaNotFound()
        {
            _productRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProduto();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            //arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Livro azul",
                Preco = 5,
                QuantidadeEstoque = 1

            };
            _productRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await _controller.Post(produto);

            //Asserts
            _produtoRepositoryMock
          .Verify(u => u.SalvarUsuario(It.IsAny<Produto>()), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

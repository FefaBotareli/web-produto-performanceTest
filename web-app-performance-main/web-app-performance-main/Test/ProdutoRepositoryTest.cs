using Moq;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class ProdutooRepositoryTest
    {
        [Fact]
        public async Task ListarProdutos()
        {
            //Arrange
            var produtos = new List<Produto>() {
                new Produto()
                {
                    Id = 1,
                    Nome = "Livro azul",
                    Preco = 5,
                    QuantidadeEstoque = 1
                },
                 new Produto()
                {
                    Id = 2
                    Nome = "Livro vermelho",
                    Preco = 7,
                    QuantidadeEstoque = 3
                }
            };
            var productRepositoryMock = new Mock<IProdutoRepository>();
            productRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync(produtos);
            var productRepository = productRepositoryMock.Object;

            //Act 
            var result = await productRepository.ListarProdutos();

            //Assert
            Assert.Equal(produtos, result);
        }


        [Fact]
        public async Task SalvarProduto()
        {
            //arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Livro azul",
                Preco = 5,
                QuantidadeEstoque = 1
            };
            var productRepositoryMock = new Mock<IProdutoRepository>();
            productRepositoryMock
                .Setup(u => u.SalvarProduto(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);
            var productRepository = productRepositoryMock.Object;

            //act
            await productRepository.SalvarProduto(produto);

            //assert
            productRepositoryMock
          .Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }

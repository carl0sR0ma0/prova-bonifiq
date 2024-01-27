using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
        /// <summary>
        /// Precisamos fazer algumas alterações:
        /// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
        /// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
        /// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
        /// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
        /// 
        /// Resposta: Para criar uma estrutura melhor sem repetição de código utilizando a injeção de dependência eu criaria uma Interface ‘IService’ e definiria os métodos:
		///			      * ReturnList<Product> List(int page);
		///			      * ReturnList<Customer> List(int page);
		///			  Em seguida criaria a classe Service e implementaria os métodos da interface. Porém, eu acredito que não seja uma prática legal, pois iria
		///			  quebrar o principio do Single Responsibility Principle (SOLID), no qual ser iriamos deixar de ter uma serviço especifico para cada classe.
        /// </summary>
        TestDbContext _ctx;
		private readonly IService<Product> _productService;
		private readonly IService<Customer> _customerService;

		public Parte2Controller(TestDbContext ctx)
		{
			_ctx = ctx;
			_productService = new ProductService(ctx);
            _customerService = new CustomerService(ctx);
		}
	
		[HttpGet("products")]
		public ReturnList<Product> ListProducts(int page)
		{
			return _productService.List(page);
        }

		[HttpGet("customers")]
		public ReturnList<Customer> ListCustomers(int page)
		{
			return _customerService.List(page);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Models.DTO;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProduct() 
        {
            var product = productRepository.GetAllProduct();
            return Ok(mapper.Map<List<ProductDTO>>(product));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
           var product = productRepository.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (productRepository.ProductExists(productDTO.Name))
            {
                ModelState.AddModelError(string.Empty, $"ya existe un producto con el nombre {productDTO.Name}");
                return StatusCode(404, ModelState);
            }

            var product = mapper.Map<Product>(productDTO);

            if (!productRepository.CreateProduct(product))
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar guardar el producto {productDTO.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProduct", new {id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id) return BadRequest(ModelState);

            var product = mapper.Map<Product>(productDTO);
            if (!productRepository.UpdateProduct(product))
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar actualizar el producto {productDTO.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (!productRepository.ProductExists(id)) return NotFound();

            var product = productRepository.GetProduct(id);

            if (!productRepository.DeleteProduct(product))
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar eliminar el producto {product.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

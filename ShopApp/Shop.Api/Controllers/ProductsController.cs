using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Dtos.ProductDtos;
using Shop.Core.Repositories;
using Shop.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,IBrandRepository brandRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create(ProductPostDto postDto)
        {
            if(!_brandRepository.IsExist(x=>x.Id == postDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "BrandId is not correct");
                return BadRequest(ModelState);
            }

            Product product = _mapper.Map<Product>(postDto);

            _productRepository.Add(product);
            _productRepository.Commit();
            return StatusCode(201, new { Id = product.Id });
        }
        [HttpGet("all")]
        public ActionResult<List<ProductGetAllItemDto>> GetAll()
        {
            var data = _mapper.Map<List<ProductGetAllItemDto>>(_productRepository.GetAll(x => true, "Brand"));

            return Ok(data);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id,ProductPutDto putDto)
        {
            Product entity = _productRepository.Get(x=>x.Id == id);

            if (entity == null) return NotFound();

            if (!_brandRepository.IsExist(x => x.Id == putDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "BrandId is not correct");
                return BadRequest(ModelState);
            }

            entity.BrandId = putDto.BrandId;
            entity.Name = putDto.Name;
            entity.SalePrice= putDto.SalePrice;
            entity.DiscountPercent= putDto.DiscountPercent;
            entity.CostPrice= putDto.CostPrice;

            _productRepository.Commit();

            return NoContent();
        }


        [HttpGet("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {
            Product entity = _productRepository.Get(x => x.Id == id,"Brand");
            if (entity == null) return NotFound();

            var data = _mapper.Map<ProductGetDto>(entity);

            return Ok(data);
        }
    }
}

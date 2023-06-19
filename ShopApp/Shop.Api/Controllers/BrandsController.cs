using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Dtos.BrandDtos;
using Shop.Core.Entities;
using Shop.Core.Repositories;
using Shop.Data;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandsController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create(BrandPostDto postDto)
        {
            Brand brand = _mapper.Map<Brand>(postDto);

            _brandRepository.Add(brand);
            _brandRepository.Commit();

            return StatusCode(201, new { brand.Id });
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, BrandPutDto putDto)
        {
            Brand brand = _brandRepository.Get(x => x.Id == id);

            if (brand == null)
                return NotFound();

            brand.Name = putDto.Name;
            _brandRepository.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Brand brand = _brandRepository.Get(x => x.Id == id);

            if (brand == null)
                return NotFound();

            _brandRepository.Remove(brand);
            _brandRepository.Commit();


            return NoContent();
        }

        [HttpGet("all")]
        public ActionResult<List<BrandGetAllItemDto>> GetAll()
        {
            var data = _mapper.Map<List<BrandGetAllItemDto>>(_brandRepository.GetAll(x => true));

            return Ok(data);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Data.IRepository;
using StudentAdminPortal.API.Models.DomainModels;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("gender")]
    public class GendersController : Controller
    {
        private readonly IRepositoryGender _repository;
        private readonly IMapper _mapper;

        public GendersController(IRepositoryGender repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllGenders() {
            var genders = await _repository.GetAllGenders();
            return Ok(_mapper.Map<IEnumerable<Gender>>(genders));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Data.IRepository;
using AutoMapper;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentsController : Controller
    {
        private readonly IRepositoryStudent _repositoryStudent;
        private readonly IMapper _mapper;

        public StudentsController(IRepositoryStudent repositoryStudent, IMapper mapper) {
            _repositoryStudent = repositoryStudent;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllStudent(){
            var students =  await _repositoryStudent.GetAllStudent();
            return Ok(_mapper.Map<IEnumerable<Student>>(students));
        }

        [HttpGet]
        [Route("{name?}")]
        public async Task<ActionResult> GetStudentByName(string name){
            var studentsByName = await _repositoryStudent.GetStudentByName(name);
            return Ok(_mapper.Map<IEnumerable<Student>>(studentsByName));
        }
    }
}
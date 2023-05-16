using System.Net;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Models.DomainModels;
using Entity = StudentAdminPortal.API.Models.Entites;
using StudentAdminPortal.API.Data.IRepository;
using AutoMapper;
using StudentAdminPortal.API.Models.RequestModels;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("student")]
    public class StudentsController : Controller
    {
        private readonly IRepositoryStudent _repositoryStudent;
        private readonly IMapper _mapper;
        private readonly IRepositoryImage _repositoryImage;

        public StudentsController(
            IRepositoryStudent repositoryStudent, 
            IMapper mapper,
            IRepositoryImage repositoryImage
        ) {
            _repositoryStudent = repositoryStudent;
            _mapper = mapper;
            _repositoryImage = repositoryImage;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllStudents()
        {
            var students =  await _repositoryStudent.GetAllStudent();
            return Ok(_mapper.Map<IEnumerable<Student>>(students));
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> GetStudentsByName(string name)
        {
            var studentsByName = await _repositoryStudent.GetStudentByName(name);
            return Ok(_mapper.Map<IEnumerable<Student>>(studentsByName));
        }

        [HttpGet]
        [Route("id/{id?}")]
        public async Task<ActionResult> GetSudentById(Guid id) 
        {
            var student = await _repositoryStudent.GetStudentById(id);
            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("id/{id}")]
        public async Task<ActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequest request) 
        {
            if (await _repositoryStudent.ExistStudent(id)) {
                var student = await _repositoryStudent.UpdateStudent(id, _mapper.Map<Entity.Student>(request));

                if (student != null) {
                    return Ok(_mapper.Map<Student>(student));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("id/{id}")]
        public async Task<ActionResult> DeleteStudent(Guid id) 
        {
            if (await _repositoryStudent.ExistStudent(id)) {
                var result = await _repositoryStudent.DeleteStudent(id);
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateStudent([FromBody] AddStudentRequest request)
        {
            var student = await _repositoryStudent.CreateStudent(_mapper.Map<Entity.Student>(request));
            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("upload-image/{id}")]
        public async Task<ActionResult> UploadImage(Guid id, IFormFile profileImage) 
        {   
            if (profileImage != null && profileImage.Length > 0) {
                if (await _repositoryStudent.ExistStudent(id)) {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(profileImage.FileName)}";
                    var filePath = await _repositoryImage.Upload(profileImage, fileName);
                    if ( await _repositoryStudent.UploadImage(id, filePath))
                    {
                        return Ok(filePath);
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in uploading image");
                }
                return BadRequest("This is not a valid image format");
            }

            return NotFound();
        }

        [HttpPut]
        [Route("upload-image/{id}")]
        public async Task<ActionResult> UploadImageBase64(Guid id, IFormFile profile) {
            if (await _repositoryStudent.ExistStudent(id)) {
                return Ok(await _repositoryStudent.UploadImageBase64(id, profile));
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error in upload image base64");
        }

    }
}
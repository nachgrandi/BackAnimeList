using Microsoft.AspNetCore.Mvc;
using AnimeList.Models;
using AnimeList.Repository;
using System.Collections.Generic;

namespace AnimeList.Controllers
{
    [Route("animelist/[controller]")]
    [ApiController]
    public abstract class BaseController<T>: ControllerBase where T: BaseModel
    {
        protected IRepository<T> _repo {get;set;}
        public BaseController(IRepository<T> repo){
            this._repo=repo;
        }

        [HttpGet]
        public virtual ActionResult<List<T>> GetAll(){
            if(!ModelState.IsValid) return BadRequest();
            var result= _repo.FindAll();
            if(result==null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual ActionResult<T> GetById(int id){
            if(!ModelState.IsValid) return BadRequest();
            var result=_repo.FindById(id);
            if(result==null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public virtual ActionResult CreateUpdate(T entity){
            if(!ModelState.IsValid) return BadRequest();
            if (entity.Id > 0)
                _repo.Update(entity);
            else
                _repo.Create(entity);
            return Ok();
        }

        
    }
}
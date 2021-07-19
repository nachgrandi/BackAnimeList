using AnimeList.Models;
using AnimeList.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AnimeList.Controllers
{   
    public class AnimeController: BaseController<Anime>
    {
        public AnimeRepository<AnimeListDbContext> context {get;set;}
        public AnimeController(AnimeRepository<AnimeListDbContext> _repo):base(_repo){
            this.context=_repo;
        }

        [HttpDelete("{id}")]
        [Authorize] 
        public virtual ActionResult Delete(int id){
            if(!ModelState.IsValid) return BadRequest();
            Anime anime = new Anime() {Id = id};
            _repo.Delete(anime);
            return Ok();
        }

        [HttpGet("title")]
        public virtual ActionResult OrderByTitle(){
            if(!ModelState.IsValid) return BadRequest();
             var result = this.context.OrderByTitle();
            return Ok(result);
        }

        [HttpGet("date")]
        public virtual ActionResult OrderByDate(){
            if(!ModelState.IsValid) return BadRequest();
             var result = this.context.OrderByDate();
            return Ok(result);
        }

        [HttpGet("title/{title}")]
        public virtual ActionResult FindByTitle(string title){
            if(!ModelState.IsValid) return BadRequest();
            var result = this.context.FindByTitle(title);
            return Ok(result);
        }
    }
}
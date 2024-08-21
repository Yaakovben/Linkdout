using Linkdout.DTO;
using Linkdout.Models;
using Linkdout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Linkdout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]


    public class PostController : ControllerBase
    {
        private PostService postService;

        public PostController(PostService _postService)
        {
            postService = _postService;
        }

        // מחזיר את כל הפוסטים
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostsListDTO>>GetAllPosts()
        {
              return Ok(await postService.GetAll());
        }
        // מחזיר פוסט מסויים 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostModel>> GetSinglePost(int id)
        {
            return Ok(await postService.GetPostById(id));
        }


        //יצירת פוסט
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePost([FromBody] NewPostDTO req)
        {
            bool res = await postService.AddNewPost(req);
            if (res)
            {
            
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }

        // עריכת פוסט
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> editPost([FromBody] EditPostDTO req)
        {
            string oldBody = await postService.EditPostBody(req.PostId, req.NewBody);
            return oldBody == String.Empty ? Ok(oldBody):NotFound();

        }

        // מחיקת פוסט
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Delet(int id)
        {
            int res = await postService.DeletPost(id);
            return res != -1 ? Ok(res) : NotFound();

        }

    }
}

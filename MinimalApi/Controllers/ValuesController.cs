using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IUserData _data;

    public ValuesController(IUserData data)
    {
        _data = data;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        IEnumerable<UserModel> result = await _data.GetUsers();

        if (result is null) 
            return NotFound( new
            {
                success = false,
                message = "No data in the database",
            
            });

        if (result is not null) 
            return Ok( new 
            { 
                success = true,
                message = "Here are the list of user", 
                data = result 
            });

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _data.GetUser(id);

        if (result is null) 
            return NotFound( new
            {
                success = false,
                message = "No data found in the database"
            });

        if (result is not null)
            return Ok( new
            {
                success = true,
                message = "Here is the user",
                data = result
            });

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserModel user)
    {
        if (user is null) 
            return BadRequest( new
            {
                success = false,
                message = "Please input all fields."
            });

        if (user is not null)
        {
            await _data.InsertUser(user);
            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }

        return NoContent();
            
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(int id, UserModel user)
    //{
    //    if (id != user.Id)
    //        return BadRequest(new
    //        {
    //            success = false,
    //            message = "Error while updating."
    //        });

    //    if (id == user.Id)
    //    {
    //        await _data.UpdateUser(user);
    //        return Ok(new { success = true, message = "Updated sucessfully" });
    //    }

    //    return NoContent();


    //}


    [HttpPut]
    public async Task<IActionResult> Update(UserModel user)
    {
        if (user == null)
            return BadRequest(new
            {
                success = false,
                message = "Error while updating."
            });

        if (user != null)
        {
            await _data.UpdateUser(user);
            return Ok(new
            {
                success = true,
                message = "Successfuly updated",

            });
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {


        if (id == 0)
            return NotFound(new
            {
                success = false,
                message = "Error while deleting the data."
            });

        if (id != 0)
        {
            await _data.DeleteUser(id);
            return Ok(new
            {
                success = true,
                message = "Deleted Successfully",
                
            });
        }
        return NoContent();
    }

}

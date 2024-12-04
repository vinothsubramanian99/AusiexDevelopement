using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject3.Data;
using ApiProject3.DTO;
using ApiProject3.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using ApiProject3.Models;

namespace ApiProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly AdventureWorksContext _db;
        private readonly ILogger<SampleController> _logger;

        public SampleController(AdventureWorksContext db, ILogger<SampleController> logger)
        {
           // _db = db;
          //  _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            //try
           // {
                _logger.LogInformation("Getting all addresses");
                var data = await _db.DuplicateData.ToListAsync();

                _logger.LogInformation("Successfully retrieved data");
                if (data == null || !data.Any())
                {
                    _logger.LogInformation("No data found");
                    return NoContent(); // Returns a 204 No Content status code
                }
                else
                {
                    return Ok(data);
                }
           // }
           /* catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving data");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }*/
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoanDetailDTO loan)
        {
          //  try {
               // if (loan == null)
             //   { return NotFound();}
              //  else
              //  {  
                    var S=new LoanDetail();
                    S.LoanNumber=loan.LoanNumber;
                    S.UserName=loan.UserName;
                    
                    await _db.LoanDetail.AddAsync(S);
                    await _db.SaveChangesAsync();
                     _logger.LogInformation("Data saved");
                    return Ok(loan);
                //}
           // }
           /* catch (DbUpdateException dbEx){
                _logger.LogError(dbEx, "A database update error occurred while creating DuplicateDatum.");
                return StatusCode(StatusCodes.Status500InternalServerError, "A database error occurred while processing your request.");
            }
            catch (ArgumentNullException argEx){
                _logger.LogError(argEx, "An argument null exception occurred.");
                 return BadRequest("A required argument was null.");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An unexpected error occurred while creating DuplicateDatum."); 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }*/




        }
        [HttpPut ("{id}")]

        public async Task <IActionResult> Put(int id,[FromBody] LoanDetail loan){
            try{
            if(id==0){ return NotFound(); }
            else{
                var eisitingdata=await _db.LoanDetail.FindAsync(id);
                if(eisitingdata!=null){
                eisitingdata.LoanNumber=loan.LoanNumber;
                return Ok(eisitingdata);
                }
                return BadRequest();
                
            }
                       
            }
            catch(Exception ex){
                 return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            
        }

        [HttpPatch("{id}")]

        public async Task<IActionResult> patch(int id, LoanDetail ld){
            var ExistingData=await _db.LoanDetail.FindAsync(id);
            //try{

            
            if (ExistingData == null)
            {
                return NotFound();
            }
            else{
                ExistingData.UserName=ld.UserName;
                await _db.SaveChangesAsync();
                return Ok();
            }
                
            }
          /*  catch(Exception ex){
                _logger.LogError(ex.Message);
               return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request."); 
            }*/
       // }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id){

            //try{

           
            var ExistingData=await _db.LoanDetail.FindAsync(id);

            if(ExistingData==null){
                return NotFound();
            }
            else{
                _db.LoanDetail.Remove(ExistingData);
                await _db.SaveChangesAsync();
                return Ok ( _db.LoanDetail.ToList());
            }
             //}
           /*  catch (Exception ex){
                _logger.LogError(ex.Message);
                 return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
             }*/
        }
    }
}

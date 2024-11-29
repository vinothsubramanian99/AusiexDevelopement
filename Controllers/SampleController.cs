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
            _db = db;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoanDetailDTO loan)
        {
         
                    var S=new LoanDetail();
                    S.LoanNumber=loan.LoanNumber;
                    S.UserName=loan.UserName;
                    
                    await _db.LoanDetail.AddAsync(S);
                    await _db.SaveChangesAsync();
                     _logger.LogInformation("Data saved");
                    return Ok(loan);
            




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

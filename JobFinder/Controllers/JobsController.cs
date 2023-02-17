using JobFinder.Data;
using JobFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace JobFinder.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext jobsDbContext;
    
    public JobsController(ApplicationDbContext jobsDbContext)
    {
        this.jobsDbContext = jobsDbContext;
    }
    
    //Get al Jobs
    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobs = await jobsDbContext.Jobs.ToListAsync();
        return Ok(jobs);
    }
    
    //Get a specific Job
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetOneJob([FromRoute] int id)
    {
        var jobs = await jobsDbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
        if (jobs != null)
        {
            return Ok(jobs);
        }
        return NotFound("You have entered an invalid Job ID");
    }
    
    //Create a Job
    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] Job job)
    {
        await jobsDbContext.Jobs.AddAsync(job);
        await jobsDbContext.SaveChangesAsync();
        return Ok(job);
    }
    
    //Update a Job
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateJob([FromRoute] int id, [FromBody] Job job)
    {
        var jobs = await jobsDbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
        if (jobs == null) return NotFound("You have entered an invalid Job ID");
        jobs.Title = job.Title;
        jobs.Description = job.Description;
        jobs.Location = job.Location;
        jobs.Company = job.Company;
        jobs.Contact = job.Contact;
        jobs.PaymentPerHour = job.PaymentPerHour;
        jobs.Role = job.Role;
        await jobsDbContext.SaveChangesAsync();
        return Ok(jobs);
    }
    
    //Delete a Job
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteJob([FromRoute] int id)
    {
        var jobs = await jobsDbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == id);
        if (jobs == null) return NotFound("You have entered an invalid Job ID");
        jobsDbContext.Jobs.Remove(jobs);
        await jobsDbContext.SaveChangesAsync();
        return Ok("Job Deleted");
    }
}
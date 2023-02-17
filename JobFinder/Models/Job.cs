using System.ComponentModel.DataAnnotations;

namespace JobFinder.Models;

public class Job
{
    [Key]
    public int JobId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public string Company { get; set; }
    
    [Required]
    public string Contact { get; set; }
    
    [Required]
    public int PaymentPerHour { get; set; }
    
    public string Role { get; set; }
}
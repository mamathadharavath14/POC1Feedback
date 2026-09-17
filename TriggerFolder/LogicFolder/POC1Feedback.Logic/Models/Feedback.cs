namespace POC1Feedback.Logic.Models
{
public class Feedback
{
    public required string CustomerName{get;  set;}
    public required string CustomerId{get; set;}
    public required string Comments{get; set;}
    public required string Email{get; set;}
    public int Rating{get; set;}
    public DateTime SubmittedOn{get; set;}

}
}
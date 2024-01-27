namespace RabbitMQ.Service.Dto
{
    public class ResultDto
    {
        public bool IsValid { get; set; }=false;
        public string ErrorMessage { get; set; }=string.Empty;
    }
}

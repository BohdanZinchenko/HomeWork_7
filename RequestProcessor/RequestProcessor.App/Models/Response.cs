namespace RequestProcessor.App.Models
{
    public class Response:IResponse
    {
        public bool Handled { get; set; }
        public int Code { get; set; }
        public string Content { get; set; }
      
    }
}
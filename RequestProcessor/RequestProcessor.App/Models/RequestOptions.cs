namespace RequestProcessor.App.Models
{
    internal class RequestOptions : IRequestOptions, IResponseOptions

    {
        public string Name { get; set; }
        public string Address { get; set; }
        public RequestMethod Method { get; set; }
        public string ContentType { get; set; }
        public string Body { get; set; }
        public string Path { get; set; }
        public bool IsValid { get; set; }

        public RequestOptions()
        {
            IsValid = true;
        }
    }
}
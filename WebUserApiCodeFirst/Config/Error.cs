using Newtonsoft.Json;

namespace WebUserApiCodeFirst.Config
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Path { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

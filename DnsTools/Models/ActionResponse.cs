using System.Net;

namespace DnsTools.Models;

public class ActionResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
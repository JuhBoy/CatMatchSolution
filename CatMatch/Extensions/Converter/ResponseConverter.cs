using CatMatch.Http.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace CatMatch.Extensions.Converter
{
    public static class ResponseConverter
    {
        /// <summary>
        /// Return a JSON UTF-8 Buffer Formatted Response.
        /// </summary>
        /// <typeparam name="T">Generic Reference Model</typeparam>
        /// <param name="response">The Common generic class</param>
        /// <returns>A buffer</returns>
        public static byte[] ToJsonBuffer<T>(this CommonResponse<T> response) where T: class
        {
            string body = JsonConvert.SerializeObject(response, Startup.DefaultJsonSettings);
            byte[] buffer = Encoding.UTF8.GetBytes(body);
            return buffer;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace StarWars.Api.Extensions
{
    public class Response 
    {
        protected Response()
        {
            Messages = new HashSet<string>();
        }

        protected Response(string message) : this()
        {
            Messages.Add(message);
        }

        protected Response(IEnumerable<string> messages) : this()
        {
            Messages.UnionWith(messages);
        }

        public ISet<string> Messages { get; }

        public string Message => string.Join(",", Messages.Distinct());

        public bool IsFailure => !IsSuccess;

        public bool IsSuccess => Messages.Count == 0;

        public static Response Ok()
        {
            return new Response();
        }

        public static Response Fail(string message)
        {
            return new Response(message);
        }
        
        public static Response Fail(IEnumerable<string> messages)
        {
            return new Response(messages);
        }

        public void AddError(string message) => Messages.Add(message);
    }

    public class Response<TValue> 
    {
        public Response()
        {
        }
        
        public Response(TValue value) : this()
        {
            Value = value;
        }
        public TValue Value { get; }

        public static Response<TValue> Ok(TValue value)
        {
            return new Response<TValue>(value);
        }
    }
}
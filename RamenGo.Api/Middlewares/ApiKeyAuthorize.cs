namespace RamenGo.Api.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorize : Attribute
    {
        public ApiKeyAuthorize()
        {
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Erx.Questionnaire.Api.Filter
{
    /// <summary>
    ///     Custom Authorize handler
    /// </summary>
    public class BasicKeyAuthorizeHandler : AuthorizationHandler<BasicKeyRequirement>
    {
        private const string FUNCTION_KEY_AUTHORIZATION = "Authorization";
        private const string FUNCTION_KEY_AUTHORIZATION_SUB_STRING = "key";
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="contextAccessor"></param>
        public BasicKeyAuthorizeHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        ///     HandleRequirementAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, BasicKeyRequirement requirement)
        {
            try
            {
                var httpContext = _contextAccessor.HttpContext;
                var key = httpContext.Request.Headers.ContainsKey(FUNCTION_KEY_AUTHORIZATION) &&
                          httpContext.Request.Headers[FUNCTION_KEY_AUTHORIZATION].FirstOrDefault()
                                                                                 .StartsWith(
                                                                                             FUNCTION_KEY_AUTHORIZATION_SUB_STRING)
                    ? httpContext.Request.Headers[FUNCTION_KEY_AUTHORIZATION].FirstOrDefault()
                                                                             .Substring(4)
                    : null;

                if (string.IsNullOrEmpty(key))
                {
                    key = httpContext.Request.Query["key"].FirstOrDefault() ?? string.Empty;
                }

                if (!string.IsNullOrEmpty(key))
                {
                    if (key.Equals(requirement.FunctionKey,
                        StringComparison.InvariantCulture))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            catch
            {
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    ///     FunctionKeyRequirement
    /// </summary>
    public class BasicKeyRequirement : IAuthorizationRequirement
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="functionKey"></param>
        public BasicKeyRequirement(string functionKey)
        {
            FunctionKey = functionKey;
        }

        /// <summary>
        ///     Function key
        /// </summary>
        public string FunctionKey { get; }
    }
}

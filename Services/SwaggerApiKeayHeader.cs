using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project_Quizz_API.Services
{
    /// <summary>
    /// Swagger Api Key Header. It adds the Api Key header to the swagger documentation.
    /// </summary>
    public class SwaggerApiKeayHeader : IOperationFilter
    {
        /// <summary>
        /// Apply the Api Key header to the swagger documentation.
        /// </summary>
        /// <param name="operation">The API operation in the Swagger documentation to which the Api Key header is being added.</param>
        /// <param name="context">The context of the API operation, providing additional information and metadata about the operation.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "ApiKey",
                In = ParameterLocation.Header,
                Description = "Api Key",
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = new OpenApiString("5f0af790-1da8-4332-87aa-f32fefde7edf")
                }
            });
        }
    }
}

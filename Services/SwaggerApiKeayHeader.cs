using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project_Quizz_API.Services
{
    public class SwaggerApiKeayHeader : IOperationFilter
    {
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

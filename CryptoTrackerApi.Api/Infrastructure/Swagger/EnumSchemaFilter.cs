using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CryptoTrackerApi.Infrastructure.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type.IsEnum && schema != null)
            {
                var enumDescriptions = Enum.GetValues(type)
                                           .Cast<object>()
                                           .Select(v => $"{Convert.ToInt32(v)} = {Enum.GetName(type, v)}");
                var desc = string.Join(", ", enumDescriptions);
                if (string.IsNullOrWhiteSpace(schema.Description))
                    schema.Description = "Possible values: " + desc;
                else
                    schema.Description += " Possible values: " + desc;
            }
        }
    }
}

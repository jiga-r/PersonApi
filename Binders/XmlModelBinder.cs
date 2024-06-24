using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class XmlModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Fetch the value of the argument from the value provider
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult != ValueProviderResult.None)
        {
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                // Deserialize the XML to the type of the model
                var serializer = new XmlSerializer(bindingContext.ModelType);
                using (var reader = new StringReader(value))
                {
                    var model = serializer.Deserialize(reader);
                    bindingContext.Result = ModelBindingResult.Success(model);
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
            }
        }

        return Task.CompletedTask;
    }
}
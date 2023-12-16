using FileUploadApp.Contracts.Converter;
using FileUploadApp.Contracts.Factory;
using FileUploadApp.Implementation.Converters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FileUploadApp.Implementation.Factory
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<Tuple<string, string>, Func<IFileConverter>> _converters;

        public ConverterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _converters = initConverters();
        }     

        public IFileConverter CreateConverter(string sourceType, string targetType)
        {
            var key = Tuple.Create(sourceType, targetType);
            if(_converters.TryGetValue(key, out var createConverter))
            {
                var converter = createConverter();
                
                return converter;
            }
            else
            {
                throw new NotSupportedException($"Converter not supported for the specified types: {sourceType} to {targetType}.");
            }
        }

        private IDictionary<Tuple<string, string>, Func<IFileConverter>> initConverters()
        {
            var converters = new Dictionary<Tuple<string, string>, Func<IFileConverter>>
            {
                { Tuple.Create("xml", "json"), () => _serviceProvider.GetRequiredService<XmlToJsonConverter>() },
                //add additional converters if needed
            };

            return converters;
        }
    }
}

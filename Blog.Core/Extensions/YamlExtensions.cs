using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Blog.Core.Extensions
{
    public static class YamlExtensions
    {
        public static IDictionary<string, object> ParseYaml(this string text)
        {
            var results = new Dictionary<string, object>();
            
            var input = new StringReader(text);

            var yaml = new YamlStream();
            yaml.Load(input);
            
            var root = yaml.Documents[0].RootNode;

            if (root is YamlMappingNode collection)
            {
                foreach (var entry in collection.Children)
                {
                    if (entry.Key is YamlScalarNode node)
                    {
                        results.Add(node.Value, GetValue(entry.Value));
                    }
                }
            }
            
            return results;
        }

        private static object GetValue(YamlNode value)
        {
            if (value is YamlMappingNode collection)
            {
                
                var results = new Dictionary<string, object>();
                foreach (var entry in collection.Children)
                {
                    if (entry.Key is YamlScalarNode node)
                    {
                        results.Add(node.Value, GetValue(entry.Value));
                    }
                }
                return results;
            }
            
            if (value is YamlSequenceNode list)
            {
                if (list.Children.All(_ => _ is YamlScalarNode)) 
                {
                    var listString = new List<string>();
                    
                    foreach (var entry in list.Children)
                    {
                        if (entry is YamlScalarNode node) {
                            listString.Add(node.Value);
                        }
                    }
                    
                    return listString;
                }
                else
                {
                    return list.Children.Select(GetValue).ToList();
                }
            }
            
            if (bool.TryParse(value.ToString(), out var valueBool))
            {
                return valueBool;
            }
            
            return value.ToString();
        }
    }
}
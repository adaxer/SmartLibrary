using Avalonia.Data.Converters;
using Newtonsoft.Json.Linq;

namespace SmartLibrary.Avalonia.Converters;

public static class Converters 
{
    public static FuncValueConverter<object?, bool> Truthy { get; } =
        new FuncValueConverter<object?, bool>(value =>  value switch
        {
            null => false,                            
            string s when !string.IsNullOrEmpty(s) => true, 
            bool b => b,            
            int i => i!= 0,                
            double d => d != 0.0,           
            float f => f != 0.0f,           
            decimal m => m != 0.0m,         
            long l => l != 0,               
            _ => true                                
        });
}

﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

public class AndroidConfigurationProvider : ConfigurationProvider
{
    public override void Load()
    {
        var data = new Dictionary<string, string?>
        {
            { "ApiBaseUrl", "https://smartlibraryapiservice.azurewebsites.net" }
        };

        Data = data;
    }
}

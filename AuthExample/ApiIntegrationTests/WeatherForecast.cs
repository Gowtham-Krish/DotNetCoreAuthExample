using System;
using System.Collections.Generic;
using System.Text;

namespace ApiIntegrationTests
{
    public class WeatherForecast
    {
        public string date { get; set; }

        public string dayOfWeek { get; set; }

        public int temperatureC { get; set; }

        public int temperatureF => 32 + (int)(temperatureC / 0.5556);

        public string summary { get; set; }
    }
}

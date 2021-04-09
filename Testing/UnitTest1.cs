using System;
using System.Linq;
using CineAPI.Controllers;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            WeatherForecastController weather = new WeatherForecastController();
            var weat =  weather.Get();
            Assert.True(weat.Count() > 0);
        }
    }
}

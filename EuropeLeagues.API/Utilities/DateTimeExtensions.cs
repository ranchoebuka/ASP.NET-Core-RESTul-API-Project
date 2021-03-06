﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuropeLeagues.API.Utilities
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }

    public enum ResourceUriType
    {
        PreviousPage,
        NextPage,
        Current
    }
}

using System;
using System.Drawing;

namespace Veox.Attendance.User.Application.Services.Helpers
{
    public static class ColorHelper
    {
        public static string Generate()
        {
            var random = new Random();

            var color = Color.FromArgb(random.Next(150), random.Next(150), random.Next(150));

            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
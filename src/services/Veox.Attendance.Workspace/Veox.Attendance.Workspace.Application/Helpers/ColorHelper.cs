using System;
using System.Drawing;

namespace Veox.Attendance.Workspace.Application.Helpers
{
    public static class ColorHelper
    {
        public static string Generate()
        {
            var random = new Random();

            var color = Color.FromArgb(random.Next(100), random.Next(100), random.Next(100));

            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
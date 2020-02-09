using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CatMatch.Tests.Fixtures.Models
{
    internal static class CatsModel
    {
        public static readonly string JsonList = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Fixtures\\Models\\some_cats.json", Encoding.UTF8);
    }
}

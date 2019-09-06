using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;

namespace Domain
{
    public static class SeedScripts
    {
        public static void SeedANSI(string scriptName, DatabaseContext context)
        {
            var sql = File.ReadAllText(GetProperPath(scriptName), System.Text.Encoding.GetEncoding(1252));

            context.Database.ExecuteSqlCommand(sql);
        }

        public static void SeedUnicode(string scriptName, DatabaseContext context)
        {
            var sql = File.ReadAllText(GetProperPath(scriptName));

            context.Database.ExecuteSqlCommand(sql);
        }

        public static string ReadFile(string fileName)
        {
            return File.ReadAllText(GetProperPath(fileName));
        }

        public static string GetProperPath(string scriptName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Domain\Scripts\" + scriptName);
            return path;
        }
    }
}


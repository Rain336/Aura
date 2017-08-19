using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aura.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var types = new HashSet<Type>();
                foreach (var s in args)
                {
                    types.Add(Type.GetType(s));
                }
                foreach (var info in types
                    .SelectMany(t => t.GetMethods())
                    .Where(m => m.GetCustomAttribute<TestAttribute>() != null))
                {
                    var attribute = info.GetCustomAttribute<TestAttribute>();
                    var name = attribute.Name ?? info.Name;
                    Console.WriteLine("Running Test: " + name);
                    try
                    {
                        info.Invoke(null, new object[0]);
                        Console.WriteLine("Test ran successfully.");
                    }
                    catch (Assert.AssertException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                return;
            }

            foreach (var info in Assembly.GetEntryAssembly().ExportedTypes
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttribute<TestAttribute>() != null))
            {
                var attribute = info.GetCustomAttribute<TestAttribute>();
                var name = attribute.Name ?? info.Name;
                Console.WriteLine("Running Test: " + name);
                try
                {
                    info.Invoke(null, new object[0]);
                    Console.WriteLine("Test ran successfully.");
                }
                catch (Assert.AssertException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
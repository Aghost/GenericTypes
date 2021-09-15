﻿using System;
using GenericTypes.Core.Types;

using static System.Console;

namespace GenericTypes.App
{
    class Program
    {
        static void Main(string[] args) {
            //TestListBase();
            //TestListBase2();
            TestListBase3();
        }

        static void TestListBase3() {
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericListBase<string> glbs = new(strarr);
            GenericListBase<string> glbs2 = new(glbs);

            WriteLine(glbs == glbs2);
            glbs.Add("six");
            glbs2.Add("six");
            glbs2.Add("seven");
            glbs2.Add("eight");

            foreach(var item in glbs2 - glbs) {
                WriteLine($"{item}");
            }
        }

        static void TestListBase2() {
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericListBase<string> glbs = new(strarr);

            WriteLine($"cap: {glbs.Capacity} size: {glbs.Size}");
            if (glbs.Contains("five", out int position)) { WriteLine($"five @ {position}"); }
            if (!glbs.Contains("six")) { WriteLine($"glbs does not contain six"); }
            WriteLine($"cap: {glbs.Capacity} size: {glbs.Size}");
        }

        static void TestListBase() {
            // INTS
            GenericListBase<int> glbi = new();
            WriteLine($"cap: {glbi.Capacity} size: {glbi.Size}");
            glbi.Add(1);
            glbi.Add(2);
            glbi.Add(3);
            glbi.Add(4);
            glbi.Add(5);
            WriteLine($"cap: {glbi.Capacity} size: {glbi.Size}");
            foreach(int i in glbi) { WriteLine($"{i}"); }
            glbi.Resize(2);
            WriteLine("------");
            foreach(int i in glbi) { WriteLine($"{i}"); }

            // STRINGS
            GenericListBase<string> glbs = new();
            WriteLine($"cap: {glbs.Capacity} size: {glbs.Size}");
            glbs.Add("one");
            glbs.Add("two");
            glbs.Add("three");
            glbs.Add("four");
            glbs.Add("five");
            WriteLine($"cap: {glbs.Capacity} size: {glbs.Size}");
            foreach(string s in glbs) { WriteLine($"{s}"); }
            glbs.Resize(2);
            WriteLine("------");
            foreach(string s in glbs) { WriteLine($"{s}"); }

            WriteLine(glbi == glbs);

            WriteLine("------");

            foreach(string s in glbs) { WriteLine($"{s}"); }
            GenericListBase<string> glbcombined = new(glbs + glbs);
            WriteLine("------");
            foreach(string s in glbcombined) { WriteLine($"{s}"); }
            glbcombined.Add("banaan");
            glbcombined.Add("banaan");
            glbcombined.Add("banaan");
            glbcombined.Add("banaan");
            glbcombined.Add("banaan");
            foreach(string s in glbcombined) { WriteLine($"{s}"); }
            glbcombined.Clear();
            glbs.Clear();
            WriteLine("------clear");
            foreach(string s in glbcombined) { WriteLine($"{s}"); }
            
            glbcombined.Add("banaan");
            glbcombined.Add("appels");
            glbcombined.Add("kettingzaag");
            glbcombined.Add("scooter");
            glbs.Add("banaan");
            glbs.Add("appels");
            WriteLine("---set1");
            foreach(string s in glbcombined) { WriteLine($"{s}"); }
            WriteLine("---set2");
            foreach(string s in glbs) { WriteLine($"{s}"); }
            WriteLine("---result");
            GenericListBase<string> glbsminus = glbs - glbcombined;
            foreach(string s in glbsminus) { WriteLine($"{s}"); }

            if (glbsminus < glbcombined) {
                WriteLine($"glbmins < glbcombined");
            } else {
                WriteLine($"not!");
                foreach(string s in glbsminus) { WriteLine($"{s}"); }
                foreach(string s in glbcombined) { WriteLine($"{s}"); }
            }

            GenericListBase<int> glbi2 = new(glbi);
            WriteLine(glbi == glbi2);
            glbi2.Add(new int[] { 5, 6, 7, 8 });
            WriteLine(glbi == glbi2);
            WriteLine(glbi < glbi2);
            WriteLine(glbi > glbi2);

            foreach(var v in glbi) { Write($"{v} "); }
            WriteLine();
            glbi.Add(22);
            foreach(var v in glbi2) { Write($"{v} "); }
        }

    }
}

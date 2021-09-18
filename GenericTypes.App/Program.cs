using System;
using GenericTypes.Core.Types;

using static System.Console;

namespace GenericTypes.App
{
    class Program
    {
        static void Main(string[] args) {
            TestListBase();
            TestListBase2();
            TestListBase3();
            SetTests();
            LinkedListTests();
            CarthesianSetTests();
            AggregateSetTests();
            PowerSetTests();

            SetStructTest();
            /*
            */
        }

        static void SetStructTest() {
            SetStruct<string> ls = new(4);
            ls.Add("banaan");
            ls.Add("banaan");
            ls.Add("banaan");
            ls.Add("banaan");
            ls.Add("banaan");

            WriteLine($"---1: ");
            WriteLine($"{ls.Size}");
            WriteLine($"{ls.Data.Length}");


            SetStruct<string> ls2 = new(ls.Data);
            ls2.Add("banaan");
            ls2.Add("banaan");
            ls2.Add("banaan");
            ls2.Add("banaan");
            ls2.Add("banaan");
            ls2.Add("banaan");

            WriteLine($"---2: ");
            WriteLine($"{ls2.Size}");
            WriteLine($"{ls2.Data.Length}");
            foreach(string st in ls) { Write($"{st}"); }
            WriteLine($"");
            foreach(string st in ls2) { Write($"|{st} |"); }
        }

        static void PowerSetTests() {
            string[] strarr = new string[] { "a", "b", "c" };
            SetList<string> glbs = new(strarr);

            SetList<SetList<string>> glglbs = glbs.PowerSet();
            foreach(var v in glglbs) {
                foreach (var vv in v) {
                    Write($"{vv}");
                }
                WriteLine();
            }
        }

        static void AggregateSetTests() {
            // STR
            string[] strarr = new string[] { "a", "b", "c" };
            string[] strarr2 = new string[] { "1", "2", "3" };

            SetList<string> glbs = new(strarr);
            SetList<string> glbs2 = new(strarr2);

            SetList<SetList<string>> glbglb = new(glbs ^ glbs2);

            foreach(var v in glbglb) {
                foreach(string str in v) {
                    Write($"{str} ");
                }
                WriteLine();
            }

            // INT
            /*
            int[] iarr = new int[] { 1,2,3 };
            int[] iarr2 = new int[] { 4,5,6 };
            SetList<int> glbi = new(iarr);
            SetList<int> glbi2 = new(iarr2);

            SetList<SetList<int>> glbglbi = new(glbi ^ glbi2);

            foreach(var v in glbglbi) {
                foreach(int i in v) {
                    Write($"{i} ");
                }
                WriteLine();
            }
            */
        }

        static void LinkedListTests() {
            GenericLinkedList<string> gll = new();
            gll.AddHead("banaan");
            gll.AddHead("appel");
            gll.AddHead("gordijn");
            //gll.InsertAt("paard", 2);

            foreach(var v in gll) {
                WriteLine($"{v}");
            }
        }

        static void CarthesianSetTests() {
            // STR
            string[] strarr = new string[] { "1", "2", "3" };
            string[] strarr2 = new string[] { "a", "b", "c" };
            SetList<string> glbs = new(strarr);
            SetList<string> glbs2 = new(strarr2);

            Write($"set A: ");
            foreach(var v in strarr) { Write($"{v} "); }
            WriteLine();
            Write($"set B: ");
            foreach(var v in strarr2) { Write($"{v} "); }
            WriteLine();
            WriteLine();

            SetList<SetList<string>> glbglb = new(glbs * glbs2);

            WriteLine("Carthesian Sets: ");
            foreach(var v in glbglb) {
                foreach(string str in v) {
                    Write($"\t{str} ");
                }
                WriteLine();
            }

            WriteLine();
            // INT
            int[] iarr = new int[] { 1,2,3 };
            int[] iarr2 = new int[] { 4,5,6 };
            SetList<int> glbi = new(iarr);
            SetList<int> glbi2 = new(iarr2);

            Write($"set A: ");
            foreach(var v in iarr) { Write($"{v} "); }
            WriteLine();
            Write($"set B: ");
            foreach(var v in iarr2) { Write($"{v} "); }
            WriteLine();
            WriteLine();

            SetList<SetList<int>> glbglbi = new(glbi * glbi2);

            WriteLine("Carthesian Sets: ");
            foreach(var v in glbglbi) {
                foreach(int i in v) {
                    Write($"\t{i} ");
                }
                WriteLine();
            }
        }

        static void SetTests() {
            string[] strarr = new string[] { "one", "two", "three" };
            string[] strarr2 = new string[] { "two", "three", "four" };

            SetList<string> glbs = new(strarr);
            SetList<string> glbs2 = new(strarr2);

            foreach(var v in glbs - glbs2) {
                WriteLine(v);
            }
        }

        static void TestListBase3() {
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            SetList<string> glbs = new(strarr);
            SetList<string> glbs2 = new(glbs);

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
            SetList<string> glbs = new(strarr);

            WriteLine($"cap: {glbs.Data.Length} size: {glbs.Size}");
            if (glbs.Contains("five", out int position)) { WriteLine($"five @ {position}"); }
            if (!glbs.Contains("six")) { WriteLine($"glbs does not contain six"); }
            WriteLine($"cap: {glbs.Data.Length} size: {glbs.Size}");
        }

        static void TestListBase() {
            // INTS
            SetList<int> glbi = new();
            WriteLine($"cap: {glbi.Data.Length} size: {glbi.Size}");
            glbi.Add(1);
            glbi.Add(2);
            glbi.Add(3);
            glbi.Add(4);
            glbi.Add(5);
            WriteLine($"cap: {glbi.Data.Length} size: {glbi.Size}");
            foreach(int i in glbi) { WriteLine($"{i}"); }
            glbi.Resize(2);
            WriteLine("------");
            foreach(int i in glbi) { WriteLine($"{i}"); }

            // STRINGS
            SetList<string> glbs = new();
            WriteLine($"cap: {glbs.Data.Length} size: {glbs.Size}");
            glbs.Add("one");
            glbs.Add("two");
            glbs.Add("three");
            glbs.Add("four");
            glbs.Add("five");
            WriteLine($"cap: {glbs.Data.Length} size: {glbs.Size}");
            foreach(string s in glbs) { WriteLine($"{s}"); }
            glbs.Resize(2);
            WriteLine("------");
            foreach(string s in glbs) { WriteLine($"{s}"); }

            WriteLine(glbi == glbs);

            WriteLine("------");

            foreach(string s in glbs) { WriteLine($"{s}"); }
            SetList<string> glbcombined = new(glbs + glbs);
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
            SetList<string> glbsminus = glbs - glbcombined;
            foreach(string s in glbsminus) { WriteLine($"{s}"); }

            if (glbsminus < glbcombined) {
                WriteLine($"glbmins < glbcombined");
            } else {
                WriteLine($"not!");
                foreach(string s in glbsminus) { WriteLine($"{s}"); }
                foreach(string s in glbcombined) { WriteLine($"{s}"); }
            }

            SetList<int> glbi2 = new(glbi);
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

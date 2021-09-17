using System;
using Xunit;
using GenericTypes.Core.Types;

namespace GenericTypes.Test
{
    public class GenericListBaseTests
    {
        [Fact]
        public void GenericListBaseConstructors() {
            // Assign
            GenericListBase<int> glb = new();
            GenericListBase<string> glb8 = new(8);

            string[] strarr = new string[] { "aap", "mies" };
            GenericListBase<string> glbstr = new(strarr);


            // Act and Assert
            int actual = glb.Data.Length;
            Assert.Equal(4, actual);

            actual = glb8.Data.Length;
            Assert.Equal(8, actual);

            actual = glbstr.Data.Length;
            Assert.Equal(2, actual);
        }

        [Fact]
        public void GenericListBaseAddAndContains() {
            // Assign
            int actual = -1;
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericListBase<string> glbstr = new(strarr);
            glbstr.Add("six");
            glbstr.Add(new string[] { "seven", "eight" });
            // Act
            if (glbstr.Contains("six", out int position))
                if (glbstr.Contains("eight") && (!glbstr.Contains("nine")))
                    actual = position;

            // Assert
            Assert.Equal(5, actual);
        }
        
        [Fact]
        public void GenericListBaseResize() {
            // Assign
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericListBase<string> glbstr = new(strarr);

            glbstr.Resize(4);

            // Act and Assert
            Assert.Equal(4, glbstr.Data.Length);

            glbstr.Resize(6);

            Assert.Equal(4, glbstr.Size);

            glbstr.Resize(2);

            Assert.Equal(2, glbstr.Data.Length);
        }

        [Fact]
        public void GenericListBaseMinus() {
            string[] strarr = new string[] { "one", "two", "three" };
            string[] strarr2 = new string[] { "one", "two", "six" };

            GenericListBase<string> glbstr = new(strarr);
            GenericListBase<string> glbstr2 = new(strarr2);

            GenericListBase<string> glbstr3a = glbstr2 - glbstr;
            string[] strarr3b = new string[] { "six" };
            GenericListBase<string> glbstr3b = new(strarr3b);
            Assert.Equal(glbstr3a, glbstr3b);

            GenericListBase<string> glbstr4a = glbstr - glbstr2;
            string[] strarr4b = new string[] { "three" };
            GenericListBase<string> glbstr4b = new(strarr4b);
            Assert.Equal(glbstr4a, glbstr4b);
        }

        [Fact]
        public void GenericListBaseComparatorMethodsTests() {
            int[] iarr = new int[] { 1, 2, 3 };
            GenericListBase<int> glbi = new(iarr);
            GenericListBase<int> glbi2 = new(glbi);

            Assert.Equal(glbi, glbi2);

            bool isSubset = glbi < glbi2;
            bool isSuperset = glbi > glbi2;

            bool isSubset2 = glbi2 < glbi;
            bool isSuperset2 = glbi2 > glbi;

            bool expected = true;
            Assert.Equal(expected, isSubset);
            Assert.Equal(expected, isSuperset);

            Assert.Equal(expected, isSubset2);
            Assert.Equal(expected, isSuperset2);

            expected = false;
            
            GenericListBase<int> glbi3 = new();

            bool isSuperset3 = glbi3 > glbi2;
            Assert.Equal(expected, isSuperset3);
        }

        [Fact]
        public void GenericListBaseCarthesianProductTest() {
            string[] strarr = new string[] { "1", "2", "3" };
            string[] strarr2 = new string[] { "x", "y", "z" };
            GenericListBase<string> glbstr = new(strarr); 
            GenericListBase<string> glbstr2 = new(strarr2);
            GenericListBase<GenericListBase<string>> glbstr3 = new(glbstr * glbstr2);

            string actual = "";
            foreach (var v in glbstr3) {
                foreach (var vv in v) {
                    actual += vv;
                }
            }
            string expected = "x1y1z1x2y2z2x3y3z3";

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GenericListBasePowerSet() {
            string[] strarr = new string[] { "a", "b", "c" };
            GenericListBase<string> glbstr = new(strarr);

            GenericListBase<GenericListBase<string>> powerset = glbstr.PowerSet();
            string actual = "";
            foreach(var v in powerset) {
                foreach(var vv in v) {
                    actual += vv;
                }
            }

            string expected = "ababcacbcabc";

            Assert.Equal(actual, expected);
        }

        /*
        [Fact]
        public void GenericListBaseZip() {
        }
        */
    }
}

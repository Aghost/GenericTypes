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

            int actual;

            // Act and Assert
            actual = glb.Capacity;
            Assert.Equal(4, actual);

            actual = glb8.Capacity;
            Assert.Equal(8, actual);

            actual = glbstr.Capacity;
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
            Assert.Equal(8, glbstr.Capacity);
        }
        
        [Fact]
        public void GenericListBaseResize() {
            // Assign
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericListBase<string> glbstr = new(strarr);

            glbstr.Resize(4);

            // Assert
            Assert.Equal(4, glbstr.Capacity);
            Assert.Equal(4, glbstr.Size);

            glbstr.Resize(6);

            Assert.Equal(6, glbstr.Capacity);
            Assert.Equal(4, glbstr.Size);

            glbstr.Resize(2);

            Assert.Equal(2, glbstr.Capacity);
            Assert.Equal(2, glbstr.Size);
        }

        [Fact]
        public void GenericListBaseMinus() {
            string[] strarr = new string[] { "one", "two", "three" };
            string[] strarr2 = new string[] { "one", "two", "six" };

            GenericListBase<string> glbstr = new(strarr);
            GenericListBase<string> glbstr2 = new(strarr2);

            GenericListBase<string> glbstr3a = glbstr2 - glbstr;
            string[] strarr3b = new string[] { "six", null, null, null };
            GenericListBase<string> glbstr3b = new(strarr3b);
            Assert.Equal(glbstr3a, glbstr3b);

            GenericListBase<string> glbstr4a = glbstr - glbstr2;
            string[] strarr4b = new string[] { "three", null, null, null };
            GenericListBase<string> glbstr4b = new(strarr4b);
            Assert.Equal(glbstr4a, glbstr4b);


            //foreach(var v in glbstr) { WriteLine($"{v}"); }

        }

        /*
        [Fact]
        public void GenericListBaseComparatorMethodsTests() {
            int[] iarr = new int[] { 1, 2 };
            GenericListBase<int> glbi = new(iarr);
            GenericListBase<int> glbi2 = new(glbi);

            glbi.Add(3);
            glbi2.Add(3);
            Assert.Equal(glbi, glbi2);

            glbi2.Add(4);
            bool subset = glbi < glbi2;
            bool expected = true;
            Assert.Equal(expected, subset);

            bool superset = glbi > glbi2;
            expected = false;
            Assert.Equal(expected, superset);

            glbi.Add(3);
            glbi.Add(8);
            glbi.Add(9);
            superset = glbi < glbi2;
            expected = false;
            Assert.Equal(expected, superset);
        }
        */
    }
}
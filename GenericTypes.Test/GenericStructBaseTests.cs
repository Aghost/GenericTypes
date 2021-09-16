using System;
using Xunit;
using GenericTypes.Core.Types;

namespace GenericTypes.Test
{
    public class GenericStructBaseTests
    {
        [Fact]
        public void GenericStructConstructors() {
            // Assign
            GenericStructBase<int> glb = new();
            GenericStructBase<string> glb8 = new(8);

            string[] strarr = new string[] { "aap", "mies" };
            GenericStructBase<string> glbstr = new(strarr);

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
        public void GenericStructAddAndContains() {
            // Assign
            int actual = -1;
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericStructBase<string> glbstr = new(strarr);
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
        public void GenericStructResize() {
            // Assign
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            GenericStructBase<string> glbstr = new(strarr);

            glbstr.Resize(4);

            // Act and Assert
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
        public void GenericStructMinus() {
            string[] strarr = new string[] { "one", "two", "three" };
            string[] strarr2 = new string[] { "one", "two", "six" };

            GenericStructBase<string> glbstr = new(strarr);
            GenericStructBase<string> glbstr2 = new(strarr2);

            GenericStructBase<string> glbstr3a = glbstr2 - glbstr;
            string[] strarr3b = new string[] { "six" };
            GenericStructBase<string> glbstr3b = new(strarr3b);
            Assert.Equal(glbstr3a, glbstr3b);

            GenericStructBase<string> glbstr4a = glbstr - glbstr2;
            string[] strarr4b = new string[] { "three" };
            GenericStructBase<string> glbstr4b = new(strarr4b);
            Assert.Equal(glbstr4a, glbstr4b);
        }

        [Fact]
        public void GenericStructComparatorMethodsTests() {
            int[] iarr = new int[] { 1, 2, 3 };
            GenericStructBase<int> glbi = new(iarr);
            GenericStructBase<int> glbi2 = new(glbi);

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
            
            GenericStructBase<int> glbi3 = new();

            bool isSuperset3 = glbi3 > glbi2;
            Assert.Equal(expected, isSuperset3);
        }
        /*
        */
    }
}
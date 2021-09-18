using System;
using Xunit;
using GenericTypes.Core.Types;

namespace GenericTypes.Test
{
    public class SetListTests
    {
        [Fact]
        public void SetListConstructors() {
            // Assign
            SetList<int> glb = new();
            SetList<string> glb8 = new(8);

            string[] strarr = new string[] { "aap", "mies" };
            SetList<string> glbstr = new(strarr);


            // Act and Assert
            int actual = glb.Data.Length;
            Assert.Equal(4, actual);

            actual = glb8.Data.Length;
            Assert.Equal(8, actual);

            actual = glbstr.Data.Length;
            Assert.Equal(2, actual);
        }

        [Fact]
        public void SetListAddAndContains() {
            // Assign
            int actual = -1;
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            SetList<string> glbstr = new(strarr);
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
        public void SetListResize() {
            // Assign
            string[] strarr = new string[] { "one", "two", "three", "four", "five" };
            SetList<string> glbstr = new(strarr);

            glbstr.Resize(4);

            // Act and Assert
            Assert.Equal(4, glbstr.Data.Length);

            glbstr.Resize(6);

            Assert.Equal(4, glbstr.Size);

            glbstr.Resize(2);

            Assert.Equal(2, glbstr.Data.Length);
        }

        [Fact]
        public void SetListMinus() {
            string[] strarr = new string[] { "one", "two", "three" };
            string[] strarr2 = new string[] { "one", "two", "six" };

            SetList<string> glbstr = new(strarr);
            SetList<string> glbstr2 = new(strarr2);

            SetList<string> glbstr3a = glbstr2 - glbstr;
            string[] strarr3b = new string[] { "six" };
            SetList<string> glbstr3b = new(strarr3b);
            Assert.Equal(glbstr3a, glbstr3b);

            SetList<string> glbstr4a = glbstr - glbstr2;
            string[] strarr4b = new string[] { "three" };
            SetList<string> glbstr4b = new(strarr4b);
            Assert.Equal(glbstr4a, glbstr4b);
        }

        [Fact]
        public void SetListComparatorMethods() {
            int[] iarr = new int[] { 1, 2, 3 };
            SetList<int> glbi = new(iarr);
            SetList<int> glbi2 = new(glbi);

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
            
            SetList<int> glbi3 = new();

            bool isSuperset3 = glbi3 > glbi2;
            Assert.Equal(expected, isSuperset3);
        }

        [Fact]
        public void SetListCarthesianProduct() {
            string[] strarr = new string[] { "1", "2", "3" };
            string[] strarr2 = new string[] { "x", "y", "z" };
            SetList<string> glbstr = new(strarr); 
            SetList<string> glbstr2 = new(strarr2);
            SetList<SetList<string>> glbstr3 = new(glbstr * glbstr2);

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
        public void SetListPowerSet() {
            string[] strarr = new string[] { "a", "b", "c" };
            SetList<string> glbstr = new(strarr);

            SetList<SetList<string>> powerset = glbstr.PowerSet();
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
        public void SetListZip() {
        }
        */
    }
}

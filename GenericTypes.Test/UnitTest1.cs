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
    }
}

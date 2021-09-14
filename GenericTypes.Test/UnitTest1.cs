using System;
using Xunit;
using GenericTypes.Core.Types;

namespace GenericTypes.Test
{
    public class GenericListBaseTests
    {
        [Fact]
        public void GenericListBaseConstructor1() {
            // Assign
            GenericListBase<string> glb = new();

            // Act
            int actual = glb.Capacity;
            int expected = 4;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenericListBaseConstructor2() {
            // Assign

            // Act

            // Assert
        }
    }
}

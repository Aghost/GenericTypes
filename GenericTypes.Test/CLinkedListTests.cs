using System;
using Xunit;
using GenericTypes.Core.Types;

namespace GenericTypes.Test
{
    public class CLinkedListTests
    {
        [Fact]
        public void CLinkedListConstructors() {
            string[] data = new string[] { "a", "b", "c" };
            CLinkedList<string> cll = new();
            CLinkedList<string> cll2 = new();

            int expected = 0;
            int actual = cll.Count();

            Assert.Equal(expected, actual);

            expected = 3;
            actual = cll2.Count();
        }

        [Fact]
        public void CLinkedListContains() {
            CLinkedList<string> cll = new();

            cll.AddHead("a");
            cll.AddHead("b");
            cll.AddHead("c");

            bool actual = cll.Contains("d");
            bool expected = false;

            Assert.Equal(actual, expected);

            cll.AddHead("d");
            actual = cll.Contains("d");
            expected = true;
            Assert.Equal(actual, expected);
        }
    }
}

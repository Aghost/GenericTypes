using System.Collections.Generic;

namespace GenericTypes.Core
{
    public static class Combiner
    {
        // Group 2 elements
        public static IEnumerable<T> Group<T>(T a, T b) {
            yield return a;
            yield return b;
        }

        // append set to element
        public static IEnumerable<T> Append<T>(T element, IEnumerable<T> elementset) {
            yield return element;

            foreach (T e in elementset)
                yield return e;
        }

        // append element to set
        public static IEnumerable<T> Append<T>(IEnumerable<T> elementset, T element) {
            foreach (T e in elementset)
                yield return e;

            yield return element;
        }

        // combine 2 sets
        public static IEnumerable<T> Combine<T>(IEnumerable<T> setA, IEnumerable<T> setB) {
            foreach (T element in setA) { yield return element; }
            foreach (T element in setB) { yield return element; }
        }

        // Combine 1 matrix and 1 element
        public static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<IEnumerable<T>> matrix, T element) {
            bool found = false;

            foreach (IEnumerable<T> elementset in matrix) {
                found = true;
                yield return Append(elementset, element);
            }

            if (!found)
                yield return new T[] { element };
        }

        // Combine 1 element and 1 matrix
        public static IEnumerable<IEnumerable<T>> Combine<T>(T element, IEnumerable<IEnumerable<T>> matrix) {
            bool found = false;

            foreach (IEnumerable<T> elementset in matrix) {
                found = true;
                yield return Append(element, elementset);
            }

            if (!found)
                yield return new T[] { element };
        }

        // combine 1 set and 1 matrix
        /*
        public static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<T> elementset, IEnumerable<IEnumerable<T>> matrix) {
            bool found = false;

            foreach (IEnumerable<T> matrixset in matrix) {
                found = true;
                // type arguments cannot be inferred???
                yield return Append(elementset, matrixset);
            }

            if (!found)
                yield return elementset;
        }

        // combine 1 matrix and 1 set
        public static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<IEnumerable<T>> matrix, IEnumerable<T> elementset) {
            bool found = false;

            foreach (IEnumerable<T> matrixset in matrix) {
                found = true;
                // type arguments cannot be inferred???
                yield return Append(matrixset, elementset);
            }

            if (!found)
                yield return elementset;
        }

        // combine 2 matrices
        public static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<IEnumerable<T>> matrixA, IEnumerable<IEnumerable<T>> matrixB) {
            bool found = false;

            foreach (IEnumerable<T> setA in matrixA) {
                found = true;

                foreach (IEnumerable<T> setB in matrixB)
                    // type arguments cannot be inferred???
                    yield return Append(setA, setB);
            }

            if (!found)
                foreach (IEnumerable<T> setB in matrixB)
                    yield return setB;
        }
        */

        // Combinator on any number of elementArrays
        public static IEnumerable<IEnumerable<T>> Combinator<T>(params IEnumerable<T>[] elementsArray) {
            IEnumerable<IEnumerable<T>> result = new T[0][];

            foreach (IEnumerable<T> elementSet in elementsArray) {
                result = Combiner.Combine(result, Combinations(elementSet));
            }

            return result;
        }

        // helper method for combiner
        private static IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> elements) {
            foreach (T e in elements) {
                yield return new T[] { e };
            }
        }

    }
}

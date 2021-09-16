# GenericTypes

## Generic List
- properties
`Data`
`Capacity`
`Size`

- Constructors
`GenericListBase(int initCapacity = 4);`
`GenericListBase(T[] Data);`
`GenericListBase(List<T> rhs);`

- Methods
`void Add(T element)`
`void Add(T[] elements)`
`bool Contains(T element)`
`bool Contains(T element, out int position)`
`void Resize()`
`void Resize(int newSize)`
`T[] ToArray()`
`void Clear()`
`IEnumerator IEnumerable.GetEnumerator()`
`IEnumerator<T> GetEnumerator()`
`override int GetHashCode()`
`override bool Equals(Object obj)`
`bool ValueEquals(List<T> rhs)`
`bool ==(List<T> lhs, List<T> rhs)`
`bool !=(List<T> lhs, List<T> rhs)`
`List<T> +(List<T> lhs, List<T> rhs)`
`List<T> -(List<T> lhs, List<T> rhs)`
`List<T> /(List<T> lhs, List<T> rhs)`
`bool >(List<T> lhs, List<T> rhs)`
`bool <(List<T> lhs, List<T> rhs)`
`List<List<T>> *(List<T> lhs, List<T> rhs`
`List<List<T>> ^(List<T> lhs, List<T> rhs`

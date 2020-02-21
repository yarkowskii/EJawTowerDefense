using System.Collections.Generic;

public interface IHolder<T>
{
    List<T> Objects { get; }
}


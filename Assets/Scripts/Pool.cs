using System;
using System.Collections.Generic;

/// <summary>
/// Generic pool
/// </summary>
/// <typeparam name="T">Type being pooled</typeparam>
public class Pool<T>
{
    readonly Queue<T> contents = new Queue<T>();
    readonly Func<T> CreateNew;
    readonly Action<bool, T> OnChange;

    /// <summary>How many instances are available to use</summary>
    public int FreeInstancesCount => contents.Count;

    /// <param name="CreateNew">Method that will be used to create new instances </param>
    /// <param name="OnChange">Method that's run whenever an instance is being taken or returned. True meaning is being taken.</param>
    public Pool(Func<T> CreateNew, Action<bool, T> OnChange) : this(CreateNew)
        => this.OnChange = OnChange;

    /// <param name="CreateNew">Method that will be used to create new instances </param>
    public Pool(Func<T> CreateNew)
        => this.CreateNew = CreateNew;

    public void Return(T thing)
    {
        if (thing == null) return;

        contents.Enqueue(thing);
        OnChange?.Invoke(false, thing);
    }

    public void Return(T[] things)
    {
        if (things == null) return;

        for (int i = 0; i < things.Length; i++)
        {
            Return(things[i]);
            things[i] = default;
        }

    }

    public T Borrow()
    {
        if (contents.Count > 0)
        {
            T content = contents.Dequeue();
            OnChange?.Invoke(true, content);
            return content;
        }
        return CreateNew();
    }

    public void BorrowNonAlloc(T[] array, int count)
    {
        for (int i = 0; i < count; i++)
            array[i] = Borrow();
    }

    public void BorrowNonAlloc(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            array[i] = Borrow();
    }

    public T[] Borrow(int count)
    {
        T[] array = new T[count];
        for (int i = 0; i < count; i++)
            array[i] = Borrow();
        return array;
    }
}
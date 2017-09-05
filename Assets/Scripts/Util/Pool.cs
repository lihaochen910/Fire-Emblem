using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : class, new()
{

    private List<T> pool;

    public Pool()
    {
        pool = new List<T>();
    }

    public T this[int index]
    {//索引器
        get
        {
            if (index < pool.Count)
                return pool[index];
            else
                return new T();
        }
    }

    public int Count
    {
        get { return pool.Count; }
    }

    public T Get()
    {//从池中首个元素拿出
        return pool.Count == 0 ? null : pool[0];
    }

    public bool RemoveFirstElement()
    {//删除池中的首个元素,通常与Get方法一起使用
        if (pool.Count != 0)
        {
            pool.RemoveAt(0);
            return true;
        }
        else
            return false;
    }
    public void Store(T t)//放回池
    {
        pool.Add(t);
    }
}

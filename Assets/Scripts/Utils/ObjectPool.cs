using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class, new() {
    public Stack<T> Stack = new Stack<T>();

    private static ObjectPool<T> _Instance;
    public static ObjectPool<T> Instance {
        get {
            if(_Instance == null)
                _Instance = new ObjectPool<T>();
            return _Instance;
        }
    }

    public T AcquireReusable() {
        return this.Stack.Count>0 ? this.Stack.Pop() : 
            new();
    }
    public void ReleaseReusable(T reusable) { 
        this.Stack.Push(reusable);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool<T> where T : MonoBehaviour {
    private Stack<T> Pool;
    private T Prefab;
    private Transform Parent;
    private static GameObjectPool<T> _instance;


    public static GameObjectPool<T> Instance {
        get {
            if(_instance == null) {
                _instance = new GameObjectPool<T>();
            }
            return _instance;
        }
    }

    public void Initialize(T prefab, int initialPoolSize, Transform parent = null) {
        this.Prefab = prefab;
        this.Parent = parent;
        this.Pool = new Stack<T>();

        for(int i = 0; i < initialPoolSize; i++) {
            T newObject = CreateNewObject();
            Pool.Push(newObject);
            newObject.gameObject.SetActive(false);
        }
    }

    private T CreateNewObject() {
        GameObject newGameObject = GameObject.Instantiate(this.Prefab.gameObject);
        T newObject = newGameObject.GetComponent<T>();

        if(this.Parent != null) {
            newGameObject.transform.SetParent(this.Parent);
        }

        return newObject;
    }

    public T GetObjectFromPool() {
        T objectToUse;
        if(this.Pool.Count == 0) {
            objectToUse = CreateNewObject();
        }
        else {
            objectToUse = this.Pool.Pop();
        }

        objectToUse.gameObject.SetActive(true);
        return objectToUse;
    }

    public void ReturnObjectToPool(T objectToReturn) {
        objectToReturn.gameObject.SetActive(false);
        this.Pool.Push(objectToReturn);
    }
}

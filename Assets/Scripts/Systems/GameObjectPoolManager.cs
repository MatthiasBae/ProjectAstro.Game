using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolManager : MonoBehaviour {

    public ActorBase ActorPrefab;

    public GameObject PoolContainer;

    private void Awake() {
        GameObjectPool<ActorBase>.Instance.Initialize(this.ActorPrefab, 20, this.PoolContainer.transform);
    }
}

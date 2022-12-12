using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorConfig", menuName = "Configs/ActorConfig", order = 0)]
public class ActorConfig : ScriptableObject {
    public string Name;
    public float WalkSpeed;
    public float RunSpeed;
    public ActorTypeConfig TypeConfig;
}

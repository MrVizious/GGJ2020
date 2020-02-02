using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    private Transform target;
    private ObjectPool bulletPool;
    public abstract bool Hurt();
    public abstract void setTarget(Transform t);
    public abstract void setBulletPool(ObjectPool o);
    public abstract void Freeze(float t);
}

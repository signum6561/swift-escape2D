using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 2f);
    }
    private void ReturnToPool()
    {
        PoolManager.Instance.CoolObject(gameObject, ObjectType.BoxParts);
    }
}

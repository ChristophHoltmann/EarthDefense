using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField]
    private GameObject onDestroyObject = null;

    public void Destroy()
    {
        if(onDestroyObject != null)
        {
            var destroyObject = Instantiate(onDestroyObject);

            destroyObject.transform.position = transform.position;
            destroyObject.transform.rotation = transform.rotation;
        }

        Destroy(gameObject);
    }
}

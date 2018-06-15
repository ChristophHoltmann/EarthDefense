using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDestoyer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var meteor = other.GetComponent<Meteor>();
        if (meteor != null)
        {
            meteor.OnClick();
            return;
        }

        if (other.tag.Equals("Destroyable"))
        {
            Destroy(other.gameObject);
        }
    }
}

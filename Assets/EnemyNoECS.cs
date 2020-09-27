using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoECS : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        float targetZ = EnemyManager.EnemyContainerInstance.transform.position.z;

        if (Mathf.Abs(targetZ - transform.position.z) > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(0,0, targetZ -transform.position.z).normalized*100);
    }
}

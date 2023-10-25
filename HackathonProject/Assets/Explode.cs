using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public bool isHit;
    public LayerMask groundMask;
    public float groundDistance = 10f;
    public string areWeGettingExploded = "tak";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        isHit = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        Transform objectHit = other.transform;

        if (objectHit.parent.GetComponent<enemyHP>())
        {
            enemyHP hpScript = objectHit.parent.GetComponent<enemyHP>();
            hpScript.slowDown();
        }




    }


}

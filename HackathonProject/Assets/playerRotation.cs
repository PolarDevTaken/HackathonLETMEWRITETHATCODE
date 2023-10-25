using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraRot;
    public float sens = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sens, 0);
    }
}

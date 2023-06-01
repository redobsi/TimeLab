using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    public Camera PlayerCam;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity= false;

    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity= true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (objectGrabPointTransform != null)
        {
            CameraZoom();
            float lerpSpeed = 7f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
        else
        {
            CameraDeZoom();
        }
    }
    private void CameraZoom()
    {
        if(PlayerCam.fieldOfView > 70) 
        {
            PlayerCam.fieldOfView += -80f * Time.deltaTime;
        }    
        
    }
    private void CameraDeZoom()
    {
        if (PlayerCam.fieldOfView < 100)
        {
            PlayerCam.fieldOfView -= -80f * Time.deltaTime;
        }
    }
}

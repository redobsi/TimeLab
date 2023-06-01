using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;
    public float holdingSpeed;
    public PlayerMovement PlayerMov;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objectGrabbable == null)
            {
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        PlayerMov.moveSpeed = holdingSpeed;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (objectGrabbable != null)
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                PlayerMov.moveSpeed = PlayerMov.defaultSpeed;
            }
        }
    }



}

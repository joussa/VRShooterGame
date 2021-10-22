using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;

    private InputAction _thumbstick;
    private bool _isActive;

    void Start()
    {
        //Start out by disabling the ray interactor
        //to only show the line 
        //when you're pushing the stick forward
        rayInteractor.enabled = false;

        //find the status activate of teleportation action with left hand 
        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        //Every time this action is performed, is going to run the method
        activate.performed += OnTeleportActivate;


        //find the status desactivate of teleportation action with left hand 
        var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        _thumbstick.Enable();

    }

    //a toggle that find the status activate of the teleportation ray  
    //when the teleportation is active
    //at the very last moment when it  becomes not active
    //that's when we want to grab the  
    //position and that's when we want to teleport
    void Update()
    {
        //if teleportation Ray is not active 
        //then you're going to escape
        if(!_isActive)
            return;

        //if the thumbstick is still pushed forward
        //then that means that you doesn't want to teleport yet
        if (_thumbstick.triggered)
            return;

        //method "GetCurrent..." give us back a raycast hit variable 
        if(!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            //trun off the ray interactor 
            //turn  _isActive off

            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            //destinationRotation =,
            //matchOrientation =,
            //requestTime =;
        };

        provider.QueueTeleportRequest(request);
    }

    //Init both methods to activate and cancel 
    //teleportation mode action

    //Activate the teleportation mode!
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;

    }

    //Cancel the teleportation mode !
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;

    }
}

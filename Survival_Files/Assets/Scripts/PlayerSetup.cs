using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
    public GameObject fpCamera; // Drag FP_Camera here
    public CharacterController characterController; // Drag CharacterController here
    public FirstPersonController movementScript; // Drag FirstPersonController script here

    private void Start()
    {
        bool isMine = photonView.IsMine;

        fpCamera.SetActive(isMine);
        characterController.enabled = isMine;
        movementScript.enabled = isMine;

        if (!isMine && fpCamera.TryGetComponent(out AudioListener listener))
        {
            listener.enabled = false;
        }
    }
}
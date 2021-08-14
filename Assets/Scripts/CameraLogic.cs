using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    float CameraMovementOFfset = 0.15f;
   [SerializeField] float Cameraoffset_Z = 5f; 
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        CenterCamaeraInputCheck();
    }

    private void CenterCamaeraInputCheck()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CenterCameraOnPLayer();
        }
    }

    void UpdateCameraPosition()
    {
        if(Input.mousePosition.x > Screen.width)
        {
            // move camera right
            transform.position = new Vector3(transform.position.x + CameraMovementOFfset, transform.position.y, transform.position.z);
        }
        else if (Input.mousePosition.x < 0.0f)
        {
            // Move Camera Left
            transform.position = new Vector3(transform.position.x - CameraMovementOFfset, transform.position.y, transform.position.z);
        }
        if(Input.mousePosition.y > Screen.height)
        {
            //Move Camera Up
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z + CameraMovementOFfset);
        }
        else if(Input.mousePosition.y < 0.0f)
        {
            //Move Camera Down
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - CameraMovementOFfset);
        }
    }

    void CenterCameraOnPLayer()
    {
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z - Cameraoffset_Z);
    }
}

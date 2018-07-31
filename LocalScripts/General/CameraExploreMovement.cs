using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExploreMovement : MonoBehaviour {

    public float mouseSensitivity = 5f;
    public float speed = 5f;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        RotateCamera();
        Move();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float mouseRotationX = mouseX * mouseSensitivity;
        float mouseRotationY = mouseY * mouseSensitivity;


        Vector3 camRotatation = transform.rotation.eulerAngles;

        camRotatation.x -= mouseRotationY;
        camRotatation.z = 0;
        camRotatation.y += mouseRotationX;


        transform.rotation = Quaternion.Euler(camRotatation);
        transform.rotation = Quaternion.Euler(camRotatation);




    }
    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 moveDirSide = transform.right * hor * speed;
        Vector3 moveDirForward = transform.forward * vert * speed;

        transform.Translate(Vector3.right * Time.deltaTime * hor * speed);
        transform.Translate(Vector3.forward * Time.deltaTime * vert * speed);


    }


}

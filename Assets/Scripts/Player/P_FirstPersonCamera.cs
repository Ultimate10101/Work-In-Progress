using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to a cam holder_empty game object
public class P_FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private GameObject gameCam;
    [SerializeField] private GameObject player;

    private float xRotation;
    private float yRotation;

    private int sensitivityX = 5;
    private int sensitivityY = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameCam.transform.position = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManagerRef.GamePaused && !GameManager.gameManagerRef.GameOver)
        {
            CameraMovement();
        }
    }

    void LateUpdate()
    {
        gameCam.transform.position = transform.position;
    
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY;

        yRotation += mouseX;
        xRotation -= mouseY;

        // Constraints X rotation; prevents player from looking more than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        gameCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
        player.transform.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }


}

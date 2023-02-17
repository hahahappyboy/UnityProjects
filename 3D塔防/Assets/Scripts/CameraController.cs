using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // X 20 40
    // Y 17
    // Z 2 30\
    private Transform cameraTrans;
    
    public float sensitivityDrag = 1;
    void Start()
    {
        cameraTrans = this.GetComponent<Camera>().transform;
        cameraTrans.position = new Vector3(20, 17, 2);
    }

    // Update is called once per frame
    void Update() {
        MouseLeftDragControl();
    }
    
    private void MouseLeftDragControl()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 transPosition = cameraTrans.position
                         - Vector3.right * Input.GetAxisRaw("Mouse X") * sensitivityDrag * Time.timeScale
                         - Vector3.forward * Input.GetAxisRaw("Mouse Y") * sensitivityDrag * Time.timeScale;
            cameraTrans.position = transPosition;
            cameraTrans.position = new Vector3(Mathf.Clamp(cameraTrans.position.x,20,40),
                                   cameraTrans.position.y,
                                   Mathf.Clamp(cameraTrans.position.z,2, 30));
            
        }
    }


}

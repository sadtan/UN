using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable3D : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance = 0.1f;

    public float minScale = 0.1f;
    public float maxScale = 10f;


    private const string AXIS_MOUSE_X = "Mouse X";
    private const string AXIS_MOUSE_Y = "Mouse Y";
    private const string AXIS_JOYSTICK_X = "Horizontal";
    private const string AXIS_JOYSTICK_Y = "Vertical";

    private float mouseX = 0;
    private float mouseY = 0;
    private float mouseZ = 0;

    public Quaternion rotation {get; private set;}
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // float tt = transform.localScale.x;
        // tt+=Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime * 20f;
        // tt = Mathf.Clamp(tt, minScale, maxScale);
        // distance += Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime * 20f;
        // // transform.localScale = new Vector3(tt, tt, tt);

        // transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        if (CanChangeYawPitch()) {
            mouseX -= Input.GetAxis(AXIS_MOUSE_X) * 100f * 3 * Time.deltaTime;
            mouseX += Input.GetAxis(AXIS_JOYSTICK_X) * 100f * Time.deltaTime;

            if (mouseX <= -180)
            {
                mouseX += 360;
            }
            else if (mouseX > 180)
            {
                mouseX -= 360;
            }

            mouseY -= Input.GetAxis(AXIS_MOUSE_Y) * 48f * 3 * Time.deltaTime;
            mouseY += Input.GetAxis(AXIS_JOYSTICK_Y) * 48f * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -30, 30);


            // rotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
            rotation = Quaternion.Euler(mouseZ, mouseX, mouseY);
            transform.localRotation = rotation;
        }

        // Debug.Log("?");
        // transform.rotation.eulerAngles
        // transform.Rotate((Input.GetAxis("Mouse Y") * 60f * Time.deltaTime), (Input.GetAxis("Mouse X") * 60f * Time.deltaTime), 0, Space.World);
    }

    public void PositionInCamera() {
        // transform.localScale = new Vector3(1f, 1f, 1f);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
    }

    public IEnumerator Scale(float targetScale, float time) {
        while (!IsBetween(transform.localScale.x,  targetScale -0.5f, targetScale + 0.5f)) {
            
            float step = targetScale * Time.deltaTime / time; 
            transform.localScale += new Vector3(step, step, step);
            yield return null;

        }


    }


    private bool CanChangeYawPitch() {
        if (GameEvents.current.GetGlobalState() == "ObjInteraction") {

            if (Input.touchCount == 1) {
            Touch touchZero = Input.GetTouch(0);
            if (touchZero.phase == TouchPhase.Moved) return true;
            }

            if (Input.GetAxis(AXIS_JOYSTICK_X) != 0 || Input.GetAxis(AXIS_JOYSTICK_Y) != 0) return true;

            return Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.LeftAlt);

        }
        
         return false;
    }

    public bool IsBetween(double testValue, double bound1, double bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }
}

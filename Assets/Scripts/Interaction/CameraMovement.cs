using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public static CameraMovement current;
    Vector2 touchDeltaPosition;
    public Quaternion HeadRotation { get; private set; }
    public Vector3 HeadPosition { get; private set; }
    private static readonly Vector3 NECK_OFFSET = new Vector3(0, 0.075f, 0.08f);
    private const string AXIS_MOUSE_X = "Mouse X";
    private const string AXIS_MOUSE_Y = "Mouse Y";
    private const string AXIS_JOYSTICK_X = "Horizontal";
    private const string AXIS_JOYSTICK_Y = "Vertical";
    private float mouseX = 0;
    private float mouseY = 0;
    private float mouseZ = 0;

    private Transform target;
    public bool cRunning = false;
    public bool zoom = true;


    public void Awake() {
        current = this;
        ApplyHeadOrientation();
    }

    void Update () {

        if (GameEvents.current.GetGlobalState() == "CamInteraction") {
            if ( CanChangeYawPitch() ) {

                GameEvents.current.CamInteractionEnter();

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                mouseX -= Input.GetAxis(AXIS_MOUSE_X) * 100f * 3 * Time.deltaTime;
                mouseX += Input.GetAxis(AXIS_JOYSTICK_X) * 100f * Time.deltaTime;
                if (mouseX <= -180) mouseX += 360;
                else if (mouseX > 180)  mouseX -= 360;

                mouseY += Input.GetAxis(AXIS_MOUSE_Y) * 48f * 3 * Time.deltaTime;
                mouseY -= Input.GetAxis(AXIS_JOYSTICK_Y) * 48f * Time.deltaTime;
                mouseY = Mathf.Clamp(mouseY, -85, 70);
                
            } else {
                GameEvents.current.CamInteractionExit();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            ApplyHeadOrientation();

        } else if (GameEvents.current.GetGlobalState() == "ModalInteraction") { // show cursor if it's moving
            
            if (Mathf.Abs(Input.GetAxis(AXIS_MOUSE_X)) > 0 || Mathf.Abs(Input.GetAxis(AXIS_MOUSE_Y)) > 0) Cursor.visible = true; 
            else Cursor.visible = false;

        } else if (GameEvents.current.GetGlobalState() == "Focusing") {

            Cursor.visible = false;

            if (!cRunning) {
                cRunning = true;
                LookAtTarget();
            }

            mouseX = Clamp0360(mouseX);
            mouseY = Clamp0360(mouseY);
            
            ApplyHeadOrientation();
        }

        if (mouseY >= 85) {
            mouseY -= 360;
        }
    }

    private bool CanChangeYawPitch() {
        if (GameEvents.current.GetGlobalState() == "CamInteraction") {

            if (Input.touchCount == 1) {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Moved) return true;
            }

            if (Input.GetAxis(AXIS_JOYSTICK_X) != 0 || Input.GetAxis(AXIS_JOYSTICK_Y) != 0) return true;

            return Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.LeftAlt);
        }
        return false;
    }

    public void LookAtTarget() {
        StartCoroutine(LookAndZoom());
    }

    public IEnumerator LookAndZoom() {
        Quaternion result = Quaternion.LookRotation( target.position - transform.parent.position);
        StartCoroutine (IELookAtTarget());

        while (transform.rotation != result) {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        if (zoom) {
            StartCoroutine (FoVLerp(1.5f, 40f));
            yield return new WaitForSeconds(1.5f);
            GameEvents.current.SetGlobalState("Done");
        } else GameEvents.current.SetGlobalState("Done");

        cRunning = false;
    }
    public IEnumerator IELookAtTarget() {

        Quaternion result = Quaternion.LookRotation( target.position - transform.parent.position);

        Transform startRot = transform;

        var q = Quaternion.LookRotation(target.position - transform.position);

        while (transform.rotation != result) {
            
            float prevoiusY = transform.eulerAngles.y;
            float prevoiusX = transform.eulerAngles.x;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q,  40f * Time.deltaTime );
            float currentY = transform.eulerAngles.y;
            float currentX = transform.eulerAngles.x;
            
            if (  Mathf.Abs(currentX - prevoiusX) < 360 && Mathf.Abs(currentY - prevoiusY) < 360 ) {
                mouseX += currentY - prevoiusY;
                mouseY += currentX - prevoiusX;
            } 

            yield return null;
        }
    }
    public static float Clamp0360(float eulerAngles) {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
            result += 360f;
        return result;
    }
    public void LerpFoV() {
        StartCoroutine (FoVLerp(0.5f, 92f));
    }
    public IEnumerator FoVLerp(float time, float FoV) {
        //  Camera.main.fieldOfView != FoV
        while ( !IsBetween(Mathf.Abs(Camera.main.fieldOfView - FoV), -5f, 5f)) {
             if (Camera.main.fieldOfView != FoV) 
                if (Camera.main.fieldOfView > FoV)
                    Camera.main.fieldOfView -= (Camera.main.fieldOfView * Time.deltaTime / time);
                else if (Camera.main.fieldOfView <= FoV)
                    Camera.main.fieldOfView += (Camera.main.fieldOfView * Time.deltaTime / time);
            yield return null;
        }
        
    }
    public bool IsBetween(double testValue, double bound1, double bound2) {
        if (bound1 > bound2) return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }

    private void ApplyHeadOrientation() {
        HeadRotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
        transform.localRotation = HeadRotation;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}

    // if (Input.GetMouseButton(0))
    // {
    //     float pointer_x = Input.GetAxis("Mouse X");
    //     float pointer_y = Input.GetAxis("Mouse Y");
        
    //     transform.Rotate(pointer_y * 0.5f, -pointer_x * 0.5f, 0);
    // }
    
    // if (Input.touchCount == 1)
    // {
    //     Touch touchZero = Input.GetTouch(0);
    //     if (touchZero.phase == TouchPhase.Moved)
    //     {
    //         touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    //         // gameObject.transform.Rotate(touchDeltaPosition.y * .05f, -touchDeltaPosition.x * .4f, 0);
    //     }
    // }

    // public IEnumerator LookAt() {
    //     Debug.Log("C Start: " + cRunning);
    //     autoCam = true;
    //     var step = 40f * Time.deltaTime;
    //     // target = transform.Inver;
    //     // transform.rotation = Quaternion.RotateTowards(  transform.rotation, target.rotation, step);

    //     // transform.rotation = Quaternion.LookRotation( target.position - transform.position);
    //     // Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime);

    //     var q = Quaternion.LookRotation(target.position - transform.position);
    //     Transform prevRotation = transform;
    //     transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 40f * Time.deltaTime);
    //     Transform currentRotation = transform;

    //     Debug.Log( Vector3.Angle( prevRotation.transform.forward, currentRotation.transform.forward ));
    //     mouseX -= Vector3.Angle( prevRotation.transform.forward, currentRotation.transform.forward );
    //     // mouseY += 5f;
        
    //     var newDir = Vector3.RotateTowards(current.transform.forward, target.position - current.transform.position, step, 0.0f);
    //     // yield return Vector3.Angle( current.transform.forward, newDir) < 1;
    //     yield return new WaitForSeconds(2f);
    //     autoCam = false;
    // }

    // if (Camera.main.fieldOfView > FoV)
    //     Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, FoV, 5f * Time.deltaTime);
    // else Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, FoV, -5f * Time.deltaTime);

    

    // while ( !IsBetween(Camera.main.fieldOfView, FoV - 0.5f, FoV + 0.5f) ) {
    //     Debug.Log("C Stop" + FoV + " ++ " + Camera.main.fieldOfView);
    //     yield return null;
    // }

     // Debug.Log(mouseX);
    // mouseX += 5f;

    // Quaternion result = Quaternion.RotateTowards(transform.rotation, q, 0.2f * Time.deltaTime);
    // Transform prevRotation = transform;

    // Debug.Log(Vector3.Angle( prevRotation.transform.forward, currentRotation.transform.forward ));
    // mouseX -= 40f * Time.deltaTime;
    // mouseY += 40f * Time.deltaTime;
    
    // var newDir = Vector3.RotateTowards(current.transform.forward, target.position - current.transform.position, step, 0.0f);
    // yield return Vector3.Angle( current.transform.forward, newDir) < 1;
    // yield return new WaitForSeconds(2f);
    // Debug.Log(Vector3.Angle( current.transform.forward, newDir).ToString());
    
    // if (transform.rotation == result) cRunning = false;

    // Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime);

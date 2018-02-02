using UnityEngine;
using System.Collections;
using UnityEngine.iOS;

public class Portal : MonoBehaviour {

	[Header("Cameras")]
	public Camera mainCam;
	public Camera portalCam;

    public bool portalCamLock = true;

  
	void Update () {

        

		// Move portal camera position based on main camera distance from the portal - tested without this and potalcam Z axis still rotates
		Vector3 cameraOffset = mainCam.transform.position - transform.position;
		portalCam.transform.position = transform.position + cameraOffset;

        // Make portal cam face the same direction as the main camera - this is the original code 
        //portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);


        if (portalCamLock)
        {
            float xAngle = GetAngleByDeviceAxis(Vector3.forward);
            //Debug.Log("Device Z axis is:" + (xAngle));

            // conerts quaternion maincam rotation to euler maincam rotation
            // sets z axis of maincam rotation to inverse of xAngle, which is the inverse of device physical rotation
            // portalcam rotation is set to the this new value, with the z value inverted. this also affects the x rotation.
            var rotationVector = mainCam.transform.rotation.eulerAngles; 
            rotationVector.z = xAngle;  
            portalCam.transform.rotation = Quaternion.Euler(rotationVector.x,rotationVector.y,+rotationVector.z); //succeeded in making the camera rotate more! 90 degrees device rotation == 180 degrees portalcam rotation


        }else{

            // Make portal cam face the same direction as the main camera.
            portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
        }
	}

    //-------------------------------------------------------------------------------------

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("MainCamera")) {
            // Use xor operator to toggle the ARWorld layer in the mainCam's culling mask.
            mainCam.cullingMask ^= 1 << LayerMask.NameToLayer("ARWorld");
            if (portalCamLock)
            {
                portalCamLock = false;
                    Debug.Log("portalCamLock is false");
            }
            else { portalCamLock = true; }

		}
	}

    //-----------------------------------------------------------------------------------------

    // returns the z axis rotation

    float GetAngleByDeviceAxis(Vector3 axis)
    {
        Quaternion deviceRotation = DeviceRotation.GetRotation();
        Quaternion eliminationOfOthers = Quaternion.Inverse(
            Quaternion.FromToRotation(axis, deviceRotation * axis)
        );
        Vector3 filteredEuler = (eliminationOfOthers * deviceRotation).eulerAngles;

        float result = filteredEuler.z;
        if (axis == Vector3.up)
        {
            result = filteredEuler.y;
        }
        if (axis == Vector3.right)
        {
            // incorporate different euler representations.
            result = (filteredEuler.y > 90 && filteredEuler.y < 270) ? 180 - filteredEuler.x : filteredEuler.x;
        }
        return result;
    }


}



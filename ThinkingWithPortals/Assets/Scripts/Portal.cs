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


       // Vector3 mainCamRotation = mainCam.transform.rotation.eulerAngles;
       // portalCam.transform.rotation.eulerAngles = new Vector3(mainCamRotation.x,mainCamRotation.y,0);
         

        //Debug.Log("portalcam Z rotation:  " + portalCam.transform.rotation.z);


    

        if (portalCamLock)
        {

            //-----------------------------------------------Attempt 1-----------------------------------------------------------

            // convert the portalcam rotation to eulerangles, then lock the Z axis rotation to zero
            //Vector3 portalCamRoatation = portalCam.transform.rotation.eulerAngles;
            //portalCamRoatation = new Vector3(portalCamRoatation.x, portalCamRoatation.y, 0);

            //-----------------------------------------------Attempt 2------------------------------------------------------------

            // Make portal cam face same as mainCam, using vector3.forward to lock z axis rotations
            //portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.position += Vector3.forward, Vector3.up);

            //------------------------------------------------Attempt 3------------------------------------------------------------

            var rotationVector = mainCam.transform.rotation.eulerAngles;
            rotationVector.z = 0;
            //rotationVector.x = 0;
            portalCam.transform.rotation = Quaternion.Euler(rotationVector);
            Debug.Log("portalcam Z rotation:  " + portalCam.transform.rotation.eulerAngles.z);

            //-----------------------------------------------Attempt 4------------------------------------------------------------

            //Quaternion deviceRotation = DeviceRotation.Get;


        }else{

            // Make portal cam face the same direction as the main camera.
            portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
        }
        


	}

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


}



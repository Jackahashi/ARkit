using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	[Header("Cameras")]
	public Camera mainCam;
	public Camera portalCam;



	void Update () {

		// Move portal camera position based on main camera distance from the portal.
		Vector3 cameraOffset = mainCam.transform.position - transform.position;
		portalCam.transform.position = transform.position + cameraOffset;

		// Make portal cam face the same direction as the main camera.
		portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);

        // create a boolean condition around portal entry, then call the 'non rotating' camera whenever we're outside the portal.

        //convert main camera transform from quaternion to eulerangles

        //use the eulerangles rotation to position the portalcam, but set the  z axis to zero to prevent the portal image from rotating
       // mainCam.transform.rotation.eulerAngles = new Vector3(0, 0, 0);
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("MainCamera")) {
            // Use xor operator to toggle the ARWorld layer in the mainCam's culling mask.
            mainCam.cullingMask ^= 1 << LayerMask.NameToLayer("ARWorld");

		}
	}


}
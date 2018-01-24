using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	[Header("Cameras")]
	public Camera mainCam;
	public Camera portalCam;
    public string arworld1;
   // public float coolDown = 1.0f;
   // private bool portalActive = true;

	void Update () {

		// Move portal camera position based on main camera distance from the portal.
		Vector3 cameraOffset = mainCam.transform.position - transform.position;
		portalCam.transform.position = transform.position + cameraOffset;

		// Make portal cam face the same direction as the main camera.
		portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("MainCamera")) {
            // Use xor operator to toggle the ARWorld layer in the mainCam's culling mask.
            mainCam.cullingMask ^= 1 << LayerMask.NameToLayer(arworld1);
            portalCam.cullingMask ^= 1 << LayerMask.NameToLayer(arworld1);
           // portalActive = false;
           // StartCoroutine(CoolDown());
		}
	}

  //  IEnumerator CoolDown()
  //  {
  //     yield return new WaitForSeconds(coolDown);
  //     portalActive = true
    //
  //  }
}
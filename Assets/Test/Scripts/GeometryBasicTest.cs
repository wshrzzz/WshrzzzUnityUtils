using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtils;

public class GeometryBasicTest : MonoBehaviour {

    public Transform testCube1;
    public Transform testCube2;
    public Transform testCube3;

	// Update is called once per frame
	void Update () {
        testCube1.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);

        testCube2.rotation = GeometryBasic.LookToQuaternion(testCube1.position - testCube2.position, Vector3.up, upIsPrimary: true);
        testCube3.rotation = GeometryBasic.LookToQuaternion(testCube1.position - testCube3.position, Vector3.up, upIsPrimary: false);
	}
}

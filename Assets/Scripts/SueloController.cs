using UnityEngine;
using System.Collections;

public class SueloController : MonoBehaviour {

    private bool isAndroid;

    void Start () {
#if UNITY_ANDROID
        isAndroid = true;
#else
        isAndroid = false;
#endif
    }
	
	void FixedUpdate () {
        if (isAndroid)
        {
            float posH = Input.acceleration.x;
            float posV = Input.acceleration.y;

            Vector3 movimiento = new Vector3(posV, posH, 0);

            transform.Rotate(movimiento);
        }
        else
        {
            float posH = Input.GetAxis("Horizontal");
            float posV = Input.GetAxis("Vertical");

            Vector3 movimiento = new Vector3(posV, posH, 0);

            transform.Rotate(movimiento);
        }
    }
}

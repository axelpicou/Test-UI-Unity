using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    public new Camera camera;
    public RectTransform objectToRotate;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, camera.transform.rotation, 3f * Time.deltaTime);
    }
}


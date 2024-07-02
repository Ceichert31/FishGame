using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVObjectScaling : MonoBehaviour
{

    [SerializeField] private float scaleFactor;

    private Camera mainCamera;

    private Vector3 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInParent<Camera>();

        originPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scaledPosition = new (transform.localPosition.x, transform.localPosition.y, mainCamera.fieldOfView % scaleFactor); 

        transform.localPosition = scaledPosition;
    }
}

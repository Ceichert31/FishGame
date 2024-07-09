using UnityEngine;

public class BobberController : MonoBehaviour
{
    private LineRenderer bobberLineRenderer;

    private Transform bobberTransform;

    // Start is called before the first frame update
    void Start()
    {
        bobberLineRenderer = GetComponent<LineRenderer>();

        bobberTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        bobberLineRenderer.SetPosition(1, bobberTransform.localPosition);  
    }
}

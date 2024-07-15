using UnityEngine;

public class BobberController : MonoBehaviour
{
    [Header("Bobber References")]

    private LineRenderer bobberLineRenderer;

    [SerializeField] private GameObject bobberPrefab;

    private GameObject bobberInstance;

    // Start is called before the first frame update
    void Start()
    {
        bobberLineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bobberLineRenderer.SetPosition(0, transform.position);

        if (bobberInstance != null)
            bobberLineRenderer.SetPosition(1, bobberInstance.transform.position);  
    }

    public void ApplyForcesOnBobber(float castDistance, float initialVelocity)
    {
        DisableBobber();

        bobberInstance = Instantiate(bobberPrefab, transform.position, Quaternion.identity);
        
        Rigidbody rb = bobberInstance.GetComponent<Rigidbody>();

        bobberLineRenderer.enabled = true;

        rb.velocity = castDistance * new Vector3(transform.parent.forward.x, initialVelocity, transform.parent.forward.z);
    }
    public void DisableBobber()
    {
        bobberLineRenderer.enabled = false;

        if (bobberInstance != null)
            Destroy(bobberInstance);
    }
}

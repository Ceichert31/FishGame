using UnityEngine;

public class BobberController : MonoBehaviour
{
    [Header("Bobber References")]
    private LineRenderer bobberLineRenderer;

    [SerializeField] private GameObject bobberPrefab;

    private GameObject bobberInstance;

    void Start()
    {
        bobberLineRenderer = GetComponent<LineRenderer>();
    }

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
        bobberLineRenderer.SetPosition(1, transform.position);

        bobberLineRenderer.enabled = false;

        if (bobberInstance != null)
            Destroy(bobberInstance);
    }

   
}

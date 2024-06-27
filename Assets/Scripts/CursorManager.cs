using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private CursorLockMode defaultMode;
    private void Awake() => Cursor.lockState = defaultMode;


    private void Update()
    {
        if (Time.timeScale == 0)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = defaultMode;
    }
}

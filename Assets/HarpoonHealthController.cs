using UnityEngine;

public class HarpoonHealthController : MonoBehaviour
{
    [SerializeField] Material fillMat;

    const float MAX_HEALTH_VALUE = 0.673f;
    const float MIN_HEALTH_VALUE = 0.368f;
    float maxHealth;

    private void Start()
    {
        fillMat.SetFloat("_FillAmount", MAX_HEALTH_VALUE);
    }

    /// <summary>
    /// Gets the players current health and updates the display accordingly 
    /// </summary>
    /// <param name="ctx"></param>
    public void SetDisplayHealth(FloatEvent ctx)
    {
        float currentHealth = ctx.FloatValue;

        //Divide by max health to make a value between 0-1
        currentHealth /= maxHealth;

        //Normalize health between min and max ranges
        currentHealth *= MAX_HEALTH_VALUE;

        //Update material after clamping between min and max
        fillMat.SetFloat("_FillAmount", currentHealth);
    }

    public void SetMaxHealth(FloatEvent ctx) 
    {
        maxHealth = ctx.FloatValue;
    }
}

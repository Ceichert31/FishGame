using UnityEngine;

public class SetTerrain : MonoBehaviour
{
    [SerializeField] GameObject terrain;
    public void SetTerrainObject(BoolEvent ctx)
    {
        terrain.SetActive(ctx.Value);
    }
}

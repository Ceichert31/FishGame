using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockStateController : MonoBehaviour
{
    [SerializeField] MeshFilter mainMesh;

    [SerializeField] Flock flockMain;

    List<FlockUnit> actualUnits = new List<FlockUnit>();

    [SerializeField] Vector3[] verts;
    [SerializeField] float lerpSpeed;

    [SerializeField] bool goThere;

    public bool GoThere { get => goThere; set {goThere = value; } }

    [SerializeField] AnimationCurve animCirve;

    // Start is called before the first frame update
    void Start()
    {
        verts = mainMesh.mesh.vertices;


        

        AssignProperVerts();
    }

    // Update is called once per frame
    void Update()
    {
        if(goThere)
        {
            LerpThoseBitches();
        }
    }

    void AssignProperVerts()
    {
        int flockUnitCounter = 0;
        for (int i = 0; i < mainMesh.mesh.vertices.Length; i += 4)
        {
            if(flockUnitCounter >= flockMain.allUnits.Length)
            {
                break;
            }
            actualUnits.Add(flockMain.allUnits[flockUnitCounter]);
            actualUnits[flockUnitCounter].assignedVert = i;
            flockUnitCounter++;
        }
    }

    void LerpThoseBitches()
    {
        int length = actualUnits.Count;
        for (int i = 0; i < length; i++)
        {
            FlockUnit unit = actualUnits[i];
            Vector3 targetWorld = mainMesh.transform.TransformPoint(verts[unit.assignedVert]);

            actualUnits[i].transform.localPosition = Vector3.Lerp(unit.transform.position, targetWorld, Time.deltaTime * lerpSpeed);
            actualUnits[i].transform.forward = (targetWorld - unit.transform.position).normalized;
        }
    }
}

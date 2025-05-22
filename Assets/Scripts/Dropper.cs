using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]float timeToWait = 2f;

    MeshRenderer myMeshRenderer;
    Rigidbody myRidgibody;
    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        myMeshRenderer.enabled = false;

        myRidgibody = GetComponent<Rigidbody>();
        myRidgibody.useGravity = false;
    }

   
    void Update()
    {
        if (Time.time > timeToWait)
        {
            myMeshRenderer.enabled = true;
            myRidgibody.useGravity = true;
        }
    }
}

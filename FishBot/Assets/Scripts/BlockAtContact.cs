using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles blocking when hitting a platform (INCOMPLETE)
public class BlockAtContact : MonoBehaviour
{
    public Mesh mesh;
    public Vector3[] vertices;

    //private GameController gameController;
    private Rigidbody player;
    private bool touchingBlock;

    void OnTriggerEnter(Collider other)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Debug.Log(mesh.vertexCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

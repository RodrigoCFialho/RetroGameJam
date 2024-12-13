using UnityEngine;

public class CubeDemo : MonoBehaviour
{
    public GameObject cube;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.Rotate(new Vector3(20, 20, 20) * Time.deltaTime);
    }
}

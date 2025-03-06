using UnityEngine;

public class Spawner : MonoBehaviour
{   
    public GameObject pipePrefab;
    public float spawnTime = 10;
    public float spawnDelay = 0;
    public float offset = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        spawnPipe();
    }

    // Update is called once per frame
    void Update(){
        if(spawnDelay < spawnTime) {
            spawnDelay += Time.deltaTime;
        }
        else {
            spawnDelay = 0;
            spawnPipe();
        }
    }

    void spawnPipe(){
        float low = transform.position.y - offset;
        float high = transform.position.y + offset;
        Vector3 position = new Vector3(transform.position.x, Random.Range(low, high), transform.position.z);
        Instantiate(pipePrefab, position, Quaternion.identity);
    }
}

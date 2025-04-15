using UnityEngine;

public class ChaserBehaviour : MonoBehaviour
{
    public Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Vector3 current = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        current = Vector3.Lerp(current, player.position, 0.3f * Time.deltaTime);
        transform.position = current;
    }
}

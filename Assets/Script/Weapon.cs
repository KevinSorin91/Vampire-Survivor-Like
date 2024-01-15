using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform player;
    public Animation slash;
    public GameObject expShard;
    public AudioSource deathSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            // Instantiate Exp shards
            expShard.transform.position = collision.collider.transform.position;
            Instantiate(expShard, expShard.transform);

            // Destroy enemy prefab and adding DeathSound
            collision.collider.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(deathSound.clip, collision.collider.transform.position);
            Destroy(collision.collider.gameObject, collision.collider.gameObject.GetComponent<AudioSource>().clip.length);
            FindAnyObjectByType<GameManager>().RespawnEnemy();
        }
    }

    private void Start()
    {
        transform.parent = player.transform;
    }

    public void Attack()
    {
        slash.Play();
    }
}

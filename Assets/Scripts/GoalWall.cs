using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalWall : MonoBehaviour
{
    [SerializeField] string wallTag = "Wall";
    [SerializeField] AudioClip hitWallSound;

    public string ReturnTag()
    {
        return wallTag;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        FindObjectOfType<GameSession>().CountScore(wallTag);
        AudioSource.PlayClipAtPoint(hitWallSound, Camera.main.transform.position);
    }
}

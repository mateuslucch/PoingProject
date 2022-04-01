using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioClip ballSounds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<GoalWall>())
        {
            AudioSource.PlayClipAtPoint(ballSounds, Camera.main.transform.position);
        }
    }
}

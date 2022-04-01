using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{

    [SerializeField] float speed = 1f;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    void Update()
    {
        MoveThePaddle();
    }

    private void MoveThePaddle()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector2(transform.position.x, newYPos);
    }
}

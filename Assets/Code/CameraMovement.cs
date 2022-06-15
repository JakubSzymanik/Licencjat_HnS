using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float lerpValue;
    [SerializeField] Transform player;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, lerpValue * Time.deltaTime);
        //test
    }
}

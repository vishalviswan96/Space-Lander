using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //Script used for followin the target Player

    [SerializeField] public Transform target;
    [SerializeField] public Transform background_Sprite;

    [SerializeField] private float min_Height, max_Height;

    private float last_Position;

    private void Start()
    {
        last_Position = transform.position.x;
    }
    private void Update()
    {
        //Clamps the Background between min and max height

        float clampedY = Mathf.Clamp(target.position.y, min_Height, max_Height);

        transform.position = new Vector3(target.position.x, clampedY, transform.position.z);

        float amountToMoveX = transform.position.x - last_Position;

        background_Sprite.position += new Vector3(amountToMoveX, 0f, 0f);

        last_Position = transform.position.x;
    }
}

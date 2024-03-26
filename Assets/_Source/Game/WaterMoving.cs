using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    [SerializeField]
    float speedX;
    [SerializeField]
    float amplitudeX;
    [SerializeField]
    float speedZ;
    [SerializeField]
    float amplitudeZ;


    void Update()
    {
        var y = gameObject.transform.position.y;
        var x = Mathf.Sin((float)Time.timeAsDouble * speedX) * amplitudeX;
        var z = Mathf.Sin((float)Time.timeAsDouble * speedZ) * amplitudeZ;
        gameObject.transform.position = new Vector3(x, y, z);
    }
}

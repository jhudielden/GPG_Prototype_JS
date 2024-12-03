using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    // Movement Script https://youtu.be/FXqwunFQuao?si=MSj36lxP8ikZEhj_
    // Clamping Script https://youtu.be/Fqht4gyqFbo?si=vquIaBIUKIUl3aTG
    public float FollowSpeed = 2f;
   
    public Transform target;
    public Vector3 minValues, maxValues;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(
              Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
              Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
              Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z)
              );
        
        transform.position = Vector3.Slerp(transform.position, boundPosition, FollowSpeed * Time.deltaTime);
       
    }
}

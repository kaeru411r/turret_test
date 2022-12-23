using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform _sight;
    [SerializeField] Vector2 _speed;
    [SerializeField] Transform _barrel;
    float time = 1;
    private void FixedUpdate()
    {
        if (_sight)
        {
            Rotate(transform, transform, _sight.forward, _speed.x, Vector3.up, Time.fixedDeltaTime);
            if (_barrel)
            {
                Pitch(transform, _barrel, _sight.forward, _speed.y, Vector3.right, Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coordinateTransform"></param>
    /// <param name="selfTransform"></param>
    /// <param name="forward"></param>
    /// <param name="speed"></param>
    /// <param name="axis"></param>
    /// <param name="deltaTime"></param>
    void Rotate (Transform coordinateTransform, Transform selfTransform, Vector3 forward, float speed, Vector2 axis, float deltaTime)
    {
        Vector3 dir = coordinateTransform.InverseTransformDirection(forward);
        float side = Vector3.Dot(Vector3.Cross(Vector3.back, axis), dir);
        float y = Mathf.Atan2(side, dir.z) / Mathf.PI * 180;
        float lr = y / Mathf.Abs(y);
        float angle = Mathf.Abs(speed * deltaTime) < Mathf.Abs(y) ? speed * deltaTime * lr : y;

        selfTransform.Rotate(axis, angle, Space.Self);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coordinateTransform"></param>
    /// <param name="selfTransform"></param>
    /// <param name="forward"></param>
    /// <param name="speed"></param>
    /// <param name="axis"></param>
    /// <param name="deltaTime"></param>
    void Pitch(Transform coordinateTransform, Transform selfTransform, Vector3 forward, float speed, Vector2 axis, float deltaTime)
    {
        Vector3 dir = coordinateTransform.InverseTransformDirection(forward).normalized;
        float sight = Mathf.Acos(Vector3.Dot(coordinateTransform.up, dir)) / Mathf.PI * 180;
        float self = Mathf.Asin(Vector3.Dot(coordinateTransform.up, selfTransform.forward)) / Mathf.PI * 180;
        float y = sight - self;
        float lr = y / Mathf.Abs(y);
        Debug.Log($"{self}, {sight}");
        float angle = Mathf.Abs(speed * deltaTime) < Mathf.Abs(y) ? speed * deltaTime * lr : y;

        selfTransform.Rotate(axis, angle, Space.Self);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            Rotate(transform, transform, _sight.forward, _speed.x, Time.fixedDeltaTime);
            if (_barrel)
            {
                Pitch(transform, _barrel, _sight.forward, _speed.y, Time.fixedDeltaTime);
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
    void Rotate(in Transform coordinateTransform, Transform selfTransform, in Vector3 forward, in float speed, in float deltaTime)
    {
        Vector3 dir = coordinateTransform.InverseTransformDirection(forward);
        float side = Vector3.Dot(Vector3.right, dir);
        float y = Mathf.Atan2(side, dir.z) / Mathf.PI * 180;
        float lr = y < 0 ? -1 : 1;
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y < 0 ? -1 : 1) : y;

        selfTransform.Rotate(Vector3.up, angle, Space.Self);
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
    void Pitch(in Transform coordinateTransform, Transform selfTransform, in Vector3 forward, float speed,  in float deltaTime)
    {
        float sightDot = Mathf.Clamp(Vector3.Dot(coordinateTransform.up, forward), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(coordinateTransform.up, selfTransform.forward), -1, 1);
        float sight = Mathf.Acos(sightDot) * Mathf.Rad2Deg;
        float self = Mathf.Acos(selfDot) * Mathf.Rad2Deg;
        float y = sight - self;
        int up = (selfTransform.up.y < 0 ? -1 : 1);
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y > 0 ? 1 : -1) : y * up;
        selfTransform.Rotate(Vector3.right, angle, Space.Self);
    }
}

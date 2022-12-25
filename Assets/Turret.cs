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
            transform.rotation = Rotate(transform, transform, _sight.forward, _speed.x, Time.fixedDeltaTime);
            if (_barrel)
            {
                _barrel.rotation = Pitch(transform, _barrel, _sight.forward, _speed.y, Time.fixedDeltaTime);
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
    /// <param name="deltaTime"></param>
    Quaternion Rotate(in Transform coordinateTransform, in Transform selfTransform, in Vector3 forward, in float speed, in float deltaTime)
    {
        Vector3 dir = coordinateTransform.InverseTransformDirection(forward);
        float y = Mathf.Atan2(dir.x, dir.z) / Mathf.PI * 180;
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y < 0 ? -1 : 1) : y;

        return selfTransform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coordinateTransform"></param>
    /// <param name="selfTransform"></param>
    /// <param name="forward"></param>
    /// <param name="speed"></param>
    /// <param name="deltaTime"></param>
    Quaternion Pitch(in Transform coordinateTransform, in Transform selfTransform, in Vector3 forward, in float speed, in float deltaTime)
    {
        float sightDot = Mathf.Clamp(Vector3.Dot(coordinateTransform.up, forward), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(coordinateTransform.up, selfTransform.forward), -1, 1);
        float sight = Mathf.Acos(sightDot) * Mathf.Rad2Deg;
        float self = Mathf.Acos(selfDot) * Mathf.Rad2Deg;
        float y = sight - self;
        int up = (selfTransform.up.y < 0 ? -1 : 1);
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y > 0 ? 1 : -1) : y * up;

        return selfTransform.rotation * Quaternion.AngleAxis(angle, Vector3.right);
    }
}

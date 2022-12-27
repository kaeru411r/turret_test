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
            transform.rotation = Rotate(transform, _sight.forward, _speed.x, Time.fixedDeltaTime);
            if (_barrel)
            {
                _barrel.rotation = Pitch(_barrel, transform.up, _sight.forward, _speed.y, Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// 旋回を計算する
    /// </summary>
    /// <param name="transform">動かすオブジェクトのTransform</param>
    /// <param name="forward">サイトの正面ベクトル</param>
    /// <param name="speed">旋回速度</param>
    /// <param name="deltaTime">経過時間</param>
    /// <returns>回転後のQuaternion</returns>
    Quaternion Rotate(in Transform transform, in Vector3 forward, in float speed, in float deltaTime)
    {
        Vector3 dir = transform.InverseTransformDirection(forward);
        float y = Mathf.Atan2(dir.x, dir.z) * Mathf.Deg2Rad;
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y < 0 ? -1 : 1) : y;

        return transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
    }

    /// <summary>
    /// 砲身の動作を計算する
    /// </summary>
    /// <param name="selfTransform">動かすオブジェクトのTransform</param>
    /// <param name="baseAxis">基部の回転軸</param>
    /// <param name="forward">サイトの正面ベクトル</param>
    /// <param name="speed">旋回速度</param>
    /// <param name="deltaTime">経過時間</param>
    /// <returns>回転後のQuaternion</returns>
    Quaternion Pitch(in Transform selfTransform, in Vector3 baseAxis, in Vector3 forward, in float speed, in float deltaTime)
    {
        float sightDot = Mathf.Clamp(Vector3.Dot(baseAxis, forward), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(baseAxis, selfTransform.forward), -1, 1);
        float sight = Mathf.Acos(sightDot) * Mathf.Rad2Deg;
        float self = Mathf.Acos(selfDot) * Mathf.Rad2Deg;
        float y = sight - self;
        int up = (Vector3.Dot(baseAxis, selfTransform.up) < 0 ? -1 : 1);
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y > 0 ? 1 : -1) : y * up;

        return selfTransform.rotation * Quaternion.AngleAxis(angle, Vector3.right);
    }
}

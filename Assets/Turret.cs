using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform _sight;
    [SerializeField] Vector2 _speed;
    [SerializeField] Transform _turret;
    [SerializeField] Transform _barrel;


    private void FixedUpdate()
    {
        if (_sight && _turret)
        {
            _turret.rotation = Rotate(_turret, _sight.forward, _speed.x, Time.fixedDeltaTime);
            if (_barrel)
            {
                _barrel.rotation = Pitch(_barrel, _turret.up, _sight.forward, _speed.y, Time.fixedDeltaTime);
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
        float y = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
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
        Vector3 bAxisNomal = baseAxis.normalized;
        //ベクトルの基部に対する高さ成分
        //(これらはDot関数の戻り値が1を超過したり-1未満だったりすることがあるので丸めてある)
        float sightDot = Mathf.Clamp(Vector3.Dot(bAxisNomal, forward.normalized), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(bAxisNomal, selfTransform.forward), -1, 1);

        //基部からの角度[度]
        float sightTheta = Mathf.Acos(sightDot) * Mathf.Rad2Deg;
        float selfTheta = Mathf.Acos(selfDot) * Mathf.Rad2Deg;

        float y = sightTheta - selfTheta;
        int up = (Vector3.Dot(bAxisNomal, selfTransform.up) < 0 ? -1 : 1);
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y > 0 ? 1 : -1) : y * up;

        return selfTransform.rotation * Quaternion.AngleAxis(angle, Vector3.right);
    }
}

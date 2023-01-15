using UnityEngine;

public class Turret : MonoBehaviour
{
    /// <summary>ミルを角度に変換する</summary>
    public const float Mil2Deg = 0.05625f;

    [Tooltip("サイトのTransform")]
    [SerializeField] Transform _sight;
    [Tooltip("")]
    [SerializeField] Vector2 _speed;
    [Tooltip("サイトのTransform")]
    [SerializeField] Transform _turret;
    [Tooltip("サイトのTransform")]
    [SerializeField] Transform _barrel;
    [Tooltip("仰角")]
    [SerializeField, Range(-90f, 90f)] float _elevationAngle = 90f;
    [Tooltip("俯角")]
    [SerializeField, Range(-90f, 90f)] float _depressionAngle = 90f;


    private void Update()
    {
        if (_sight && _turret)
        {
            Rotate(_turret, _sight.forward, _speed.x, Time.deltaTime);
            if (_barrel)
            {
                Pitch(_barrel, _turret.up, _sight.forward, _elevationAngle, _depressionAngle, _speed.y, Time.deltaTime);
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
    void Rotate(Transform transform, in Vector3 forward, float speed, in float deltaTime)
    {
        //砲塔に対する照準器の正面のベクトル
        Vector3 dir = transform.InverseTransformDirection(forward);
        //砲塔と照準器のy軸の角度の差
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        speed *= Mil2Deg;
        //今回の処理で動かすべき角度
        angle = (speed * deltaTime < Mathf.Abs(angle)) ? (speed * deltaTime * Mathf.Sign(angle)) : angle;

        //元のrotationを必要分回転させたQuaternion
        transform.Rotate(Vector3.up, angle);
    }

    /// <summary>
    /// 砲身の動作を計算する
    /// </summary>
    /// <param name="selfTransform">動かすオブジェクトのTransform</param>
    /// <param name="baseAxis">基部の回転軸(上方向)</param>
    /// <param name="forward">サイトの正面ベクトル</param>
    /// <param name="elevation">仰角の制限</param>
    /// <param name="depression">俯角の制限</param>
    /// <param name="speed">旋回速度</param>
    /// <param name="deltaTime">経過時間</param>
    void Pitch(Transform selfTransform, in Vector3 baseAxis, in Vector3 forward, float elevation, float depression, in float speed, in float deltaTime)
    {
        //ベクトルの基部に対する高さ成分
        //(これらはDot関数の戻り値が1を超過したり-1未満だったりすることがあるので丸めてある)
        float sightDot = Mathf.Clamp(Vector3.Dot(baseAxis, forward.normalized), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(baseAxis, selfTransform.forward), -1, 1);

        elevation = 90f - elevation;
        depression += 90f;
        //基部からの角度[度]
        float sightTheta = Mathf.Clamp(Mathf.Acos(sightDot) * Mathf.Rad2Deg, elevation, depression);
        float selfTheta = Mathf.Acos(selfDot) * Mathf.Rad2Deg;

        float up = Mathf.Sign(Vector3.Dot(baseAxis, selfTransform.up));
        float y = (sightTheta - selfTheta) * up;
        float angle = (speed * deltaTime < Mathf.Abs(y)) ? (speed * deltaTime * Mathf.Sign(y)) : y;

        selfTransform.Rotate(Vector3.right, angle);
    }
}

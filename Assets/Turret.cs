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
    /// ������v�Z����
    /// </summary>
    /// <param name="transform">�������I�u�W�F�N�g��Transform</param>
    /// <param name="forward">�T�C�g�̐��ʃx�N�g��</param>
    /// <param name="speed">���񑬓x</param>
    /// <param name="deltaTime">�o�ߎ���</param>
    /// <returns>��]���Quaternion</returns>
    Quaternion Rotate(in Transform transform, in Vector3 forward, in float speed, in float deltaTime)
    {
        Vector3 dir = transform.InverseTransformDirection(forward);
        float y = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y < 0 ? -1 : 1) : y;

        return transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
    }

    /// <summary>
    /// �C�g�̓�����v�Z����
    /// </summary>
    /// <param name="selfTransform">�������I�u�W�F�N�g��Transform</param>
    /// <param name="baseAxis">��̉�]��</param>
    /// <param name="forward">�T�C�g�̐��ʃx�N�g��</param>
    /// <param name="speed">���񑬓x</param>
    /// <param name="deltaTime">�o�ߎ���</param>
    /// <returns>��]���Quaternion</returns>
    Quaternion Pitch(in Transform selfTransform, in Vector3 baseAxis, in Vector3 forward, in float speed, in float deltaTime)
    {
        Vector3 bAxisNomal = baseAxis.normalized;
        //�x�N�g���̊�ɑ΂��鍂������
        //(������Dot�֐��̖߂�l��1�𒴉߂�����-1�����������肷�邱�Ƃ�����̂Ŋۂ߂Ă���)
        float sightDot = Mathf.Clamp(Vector3.Dot(bAxisNomal, forward.normalized), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(bAxisNomal, selfTransform.forward), -1, 1);

        //�����̊p�x[�x]
        float sightTheta = Mathf.Acos(sightDot) * Mathf.Rad2Deg;
        float selfTheta = Mathf.Acos(selfDot) * Mathf.Rad2Deg;

        float y = sightTheta - selfTheta;
        int up = (Vector3.Dot(bAxisNomal, selfTransform.up) < 0 ? -1 : 1);
        float angle = speed * deltaTime < Mathf.Abs(y) ? speed * deltaTime * (y > 0 ? 1 : -1) : y * up;

        return selfTransform.rotation * Quaternion.AngleAxis(angle, Vector3.right);
    }
}

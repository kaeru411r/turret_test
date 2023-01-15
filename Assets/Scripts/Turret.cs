using UnityEngine;

public class Turret : MonoBehaviour
{
    /// <summary>�~�����p�x�ɕϊ�����</summary>
    public const float Mil2Deg = 0.05625f;

    [Tooltip("�T�C�g��Transform")]
    [SerializeField] Transform _sight;
    [Tooltip("")]
    [SerializeField] Vector2 _speed;
    [Tooltip("�T�C�g��Transform")]
    [SerializeField] Transform _turret;
    [Tooltip("�T�C�g��Transform")]
    [SerializeField] Transform _barrel;
    [Tooltip("�p")]
    [SerializeField, Range(-90f, 90f)] float _elevationAngle = 90f;
    [Tooltip("��p")]
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
    /// ������v�Z����
    /// </summary>
    /// <param name="transform">�������I�u�W�F�N�g��Transform</param>
    /// <param name="forward">�T�C�g�̐��ʃx�N�g��</param>
    /// <param name="speed">���񑬓x</param>
    /// <param name="deltaTime">�o�ߎ���</param>
    /// <returns>��]���Quaternion</returns>
    void Rotate(Transform transform, in Vector3 forward, float speed, in float deltaTime)
    {
        //�C���ɑ΂���Ə���̐��ʂ̃x�N�g��
        Vector3 dir = transform.InverseTransformDirection(forward);
        //�C���ƏƏ����y���̊p�x�̍�
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        speed *= Mil2Deg;
        //����̏����œ������ׂ��p�x
        angle = (speed * deltaTime < Mathf.Abs(angle)) ? (speed * deltaTime * Mathf.Sign(angle)) : angle;

        //����rotation��K�v����]������Quaternion
        transform.Rotate(Vector3.up, angle);
    }

    /// <summary>
    /// �C�g�̓�����v�Z����
    /// </summary>
    /// <param name="selfTransform">�������I�u�W�F�N�g��Transform</param>
    /// <param name="baseAxis">��̉�]��(�����)</param>
    /// <param name="forward">�T�C�g�̐��ʃx�N�g��</param>
    /// <param name="elevation">�p�̐���</param>
    /// <param name="depression">��p�̐���</param>
    /// <param name="speed">���񑬓x</param>
    /// <param name="deltaTime">�o�ߎ���</param>
    void Pitch(Transform selfTransform, in Vector3 baseAxis, in Vector3 forward, float elevation, float depression, in float speed, in float deltaTime)
    {
        //�x�N�g���̊�ɑ΂��鍂������
        //(������Dot�֐��̖߂�l��1�𒴉߂�����-1�����������肷�邱�Ƃ�����̂Ŋۂ߂Ă���)
        float sightDot = Mathf.Clamp(Vector3.Dot(baseAxis, forward.normalized), -1, 1);
        float selfDot = Mathf.Clamp(Vector3.Dot(baseAxis, selfTransform.forward), -1, 1);

        elevation = 90f - elevation;
        depression += 90f;
        //�����̊p�x[�x]
        float sightTheta = Mathf.Clamp(Mathf.Acos(sightDot) * Mathf.Rad2Deg, elevation, depression);
        float selfTheta = Mathf.Acos(selfDot) * Mathf.Rad2Deg;

        float up = Mathf.Sign(Vector3.Dot(baseAxis, selfTransform.up));
        float y = (sightTheta - selfTheta) * up;
        float angle = (speed * deltaTime < Mathf.Abs(y)) ? (speed * deltaTime * Mathf.Sign(y)) : y;

        selfTransform.Rotate(Vector3.right, angle);
    }
}

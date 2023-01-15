using UnityEngine;

public class TestSight : MonoBehaviour
{
    [Tooltip("�^�[�Q�b�g��Transform")]
    [SerializeField] Transform _target;
    [Tooltip("��΂�Ray�̐F")]
    [SerializeField] Color _color = Color.red;


    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        if (_target)    // _target�ɃI�u�W�F�N�g�������A_target�܂Ő�������
        {
            transform.LookAt(_target);
            Gizmos.DrawLine(transform.position, _target.position);
        }
        else            // _target�������̂ŁA���ʂ�10����������
        {
            Gizmos.DrawRay(transform.position, transform.forward * 10);
        }
    }
}

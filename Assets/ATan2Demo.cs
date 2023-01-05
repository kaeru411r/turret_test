using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATan2Demo : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Transform _variable;
    [SerializeField] float _length;

    private void OnDrawGizmos()
    {
        if (_variable)
        {
            Vector3 dir = _variable.forward;

            float deg = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            Gizmos.DrawRay(Camera.main.transform.position + Camera.main.transform.forward, Vector3.forward * _length);
            Gizmos.DrawRay(Camera.main.transform.position + Camera.main.transform.forward, dir * _length);

            if (_text)
            {
                _text.text = deg.ToString();
            }
        }
    }
}

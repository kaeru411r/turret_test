using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATan2Demo : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Transform _variable;
    [SerializeField] float _length;
    [SerializeField] float _speed;

    private void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(Rotate());
        }
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            yield return null;
            _variable.Rotate(0, _speed * Time.deltaTime, 0);
        }
    }

    private void OnDrawGizmos()
    {
        if (_variable)
        {
            Vector3 dir = _variable.forward;

            float rad = Mathf.Atan2(dir.x, dir.z);
            float deg = rad* Mathf.Rad2Deg;
            float rad2 = (float)System.Math.Atan2(dir.x, dir.z);
            float deg2 = rad* Mathf.Rad2Deg;

            Gizmos.DrawRay(Camera.main.transform.position + Camera.main.transform.forward, Vector3.forward * _length);
            Gizmos.DrawRay(Camera.main.transform.position + Camera.main.transform.forward, dir * _length);

            if (_text)
            {
                _text.text = $"{rad.ToString("F2")}[rad]\n{deg.ToString("F2")}[deg]\n{rad2.ToString("F2")}[rad]\n{deg2.ToString("F2")}[deg]";
            }
        }
    }
}

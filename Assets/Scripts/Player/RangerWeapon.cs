using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerWeapon : MonoBehaviour
{
    public float offset;

    public Transform shotPoint;

    public float startTimeBtwShots;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rotZ);
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    private void FixedUpdate()
    {
        Flip();
    }

    void Flip()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if ((mousePos.x > transform.position.x && transform.localScale.x < 0) ||
            (mousePos.x < transform.position.x && transform.localScale.x > 0))
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x *= -1;
            scaleTemp.y *= -1;
            transform.localScale = scaleTemp;
        }
    }
}

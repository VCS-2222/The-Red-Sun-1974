using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject[] points;
    public GameObject floatingCamera;

    private void Start()
    {
        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            floatingCamera.transform.position = points[0].transform.position;
            floatingCamera.transform.rotation = points[0].transform.rotation;

            yield return new WaitForSeconds(5f);

            floatingCamera.transform.position = points[1].transform.position;
            floatingCamera.transform.rotation = points[1].transform.rotation;

            yield return new WaitForSeconds(5f);

            floatingCamera.transform.position = points[2].transform.position;
            floatingCamera.transform.rotation = points[2].transform.rotation;

            yield return new WaitForSeconds(5f);

            floatingCamera.transform.position = points[3].transform.position;
            floatingCamera.transform.rotation = points[3].transform.rotation;

            yield return new WaitForSeconds(5f);

            floatingCamera.transform.position = points[4].transform.position;
            floatingCamera.transform.rotation = points[4].transform.rotation;

            yield return new WaitForSeconds(5f);

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        floatingCamera.transform.position += floatingCamera.transform.forward * 0.2f * Time.deltaTime;
    }
}

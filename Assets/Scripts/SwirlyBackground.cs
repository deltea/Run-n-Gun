using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlyBackground : MonoBehaviour
{

    public GameObject circlePrefab;
    public Color color1;
    public Color color2;
    public int circleNum = 36;

    void Start() {
        for (int i = 0; i < circleNum; i++)
        {
            GameObject circle = Instantiate(circlePrefab, new Vector2(circlePrefab.transform.localScale.x / 2, 0), Quaternion.identity, transform);
            circle.transform.RotateAround(Vector2.zero, Vector3.forward, 360 / circleNum * i);

            if (i % 2 == 0)
            {
                circle.GetComponent<SpriteRenderer>().color = color1;
            } else
            {
                circle.GetComponent<SpriteRenderer>().color = color2;
            }
        }
    }    

}

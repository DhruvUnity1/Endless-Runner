using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> easyObstacles;
    [SerializeField] List<GameObject> mediumObstacles;
    [SerializeField] List<GameObject> hardObstacles;
    private void OnEnable()
    {
        float zDiffer = this.transform.position.z - this.gameObject.transform.localScale.z / 2;
        int dificulty = 1;
        Debug.Log(this.gameObject.transform.localScale.z);
        for (int i = 0; i < this.gameObject.transform.localScale.z / 5; i++)
        {
            int probabilityToSpawn = Random.Range(0, 10);
            if (probabilityToSpawn <= 7 / dificulty)
            {
                zDiffer += 5;

            }
            else
            {


                GameObject obstacle = Instantiate(PickAnObjectFfromList(dificulty)[Random.Range(0, PickAnObjectFfromList(dificulty).Count)], new Vector3(0, 1, zDiffer), Quaternion.identity, parent);
                zDiffer += 5;
            }

        }
    }

    List<GameObject> PickAnObjectFfromList(int i)
    {
        if (i == 1)
        {
            return easyObstacles;
        }
        else if (i == 2)
        {
            return mediumObstacles;
        }
        else
        {
            return hardObstacles;
        }

    }
    private void OnDisable()
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

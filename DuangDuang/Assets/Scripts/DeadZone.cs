using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "TeamA")
        {
            ScorePanel.scoreValue--;
            StartCoroutine(BackToGame(col.gameObject));
        }
        else if (col.gameObject.tag == "TeamB")
        {
            ScorePanel.scoreValue++;
            StartCoroutine(BackToGame(col.gameObject));
        }

    }

    public IEnumerator BackToGame(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.transform.position = new Vector3(0, 0, 0);
    }
}

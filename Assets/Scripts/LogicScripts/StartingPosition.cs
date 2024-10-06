using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
    }

    private void Awake()
    {
        if ((PlayerPrefs.GetInt("saveExists") == 1 ? true : false) && !SavesHandling.Instance.saveLoaded)
        {
            GameManager.Instance.checkpointposition = new Vector3Double(PlayerPrefs.GetFloat("spawnPositionX"), PlayerPrefs.GetFloat("spawnPositionY"), PlayerPrefs.GetFloat("spawnPositionZ"));
            PlayerManager.Instance.player.transform.position = GameManager.Instance.checkpointposition.ToVector3();
            SavesHandling.Instance.saveLoaded = true;
        }
        else
        {
            GameManager.Instance.setCheckPoint(this.gameObject.transform);
            GameManager.Instance.backToCheckPoint();
            SavesHandling.Instance.saveLoaded = true;
        }
        SavesHandling.Instance.Save();
    }
}

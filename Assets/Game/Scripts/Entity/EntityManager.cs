using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{

    private uint playerKncokedDown = 0;
    public uint PlayerKncokedDown
    {
        get { return playerKncokedDown; }
        set
        {
            playerKncokedDown = value;
            GameState.Instance.IsGameOver(playerKncokedDown);
        }
    }

    private void Start()
    {
    }

    public void SpawnEntity(GameObject _entity)
    {
        Instantiate(_entity);
    }
}

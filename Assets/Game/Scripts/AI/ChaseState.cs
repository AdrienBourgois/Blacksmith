using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Game.Scripts.Entity;
using Game.Scripts.Navigation;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class ChaseState : IEnemyState
    {
        private EnemyBehavior myBehavior;
        public BaseEntity target;

        private List<Vector2> path;
        private Vector3 destination;

        private int pathId;
        private int updatePathTimerId;

        private float speed
        {
            get { return myBehavior.myEntity.speed; }
        }
        private Vector3 position
        {
            get { return myBehavior.myEntity.transform.position; }
        }

        public ChaseState(EnemyBehavior _behavior)
        {
            updatePathTimerId = -1;
            myBehavior = _behavior;
        }

        public void ToIdleState()
        {
        }

        public void ToSelectTargetState()
        {

        }

        public void ToChaseState()
        {

        }

        public void ToAttackState()
        {
        }

        public void Update()
        {
            if (updatePathTimerId == -1)
            {
                Debug.Log("Start Timer");
                updatePathTimerId = TimerManager.Instance.AddTimer("Update Path", 2f, true, true, UpdatePath, UpdatePath, null, null, null);
            }

            //Move();
        }

        private void Move()
        {
            myBehavior.myEntity.TryMove(destination.normalized);//(Vector3.MoveTowards(position, destination, speed * Time.deltaTime));
            /*if (IsArrived())
            {
                Debug.Log("IsArrived");
                //UpdateDestination();
            }*/
        }

        private bool IsArrived()
        {
            return Vector3.Distance(position, destination) < 0.2f;
        }

        private void UpdateDestination()
        {
            if (path == null)
                return;

            Debug.Log("UpdateDestination");

            ++pathId;

            if (pathId < path.Count && pathId >= 0)
                destination = new Vector3(path[pathId].x, 0, path[pathId].y).ToGameSpace();
        }

        private void UpdatePath()
        {
            Debug.Log("UpdatePath");

            Collider2D floor = Physics2D.OverlapPoint(position, LayerMask.GetMask("Floor"));
            if (floor != null)
            {
                if (path != null && path.Count != 0)
                    path.Clear();

                floor.GetComponent<PathFinding>().WeightedFindPath(position, target.transform.position, out path);
                if (path != null)
                {
                    pathId = -1;
                    UpdateDestination();
                }
                else
                    Debug.Log("Path is null");
            }
            else
                Debug.Log("Floor not found");
        }
    }
}

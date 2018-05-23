using System.Collections.Generic;
using Game.Scripts.Entity;
using Game.Scripts.Navigation;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class ChaseState : IEnemyState
    {
        private EnemyBehavior myBehavior;

        private List<Vector2> path;
        private Vector3 destination;

        private int pathId;
        private int updatePathTimerId;

        private float speed
        {
            get { return myBehavior.MyEntity.speed; }
        }
        private Vector3 position
        {
            get { return myBehavior.MyEntity.transform.position; }
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
            // this
        }

        public void ToAttackState()
        {
            if (myBehavior.IsInRange())
                myBehavior.ToAttackState();
        }

        public void Update()
        {
            if (updatePathTimerId == -1)
                updatePathTimerId = TimerManager.Instance.AddTimer("Update Path", 2f, true, true, UpdatePath, UpdatePath, null, null, null);

            Move();
            ToAttackState();
        }

        private void Move()
        {
            myBehavior.MyEntity.TryMove(destination.normalized);//(Vector3.MoveTowards(position, destination, speed * Time.deltaTime));
            if (IsArrived())
            {
                Debug.Log("IsArrived");
                UpdateDestination();
            }
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

            Collider2D floor = Physics2D.OverlapPoint(position, LayerMask.GetMask("Floor"));
            if (floor != null)
            {
                if (path != null && path.Count != 0)
                    path.Clear();

                //Debug.Log("from : " + position);
                //Debug.Log("to : " + target.transform.position);

                if (floor.GetComponent<PathFinding>().WeightedFindPath(position, myBehavior.Target.transform.position, out path))
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

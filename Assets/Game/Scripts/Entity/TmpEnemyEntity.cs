namespace Game.Scripts.Entity
{
    public class TmpEnemyEntity : BaseEntity {

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            LightGroundedAttack();
        }

        public override void Die()
        {
            base.Die();

            EntityManager.Instance.EnemyNum -= 1;
            gameObject.SetActive(false);
            // notify entity manager
        }
    }
}

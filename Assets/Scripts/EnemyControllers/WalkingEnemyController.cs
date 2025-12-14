
using UnityEngine;

namespace Assets.Scripts.EnemyControllers
{
    public class WalkingEnemyController: PatrolEnemyController
    {
        [Header("Physics")]
        public PhysicsMaterial2D WalkMaterial;
        public PhysicsMaterial2D StandMaterial;


        public override void OnCooldown()
        {
            base.OnCooldown();
            Col.sharedMaterial = StandMaterial;
        }

        public override void OnCooldownFinish() 
        {
            Col.sharedMaterial = WalkMaterial;
        }

    }
}

﻿using ai.behaviours;

namespace Topic_of_Love.Mian.CustomAssets.AI.CustomBehaviors.ivf;
public class BehTrySexualIvf : BehaviourActionActor
{
    public override BehResult execute(Actor pActor)
    {
        Util.Debug("Trying sexual ivf for "+pActor.getName());

        if (!pActor.hasHouse() || pActor.hasStatus("pregnant"))
            return BehResult.Stop;
        var home = pActor.getHomeBuilding();
            
        if(pActor.distanceToObjectTarget(home) >= 75f)
            return BehResult.Stop;
            
        if (pActor.hasLover() && Util.CanReproduce(pActor, pActor.lover) 
                              && pActor.isSameIslandAs(pActor.lover)
                              && pActor.lover.distanceToObjectTarget(home) < 75f && !pActor.lover.hasStatus("pregnant"))
            pActor.beh_actor_target = pActor.lover;
        else if(pActor.hasBestFriend() && Util.CanReproduce(pActor, pActor.getBestFriend()) 
                                       && pActor.isSameIslandAs(pActor.getBestFriend())
                                       && pActor.getBestFriend().distanceToObjectTarget(home) < 75f && !pActor.getBestFriend().hasStatus("pregnant"))
            pActor.beh_actor_target = pActor.getBestFriend();
            
        if (pActor.beh_actor_target == null)
            return BehResult.Stop;
        var target = pActor.beh_actor_target.a;
        pActor.beh_building_target = home;
        target.beh_actor_target = pActor;
        target.beh_building_target = home;
            
        Util.Debug("Starting sexual ivf tasks for "+pActor.getName()+" and "+target.getName());

        pActor.beh_actor_target.a.setTask("go_and_wait_sexual_ivf", pCleanJob: true, pClean:false, pForceAction:true);
        pActor.beh_actor_target.a.timer_action = 0.0f;
        return forceTask(pActor, "go_sexual_ivf", pClean: false);
    }
}

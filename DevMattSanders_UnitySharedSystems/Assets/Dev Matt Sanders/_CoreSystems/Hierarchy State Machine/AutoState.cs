using System.Collections.Generic;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    [CreateAssetMenu(menuName = "MattSanders/HSM/Auto State")]

    public class AutoState : GlobalScriptable
    {
        //  [InfoBox("WARNING: Make sure condition states are not in the same HSM as target or default states")]
        //  public HSM hsm;

        public State defaultState;

        public List<AutoStateCase> cases = new List<AutoStateCase>();

        public override void SoStart()
        {
            base.SoStart();

            foreach(AutoStateCase autoStateCase in cases)
            {
                autoStateCase.gameCondition.AddListener(GameConditionChagned);
            }

            CheckStates();
        }

        public override void SoEnd()
        {
            base.SoEnd();

            foreach (AutoStateCase autoStateCase in cases)
            {
                autoStateCase.gameCondition.RemoveListener(GameConditionChagned);
            }
        }

        private void GameConditionChagned(bool val)
        {
            CheckStates();
        }

        private void CheckStates()
        {
            //bool foundState = false;
            State nextState = defaultState;

            foreach(AutoStateCase next in cases)
            {
                if (next.gameCondition.conditionMet)
                {
                    nextState = next.targetState;
                }
            }

            nextState.EnterState();
        }

    }

    [System.Serializable]
    public class AutoStateCase
    {
        public GameCondition gameCondition;

        public State targetState;
    }
}
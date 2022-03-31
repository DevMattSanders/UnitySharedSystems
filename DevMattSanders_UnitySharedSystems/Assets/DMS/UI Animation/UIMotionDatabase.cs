using DMS.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.UI_Animation
{
    [CreateAssetMenu(menuName = "MattSanders/UIMotion/UIMotionDatabase")]
    public class UIMotionDatabase : GlobalScriptable
    {
        public static UIMotionDatabase instance;

#if  UNITY_EDITOR


        public override void EditorUpdate()
        {
            base.EditorUpdate();
            instance = this;
            // Debug.Log("Instance set");
            MotionHeartbeat();
        }
#endif

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            instance = this;
        }
#if(UNITY_EDITOR)
        [ReadOnly]
        [SerializeField] private bool heartbeatVisual;
        public void MotionHeartbeat()
        {
            if (heartbeatVisual) { heartbeatVisual = false; } else { heartbeatVisual = true; }

            for (int i = 0; i < UIMotion.uiMotions.Count; i++)
            {
                if (UIMotion.uiMotions[i] == null)
                {
                    UIMotion.uiMotions.RemoveAt(i);
                    i--;
                }
                else
                {
                    UIMotion.uiMotions[i].UIMotionHeartbeatUpdate();
                }
            }
        }
#endif
        [ReadOnly]
        public int motionCount = 0;

        public void Add()
        {
            motionCount++;
            Debug.Log("Motions: " + motionCount);
        }

        public void Remove()
        {
            motionCount--;
            if (motionCount < 0)
            {
                Debug.Log("Too many motions being removed!");
                motionCount = 0;
            }
            Debug.Log("Motions: " + motionCount);
        }

        public static UnityEngine.Events.UnityEvent resetMotions = new UnityEngine.Events.UnityEvent();

        [ShowIf("ShowResetMotions")]
        [Button]
        public void ResetMotions()
        {
            resetMotions.Invoke();
        }

        [Button]
        public void SetToZero()
        {
            motionCount = 0;
        }

        private bool ShowResetMotions()
        {
            if (motionCount > 0)
            {
                return true;
            }
            return false;
        }

        // [InlineButton("ClearActiveUIMotions")]
        //  public List<UIMotion> activeUIMotions = new List<UIMotion>();

        // public void AddUIMotion(UIMotion m)
        //  {
        //     if (!activeUIMotions.Contains(m)) activeUIMotions.Add(m);
        // }

        // public void RemoveUIMotion(UIMotion m)
        //  {
        //     if (activeUIMotions.Contains(m)) activeUIMotions.Remove(m);
        // }

        //  private void ClearActiveUIMotions()
        //  {
        //     activeUIMotions.Clear();
        // }

 
    }
}

using System.Collections.Generic;
using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace DevMattSanders._CoreSystems
{
    [CreateAssetMenu(menuName = "MattSanders/HSM/HSM")]
    public class HSM : GlobalScriptable
    {
#if UNITY_EDITOR
    [InlineButton("LoadHSMScene",label:"Toggle")]
    public Object hsmScene;

    private void LoadHSMScene()
    {
      //  Scene[] scenes = SceneManager.GetAllScenes();
        if (SceneManager.GetSceneByName(hsmScene.name).isLoaded == false)
        {
            string path = AssetDatabase.GetAssetPath(hsmScene);
            Scene toLoad = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
        }
        else
        {
            EditorSceneManager.SaveScene(SceneManager.GetSceneByName(hsmScene.name));// SaveModifiedScenesIfUserWantsTo();
            EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(hsmScene.name), true);
        }
      //  if(SceneManager.GetAllScenes())
    }

#endif

        public static UnityEvent CallSetNoState = new UnityEvent();

        // public UnityEvent HSMMonoUpdateSignal = new UnityEvent();

        public State firstState;
        [ReadOnly]  public State currentState;

        // [Header("Top")]
        private List<State> currentStates = new List<State>();
        private List<State> newCurrentStates = new List<State>();
        private List<State> newEnterStates = new List<State>();

        // [Header("Bottom")]
        private List<State> queueStates = new List<State>();
        private List<State> statesThisFrame = new List<State>();
        private List<State> statesToExit = new List<State>();

        [Header("States")]
        [InlineEditor]
        public List<State> states = new List<State>();


        public override void OnEnable()
        {
            base.OnEnable();
            //   Debug.Log("Enable HSM");
            CallSetNoState.AddListener(SetNoState);
        }

        private void OnDisable()
        {
            //  Debug.Log("Disable HSM");
            CallSetNoState.RemoveListener(SetNoState);
        }

        public override void SoStart()
        {
            base.SoStart();

            SetNoState();

            queueStates.Clear();
            statesThisFrame.Clear();
            statesToExit.Clear();

            if (firstState != null)
            {
                firstState.hsm = this;
                firstState.EnterState();
            }
        }

        public override void SoEnd()
        {
            base.SoEnd();
            SetNoState();
        }

        public void ReadFromHSMMono(HSMMono hsmMono)
        {
            states.Clear();

            foreach(StateMono next in hsmMono.stateMonos)
            {
                if (next.state == null) continue;

                if (states.Contains(next.state))
                {
                    Debug.Log("Duplicated state reference! " + next.state.name + " " + next.name);
                    continue;
                }

                State state = next.state;
                states.Add(state);
                state.hsm = this;

                if (next.parentState != null)
                {
                    if(next.parentState.state == null)
                    {                    
                        Debug.Log("Parent State Null! " + next.parentState.name);
                    }
                    else
                    {
                    
                        state.parentState = next.parentState.state;
                    }
                }
                else
                {
                    state.parentState = null;
                }
                                  
                //foreach(StateMono nextMono in next)
            }
        }    

        [Button]
        public void SetNoState()
        {
            ChangeState(null);

            currentStates.Clear();
            newCurrentStates.Clear();
            newEnterStates.Clear();

            queueStates.Clear();
            statesThisFrame.Clear();
            statesToExit.Clear();
     
            foreach (State next in states)
            {
                if (next.stateActive)
                {
                    SetStateAsNotActive(next);
                }
            }
        }

        public void ChangeState(State state)
        {
            if (state == null)
            {
                EnterState(null);
                return;
            }

            if (!statesThisFrame.Contains(state))
            {
       
                statesThisFrame.Add(state);

                if (queueStates.Count == 0)
                {
                    queueStates.Add(state);

                    while (queueStates.Count > 0)
                    {
                        EnterState(queueStates[0]);
                        queueStates.RemoveAt(0);
                    }
                }
                else
                {
                    queueStates.Add(state);
                }
            }

            if (queueStates.Count == 0)
            {
                if (statesThisFrame.Count > 0)
                {
                    statesThisFrame.Clear();
                }
            }
        }

        private void EnterState(State state)
        {
            bool newStateNull = false;
            if (state == null) newStateNull = true;

            if (states.Contains(state) || newStateNull)
            {
                State nextState = null;

                if(newStateNull == false)
                {
                    nextState = state;
                }

                // if(state != null)
                // {

                //  }
                if (currentState != nextState)
                {               
                    newCurrentStates.Clear();
                    if (!newStateNull)
                    {
                        newCurrentStates.Add(nextState);
                        State loopState = nextState;
                        while (true)
                        {
                            if (loopState.parentState != null)
                            {
                                newCurrentStates.Add(loopState.parentState);
                                loopState = loopState.parentState;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    newEnterStates.Clear();

                    foreach (State next in newCurrentStates)
                    {
                        if (!currentStates.Contains(next))
                        {
                            newEnterStates.Add(next);
                        }
                    }

                    statesToExit.Clear();

                    foreach (State next in currentStates)
                    {
                        if (!newCurrentStates.Contains(next))
                        {
                            statesToExit.Add(next);
                        }
                    }
                    //  Debug.Log("Here 1");
                    for (int i = newEnterStates.Count - 1; i >= 0; i--)
                    {
                        SetStateAsActive(newEnterStates[i]);
                    }
                    //   Debug.Log("Here 2");
                    currentStates.Clear();
                    currentStates.AddRange(newCurrentStates);

                    currentState = nextState;

                    foreach(State next in statesToExit)
                    {
                        SetStateAsNotActive(next);
                    }
                }

                //  HSMMonoUpdateSignal.Invoke();
                HSMMono.AllMonoReadFromHSM.Invoke();
            }
            else
            {
                Debug.Log("State does not exists! (" + state.name + ")");
            }
        }

        private void SetStateAsActive(State state)
        {
            if (state == currentState)
            {
                state.stateActiveAsPrimary = true;
                state.stateActiveAsSecondary = false;
            }
            else
            {
                state.stateActiveAsPrimary = false;
                state.stateActiveAsSecondary = true;
            }

            if (state.stateActive == false) state.stateActive = true;
        }

        private void SetStateAsNotActive(State state)
        {
            if (state.stateActiveAsPrimary) state.stateActiveAsPrimary = false;
            if (state.stateActiveAsSecondary) state.stateActiveAsSecondary = false;

            if (state.stateActive) state.stateActive = false;

      
        }
    }
}
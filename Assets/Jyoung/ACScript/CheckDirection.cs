/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2021
 *	
 *	"ActionCheckTemplate.cs"
 * 
 *	This is a blank action template, which has two outputs.
 * 
 */

using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

    [System.Serializable]
    public class CheckDirection : Action
    {

        // Declare properties here
        public override ActionCategory Category { get { return ActionCategory.Custom; } }
        public override string Title { get { return "Direction Check"; } }
        public override string Description { get { return "Check if the Player is grounded."; } }

        public string variableName;
        public override float Run()
        {
            var directionInt = KickStarter.player.GetMoveDirection().magnitude;

            Debug.Log(directionInt);
            //GlobalVariables.GetVariable(variableName).IntegerValue = directionInt;
            return base.Run();
        }


#if UNITY_EDITOR

		public override void ShowGUI ()
		{
			// Action-specific Inspector GUI code here.  The "Condition is met" / "Condition is not met" GUI is rendered automatically afterwards.
		}
		

		public override string SetLabel ()
		{
			// (Optional) Return a string used to describe the specific action's job.
			
			return string.Empty;
		}

#endif

    }

}
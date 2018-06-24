using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

namespace Assets.Scripts{
    public static class AvailableMatchList
//namespace Assets.Scripts
    {
        public static event Action<List<MatchInfoSnapshot>> OnAvailableMatchesChanged = delegate { };
       

        private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();

        public static void HandleNewMatchList(List<MatchInfoSnapshot> matchList)
        {
            matches = matchList;
            OnAvailableMatchesChanged(matches);
        }
    

    }
}

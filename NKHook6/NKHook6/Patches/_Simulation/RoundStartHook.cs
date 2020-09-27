﻿using Assets.Scripts.Simulation;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Simulation;

namespace NKHook6.Patches.Simulate
{
    [HarmonyPatch(typeof(Simulation), "OnRoundStart")]
    class RoundStartHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Simulation __instance, ref int roundArrayIndex)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new RoundStartEvent.Pre(ref __instance, ref roundArrayIndex);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                roundArrayIndex = o.roundArrayIndex;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }

        [HarmonyPostfix]
        internal static void Postfix(ref Simulation __instance, ref int roundArrayIndex)
        {
            if (sendPostfixEvent)
            {
                var o = new RoundStartEvent.Post(ref __instance, ref roundArrayIndex);
                EventRegistry.subscriber.dispatchEvent(ref o);
                __instance = o.instance;
                roundArrayIndex = o.roundArrayIndex;
            }

            sendPostfixEvent = !sendPostfixEvent;
        }
    }
}
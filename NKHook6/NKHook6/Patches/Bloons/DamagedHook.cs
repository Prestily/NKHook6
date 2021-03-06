﻿using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Harmony;
using NKHook6.Api.Events;
using NKHook6.Api.Events._Bloons;
using System;
using UnhollowerBaseLib;

namespace NKHook6.Patches._Bloons
{
    [HarmonyPatch(typeof(Bloon), "Damage", new Type[] { typeof(float), typeof(Il2CppStringArray), typeof(Projectile), typeof(bool),
        typeof(bool), typeof(bool), typeof(Tower), typeof(bool), typeof(Il2CppStringArray), typeof(bool), typeof(bool) })]
    class DamagedHook
    {
        private static bool sendPrefixEvent = true;
        private static bool sendPostfixEvent = true;

        [HarmonyPrefix]
        internal static bool Prefix(ref Bloon __instance, ref float totalAmount, ref Il2CppStringArray types, ref Projectile projectile,
        ref bool distributeToChildren, ref bool overrideDistributeBlocker, ref bool createEffect, ref Tower tower,
        ref bool canDestroyProjectile, ref Il2CppStringArray ignoreImmunityForBloonTypes, ref bool ignoreNonTargetable,
        ref bool blockSpawnChildren)
        {
            bool allowOriginalMethod = true;
            if (sendPrefixEvent)
            {
                var o = new BloonEvents.DamagedEvent(ref __instance, ref totalAmount, ref types, ref projectile,
                    ref distributeToChildren, ref overrideDistributeBlocker, ref createEffect, ref tower,
                    ref canDestroyProjectile, ref ignoreImmunityForBloonTypes, ref ignoreNonTargetable,
                    ref blockSpawnChildren);
                EventRegistry.instance.dispatchEvent(ref o);

                totalAmount = o.damageTaken;
                types = o.damageTypes;
                projectile = o.projectile;
                distributeToChildren = o.distrubuteToChildren;
                overrideDistributeBlocker = o.overrideDistributeBlocker;
                createEffect = o.createEffect;
                tower = o.tower;
                canDestroyProjectile = o.canDestroyProjectile;
                ignoreImmunityForBloonTypes = o.ignoreImmunityForBloonTypes;
                ignoreNonTargetable = o.ignoreNonTargetables;
                blockSpawnChildren = o.blockSpawnChildren;
                allowOriginalMethod = !o.isCancelled();
            }

            sendPrefixEvent = !sendPrefixEvent;

            return allowOriginalMethod;
        }
    }
}

﻿using Assets.Scripts.Models;
using Assets.Scripts.Models.Rounds;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Utils;
using Il2CppSystem.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using static Assets.Scripts.Simulation.Simulation;

namespace NKHook6.Api.Extensions
{
    public static class InGameExt
    {
        public static double getCash(this InGame inGame)
        {
            try
            {
                CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
                return cashManager.cash.Value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
                return 0;
            }
        }
        public static void addCash(this InGame inGame, double newCash)
        {
            try
            {
                CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
                cashManager.cash.Value += newCash;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        public static void setCash(this InGame inGame, double newCash)
        {
            try
            {
                CashManager cashManager = inGame.bridge.simulation.cashManagers.entries[0].value;
                cashManager.cash.Value = newCash;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        public static double getHealth(this InGame inGame)
        {
            try
            {
                return inGame.bridge.simulation.health.Value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
                return 0;
            }
        }
        public static void addHealth(this InGame inGame, double newHealth)
        {
            try
            {
                inGame.bridge.simulation.health.Value += newHealth;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        public static void setHealth(this InGame inGame, double newHealth)
        {
            try
            {
                inGame.bridge.simulation.health.Value = newHealth;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        public static List<BloonToSimulation> getBloons(this InGame inGame)
        {
            return inGame.bridge.GetAllBloons();
        }
        public static void spawnBloons(this InGame inGame, Il2CppReferenceArray<BloonEmissionModel> bloonEmissionModels)
        {
            int round = InGame.instance.bridge.GetCurrentRound();

            inGame.bridge.SpawnBloons(bloonEmissionModels, round, 0);
        }
        public static void spawnBloons(this InGame inGame, string bloonName, float spacing, int number)
        {
            Il2CppReferenceArray<BloonEmissionModel> bloonEmissionModels = new Il2CppReferenceArray<BloonEmissionModel>(number);
            float time = 0;
            for (int i = 0; i < number; i++)
            {
                time += spacing;
                bloonEmissionModels[i] = (new BloonEmissionModel(bloonName, time, bloonName));
            }
            inGame.spawnBloons(bloonEmissionModels);


        }
        public static void spawnBloons(this InGame inGame, int round)
        {
            GameModel model = Game.instance.model;
            if (round < 100)
            {

                var rounds = model.GetRoundSet().rounds;
                var emissions = rounds[round - 1].emissions;
                inGame.spawnBloons(emissions);
            }
            if (round > 100)
            {
                var emissions = model.freeplayGroups[round - 100].bloonEmissions;
                inGame.spawnBloons(emissions);
            }
        }
        public static List<TowerToSimulation> getTowers(this InGame inGame)
        {
            return inGame.bridge.GetAllTowers();
        }
        public static List<AbilityToSimulation> getAbilities(this InGame inGame)
        {
            return inGame.bridge.GetAllAbilities(true);
        }
        public static List<Projectile> getProjectiles(this InGame inGame)
        {
            return inGame.bridge.GetAllProjectiles();
        }
    }
}

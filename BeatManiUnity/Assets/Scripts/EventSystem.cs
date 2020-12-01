using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a static method for every event in the game. Method returns the event name as a string.
/// </summary>
public class EventSystem : MonoBehaviour {

    #region example method


    ///// <summary>
    ///// [Description]
    ///// <para>int: </para>
    ///// <para>string: </para>
    ///// </summary>
    //public static string EventName()
    //{
    //    return "EventName";
    //}


    #endregion


    #region Building Events


    /// <summary>
    /// Villager Starts Work At Building
    /// <para>int: building entityID</para>
    /// <para>string[]: villiger entityID</para> 
    /// </summary>
    public static string StartProgressingBuilding()
    {
        return "StartProgressingBuilding";
    }

    /// <summary>
    /// Villager Stops Work At Building
    /// <para>int: building entityID</para>
    /// <para>string[]: villiger entityID</para> 
    /// </summary>
    public static string EndProgressingBuilding()
    {
        return "EndProgressingBuilding";
    }


    #endregion


    #region Villager Events
    /// <summary>
    /// Deal Damage to villager (or Building!)
    /// <para>int: receiving entityID</para>
    /// <para>string[]: damage, entityID of damage dealer, bool friend?</para> 
    /// </summary>
    public static string TakeDamage()
    {
        return "TakeDamage";
    }

    /// <summary>
    /// NPC took damage
    /// <para>int: entityID of enemy that took damage</para>
    /// <para>string[]: damage, entityID of damage dealer (if existant)</para>
    /// </summary>
    /// <returns></returns>
    public static string DamageTaken()
    {
        return "DamageTaken";
    }

    public static string Die()
    {
        return "Die";
    }

    /// <summary>
    /// Triggered when NPC gets killed
    /// <para>int: entityID of entity that died</para>
    /// <para>string[]: entityID of killer (if existant)</para>
    /// </summary>
    /// <returns></returns>
    public static string Killed()
    {
        return "Killed";
    }


    public static string TriggerAction()
    {
        return "TriggerAction";
    }


    /// <summary>
    /// NPC attacked enemy
    /// <para>int: entityID of attacker</para>
    /// <para>string[]: entityID of defender, optional: add anything to make the npc use only fists</para>
    /// </summary>
    /// <returns></returns>
    public static string AttackEnemy()
    {
        return "AttackEnemy";
    }

    /// <summary>
    /// NPC tries to end fight
    /// <para>int: entityID of attacked npc</para>
    /// <para>string[]: entityID of attacker</para>
    /// </summary>
    /// <returns></returns>
    public static string TryEndFight()
    {
        return "TryEndFight";
    }


    /// <summary>
    /// NPC want to move inside a house
    /// <para>int: entityID of requesting npc</para>
    /// <para>string[]: entityID of house</para>
    /// </summary>
    /// <returns></returns>
    public static string EnterBuilding()
    {
        return "EnterBuilding";
    }

    /// <summary>
    /// NPC want to move outside a house, he is currently in
    /// <para>int: entityID of requesting npc</para>
    /// <para>string[]: entityID of house</para>
    /// </summary>
    /// <returns></returns>
    public static string LeaveBuilding()
    {
        return "LeaveBuilding";
    }

    /// <summary>
    /// A Notification for villagers inside a building, which
    /// is nearly destroyed
    /// <para>int: entityID of the building</para>
    /// <para>string[]: entityIDs of villagers inside the building</para>
    /// </summary>
    /// <returns></returns>
    public static string BuildingDestructionNotification()
    {
        return "BuildingDestructionNotification";
    }

    /// <summary>
    /// Heals NPC based on the abilities of the healer
    /// <para>int: id of npc that should be healed</para>
    /// <para>string[] id of the healer, (only if triggered by HealBehaviourNode) the compositeKeepHealingKey of the HealBehaviourNode</para>
    /// </summary>
    /// <returns></returns>
    public static string HealNPC()
    {
        return "HealNPC";
    }

    /// <summary>
    /// Called when a NPC prays to their god
    /// <para>int: id of npc that prays</para>
    /// <para>string[] empty</para>
    /// </summary>
    /// <returns></returns>
    public static string Pray()
    {
        return "Pray";
    }

    /// <summary>
    /// Called when a NPC interacts with other npcs
    /// <para>int: id of npc that starts interaction</para>
    /// <para>string[] {key where npcs are stored, behaviourType,  speech topic}</para>
    /// </summary>
    /// <returns></returns>
    public static string InteractWithNPCs()
    {
        return "InteractWithNPCs";
    }

    /// <summary>
    /// Called when a NPC interacts with other npcs
    /// <para>int: id of npc that starts fist fight</para>
    /// <para>string[] </para>
    /// </summary>
    /// <returns></returns>
    public static string FistFight()
    {
        return "FistFight";
    }

    /// <summary>
    /// Called when a NPC interacts with other npcs and convinces them to start a war
    /// <para>int: id of npc that tries to start a war</para>
    /// <para>string[] </para>
    /// </summary>
    /// <returns></returns>
    public static string StartWar()
    {
        return "StartWar";
    }

    /// <summary>
    /// Gets called after successfully crafting an item
    /// <para>int: id of npc that crafted the item</para>
    /// <para>string[] name of the item</para>
    /// </summary>
    /// <returns></returns>
    public static string ItemCrafted()
    {
        return "ItemCrafted";
    }

    /// <summary>
    /// NPC was healed
    /// <para>int: id of npc that was healed</para>
    /// <para>string[] id of the healer, the amount of health that was restored</para>
    /// </summary>
    /// <returns></returns>
    public static string NPCHealed()
    {
        return "NPCHealed";
    }

    /// <summary>
    /// Entity was fully healed
    /// <para>int: id of entity that was healed</para>
    /// <para>string[] id of the healer, (only if triggered by HealBehaviourNode) the compositeKeepHealingKey of the HealBehaviourNode</para>
    /// </summary>
    /// <returns></returns>
    public static string FullyHealed()
    {
        return "FullyHealed";
    }

    /// <summary>
    /// Entity is religiously affected
    /// <para>int: id of entity that is affected</para>
    /// <para>string[] id of the affector, affection value, religion?</para>
    /// </summary>
    /// <returns></returns>
    public static string AffectedReligiously()
    {
        return "AffectedReligiously";
    }


    #endregion

    #region Resource Events

    /// <summary>
    /// increase resources and shrink ressource pile
    /// <para>int: entityID of villager</para>
    /// <para>string[]: [0] pileID ; [1] value </para> 
    /// </summary>
    public static string GatheredResource()
    {
        return "GatheredResource";
    }

    /// <summary>
    /// vanish pile and order new one
    /// <para>int: pileID</para>
    /// <para>string[]: null</para> 
    /// </summary>
    public static string ResourceExhausted()
    {
        return "ResourceExhausted";
    }

    /// <summary>
    /// Call if ressource amount should be reduced
    /// <para>int: id of entity that reduces ressource amount(important to find right ressourcePool)</para>
    /// <para>string[]: amount of ressources taken, ressource type (enum InGameRessources), optional: exactAmount needed (true/false). if true, returns 0 if there is less than amount of resource present</para>
    /// </summary>
    /// <returns></returns>
    public static string RemoveResource()
    {
        return "RemoveRessource";
    }

    /// <summary>
    /// Call if ressource amount should be added
    /// <para>int: id of entity that added ressource amount (important to find right ressourcePool)</para>
    /// <para>string[]: amount of ressources added, ressource type (enum InGameRessources)</para>
    /// </summary>
    /// <returns></returns>
    public static string AddResource()
    {
        return "AddRessource";
    }

    #endregion

    public static string Sign()
    {
        return "Sign";
    }




}

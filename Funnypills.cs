using System.Collections.Generic;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using CustomItems1.SCP094IT;
using UnityEngine;
using Exiled.Events.EventArgs.Player;

namespace FunnyPills;
    public class Class1 : Plugin<CustomItem2Config>
{
    public static Class1 Instance;

    public override void OnEnabled()
    {
        Instance = this;

        Exiled.CustomItems.API.Features.CustomItem.RegisterItems();

        Exiled.CustomRoles.API.Features.CustomRole.RegisterRoles(false, null);

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Instance = null;

        Exiled.CustomItems.API.Features.CustomItem.UnregisterItems();

        Exiled.CustomRoles.API.Features.CustomRole.UnregisterRoles();

        base.OnDisabled();
    }
}

[CustomItem(ItemType.SCP500)]
public class FunnyPills : CustomItem
{
    public override ItemType Type { get; set; } = ItemType.SCP500;
    public override uint Id { get; set; } = 52;

    public override string Name { get; set; } = "Pillole divertenti!";

    public override string Description { get; set; } = "Sono delle pillole che ti danno effetti random!";
    public override float Weight { get; set; } = 1f;

    public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
    {
        DynamicSpawnPoints = new List<DynamicSpawnPoint>()
            {
                new DynamicSpawnPoint()
                {
                        Location = Exiled.API.Enums.SpawnLocationType.Inside096,
                        Chance = 100,
                }
            }
    };

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
        Exiled.Events.Handlers.Player.Died += OnDied;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
        Exiled.Events.Handlers.Player.Died -= OnDied;
        base.UnsubscribeEvents();
    }
    private void OnUsingItem(Exiled.Events.EventArgs.Player.UsingItemEventArgs ev)
    {
        if (ev.Player.CurrentItem == null || ev.Player.CurrentItem.Type != ItemType.SCP500)
            return;

        int chance = Random.Range(0, 101);

        switch (chance)
        {
            case int n when (n < 20):
                ev.Player.Scale = new Vector3(2.0f, 2.0f, 2.0f);
                break;

            case int n when (n < 40):
                ev.Player.Broadcast(5, "Hai ricevuto un'A7!!");
                ev.Player.AddItem(ItemType.GunA7);

                if (ev.Player.Inventory.UserInventory.Items.Count <= 8)
                {
                    ev.Player.Broadcast(5, "Non hai ricevuto l'item, hai l'inventario full!");
                    return;
                }

                break;
            case int n when (n < 60):
                ev.Player.EnableEffect(EffectType.MovementBoost, 10f);
                break;

            case int n when (n < 80):
                ev.Player.Broadcast(5, "Hai ricevuto + 50Hp");
                ev.Player.AddAhp(50f);
                break;

            case int n when (n < 100):

                if (ev.Player.Inventory.UserInventory.Items.Count <= 6)
                {
                    ev.Player.AddItem(ItemType.MicroHID);
                    ev.Player.AddItem(ItemType.ParticleDisruptor);
                }

                if (ev.Player.Inventory.UserInventory.Items.Count <= 7)
                {
                    ev.Player.AddItem(ItemType.ParticleDisruptor);
                    ev.Player.Broadcast(5, "Pultroppo avevi solo uno slot libero! hai ricevuto solaemente la ParticleDiscruptor");
                }
                if (ev.Player.Inventory.UserInventory.Items.Count <= 8)
                {
                    ev.Player.Broadcast(5, "Non hai ricevuto niente perchè avevi l'inventario pieno! ti sei perso una Micro e una ParticleDiscruptor!");
                }
                break;
        }
    }
    private void OnDied(DiedEventArgs ev)
    {
        ev.Player.DisableEffect(EffectType.MovementBoost);
        ev.Player.Scale = Vector3.one;
    }
}

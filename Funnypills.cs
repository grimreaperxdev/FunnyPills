using System.Collections.Generic;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using CustomItems1.SCP094IT;
using UnityEngine;
using Exiled.Events.EventArgs.Player;

namespace TrollGranate;
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

[CustomItem(ItemType.Adrenaline)]
public class FunnyPills : CustomItem
{
    public override ItemType Type { get; set; } = ItemType.Adrenaline;
    public override uint Id { get; set; } = 1;

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
        Exiled.Events.Handlers.Map.PickupAdded += OnPickupAdded;
        Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        Exiled.Events.Handlers.Player.ChangingItem += OnChangingItem;
        Exiled.Events.Handlers.Player.Died += OnDied;
        base.SubscribeEvents();
    }
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
        Exiled.Events.Handlers.Map.PickupAdded -= OnPickupAdded;
        Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
        Exiled.Events.Handlers.Player.ChangingItem -= OnChangingItem;
        Exiled.Events.Handlers.Player.Died -= OnDied;
        base.UnsubscribeEvents();
    }
    private void OnUsingItem(Exiled.Events.EventArgs.Player.UsingItemEventArgs ev)
    {
        if (ev.Player.CurrentItem == null || ev.Player.CurrentItem.Type != ItemType.Adrenaline)
            return;

        int chance = Random.Range(0, 101);

        switch (chance)
        {
            case int n when (n < 10):
                ev.Player.Scale = new Vector3(2.0f, 2.0f, 2.0f);

                if (!ev.Player.IsDead)
                {
                    ev.Player.Scale = Vector3.one;
                }
                break;

        }
    }
    private void OnDied(DiedEventArgs ev)
    {
    }


    private void OnPickingUpItem(Exiled.Events.EventArgs.Player.PickingUpItemEventArgs ev)
    {
    }

    private void OnPickupAdded(Exiled.Events.EventArgs.Map.PickupAddedEventArgs ev)
    {
    }

    private void OnChangingItem(Exiled.Events.EventArgs.Player.ChangingItemEventArgs ev)
    {
    }
}
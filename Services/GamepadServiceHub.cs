﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorGamepad.Services {
    public class GamepadServiceHub : Hub<IGamePadService> {
        public static string ChannelName = "gamepad";

        public GamepadServiceHub() { }

        public async Task UpdateAsync(Gamepad[] gamepads) {
            await Clients.All.UpdateAsync(gamepads);
        }

        public async Task UpdateJsonAsync(JsonElement[] gamepadElements) {
            var gamepads = new List<Gamepad>();
            foreach (var gamepadElement in gamepadElements) {
                var rawString = gamepadElement.GetRawText();
                var gamepad = JsonSerializer.Deserialize<Gamepad>(rawString);
                gamepads.Add(gamepad);
            }
            await UpdateAsync(gamepads.ToArray());
        }
    }
}
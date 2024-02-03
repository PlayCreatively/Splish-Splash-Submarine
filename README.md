# Splish Splash Submarine

![Visual representation of the truth](splish_splash.png)

# GlobalSettings

- SPAWNER
  - spawning frequency
  - spawns[ ]
    - prefab (enemy)
      - HP
      - falling speed
      - movement speed
    - likelyhood
- RADAR
  - scan frequency
  - blip duration
- SHOOTING
  - bullet speed
  - reload time
<s>
- PLAYER
  - HP
  - movement speed
- SCREEN
  - visible ratio (20/80)
</s>

It's important to have a window of the GlobalSettings open on a side panel in order to change the fundemental game settings during testing for quick and iterative game design.

## How to set up the settings window

![How to set up a settings window](settings_window_setup.png)

## Creating Settings

![How to create settings](creating_settings.png)

1. Right click inside the `Assets/Resources/Settings` folder.
2. select `Create/Setting Objects/` and what settings you want to create.

You can also simply dublicate a setting by selecting the file and pressing `ctrl+D`.

`GameSettings` is the top most setting that contains all the other settings. Treat it like a setting for creating unique game modes by combining different settings to create a unique experience. Then call it something memorable like "Slow & Deliberate".

## Saving

NB! Though changes to the settings are registered in-game, they don't change on git until you explicitly save them using `ctrl+S` in the editor.

When iterating over different values, make sure that you push the changes you want on git and `Discard` or `Revert` changes you don't want to push.
BeatMania is a Game designed and developed as part of a masters program.

### Game Design ###
BeatMania is a rhythm-based 2D platformer with combat elements. The game takes place on an alien planet where everything moves to the rhythm 
of the world's music. The player also needs to match the rhythm in order to collect the pieces of their broken spaceship.

As the player explores the game world, using the rhythm-guided movement system, they encounter aliens that attack, help or ignore them. 
As the game progresses, the player unlocks new music. The songs have different rhythm speeds that impact the rhythm-guided movement system. 
When the music changes, the behavior of aliens and moving platforms also change, 
challenging the player to adapt to the new situation. 
For example, an alien that was previously helpful now attacks the player. Furthermore, the environment and 
aliens change their color pallet, which is matched to the music's atmosphere, to visually indicate their behavioral change.
Changing the world's music also allows the player to explore new game sections that were previously locked off, 
enabling further progression through the game. 

### Rhythm-Guided Movement System ###
The player's basic horizontal movement is independent of the rhythm. However, their other actions (i.e. jumping and attacking) receive a bonus when 
they are performed in union with the rhythm. For example, the player can always jump, but when they do so and miss the beat, their jump becomes much 
shorter. Conversely, beat-aligned jump is higher and allows for a second jump in the air (i.e. a double jump).

The player's combat system has two basic abilities: an attack and a shield. The shield is independent of the rhythm, 
while the attack receives a range and damage bonus when executed in union with the rhythm. Consecutive beat-aligned attacks 
lead to increased attack damage and a staggering effect, stopping the damaged alien from attacking. Concerning the shield component, 
the player can shield-attack or dash in order to perform further special beat-aligned attacks. 

The rhythm is visualized within the environment by trees and flowers that move and glow. Additionally, a beat indicator is provided, 
designed to have moving lines that form a circle on every beat. This circle glows white in the time interval where the player's input 
would count as beat-aligned. Once the player triggers an action, the circle flashes green or red depending on if the action was in union 
with the beat or not.

![BeatMania Picture](https://user-images.githubusercontent.com/25256413/220222765-572cae3b-6f20-4cdd-9182-808f72304cac.PNG)

[More Images](https://imgur.com/a/aivYn3d)

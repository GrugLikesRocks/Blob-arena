![image](https://github.com/GrugLikesRocks/Blob-arena/assets/92889945/8f1ff779-1b86-4c4c-8d9d-aecd404984b1)


# Blob Arena




# Overview

"Blob arena" is a mini-game designed for a game jam, featuring unique characters known as "Bloberts" engaging in strategic, Pokémon-like battles. Players will navigate through exciting encounters, against other players or against AI, using their Bloberts’ distinctive traits to outsmart and defeat opponents. The game’s core mechanic revolves around an enhanced rock-paper-scissors style combat modified by each character’s attributes.

## Game Concept

After players mint a Bloberts, grumpy yet fierce creatures, each boasting unique attributes that influence their combat effectiveness. The goal is to challenge and defeat opponents' Bloberts in tactical battles, with the victor claiming the loser’s character. The game focuses on strategy, prediction, and understanding the nuanced effects of Blobert attributes on combat outcomes.

Characters: Bloberts

Each Blobert character comes with a set of four primary attributes: Attack, Defence, Speed, and Strength. These attributes, which range from 1-5, significantly impact the combat mechanics.

* Attack: Determines the damage a Blobert can inflict with its moves.
* Defense: Reduces the amount of damage a Blobert receives from opponents' attacks.
* Speed: Decides which Blobert acts first in a battle.
* Strength: Multiplier for both the Attacker’s Attack or the Defender’s Defence.

## Combat Mechanics
Bloberts have access to three combat moves:

Beat: A powerful attack that can crush a Counter strategy.
Rush: A swift move that outpaces and defeats Beat.
Counter: A strategic defense that turns the tables on Rush.

### Two-Phase Combat System

#### Phase 1: Rock-Paper-Scissors Logic

* Beat, Rush, and Counter are the three moves available, with each having an inherent advantage over another:
  
   - Rush > Beat
   - Beat > Counter
   - Counter > Rush

The outcome of this phase determines the base result of the encounter, essentially who has the upper hand before attributes are applied.

#### Phase 2: Attribute Modifier Application

After establishing the initial advantage based on the rock-paper-scissors mechanic, the outcome is then adjusted by applying a multiplier derived from the Bloberts' attributes. This multiplier reflects the nuanced differences between each Blobert, considering their Attack, Strength, Defence, and Speed.


### Final Outcome Calculation

The final outcome of the battle takes the initial rock-paper-scissors result and applies the attribute multiplier to adjust the advantage. This can determine the extent of victory or mitigate a loss, depending on the multiplier's final value.

` Final Outcome = Initial RPS Outcome × Multiplier `

## Winning and Progression

The winner of a battle takes the opponent’s Blobert, adding it to their collection. This mechanic encourages players to think strategically about which Bloberts they bring into battle, considering the risk of losing their cherished characters against the potential reward of gaining new ones.

To enhance the initial player experience and introduce a competitive edge from the outset, "Blob Arena" will feature an AI Challenge Mode. This mode serves as the gateway for players to unlock the ability to battle against other real players' Bloberts. The AI Challenge Mode aims to test the player's strategic understanding and skill before entering the broader player-vs-player (PvP) arena.

### Mechanics

Initial Challenge: Upon starting the game for the first time, players are required to battle against an AI-controlled Blobert. This fight is mandatory to unlock PvP battles.

Consequences of Losing:

* If a player loses against the AI, their Blobert will be "injured" (unusable for a day), symbolizing the defeat and the need to improve their strategy.
  - Game Jam Exception: For the game jam version, if the player's collection consists of only one Blobert, the defeat will not result in the Blobert being injured. This ensures that all participants can continue enjoying the game without any risk.
    
Rewards for Winning:

* Defeating the AI-controlled Blobert awards the player with a +1 on its consecutive win streak which allows him to rank up the leaderboard.
  - Game Jam Exception: For the game jam version, defeating the AI-controlled Blobert awards the player the AI's Blobert, expanding their collection and signifying their readiness to challenge real-world opponents.
* Victory against the AI unlocks the PvP mode, allowing the player to engage in battles against Bloberts controlled by other players.
* If a player wins against another player, the victor will be able to claim the loser’s Blobert. 

### Monetization and Rewards Update

To add a layer of strategic investment and to incentivize competition, participation in battles within "Blob Arena" could incorporate a token-based entry fee and reward system using the game's virtual currency, $LORDS. This system is designed to not only elevate the stakes of each battle but also to cultivate a self-sustaining economy that rewards skill, strategy, and dedication.

#### Entry Fee Mechanism

$LORDS as Entry Fees: Players will need to spend a nominal amount of $LORDS to participate in battles, both against AI in the AI Challenge Mode and against other players in PvP mode. This entry fee is pooled to fund the rewards for the top competitors, as well as to support game maintenance and development.

Fee Amount: The specific amount of $LORDS required as an entry fee will be carefully calibrated to balance accessibility for new and casual players while ensuring meaningful rewards for competitive play.

#### Reward Distribution

Leaderboard Incentives: The top three players in both the AI Conquest Leaderboard and the PvP Victory Leaderboard will receive significant portions of the $LORDS collected from the entry fees. The distribution will be weighted, with the first-place player receiving the largest share, followed by the second and third-place players receiving smaller, yet substantial, portions of the pool.

Reward Allocation:

* First Place: Receives 50% of the pooled $LORDS.
* Second Place: Receives 30% of the pooled $LORDS.
* Third Place: Receives 20% of the pooled $LORDS.

### Leaderboards

To foster a competitive environment and recognize the accomplishments of players, "Blob Arena" will feature two distinct leaderboards:

AI Conquest Leaderboard: Tracks the players with the most consecutive wins against AI-controlled Bloberts. This leaderboard celebrates players who have mastered the game's mechanics and consistently outmaneuver the AI opponents.

PvP Victory Leaderboard: Highlights players who have achieved the most consecutive wins in player-vs-player battles. This leaderboard showcases the game's elite, highlighting those who excel in strategic planning and execution against other real-world players.

### Technical Specifications

Platform: Unity, Cairo, Dojo.

Graphics: Simplistic yet charming 2D sprites to represent the Bloberts and the battle arena.

Sound: Catchy, upbeat background music with distinct sound effects for each move to enhance the battle experience.

### Development Considerations

Balance: Careful adjustment of Blobert attributes and move effectiveness to ensure a fair and engaging gameplay experience.

Expansion: Potential for future updates with new Bloberts, moves, and battle arenas to keep the game fresh and engaging.

Community: Implementing a simple way for players to share their experiences and strategies could foster a supportive community around the game.

## Conclusion

"Blob Arena" aims to offer a compact yet thrilling tactical battle experience, inviting players to engage in strategic thinking, risk assessment, and the joy of collecting and battling with an ever-growing roster of Bloberts. This game design document provides a foundational overview of the game’s mechanics, goals, and development considerations, serving as a guidepost for the exciting journey of bringing "Blob Arena" to life.

# CodeCompetition.Sdk
The rules are simple. Code your bot to compete with others. You have 200 HP and 9 energy points per round.
In your round You can:
**Attack:**
* **Sensors** - 3 EP, Takes 10 HP
* **Head** - 2 EP, Takes 5 HP
* **Torso** - 1 EP, Takes 1 HP
* **Belly** - 1 EP, Takes 1 HP
 
 or **defend** above area(s) using 3 EP per area. If area is blocked, opponent will not give any damage when attacking it. 
 Defense is set per round for every opponent move.
 
 You can code your strategy having previous round informations such as:
 * **LastOpponentMoves** - What opponent did in previous round
 * **MyDamage** - Damage dealt
 * **OpponentDamage** - Damage taken
 
 You can also loose by:
 - Making a move above Your energy limit
 - Exception in Your code
 - Round timeout - 0,5s
 
 
 ## Testing Your bot code
 You can test Your bot in the small console application inside this project (CodeCompetition.TestingApp) where inside 
 PlayerBot.cs You can write Your algorithm and test it against two sample bots.
